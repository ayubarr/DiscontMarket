using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Category
{
    public class GetCategoryDTO : BaseDTO
    {
        public List<string> Categories { get; set; }
    }
}
