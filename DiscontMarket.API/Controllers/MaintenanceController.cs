using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DiscontMarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceController : ControllerBase
    {
        private readonly string _filePath;

        public MaintenanceController(IWebHostEnvironment env)
        {
            // Формируем абсолютный путь к maintenance.json
            _filePath = Path.Combine(env.WebRootPath, "js", "json", "maintenance.json");
        }


        // GET: Получение текущего состояния
        [HttpGet]
        [Route("get-maintenance")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetMaintenanceState()
        {
            if (System.IO.File.Exists(_filePath))
            {
                var json = System.IO.File.ReadAllText(_filePath);
                return Ok(JsonSerializer.Deserialize<object>(json));
            }

            return Ok(new { is_under_maintenance = false });
        }

        // POST: Обновление состояния
        [HttpPost]
        [Route("set-maintenance")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> SetMaintenanceState([FromBody] MaintenanceState isUnderMaintenance)
        {
            if (isUnderMaintenance == null || isUnderMaintenance.IsUnderMaintenance == null)
            {
                return BadRequest(new { success = false, message = "Invalid input" });
            }

            var newState = new { is_under_maintenance = isUnderMaintenance.IsUnderMaintenance };
            var json = JsonSerializer.Serialize(newState);

            await System.IO.File.WriteAllTextAsync(_filePath, json);

            return Ok(new { success = true });
        }

        public class MaintenanceState
        {
            public bool? IsUnderMaintenance { get; set; }
        }
    }
}
