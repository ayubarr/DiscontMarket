﻿using DiscontMarket.ApiModels.DTO.EntityDTOs.Product;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Domain.Models.Enums;

namespace DiscontMarket.Services.Services.Interfaces
{
    public interface IProductService : IBaseService<Product>
    {
        IBaseResponse<IEnumerable<ProductDTO>> GetAllProducts(FilterProductDTO filterProductDTO, string? sortProductDTO);
        IBaseResponse<IEnumerable<ProductDTO>> GetAllProductsNews();
        IBaseResponse<IEnumerable<ProductDTO>> GetAllProductsHits();

        IBaseResponse<GetProductDTO> GetProductById(uint Id);

        IBaseResponse<IEnumerable<GetProductDTO>> GetProductsByName(string name);

        IBaseResponse<bool> CreateProduct(CreateProductDTO entityDTO);

        IBaseResponse<bool> DeleteByProductName(string productName);

    }
}
