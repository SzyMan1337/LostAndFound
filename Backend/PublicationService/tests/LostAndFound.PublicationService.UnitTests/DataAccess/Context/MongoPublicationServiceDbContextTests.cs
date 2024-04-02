using FluentAssertions;
using LostAndFound.PublicationService.DataAccess.Context;
using System;
using Xunit;

namespace LostAndFound.PublicationService.UnitTests.DataAccess.Context
{
    public class MongoPublicationServiceDbContextTests
    {
        [Fact]
        public void MongoPublicationServiceDbContextConstructor_InvokedWithNullArgument_ThrowsArgumentNullException()
        {
            var act = () => new MongoPublicationServiceDbContext(null!);

            act.Should().Throw<ArgumentNullException>();
        }
    }
}
