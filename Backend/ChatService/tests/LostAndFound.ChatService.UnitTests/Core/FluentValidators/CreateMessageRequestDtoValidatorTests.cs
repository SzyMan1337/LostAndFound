using FluentValidation.TestHelper;
using LostAndFound.ChatService.Core.FluentValidators;
using LostAndFound.ChatService.CoreLibrary.Requests;
using Xunit;

namespace LostAndFound.ChatService.UnitTests.Core.FluentValidators
{
    public class CreateMessageRequestDtoValidatorTests
    {
        private readonly CreateMessageRequestDtoValidator _validator;

        public CreateMessageRequestDtoValidatorTests()
        {
            _validator = new CreateMessageRequestDtoValidator();
        }


        [Theory]
        [MemberData(nameof(GetValidCreateMessageRequestDtos))]
        public void Validate_WithValidDto_ReturnsSuccess(CreateMessageRequestDto requestDto)
        {
            var result = _validator.TestValidate(requestDto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [MemberData(nameof(GetInvalidCreateMessageRequestDtos))]
        public void Validate_WithInalidDto_ReturnsFailure(CreateMessageRequestDto requestDto)
        {
            var result = _validator.TestValidate(requestDto);

            result.ShouldHaveAnyValidationError();
        }

        public static TheoryData<CreateMessageRequestDto> GetValidCreateMessageRequestDtos() => new()
        {
            new CreateMessageRequestDto()
            {
                Content = "valid content",
            },
            new CreateMessageRequestDto()
            {
                Content = "notrersat4w3452fdsf24",
            }
        };

        public static TheoryData<CreateMessageRequestDto> GetInvalidCreateMessageRequestDtos() => new()
        {
            new CreateMessageRequestDto()
            {
                Content = "",
            },
        };
    }
}
