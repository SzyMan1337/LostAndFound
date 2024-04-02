using FluentValidation.TestHelper;
using LostAndFound.ProfileService.Core.FluentValidators;
using LostAndFound.ProfileService.CoreLibrary.Requests;
using Xunit;

namespace LostAndFound.ProfileService.UnitTests.Core.FluentValidators
{
    public class UpdateProfileCommentRequestDtoValidatorTests
    {
        private readonly UpdateProfileCommentRequestDtoValidator _validator;

        public UpdateProfileCommentRequestDtoValidatorTests()
        {
            _validator = new UpdateProfileCommentRequestDtoValidator();
        }

        [Theory]
        [MemberData(nameof(GetValidLoginRequestDtos))]
        public void Validate_WithValidDto_ReturnsSuccess(UpdateProfileCommentRequestDto requestDto)
        {
            var result = _validator.TestValidate(requestDto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [MemberData(nameof(GetInvalidLoginRequestDtos))]
        public void Validate_WithInalidDto_ReturnsFailure(UpdateProfileCommentRequestDto requestDto)
        {
            var result = _validator.TestValidate(requestDto);

            result.ShouldHaveAnyValidationError();
        }

        public static TheoryData<UpdateProfileCommentRequestDto> GetValidLoginRequestDtos() => new()
        {
            new UpdateProfileCommentRequestDto()
            {
                Content = "Nice job",
                ProfileRating = 0f,
            },
            new UpdateProfileCommentRequestDto()
            {
                Content = "Nice job",
                ProfileRating = 5f,
            },
            new UpdateProfileCommentRequestDto()
            {
                Content = "Thanks",
                ProfileRating = 3.5f,
            }
        }; 

        public static TheoryData<UpdateProfileCommentRequestDto> GetInvalidLoginRequestDtos() => new()
        {
            new UpdateProfileCommentRequestDto()
            {
                Content = "",
                ProfileRating = 2f,
            },
            new UpdateProfileCommentRequestDto()
            {
                Content = "Nice job",
                ProfileRating = -5f,
            },
            new UpdateProfileCommentRequestDto()
            {
                Content = "Thanks",
                ProfileRating = 73.5f,
            }
        };
    }
}
