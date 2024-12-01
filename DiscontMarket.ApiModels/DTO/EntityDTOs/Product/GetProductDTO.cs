using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Product
{
    public class GetProductDTO : BaseDTO
    {
        public uint productId { get; set; }
        public string title { get; set; }
        public double rating { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public Dictionary<string, string> characteristics { get; set; }
        public List<string> images { get; set; }
        public string fullDescription { get; set; }
    }
}
