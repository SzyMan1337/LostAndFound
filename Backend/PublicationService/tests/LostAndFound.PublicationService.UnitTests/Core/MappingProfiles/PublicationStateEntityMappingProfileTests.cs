using AutoMapper;
using FluentAssertions;
using LostAndFound.PublicationService.Core.MappingProfiles;
using Xunit;
using PublicationDtoState = LostAndFound.PublicationService.CoreLibrary.Enums.PublicationState;
using PublicationEntityState = LostAndFound.PublicationService.DataAccess.Entities.PublicationEnums.State;

namespace LostAndFound.PublicationService.UnitTests.Core.MappingProfiles
{
    public class PublicationStateEntityMappingProfileTests
    {
        private readonly IMapper _mapper;

        public PublicationStateEntityMappingProfileTests()
        {
            MapperConfiguration mapperConfig = new(
                cfg =>
                {
                    cfg.AddProfile(new PublicationStateEntityMappingProfile());
                });

            _mapper = new Mapper(mapperConfig);
        }

        [Fact]
        public void ValidateProfileCommentEntityMappingProfileIsValid()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(PublicationEntityState.Closed, PublicationDtoState.Closed)]
        [InlineData(PublicationEntityState.Open, PublicationDtoState.Open)]
        public void AutoMapper_ConvertFromPublicationEntityState_IsValid(
            PublicationEntityState entityState, PublicationDtoState expectedPublicationState)
        {
            var value = _mapper.Map<PublicationDtoState>(entityState);

            value.Should().Be(expectedPublicationState);
        }

        [Theory]
        [InlineData(PublicationDtoState.Closed, PublicationEntityState.Closed)]
        [InlineData(PublicationDtoState.Open, PublicationEntityState.Open)]
        public void AutoMapper_ConvertFromPublicationDtoType_IsValid(
            PublicationDtoState dtoState, PublicationEntityState expectedEntityPublicationState)
        {
            var value = _mapper.Map<PublicationEntityState>(dtoState);

            value.Should().Be(expectedEntityPublicationState);
        }
    }
}
