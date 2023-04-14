using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TennisBookings.Merchandise.Api.Data;
using TennisBookings.Merchandise.Api.Models.Output;

namespace TennisBookings.Merchandise.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryProvider _categoryProvider;

        public CategoriesController(ICategoryProvider categoryProvider) => 
            _categoryProvider = categoryProvider;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseCache(Duration = 300)]
        public ActionResult<CategoriesOutputModel> GetAll()
        {
            var allowedCategories = _categoryProvider.AllowedCategories();

            return new CategoriesOutputModel(allowedCategories);
        }
    }
}
