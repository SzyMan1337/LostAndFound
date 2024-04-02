using AutoMapper;
using LostAndFound.ProfileService.Core;
using LostAndFound.ProfileService.Core.DateTimeProviders;
using LostAndFound.ProfileService.Core.UserProfileServices.Interfaces;
using LostAndFound.ProfileService.CoreLibrary.Settings;
using LostAndFound.ProfileService.DataAccess.Context.Interfaces;
using LostAndFound.ProfileService.DataAccess.Repositories.Interfaces;
using LostAndFound.ProfileService.ThirdPartyServices.AzureServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using Xunit;

namespace LostAndFound.ProfileService.UnitTests.ServiceRegistrations
{
    public class CoreServicesRegistrationTests
    {
        private readonly ServiceCollection _services;

        public CoreServicesRegistrationTests()
        {
            _services = new ServiceCollection();

            var mockedAuthenticationSettings = new Mock<AuthenticationSettings>();
            _services.AddSingleton(mockedAuthenticationSettings.Object);
        }

        [Theory]
        [InlineData(typeof(IDateTimeProvider))]
        public void AddCoreServices_Execute_ResultsInExpectedServiceIsRegistered(Type type)
        {
            _services.AddCoreServices();
            var serviceProvider = _services.BuildServiceProvider();

            Assert.NotNull(serviceProvider.GetService(type));
        }

        [Fact]
        public void AddCoreServices_Execute_ResultsInUserProfileServiceIsRegistered()
        {
            var mockedAccountssRepository = new Mock<IProfilesRepository>();
            _services.AddSingleton(mockedAccountssRepository.Object);
            var mockedMongoAuthServiceDbContext = new Mock<IMongoProfileServiceDbContext>();
            _services.AddSingleton(mockedMongoAuthServiceDbContext.Object);
            var mockedFileStorageService = new Mock<IFileStorageService>();
            _services.AddSingleton(mockedFileStorageService.Object);
            _services.AddCoreServices();

            var serviceProvider = _services.BuildServiceProvider();

            Assert.NotNull(serviceProvider.GetService(typeof(IUserProfileService)));
        }

        [Fact]
        public void AddCoreServices_Execute_ResultsInProfileCommentServiceIsRegistered()
        {
            var mockedAccountssRepository = new Mock<IProfilesRepository>();
            _services.AddSingleton(mockedAccountssRepository.Object);
            var mockedMongoAuthServiceDbContext = new Mock<IMongoProfileServiceDbContext>();
            _services.AddSingleton(mockedMongoAuthServiceDbContext.Object);
            _services.AddCoreServices();

            var serviceProvider = _services.BuildServiceProvider();

            Assert.NotNull(serviceProvider.GetService(typeof(IProfileCommentService)));
        }

        [Fact]
        public void AddApplicationBusinessLogicServices_Execute_AutoMapperServiceIsRegistered()
        {

            _services.AddCoreServices();
            var serviceProvider = _services.BuildServiceProvider();

            Assert.NotNull(serviceProvider.GetService<IMapper>());
        }
    }
}
