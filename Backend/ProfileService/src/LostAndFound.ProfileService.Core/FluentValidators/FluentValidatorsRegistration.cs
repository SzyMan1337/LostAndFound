using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace LostAndFound.ProfileService.Core.FluentValidators
{
    public static class FluentValidatorsRegistration
    {
        public static IServiceCollection AddFluentValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateProfileCommentRequestDtoValidator>();

            return services;
        }
    }
}
