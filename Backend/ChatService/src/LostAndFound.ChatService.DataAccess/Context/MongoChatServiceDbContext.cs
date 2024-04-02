using LostAndFound.ChatService.DataAccess.Context.Interfaces;
using LostAndFound.ChatService.DataAccess.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LostAndFound.ChatService.DataAccess.Context
{
    public class MongoChatServiceDbContext : IMongoChatServiceDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoClient _mongoClient;

        public MongoChatServiceDbContext(IOptions<ChatServiceDatabaseSettings> configuration)
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
