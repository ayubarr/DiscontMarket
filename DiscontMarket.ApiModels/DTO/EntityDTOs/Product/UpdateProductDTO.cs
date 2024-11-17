using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Product
{
    public class UpdateProductDTO : BaseDTO
    {
        public string? ProductName { get; set; }
        public decimal? Cost { get; set; }
        public bool? AvailStatus { get; set; }

        //Пользователь обновивший продукт
        public uint? UserID { get; set; }
    }
}
