using FluentValidation.TestHelper;
using LostAndFound.AuthService.Core.FluentValidators;
using LostAndFound.AuthService.CoreLibrary.Requests;
using Xunit;

namespace LostAndFound.AuthService.UnitTests.Core.FluentValidators
{
    public class LoginRequestDtoValidatorTests
    {
        private readonly LoginRequestDtoValidator _loginRequestDtoValidator;

        public LoginRequestDtoValidatorTests()
        {
            _loginRequestDtoValidator = new LoginRequestDtoValidator();
        }

        [Theory]
        [MemberData(nameof(GetValidLoginRequestDtos))]
        public void Validate_WithValidDto_ReturnsSuccess(LoginRequestDto requestDto)
        {
            var result = _loginRequestDtoValidator.TestValidate(requestDto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [MemberData(nameof(GetInvalidLoginRequestDtos))]
        public void Validate_WithInalidDto_ReturnsFailure(LoginRequestDto requestDto)
        {
            var result = _loginRequestDtoValidator.TestValidate(requestDto);

            result.ShouldHaveAnyValidationError();
        }

        public static TheoryData<LoginRequestDto> GetValidLoginRequestDtos() => new()
        {
            new LoginRequestDto()
            {
                Email = "correct@email.pl",
                Password = "secure-Password123",
            },
            new LoginRequestDto()
            {
                Email = "anotherEmail213@email.pl",
                Password = "goodPassword1!",
            }
        };

        public static TheoryData<LoginRequestDto> GetInvalidLoginRequestDtos() => new()
        {
            new LoginRequestDto()
            {
                Email = "",
                Password = "",
            },
            new LoginRequestDto()
            {
                Email = "correctEmail@email.pl",
                Password = "!",
            },
            new LoginRequestDto()
            {
                Email = "anr",
                Password = "!",
            }
        };
    }
}
