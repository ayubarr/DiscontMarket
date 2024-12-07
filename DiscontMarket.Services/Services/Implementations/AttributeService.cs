using DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute;
using DiscontMarket.ApiModels.Responce.Helpers;
using DiscontMarket.ApiModels.Responce.Implementations;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.Domain.Models.Abstractions.LinkEntities;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Services.Services.Interfaces;
using DiscontMarket.Validation;

namespace DiscontMarket.Services.Services.Implementations
{
    public class AttributeService : BaseService<AttributeEntity>, IAttributeService
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IBrandRepository _brandRepository;


        public AttributeService(IAttributeRepository attributeRepository, IBaseRepository<Category> categoryRepository, IBrandRepository brandRepository) : base(attributeRepository)
        {
            _attributeRepository = attributeRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
        }

        public IBaseResponse<AttributeEntity> CreateAttribute(CreateAttributeDTO entityDTO)
        {
            try
            {
                ObjectValidator<CreateAttributeDTO>.CheckIsNotNullObject(entityDTO);

                var categories = _categoryRepository.GetAll().Where(c => c.Name.Equals(entityDTO.CategoryName));

                var entity = new AttributeEntity 
                { 
                    Name =  entityDTO.Name,
                    Type = entityDTO.Type,
                    NameTranslate = entityDTO.NameTranslate,
                    AttributeCategories = categories.Select(category => new AttributeCategory
                    {
                        Category = category // Связываем существующую категорию

                    }).ToList()
                };

                _attributeRepository.Create(entity);

                return ResponseFactory<AttributeEntity>.CreateSuccessResponse(entity);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<AttributeEntity>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<AttributeEntity>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<bool>> DeleteByNameAsync(string attributeName)
        {
            try
            {
                StringValidator.CheckIsNotNull(attributeName);

                var attribute = _attributeRepository.GetAll().Where(a => a.Name.Equals(attributeName)).FirstOrDefault();
                if (attribute is null)
                {
                    throw new Exception($"not found atribute: {attributeName}");
                }

                await _attributeRepository.DeleteAsync(attribute);
                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (Exception ex)
            {
                return ResponseFactory<bool>.CreateErrorResponse(ex);
            }
        }

 

        public IBaseResponse<IEnumerable<string>> GetAllAttributesByType(string attributeType)
        {
            try
            {
                var atributes = _attributeRepository.GetAttributeNamesByType(attributeType);

                if (atributes is null)
                {
                    throw new Exception($"not found atribute type: {attributeType}");
                }


                ObjectValidator<IEnumerable<string>>.CheckIsNotNullObject(atributes);

                return ResponseFactory<IEnumerable<string>>.CreateSuccessResponse(atributes);
            }
            catch (Exception ex)
            {
                return ResponseFactory<IEnumerable<string>>.CreateErrorResponse(ex);

            }
        }

        public IBaseResponse<IEnumerable<AttributeEntity>> GetAllByCategoryName(string categoryName)
        {
            try
            {
                var category = GetCategory(categoryName);


                if (category == null)
                {
                    throw new Exception($"not found category: {categoryName}");
                }

                var atributes = _attributeRepository.GetAll()
                    .Where(a => a.AttributeCategories
                    .Any(c => c.CategoryID == category.ID))
                    .AsEnumerable();

                ObjectValidator<IEnumerable<AttributeEntity>>.CheckIsNotNullObject(atributes);

                return ResponseFactory<IEnumerable<AttributeEntity>>.CreateSuccessResponse(atributes);
            }
            catch (Exception ex)
            {
                return ResponseFactory<IEnumerable<AttributeEntity>>.CreateErrorResponse(ex);

            }
        }

        public IBaseResponse<IEnumerable<string>> GetAllAttributeTypesByCategoryName(string categoryName)
        {
            try
            {
                var category = GetCategory(categoryName);

                if (category == null)
                {
                    throw new Exception($"not found category: {categoryName}");
                }

                var atributes = _attributeRepository.GetAttributeTypesByCategory(category);
                    
                ObjectValidator<IEnumerable<string>>.CheckIsNotNullObject(atributes);

                return ResponseFactory<IEnumerable<string>>.CreateSuccessResponse(atributes);
            }
            catch (Exception ex)
            {
                return ResponseFactory<IEnumerable<string>>.CreateErrorResponse(ex);

            }
        }

        private Category GetCategory(string categoryName)
        {
            return _categoryRepository
                    .GetAll()
                    .Where(c => c.Name.Equals(categoryName))
                    .FirstOrDefault();
        }

        public IBaseResponse<CategoryFilters> GetAllNames(string categoryName)
        {
            try
            {
                // Получаем категорию по имени
                var category = GetCategory(categoryName);

                if (category == null)
                {
                    if(categoryName == "discount" || categoryName == "damagedpackage" || categoryName == "minordefects")
                    {
                        return new BaseResponse<CategoryFilters>
                        {
                            IsSuccess = true,
                            Data = new CategoryFilters()
                            {
                                Brands = null,
                                Attributes = null
                            },
                            StatusCode = 200,
                        };
                    }

                    throw new Exception($"Category not found: {categoryName}");
                }

                // Получаем типы атрибутов для категории
                var attributeTypes = _attributeRepository.GetAttributeTypesByCategory(category);

                if (attributeTypes == null || !attributeTypes.Any())
                {
                    throw new Exception($"No attribute types found for category: {categoryName}");
                }

                // Собираем все атрибуты
                var allAttributes = new List<string>();


                var brands = _brandRepository.GetAll().Where(b => b.Type.Equals("Бренды")).Select(x => x.Name).ToList();

                if (brands == null || !brands.Any())
                {
                    throw new Exception($"No attributes found for type: {"Бренды"}");
                }

                foreach (var attributeType in attributeTypes)
                {
                    var attributes = _attributeRepository.GetAttributeNamesByType(attributeType);

                    if (attributes == null || !attributes.Any())
                    {
                        throw new Exception($"No attributes found for type: {attributeType}");
                    }

                    // Добавляем атрибуты в общий список
                    allAttributes.AddRange(attributes);
                }

                var attributeAndBrands = new CategoryFilters()
                {
                    Brands = brands,
                    Attributes = allAttributes
                };

                // Проверяем, что результат не пуст
                ObjectValidator<CategoryFilters>.CheckIsNotNullObject(attributeAndBrands);

                // Возвращаем успешный ответ
                return ResponseFactory<CategoryFilters>.CreateSuccessResponse(attributeAndBrands);
            }
            catch (Exception ex)
            {
                // Возвращаем ошибку
                return ResponseFactory<CategoryFilters>.CreateErrorResponse(ex);
            }
        }
    }
}
