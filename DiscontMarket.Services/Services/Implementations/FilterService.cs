using DiscontMarket.ApiModels.DTO.BaseDTOs;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Brand;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Category;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Product;
using DiscontMarket.ApiModels.Responce.Helpers;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.Domain.Models.Enums;
using DiscontMarket.Services.Services.Interfaces;
using DiscontMarket.Validation;
using Microsoft.EntityFrameworkCore;

namespace DiscontMarket.Services.Services.Implementations
{
    public class FilterService : IFilterService
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IAttributeService _attributeService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;



        public FilterService(IAttributeRepository attributeRepository, IBrandRepository brandRepository,
            IAttributeService attributeService, IBrandService brandService, ICategoryService categoryService, IProductService productService)
        {
            _attributeRepository = attributeRepository;
            _brandRepository = brandRepository;
            _attributeService = attributeService;
            _brandService = brandService;
            _categoryService = categoryService;
            _productService = productService;
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
                            Options = item.Value.Select(option => new OptionDTO
                            {
                                Label = option.NameTranslate,
                                Value = option.Name
                            }).ToList()
                        }).ToList()
                }
            );

            return result;
        }

        public IBaseResponse<int> GetMaxPriceByCategory(string category)
        {
            try
            {
                StringValidator.CheckIsNotNull(category);

                var productDto = new FilterProductDTO();

                if (!IsCategory(category))
                {
                    var productsWithoutCategory = _productService.GetAllProductsByStatus(productDto, SortTypes.Popularity.ToString(), category);

                    ObjectValidator<IBaseResponse<IEnumerable<ProductDTO>>>.CheckIsNotNullObject(productsWithoutCategory);
                    ObjectValidator<IEnumerable<ProductDTO>>.CheckIsNotNullObject(productsWithoutCategory.Data);

                    int maxPriceWithoutCategory = (int)productsWithoutCategory.Data.Max(x => x.price);

                    return ResponseFactory<int>.CreateSuccessResponse(maxPriceWithoutCategory);
                }



                productDto.CategoryDTO = new CategoryDTO { Name = category };
                var products = _productService.GetAllProducts(productDto, SortTypes.Popularity.ToString());

                ObjectValidator<IBaseResponse<IEnumerable<ProductDTO>>>.CheckIsNotNullObject(products);
                ObjectValidator<IEnumerable<ProductDTO>>.CheckIsNotNullObject(products.Data);

                int maxPrice = (int)products.Data.Max(x => x.price);


                // Сохраняем изменения
                return ResponseFactory<int>.CreateSuccessResponse(maxPrice);
            }
            catch (Exception ex)
            {
                return ResponseFactory<int>.CreateErrorResponse(ex);
            }
        }

        public IBaseResponse<int> GetMinPriceByCategory(string category)
        {
            try
            {
                StringValidator.CheckIsNotNull(category);           
                var productDto = new FilterProductDTO();

                if (!IsCategory(category))
                {
                    var productsWithoutCategory = _productService.GetAllProductsByStatus(productDto, SortTypes.Popularity.ToString(), category);

                    ObjectValidator<IBaseResponse<IEnumerable<ProductDTO>>>.CheckIsNotNullObject(productsWithoutCategory);
                    ObjectValidator<IEnumerable<ProductDTO>>.CheckIsNotNullObject(productsWithoutCategory.Data);

                    int minPriceAllProducts = (int)productsWithoutCategory.Data.Min(x => x.price);

                    return ResponseFactory<int>.CreateSuccessResponse(minPriceAllProducts);
                }


                productDto.CategoryDTO = new CategoryDTO { Name = category };

                var products = _productService.GetAllProducts(productDto, SortTypes.Popularity.ToString());

                ObjectValidator<IBaseResponse<IEnumerable<ProductDTO>>>.CheckIsNotNullObject(products);
                ObjectValidator<IEnumerable<ProductDTO>>.CheckIsNotNullObject(products.Data);

                int minPrice = (int)products.Data.Min(x => x.price);

                return ResponseFactory<int>.CreateSuccessResponse(minPrice);
            }
            catch (Exception ex)
            {
                return ResponseFactory<int>.CreateErrorResponse(ex);
            }
        }

        private bool IsCategory(string category)
        {
            switch (category.ToLower())
            {
                case "discount":
                    break;

                case "damagedpackage":
                    break;

                case "minordefects":
                    break;

                default:
                    return true;
            }
            return false;
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
                        UpdateAttributes(currentAttributes[categoryName], updatedFilterCategory.Filters, categoryName);
                    }

                    // Бренды
                    if (currentBrands.ContainsKey(categoryName))
                    {
                        UpdateBrands(currentBrands[categoryName], updatedFilterCategory.Filters, categoryName);
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

        private void UpdateAttributes(Dictionary<string, List<FilterAtributeAndBrandDTO>> currentAttributes, List<FilterDTO> updatedFilters, string categoryName)
        {
            var updatedCategories = updatedFilters.Select(filter => filter.Title).ToHashSet(StringComparer.OrdinalIgnoreCase);

            var categoriesToRemove = currentAttributes.Keys
                .Where(currentCategory => !updatedCategories.Contains(currentCategory))
                .ToList();

            foreach (var categoryToRemove in categoriesToRemove)
            {
                var itemsToRemove = currentAttributes[categoryToRemove];

                foreach (var item in itemsToRemove)
                {
                    _attributeService.DeleteByName(item.Name);
                }

                currentAttributes.Remove(categoryToRemove);
            }

            foreach (var updatedFilter in updatedFilters)
            {
                var type = updatedFilter.Title;
                if (type == "Бренды") continue;

                if (currentAttributes.ContainsKey(type))
                {
                    var currentItems = currentAttributes[type];

                    foreach (var updatedOption in updatedFilter.Options)
                    {
                        var newItem = new FilterAtributeAndBrandDTO
                        {
                            Type = type,
                            Name = updatedOption.Value,
                            NameTranslate = updatedOption.Label
                        };
                        currentItems.Add(newItem);

                        var newItemDto = new CreateAttributeDTO
                        {
                            Type = type,
                            Name = updatedOption.Value,
                            NameTranslate = updatedOption.Label,
                            CategoryName = categoryName,
                        };

                        _attributeService.CreateAttribute(newItemDto);                        
                    }

                    var updatedNames = updatedFilter.Options.Select(x => x.Value).ToHashSet();
                    var itemsToRemove = currentItems.Where(x => !updatedNames.Contains(x.Name)).ToList();
                    foreach (var itemToRemove in itemsToRemove)
                    {
                        currentItems.Remove(itemToRemove);
                        _attributeService.DeleteByName(itemToRemove.Name);
                    }
                }
                else
                {
                    var newItems = updatedFilter.Options.Select(updatedOption => new FilterAtributeAndBrandDTO
                    {
                        Type = type,
                        Name = updatedOption.Value,
                        NameTranslate = updatedOption.Label
                    }).ToList();

                    currentAttributes[type] = newItems;

                    foreach (var newItem in newItems)
                    {
                        var newItemDto = new CreateAttributeDTO
                        {
                            Type = type,
                            Name = newItem.Name,
                            NameTranslate = newItem.NameTranslate,
                            CategoryName = categoryName
                        };
                        _attributeService.CreateAttribute(newItemDto);
                    }
                }
            }
        }

        private void UpdateBrands(Dictionary<string, List<FilterAtributeAndBrandDTO>> currentBrands, List<FilterDTO> updatedFilters, string categoryName)
        {
            var updatedCategories = updatedFilters.Select(filter => filter.Title).ToHashSet(StringComparer.OrdinalIgnoreCase);

            var categoriesToRemove = currentBrands.Keys
                .Where(currentCategory => !updatedCategories.Contains(currentCategory))
                .ToList();

            foreach (var categoryToRemove in categoriesToRemove)
            {
                var itemsToRemove = currentBrands[categoryToRemove];

                foreach (var item in itemsToRemove)
                {
                    _brandService.DeleteByName(item.Name);
                }

                currentBrands.Remove(categoryToRemove);
            }

            foreach (var updatedFilter in updatedFilters)
            {
                var type = updatedFilter.Title;
                if (type != "Бренды") continue;


                if (currentBrands.ContainsKey(type))
                {
                    var currentItems = currentBrands[type];

                    foreach (var updatedOption in updatedFilter.Options)
                    {
                        var newItem = new FilterAtributeAndBrandDTO
                        {
                            Type = type,
                            Name = updatedOption.Value,
                            NameTranslate = updatedOption.Label
                        };
                        currentItems.Add(newItem);

                        var newItemDto = new CreateBrandDTO
                        {
                            Type = type,
                            Name = updatedOption.Value,
                            NameTranslate = updatedOption.Label,
                            CategoryName = categoryName 
                        };

                        _brandService.CreateBrand(newItemDto);                
                    }

                    var updatedNames = updatedFilter.Options.Select(x => x.Value).ToHashSet();
                    var itemsToRemove = currentItems.Where(x => !updatedNames.Contains(x.Name)).ToList();
                    foreach (var itemToRemove in itemsToRemove)
                    {
                        currentItems.Remove(itemToRemove);
                        _brandService.DeleteByName(itemToRemove.Name);
                    }
                }
                else
                {
                    var newItems = updatedFilter.Options.Select(updatedOption => new FilterAtributeAndBrandDTO
                    {
                        Type = type,
                        Name = updatedOption.Value,
                        NameTranslate = updatedOption.Label
                    }).ToList();

                    currentBrands[type] = newItems;

                    foreach (var newItem in newItems)
                    {
                        var newItemDto = new CreateBrandDTO
                        {
                            Type = type,
                            Name = newItem.Name,
                            NameTranslate = newItem.NameTranslate,
                            CategoryName = categoryName
                        };

                        _brandService.CreateBrand(newItemDto);
                    }
                }
            }
        }
    }

}
