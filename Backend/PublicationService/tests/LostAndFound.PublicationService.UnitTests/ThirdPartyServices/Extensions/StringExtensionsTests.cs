using FluentAssertions;
using LostAndFound.PublicationService.ThirdPartyServices.Extensions;
using Xunit;

namespace LostAndFound.PublicationService.UnitTests.ThirdPartyServices.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("test12test13", 6, "test12")]
        [InlineData("toShortTextToTruncate", 60, "toShortTextToTruncate")]
        [InlineData("Only1extra1", 10, "Only1extra")]
        [InlineData("test", 1, "t")]

        public void TruncateToShortString_WithValidParameters_ReturnsExpectedString(string stringToTest, int maxLength, string expectedString)
        {
            var result = stringToTest.TruncateToShortString(maxLength);

            result.Should().Be(expectedString);
        }

        [Fact]
        public void TruncateToShortString_WithNull_ReturnsNull()
        {
            string stringToTest = null!;

            var result = stringToTest.TruncateToShortString(10);

            result.Should().BeNull();
        }

        [Fact]
        public void TruncateToShortString_WithEmptyString_ReturnsEmptyString()
        {
            string stringToTest = string.Empty;

            var result = stringToTest.TruncateToShortString(10);

            result.Should().BeEmpty();
        }
    }
}
