using FluentAssertions;
using LostAndFound.AuthService.Core.TokenValidators;
using LostAndFound.AuthService.CoreLibrary.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Xunit;

namespace LostAndFound.AuthService.UnitTests.Core.TokenValidators
{
    public class RefreshTokenValidatorTests
    {
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly SigningCredentials _signingCredentials;

        public RefreshTokenValidatorTests()
        {
            _authenticationSettings = new AuthenticationSettings()
            {
                RefreshTokenSecret = "-SO079cbmTsrhr90g1WX5XgCYv8KonTtNNiqRwGQrl6Dm-uM9ehLU1lzkrb9xDD65rMieafbjpiYl94FWUl4wu1dxX4Axkr929j2Y1xdcH49Beqj4GNY6PXNNmilngKc82RcUEl727Ys3w5121yXy7LU-Oq0D9IQwIn51KzRTAs",
                Audience = "test-host",
                Issuer = "test-host",
            };
            _refreshTokenValidator = new RefreshTokenValidator(_authenticationSettings);

            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.RefreshTokenSecret));
            _signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("wrong")]
        [InlineData("123")]
        public void ValidateRefreshToken_WithIncorrectRefreshToken_ReturnsFalse(string refreshToken)
        {
            var result = _refreshTokenValidator.ValidateRefreshToken(refreshToken);

            result.Should().BeFalse();
        }

        [Fact]
        public void ValidateRefreshToken_WithCorrectRefreshToken_ReturnsTrue()
        {
            var token = new JwtSecurityToken(
                _authenticationSettings.Issuer,
                _authenticationSettings.Audience,
                null,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(30),
                _signingCredentials);
            var refreshToken = new JwtSecurityTokenHandler().WriteToken(token);

            var result = _refreshTokenValidator.ValidateRefreshToken(refreshToken);

            result.Should().BeTrue();
        }

        [Fact]
        public void ValidateRefreshToken_WithExpiredRefreshToken_ReturnsFalse()
        {
            var token = new JwtSecurityToken(
                _authenticationSettings.Issuer,
                _authenticationSettings.Audience,
                null,
                DateTime.UtcNow.AddMinutes(-31),
                DateTime.UtcNow.AddMinutes(-30),
                _signingCredentials);
            var refreshToken = new JwtSecurityTokenHandler().WriteToken(token);

            var result = _refreshTokenValidator.ValidateRefreshToken(refreshToken);

            result.Should().BeFalse();
        }

        [Fact]
        public void ValidateRefreshToken_WithIncorrectIssuer_ReturnsFalse()
        {
            var token = new JwtSecurityToken(
                "wrong_issuer",
                _authenticationSettings.Audience,
                null,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(30),
                _signingCredentials);
            var refreshToken = new JwtSecurityTokenHandler().WriteToken(token);

            var result = _refreshTokenValidator.ValidateRefreshToken(refreshToken);

            result.Should().BeFalse();
        }

        [Fact]
        public void ValidateRefreshToken_WithIncorrectAudience_ReturnsFalse()
        {
            var token = new JwtSecurityToken(
                _authenticationSettings.Issuer,
                "wrong_audience",
                null,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(30),
                _signingCredentials);
            var refreshToken = new JwtSecurityTokenHandler().WriteToken(token);

            var result = _refreshTokenValidator.ValidateRefreshToken(refreshToken);

            result.Should().BeFalse();
        }

        [Fact]
        public void ValidateRefreshToken_WithSigningCredentialsBasedOnDifferentRefreshTokenSecret_ReturnsFalse()
        {
            var incorrectRefreshTokenSecret = "zGRtr1lcMq0NUAGm0ICsEqXyBuUI-fz8mEoPzYaXlRARt9eEqnYBQM5VWFpbb7E3ogcImCv4kZeRST6iQYCv_O5-HOKDqhzCmYAf98o-mordvPE9BeIyO9VpZq57a1S149lNGBs0tOL46PBeD1zfkxg5eLHOZ6HYsi_seuNEMKc";
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(incorrectRefreshTokenSecret));
            var incorrectSigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _authenticationSettings.Issuer,
                _authenticationSettings.Audience,
                null,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(30),
                incorrectSigningCredentials);
            var refreshToken = new JwtSecurityTokenHandler().WriteToken(token);

            var result = _refreshTokenValidator.ValidateRefreshToken(refreshToken);

            result.Should().BeFalse();
        }

        [Fact]
        public void ValidateRefreshToken_ExecutedTwoTimes_ReturnsTheSameResults()
        {
            var token = new JwtSecurityToken(
                _authenticationSettings.Issuer,
                _authenticationSettings.Audience,
                null,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(30),
                _signingCredentials);
            var refreshToken = new JwtSecurityTokenHandler().WriteToken(token);

            var result = _refreshTokenValidator.ValidateRefreshToken(refreshToken);
            var result2 = _refreshTokenValidator.ValidateRefreshToken(refreshToken);

            result.Should().Be(result2);
        }

        [Fact]
        public void RefreshTokenValidatorConstructor_WithNullAuthenticationSettings_ThrowsArgumentNullException()
        {
            var act = () => new RefreshTokenValidator(null!);

            act.Should().Throw<ArgumentNullException>();
        }
    }
}
