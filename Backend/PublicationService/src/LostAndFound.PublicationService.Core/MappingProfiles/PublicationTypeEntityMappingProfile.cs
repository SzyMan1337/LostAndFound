using AutoMapper;
using PublicationDtoType = LostAndFound.PublicationService.CoreLibrary.Enums.PublicationType;
using PublicationEntityType = LostAndFound.PublicationService.DataAccess.Entities.PublicationEnums.Type;

namespace LostAndFound.PublicationService.Core.MappingProfiles
{
    public class PublicationTypeEntityMappingProfile : Profile
    {
        public PublicationTypeEntityMappingProfile()
        {
            CreateMap<PublicationEntityType, PublicationDtoType>()
                .ConvertUsing((value, destination) =>
                {
                    return value switch
                    {
                        PublicationEntityType.LostSubject => PublicationDtoType.LostSubject,
                        PublicationEntityType.FoundSubject => PublicationDtoType.FoundSubject,
                        _ => default,
                    };
                });

            CreateMap<PublicationDtoType, PublicationEntityType>()
                .ConvertUsing((value, destination) =>
                {
                    return value switch
                    {
                        PublicationDtoType.FoundSubject => PublicationEntityType.FoundSubject,
                        PublicationDtoType.LostSubject => PublicationEntityType.LostSubject,
                        _ => default,
                    };
                });
        }
    }
}
