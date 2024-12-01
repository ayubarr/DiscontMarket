using DiscontMarket.ApiModels.DTO.EntityDTOs.Brand;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.Domain.Models.Entities;

namespace DiscontMarket.Services.Services.Interfaces
{
    public interface IBrandService : IBaseService<Brand>
    {
        IBaseResponse<IEnumerable<Brand>> GetAllByCategoryName(string categoryName);
        IBaseResponse<IEnumerable<string>> GetAllBrandNames(string brandType);
        IBaseResponse<IEnumerable<string>> GetAllBrandTypesByCategoryName(string categoryName);
        IBaseResponse<Brand> CreateBrand(CreateBrandDTO entityDTO);

        Task<IBaseResponse<bool>> DeleteByNameAsync(string brandName);
    }
}
