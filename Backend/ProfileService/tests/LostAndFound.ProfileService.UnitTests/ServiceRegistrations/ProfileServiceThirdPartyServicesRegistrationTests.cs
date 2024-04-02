using Azure.Storage.Blobs;
using FluentAssertions;
using LostAndFound.ProfileService.ThirdPartyServices;
using LostAndFound.ProfileService.ThirdPartyServices.AzureServices.Interfaces;
using LostAndFound.ProfileService.ThirdPartyServices.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace LostAndFound.ProfileService.UnitTests.ServiceRegistrations
{
    public class ProfileServiceThirdPartyServicesRegistrationTests
    {
        private readonly IConfiguration _configuration;
        private readonly ServiceCollection _services;
        private readonly Dictionary<string, string> _testCustomConfiguration;

        public ProfileServiceThirdPartyServicesRegistrationTests()
        {
            _testCustomConfiguration = new Dictionary<string, string>
            {
                {
                    "LostAndFoundBlobStorageSettings:ConnectionString",
                    "DefaultEndpointsProtocol=https;AccountName=lostandfoundstorageTest;AccountKey=i27BG+cowBxceqL7KVTktwCv656A0n6aIA6BjQeN03BEkvE5WrT5yL6T/Q5xkJzxGvzDNdYeIrdq+AStvgk5Nw==;EndpointSuffix=core.windows.net"
                },
                {
                    "LostAndFoundBlobStorageSettings:ProfilePicturesContainerName",
                    "profile-container"
                }
            };
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(_testCustomConfiguration)
                .Build();

            _services = new ServiceCollection();
        }

        [Theory]
        [InlineData(typeof(IFileStorageService))]
        [InlineData(typeof(BlobServiceClient))]
        public void AddThirdPartyServices_Execute_ResultsInExpectedServiceIsRegistered(Type type)
        {
            _services.AddThirdPartyServices(_configuration);
            var serviceProvider = _services.BuildServiceProvider();

            serviceProvider.GetService(type).Should().NotBeNull();
        }

        [Fact]
        public void AddThirdPartyServices_Execute_BlobStorageSettingsAreConfiguredCorrectly()
        {
            _services.AddThirdPartyServices(_configuration);
            var serviceProvider = _services.BuildServiceProvider();

            var configuration = serviceProvider.GetService(typeof(BlobStorageSettings)) as BlobStorageSettings;

            configuration?.Should().NotBeNull();
            configuration!.ConnectionString.Should().Be(_testCustomConfiguration["LostAndFoundBlobStorageSettings:ConnectionString"]);
            configuration!.ProfilePicturesContainerName.Should().Be(_testCustomConfiguration["LostAndFoundBlobStorageSettings:ProfilePicturesContainerName"]);
        }
    }
}
