using CoreApp.Authentication.Jwt;
using CoreApp.Authentication.Models;
using CoreApp.Common.Constants;
using CoreApp.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CoreApp.Authentication.Helpers
{
    public static class TokenHelper
    {
        /// <summary>
        /// Generate Token
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="jwtFactory"></param>
        /// <param name="userName"></param>
        /// <param name="jwtOptions"></param>
        /// <param name="serializerSettings"></param>
        /// <returns></returns>
        public static async Task<string> GenerateJwt(IList<Claim> claims, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                id = claims.Single(c => c.Type == JwtConstants.ClaimIdentifiers.Id).Value,
                auth_token = await jwtFactory.GenerateEncodedToken(userName, claims),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static string GenerateRefreshToken(RefreshTokenInfo model, JsonSerializerSettings serializerSettings)
        {
            var modelString = JsonConvert.SerializeObject(model, serializerSettings);
            var modelByte = System.Text.Encoding.UTF8.GetBytes(modelString);
            return Convert.ToBase64String(modelByte);
        }

        public static RefreshTokenInfo Base64DecodeRefreshToken(string refreshToken)
        {
            var modelByte = System.Convert.FromBase64String(refreshToken);
            var modelString = System.Text.Encoding.UTF8.GetString(modelByte);
            return JsonConvert.DeserializeObject<RefreshTokenInfo>(modelString);
        }

        public static string GeneratePrivateKey()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public static ClaimsPrincipal GetPrincipalFromExpiredToken(IJwtFactory jwtFactory, string token)
        {
            return jwtFactory.GetPrincipalFromExpiredToken(token);
        }
    }
}
