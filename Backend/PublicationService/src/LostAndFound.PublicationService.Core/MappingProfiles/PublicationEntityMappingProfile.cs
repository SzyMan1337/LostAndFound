using AutoMapper;
using LostAndFound.PublicationService.CoreLibrary.Enums;
using LostAndFound.PublicationService.CoreLibrary.Requests;
using LostAndFound.PublicationService.CoreLibrary.Responses;
using LostAndFound.PublicationService.DataAccess.Entities;

namespace LostAndFound.PublicationService.Core.MappingProfiles
{
    public class PublicationEntityMappingProfile : Profile
    {
        public PublicationEntityMappingProfile()
        {
            CreateMap<CreatePublicationRequestDto, Publication>()
                .ForMember(entity => entity.Title, opt => opt.MapFrom(dto => dto.Title))
                .ForMember(entity => entity.Description, opt => opt.MapFrom(dto => dto.Description))
                .ForMember(entity => entity.IncidentAddress, opt => opt.MapFrom(dto => dto.IncidentAddress))
                .ForMember(entity => entity.IncidentDate, opt => opt.MapFrom(dto => dto.IncidentDate))
                .ForMember(entity => entity.SubjectCategoryId, opt => opt.MapFrom(dto => dto.SubjectCategoryId))
                .ForMember(entity => entity.Type, opt => opt.MapFrom(dto => dto.PublicationType))
                .ForMember(entity => entity.State, opt => opt.MapFrom(dto => PublicationState.Open))
                .ForMember(entity => entity.AggregateRating, opt => opt.MapFrom(dto => 0))
                .ForMember(entity => entity.ExposedId, opt => opt.MapFrom(dto => Guid.NewGuid()))
                .ForMember(entity => entity.Author, opt => opt.Ignore())
                .ForMember(entity => entity.SubjectCategoryName, opt => opt.Ignore())
                .ForMember(entity => entity.SubjectPhotoUrl, opt => opt.Ignore())
                .ForMember(entity => entity.Latitude, opt => opt.Ignore())
                .ForMember(entity => entity.Longitude, opt => opt.Ignore())
                .ForMember(entity => entity.LastModificationDate, opt => opt.Ignore())
                .ForMember(entity => entity.CreationTime, opt => opt.Ignore())
                .ForMember(entity => entity.Votes, opt => opt.Ignore())
                .ForMember(entity => entity.Id, opt => opt.Ignore());

            CreateMap<UpdatePublicationDetailsRequestDto, Publication>()
                .ForMember(entity => entity.Title, opt => opt.MapFrom(dto => dto.Title))
                .ForMember(entity => entity.Description, opt => opt.MapFrom(dto => dto.Description))
                .ForMember(entity => entity.IncidentAddress, opt => opt.MapFrom(dto => dto.IncidentAddress))
                .ForMember(entity => entity.IncidentDate, opt => opt.MapFrom(dto => dto.IncidentDate))
                .ForMember(entity => entity.SubjectCategoryId, opt => opt.MapFrom(dto => dto.SubjectCategoryId))
                .ForMember(entity => entity.Type, opt => opt.MapFrom(dto => dto.PublicationType))
                .ForMember(entity => entity.State, opt => opt.MapFrom(dto => dto.PublicationState))
                .ForMember(entity => entity.AggregateRating, opt => opt.Ignore())
                .ForMember(entity => entity.ExposedId, opt => opt.Ignore())
                .ForMember(entity => entity.Author, opt => opt.Ignore())
                .ForMember(entity => entity.SubjectCategoryName, opt => opt.Ignore())
                .ForMember(entity => entity.SubjectPhotoUrl, opt => opt.Ignore())
                .ForMember(entity => entity.Latitude, opt => opt.Ignore())
                .ForMember(entity => entity.Longitude, opt => opt.Ignore())
                .ForMember(entity => entity.LastModificationDate, opt => opt.Ignore())
                .ForMember(entity => entity.CreationTime, opt => opt.Ignore())
                .ForMember(entity => entity.Votes, opt => opt.Ignore())
                .ForMember(entity => entity.Id, opt => opt.Ignore());

            CreateMap<Publication, PublicationBaseDataResponseDto>()
                .ForMember(dto => dto.PublicationId, opt => opt.MapFrom(entity => entity.ExposedId))
                .ForMember(dto => dto.Title, opt => opt.MapFrom(entity => entity.Title))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(entity => entity.Description))
                .ForMember(dto => dto.SubjectCategoryId, opt => opt.MapFrom(entity => entity.SubjectCategoryId))
                .ForMember(dto => dto.SubjectPhotoUrl, opt => opt.MapFrom(entity => entity.SubjectPhotoUrl))
                .ForMember(dto => dto.IncidentAddress, opt => opt.MapFrom(entity => entity.IncidentAddress))
                .ForMember(dto => dto.IncidentDate, opt => opt.MapFrom(entity => entity.IncidentDate))
                .ForMember(dto => dto.PublicationType, opt => opt.MapFrom(entity => entity.Type))
                .ForMember(dto => dto.PublicationState, opt => opt.MapFrom(entity => entity.State))
                .ForMember(dto => dto.AggregateRating, opt => opt.MapFrom(entity => entity.AggregateRating))
                .ForMember(dto => dto.UserVote, opt => opt.Ignore());

            CreateMap<Publication, PublicationDetailsResponseDto>()
                .ForMember(dto => dto.PublicationId, opt => opt.MapFrom(entity => entity.ExposedId))
                .ForMember(dto => dto.Title, opt => opt.MapFrom(entity => entity.Title))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(entity => entity.Description))
                .ForMember(dto => dto.SubjectPhotoUrl, opt => opt.MapFrom(entity => entity.SubjectPhotoUrl))
                .ForMember(dto => dto.IncidentAddress, opt => opt.MapFrom(entity => entity.IncidentAddress))
                .ForMember(dto => dto.IncidentDate, opt => opt.MapFrom(entity => entity.IncidentDate))
                .ForMember(dto => dto.AggregateRating, opt => opt.MapFrom(entity => entity.AggregateRating))
                .ForMember(dto => dto.SubjectCategoryId, opt => opt.MapFrom(entity => entity.SubjectCategoryId))
                .ForMember(dto => dto.PublicationType, opt => opt.MapFrom(entity => entity.Type))
                .ForMember(dto => dto.PublicationState, opt => opt.MapFrom(entity => entity.State))
                .ForMember(dto => dto.LastModificationDate, opt => opt.MapFrom(entity => entity.LastModificationDate))
                .ForMember(dto => dto.CreationDate, opt => opt.MapFrom(entity => entity.CreationTime))
                .ForMember(dto => dto.Author, opt => opt.MapFrom(entity => new UserDataResponseDto()
                {
                    Id = entity.Author.Id,
                    Username = entity.Author.Username,
                }))
                .ForMember(dto => dto.UserVote, opt => opt.Ignore());
        }
    }
}
