using FluentAssertions;
using LostAndFound.AuthService.DataAccess.Context;
using System;
using Xunit;

namespace LostAndFound.AuthService.UnitTests.DataAccess.Context
{
    public class MongoAuthServiceDbContextTests
    {

        [Fact]
        public void MongoAuthServiceDbContextConstructor_InvokedWithNullArgument_ThrowsArgumentNullException()
        {
            var act = () => new MongoAuthServiceDbContext(null!);

            act.Should().Throw<ArgumentNullException>();
        }
    }
}
