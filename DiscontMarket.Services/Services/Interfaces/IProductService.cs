using DiscontMarket.ApiModels.DTO.BaseDTOs;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Product;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Domain.Models.Enums;

namespace DiscontMarket.Services.Services.Interfaces
{
    public interface IProductService : IBaseService<Product>
    {
        IBaseResponse<IEnumerable<Product>> GetAllProducts(FilterProductDTO filterProductDTO, SortTypes? sortProductDTO);
        IBaseResponse<Product> CreateProduct(CreateProductDTO entityDTO);

    }
}
