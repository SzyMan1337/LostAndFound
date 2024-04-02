using AutoMapper;
using LostAndFound.PublicationService.CoreLibrary.Responses;
using LostAndFound.PublicationService.DataAccess.Entities;

namespace LostAndFound.PublicationService.Core.MappingProfiles
{
    public class CategoryEntityMappingProfile : Profile
    {
        public CategoryEntityMappingProfile()
        {
            CreateMap<Category, CategoryResponseDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(entity => entity.ExposedId))
                .ForMember(dto => dto.DisplayName, opt => opt.MapFrom(entity => entity.DisplayName));
        }
    }
}
