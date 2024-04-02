using AutoMapper;
using LostAndFound.PublicationService.CoreLibrary.Enums;
using LostAndFound.PublicationService.DataAccess.Entities;

namespace LostAndFound.PublicationService.Core.MappingProfiles
{
    public class VoteEntityMappingProfile : Profile
    {
        public VoteEntityMappingProfile()
        {
            CreateMap<SinglePublicationVote, Vote>()
                .ForMember(entity => entity.Rating, opt => opt.MapFrom(dto => dto))
                .ForMember(entity => entity.CreationDate, opt => opt.Ignore())
                .ForMember(entity => entity.VoterId, opt => opt.Ignore());

            CreateMap<Vote, SinglePublicationVote>()
                .ConvertUsing((value, destination) =>
                {
                    return value.Rating switch
                    {
                        0 => SinglePublicationVote.NoVote,
                        1 => SinglePublicationVote.Up,
                        -1 => SinglePublicationVote.Down,
                        _ => default,
                    };
                });

            CreateMap<SinglePublicationVote, int>()
                .ConvertUsing((value, destination) =>
                {
                    return value switch
                    {
                        SinglePublicationVote.NoVote => 0,
                        SinglePublicationVote.Up => 1,
                        SinglePublicationVote.Down => -1,
                        _ => default,
                    };
                });
        }
    }
}
