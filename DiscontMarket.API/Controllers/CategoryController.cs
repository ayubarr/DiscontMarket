using DiscontMarket.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DiscontMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Route("get-all-names")]
        public IActionResult GetAllNamesByCategory([FromBody] string categoryName)
        {
            var response = _categoryService.GetAllNames();

            if (response.IsSuccess)
                return Ok(response.Data);

            return BadRequest(response.Message);
        }

    }
}
