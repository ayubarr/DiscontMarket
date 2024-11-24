using DiscontMarket.ApiModels.DTO.BaseDTOs;
using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscontMarket.Services.Services.Implementations
{
    public class FilterService : IFilterService
    {
        private readonly IAttributeRepository _attributeRepository;

        public FilterService(IAttributeRepository attributeRepository)
        {
            _attributeRepository = attributeRepository;
        }

        public Dictionary<string, CategoryFiltersDTO> GetFilters()
        {
            // Пример данных из базы данных
            var data = _attributeRepository.GetAllAttributesGroupedByCategory();

            // Группируем атрибуты по категориям и преобразуем в нужный формат
            var result = data.ToDictionary(
                category => category.Key,
                category => new CategoryFiltersDTO
                {
                    Filters = category.Value.Select(attr => new FilterDTO
                    {
                        Title = attr.Key,
                        Options = attr.Value.Select(option => new FilterOptionDTO
                        {
                            Label = option.Type,
                            Value = option.Type
                        }).ToList()
                    }).ToList()
                }
            );

            return result;
        }
    }

}
