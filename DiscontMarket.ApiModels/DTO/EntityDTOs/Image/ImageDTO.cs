using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute
{
    public class ImageDTO : BaseDTO
    {
        public uint id { get; set; }
        public string Path { get; set; }
    }
}
