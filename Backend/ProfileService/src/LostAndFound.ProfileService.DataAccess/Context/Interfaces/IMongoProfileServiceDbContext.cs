using MongoDB.Driver;

namespace LostAndFound.ProfileService.DataAccess.Context.Interfaces
{
    public interface IMongoProfileServiceDbContext
    {
        IMongoCollection<BaseDocument> GetCollection<BaseDocument>(string name);
    }
}
