using LostAndFound.PublicationService.DataAccess.Attributes;
using LostAndFound.PublicationService.DataAccess.Context.Interfaces;
using LostAndFound.PublicationService.DataAccess.DatabaseSeeder.Interfaces;
using LostAndFound.PublicationService.DataAccess.Entities;
using MongoDB.Driver;

namespace LostAndFound.PublicationService.DataAccess.DatabaseSeeder
{
    public class MongoDbSeeder : IDbSeeder
    {
        protected readonly IMongoPublicationServiceDbContext _context;

        public MongoDbSeeder(IMongoPublicationServiceDbContext publicationServiceDbContext)
        {
            _context = publicationServiceDbContext ?? throw new ArgumentNullException(nameof(publicationServiceDbContext));
        }

        public void SeedCategoriesCollection()
        {
            var collectionName = (typeof(Category).GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault()
                as BsonCollectionAttribute)!.CollectionName;
            var collection = _context.GetCollection<Category>(collectionName);

            if (collection is not null && !collection.Find(_ => true).Any())
            {
                var categories = GetCategories();
                collection.InsertMany(categories.ToList());
            }
        }

        private static Category[] GetCategories()
        {
            var categories = new Category[]
            {
                CreateCategory("Motoryzacja", "Motorization"),
                CreateCategory("Elektronika", "Electronics"),
                CreateCategory("Zwierzęta", "Pets"),
                CreateCategory("Inne", "Other"),
                CreateCategory("Odzież", "Clothes"),
                CreateCategory("Dokumenty", "Documents"),
                CreateCategory("Biżuteria", "Jewelry"),
                CreateCategory("Torebki", "Handbags"),
                CreateCategory("Sport", "Sport"),
                CreateCategory("Klucze", "Keys"),
                CreateCategory("Portfele", "Wallets"),
                CreateCategory("Leki", "Medicines"),
                CreateCategory("Ksiązki", "Books"),
                CreateCategory("Kosmetyki", "Cosmetics"),
                CreateCategory("Zabawki", "Toys"),
            };

            return categories;
        }

        private static Category CreateCategory(string displayName, string exposedId)
        {
            return new Category()
            {
                DisplayName = displayName,
                ExposedId = exposedId,
                CreationTime = DateTime.Now,
            };
        }
    }
}
