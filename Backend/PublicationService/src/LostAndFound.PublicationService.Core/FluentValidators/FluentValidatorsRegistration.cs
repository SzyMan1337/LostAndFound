using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace LostAndFound.PublicationService.Core.FluentValidators
{
    public static class FluentValidatorsRegistration
    {
        public static IServiceCollection AddFluentValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreatePublicationRequestDtoValidator>();

            return services;
        }
    }
}
