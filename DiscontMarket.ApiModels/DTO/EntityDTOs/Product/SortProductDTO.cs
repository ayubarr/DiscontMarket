using DiscontMarket.Domain.Models.Enums;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Product
{
    public class SortProductDTO
    {
        public SortTypes PopularitySort { get; set; }
        public SortTypes PriceSort { get; set; }
        public SortTypes RatingSort { get; set; }
        public SortTypes TitleSort { get; set; }
        public SortTypes QuantitySort { get; set; }

    }
}
