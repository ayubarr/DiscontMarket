using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Order
{
    public class OrderRequest : BaseDTO
    {
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string datetime { get; set; }
    }
}
