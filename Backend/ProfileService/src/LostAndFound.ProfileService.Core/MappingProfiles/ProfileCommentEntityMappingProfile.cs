using LostAndFound.ProfileService.CoreLibrary.Requests;
using LostAndFound.ProfileService.CoreLibrary.Responses;
using LostAndFound.ProfileService.DataAccess.Entities;

namespace LostAndFound.ProfileService.Core.MappingProfiles
{
    public class ProfileCommentEntityMappingProfile : AutoMapper.Profile
    {
        public ProfileCommentEntityMappingProfile()
        {
            CreateMap<CreateProfileCommentRequestDto, Comment>()
                .ForMember(entity => entity.Content, opt => opt.MapFrom(dto => dto.Content))
                .ForMember(entity => entity.Rating, opt => opt.MapFrom(dto => dto.ProfileRating))
                .ForMember(entity => entity.AuthorId, opt => opt.Ignore())
                .ForMember(entity => entity.AuthorUsername, opt => opt.Ignore())
                .ForMember(entity => entity.CreationTime, opt => opt.Ignore())
                .ForMember(entity => entity.LastModificationDate, opt => opt.Ignore());

            CreateMap<UpdateProfileCommentRequestDto, Comment>()
                .ForMember(entity => entity.Content, opt => opt.MapFrom(dto => dto.Content))
                .ForMember(entity => entity.Rating, opt => opt.MapFrom(dto => dto.ProfileRating))
                .ForMember(entity => entity.AuthorId, opt => opt.Ignore())
                .ForMember(entity => entity.AuthorUsername, opt => opt.Ignore())
                .ForMember(entity => entity.CreationTime, opt => opt.Ignore())
                .ForMember(entity => entity.LastModificationDate, opt => opt.Ignore());

            CreateMap<Comment, CommentDataResponseDto>()
                .ForMember(dto => dto.Content, opt => opt.MapFrom(entity => entity.Content))
                .ForMember(dto => dto.ProfileRating, opt => opt.MapFrom(entity => entity.Rating))
                .ForMember(dto => dto.CreationDate, opt => opt.MapFrom(entity => entity.CreationTime))
                .ForMember(dto => dto.Author, opt => opt.MapFrom(entity =>
                    new AuthorDataResponseDto()
                    {
                        Id = entity.AuthorId,
                        Username = entity.AuthorUsername,
                    }));
        }
    }
}
