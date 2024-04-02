using FluentValidation;
using LostAndFound.AuthService.CoreLibrary.Requests;

namespace LostAndFound.AuthService.Core.FluentValidators
{
    public class ChangeAccountPasswordRequestDtoValidator : AbstractValidator<ChangeAccountPasswordRequestDto>
    {
        public ChangeAccountPasswordRequestDtoValidator()
        {
            RuleFor(dto => dto.Password)
                .MinimumLength(8)
                .WithMessage("Hasło musi składać się z przynajmniej 8 znaków.");

            RuleFor(dto => dto.NewPassword)
                .MinimumLength(8)
                .WithMessage("Nowe hasło musi składać się z przynajmniej 8 znaków.");
        }
    }
}
