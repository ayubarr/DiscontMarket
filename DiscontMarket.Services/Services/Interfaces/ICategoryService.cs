using DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Category;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.Domain.Models.Entities;

namespace DiscontMarket.Services.Services.Interfaces
{
    public interface ICategoryService : IBaseService<Category>
    {
        IBaseResponse<bool> CreateCategory(CreateCategoryDTO entityDTO);
        IBaseResponse<IEnumerable<string>> GetAllNames();
        Task<IBaseResponse<bool>> DeleteByNameAsync(string attributeName);

        IBaseResponse<bool> DeleteByName(string name);

    }
}
