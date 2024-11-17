using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Product
{
    public class ProductDTO : BaseDTO
    {
        public uint ID { get; set; }
        public string ProductName { get; set; }
        public decimal Cost { get; set; }
        public bool AvailStatus { get; set; }

        //Пользователь работавший с продуктом в последний раз
        public uint UserID { get; set; }
    }

}
