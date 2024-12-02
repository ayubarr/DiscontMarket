using DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Brand;
using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.DAL.SqlServer.Context;
using DiscontMarket.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscontMarket.DAL.Repository.Implementations
{
    public class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext context) : base(context)
        {
        }

        public Dictionary<string, Dictionary<string, List<FilterAtributeAndBrandDTO>>> GetAllBrandsGroupedByCategory()
        {
            // Извлекаем данные из базы, включая связанные категории через BrandCategories
            var data = _context.Brands
                .Include(a => a.BrandCategories)  // Подгружаем BrandCategories
                .ThenInclude(ac => ac.Category)       // Подгружаем саму категорию через BrandCategories
                .AsEnumerable()                      // Переключаемся на LINQ to Objects для удобства группировки
                .SelectMany(b => b.BrandCategories.Select(bc => new
                {
                    CategoryName = bc.Category.Name,
                    Brand = b
                }))
                .GroupBy(x => x.CategoryName) // Группируем по имени категории
                .ToDictionary(
                    categoryGroup => categoryGroup.Key, // Ключ — имя категории
                    categoryGroup => categoryGroup
                        .GroupBy(br => br.Brand.Type) // Внутри каждой категории группируем по имени бренда
                        .ToDictionary(
                            attributeGroup => attributeGroup.Key, // Ключ — имя бренда
                            attributeGroup => attributeGroup.Select(br => new FilterAtributeAndBrandDTO
                            {
                                ID = br.Brand.ID,
                                Type = br.Brand.Type,
                                Name = br.Brand.Name,
                                NameTranslate = br.Brand.NameTranslate
                            }).ToList() // Преобразуем атрибуты в DTO
                        )
                );
            return data;
        }

        public List<string> GetBrandNamesByType(string brandType)
        {
            return _context.Brands
                           .Where(x => x.Type.ToLower()
                           .Equals(brandType.ToLower()))
                           .Select(x => x.Name)
                           .ToList();
        }

        public List<string> GetBrandTypesByCategory(Category category)
        {
            return _context.Brands
                         .Where(a => a.BrandCategories.Any(c => c.CategoryID == category.ID))
                         .Select(a => a.Type)
                         .Distinct()
                         .ToList();
        }
    }
}
