using DiscontMarket.Domain.Models.Entities;
using System.Linq.Expressions;

namespace DiscontMarket.DAL.Repository.Interfaces
{
    public interface IAttributeRepository : IBaseRepository<AttributeEntity>
    {
        public IEnumerable<Product> GetBrandsByCategoryName(string categoryName);

    }
}
