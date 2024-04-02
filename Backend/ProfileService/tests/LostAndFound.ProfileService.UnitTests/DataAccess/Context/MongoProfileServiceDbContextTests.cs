using FluentAssertions;
using LostAndFound.ProfileService.DataAccess.Context;
using System;
using Xunit;

namespace LostAndFound.ProfileService.UnitTests.DataAccess.Context
{
    public class MongoProfileServiceDbContextTests
    {
        [Fact]
        public void MongoProfileServiceDbContextConstructor_InvokedWithNullArgument_ThrowsArgumentNullException()
        {
            var act = () => new MongoProfileServiceDbContext(null!);

            act.Should().Throw<ArgumentNullException>();
        }
    }
}
