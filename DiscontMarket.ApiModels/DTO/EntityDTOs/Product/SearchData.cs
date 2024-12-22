using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Product
{
    public class SearchData : BaseDTO
    {
        public string categoryname { get; set; }
        public string title { get; set; }
    }
}
