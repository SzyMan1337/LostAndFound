using FluentValidation;
using LostAndFound.AuthService.CoreLibrary.Requests;

namespace LostAndFound.AuthService.Core.FluentValidators
{
    public class RefreshRequestDtoValidator : AbstractValidator<RefreshRequestDto>
    {
        public RefreshRequestDtoValidator()
        {
            RuleFor(dto => dto.RefreshToken)
                .NotNull()
                .NotEmpty();
        }
    }
}
