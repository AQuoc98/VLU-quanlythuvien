using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using CoreApp.Domain.Systems.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using CoreAppDbContext;
using CoreApp.Identity.Models;
using CoreApp.Common.Enums;
using System.Threading.Tasks;

namespace CoreApp.Identity.Managers
{
    public class UserManager : IUserManager
    {
        #region Fields
        private readonly ICoreUserDm _userDm;
        private readonly IMapper _mapper;
        #endregion

        #region Contructors
        public UserManager(ICoreUserDm userDm, IMapper mapper)
        {
            _userDm = userDm;
            _mapper = mapper;
        }
        #endregion

        #region Utilities
        private IEnumerable<Claim> GetUserClaims(CoreUser user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            claims.AddRange(this.GetUserRoleClaims(user));
            return claims;
        }

        private IEnumerable<Claim> GetUserRoleClaims(CoreUser user)
        {
            List<Claim> claims = new List<Claim>();
            var roles = _userDm.GeRolesByUserId(user.Id).ToList();
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Code));
                claims.AddRange(this.GetUserPermissionClaims(role));
            }

            return claims;
        }

        private IEnumerable<Claim> GetUserPermissionClaims(CoreRole role)
        {
            var claims = new List<Claim>();
            var permissions = _userDm.GetPermissionsByRoleId(role.Id).ToList();

            foreach (var permission in permissions)
            {
                claims.Add(new Claim("Permission", permission.Code));
            }

            return claims;
        }
        private CoreUser Validate(string loginTypeCode, string identifier, string secret)
        {
            var credentialType = _userDm.GetCredentialTypeByCode(loginTypeCode);
            if (credentialType == null)
                return null;

            var credential = _userDm.GetCredential(identifier, secret, credentialType.Id);
            if (credential == null)
                return null;

            return _userDm.GetById(credential.UserId);
        }
        #endregion

        #region Methods
        public async Task<LoginResult> SignIn(HttpContext httpContext, SignInModel signInModel)
        {
            var user = Validate(signInModel.LoginTypeCode, signInModel.Identifier, signInModel.Secret);
            if (user == null)
                return LoginResult.Failure;

            var identity = new ClaimsIdentity(GetUserClaims(user), CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await httpContext.SignInAsync(
              CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() { IsPersistent = signInModel.IsPersistent }
            );

            return LoginResult.Succeeded;
        }
        public async Task SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        public long GetCurrentUserId(HttpContext httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return -1;

            Claim claim = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
                return -1;


            if (!long.TryParse(claim.Value, out long currentUserId))
                return -1;

            return currentUserId;
        }
        public CoreUser GetCurrentUser(HttpContext httpContext)
        {
            var currentUserId = GetCurrentUserId(httpContext);

            if (currentUserId == -1)
                return null;

            return _userDm.GetById(currentUserId);
        }
        #endregion
    }
}
