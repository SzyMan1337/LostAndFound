using FluentAssertions;
using LostAndFound.ChatService.Core.DateTimeProviders;
using System;
using Xunit;

namespace LostAndFound.ChatService.UnitTests.Core.DateTimeProviders
{
    public class DateTimeProviderTests
    {
        private readonly DateTimeProvider _dateTimeProvider;

        public DateTimeProviderTests()
        {
            _dateTimeProvider = new DateTimeProvider();
        }

        [Fact]
        public void UtcNow_ReturnsDateTimeWithUtcTimeZone()
        {
            var utcDateTimeNow = _dateTimeProvider.UtcNow;

            var diffrence = utcDateTimeNow.ToUniversalTime() - utcDateTimeNow;
            diffrence.Should().Be(TimeSpan.Zero);
        }
    }
}
