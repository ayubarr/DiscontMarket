using DiscontMarket.ApiModels.DTO.EntityDTOs.Product;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Domain.Models.Enums;

namespace DiscontMarket.Services.Services.Interfaces
{
    public interface IProductService : IBaseService<Product>
    {
        IBaseResponse<IEnumerable<ProductDTO>> GetAllProducts(FilterProductDTO filterProductDTO, SortTypes? sortProductDTO);
        IBaseResponse<GetProductDTO> GetProductById(uint Id);

        IBaseResponse<GetProductDTO> GetProductByName(string name);

        IBaseResponse<Product> CreateProduct(CreateProductDTO entityDTO);

        IBaseResponse<bool> DeleteByProductName(string productName);

    }
}
