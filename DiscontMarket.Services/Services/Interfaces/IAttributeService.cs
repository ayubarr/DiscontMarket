using DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.Domain.Models.Entities;

namespace DiscontMarket.Services.Services.Interfaces
{
    public interface IAttributeService : IBaseService<AttributeEntity>
    {
        IBaseResponse<IEnumerable<AttributeEntity>> GetAllByCategoryName(string categoryName);
        IBaseResponse<IEnumerable<string>> GetAllByAttributesByType(string attributeName);
        IBaseResponse<IEnumerable<string>> GetAllAttributeTypesByCategoryName(string categoryName);

        IBaseResponse<AttributeEntity> CreateAttribute(CreateAttributeDTO entityDTO);

        Task<IBaseResponse<bool>> DeleteByNameAsync(string attributeName);


    }
}
