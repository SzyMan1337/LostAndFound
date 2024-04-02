using FluentAssertions;
using LostAndFound.PublicationService.DataAccess.Attributes;
using System;
using System.Linq;
using Xunit;

namespace LostAndFound.PublicationService.UnitTests.DataAccess.Attributes
{
    public class BsonCollectionAttributeTests
    {
        [Fact]
        public void BsonCollectionAttributeConstructor_InvokedWithNullCollectionName_ThrowsArgumentNullException()
        {
            var act = () => new BsonCollectionAttribute(null!);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CollectionName_Get_ReturnsExpectedString()
        {
            var expectedCollectionName = "testName";
            var attr = new BsonCollectionAttribute(expectedCollectionName);

            attr.CollectionName.Should().Be(expectedCollectionName);
        }

        [Fact]
        public void BsonCollectionAttribute_AppliedToClass_Succeed()
        {
            var classAttr = typeof(TestBsonCollectionAttributeClass)
                .GetCustomAttributes(typeof(BsonCollectionAttribute), true)
                .FirstOrDefault() as BsonCollectionAttribute;


            classAttr.Should().NotBeNull();
            classAttr!.CollectionName.Should().Be("testName");
        }


        [BsonCollection("testName")]
        private class TestBsonCollectionAttributeClass { }
    }
}
