using LostAndFound.ChatService.Core.ChatServices;
using LostAndFound.ChatService.Core.ChatServices.Interfaces;
using LostAndFound.ChatService.Core.DateTimeProviders;
using LostAndFound.ChatService.Core.MessageServices;
using LostAndFound.ChatService.Core.MessageServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LostAndFound.ChatService.Core
{
    public static class CoreServicesRegistration
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IChatActionService, ChatActionService>();

            return services;
        }
    }
}
