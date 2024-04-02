using FluentValidation;
using LostAndFound.PublicationService.CoreLibrary.Requests;

namespace LostAndFound.PublicationService.Core.FluentValidators
{
    public class UpdatePublicationRatingRequestDtoValidator : AbstractValidator<UpdatePublicationRatingRequestDto>
    {
        public UpdatePublicationRatingRequestDtoValidator()
        {
            RuleFor(dto => dto.NewPublicationVote)
                .NotNull()
                .IsInEnum();
        }
    }
}
