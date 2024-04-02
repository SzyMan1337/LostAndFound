using LostAndFound.ChatService.DataAccess.Entities;

namespace LostAndFound.ChatService.DataAccess.Repositories.Interfaces
{
    public interface IChatsRepository : IRepository<Chat>
    {
        Task InsertNewChatMessage(Guid chatId, Message messageEntity);
        Task ReadChatMessages(Chat chatEntity);
        Task<IEnumerable<Chat>> GetUserChatsWithUnreadMessage(Guid userId);
    }
}
