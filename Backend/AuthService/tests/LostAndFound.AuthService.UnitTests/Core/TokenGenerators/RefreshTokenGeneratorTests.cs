using FluentAssertions;
using LostAndFound.AuthService.Core.DateTimeProviders;
using LostAndFound.AuthService.Core.TokenGenerators;
using LostAndFound.AuthService.CoreLibrary.Settings;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace LostAndFound.AuthService.UnitTests.Core.TokenGenerators
{
    public class RefreshTokenGeneratorTests
    {
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly Mock<IJwtTokenGenerator> _mockedJwtTokenGeneratorMock;
        private readonly Mock<IDateTimeProvider> _mockedDateTimeProvider;
        private readonly DateTime _utcDateNowForTests;

        public RefreshTokenGeneratorTests()
        {
            _authenticationSettings = new AuthenticationSettings()
            {
                RefreshTokenSecret = "-SO079cbmTsrhr90g1WX5XgCYv8KonTtNNiqRwGQrl6Dm-uM9ehLU1lzkrb9xDD65rMieafbjpiYl94FWUl4wu1dxX4Axkr929j2Y1xdcH49Beqj4GNY6PXNNmilngKc82RcUEl727Ys3w5121yXy7LU-Oq0D9IQwIn51KzRTAs",
                Audience = "test-host",
                Issuer = "test-host",
                RefreshTokenExpirationMinutes = 30,
            };

            _mockedJwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
            _mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            _utcDateNowForTests = DateTime.UtcNow;
            _mockedDateTimeProvider.Setup(m => m.UtcNow).Returns(_utcDateNowForTests);
        }

        [Fact]
        public void GenerateRefreshToken_Execute_InvokeGenerateJwtTokenWithExpectedValues()
        {
            var expectedExpirationDate = _utcDateNowForTests.AddMinutes(_authenticationSettings.RefreshTokenExpirationMinutes);
            _mockedJwtTokenGeneratorMock.Setup(j =>
                j.GenerateJwtToken(
                    _authenticationSettings.RefreshTokenSecret,
                    _authenticationSettings.Issuer,
                    _authenticationSettings.Audience,
                    expectedExpirationDate,
                    null
                )).Returns<string, string, string, DateTime, IEnumerable<Claim>?>((_, _, _, _, _) => "refreshToken")
                .Verifiable();
            var refreshTokenGenerator = new RefreshTokenGenerator(_authenticationSettings, _mockedJwtTokenGeneratorMock.Object, _mockedDateTimeProvider.Object);

            var refreshToken = refreshTokenGenerator.GenerateRefreshToken();

            _mockedJwtTokenGeneratorMock.VerifyAll();
        }

        [Fact]
        public void GenerateRefreshToken_Execute_ReturnsExpectedRefreshToken()
        {
            var expectedRefreshToken = "mockedRefreshToken";
            _mockedJwtTokenGeneratorMock.Setup(j =>
                j.GenerateJwtToken(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<IEnumerable<Claim>?>()
                )).Returns<string, string, string, DateTime, IEnumerable<Claim>?>((_, _, _, _, _) => expectedRefreshToken);
            var refreshTokenGenerator = new RefreshTokenGenerator(_authenticationSettings, _mockedJwtTokenGeneratorMock.Object, _mockedDateTimeProvider.Object);

            var refreshToken = refreshTokenGenerator.GenerateRefreshToken();

            refreshToken.Should().Be(expectedRefreshToken);
        }

        [Fact]
        public void GenerateRefreshToken_Execute_AddsExpectedExpirationDate()
        {
            var expectedExpirationDate = _utcDateNowForTests.AddMinutes(_authenticationSettings.RefreshTokenExpirationMinutes);
            _mockedJwtTokenGeneratorMock.Setup(j =>
                j.GenerateJwtToken(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    expectedExpirationDate,
                    It.IsAny<IEnumerable<Claim>?>()
                )).Returns<string, string, string, DateTime, IEnumerable<Claim>?>((_, _, _, expirationDate, _) => expirationDate.ToString());
            var refreshTokenGenerator = new RefreshTokenGenerator(_authenticationSettings, _mockedJwtTokenGeneratorMock.Object, _mockedDateTimeProvider.Object);

            var refreshToken = refreshTokenGenerator.GenerateRefreshToken();

            refreshToken.Should().Be(expectedExpirationDate.ToString());
        }

        [Fact]
        public void RefreshTokenGeneratorConstructor_WithOneNullArguments_ThrowsArgumentNullException()
        {
            var actAuthenticationSettingsNull = () => new RefreshTokenGenerator(null!, _mockedJwtTokenGeneratorMock.Object, _mockedDateTimeProvider.Object);
            var actJwtTokenGeneratorNull = () => new RefreshTokenGenerator(_authenticationSettings!, null!, _mockedDateTimeProvider.Object);
            var actDateTimeProvider = () => new RefreshTokenGenerator(_authenticationSettings!, _mockedJwtTokenGeneratorMock.Object, null!);

            actAuthenticationSettingsNull.Should().Throw<ArgumentNullException>();
            actJwtTokenGeneratorNull.Should().Throw<ArgumentNullException>();
            actDateTimeProvider.Should().Throw<ArgumentNullException>();
        }
    }
}
