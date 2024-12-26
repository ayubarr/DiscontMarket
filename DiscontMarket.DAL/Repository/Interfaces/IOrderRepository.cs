using DiscontMarket.ApiModels.DTO.EntityDTOs.Order;
using DiscontMarket.Domain.Models.Entities;

namespace DiscontMarket.DAL.Repository.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        List<OrderRequest> GetAllOrders();
        void UpdateOrder(Order entity);

    }
}
