using LostAndFound.AuthService.DataAccess.Entities.Interfaces;
using System.Linq.Expressions;

namespace LostAndFound.AuthService.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T> where T : IDocument
    {
        IQueryable<T> AsQueryable();
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FilterByAsync(Expression<Func<T, bool>> filterExpression);
        Task InsertOneAsync(T document);
        Task InsertManyAsync(ICollection<T> documents);
        Task ReplaceOneAsync(T document);
        Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression);
        Task DeleteManyAsync(Expression<Func<T, bool>> filterExpression);
    }
}
