using FluentValidation;
using LostAndFound.ProfileService.CoreLibrary.Requests;

namespace LostAndFound.ProfileService.Core.FluentValidators
{
    public class CreateProfileCommentRequestDtoValidator : AbstractValidator<CreateProfileCommentRequestDto>
    {
        public CreateProfileCommentRequestDtoValidator()
        {
            RuleFor(dto => dto.Content)
                .NotEmpty()
                .WithMessage("Treść komentarza nie może być pusta.");

            RuleFor(dto => dto.ProfileRating)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(5);
        }
    }
}
