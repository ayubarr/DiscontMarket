using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute
{
    public class UpdateAttributeDTO : BaseDTO
    {
        public string? Name { get; set; }

        public string? Type { get; set; }
    }
}
