using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.DAL.SqlServer.Context;
using DiscontMarket.Domain.Models.Entities;

namespace DiscontMarket.DAL.Repository.Implementations
{
    public class AttributeRepository : BaseRepository<AttributeEntity>, IAttributeRepository
    {
        public AttributeRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetBrandsByCategoryName(string categoryName)
        {
            throw new NotImplementedException();
        }
    }
}
