using FluentValidation;
using LostAndFound.PublicationService.Core.Helpers.DateTimeProviders;
using LostAndFound.PublicationService.CoreLibrary.Requests;
using LostAndFound.PublicationService.DataAccess.Repositories.Interfaces;

namespace LostAndFound.PublicationService.Core.FluentValidators
{
    public class UpdatePublicationDetailsRequestDtoValidator : AbstractValidator<UpdatePublicationDetailsRequestDto>
    {
        public UpdatePublicationDetailsRequestDtoValidator(IDateTimeProvider dateTimeProvider,
            ICategoriesRepository categoriesRepository)
        {
            RuleFor(dto => dto.Title)
                .NotEmpty()
                .WithMessage("Tytuł ogłoszenia nie może być pusty");

            RuleFor(dto => dto.Description)
                .NotEmpty()
                .WithMessage("Opis ogłoszenia nie może być pusty");

            RuleFor(dto => dto.IncidentAddress)
                .NotEmpty()
                .WithMessage("Miejsce zdarzenia w ogłoszeniu nie może być puste");

            RuleFor(dto => dto.IncidentDate)
                .NotEmpty()
                .WithMessage("Data zdarzenia w ogłoszeniu nie może być pusta")
                .LessThan(dateTimeProvider.UtcNow)
                .WithMessage("Data zdarzenia w ogłoszeniu nie może być z przyszłości");

            RuleFor(dto => dto.SubjectCategoryId)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    if (categoriesRepository.DoesCategoryExist(value))
                    {
                        context.AddFailure("SubjectCategoryId", "Kategoria o podanym Id nie istnieje");
                    }
                });

            RuleFor(dto => dto.PublicationType)
                .NotNull()
                .WithMessage("Typ ogłoszenia nie może być pusty")
                .IsInEnum()
                .WithMessage("Typ ogłoszenia jest niewłaściwy");

            RuleFor(dto => dto.PublicationState)
                .NotNull()
                .WithMessage("Stan ogłoszenia nie może być pusty")
                .IsInEnum()
                .WithMessage("Stan ogłoszenia jest niewłaściwy");
        }
    }
}
