using AutoMapper;
using FluentAssertions;
using LostAndFound.PublicationService.Core.MappingProfiles;
using LostAndFound.PublicationService.CoreLibrary.Enums;
using LostAndFound.PublicationService.DataAccess.Entities;
using System.Collections.Generic;
using Xunit;

namespace LostAndFound.PublicationService.UnitTests.Core.MappingProfiles
{
    public class VoteEntityMappingProfileTests
    {
        private readonly IMapper _mapper;

        public VoteEntityMappingProfileTests()
        {
            MapperConfiguration mapperConfig = new(
                cfg =>
                {
                    cfg.AddProfile(new VoteEntityMappingProfile());
                });

            _mapper = new Mapper(mapperConfig);
        }

        [Fact]
        public void ValidateProfileCommentEntityMappingProfileIsValid()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(SinglePublicationVote.Up, 1)]
        [InlineData(SinglePublicationVote.NoVote, 0)]
        [InlineData(SinglePublicationVote.Down, -1)]
        public void AutoMapper_ConvertFromSinglePublicationVoteToInt_IsValid(
            SinglePublicationVote vote, int expectedValue)
        {
            var value = _mapper.Map<int>(vote);

            value.Should().Be(expectedValue);
        }

        [Theory]
        [MemberData(nameof(GetMatchingSingleVoteAndVoteMemberData))]
        public void AutoMapper_ConvertFromSinglePublicationVoteToVote_IsValid(
            SinglePublicationVote singleVote, Vote expectedVote)
        {
            var value = _mapper.Map<Vote>(singleVote);

            value.Should().BeEquivalentTo(expectedVote);
        }

        [Theory]
        [MemberData(nameof(GetMatchingSingleVoteAndVoteMemberData))]
        public void AutoMapper_ConvertFromVoteToSinglePublicationVote_IsValid(
            SinglePublicationVote expectedSingleVote, Vote vote)
        {
            var value = _mapper.Map<SinglePublicationVote>(vote);

            value.Should().Be(expectedSingleVote);
        }

        public static IEnumerable<object[]> GetMatchingSingleVoteAndVoteMemberData()
        {
            yield return new object[]
            {
                SinglePublicationVote.Up,
                new Vote()
                {
                    Rating = 1,
                }
            };

            yield return new object[]
            {
                SinglePublicationVote.NoVote,
                new Vote()
                {
                    Rating = 0,
                }
            };

            yield return new object[]
            {
                SinglePublicationVote.Down,
                new Vote()
                {
                    Rating = -1,
                }
            };
        }
    }
}
