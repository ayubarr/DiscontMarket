using DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute;
using DiscontMarket.Domain.Models.Entities;

namespace DiscontMarket.DAL.Repository.Interfaces
{
    public interface IAttributeRepository : IBrandRepository<AttributeEntity>
    {
        Dictionary<string, Dictionary<string, List<FilterAttributeDTO>>> GetAllAttributesGroupedByCategory();

    }
}
