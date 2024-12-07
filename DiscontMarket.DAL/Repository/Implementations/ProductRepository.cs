using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.DAL.SqlServer.Context;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Validation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DiscontMarket.DAL.Repository.Implementations
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {

        public ProductRepository(AppDbContext contex) : base(contex)
        {

        }

        public IEnumerable<Product> GetFilteredProducts(Expression<Func<Product, bool>> filter)
        {
            return GetAll()
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.ProductAttributes)
                    .ThenInclude(pa => pa.Attribute) // если нужно загружать атрибуты
                .AsEnumerable()
                .Where(filter.Compile())
                .ToList();
        }

        void IProductRepository.UpdateProduct(Product entity)
        {
            ObjectValidator<Product>.CheckIsNotNullObject(entity);

            // Проверяем, существует ли сущность в базе данных по идентификатору
            var existingEntity =  _dbSet.Find(entity.ID); // Предполагается, что у T есть свойство Id

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
