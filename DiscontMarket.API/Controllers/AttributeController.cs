
using DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Product;
using DiscontMarket.Services.Services.Implementations;
using DiscontMarket.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DiscontMarket.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        private readonly IAttributeService _attributeService;

        public AttributeController(IAttributeService attributeService)
        {
            _attributeService = attributeService;
        }

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPost]
        [Route("get-by-category")]
        public IActionResult GetAll([FromBody] string categoryName)
        {
            var response = _attributeService.GetAllByCategoryName(categoryName);
            if (response.IsSuccess)
                return Ok(response.Data);

            return BadRequest(response.Message);
        }

        // Получение атрибутов по названию атрибута
        [HttpPost]
        [Route("get-by-attribute")]
        public IActionResult GetAllByAttributeName([FromBody] string attributeName)
        {
            var response = _attributeService.GetAllByAttributesName(attributeName);
            if (response.IsSuccess)
                return Ok(response.Data);

            return BadRequest(response.Message);
        }

        // Создание нового атрибута
        [HttpPost]
        [Route("create")]
        public IActionResult CreateAttribute([FromBody] CreateAttributeDTO entityDTO)
        {
            if (entityDTO == null)
                return BadRequest("Invalid attribute data.");

            var response = _attributeService.CreateAttribute(entityDTO);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        // Обновить существующий продукт
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPut]
        [Route("update/{projectid}")]
        public async Task<IActionResult> Update(UpdateAttributeDTO attributeDto)
        {
            var response = await _attributeService.UpdateAsync(attributeDto);
            return Ok(response);
        }

        // Удалить продукт по ID
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] string attributeName)
        {
            var response = await _attributeService.DeleteByNameAsync(attributeName);
            return Ok(response);
        }
    }
}
