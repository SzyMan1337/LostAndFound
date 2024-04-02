using LostAndFound.AuthService.DataAccess.Context.Interfaces;
using LostAndFound.AuthService.DataAccess.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LostAndFound.AuthService.DataAccess.Context
{
    public class MongoAuthServiceDbContext : IMongoAuthServiceDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoClient _mongoClient;

        public MongoAuthServiceDbContext(IOptions<AuthServiceDatabaseSettings> configuration)
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
