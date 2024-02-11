using Microsoft.IdentityModel.Tokens;
using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.Util;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace ObjectiveManagerApp.UI.Services
{
    public class TokenService : ITokenService
    {
        private const int TokenExpirationTime = 60;

        public async Task<string> GetTokenAsync(TokenPayload payload)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, payload.Username),
                new Claim(ClaimTypes.Role, payload.RoleName)
            };

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(TokenExpirationTime)),
                    signingCredentials: new SigningCredentials(
                        AuthOptions.GetSymmetricSecurityKey(),
                        SecurityAlgorithms.HmacSha256
                    ));

            return await Task.Run(() => { return new JwtSecurityTokenHandler().WriteToken(jwt); });
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            return await Task.Run(() =>
            {
                SecurityToken validatedToken;
                IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                return principal.Identity != null && principal.Identity.IsAuthenticated;
            });
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = AuthOptions.Issuer,
                ValidAudience = AuthOptions.Audience,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
            };
        }
    }
}
