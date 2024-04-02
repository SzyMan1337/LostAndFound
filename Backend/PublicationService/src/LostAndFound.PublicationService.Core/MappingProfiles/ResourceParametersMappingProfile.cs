using AutoMapper;
using LostAndFound.PublicationService.CoreLibrary.ResourceParameters;
using LostAndFound.PublicationService.DataAccess.Models;

namespace LostAndFound.PublicationService.Core.MappingProfiles
{
    public class ResourceParametersMappingProfile : Profile
    {
        public ResourceParametersMappingProfile()
        {
            CreateMap<PublicationsResourceParameters, PublicationEntityPageParameters>()
                .ForMember(ent => ent.PageNumber, opt => opt.MapFrom(p => p.PageNumber))
                .ForMember(ent => ent.PageSize, opt => opt.MapFrom(p => p.PageSize))
                .ForMember(ent => ent.OnlyUserPublications, opt => opt.MapFrom(p => p.OnlyUserPublications))
                .ForMember(ent => ent.SubjectCategoryId, opt => opt.MapFrom(p => p.SubjectCategoryId))
                .ForMember(ent => ent.SearchQuery, opt => opt.MapFrom(p => p.SearchQuery))
                .ForMember(ent => ent.FromDate, opt => opt.MapFrom(p => p.FromDate))
                .ForMember(ent => ent.ToDate, opt => opt.MapFrom(p => p.ToDate))
                .ForMember(ent => ent.PublicationState, opt => opt.MapFrom(p => p.PublicationState))
                .ForMember(ent => ent.PublicationType, opt => opt.MapFrom(p => p.PublicationType))
                .ForMember(ent => ent.CoordinateBoundaries, opt => opt.Ignore())
                .ForMember(ent => ent.SortIndicator, opt => opt.Ignore());
        }
    }
}
