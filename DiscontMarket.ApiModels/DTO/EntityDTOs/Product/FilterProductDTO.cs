using DiscontMarket.ApiModels.DTO.BaseDTOs;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Brend;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Category;
using DiscontMarket.Domain.Models.Enums;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Product
{
    public class FilterProductDTO : BaseDTO
    {
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public List<string>? Availability { get; set; }
        public List<string>? Status { get; set; }
        public CategoryDTO? CategoryDTO { get; set; }
        public List<string>? Attributes { get; set; }
        public List<string>? Brands { get; set; }


        public SortTypes? SortOrder { get; set; }
    }
}
