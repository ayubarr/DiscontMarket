using DiscontMarket.ApiModels.DTO.BaseDTOs;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiscontMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly IFilterService _filterService;

        public FilterController(IFilterService filterService)
        {
            _filterService = filterService;
        }

        [HttpGet]
        [Route("get-filters")]
        public IActionResult GetFilters()
        {
            var filters = _filterService.GetFilters();
            return Ok(filters);
        }

        [HttpPost]
        [Route("set-filters")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult SetFilters([FromBody] Dictionary<string, FilterCategoryDTO> data)
        {
            var response = _filterService.SetFilters(data);
            return Ok(response);

        }

        [HttpPost]
        [Route("get-min")]
        public IActionResult GetMinPrice([FromBody] string category)
        {
            var response = _filterService.GetMinPriceByCategory(category);
            return Ok(response.Data);

        }

        [HttpPost]
        [Route("get-max")]
        public IActionResult GetMaxPrice([FromBody] string category)
        {
            var response = _filterService.GetMaxPriceByCategory(category);
            return Ok(response.Data); 
        }
    }
}
