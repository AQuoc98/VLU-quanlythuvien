using AutoMapper;
using CoreApp.Authentication.Helpers;
using CoreApp.Authentication.Models;
using CoreApp.Common.Enums;
using CoreApp.Common.Helpers;
using CoreApp.Common.Models;
using CoreApp.Domain.Systems.Interfaces;
using CoreApp.EntityFramework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreApp.Authentication.Jwt
{
    public class UserManager : IUserManager
    {
        #region Fields
        private readonly ICoreUserDm _userDm;
        private readonly ICoreModuleDm _moduleDm;
        private readonly IMapper _mapper;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ClaimsPrincipal _caller;
        #endregion

        #region Contructors
        public UserManager(ICoreUserDm userDm,
            ICoreModuleDm moduleDm,
            IMapper mapper,
            IJwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptions,
            IHttpContextAccessor httpContextAccessor)
        {
            _userDm = userDm;
            _mapper = mapper;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _caller = httpContextAccessor.HttpContext.User;
            _moduleDm = moduleDm;
        }
        #endregion

        #region Utilities

        /// <summary>
        /// Generate Claims Identity
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private IList<Claim> GenerateClaims(CoreUser user)
        {
            var claims = GetUserClaims(user);
            return claims;
        }

        /// <summary>
        /// Get user claim
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private IList<Claim> GetUserClaims(CoreUser user)
        {
            // User Claims
            var claims = new List<Claim>
            {
                new Claim(Common.Constants.JwtConstants.ClaimIdentifiers.Id, user.Id.ToString())
            };

            // Role Claims
            claims.AddRange(this.GetRoleClaims(user));

            return claims;
        }

        /// <summary>
        /// Get role claim
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private IList<Claim> GetRoleClaims(CoreUser user)
        {
            var claims = new List<Claim>();
            var roles = _userDm.GeRolesByUserId(user.Id).ToList();
            foreach (var role in roles)
            {
                claims.Add(new Claim(Common.Constants.JwtConstants.ClaimIdentifiers.Roles, role.Code));
            }

            // Module Claims
            claims.AddRange(this.GetModuleClaims(roles));

            return claims;
        }

        /// <summary>
        /// Get module claims
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        private IList<Claim> GetModuleClaims(IList<CoreRole> roles)
        {
            var claims = new List<Claim>();
            var roleIds = roles.Select(s => s.Id).ToArray();
            var modules = _moduleDm.GetModulesByRoleIds(roleIds);
            foreach (var module in modules)
            {
                var permission = module.Permissions.Select(s => s.Code).ToArray();
                claims.Add(new Claim(module.Code, JsonHelper.Serialize(permission)));
            }

            return claims;
        }

        /// <summary>
        /// Validate sign in info
        /// </summary>
        /// <param name="loginTypeCode"></param>
        /// <param name="identifier"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        private ResultModel<CoreUser> Validate(string loginTypeCode, string identifier, string secret)
        {
            var resultObj = new ResultModel<CoreUser>();

            var credentialType = _userDm.GetCredentialTypeByCode(loginTypeCode);
            if (credentialType == null)
            {
                resultObj.Status = ResultStatus.CredentialTypeInvalid;
                resultObj.Messages = new string[] { "Phương thức đăng nhập không chính xác." };
                return resultObj;
            }

            var credential = _userDm.GetCredential(identifier, secret, credentialType.Id);
            if (credential == null)
            {
                resultObj.Status = ResultStatus.CreadentialInvalid;
                resultObj.Messages = new string[] { "Tên đăng nhập hoặc mật khẩu không chính xác." };
                return resultObj;
            }

            var user = _userDm.GetById(credential.UserId);

            resultObj.Status = ResultStatus.Success;
            resultObj.ExtendData = _userDm.GetById(credential.UserId);

            return resultObj;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sign In
        /// </summary>
        /// <param name="signInModel"></param>
        /// <returns></returns>
        public async Task<ResultModel<JwtResult<CoreUser>>> SignIn(SignInModel signInModel)
        {
            var resultObj = new ResultModel<JwtResult<CoreUser>>();
            var validateResultObj = Validate(signInModel.LoginTypeCode, signInModel.Identifier, signInModel.Secret);

            if (validateResultObj.Status != ResultStatus.Success)
            {
                resultObj.Messages = validateResultObj.Messages;
                resultObj.Status = validateResultObj.Status;
                return resultObj;
            }

            var user = validateResultObj.ExtendData;
            var claims = GenerateClaims(user);
            //var principal = new ClaimsPrincipal(identity);
            var refreshTokenInfo = new RefreshTokenInfo()
            {
                UserId = user.Id,
                // Expired Date Refresh Token
                ExpiredDate = DateTime.Now.AddDays(37)
            };
            var jwt = await TokenHelper.GenerateJwt(claims, _jwtFactory, signInModel.Identifier, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            var result = new JwtResult<CoreUser>
            {
                User = user,
                Token = jwt,
                RefreshToken = TokenHelper.GenerateRefreshToken(refreshTokenInfo, new JsonSerializerSettings { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
            };
            resultObj.Status = ResultStatus.Success;
            resultObj.ExtendData = result;

            _userDm.Update(user);

            //result.User.CoreUserRoles.Clear();
            result.User.CoreCredentials.Clear();
            return resultObj;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            return TokenHelper.GetPrincipalFromExpiredToken(_jwtFactory, token);
        }

        public async Task<ResultModel<JwtResult<CoreUser>>> RefreshToken(CoreUser user)
        {
            var resultObj = new ResultModel<JwtResult<CoreUser>>();

            var claims = GenerateClaims(user);
            //var principal = new ClaimsPrincipal(identity);
            var refreshTokenInfo = new RefreshTokenInfo()
            {
                UserId = user.Id,
                // Expired Date Refresh Token
                ExpiredDate = DateTime.Now.AddDays(37)
            };
            var identifier = user.CoreCredentials.First().Identifier;
            var jwt = await TokenHelper.GenerateJwt(claims, _jwtFactory, identifier, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            var result = new JwtResult<CoreUser>
            {
                Token = jwt,
                RefreshToken = TokenHelper.GenerateRefreshToken(refreshTokenInfo, new JsonSerializerSettings { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
            };
            resultObj.Status = ResultStatus.Success;
            resultObj.ExtendData = result;

            _userDm.Update(user);
            return resultObj;
        }

        /// <summary>
        /// Get Current User Id
        /// </summary>
        /// <returns></returns>
        public Guid GetCurrentUserId()
        {
            var currentUser = GetCurrentUser();
            return currentUser.Id;
        }

        /// <summary>
        /// Current User Data
        /// </summary>
        /// <returns></returns>
        public CoreUser GetCurrentUser()
        {
            return _userDm.GetCurrentUser();
        }

        public string GetCurrentFireBaseIdentifier()
        {
            var identifier = _caller.Claims.SingleOrDefault(c => c.Type == Common.Constants.JwtConstants.ClaimIdentifiers.phone);
            return identifier?.Value ?? string.Empty;
        }
        #endregion
    }
}
