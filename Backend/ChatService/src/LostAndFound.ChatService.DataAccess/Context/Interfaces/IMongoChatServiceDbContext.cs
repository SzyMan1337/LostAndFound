using MongoDB.Driver;

namespace LostAndFound.ChatService.DataAccess.Context.Interfaces
{
    public interface IMongoChatServiceDbContext
    {
        IMongoCollection<BaseDocument> GetCollection<BaseDocument>(string name);
    }
}
