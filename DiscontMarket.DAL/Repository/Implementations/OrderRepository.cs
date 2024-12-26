using DiscontMarket.ApiModels.DTO.EntityDTOs.Order;
using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.DAL.SqlServer.Context;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Domain.Models.Enums;
using DiscontMarket.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscontMarket.DAL.Repository.Implementations
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public List<OrderRequest> GetAllOrders()
        {
            return GetAll()
                .Select(o => new OrderRequest
                {
                    phoneNumber = o.PhoneNumber,
                    datetime = o.CreationDate.ToString("dd.MM.yyyy HH:mm"), 
                    name = o.ClientsName,
                    url = o.ProductAddress,
                }).ToList();
        }

        public void UpdateOrder(Order entity)
        {
            ObjectValidator<Order>.CheckIsNotNullObject(entity);

            // Проверяем, существует ли сущность в базе данных по идентификатору
            var existingEntity = _dbSet.Find(entity.ID); // Предполагается, что у T есть свойство Id

            if (existingEntity == null)
            {
                throw new InvalidOperationException("Entity not found.");
            }

            // Обновляем значения полей сущности, за исключением идентификатора, если это необходимо
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);

            _context.SaveChanges();
        }
    }
}
