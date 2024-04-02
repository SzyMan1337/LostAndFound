using LostAndFound.AuthService.Core.FluentValidators;
using LostAndFound.AuthService.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using Xunit;

namespace LostAndFound.AuthService.UnitTests.ServiceRegistrations
{
    public class FluentValidatorsRegistrationTests
    {
        private readonly ServiceCollection _services;

        public FluentValidatorsRegistrationTests()
        {
            _services = new ServiceCollection();
        }

        [Theory]
        [InlineData(typeof(RegisterUserRequestDtoValidator))]
        [InlineData(typeof(LoginRequestDtoValidator))]
        [InlineData(typeof(RefreshRequestDtoValidator))]
        [InlineData(typeof(ChangeAccountPasswordRequestDtoValidator))]
        public void AddFluentValidators_Execute_ResultsInExpectedValidatorIsRegistered(Type type)
        {
            var mockedAccountsRepository = new Mock<IAccountsRepository>();
            _services.AddSingleton(mockedAccountsRepository.Object);

            _services.AddFluentValidators();
            var serviceProvider = _services.BuildServiceProvider();

            Assert.NotNull(serviceProvider.GetService(type));
        }
    }
}
