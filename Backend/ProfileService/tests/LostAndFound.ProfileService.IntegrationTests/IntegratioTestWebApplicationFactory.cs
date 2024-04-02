using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using LostAndFound.ProfileService.BackgroundServices;
using LostAndFound.ProfileService.CoreLibrary.Settings;
using LostAndFound.ProfileService.DataAccess.Context;
using LostAndFound.ProfileService.DataAccess.Context.Interfaces;
using LostAndFound.ProfileService.DataAccess.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LostAndFound.ProfileService.IntegrationTests
{
    public class IntegratioTestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>, IAsyncLifetime
        where TStartup : class
    {
        private readonly TestcontainerDatabase _mongoContainer;
        private readonly RabbitMqTestcontainer _rabbitMqContainer;

        public IntegratioTestWebApplicationFactory()
        {
            _mongoContainer = new TestcontainersBuilder<MongoDbTestcontainer>()
                .WithDatabase(new MongoDbTestcontainerConfiguration
                {
                    Database = "test_profile_db",
                    Username = "mongo",
                    Password = "mongo",
                })
                .Build();

            _rabbitMqContainer = new TestcontainersBuilder<RabbitMqTestcontainer>()
                .WithMessageBroker(new RabbitMqTestcontainerConfiguration
                {
                    Username = "guest",
                    Password = "guest",
                })
                .WithExposedPort(Random.Shared.Next() % 10000)
                .Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<IMongoProfileServiceDbContext>();

                var options = Options.Create(new ProfileServiceDatabaseSettings()
                {
                    ConnectionString = _mongoContainer.ConnectionString,
                    DatabaseName = _mongoContainer.Database
                });
                services.AddSingleton<IMongoProfileServiceDbContext>(_ =>
                    new MongoProfileServiceDbContext(options));

                services.RemoveAll<RabbitMQBackgroundConsumerService>();
                services.RemoveAll<RabbitMQSettings>();

                var rabbitSetting = new RabbitMQSettings()
                {
                    HostName = _rabbitMqContainer.Hostname,
                    QueueName = $"accounts{Random.Shared.NextInt64()}",
                    Port = _rabbitMqContainer.GetMappedPublicPort(5672)
                };
                services.AddSingleton(rabbitSetting);
                services.AddHostedService<RabbitMQBackgroundConsumerService>();
            });
        }

        public async Task InitializeAsync()
        {
            await _mongoContainer.StartAsync();
            await _rabbitMqContainer.StartAsync();
        }

        public new async Task DisposeAsync()
        {
            await _mongoContainer.DisposeAsync();
            await _rabbitMqContainer.DisposeAsync();
        }
    }
}
