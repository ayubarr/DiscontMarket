using DiscontMarket.ApiModels.DTO.EntityDTOs.Product;
using DiscontMarket.Domain.Models.Enums;
using DiscontMarket.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DiscontMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // Получить все продукты с фильтром и сортировкой
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet]
        [Route("get-all")]
        public IActionResult GetAll([FromQuery] FilterProductDTO projectFilterDto, [FromQuery] SortTypes? sortOrder)
        {
            var response = _productService.GetAllProducts(projectFilterDto, sortOrder);
            return Ok(response.Data);
        }

        // Получить продукт по ID
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> Get(uint id)
        {
            var response = await _productService.GetByIdAsync(id);
            return Ok(response);
        }

        // Создать новый продукт
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CreateProductDTO createProductDto)
        {
            var response = await _productService.CreateAsync(createProductDto);
            return Ok(response);
        }

        // Обновить существующий продукт
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPut]
        [Route("update/{projectid}")]
        public async Task<IActionResult> Update(UpdateProductDTO projectDto)
        {
            var response = await _productService.UpdateAsync(projectDto);
            return Ok(response);
        }

        // Удалить продукт по ID
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(uint id)
        {
            var response = await _productService.DeleteByIdAsync(id);
            return Ok(response);
        }
    }
}
