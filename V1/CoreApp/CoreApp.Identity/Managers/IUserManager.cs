using CoreApp.Common.Enums;
using CoreApp.Identity.Models;
using CoreAppDbContext;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CoreApp.Identity.Managers
{
    public interface IUserManager
    {
        Task<LoginResult> SignIn(HttpContext httpContext, SignInModel signInModel);
        Task SignOut(HttpContext httpContext);
        long GetCurrentUserId(HttpContext httpContext);
        CoreUser GetCurrentUser(HttpContext httpContext);
    }
}
