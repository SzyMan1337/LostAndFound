using FluentAssertions;
using LostAndFound.AuthService.Core.DateTimeProviders;
using LostAndFound.AuthService.Core.TokenGenerators;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace LostAndFound.AuthService.UnitTests.Core.TokenGenerators
{
    public class JwtTokenGeneratorTests
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly string _secretForTests = "-SO079cbmTsrhr90g1WX5XgCYv8KonTtNNiqRwGQrl6Dm-uM9ehLU1lzkrb9xDD65rMieafbjpiYl94FWUl4wu1dxX4Axkr929j2Y1xdcH49Beqj4GNY6PXNNmilngKc82RcUEl727Ys3w5121yXy7LU-Oq0D9IQwIn51KzRTAs";
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly DateTime _utcDateNowForTests;

        public JwtTokenGeneratorTests()
        {
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            _utcDateNowForTests = DateTime.UtcNow;
            mockedDateTimeProvider.Setup(m => m.UtcNow).Returns(_utcDateNowForTests);

            _jwtTokenGenerator = new JwtTokenGenerator(mockedDateTimeProvider.Object);
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        [Fact]
        public void GenerateJwtToken_Execution_ReturnsTokenWithExpectedIssuerAndAudience()
        {
            var mockedIssuer = "issuer-mocked";
            var mockedAudience = "audience-mocked";
            var jwtToken = _jwtTokenGenerator.GenerateJwtToken(
                _secretForTests,
                mockedIssuer,
                mockedAudience,
                DateTime.UtcNow.AddHours(1));

            var decodedToken = _jwtSecurityTokenHandler.ReadJwtToken(jwtToken);
            decodedToken.Issuer.Should().Be(mockedIssuer);
            decodedToken.Audiences.Should().Contain(mockedAudience);
        }

        [Fact]
        public void GenerateJwtToken_Execution_ReturnsTokenWithExpectedExpirationDate()
        {
            var expectedExpirationDate = _utcDateNowForTests.AddMinutes(30);
            var jwtToken = _jwtTokenGenerator.GenerateJwtToken(
                _secretForTests,
                "",
                "",
                expectedExpirationDate);

            var decodedToken = _jwtSecurityTokenHandler.ReadJwtToken(jwtToken);
            ((int)(decodedToken.ValidTo - expectedExpirationDate).TotalSeconds).Should().Be(0);
        }

        [Fact]
        public void GenerateJwtToken_Execution_ReturnsTokenWithExpectedCreationDate()
        {
            var jwtToken = _jwtTokenGenerator.GenerateJwtToken(
                _secretForTests,
                "",
                "",
                DateTime.UtcNow.AddHours(1));

            var decodedToken = _jwtSecurityTokenHandler.ReadJwtToken(jwtToken);
            ((int)(decodedToken.ValidFrom - _utcDateNowForTests).TotalSeconds).Should().Be(0);
        }

        [Fact]
        public void GenerateJwtToken_Execution_ReturnsTokenWithExpectedClaims()
        {
            var claims = new List<Claim>()
            {
                new Claim("id", "userId"),
                new Claim("email", "userEmail"),
            };
            var jwtToken = _jwtTokenGenerator.GenerateJwtToken(
                _secretForTests,
                "",
                "",
                DateTime.UtcNow.AddHours(1),
                claims);

            var decodedToken = _jwtSecurityTokenHandler.ReadJwtToken(jwtToken);
            decodedToken.Claims.FirstOrDefault(c => c.Type == "id" && c.Value == "userId").Should().NotBeNull();
            decodedToken.Claims.FirstOrDefault(c => c.Type == "email" && c.Value == "userEmail").Should().NotBeNull();
        }

        [Fact]
        public void GenerateJwtToken_Execution_ReturnsValidToken()
        {
            var jwtToken = _jwtTokenGenerator.GenerateJwtToken(
                _secretForTests,
                "testIssuer",
                "testAudience",
                DateTime.UtcNow.AddHours(1));
            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretForTests)),
                ValidAudience = "testAudience",
                ValidIssuer = "testIssuer"
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(jwtToken, validationParameters, out SecurityToken validatedToken);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }

        [Fact]
        public void JwtTokenGeneratorConstructor_WithNullDateTimeProvider_ThrowsArgumentNullException()
        {
            var act = () => new JwtTokenGenerator(null!);

            act.Should().Throw<ArgumentNullException>();
        }
    }
}
