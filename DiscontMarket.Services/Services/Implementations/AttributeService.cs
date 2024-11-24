using DiscontMarket.ApiModels.Responce.Helpers;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Services.Services.Interfaces;
using DiscontMarket.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscontMarket.Services.Services.Implementations
{
    public class AttributeService : BaseService<AttributeEntity>, IAttributeService
    {
        private readonly IBaseRepository<AttributeEntity> _attributeRepository;
        private readonly IBaseRepository<Category> _categoryRepository;


        public AttributeService(IBaseRepository<AttributeEntity> repository, IBaseRepository<Category> categoryRepository) : base(repository)
        {
            _attributeRepository = repository;
            _categoryRepository = categoryRepository;
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
                    throw new Exception( $"not found category: {categoryName}");
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
