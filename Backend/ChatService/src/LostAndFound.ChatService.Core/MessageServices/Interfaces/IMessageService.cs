using LostAndFound.ChatService.CoreLibrary.Internal;
using LostAndFound.ChatService.CoreLibrary.Requests;
using LostAndFound.ChatService.CoreLibrary.ResourceParameters;
using LostAndFound.ChatService.CoreLibrary.Responses;

namespace LostAndFound.ChatService.Core.MessageServices.Interfaces
{
    public interface IMessageService
    {
        Task<(IEnumerable<MessageResponseDto>?, PaginationMetadata)> GetChatMessages(string rawUserId,
            MessagesResourceParameters messagesResourceParameters, Guid recipentId);
        Task<MessageResponseDto> SendMessage(string rawUserId, 
            CreateMessageRequestDto messageRequestDto, Guid recipentId);
    }
}
