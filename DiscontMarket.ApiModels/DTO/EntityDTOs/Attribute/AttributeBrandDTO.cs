using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute
{
    public class AttributeBrandDTO : BaseDTO
    {
        public List<string> Brands { get; set; }
        public List<string> Attributes { get; set; }
    }
}
