using LostAndFound.PublicationService.DataAccess.Context.Interfaces;
using LostAndFound.PublicationService.DataAccess.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LostAndFound.PublicationService.DataAccess.Context
{
    public class MongoPublicationServiceDbContext : IMongoPublicationServiceDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoClient _mongoClient;

        public MongoPublicationServiceDbContext(IOptions<PublicationServiceDatabaseSettings> configuration)
        {
            _ = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _mongoClient = new MongoClient(configuration.Value.ConnectionString);
            _database = _mongoClient.GetDatabase(configuration.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}
