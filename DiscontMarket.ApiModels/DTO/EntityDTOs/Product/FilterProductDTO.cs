using DiscontMarket.Domain.Models.Enums;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Product
{
    public class FilterProductDTO
    {
        public decimal Price { get; set; } 
        public Availability ProductAvailability { get; set; }
        public ProductStatus ProductStatus { get; set; }

    }
}
