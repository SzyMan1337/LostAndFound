using LostAndFound.ProfileService.DataAccess.Context.Interfaces;
using LostAndFound.ProfileService.DataAccess.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LostAndFound.ProfileService.DataAccess.Context
{
    public class MongoProfileServiceDbContext : IMongoProfileServiceDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoClient _mongoClient;

        public MongoProfileServiceDbContext(IOptions<ProfileServiceDatabaseSettings> configuration)
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
