using LostAndFound.PublicationService.CoreLibrary.Responses;

namespace LostAndFound.PublicationService.Core.CategoryServices.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDto>> GetAllCategories();
    }
}
