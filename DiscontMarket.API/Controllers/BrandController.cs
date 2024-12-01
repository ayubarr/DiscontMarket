using DiscontMarket.ApiModels.DTO.EntityDTOs.Brand;
using DiscontMarket.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DiscontMarket.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet]
        [Route("get-by-category")]
        public IActionResult GetAllByCategory([FromBody] string categoryName)
        {
            var response = _brandService.GetAllByCategoryName(categoryName);

            if (response.IsSuccess)
                return Ok(response.Data);

            return BadRequest(response.Message);
        }

        [HttpGet]
        [Route("get-types-by-category")]
        public IActionResult GetAllBrandTypesByCategory([FromBody] string categoryName)
        {
            var response = _brandService.GetAllBrandTypesByCategoryName(categoryName);

            if (response.IsSuccess)
                return Ok(response.Data);

            return BadRequest(response.Message);
        }

        // Получение атрибутов по названию атрибута
        [HttpGet]
        [Route("get-names-by-attribute-type")]
        public IActionResult GetAllByBrandName([FromBody] string brandType)
        {
            var response = _brandService.GetAllBrandsByType(brandType);
            if (response.IsSuccess)
                return Ok(response.Data);

            return BadRequest(response.Message);
        }

        // Создание нового бренда
        [HttpPost]
        [Route("create")]
        public IActionResult CreateBrand([FromBody] CreateBrandDTO entityDTO)
        {
            if (entityDTO == null)
                return BadRequest("Invalid brand data.");

            var response = _brandService.CreateBrand(entityDTO);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        // Обновить существующий бренд
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPut]
        [Route("update/{projectid}")]
        public async Task<IActionResult> Update(UpdateBrandDTO brandDto)
        {
            var response = await _brandService.UpdateAsync(brandDto);
            return Ok(response);
        }

        // Удалить бренд по ID
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] string brandName)
        {
            var response = await _brandService.DeleteByNameAsync(brandName);
            return Ok(response);
        }
    }
}
