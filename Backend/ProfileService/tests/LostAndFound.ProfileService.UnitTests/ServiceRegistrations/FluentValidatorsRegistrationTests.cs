using LostAndFound.ProfileService.Core.FluentValidators;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace LostAndFound.ProfileService.UnitTests.ServiceRegistrations
{
    public class FluentValidatorsRegistrationTests
    {
        private readonly ServiceCollection _services;

        public FluentValidatorsRegistrationTests()
        {
            _services = new ServiceCollection();
        }

        [Theory]
        [InlineData(typeof(UpdateProfileCommentRequestDtoValidator))]
        [InlineData(typeof(CreateProfileCommentRequestDtoValidator))]
        public void AddFluentValidators_Execute_ResultsInExpectedValidatorIsRegistered(Type type)
        {
            _services.AddFluentValidators();
            var serviceProvider = _services.BuildServiceProvider();

            Assert.NotNull(serviceProvider.GetService(type));
        }
    }
}
