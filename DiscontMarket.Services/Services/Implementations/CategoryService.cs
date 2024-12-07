using DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Category;
using DiscontMarket.ApiModels.Responce.Helpers;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.DAL.Repository.Implementations;
using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Services.Services.Interfaces;
using DiscontMarket.Validation;

namespace DiscontMarket.Services.Services.Implementations
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;


        public CategoryService(ICategoryRepository categoryRepository) : base(categoryRepository)
        {
            _categoryRepository = categoryRepository; 
        }

        public IBaseResponse<bool> CreateCategory(CreateCategoryDTO entityDTO)
        {
            try
            {
                ObjectValidator<CreateCategoryDTO>.CheckIsNotNullObject(entityDTO);

                var entity = new Category
                {
                    Name = entityDTO.Name,
                };

                _categoryRepository.Create(entity);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<bool>> DeleteByNameAsync(string name)
        {
            try
            {
                StringValidator.CheckIsNotNull(name);

                var attribute = _categoryRepository.GetAll().Where(a => a.Name.ToLower().Equals(name.ToLower())).FirstOrDefault();
                if (attribute is null)
                {
                    throw new Exception($"not found atribute: {name}");
                }

                await _categoryRepository.DeleteAsync(attribute);
                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (Exception ex)
            {
                return ResponseFactory<bool>.CreateErrorResponse(ex);
            }
        }

        public IBaseResponse<IEnumerable<string>> GetAllNames()
        {
            try
            {
                var entities = _categoryRepository.GetAllCategoryNames();

                ObjectValidator<IEnumerable<string>>.CheckIsNotNullObject(entities);

                return ResponseFactory<IEnumerable<string>>.CreateSuccessResponse(entities);
            }
            catch (Exception ex)
            {
                return ResponseFactory<IEnumerable<string>>.CreateErrorResponse(ex);

            }
        }
    }
}
