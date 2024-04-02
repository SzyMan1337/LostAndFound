using FluentAssertions;
using LostAndFound.ProfileService.DataAccess;
using LostAndFound.ProfileService.DataAccess.Context.Interfaces;
using LostAndFound.ProfileService.DataAccess.Repositories.Interfaces;
using LostAndFound.ProfileService.DataAccess.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Xunit;

namespace LostAndFound.ProfileService.UnitTests.ServiceRegistrations
{
    public class ProfileServiceDataAccessRegistrationTests
    {
        private readonly IConfiguration _configuration;
        private readonly ServiceCollection _services;
        private readonly Dictionary<string, string> _testCustomConfiguration;

        public ProfileServiceDataAccessRegistrationTests()
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
        [InlineData(typeof(IProfilesRepository))]
        public void AddApplicationDataAccessServices_Execute_RepositoriesAreRegistered(Type type)
        {
            _services.AddDataAccessServices(_configuration);
            var serviceProvider = _services.BuildServiceProvider();

            serviceProvider.GetService(type).Should().NotBeNull();
        }

        [Fact]
        public void AddApplicationDataAccessServices_Execute_MongoProfileServiceDbContextIsRegistered()
        {
            _services.AddDataAccessServices(_configuration);
            var serviceProvider = _services.BuildServiceProvider();

            serviceProvider.GetService(typeof(IMongoProfileServiceDbContext)).Should().NotBeNull();
        }

        [Fact]
        public void AddApplicationDataAccessServices_Execute_ProfileServiceDatabaseSettingsAreConfiguredCorrectly()
        {
            _services.AddDataAccessServices(_configuration);
            var serviceProvider = _services.BuildServiceProvider();

            var configuration = serviceProvider.GetService(typeof(IOptions<ProfileServiceDatabaseSettings>))
                as IOptions<ProfileServiceDatabaseSettings>;

            configuration?.Value.Should().NotBeNull();
            configuration!.Value.ConnectionString.Should().Be(_testCustomConfiguration["LostAndFoundMongoCluster:ConnectionString"]);
            configuration!.Value.DatabaseName.Should().Be(_testCustomConfiguration["LostAndFoundMongoCluster:DatabaseName"]);
        }
    }
}
