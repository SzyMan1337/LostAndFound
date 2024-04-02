using AutoMapper;
using LostAndFound.ProfileService.Core.MappingProfiles;
using Xunit;

namespace LostAndFound.ProfileService.UnitTests.Core.MappingProfiles
{
    public class ProfileCommentEntityMappingProfileTests
    {
        [Fact]
        public void ValidateProfileCommentEntityMappingProfileIsValid()
        {
            MapperConfiguration mapperConfig = new(
                cfg =>
                {
                    cfg.AddProfile(new ProfileCommentEntityMappingProfile());
                });

            IMapper mapper = new Mapper(mapperConfig);

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
