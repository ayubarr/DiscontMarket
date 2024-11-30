using DiscontMarket.ApiModels.DTO.BaseDTOs;
using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.Services.Services.Interfaces;

namespace DiscontMarket.Services.Services.Implementations
{
    public class FilterService : IFilterService
    {
        private readonly IAttributeRepository _attributeRepository;


        public FilterService(IAttributeRepository attributeRepository)
        {
            _attributeRepository = attributeRepository;
        }

        public Dictionary<string, FilterCategoryDTO> GetFilters()
        {
            // Пример данных из базы данных
            var data = _attributeRepository.GetAllAttributesGroupedByCategory();

            // Группируем атрибуты по категориям и преобразуем в нужный формат
            var result = data.ToDictionary(
                category => category.Key,
                category => new FilterCategoryDTO
                {
                    Filters = category.Value.Select(attr => new FilterDTO
                    {
                        Title = attr.Key,
                        Options = attr.Value.Select(option => new FilterOptionDTO
                        {
                            Label = option.NameTranslate,
                            Value = option.Name
                        }).ToList()
                    }).ToList()
                }
            );

            return result;
        }
    }

}
