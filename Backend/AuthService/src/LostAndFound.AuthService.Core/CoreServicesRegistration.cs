using LostAndFound.AuthService.Core.AccountServices;
using LostAndFound.AuthService.Core.DateTimeProviders;
using LostAndFound.AuthService.Core.PasswordHashers;
using LostAndFound.AuthService.Core.TokenGenerators;
using LostAndFound.AuthService.Core.TokenValidators;
using LostAndFound.AuthService.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LostAndFound.AuthService.Core
{
    public static class CoreServicesRegistration
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IPasswordHasher<Account>, BCryptPasswordHasher<Account>>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IAccessTokenGenerator, AccessTokenGenerator>();
            services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddScoped<IRefreshTokenValidator, RefreshTokenValidator>();

            return services;
        }
    }
}
