using AutoMapper;
using LostAndFound.PublicationService.Core;
using LostAndFound.PublicationService.Core.CategoryServices.Interfaces;
using LostAndFound.PublicationService.Core.Helpers.DateTimeProviders;
using LostAndFound.PublicationService.Core.Helpers.PropertyMapping.Interfaces;
using LostAndFound.PublicationService.Core.PublicationServices.Interfaces;
using LostAndFound.PublicationService.CoreLibrary.Settings;
using LostAndFound.PublicationService.DataAccess.Repositories.Interfaces;
using LostAndFound.PublicationService.ThirdPartyServices.AzureServices.Interfaces;
using LostAndFound.PublicationService.ThirdPartyServices.GeocodingServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using Xunit;

namespace LostAndFound.PublicationService.UnitTests.ServiceRegistrations
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
        [InlineData(typeof(IPropertyMappingService))]
        public void AddCoreServices_Execute_ResultsInExpectedServiceIsRegistered(Type type)
        {
            _services.AddCoreServices();
            var serviceProvider = _services.BuildServiceProvider();

            Assert.NotNull(serviceProvider.GetService(type));
        }

        [Fact]
        public void AddCoreServices_Execute_ResultsInPublicationActionsServiceIsRegistered()
        {
            var categoriesRepositoryMock = new Mock<ICategoriesRepository>();
            _services.AddSingleton(categoriesRepositoryMock.Object);
            var publicationsRepositoryMock = new Mock<IPublicationsRepository>();
            _services.AddSingleton(publicationsRepositoryMock.Object);
            var fileStorageServiceMock = new Mock<IFileStorageService>();
            _services.AddSingleton(fileStorageServiceMock.Object);
            var googleGeocodingServiceMock = new Mock<IGeocodingService>();
            _services.AddSingleton(googleGeocodingServiceMock.Object);
            _services.AddCoreServices();

            var serviceProvider = _services.BuildServiceProvider();

            Assert.NotNull(serviceProvider.GetService(typeof(IPublicationActionsService)));
        }

        [Fact]
        public void AddCoreServices_Execute_ResultsInPCategoryServiceIsRegistered()
        {
            var categoriesRepositoryMock = new Mock<ICategoriesRepository>();
            _services.AddSingleton(categoriesRepositoryMock.Object);
            _services.AddCoreServices();

            var serviceProvider = _services.BuildServiceProvider();

            Assert.NotNull(serviceProvider.GetService(typeof(ICategoryService)));
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
