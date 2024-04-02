using LostAndFound.ChatService.DataAccess.Attributes;
using LostAndFound.ChatService.DataAccess.Context.Interfaces;
using LostAndFound.ChatService.DataAccess.Entities.Interfaces;
using LostAndFound.ChatService.DataAccess.Repositories.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace LostAndFound.ChatService.DataAccess.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : IDocument
    {
        protected readonly IMongoCollection<T> _collection;
        protected readonly IMongoChatServiceDbContext _context;

        public BaseRepository(IMongoChatServiceDbContext chatServiceDbContext)
        {
            _context = chatServiceDbContext ?? throw new ArgumentNullException(nameof(chatServiceDbContext));
            _collection = chatServiceDbContext.GetCollection<T>(GetCollectionName());
        }

        private static string GetCollectionName()
        {
            return (typeof(T).GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault()
                as BsonCollectionAttribute)!.CollectionName;
        }

        public virtual IQueryable<T> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public virtual async Task DeleteManyAsync(Expression<Func<T, bool>> filterExpression)
        {
            await _collection.DeleteManyAsync(filterExpression);
        }

        public virtual async Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            await _collection.FindOneAndDeleteAsync(filterExpression);
        }

        public virtual async Task<IEnumerable<T>> FilterByAsync(Expression<Func<T, bool>> filterExpression)
        {
            return (await _collection.FindAsync(filterExpression)).ToEnumerable();
        }

        public virtual async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate).FirstOrDefaultAsync();
        }

        public virtual async Task InsertManyAsync(ICollection<T> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        public virtual async Task InsertOneAsync(T document)
        {
            await _collection.InsertOneAsync(document);
        }

        public virtual async Task ReplaceOneAsync(T document)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }
    }
}
