using Azure.Storage.Blobs;
using LostAndFound.ProfileService.ThirdPartyServices.AzureServices;
using LostAndFound.ProfileService.ThirdPartyServices.AzureServices.Interfaces;
using LostAndFound.ProfileService.ThirdPartyServices.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LostAndFound.ProfileService.ThirdPartyServices
{
    public static class ProfileServiceThirdPartyServicesRegistration
    {
        public static IServiceCollection AddThirdPartyServices(this IServiceCollection services, IConfiguration configuration)
        {
            var blobStorageSettings = new BlobStorageSettings();
            configuration.Bind(BlobStorageSettings.SettingName, blobStorageSettings);
            services.AddSingleton(blobStorageSettings);

            services.AddSingleton(x =>
                new BlobServiceClient(blobStorageSettings.ConnectionString));

            services.AddScoped<IFileStorageService, BlobStorageService>();

            return services;
        }
    }
}
