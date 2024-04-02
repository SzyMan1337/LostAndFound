using System.Security.Claims;

namespace LostAndFound.AuthService.Core.TokenGenerators
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(string secretKey, string issuer, string audience, DateTime utcExpirationTime, IEnumerable<Claim>? claims = null);
    }
}
