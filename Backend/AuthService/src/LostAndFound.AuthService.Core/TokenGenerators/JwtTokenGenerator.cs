using LostAndFound.AuthService.Core.DateTimeProviders;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LostAndFound.AuthService.Core.TokenGenerators
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public readonly IDateTimeProvider _dateTimeProvider;

        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public string GenerateJwtToken(string secretKey, string issuer, string audience, DateTime utcExpirationTime, IEnumerable<Claim>? claims = null)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                _dateTimeProvider.UtcNow,
                utcExpirationTime,
                credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
