using FluentValidation;
using LostAndFound.ChatService.CoreLibrary.Requests;

namespace LostAndFound.ChatService.Core.FluentValidators
{
    public class CreateMessageRequestDtoValidator : AbstractValidator<CreateMessageRequestDto>
    {
        public CreateMessageRequestDtoValidator()
        {
            RuleFor(dto => dto.Content)
                .NotEmpty();
        }
    }
}
