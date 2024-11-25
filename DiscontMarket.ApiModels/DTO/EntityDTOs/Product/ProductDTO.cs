using DiscontMarket.ApiModels.DTO.BaseDTOs;
using DiscontMarket.Domain.Models.Enums;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Product
{
    public class ProductDTO : BaseDTO
    {
        public uint id { get; set; }
        public string productName { get; set; }
        public decimal price { get; set; }
        public string productAvailability { get; set; }
        public string productStatus { get; set; }
        public string image { get; set; }
        public uint quantity { get; set; }


        //Пользователь работавший с продуктом в последний раз
        public uint? userid { get; set; }
        public uint? categoryid { get; set; }
        public uint? brendId { get; set; }

    }

}
