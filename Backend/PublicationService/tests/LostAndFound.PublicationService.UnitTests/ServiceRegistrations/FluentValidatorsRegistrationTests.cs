using FluentAssertions;
using LostAndFound.PublicationService.Core.FluentValidators;
using LostAndFound.PublicationService.Core.Helpers.DateTimeProviders;
using LostAndFound.PublicationService.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using Xunit;

namespace LostAndFound.PublicationService.UnitTests.ServiceRegistrations
{
    public class FluentValidatorsRegistrationTests
    {
        private readonly ServiceCollection _services;

        public FluentValidatorsRegistrationTests()
        {
            _services = new ServiceCollection();

            var mockedCategoriesRepository = new Mock<ICategoriesRepository>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            _services.AddSingleton(mockedDateTimeProvider.Object);
            _services.AddSingleton(mockedCategoriesRepository.Object);
        }

        [Theory]
        [InlineData(typeof(CreatePublicationRequestDtoValidator))]
        [InlineData(typeof(UpdatePublicationDetailsRequestDtoValidator))]
        [InlineData(typeof(UpdatePublicationRatingRequestDtoValidator))]
        [InlineData(typeof(UpdatePublicationStateRequestDtoValidator))]
        public void AddFluentValidators_Execute_ResultsInExpectedValidatorIsRegistered(Type type)
        {

            _services.AddFluentValidators();
            var serviceProvider = _services.BuildServiceProvider();

            serviceProvider.GetService(type).Should().NotBeNull();
        }
    }
}
