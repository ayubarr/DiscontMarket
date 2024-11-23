using DiscontMarket.ApiModels.DTO.BaseDTOs;
using DiscontMarket.Domain.Models.Enums;


namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Product
{
    public class CreateProductDTO : BaseDTO
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public bool AvailStatus { get; set; }
        public Availability ProductAvailability { get; set; }
        public ProductStatus ProductStatus { get; set; }

        //Пользователь создавший продукт
        public uint? UserID { get; set; }

    }
}
