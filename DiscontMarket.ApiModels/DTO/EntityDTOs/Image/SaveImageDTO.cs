using DiscontMarket.ApiModels.DTO.BaseDTOs;
using Microsoft.AspNetCore.Http;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Image
{
    public class SaveImageDTO : BaseDTO
    {
        public string image {  get; set; }
        public string imageName { get; set; }

    }
}
