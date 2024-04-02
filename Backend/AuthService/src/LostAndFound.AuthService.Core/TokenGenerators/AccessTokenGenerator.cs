using LostAndFound.AuthService.Core.DateTimeProviders;
using LostAndFound.AuthService.CoreLibrary.Internal;
using LostAndFound.AuthService.CoreLibrary.Settings;
using LostAndFound.AuthService.DataAccess.Entities;
using System.Security.Claims;

namespace LostAndFound.AuthService.Core.TokenGenerators
{
    public class AccessTokenGenerator : IAccessTokenGenerator
    {
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IDateTimeProvider _dateTimeProvider;

        public AccessTokenGenerator(AuthenticationSettings authenticationSettings, IJwtTokenGenerator jwtTokenGenerator, IDateTimeProvider dateTimeProvider)
        {
            _authenticationSettings = authenticationSettings ?? throw new ArgumentNullException(nameof(authenticationSettings));
            _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public AccessToken GenerateAccessToken(Account account)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, account.UserId.ToString()),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim("Username", account.Username),
            };

            var expirationTime = _dateTimeProvider.UtcNow.AddMinutes(_authenticationSettings.AccessTokenExpirationMinutes);
            string accessToken = _jwtTokenGenerator.GenerateJwtToken(
                _authenticationSettings.AccessTokenSecret,
                _authenticationSettings.Issuer,
                _authenticationSettings.Audience,
                expirationTime,
                claims);

            return new AccessToken()
            {
                Value = accessToken,
                ExpirationTime = expirationTime,
            };
        }
    }
}
