using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreApp.Authentication.Jwt
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, IList<Claim> identity);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
