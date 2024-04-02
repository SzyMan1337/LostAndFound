using LostAndFound.ChatService.CoreLibrary.Internal;
using LostAndFound.ChatService.CoreLibrary.ResourceParameters;
using LostAndFound.ChatService.CoreLibrary.Responses;

namespace LostAndFound.ChatService.Core.ChatServices.Interfaces
{
    public interface IChatActionService
    {
        Task<(IEnumerable<ChatBaseDataResponseDto>?, PaginationMetadata)> GetChats(
            string rawUserId, ChatsResourceParameters chatsResource);
        Task ReadChatMessages(string rawUserId, Guid chatMemberId);
        Task<ChatNotificationResponseDto> GetUnreadChatNotification(string rawUserId);
    }
}
