using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute
{
    public class CreateAttributeDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string TypeTranslate { get; set; }

        public string CategoryName { get; set; }
    }
}
