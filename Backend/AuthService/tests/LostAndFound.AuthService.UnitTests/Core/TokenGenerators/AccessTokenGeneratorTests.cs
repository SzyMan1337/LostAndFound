using FluentAssertions;
using LostAndFound.AuthService.Core.DateTimeProviders;
using LostAndFound.AuthService.Core.TokenGenerators;
using LostAndFound.AuthService.CoreLibrary.Settings;
using LostAndFound.AuthService.DataAccess.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace LostAndFound.AuthService.UnitTests.Core.TokenGenerators
{
    public class AccessTokenGeneratorTests
    {
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly Mock<IJwtTokenGenerator> _mockedJwtTokenGeneratorMock;
        private readonly Mock<IDateTimeProvider> _mockedDateTimeProvider;
        private readonly DateTime _utcDateNowForTests;

        public AccessTokenGeneratorTests()
        {
            _authenticationSettings = new AuthenticationSettings()
            {
                AccessTokenSecret = "-SO079cbmTsrhr90g1WX5XgCYv8KonTtNNiqRwGQrl6Dm-uM9ehLU1lzkrb9xDD65rMieafbjpiYl94FWUl4wu1dxX4Axkr929j2Y1xdcH49Beqj4GNY6PXNNmilngKc82RcUEl727Ys3w5121yXy7LU-Oq0D9IQwIn51KzRTAs",
                Audience = "test-host",
                Issuer = "test-host",
                AccessTokenExpirationMinutes = 30,
            };

            _mockedJwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
            _mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            _utcDateNowForTests = DateTime.UtcNow;
            _mockedDateTimeProvider.Setup(m => m.UtcNow).Returns(_utcDateNowForTests);
        }

        [Fact]
        public void GenerateAccessToken_Execute_ReturnModelWithExpectedExpirationDate()
        {
            var expectedExpirationDate = _utcDateNowForTests.AddMinutes(_authenticationSettings.AccessTokenExpirationMinutes);
            _mockedJwtTokenGeneratorMock.Setup(j =>
                j.GenerateJwtToken(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    expectedExpirationDate,
                    It.IsAny<IEnumerable<Claim>?>()
                )).Returns<string, string, string, DateTime, IEnumerable<Claim>?>((_, _, _, _, _) => "");
            var accessTokenGenerator = new AccessTokenGenerator(_authenticationSettings, _mockedJwtTokenGeneratorMock.Object, _mockedDateTimeProvider.Object);
            var validUserModel = GetUserModel();

            var accessToken = accessTokenGenerator.GenerateAccessToken(validUserModel);

            ((int)(accessToken.ExpirationTime - expectedExpirationDate).TotalSeconds).Should().Be(0);
        }

        [Fact]
        public void GenerateRefreshToken_Execute_ReturnsExpectedAccessTokenValue()
        {
            _mockedJwtTokenGeneratorMock.Setup(j =>
                j.GenerateJwtToken(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<IEnumerable<Claim>?>()
                )).Returns<string, string, string, DateTime, IEnumerable<Claim>?>((_, _, _, _, _) => "accessToken");
            var accessTokenGenerator = new AccessTokenGenerator(_authenticationSettings, _mockedJwtTokenGeneratorMock.Object, _mockedDateTimeProvider.Object);
            var validUserModel = GetUserModel();

            var accessToken = accessTokenGenerator.GenerateAccessToken(validUserModel);

            accessToken.Value.Should().Be("accessToken");
        }

        [Fact]
        public void GenerateRefreshToken_Execute_InvokeGenerateJwtTokenWithExpectedValues()
        {
            var expectedExpirationDate = _utcDateNowForTests.AddMinutes(_authenticationSettings.AccessTokenExpirationMinutes);
            Account account = GetUserModel();
            IEnumerable<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, account.UserId.ToString()),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim("Username", account.Username),
            };

            _mockedJwtTokenGeneratorMock.Setup(j =>
                j.GenerateJwtToken(
                    _authenticationSettings.AccessTokenSecret,
                    _authenticationSettings.Issuer,
                    _authenticationSettings.Audience,
                    expectedExpirationDate,
                    It.Is<IEnumerable<Claim>?>(x => CompareListOfClaims(x, claims))
                )).Returns<string, string, string, DateTime, IEnumerable<Claim>?>((_, _, _, _, _) => "accessToken")
                .Verifiable();
            var accessTokenGenerator = new AccessTokenGenerator(_authenticationSettings, _mockedJwtTokenGeneratorMock.Object, _mockedDateTimeProvider.Object);

            var accessToken = accessTokenGenerator.GenerateAccessToken(account);

            _mockedJwtTokenGeneratorMock.VerifyAll();
        }

        [Fact]
        public void AccessTokenGeneratorConstructor_WithOneNullArguments_ThrowsArgumentNullException()
        {
            var actAuthenticationSettingsNull = () => new RefreshTokenGenerator(null!, _mockedJwtTokenGeneratorMock.Object, _mockedDateTimeProvider.Object);
            var actJwtTokenGeneratorNull = () => new RefreshTokenGenerator(_authenticationSettings!, null!, _mockedDateTimeProvider.Object);
            var actDateTimeProvider = () => new RefreshTokenGenerator(_authenticationSettings!, _mockedJwtTokenGeneratorMock.Object, null!);

            actAuthenticationSettingsNull.Should().Throw<ArgumentNullException>();
            actJwtTokenGeneratorNull.Should().Throw<ArgumentNullException>();
            actDateTimeProvider.Should().Throw<ArgumentNullException>();
        }

        private static bool CompareListOfClaims(IEnumerable<Claim>? l1, IEnumerable<Claim> l2)
        {
            if (l1 == null)
            {
                return false;
            }

            foreach (var claim in l2)
            {
                if (l1.FirstOrDefault(c => c.Type == claim.Type && c.Value == claim.Value) == null)
                {
                    return false;
                }
            }
            return true;
        }

        private static Account GetUserModel()
        {
            return new Account()
            {
                Email = "email@gmail.com",
                Username = "koko19",
            };
        }
    }
}
