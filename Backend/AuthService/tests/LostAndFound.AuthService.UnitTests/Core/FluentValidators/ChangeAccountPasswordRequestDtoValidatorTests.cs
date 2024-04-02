using FluentValidation.TestHelper;
using LostAndFound.AuthService.Core.FluentValidators;
using LostAndFound.AuthService.CoreLibrary.Requests;
using Xunit;

namespace LostAndFound.AuthService.UnitTests.Core.FluentValidators
{
    public class ChangeAccountPasswordRequestDtoValidatorTests
    {
        private readonly ChangeAccountPasswordRequestDtoValidator _validator;

        public ChangeAccountPasswordRequestDtoValidatorTests()
        {
            _validator = new ChangeAccountPasswordRequestDtoValidator();
        }

        [Theory]
        [MemberData(nameof(GetValidRefreshRequestDtos))]
        public void Validate_WithValidDto_ReturnsSuccess(ChangeAccountPasswordRequestDto requestDto)
        {
            var result = _validator.TestValidate(requestDto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [MemberData(nameof(GetInvalidRefreshRequestDtos))]
        public void Validate_WithInalidDto_ReturnsFailure(ChangeAccountPasswordRequestDto requestDto)
        {
            var result = _validator.TestValidate(requestDto);

            result.ShouldHaveAnyValidationError();
        }

        public static TheoryData<ChangeAccountPasswordRequestDto> GetValidRefreshRequestDtos() => new()
        {
            new ChangeAccountPasswordRequestDto()
            {
                Password = "21312423412341234",
                NewPassword = "as123412341234da"
            },
            new ChangeAccountPasswordRequestDto()
            {
                Password = "41234123412341234",
                NewPassword = "as12341234123da"
            }
        };

        public static TheoryData<ChangeAccountPasswordRequestDto> GetInvalidRefreshRequestDtos() => new()
        {
            new ChangeAccountPasswordRequestDto()
            {
                Password = "",
                NewPassword = "asda"
            },
            new ChangeAccountPasswordRequestDto()
            {
                Password = "fffffffff",
                NewPassword = "asda"
            },
        };
    }
}
