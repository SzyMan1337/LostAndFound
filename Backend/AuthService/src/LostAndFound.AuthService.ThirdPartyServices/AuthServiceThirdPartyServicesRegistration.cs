using LostAndFound.AuthService.ThirdPartyServices.RabbitMQ.MessageProducers;
using LostAndFound.AuthService.ThirdPartyServices.RabbitMQ.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LostAndFound.AuthService.ThirdPartyServices
{
    public static class AuthServiceThirdPartyServicesRegistration
    {
        public static IServiceCollection AddThirdPartyServices(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitmqSettings = new RabbitMQSettings();
            configuration.Bind(RabbitMQSettings.SettingName, rabbitmqSettings);
            services.AddSingleton(rabbitmqSettings);

            services.AddScoped<IMessageProducer, RabbitMQMessageProducer>();

            return services;
        }
    }
}
