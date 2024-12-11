using DiscontMarket.ApiModels.DTO.BaseDTOs;
using DiscontMarket.ApiModels.Responce.Interfaces;

namespace DiscontMarket.Services.Services.Interfaces
{
    public interface IFilterService
    {
        Dictionary<string, FilterCategoryDTO> GetFilters();
        IBaseResponse<bool> SetFilters(Dictionary<string, FilterCategoryDTO> updatedFilters);

        IBaseResponse<int> GetMinPriceByCategory(string category);
        IBaseResponse<int> GetMaxPriceByCategory(string category);


    }
}
