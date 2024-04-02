using FluentAssertions;
using LostAndFound.AuthService.ThirdPartyServices;
using LostAndFound.AuthService.ThirdPartyServices.RabbitMQ.MessageProducers;
using LostAndFound.AuthService.ThirdPartyServices.RabbitMQ.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace LostAndFound.PublicationService.UnitTests.ServiceRegistrations
{
    public class AuthServiceThirdPartyServicesRegistrationTests
    {
        private readonly IConfiguration _configuration;
        private readonly ServiceCollection _services;
        private readonly Dictionary<string, string> _testCustomConfiguration;

        public AuthServiceThirdPartyServicesRegistrationTests()
        {
            _testCustomConfiguration = new Dictionary<string, string>
            {
                {
                    "RabbitMQ:HostName",
                    "lostandfound.rabbitmq"
                },
                {
                    "RabbitMQ:QueueName",
                    "accounts"
                },
            };
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(_testCustomConfiguration)
                .Build();

            _services = new ServiceCollection();
        }

        [Theory]
        [InlineData(typeof(IMessageProducer))]
        public void AddThirdPartyServices_Execute_ResultsInExpectedServiceIsRegistered(Type type)
        {
            _services.AddThirdPartyServices(_configuration);
            var serviceProvider = _services.BuildServiceProvider();

            serviceProvider.GetService(type).Should().NotBeNull();
        }

        [Fact]
        public void AddThirdPartyServices_Execute_RabbitMQSettingsAreConfiguredCorrectly()
        {
            _services.AddThirdPartyServices(_configuration);
            var serviceProvider = _services.BuildServiceProvider();

            var configuration = serviceProvider.GetService(typeof(RabbitMQSettings)) as RabbitMQSettings;

            configuration?.Should().NotBeNull();
            configuration!.HostName.Should().Be(_testCustomConfiguration["RabbitMQ:HostName"]);
            configuration!.QueueName.Should().Be(_testCustomConfiguration["RabbitMQ:QueueName"]);
        }
    }
}
