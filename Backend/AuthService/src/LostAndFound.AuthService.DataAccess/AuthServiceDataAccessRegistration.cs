using LostAndFound.AuthService.DataAccess.Context;
using LostAndFound.AuthService.DataAccess.Context.Interfaces;
using LostAndFound.AuthService.DataAccess.Repositories;
using LostAndFound.AuthService.DataAccess.Repositories.Interfaces;
using LostAndFound.AuthService.DataAccess.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LostAndFound.AuthService.DataAccess
{
    public static class AuthServiceDataAccessRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthServiceDatabaseSettings>(
                configuration.GetSection(AuthServiceDatabaseSettings.SettingName));

            services.AddSingleton<IMongoAuthServiceDbContext, MongoAuthServiceDbContext>();
            services.AddScoped<IAccountsRepository, AccountsRepository>();

            return services;
        }
    }
}
