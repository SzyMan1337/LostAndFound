using FluentAssertions;
using LostAndFound.PublicationService.DataAccess;
using LostAndFound.PublicationService.DataAccess.Context.Interfaces;
using LostAndFound.PublicationService.DataAccess.DatabaseSeeder.Interfaces;
using LostAndFound.PublicationService.DataAccess.Repositories.Interfaces;
using LostAndFound.PublicationService.DataAccess.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Xunit;

namespace LostAndFound.PublicationService.UnitTests.ServiceRegistrations
{
    public class PublicationServiceDataAccessRegistrationTests
    {
        private readonly IConfiguration _configuration;
        private readonly ServiceCollection _services;
        private readonly Dictionary<string, string> _testCustomConfiguration;

        public PublicationServiceDataAccessRegistrationTests()
        {
            _testCustomConfiguration = new Dictionary<string, string>
            {
                {
                    "LostAndFoundMongoCluster:ConnectionString",
                    "mongodb://localhost:27017"
                },
                {
                    "LostAndFoundMongoCluster:DatabaseName",
                    "test-name-db"
                },
            };
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(_testCustomConfiguration)
                .Build();

            _services = new ServiceCollection();
        }

        [Theory]
        [InlineData(typeof(IPublicationsRepository))]
        [InlineData(typeof(ICategoriesRepository))]
        [InlineData(typeof(IDbSeeder))]
        public void AddApplicationDataAccessServices_Execute_ExpectedServiceIsRegistered(Type type)
        {
            _services.AddDataAccessServices(_configuration);
            var serviceProvider = _services.BuildServiceProvider();

            serviceProvider.GetService(type).Should().NotBeNull();
        }

        [Fact]
        public void AddApplicationDataAccessServices_Execute_MongoPublicationServiceDbContextIsRegistered()
        {
            _services.AddDataAccessServices(_configuration);
            var serviceProvider = _services.BuildServiceProvider();

            serviceProvider.GetService(typeof(IMongoPublicationServiceDbContext)).Should().NotBeNull();
        }

        [Fact]
        public void AddApplicationDataAccessServices_Execute_PublicationServiceDatabaseSettingsAreConfiguredCorrectly()
        {
            _services.AddDataAccessServices(_configuration);
            var serviceProvider = _services.BuildServiceProvider();

            var configuration = serviceProvider.GetService(typeof(IOptions<PublicationServiceDatabaseSettings>))
                as IOptions<PublicationServiceDatabaseSettings>;

            configuration?.Value.Should().NotBeNull();
            configuration!.Value.ConnectionString.Should().Be(_testCustomConfiguration["LostAndFoundMongoCluster:ConnectionString"]);
            configuration!.Value.DatabaseName.Should().Be(_testCustomConfiguration["LostAndFoundMongoCluster:DatabaseName"]);
        }
    }
}
