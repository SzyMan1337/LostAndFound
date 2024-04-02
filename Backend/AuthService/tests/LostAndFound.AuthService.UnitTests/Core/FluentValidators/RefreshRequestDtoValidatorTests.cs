using FluentValidation.TestHelper;
using LostAndFound.AuthService.Core.FluentValidators;
using LostAndFound.AuthService.CoreLibrary.Requests;
using Xunit;

namespace LostAndFound.AuthService.UnitTests.Core.FluentValidators
{
    public class RefreshRequestDtoValidatorTests
    {
        private readonly RefreshRequestDtoValidator _refreshRequestDtoValidator;

        public RefreshRequestDtoValidatorTests()
        {
            _refreshRequestDtoValidator = new RefreshRequestDtoValidator();
        }

        [Theory]
        [MemberData(nameof(GetValidRefreshRequestDtos))]
        public void Validate_WithValidDto_ReturnsSuccess(RefreshRequestDto requestDto)
        {
            var result = _refreshRequestDtoValidator.TestValidate(requestDto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [MemberData(nameof(GetInvalidRefreshRequestDtos))]
        public void Validate_WithInalidDto_ReturnsFailure(RefreshRequestDto requestDto)
        {
            var result = _refreshRequestDtoValidator.TestValidate(requestDto);

            result.ShouldHaveAnyValidationError();
        }

        public static TheoryData<RefreshRequestDto> GetValidRefreshRequestDtos() => new()
        {
            new RefreshRequestDto()
            {
                RefreshToken = "refreshokenValid",
            },
            new RefreshRequestDto()
            {
                RefreshToken = "notrersat4w3452fdsf24",
            }
        };

        public static TheoryData<RefreshRequestDto> GetInvalidRefreshRequestDtos() => new()
        {
            new RefreshRequestDto()
            {
                RefreshToken = "",
            },
        };
    }
}
