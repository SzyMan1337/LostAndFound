using LostAndFound.ChatService.DataAccess.Context;
using LostAndFound.ChatService.DataAccess.Context.Interfaces;
using LostAndFound.ChatService.DataAccess.Repositories;
using LostAndFound.ChatService.DataAccess.Repositories.Interfaces;
using LostAndFound.ChatService.DataAccess.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LostAndFound.ChatService.DataAccess
{
    public static class ChatServiceDataAccessRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ChatServiceDatabaseSettings>(
                configuration.GetSection(ChatServiceDatabaseSettings.SettingName));

            services.AddSingleton<IMongoChatServiceDbContext, MongoChatServiceDbContext>();
            services.AddScoped<IChatsRepository, ChatsRepository>();

            return services;
        }
    }
}
