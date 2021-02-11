using CoreApp.Authentication.Models;
using CoreApp.Common.Models;
using CoreApp.EntityFramework.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreApp.Authentication.Jwt
{
    public interface IUserManager
    {
        Task<ResultModel<JwtResult<CoreUser>>> SignIn(SignInModel signInModel);
        Guid GetCurrentUserId();
        CoreUser GetCurrentUser();
        string GetCurrentFireBaseIdentifier();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task<ResultModel<JwtResult<CoreUser>>> RefreshToken(CoreUser user);
    }
}
