using MongoDB.Driver;

namespace LostAndFound.PublicationService.DataAccess.Context.Interfaces
{
    public interface IMongoPublicationServiceDbContext
    {
        IMongoCollection<BaseDocument> GetCollection<BaseDocument>(string name);
    }
}
