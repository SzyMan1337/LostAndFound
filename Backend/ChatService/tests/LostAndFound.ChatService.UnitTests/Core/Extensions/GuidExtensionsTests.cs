using FluentAssertions;
using LostAndFound.ChatService.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LostAndFound.ChatService.UnitTests.Core.Extensions
{
    public class GuidExtensionsTests
    {
        [Fact]
        public void MungeTwoGuids_DoesNotDependOnTheArgumentsOrder()
        {
            var g1 = Guid.NewGuid();
            var g2 = Guid.NewGuid();

            var result = g1.MungeTwoGuids(g2);

            result.Should().Be(g2.MungeTwoGuids(g1));
        }

        [Fact]
        public void MungeTwoGuids_WithTwoEqualGuids_ReturnsEmptyGuid()
        {
            var g1 = Guid.NewGuid();

            var result = g1.MungeTwoGuids(g1);

            result.Should().BeEmpty();
        }
    }
}
