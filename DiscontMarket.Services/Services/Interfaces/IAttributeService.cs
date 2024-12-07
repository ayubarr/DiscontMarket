using DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.Domain.Models.Entities;

namespace DiscontMarket.Services.Services.Interfaces
{
    public interface IAttributeService : IBaseService<AttributeEntity>
    {
        IBaseResponse<IEnumerable<AttributeEntity>> GetAllByCategoryName(string categoryName);
        IBaseResponse<IEnumerable<string>> GetAllAttributesByType(string attributeName);
        IBaseResponse<IEnumerable<string>> GetAllAttributeTypesByCategoryName(string categoryName);

        IBaseResponse<bool> CreateAttribute(CreateAttributeDTO entityDTO);

        IBaseResponse<CategoryFilters> GetAllNames(string categoryName);

        Task<IBaseResponse<bool>> DeleteByNameAsync(string attributeName);
        IBaseResponse<bool> DeleteByName(string attributeName);


    }
}
