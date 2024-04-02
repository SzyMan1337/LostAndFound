using AutoMapper;
using FluentAssertions;
using LostAndFound.PublicationService.Core.MappingProfiles;
using LostAndFound.PublicationService.CoreLibrary.Responses;
using LostAndFound.PublicationService.DataAccess.Entities;
using System.Collections.Generic;
using Xunit;

namespace LostAndFound.PublicationService.UnitTests.Core.MappingProfiles
{
    public class CategoryEntityMappingProfileTests
    {
        private readonly IMapper _mapper;

        public CategoryEntityMappingProfileTests()
        {
            MapperConfiguration mapperConfig = new(
                cfg =>
                {
                    cfg.AddProfile(new CategoryEntityMappingProfile());
                });

            _mapper = new Mapper(mapperConfig);
        }


        [Fact]
        public void ValidateProfileCommentEntityMappingProfileIsValid()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Theory]
        [MemberData(nameof(GetMatchingCategoryAndDtoMemberData))]
        public void AutoMapper_ConvertFromVoteToSinglePublicationVote_IsValid(
            Category category, CategoryResponseDto expectedDto)
        {
            var value = _mapper.Map<CategoryResponseDto>(category);

            value.Should().BeEquivalentTo(expectedDto);
        }

        public static IEnumerable<object[]> GetMatchingCategoryAndDtoMemberData()
        {
            var displaName = "testName";
            var id = "testId";

            yield return new object[]
            {
                new Category()
                {
                    ExposedId = id,
                    DisplayName = displaName
                },
                new CategoryResponseDto()
                {
                    Id = id,
                    DisplayName = displaName,
                },
            };
        }
    }
}
