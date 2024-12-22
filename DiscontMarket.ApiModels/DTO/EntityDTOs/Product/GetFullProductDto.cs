using DiscontMarket.ApiModels.DTO.BaseDTOs;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Product
{
    public class GetFullProductDto : BaseDTO
    {
        public int productId { get; set; }
        public string title { get; set; }
        public double rating { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public uint quantity { get; set; }

        public List<CharacteristicDTO> characteristics { get; set; }
        public List<string> images { get; set; }
        public string fullDescription { get; set; }

    }
}
