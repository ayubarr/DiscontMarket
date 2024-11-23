using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.DAL.SqlServer.Context;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Validation;
using System.Linq.Expressions;

namespace DiscontMarket.DAL.Repository.Implementations
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {

        public ProductRepository(AppDbContext contex) : base(contex)
        {

        }

        public IEnumerable<Product> GetFilteredProductsAsync(Expression<Func<Product, bool>> filter)
        {
            ObjectValidator<Expression<Func<Product, bool>>>.CheckIsNotNullObject(filter);

            return GetAll()
                .Where(filter)
                .ToList();
        }
    }
}
