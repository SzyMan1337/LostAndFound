using AutoMapper;
using FluentAssertions;
using LostAndFound.PublicationService.Core.MappingProfiles;
using LostAndFound.PublicationService.CoreLibrary.ResourceParameters;
using LostAndFound.PublicationService.DataAccess.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace LostAndFound.PublicationService.UnitTests.Core.MappingProfiles
{
    public class ResourceParametersMappingProfileTests
    {
        private readonly IMapper _mapper;

        public ResourceParametersMappingProfileTests()
        {
            MapperConfiguration mapperConfig = new(
                cfg =>
                {
                    cfg.AddProfile(new ResourceParametersMappingProfile());
                });

            _mapper = new Mapper(mapperConfig);
        }

        [Fact]
        public void ValidateProfileCommentEntityMappingProfileIsValid()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Theory]
        [MemberData(nameof(GetMatchingResourceMappingMemberData))]
        public void AutoMapper_ConvertFromVoteToSinglePublicationVote_IsValid(
            PublicationsResourceParameters resource, PublicationEntityPageParameters expectedResource)
        {
            var value = _mapper.Map<PublicationEntityPageParameters>(resource);

            value.Should().BeEquivalentTo(expectedResource);
        }

        public static IEnumerable<object[]> GetMatchingResourceMappingMemberData()
        {
            yield return new object[]
            {
                new PublicationsResourceParameters()
                {
                    PageNumber = 2,
                    PageSize = 10,
                    OnlyUserPublications = true,
                    SubjectCategoryId = "other",
                    PublicationState = CoreLibrary.Enums.PublicationState.Open,
                    SearchQuery = "",
                    FromDate = DateTime.MinValue,
                    ToDate = DateTime.MinValue,
                    PublicationType = CoreLibrary.Enums.PublicationType.FoundSubject,
                    OrderBy = "None",
                },
                new PublicationEntityPageParameters()
                {
                    PageNumber = 2,
                    PageSize = 10,
                    OnlyUserPublications = true,
                    SubjectCategoryId = "other",
                    PublicationState = PublicationService.DataAccess.Entities.PublicationEnums.State.Open,
                    SearchQuery = "",
                    FromDate = DateTime.MinValue,
                    ToDate = DateTime.MinValue,
                    PublicationType = PublicationService.DataAccess.Entities.PublicationEnums.Type.FoundSubject,
                    CoordinateBoundaries = null,
                },
            };

            yield return new object[]
            {
                new PublicationsResourceParameters()
                {
                    PageSize = 10,
                    SubjectCategoryId = "other",
                    PublicationState = CoreLibrary.Enums.PublicationState.Closed,
                    PublicationType = CoreLibrary.Enums.PublicationType.FoundSubject,
                    OrderBy = "None",
                },
                new PublicationEntityPageParameters()
                {
                    PageNumber = 1,
                    PageSize = 10,
                    SubjectCategoryId = "other",
                    PublicationState = PublicationService.DataAccess.Entities.PublicationEnums.State.Closed,
                    PublicationType = PublicationService.DataAccess.Entities.PublicationEnums.Type.FoundSubject,
                    CoordinateBoundaries = null,
                },
            };
        }
    }
}
