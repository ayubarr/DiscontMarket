using DiscontMarket.ApiModels.DTO.EntityDTOs.Order;
using DiscontMarket.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiscontMarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public OrderController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        // Получить все продукты с фильтром и сортировкой
        [HttpPost]
        [Route("send-info")]
        public async Task<IActionResult> SendInfo(OrderRequest order)
        {
            _orderService.SetOrder(order);
            var response = _orderService.SendToEmail(order);
            await _orderService.SendToTelegramAsync(order);

            return Ok(response);
        }

        // Получить все продукты с фильтром и сортировкой
        [HttpGet]
        [Route("get-email")]
        public async Task<IActionResult> GetEmail()
        {
            var response = await _userService.GetAdminsEmail();
            return Ok(response);
        }

        // Получить все продукты с фильтром и сортировкой
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("get-orders")]
        public async Task<IActionResult> GetOrders()
        {
            var response = _orderService.GetOrders();
            return Ok(response);
        }
    }
}
