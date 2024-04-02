using FluentAssertions;
using LostAndFound.ChatService.DataAccess.Context;
using System;
using Xunit;

namespace LostAndFound.ChatService.UnitTests.DataAccess.Context
{
    public class MongoChatServiceDbContextTests
    {
        [Fact]
        public void MongoChatServiceDbContextConstructor_InvokedWithNullArgument_ThrowsArgumentNullException()
        {
            var act = () => new MongoChatServiceDbContext(null!);

            act.Should().Throw<ArgumentNullException>();
        }
    }
}
