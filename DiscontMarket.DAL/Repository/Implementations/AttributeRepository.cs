using DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Brand;
using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.DAL.SqlServer.Context;
using DiscontMarket.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscontMarket.DAL.Repository.Implementations
{
    public class AttributeRepository : BaseRepository<AttributeEntity>, IAttributeRepository
    {
        public AttributeRepository(AppDbContext context) : base(context)
        {
        }

        public Dictionary<string, Dictionary<string, List<FilterAtributeAndBrandDTO>>> GetAllAttributesGroupedByCategory()
        {
            // Извлекаем данные из базы, включая связанные категории через AttributeCategories
            var data = _context.Attributes
                .Include(a => a.AttributeCategories)  // Подгружаем AttributeCategories
                .ThenInclude(ac => ac.Category)       // Подгружаем саму категорию через AttributeCategories
                .AsEnumerable()                      // Переключаемся на LINQ to Objects для удобства группировки
                .GroupBy(a => a.AttributeCategories?.FirstOrDefault()?.Category?.Name) // Группируем по имени категории
                .ToDictionary(
                    categoryGroup => categoryGroup.Key, // Ключ — имя категории
                    categoryGroup => categoryGroup
                        .GroupBy(attr => attr.Type) // Внутри каждой категории группируем по имени атрибута
                        .ToDictionary(
                            attributeGroup => attributeGroup.Key, // Ключ — имя атрибута
                            attributeGroup => attributeGroup.Select(attr => new FilterAtributeAndBrandDTO
                            {
                                ID = attr.ID,
                                Type = attr.Type,
                                Name = attr.Name,
                                NameTranslate = attr.NameTranslate
                            }).ToList() // Преобразуем атрибуты в DTO
                        )
                );
            return data;
        }

        public List<string> GetAttributeNamesByType(string attributeType)
        {
            return _context.Attributes
                .Where(x => x.Type.ToLower()
                .Equals(attributeType.ToLower()))
                .Select(x => x.Name)
                .ToList();
        }

        public List<string> GetAttributeTypesByCategory(Category category)
        {
            return _context.Attributes
               .Where(a => a.AttributeCategories.Any(c => c.CategoryID == category.ID))
               .Select(a => a.Type)
               .Distinct()
               .ToList();

        }
    }
}
