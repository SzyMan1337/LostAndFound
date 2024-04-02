using FluentAssertions;
using FluentValidation.TestHelper;
using LostAndFound.AuthService.Core.FluentValidators;
using LostAndFound.AuthService.CoreLibrary.Requests;
using LostAndFound.AuthService.DataAccess.Repositories.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LostAndFound.AuthService.UnitTests.Core.FluentValidators
{
    public class RegisterUserRequestDtoValidatorTests
    {
        private readonly Mock<IAccountsRepository> _accountsRepositoryMock;

        public RegisterUserRequestDtoValidatorTests()
        {
            _accountsRepositoryMock = new Mock<IAccountsRepository>();
        }

        [Fact]
        public void Validate_DtoWithAlreadyUsedEmail_ReturnsFailure()
        {
            _accountsRepositoryMock
                .Setup(repo => repo.IsEmailInUse(It.IsAny<string>()))
                .Returns<string>(_ => true);
            _accountsRepositoryMock
                .Setup(repo => repo.IsUsernameInUse(It.IsAny<string>()))
                .Returns<string>(_ => false);

            var validator = new RegisterUserRequestDtoValidator(_accountsRepositoryMock.Object);
            var validDtoModel = GetValidRegisterUserDto();

            var result = validator.TestValidate(validDtoModel);

            result.ShouldHaveAnyValidationError();
            result.Errors.First().ErrorMessage.Should().Contain("e-mail jest już zajęty");
        }

        [Fact]
        public void Validate_DtoWithAlreadyUsedUsername_ReturnsFailure()
        {
            _accountsRepositoryMock
                .Setup(repo => repo.IsEmailInUse(It.IsAny<string>()))
                .Returns<string>(_ => false);
            _accountsRepositoryMock
                .Setup(repo => repo.IsUsernameInUse(It.IsAny<string>()))
                .Returns<string>(_ => true);
            var validator = new RegisterUserRequestDtoValidator(_accountsRepositoryMock.Object);
            RegisterUserAccountRequestDto validDtoModel = GetValidRegisterUserDto();

            var result = validator.TestValidate(validDtoModel);

            result.ShouldHaveAnyValidationError();
            result.Errors.First().ErrorMessage.Should().Contain("nazwa użytkownika jest już zajęt");
        }

        [Fact]
        public void Validate_DtoWithUsernameTooShort_ReturnsFailure()
        {
            _accountsRepositoryMock
                .Setup(repo => repo.IsEmailInUse(It.IsAny<string>()))
                .Returns<string>(_ => false);
            _accountsRepositoryMock
                .Setup(repo => repo.IsUsernameInUse(It.IsAny<string>()))
                .Returns<string>(_ => false);
            var validator = new RegisterUserRequestDtoValidator(_accountsRepositoryMock.Object);
            var validDtoModel = CreateRegisterUserRequestDto("goodEmail@gmail.com", "short", "password1111");

            var result = validator.TestValidate(validDtoModel);

            result.ShouldHaveAnyValidationError();
            result.Errors.First().ErrorMessage.Should().Contain("Nazwa użytkownika musi składać się z przynajmniej 8 znaków.");
        }

        [Fact]
        public void Validate_DtoWithPasswordTooShort_ReturnsFailure()
        {
            _accountsRepositoryMock
                .Setup(repo => repo.IsEmailInUse(It.IsAny<string>()))
                .Returns<string>(_ => false);
            _accountsRepositoryMock
                .Setup(repo => repo.IsUsernameInUse(It.IsAny<string>()))
                .Returns<string>(_ => false);
            var validator = new RegisterUserRequestDtoValidator(_accountsRepositoryMock.Object);
            var validDtoModel = CreateRegisterUserRequestDto("goodEmail@gmail.com", "User12312", "pass1");

            var result = validator.TestValidate(validDtoModel);

            result.ShouldHaveAnyValidationError();
            result.Errors.First().ErrorMessage.Should().Contain("Hasło musi składać się z przynajmniej 8 znaków.");
        }

        [Theory]
        [MemberData(nameof(GetValidRegisterUserRequestDtos))]
        public void Validate_WithValidDto_ReturnsSuccess(RegisterUserAccountRequestDto requestDto)
        {
            _accountsRepositoryMock
                .Setup(repo => repo.IsEmailInUse(It.IsAny<string>()))
                .Returns<string>(_ => false);
            _accountsRepositoryMock
                .Setup(repo => repo.IsUsernameInUse(It.IsAny<string>()))
                .Returns<string>(_ => false);
            var validator = new RegisterUserRequestDtoValidator(_accountsRepositoryMock.Object);

            var result = validator.TestValidate(requestDto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [MemberData(nameof(GetInvalidRegisterUserRequestDtos))]
        public void Validate_WithInalidDto_ReturnsFailure(RegisterUserAccountRequestDto requestDto)
        {
            _accountsRepositoryMock
                .Setup(repo => repo.IsEmailInUse(It.IsAny<string>()))
                .Returns<string>(_ => false);
            _accountsRepositoryMock
                .Setup(repo => repo.IsUsernameInUse(It.IsAny<string>()))
                .Returns<string>(_ => false);
            var validator = new RegisterUserRequestDtoValidator(_accountsRepositoryMock.Object);

            var result = validator.TestValidate(requestDto);

            result.ShouldHaveAnyValidationError();
        }

        public static IEnumerable<object[]> GetValidRegisterUserRequestDtos()
        {
            yield return new object[]
            {
                CreateRegisterUserRequestDto("goodEmail@gmail.com", "goodUsername123", "password123")
            };

            yield return new object[]
            {
                CreateRegisterUserRequestDto("goodEmailwqwe@gmail.com", "simple1314321@!@#!4322", "password1234111")
            };
        }

        public static IEnumerable<object[]> GetInvalidRegisterUserRequestDtos()
        {
            yield return new object[]
            {
                CreateRegisterUserRequestDto("NotAnEmail", "goodUsername123", "password123")
            };

            yield return new object[]
            {
                CreateRegisterUserRequestDto("NotAnEmail", "", "password123")
            };

            yield return new object[]
            {
                CreateRegisterUserRequestDto("emailCorrect@gg.pl", "", "password123")
            };

            yield return new object[]
            {
                CreateRegisterUserRequestDto("emailCorrect@gg.pl", "user12312", "")
            };

            yield return new object[]
            {
                CreateRegisterUserRequestDto("emailCorrect@gg.pl", "user12312", "koko")
            };
        }

        private static RegisterUserAccountRequestDto CreateRegisterUserRequestDto(string email, string usernam, string password)
        {
            return new RegisterUserAccountRequestDto()
            {
                Email = email,
                Username = usernam,
                Password = password
            };
        }

        private static RegisterUserAccountRequestDto GetValidRegisterUserDto()
        {
            return new RegisterUserAccountRequestDto()
            {
                Email = "goodEmail@gmail.com",
                Username = "goodUsername123",
                Password = "password123"
            };
        }
    }
}
