using LostAndFound.PublicationService.DataAccess.Entities;

namespace LostAndFound.PublicationService.DataAccess.Repositories.Interfaces
{
    public interface ICategoriesRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllCategories();
        bool DoesCategoryExist(string categoryId);
    }
}
