using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Brand
{
    public class CreateBrandDTO : BaseDTO
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string NameTranslate { get; set; }


        public string CategoryName { get; set; }
    }
}
