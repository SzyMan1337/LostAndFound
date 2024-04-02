using AutoMapper;
using LostAndFound.ChatService.CoreLibrary.Requests;
using LostAndFound.ChatService.CoreLibrary.Responses;
using LostAndFound.ChatService.DataAccess.Entities;

namespace LostAndFound.ChatService.Core.MappingProfiles
{
    public class ChatEntityMappingProfile : Profile
    {
        public ChatEntityMappingProfile()
        {
            CreateMap<Chat, ChatBaseDataResponseDto>()
                .ForMember(dto => dto.ChatId, opt => opt.MapFrom(entity => entity.ExposedId))
                .ForMember(dto => dto.LastMessage, opt => opt.MapFrom(entity => entity.Messages.Last()))
                .ForMember(dto => dto.ContainsUnreadMessage, opt => opt.Ignore())
                .ForMember(dto => dto.ChatMember, opt => opt.Ignore());

            CreateMap<Message, MessageResponseDto>()
               .ForMember(dto => dto.AuthorId, opt => opt.MapFrom(entity => entity.AuthorId))
               .ForMember(dto => dto.Content, opt => opt.MapFrom(entity => entity.Content))
               .ForMember(dto => dto.CreationTime, opt => opt.MapFrom(entity => entity.CreationTime));

            CreateMap<CreateMessageRequestDto, Message>()
               .ForMember(entity => entity.Content, opt => opt.MapFrom(dto => dto.Content))
               .ForMember(entity => entity.AuthorId, opt => opt.Ignore())
               .ForMember(entity => entity.CreationTime, opt => opt.Ignore());
        }
    }
}
