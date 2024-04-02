using AutoMapper;
using LostAndFound.PublicationService.Core.MappingProfiles;
using Xunit;

namespace LostAndFound.PublicationService.UnitTests.Core.MappingProfiles
{
    public class PublicationEntityMappingProfileTests
    {
        [Fact]
        public void ValidateProfileCommentEntityMappingProfileIsValid()
        {
            MapperConfiguration mapperConfig = new(
                cfg =>
                {
                    cfg.AddProfile(new PublicationEntityMappingProfile());
                });

            IMapper mapper = new Mapper(mapperConfig);

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
