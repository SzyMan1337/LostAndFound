using LostAndFound.PublicationService.DataAccess.Context;
using LostAndFound.PublicationService.DataAccess.Context.Interfaces;
using LostAndFound.PublicationService.DataAccess.DatabaseSeeder;
using LostAndFound.PublicationService.DataAccess.DatabaseSeeder.Interfaces;
using LostAndFound.PublicationService.DataAccess.Repositories;
using LostAndFound.PublicationService.DataAccess.Repositories.Interfaces;
using LostAndFound.PublicationService.DataAccess.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LostAndFound.PublicationService.DataAccess
{
    public static class PublicationServiceDataAccessRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PublicationServiceDatabaseSettings>(
                configuration.GetSection(PublicationServiceDatabaseSettings.SettingName));

            services.AddSingleton<IMongoPublicationServiceDbContext, MongoPublicationServiceDbContext>();
            services.AddScoped<IPublicationsRepository, PublicationsRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();

            services.AddScoped<IDbSeeder, MongoDbSeeder>();

            return services;
        }
    }
}
