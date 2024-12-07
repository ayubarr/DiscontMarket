using DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Brand;
using DiscontMarket.Domain.Models.Entities;

namespace DiscontMarket.DAL.Repository.Interfaces
{
    public interface IAttributeRepository : IBaseRepository<AttributeEntity>
    {
        Dictionary<string, Dictionary<string, List<FilterAtributeAndBrandDTO>>> GetAllAttributesGroupedByCategory();

        List<string> GetAttributeNamesByType(string attributeType);

        List<string> GetAttributeTypesByCategory(Category category);

        public void UpdateAttribute(AttributeEntity entity);

    }
}
