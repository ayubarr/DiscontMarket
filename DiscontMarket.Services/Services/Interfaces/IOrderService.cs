using DiscontMarket.ApiModels.DTO.EntityDTOs.Order;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.Domain.Models.Entities;

namespace DiscontMarket.Services.Services.Interfaces
{
    public interface IOrderService : IBaseService<Order>
    {
        public Task<IBaseResponse<bool>> SendToTelegramAsync(OrderRequest order);
        public IBaseResponse<bool> SendToEmail(OrderRequest order);
        public IBaseResponse<bool> SetOrder(OrderRequest order);
        public IBaseResponse<List<OrderRequest>> GetOrders();


    }
}
