using FluentValidation;
using LostAndFound.AuthService.CoreLibrary.Requests;
using LostAndFound.AuthService.DataAccess.Repositories.Interfaces;

namespace LostAndFound.AuthService.Core.FluentValidators
{
    public class RegisterUserRequestDtoValidator : AbstractValidator<RegisterUserAccountRequestDto>
    {
        public RegisterUserRequestDtoValidator(IAccountsRepository accountsRepository)
        {
            RuleFor(dto => dto.Email)
                .NotEmpty()
                .WithMessage("Adres e-email nie może być pusty.")
                .EmailAddress()
                .WithMessage("Adres e-mail jest niepoprawny.");

            RuleFor(dto => dto.Password)
                .MinimumLength(8)
                .WithMessage("Hasło musi składać się z przynajmniej 8 znaków.");

            RuleFor(dto => dto.Username)
                .NotEmpty()
                .WithMessage("Nazwa użytkownika nie może być pusta.")
                .MinimumLength(8)
                .WithMessage("Nazwa użytkownika musi składać się z przynajmniej 8 znaków.");

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    if (accountsRepository.IsEmailInUse(value))
                    {
                        context.AddFailure("Email", "Podany adres e-mail jest już zajęty");
                    }
                });

            RuleFor(x => x.Username)
                .Custom((value, context) =>
                {
                    if (accountsRepository.IsUsernameInUse(value))
                    {
                        context.AddFailure("Username", "Podana nazwa użytkownika jest już zajęta");
                    }
                });
        }
    }
}
