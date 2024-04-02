using LostAndFound.ChatService.DataAccess.Context.Interfaces;
using LostAndFound.ChatService.DataAccess.Entities;
using LostAndFound.ChatService.DataAccess.Repositories.Interfaces;
using MongoDB.Driver;

namespace LostAndFound.ChatService.DataAccess.Repositories
{
    public class ChatsRepository : BaseRepository<Chat>, IChatsRepository
    {
        public ChatsRepository(IMongoChatServiceDbContext chatServiceDbContext) : base(chatServiceDbContext) { }


        public async Task<IEnumerable<Chat>> GetUserChatsWithUnreadMessage(Guid userId)
        {
            var filter = Builders<Chat>.Filter.Where(c => c.Members.Any(m => m.Id == userId))
                & Builders<Chat>.Filter.Eq(c => c.ContainUnreadMessage, true);

            return (await _collection.FindAsync(filter)).ToEnumerable();
        }

        public async Task InsertNewChatMessage(Guid chatId, Message messageEntity)
        {
            var filter = Builders<Chat>.Filter.Eq(c => c.ExposedId, chatId);
            var update = Builders<Chat>.Update.Push(c => c.Messages, messageEntity)
                .Set(c => c.ContainUnreadMessage, true);

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task ReadChatMessages(Chat chatEntity)
        {
            var filter = Builders<Chat>.Filter
                .Eq(chat => chat.ExposedId, chatEntity.ExposedId);
            var update = Builders<Chat>.Update
                .Set(chat => chat.ContainUnreadMessage, chatEntity.ContainUnreadMessage);

            await _collection.UpdateOneAsync(filter, update);
        }
    }
}
