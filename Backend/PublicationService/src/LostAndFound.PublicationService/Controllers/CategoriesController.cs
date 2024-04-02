using LostAndFound.PublicationService.Core.CategoryServices.Interfaces;
using LostAndFound.PublicationService.CoreLibrary.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.PublicationService.Controllers
{
    /// <summary>
    /// Categories controller that provides the available categories
    /// </summary>
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Default CategoriesController constructor
        /// </summary>
        /// <param name="categoryService">Instance of ICategoryService interface</param>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException when ICategoryService is null</exception>
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        /// <summary>
        /// Get list of publication categories
        /// </summary>
        /// <returns>List of available categories</returns>
        /// <response code="200">Request succed, return list of categories</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /categories
        ///
        /// </remarks>
        [ResponseCache(Duration = 86400)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetAllCategories()
        {
            var result = await _categoryService.GetAllCategories();

            return Ok(result);
        }
    }
}
