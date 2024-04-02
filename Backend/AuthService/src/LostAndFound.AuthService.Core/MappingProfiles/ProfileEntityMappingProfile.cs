using AutoMapper;
using LostAndFound.AuthService.CoreLibrary.Messages;
using LostAndFound.AuthService.CoreLibrary.Requests;
using LostAndFound.AuthService.CoreLibrary.Responses;
using LostAndFound.AuthService.DataAccess.Entities;

namespace LostAndFound.AuthService.Core.MappingProfiles
{
    public class ProfileEntityMappingProfile : Profile
    {
        public ProfileEntityMappingProfile()
        {
            CreateMap<RegisterUserAccountRequestDto, Account>()
                .ForMember(entity => entity.UserId, opt => opt.MapFrom(dto => Guid.NewGuid()))
                .ForMember(entity => entity.Email, opt => opt.MapFrom(dto => dto.Email))
                .ForMember(entity => entity.Username, opt => opt.MapFrom(dto => dto.Username))
                .ForMember(entity => entity.CreationTime, opt => opt.Ignore());

            CreateMap<Account, NewUserAccountMessageDto>()
                .ForMember(dto => dto.UserId, opt => opt.MapFrom(o => o.UserId))
                .ForMember(dto => dto.Email, opt => opt.MapFrom(o => o.Email))
                .ForMember(dto => dto.Username, opt => opt.MapFrom(o => o.Username));

            CreateMap<Account, RegisteredUserAccountResponseDto>()
                .ForMember(dto => dto.UserId, opt => opt.MapFrom(o => o.UserId.ToString()))
                .ForMember(dto => dto.Email, opt => opt.MapFrom(o => o.Email))
                .ForMember(dto => dto.Username, opt => opt.MapFrom(o => o.Username));
        }
    }
}
