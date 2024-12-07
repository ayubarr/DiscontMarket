using DiscontMarket.ApiModels.DTO.EntityDTOs.Image;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Product;
using DiscontMarket.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiscontMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // Получить все продукты с фильтром и сортировкой
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAll([FromBody] FilterProductDTO productFilterDto)
        {
            if (!IsCategory(productFilterDto))
            {
                var responseByStatus = _productService.GetAllProductsByStatus(productFilterDto, productFilterDto.Sort, productFilterDto.CategoryDTO.Name);
                return Ok(responseByStatus);
            }

            var response = _productService.GetAllProducts(productFilterDto, productFilterDto.Sort);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-random-category")]
        public IActionResult GetRandomCategory()
        {
            var response = _categoryService.GetAllNames();

            var categories = response.Data.ToList();
            var category = categories[new Random().Next(0, categories.Count)];

            response.Data = new List<string>() { category };

            return Ok(response.Data);
        }

        // Получить все продукты с фильтром и сортировкой
        [HttpGet]
        [Route("get-all-news")]
        public IActionResult GetAllNews()
        {
            var response = _productService.GetAllProductsNews();
            return Ok(response);
        }

        // Получить все продукты с фильтром и сортировкой
        [HttpGet]
        [Route("get-all-hits")]
        public IActionResult GetAllHits()
        {
            var response = _productService.GetAllProductsHits();
            return Ok(response);
        }

        // Получить все продукты с фильтром и сортировкой
        [HttpPost]
        [Route("get-by-id")]
        public IActionResult GetProductByProductID([FromBody] int productId)
        {
            var response = _productService.GetProductById((uint)productId);
            return Ok(response.Data);
        }

        // Получить все продукты с фильтром и сортировкой
        [HttpPost]
        [Route("get-by-name")]
        public IActionResult GetProductByProductName([FromBody] string productName)
        {
            var response = _productService.GetProductsByName(productName);
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
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("create")]
        public IActionResult Create([FromBody] CreateProductDTO newProduct)
        {
            var response = _productService.CreateProduct(newProduct);
            return Ok(response);
        }

        // Обновить существующий продукт
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("update/{projectid}")]
        public async Task<IActionResult> Update(UpdateProductDTO productDto)
        {
            var response = await _productService.UpdateAsync(productDto);
            return Ok(response);
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("delete-by-name")]
        public IActionResult Delete([FromBody]string productName)
        {
            var response = _productService.DeleteByProductName(productName);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            var images = response.Data;
            if (images != null)
            {
                foreach (var image in images)
                {
                    if (!string.IsNullOrEmpty(image.imagePath) && System.IO.File.Exists(image.imagePath))
                    {
                        System.IO.File.Delete(image.imagePath); // Удаление файла
                    }                 
                }
            }


            return Ok(response);
        }

        // Удалить продукт по ID
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(uint id)
        {
            var response = await _productService.DeleteByIdAsync(id);
            return Ok(response);
        }

        private bool IsCategory(FilterProductDTO producttFilterDto)
        {
            if (producttFilterDto == null || producttFilterDto.CategoryDTO == null || producttFilterDto.CategoryDTO.Name == null) return false;

            switch (producttFilterDto.CategoryDTO.Name.ToLower())
            {
                case "discount":
                    break;

                case "damagedpackage":
                    break;

                case "minordefects":
                    break;

                default:
                    return true;
            }
            return false;
        }
        private string GetCategoryName()
        {
            var categories = GetCategories().ToList();
            return categories[new Random().Next(0, categories.Count)];
        }

        private IEnumerable<string> GetCategories()
        {
            return _categoryService.GetAllNames().Data;
        }
    }
}
