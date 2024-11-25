using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute
{
    public class FilterAttributeDTO : BaseDTO
    {
        public uint ID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string NameTranslate { get; set; }
    }
}
