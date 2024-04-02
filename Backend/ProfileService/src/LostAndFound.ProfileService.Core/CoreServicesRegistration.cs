using LostAndFound.ProfileService.Core.DateTimeProviders;
using LostAndFound.ProfileService.Core.UserProfileServices;
using LostAndFound.ProfileService.Core.UserProfileServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LostAndFound.ProfileService.Core
{
    public static class CoreServicesRegistration
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IProfileCommentService, ProfileCommentService>();

            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            return services;
        }
    }
}
