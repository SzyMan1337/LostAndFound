using AutoMapper;
using PublicationDtoState = LostAndFound.PublicationService.CoreLibrary.Enums.PublicationState;
using PublicationEntityState = LostAndFound.PublicationService.DataAccess.Entities.PublicationEnums.State;

namespace LostAndFound.PublicationService.Core.MappingProfiles
{
    public class PublicationStateEntityMappingProfile : Profile
    {
        public PublicationStateEntityMappingProfile()
        {
            CreateMap<PublicationEntityState, PublicationDtoState>()
                .ConvertUsing((value, destination) =>
                {
                    return value switch
                    {
                        PublicationEntityState.Closed => PublicationDtoState.Closed,
                        PublicationEntityState.Open => PublicationDtoState.Open,
                        _ => default,
                    };
                });

            CreateMap<PublicationDtoState, PublicationEntityState>()
                .ConvertUsing((value, destination) =>
                {
                    return value switch
                    {
                        PublicationDtoState.Closed => PublicationEntityState.Closed,
                        PublicationDtoState.Open => PublicationEntityState.Open,
                        _ => default,
                    };
                });
        }
    }
}
