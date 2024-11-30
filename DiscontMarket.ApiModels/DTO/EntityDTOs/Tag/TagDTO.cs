using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Tag
{
    public class TagDTO : BaseDTO
    {
        public uint id { get; set; }
        public string Name { get; set; }
    }
}
