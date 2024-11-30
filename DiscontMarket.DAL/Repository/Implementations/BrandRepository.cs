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
                .GroupBy(a => a.BrandCategories?.FirstOrDefault()?.Category?.Name) // Группируем по имени категории
                .ToDictionary(
                    categoryGroup => categoryGroup.Key, // Ключ — имя категории
                    categoryGroup => categoryGroup
                        .GroupBy(br => br.Type) // Внутри каждой категории группируем по имени бренда
                        .ToDictionary(
                            attributeGroup => attributeGroup.Key, // Ключ — имя бренда
                            attributeGroup => attributeGroup.Select(br => new FilterAtributeAndBrandDTO
                            {
                                ID = br.ID,
                                Type = br.Type,
                                Name = br.Name,
                                NameTranslate = br.NameTranslate
                            }).ToList() // Преобразуем атрибуты в DTO
                        )
                );
            return data;
        }
    }
}
