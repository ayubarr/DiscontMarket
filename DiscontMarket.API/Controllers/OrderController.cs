using DiscontMarket.ApiModels.DTO.EntityDTOs.Order;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Product;
using DiscontMarket.Services.Services.Implementations;
using DiscontMarket.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DiscontMarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Получить все продукты с фильтром и сортировкой
        [HttpPost]
        [Route("send-info")]
        public async Task<IActionResult> SendInfo([FromBody] string phoneNumber)
        {

            var responseTelegram = await _orderService.SendToTelegramAsync(phoneNumber);
            return Ok(responseTelegram);
        }

    }
}
