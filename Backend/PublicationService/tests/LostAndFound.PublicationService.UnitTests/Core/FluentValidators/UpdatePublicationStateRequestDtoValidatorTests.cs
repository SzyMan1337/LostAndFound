using FluentValidation.TestHelper;
using LostAndFound.PublicationService.Core.FluentValidators;
using LostAndFound.PublicationService.CoreLibrary.Enums;
using LostAndFound.PublicationService.CoreLibrary.Requests;
using Xunit;

namespace LostAndFound.PublicationService.UnitTests.Core.FluentValidators
{
    public class UpdatePublicationStateRequestDtoValidatorTests
    {
        private readonly UpdatePublicationStateRequestDtoValidator _validator;

        public UpdatePublicationStateRequestDtoValidatorTests()
        {
            _validator = new UpdatePublicationStateRequestDtoValidator();
        }


        [Theory]
        [InlineData(PublicationState.Closed)]
        [InlineData(PublicationState.Open)]
        public void Validate_WithValidDto_ReturnsSuccess(PublicationState state)
        {
            var dto = new UpdatePublicationStateRequestDto()
            {
                PublicationState = state,
            };

            var result = _validator.TestValidate(dto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(100)]
        [InlineData(-1)]
        public void Validate_WithInalidDto_ReturnsFailure(int value)
        {
            var dto = new UpdatePublicationStateRequestDto()
            {
                PublicationState = (PublicationState)value,
            };

            var result = _validator.TestValidate(dto);

            result.ShouldHaveAnyValidationError();
        }
    }
}
