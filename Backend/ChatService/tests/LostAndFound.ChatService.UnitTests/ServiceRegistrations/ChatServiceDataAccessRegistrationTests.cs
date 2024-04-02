using FluentAssertions;
using LostAndFound.ChatService.DataAccess;
using LostAndFound.ChatService.DataAccess.Context.Interfaces;
using LostAndFound.ChatService.DataAccess.Repositories.Interfaces;
using LostAndFound.ChatService.DataAccess.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Xunit;

namespace LostAndFound.ChatService.UnitTests.ServiceRegistrations
{
    public class ChatServiceDataAccessRegistrationTests
    {
        private readonly IConfiguration _configuration;
        private readonly ServiceCollection _services;
        private readonly Dictionary<string, string> _testCustomConfiguration;

        public ChatServiceDataAccessRegistrationTests()
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
        [InlineData(typeof(IChatsRepository))]
        public void AddApplicationDataAccessServices_Execute_RepositoriesAreRegistered(Type type)
        {
            _services.AddDataAccessServices(_configuration);
            var serviceProvider = _services.BuildServiceProvider();

            serviceProvider.GetService(type).Should().NotBeNull();
        }

        [Fact]
        public void AddApplicationDataAccessServices_Execute_MongoChatServiceDbContextIsRegistered()
        {
            _services.AddDataAccessServices(_configuration);
            var serviceProvider = _services.BuildServiceProvider();

            serviceProvider.GetService(typeof(IMongoChatServiceDbContext)).Should().NotBeNull();
        }

        [Fact]
        public void AddApplicationDataAccessServices_Execute_ChatServiceDatabaseSettingsAreConfiguredCorrectly()
        {
            _services.AddDataAccessServices(_configuration);
            var serviceProvider = _services.BuildServiceProvider();

            var configuration = serviceProvider.GetService(typeof(IOptions<ChatServiceDatabaseSettings>))
                as IOptions<ChatServiceDatabaseSettings>;

            configuration?.Value.Should().NotBeNull();
            configuration!.Value.ConnectionString.Should().Be(_testCustomConfiguration["LostAndFoundMongoCluster:ConnectionString"]);
            configuration!.Value.DatabaseName.Should().Be(_testCustomConfiguration["LostAndFoundMongoCluster:DatabaseName"]);
        }
    }
}
