using LostAndFound.AuthService.CoreLibrary.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LostAndFound.AuthService.Core.TokenValidators
{
    public class RefreshTokenValidator : IRefreshTokenValidator
    {
        private readonly AuthenticationSettings _authenticationSettings;

        public RefreshTokenValidator(AuthenticationSettings authenticationSettings)
        {
            _authenticationSettings = authenticationSettings ?? throw new ArgumentNullException(nameof(authenticationSettings));
        }

        public bool ValidateRefreshToken(string refreshToken)
        {
            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.RefreshTokenSecret)),
                ValidIssuer = _authenticationSettings.Issuer,
                ValidAudience = _authenticationSettings.Audience,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
