using AutoMapper;
using LostAndFound.ChatService.Core.MappingProfiles;
using Xunit;

namespace LostAndFound.ChatService.UnitTests.Core.MappingProfiles
{
    public class ChatEntityMappingProfileTests
    {
        [Fact]
        public void ValidateProfileCommentEntityMappingProfileIsValid()
        {
            MapperConfiguration mapperConfig = new(
                cfg =>
                {
                    cfg.AddProfile(new ChatEntityMappingProfile());
                });

            var mapper = new Mapper(mapperConfig);

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
