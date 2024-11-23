using DiscontMarket.ApiModels.DTO.BaseDTOs;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Category;
using DiscontMarket.Domain.Models.Enums;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Product
{
    public class FilterProductDTO : BaseDTO
    {
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public Availability? ProductAvailability { get; set; }
        public ProductStatus? ProductStatus { get; set; }
        public List<CategoryDTO>? categoryDTOs { get; set; }

    }
}
