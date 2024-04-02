using AutoMapper;
using LostAndFound.PublicationService.Core.CategoryServices.Interfaces;
using LostAndFound.PublicationService.CoreLibrary.Responses;
using LostAndFound.PublicationService.DataAccess.Repositories.Interfaces;

namespace LostAndFound.PublicationService.Core.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoriesRepository categoriesRepository, IMapper mapper)
        {
            _categoriesRepository = categoriesRepository ?? throw new ArgumentNullException(nameof(categoriesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllCategories()
        {
            var categoriesList = await _categoriesRepository.GetAllCategories();
            var dtosList = _mapper.Map<IEnumerable<CategoryResponseDto>>(categoriesList);

            return dtosList;
        }
    }
}
