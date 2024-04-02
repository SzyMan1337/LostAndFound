using FluentValidation;
using LostAndFound.AuthService.CoreLibrary.Requests;

namespace LostAndFound.AuthService.Core.FluentValidators
{
    public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestDtoValidator()
        {
            RuleFor(dto => dto.Email)
                .NotEmpty()
                .WithMessage("Adres e-email nie może być pusty.")
                .EmailAddress()
                .WithMessage("Adres e-mail jest niepoprawny.");

            RuleFor(dto => dto.Password)
                .MinimumLength(8)
                .WithMessage("Hasło musi składać się z przynajmniej 8 znaków.");
        }
    }
}
