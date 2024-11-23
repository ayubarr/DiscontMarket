using DiscontMarket.ApiModels.DTO.EntityDTOs.Product;
using DiscontMarket.ApiModels.Responce.Helpers;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Domain.Models.Enums;
using DiscontMarket.Services.Helpers.Filter;
using DiscontMarket.Services.Services.Interfaces;
using DiscontMarket.Validation;

namespace DiscontMarket.Services.Services.Implementations
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository repository) : base(repository)
        {
            _productRepository = repository;
        }

        public IBaseResponse<IEnumerable<Product>> GetAllProducts(FilterProductDTO filterProductDTO, SortTypes? sortType)
        {
            try
            {
                Product p = _productRepository.GetById(4);
                string pc = p.ProductAvailability.ToString().ToLower();

                var filter = FilterHelper.CreateProductFilter(filterProductDTO);

                var entities = _productRepository.GetFilteredProductsAsync(filter);
                ObjectValidator<IEnumerable<Product>>.CheckIsNotNullObject(entities);

                entities = SortHelper.SortProducts(entities, sortType);

                return ResponseFactory<IEnumerable<Product>>.CreateSuccessResponse(entities);
            }
            catch (Exception ex)
            {
                return ResponseFactory<IEnumerable<Product>>.CreateErrorResponse(ex);

            }

        }
    }
}
