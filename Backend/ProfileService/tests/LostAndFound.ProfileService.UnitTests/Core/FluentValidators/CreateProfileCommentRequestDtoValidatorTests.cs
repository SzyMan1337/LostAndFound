using FluentValidation.TestHelper;
using LostAndFound.ProfileService.Core.FluentValidators;
using LostAndFound.ProfileService.CoreLibrary.Requests;
using Xunit;

namespace LostAndFound.ProfileService.UnitTests.Core.FluentValidators
{
    public class CreateProfileCommentRequestDtoValidatorTests
    {
        private readonly CreateProfileCommentRequestDtoValidator _validator;

        public CreateProfileCommentRequestDtoValidatorTests()
        {
            _validator = new CreateProfileCommentRequestDtoValidator();
        }

        [Theory]
        [MemberData(nameof(GetValidLoginRequestDtos))]
        public void Validate_WithValidDto_ReturnsSuccess(CreateProfileCommentRequestDto requestDto)
        {
            var result = _validator.TestValidate(requestDto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [MemberData(nameof(GetInvalidLoginRequestDtos))]
        public void Validate_WithInalidDto_ReturnsFailure(CreateProfileCommentRequestDto requestDto)
        {
            var result = _validator.TestValidate(requestDto);

            result.ShouldHaveAnyValidationError();
        }

        public static TheoryData<CreateProfileCommentRequestDto> GetValidLoginRequestDtos() => new()
        {
            new CreateProfileCommentRequestDto()
            {
                Content = "Nice job",
                ProfileRating = 0f,
            },
            new CreateProfileCommentRequestDto()
            {
                Content = "Nice job",
                ProfileRating = 5f,
            },
            new CreateProfileCommentRequestDto()
            {
                Content = "Thanks",
                ProfileRating = 3.5f,
            }
        };

        public static TheoryData<CreateProfileCommentRequestDto> GetInvalidLoginRequestDtos() => new()
        {
            new CreateProfileCommentRequestDto()
            {
                Content = "",
                ProfileRating = 2f,
            },
            new CreateProfileCommentRequestDto()
            {
                Content = "Nice job",
                ProfileRating = -5f,
            },
            new CreateProfileCommentRequestDto()
            {
                Content = "Thanks",
                ProfileRating = 73.5f,
            }
        };
    }
}
