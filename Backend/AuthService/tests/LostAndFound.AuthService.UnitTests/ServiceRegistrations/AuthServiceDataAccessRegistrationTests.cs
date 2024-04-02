using FluentAssertions;
using LostAndFound.AuthService.DataAccess;
using LostAndFound.AuthService.DataAccess.Context.Interfaces;
using LostAndFound.AuthService.DataAccess.Repositories.Interfaces;
using LostAndFound.AuthService.DataAccess.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Xunit;

namespace LostAndFound.AuthService.UnitTests.ServiceRegistrations
{
    public class AuthServiceDataAccessRegistrationTests
    {
        private readonly IConfiguration _configuration;
        private readonly ServiceCollection _services;
        private readonly Dictionary<string, string> _testCustomConfiguration;

        public AuthServiceDataAccessRegistrationTests()
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
        [InlineData(typeof(IAccountsRepository))]
        public void AddApplicationDataAccessServices_Execute_RepositoriesAreRegistered(Type type)
        {
            _services.AddDataAccessServices(_configuration);
            var serviceProvider = _services.BuildServiceProvider();

            serviceProvider.GetService(type).Should().NotBeNull();
        }

        [Fact]
        public void AddApplicationDataAccessServices_Execute_MongoAuthServiceDbContextIsRegistered()
        {
            _services.AddDataAccessServices(_configuration);
            var serviceProvider = _services.BuildServiceProvider();

            serviceProvider.GetService(typeof(IMongoAuthServiceDbContext)).Should().NotBeNull();
        }

        [Fact]
        public void AddApplicationDataAccessServices_Execute_AuthServiceDatabaseSettingsAreConfiguredCorrectly()
        {
            _services.AddDataAccessServices(_configuration);
            var serviceProvider = _services.BuildServiceProvider();

            var configuration = serviceProvider.GetService(typeof(IOptions<AuthServiceDatabaseSettings>))
                as IOptions<AuthServiceDatabaseSettings>;

            configuration?.Value.Should().NotBeNull();
            configuration!.Value.ConnectionString.Should().Be(_testCustomConfiguration["LostAndFoundMongoCluster:ConnectionString"]);
            configuration!.Value.DatabaseName.Should().Be(_testCustomConfiguration["LostAndFoundMongoCluster:DatabaseName"]);
        }
    }
}
