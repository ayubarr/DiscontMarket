using DiscontMarket.ApiModels.DTO.EntityDTOs.Brand;
using DiscontMarket.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Route("get-by-category")]
        public IActionResult GetAllByCategory([FromBody] string categoryName)
        {
            var response = _brandService.GetAllByCategoryName(categoryName);

            if (response.IsSuccess)
                return Ok(response.Data);

            return BadRequest(response.Message);
        }

        [HttpPost]
        [Route("get-types-by-category")]
        public IActionResult GetAllBrandTypesByCategory([FromBody] string categoryName)
        {
            var response = _brandService.GetAllBrandTypesByCategoryName(categoryName);

            if (response.IsSuccess)
                return Ok(response.Data);

            return BadRequest(response.Message);
        }

        // Получение атрибутов по названию атрибута
        [HttpPost]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
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
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("update/{projectid}")]
        public async Task<IActionResult> Update(UpdateBrandDTO brandDto)
        {
            var response = await _brandService.UpdateAsync(brandDto);
            return Ok(response);
        }

        // Удалить бренд по ID
        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] string brandName)
        {
            var response = await _brandService.DeleteByNameAsync(brandName);
            return Ok(response);
        }
    }
}
