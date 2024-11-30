using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Brand
{
    public class CreateBtandDTO : BaseDTO
    {
        public string BrandName { get; set; }

        public string CategoryName { get; set; }
    }
}
