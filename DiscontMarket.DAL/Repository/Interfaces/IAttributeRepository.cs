using DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute;
using DiscontMarket.Domain.Models.Entities;

namespace DiscontMarket.DAL.Repository.Interfaces
{
    public interface IAttributeRepository : IBaseRepository<AttributeEntity>
    {
        Dictionary<string, Dictionary<string, List<FilterAttributeDTO>>> GetAllAttributesGroupedByCategory();

    }
}
