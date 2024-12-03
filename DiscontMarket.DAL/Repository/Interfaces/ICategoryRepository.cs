using DiscontMarket.Domain.Models.Entities;

namespace DiscontMarket.DAL.Repository.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        List<string> GetAllCategoryNames();
    }
}
