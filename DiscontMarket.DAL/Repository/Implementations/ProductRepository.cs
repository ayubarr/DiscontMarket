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
            ObjectValidator<Expression<Func<Product, bool>>>.CheckIsNotNullObject(filter);
            var products = GetAll()
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.ProductAttributes)
                    .ThenInclude(pa => pa.Attribute) // если нужно загружать атрибуты
                .AsEnumerable()
                .ToList();


            return GetAll()
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.ProductAttributes)
                    .ThenInclude(pa => pa.Attribute) // если нужно загружать атрибуты
                .AsEnumerable()
                .Where(filter.Compile())
                .ToList();
        }
    }
}
