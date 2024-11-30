using DiscontMarket.ApiModels.DTO.EntityDTOs.Brand;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.Domain.Models.Entities;

namespace DiscontMarket.Services.Services.Interfaces
{
    public interface IBrandService : IBaseService<Brand>
    {
        IBaseResponse<IEnumerable<Brand>> GetAllByCategoryName(string categoryName);
        IBaseResponse<IEnumerable<Brand>> GetAllByBrandsName(string brandName);

        IBaseResponse<Brand> CreateBrand(CreateBtandDTO entityDTO);

        Task<IBaseResponse<bool>> DeleteByNameAsync(string brandName);
    }
}
