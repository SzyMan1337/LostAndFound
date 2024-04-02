using AutoMapper;
using FluentAssertions;
using LostAndFound.PublicationService.Core.MappingProfiles;
using Xunit;
using PublicationDtoType = LostAndFound.PublicationService.CoreLibrary.Enums.PublicationType;
using PublicationEntityType = LostAndFound.PublicationService.DataAccess.Entities.PublicationEnums.Type;


namespace LostAndFound.PublicationService.UnitTests.Core.MappingProfiles
{
    public class PublicationTypeEntityMappingProfileTests
    {
        private readonly IMapper _mapper;

        public PublicationTypeEntityMappingProfileTests()
        {
            MapperConfiguration mapperConfig = new(
                cfg =>
                {
                    cfg.AddProfile(new PublicationTypeEntityMappingProfile());
                });

            _mapper = new Mapper(mapperConfig);
        }

        [Fact]
        public void ValidateProfileCommentEntityMappingProfileIsValid()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(PublicationEntityType.LostSubject, PublicationDtoType.LostSubject)]
        [InlineData(PublicationEntityType.FoundSubject, PublicationDtoType.FoundSubject)]
        public void AutoMapper_ConvertFromPublicationEntityType_IsValid(
            PublicationEntityType entityType, PublicationDtoType expectedPublicationType)
        {
            var value = _mapper.Map<PublicationDtoType>(entityType);

            value.Should().Be(expectedPublicationType);
        }

        [Theory]
        [InlineData(PublicationDtoType.LostSubject, PublicationEntityType.LostSubject)]
        [InlineData(PublicationDtoType.FoundSubject, PublicationEntityType.FoundSubject)]
        public void AutoMapper_ConvertFromPublicationDtoType_IsValid(
            PublicationDtoType dtoType, PublicationEntityType expectedEntityPublicationType)
        {
            var value = _mapper.Map<PublicationEntityType>(dtoType);

            value.Should().Be(expectedEntityPublicationType);
        }
    }
}
