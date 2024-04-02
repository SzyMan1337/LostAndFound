using AutoMapper;
using LostAndFound.ProfileService.Core.MappingProfiles;
using Xunit;

namespace LostAndFound.ProfileService.UnitTests.Core.MappingProfiles
{
    public class ProfileEntityMappingProfileTests
    {
        [Fact]
        public void ValidateProfileEntityProfileMappingProfileIsValid()
        {
            MapperConfiguration mapperConfig = new(
                cfg =>
                {
                    cfg.AddProfile(new ProfileEntityMappingProfile());
                });

            IMapper mapper = new Mapper(mapperConfig);

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
