using LostAndFound.ProfileService.DataAccess.Context;
using LostAndFound.ProfileService.DataAccess.Context.Interfaces;
using LostAndFound.ProfileService.DataAccess.Repositories;
using LostAndFound.ProfileService.DataAccess.Repositories.Interfaces;
using LostAndFound.ProfileService.DataAccess.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LostAndFound.ProfileService.DataAccess
{
    public static class ProfileServiceDataAccessRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ProfileServiceDatabaseSettings>(
                configuration.GetSection(ProfileServiceDatabaseSettings.SettingName));

            services.AddSingleton<IMongoProfileServiceDbContext, MongoProfileServiceDbContext>();
            services.AddScoped<IProfilesRepository, ProfilesRepository>();

            return services;
        }
    }
}
