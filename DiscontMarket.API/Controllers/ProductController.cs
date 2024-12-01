using DiscontMarket.ApiModels.DTO.EntityDTOs.Product;
using DiscontMarket.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAll([FromBody] FilterProductDTO projectFilterDto)
        {
            var response = _productService.GetAllProducts(projectFilterDto, projectFilterDto.SortOrder);
            return Ok(response);
        }

        // Получить все продукты с фильтром и сортировкой
        [HttpPost]
        [Route("get-by-id")]
        public IActionResult GetProductByProductID([FromBody] uint productId)
        {
            var response = _productService.GetProductById(productId);
            return Ok(response);
        }

        // Получить все продукты с фильтром и сортировкой
        [HttpPost]
        [Route("get-by-name")]
        public IActionResult GetProductByProductName([FromBody] string productName)
        {
            var response = _productService.GetProductByName(productName);
            return Ok(response);
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
        public  IActionResult Create(CreateProductDTO createProductDto)
        {
            var response =  _productService.CreateProduct(createProductDto);
            return Ok(response);
        }

        // Обновить существующий продукт
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPut]
        [Route("update/{projectid}")]
        public async Task<IActionResult> Update(UpdateProductDTO productDto)
        {
            var response = await _productService.UpdateAsync(productDto);
            return Ok(response);
        }


        // Удалить продукт по ID
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete]
        [Route("delete-by-name")]
        public IActionResult Delete(string name)
        {
            var response =  _productService.DeleteByProductName(name);
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
