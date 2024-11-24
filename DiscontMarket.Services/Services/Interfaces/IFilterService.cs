using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.Services.Services.Interfaces
{
    public interface IFilterService
    {
        Dictionary<string, CategoryFiltersDTO> GetFilters();
    }
}
