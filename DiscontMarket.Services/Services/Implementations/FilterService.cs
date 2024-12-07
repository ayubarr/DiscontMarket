using DiscontMarket.ApiModels.DTO.BaseDTOs;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Brand;
using DiscontMarket.ApiModels.Responce.Helpers;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        public IBaseResponse<bool> SetFilters(Dictionary<string, FilterCategoryDTO> updatedFilters)
        {
            try
            {
                // Получаем текущие данные из базы
                var currentAttributes = _attributeRepository.GetAllAttributesGroupedByCategory();
                var currentBrands = _brandRepository.GetAllBrandsGroupedByCategory();

                // Обрабатываем изменения
                foreach (var updatedCategory in updatedFilters)
                {
                    var categoryName = updatedCategory.Key;
                    var updatedFilterCategory = updatedCategory.Value;

                    // Атрибуты
                    if (currentAttributes.ContainsKey(categoryName))
                    {
                        UpdateAttributes(currentAttributes[categoryName], updatedFilterCategory.Filters);
                    }

                    // Бренды
                    if (currentBrands.ContainsKey(categoryName))
                    {
                        UpdateBrands(currentBrands[categoryName], updatedFilterCategory.Filters);
                    }
                }

                // Сохраняем изменения
                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (Exception ex)
            {
                return ResponseFactory<bool>.CreateErrorResponse(ex);
            }
        }

        private void UpdateAttributes(
        Dictionary<string, List<FilterAtributeAndBrandDTO>> currentAttributes,
        List<FilterDTO> updatedFilters)
        {
            foreach (var updatedFilter in updatedFilters)
            {
                var type = updatedFilter.Title;
                if (currentAttributes.ContainsKey(type))
                {
                    var currentItems = currentAttributes[type];

                    // Обновляем или добавляем новые атрибуты
                    foreach (var updatedOption in updatedFilter.Options)
                    {
                        var existingItem = currentItems.FirstOrDefault(x => x.Name == updatedOption.Value);
                        if (existingItem != null)
                        {
                            // Обновление
                            existingItem.NameTranslate = updatedOption.Label;
                        }
                        else
                        {
                            // Добавление нового
                            var newItem = new FilterAtributeAndBrandDTO
                            {
                                Type = type,
                                Name = updatedOption.Value,
                                NameTranslate = updatedOption.Label
                            };
                            currentItems.Add(newItem);

                            var newItemDb = new AttributeEntity
                            {
                                Type = type,
                                Name = updatedOption.Value,
                                NameTranslate = updatedOption.Label
                            };
                            _attributeRepository.Create(newItemDb);
                        }
                    }

                    // Удаляем удаленные атрибуты
                    var updatedNames = updatedFilter.Options.Select(x => x.Value).ToHashSet();
                    var itemsToRemove = currentItems.Where(x => !updatedNames.Contains(x.Name)).ToList();
                    foreach (var itemToRemove in itemsToRemove)
                    {

                        var itemToRemoveDb = new AttributeEntity
                        {
                            Type = itemToRemove.Type,
                            Name = itemToRemove.Name,
                            NameTranslate = itemToRemove.NameTranslate
                        };
                        currentItems.Remove(itemToRemove);                      
                        _attributeRepository.Delete(itemToRemoveDb);
                    }
                }
            }
        }

        private void UpdateBrands(
            Dictionary<string, List<FilterAtributeAndBrandDTO>> currentBrands,
            List<FilterDTO> updatedFilters)
        {
            foreach (var updatedFilter in updatedFilters)
            {
                var type = updatedFilter.Title;
                if (currentBrands.ContainsKey(type))
                {
                    var currentItems = currentBrands[type];

                    // Обновляем или добавляем новые бренды
                    foreach (var updatedOption in updatedFilter.Options)
                    {
                        var existingItem = currentItems.FirstOrDefault(x => x.Name == updatedOption.Value);
                        if (existingItem != null)
                        {
                            // Обновление
                            existingItem.NameTranslate = updatedOption.Label;
                        }
                        else
                        {
                            // Добавление нового
                            var newItem = new FilterAtributeAndBrandDTO
                            {
                                Type = type,
                                Name = updatedOption.Value,
                                NameTranslate = updatedOption.Label
                            };
                            currentItems.Add(newItem);

                            var newItemDb = new Brand
                            {
                                Type = type,
                                Name = updatedOption.Value,
                                NameTranslate = updatedOption.Label
                            };
                            _brandRepository.Create(newItemDb);
                        }
                    }

                    // Удаляем удаленные бренды
                    var updatedNames = updatedFilter.Options.Select(x => x.Value).ToHashSet();
                    var itemsToRemove = currentItems.Where(x => !updatedNames.Contains(x.Name)).ToList();
                    foreach (var itemToRemove in itemsToRemove)
                    {
                        var itemToRemoveDb = new Brand
                        {
                            Type = itemToRemove.Type,
                            Name = itemToRemove.Name,
                            NameTranslate = itemToRemove.NameTranslate
                        };
                        currentItems.Remove(itemToRemove);
                        _brandRepository.Delete(itemToRemoveDb);
                    }
                }
            }
        }
    }

}
