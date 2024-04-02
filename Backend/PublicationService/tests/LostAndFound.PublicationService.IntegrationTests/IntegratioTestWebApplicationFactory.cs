using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using LostAndFound.PublicationService.DataAccess.Context;
using LostAndFound.PublicationService.DataAccess.Context.Interfaces;
using LostAndFound.PublicationService.DataAccess.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Xunit;

namespace LostAndFound.PublicationService.IntegrationTests
{
    public class IntegratioTestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>, IAsyncLifetime
        where TStartup : class
    {
        private readonly TestcontainerDatabase _container;

        public IntegratioTestWebApplicationFactory()
        {
            _container = new TestcontainersBuilder<MongoDbTestcontainer>()
                .WithDatabase(new MongoDbTestcontainerConfiguration
                {
                    Database = "test_publication_db",
                    Username = "mongo",
                    Password = "mongo",
                })
                .Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<IMongoPublicationServiceDbContext>();

                var options = Options.Create(new PublicationServiceDatabaseSettings()
                {
                    ConnectionString = _container.ConnectionString,
                    DatabaseName = _container.Database
                });
                services.AddSingleton<IMongoPublicationServiceDbContext>(_ =>
                    new MongoPublicationServiceDbContext(options));
            });
        }

        public async Task InitializeAsync() => await _container.StartAsync();

        public new async Task DisposeAsync() => await _container.DisposeAsync();
    }
}
