using DiscontMarket.ApiModels.DTO.BaseDTOs;


namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Product
{
    public class CreateProductDTO : BaseDTO
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public uint Quantity { get; set; }
        public string ProductAvailability { get; set; }
        public string ProductStatus { get; set; }
        public string ImagePath { get; set; }

        //Пользователь создавший продукт
        public uint? UserID { get; set; }
        public uint? BrendId { get; set; }


    }
}
