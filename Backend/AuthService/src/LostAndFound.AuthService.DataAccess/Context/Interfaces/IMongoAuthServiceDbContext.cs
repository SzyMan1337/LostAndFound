using MongoDB.Driver;

namespace LostAndFound.AuthService.DataAccess.Context.Interfaces
{
    public interface IMongoAuthServiceDbContext
    {
        IMongoCollection<BaseDocument> GetCollection<BaseDocument>(string name);
    }
}
