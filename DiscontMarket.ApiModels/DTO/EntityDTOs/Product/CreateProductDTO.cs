using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Product
{
    public class CreateProductDTO : BaseDTO
    {
        public string ProductName { get; set; }
        public decimal Cost { get; set; }
        public bool AvailStatus { get; set; }


        //Пользователь создавший продукт
        public uint? UserID { get; set; }

    }
}
