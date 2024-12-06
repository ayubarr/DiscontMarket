using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Image
{
    public class ImageDTO : BaseDTO
    {
        public string Path { get; set; }
        public string Image { get; set; }
        public string ImageName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
