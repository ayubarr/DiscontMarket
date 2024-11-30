using DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Brand;
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
    public class BrandService : BaseService<Brand>, IBrandService
    {
        private readonly IBaseRepository<AttributeEntity> _attributeRepository;
        private readonly IBaseRepository<Category> _categoryRepository;


        public BrandService(IBaseRepository<Brand> repository, IBaseRepository<Category> categoryRepository) : base(repository)
        {
            _attributeRepository = repository;
            _categoryRepository = categoryRepository;
        }

        public IBaseResponse<Brand> CreateBrand(CreateBtandDTO entityDTO)
        {
            try
            {
                ObjectValidator<CreateBtandDTO>.CheckIsNotNullObject(entityDTO);

                var categories = _categoryRepository.GetAll().Where(c => c.Name.Equals(entityDTO.CategoryName));

                var entity = new Brand
                {
                    Name = entityDTO.BrandName,
                    Type = entityDTO.Type,
                    NameTranslate = entityDTO.TypeTranslate,
                    AttributeCategories = categories.Select(category => new AttributeCategory
                    {
                        Category = category // Связываем существующую категорию

                    }).ToList()
                };

                _attributeRepository.Create(entity);

                return ResponseFactory<Brand>.CreateSuccessResponse(entity);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<Brand>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<Brand>.CreateErrorResponse(exception);
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

        public IBaseResponse<IEnumerable<AttributeEntity>> GetAllByAttributesName(string attributeName)
        {
            try
            {
                var atributes = _attributeRepository
                    .GetAll()
                    .Where(a => a.Name.Equals(attributeName))
                    .AsEnumerable();

                if (atributes is null)
                {
                    throw new Exception($"not found atribute: {attributeName}");
                }

                ObjectValidator<IEnumerable<AttributeEntity>>.CheckIsNotNullObject(atributes);

                return ResponseFactory<IEnumerable<AttributeEntity>>.CreateSuccessResponse(atributes);
            }
            catch (Exception ex)
            {
                return ResponseFactory<IEnumerable<AttributeEntity>>.CreateErrorResponse(ex);

            }
        }

        public IBaseResponse<IEnumerable<AttributeEntity>> GetAllByCategoryName(string categoryName)
        {
            try
            {
                var category = _categoryRepository
                    .GetAll()
                    .Where(c => c.Name.Equals(categoryName))
                    .FirstOrDefault();

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
    }
}
