using FluentAssertions;
using LostAndFound.AuthService.Core.PasswordHashers;
using Microsoft.AspNetCore.Identity;
using System;
using Xunit;

namespace LostAndFound.AuthService.UnitTests.Core.PasswordHashers
{
    public class BCryptPasswordHasherTests
    {
        private readonly BCryptPasswordHasher<string> _bCryptPasswordHasher;

        public BCryptPasswordHasherTests()
        {
            _bCryptPasswordHasher = new BCryptPasswordHasher<string>();
        }


        [Theory]
        [MemberData(nameof(GetNullAndWhitespacePasswords))]
        public void HashPassword_WithNullOrWhitespacePassword_ThrowsArgumentNullException(string password)
        {
            Assert.Throws<ArgumentNullException>(() => _bCryptPasswordHasher.HashPassword("", password));
        }

        [Fact]
        public void HashPassword_WhenCalledMultipleTimesWithTheSamePassword_ReturnsDifferentHash()
        {
            var repeatedPassword = Guid.NewGuid().ToString();

            var hashedPassword1 = _bCryptPasswordHasher.HashPassword("", repeatedPassword);
            var hashedPassword2 = _bCryptPasswordHasher.HashPassword("", repeatedPassword);
            var hashedPassword3 = _bCryptPasswordHasher.HashPassword("", repeatedPassword);

            hashedPassword1.Should().NotBe(hashedPassword2).And.Should().NotBe(hashedPassword3);
            hashedPassword2.Should().NotBe(hashedPassword3);
        }

        [Fact]
        public void HashPassword_WithCorrectPassword_ReturnsVerifiableHash()
        {
            var password = Guid.NewGuid().ToString();

            var hashedPassword = _bCryptPasswordHasher.HashPassword("", password);

            BCrypt.Net.BCrypt.Verify(password, hashedPassword).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(GetNullAndWhitespacePasswords))]
        public void VerifyHashedPassword_WithNullOrWhitespaceHashedPassword_ThrowsArgumentNullException(string hashedPassword)
        {
            Assert.Throws<ArgumentNullException>(() => _bCryptPasswordHasher.VerifyHashedPassword("", hashedPassword, Guid.NewGuid().ToString()));
        }

        [Theory]
        [MemberData(nameof(GetNullAndWhitespacePasswords))]
        public void VerifyHashedPassword_WithNullOrWhitespacePassword_ThrowsArgumentNullException(string password)
        {
            Assert.Throws<ArgumentNullException>(() => _bCryptPasswordHasher.VerifyHashedPassword("", Guid.NewGuid().ToString(), password));
        }

        [Fact]
        public void VerifyHashedPassword_WithPasswordThatMatchHashedPassword_ReturnsSuccess()
        {
            var password = Guid.NewGuid().ToString();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, 12);

            var result = _bCryptPasswordHasher.VerifyHashedPassword("", hashedPassword, password);

            result.Should().Be(PasswordVerificationResult.Success);
        }

        [Fact]
        public void VerifyHashedPassword_WithPasswordThatDoesNotMatchHashedPassword_ReturnsFailed()
        {
            var password = Guid.NewGuid().ToString();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Guid.NewGuid().ToString(), 12);

            var result = _bCryptPasswordHasher.VerifyHashedPassword("", hashedPassword, password);

            result.Should().Be(PasswordVerificationResult.Failed);
        }

        [Fact]
        public void VerifyHashedPassword_WithPasswordHashedByLowerEntropy_ReturnsSuccessRehashNeeded()
        {
            var password = Guid.NewGuid().ToString();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, 10);

            var result = _bCryptPasswordHasher.VerifyHashedPassword("", hashedPassword, password);

            result.Should().Be(PasswordVerificationResult.SuccessRehashNeeded);
        }

        public static TheoryData<string?> GetNullAndWhitespacePasswords() => new()
        {
            { null },
            { string.Empty },
            { " " },
            { "   " },
        };
    }
}
