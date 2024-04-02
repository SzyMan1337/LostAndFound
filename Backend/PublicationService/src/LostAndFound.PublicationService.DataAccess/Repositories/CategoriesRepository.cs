using LostAndFound.PublicationService.DataAccess.Context.Interfaces;
using LostAndFound.PublicationService.DataAccess.Entities;
using LostAndFound.PublicationService.DataAccess.Repositories.Interfaces;
using MongoDB.Driver;

namespace LostAndFound.PublicationService.DataAccess.Repositories
{
    public class CategoriesRepository : BaseRepository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(IMongoPublicationServiceDbContext publicationServiceDbContext) : base(publicationServiceDbContext) { }


        public bool DoesCategoryExist(string categoryId)
        {
            var filter = Builders<Category>.Filter.Eq(acc => acc.ExposedId, categoryId);

            return !_collection.FindSync(filter).Any();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var categories = await _collection.FindAsync(Builders<Category>.Filter.Empty);

            return categories.ToList();
        }
    }
}
