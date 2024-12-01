using DiscontMarket.ApiModels.DTO.EntityDTOs.Product;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Domain.Models.Enums;

namespace DiscontMarket.Services.Services.Interfaces
{
    public interface IProductService : IBaseService<Product>
    {
        IBaseResponse<IEnumerable<ProductDTO>> GetAllProducts(FilterProductDTO filterProductDTO, SortTypes? sortProductDTO);
        IBaseResponse<GetProductDTO> GetProductByIdAsync(uint Id);
        IBaseResponse<Product> CreateProduct(CreateProductDTO entityDTO);
    }
}
