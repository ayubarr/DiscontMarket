using DiscontMarket.Services.Services.Interfaces;
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
    }
}
