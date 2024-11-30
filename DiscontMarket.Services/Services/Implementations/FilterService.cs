using DiscontMarket.ApiModels.DTO.BaseDTOs;
using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.Services.Services.Interfaces;

namespace DiscontMarket.Services.Services.Implementations
{
    public class FilterService : IFilterService
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly IBrandRepository _brandRepository;


        public FilterService(IAttributeRepository attributeRepository, IBrandRepository brandRepository)
        {
            _attributeRepository = attributeRepository;
            _brandRepository = brandRepository;
        }

        public Dictionary<string, FilterCategoryDTO> GetFilters()
        {
            // Пример данных из базы данных
            var attributeData = _attributeRepository.GetAllAttributesGroupedByCategory();
            var brandData = _brandRepository.GetAllBrandsGroupedByCategory();

            var combinedData = brandData
               .Concat(attributeData)
               .GroupBy(
                   pair => pair.Key, // Группируем по ключу категории
                   pair => pair.Value // Берём значения словаря
               );


            // Группируем атрибуты по категориям и преобразуем в нужный формат
            //var result = combinedData.ToDictionary(
            //    category => category.Key,
            //    category => new FilterCategoryDTO
            //    {
            //        Filters = category.Value.Select(attr => new FilterDTO
            //        {
            //            Title = attr.Key,
            //            Options = attr.Value.Select(option => new FilterOptionDTO
            //            {
            //                Label = option.NameTranslate,
            //                Value = option.Name
            //            }).ToList()
            //        }).ToList()
            //    }
            //);

            var result = combinedData.ToDictionary(
                group => group.Key,
                group => new FilterCategoryDTO
                {
                    Filters = group.SelectMany(valueList => valueList)
                        .Select(item => new FilterDTO
                        {
                            Title = item.Key,
                            Options = item.Value.Select(option => new FilterOptionDTO
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
