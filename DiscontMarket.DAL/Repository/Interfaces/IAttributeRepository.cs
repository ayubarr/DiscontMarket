using DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Brand;
using DiscontMarket.Domain.Models.Entities;

namespace DiscontMarket.DAL.Repository.Interfaces
{
    public interface IAttributeRepository : IBrandRepository<AttributeEntity>
    {
        Dictionary<string, Dictionary<string, List<FilterAtributeAndBrandDTO>>> GetAllAttributesGroupedByCategory();

    }
}
