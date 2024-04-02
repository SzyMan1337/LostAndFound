using FluentValidation;
using LostAndFound.PublicationService.CoreLibrary.Requests;

namespace LostAndFound.PublicationService.Core.FluentValidators
{
    public class UpdatePublicationStateRequestDtoValidator : AbstractValidator<UpdatePublicationStateRequestDto>
    {
        public UpdatePublicationStateRequestDtoValidator()
        {
            RuleFor(dto => dto.PublicationState)
                .NotNull()
                .IsInEnum();
        }
    }
}
