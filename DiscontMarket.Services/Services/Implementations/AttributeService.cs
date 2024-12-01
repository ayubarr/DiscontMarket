using DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute;
using DiscontMarket.ApiModels.Responce.Helpers;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.Domain.Models.Abstractions.LinkEntities;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Services.Helpers.Mapping;
using DiscontMarket.Services.Services.Interfaces;
using DiscontMarket.Validation;
using System;

namespace DiscontMarket.Services.Services.Implementations
{
    public class AttributeService : BaseService<AttributeEntity>, IAttributeService
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly IBaseRepository<Category> _categoryRepository;


        public AttributeService(IAttributeRepository attributeRepository, IBaseRepository<Category> categoryRepository) : base(attributeRepository)
        {
            _attributeRepository = attributeRepository;
            _categoryRepository = categoryRepository;
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

                await _attributeRepository.Delete(attribute);
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
    }
}
