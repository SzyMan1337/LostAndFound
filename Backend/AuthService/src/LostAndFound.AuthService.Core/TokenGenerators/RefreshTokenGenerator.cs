using LostAndFound.AuthService.Core.DateTimeProviders;
using LostAndFound.AuthService.CoreLibrary.Settings;

namespace LostAndFound.AuthService.Core.TokenGenerators
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public readonly IDateTimeProvider _dateTimeProvider;

        public RefreshTokenGenerator(AuthenticationSettings authenticationSettings, IJwtTokenGenerator jwtTokenGenerator, IDateTimeProvider dateTimeProvider)
        {
            _authenticationSettings = authenticationSettings ?? throw new ArgumentNullException(nameof(authenticationSettings));
            _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider)); ;
        }

        public string GenerateRefreshToken()
        {
            var expirationTime = _dateTimeProvider.UtcNow.AddMinutes(_authenticationSettings.RefreshTokenExpirationMinutes);

            return _jwtTokenGenerator.GenerateJwtToken(
                _authenticationSettings.RefreshTokenSecret,
                _authenticationSettings.Issuer,
                _authenticationSettings.Audience,
                expirationTime);
        }
    }
}
