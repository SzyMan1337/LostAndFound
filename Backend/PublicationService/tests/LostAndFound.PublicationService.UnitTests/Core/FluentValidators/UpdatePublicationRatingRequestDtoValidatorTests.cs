using FluentValidation.TestHelper;
using LostAndFound.PublicationService.Core.FluentValidators;
using LostAndFound.PublicationService.CoreLibrary.Enums;
using LostAndFound.PublicationService.CoreLibrary.Requests;
using Xunit;

namespace LostAndFound.PublicationService.UnitTests.Core.FluentValidators
{
    public class UpdatePublicationRatingRequestDtoValidatorTests
    {
        private readonly UpdatePublicationRatingRequestDtoValidator _validator;

        public UpdatePublicationRatingRequestDtoValidatorTests()
        {
            _validator = new UpdatePublicationRatingRequestDtoValidator();
        }


        [Theory]
        [InlineData(SinglePublicationVote.NoVote)]
        [InlineData(SinglePublicationVote.Down)]
        [InlineData(SinglePublicationVote.Up)]
        public void Validate_WithValidDto_ReturnsSuccess(SinglePublicationVote vote)
        {
            var dto = new UpdatePublicationRatingRequestDto()
            {
                NewPublicationVote = vote,
            };

            var result = _validator.TestValidate(dto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(100)]
        [InlineData(-11)]
        public void Validate_WithInalidDto_ReturnsFailure(int value)
        {
            var dto = new UpdatePublicationRatingRequestDto()
            {
                NewPublicationVote = (SinglePublicationVote)value,
            };

            var result = _validator.TestValidate(dto);

            result.ShouldHaveAnyValidationError();
        }
    }
}
