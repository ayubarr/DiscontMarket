using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.DAL.SqlServer.Context;
using DiscontMarket.Domain.Models.Entities;

namespace DiscontMarket.DAL.Repository.Implementations
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public List<string> GetAllCategoryNames()
        {
            return _context.Categories
                           .Select(a => a.Name)
                           .Distinct()
                           .ToList();
        }
    }
}
