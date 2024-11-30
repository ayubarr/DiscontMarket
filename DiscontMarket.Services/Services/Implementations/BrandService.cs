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
        private readonly IBrandRepository _brandRepository;
        private readonly IBaseRepository<Category> _categoryRepository;


        public BrandService(IBrandRepository repository,
            IBaseRepository<Category> categoryRepository) : base(repository)
        {
            _brandRepository = repository;
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
                    Name = Brand.Name,
                    Type = Brand.Type,
                    NameTranslate = Brand.NameTranslate,
                    BrandCategories = categories.Select(category => new BrandCategory
                    {
                        Category = category // Связываем существующую категорию

                    }).ToList()
                };

                _brandRepository.Create(entity);

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

        public async Task<IBaseResponse<bool>> DeleteByNameAsync(string brandName)
        {
            try
            {
                StringValidator.CheckIsNotNull(brandName);

                var attribute = _brandRepository.GetAll().Where(a => a.Name.Equals(brandName)).FirstOrDefault();
                if (attribute is null)
                {
                    throw new Exception($"not found brand: {brandName}");
                }

                await _brandRepository.Delete(attribute);
                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (Exception ex)
            {
                return ResponseFactory<bool>.CreateErrorResponse(ex);
            }
        }

        public IBaseResponse<IEnumerable<Brand>> GetAllByBrandsName(string brandName)
        {
            try
            {
                var brands = _brandRepository
                    .GetAll()
                    .Where(a => a.Name.Equals(brandName))
                    .AsEnumerable();

                if (brands is null)
                {
                    throw new Exception($"not found brand: {brandName}");
                }

                ObjectValidator<IEnumerable<Brand>>.CheckIsNotNullObject(brands);

                return ResponseFactory<IEnumerable<Brand>>.CreateSuccessResponse(brands);
            }
            catch (Exception ex)
            {
                return ResponseFactory<IEnumerable<Brand>>.CreateErrorResponse(ex);

            }
        }

        public IBaseResponse<IEnumerable<Brand>> GetAllByCategoryName(string categoryName)
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

                var brands = _brandRepository.GetAll()
                    .Where(a => a.BrandCategories
                    .Any(c => c.CategoryID == category.ID))
                    .AsEnumerable();

                ObjectValidator<IEnumerable<Brand>>.CheckIsNotNullObject(brands);

                return ResponseFactory<IEnumerable<Brand>>.CreateSuccessResponse(brands);
            }
            catch (Exception ex)
            {
                return ResponseFactory<IEnumerable<Brand>>.CreateErrorResponse(ex);

            }
        }
    }
}
