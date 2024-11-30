using DiscontMarket.ApiModels.DTO.BaseDTOs;


namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Product
{
    public class UpdateSessionDTO : BaseDTO
    {
        public DateTime? InitialTime { get; set; }
        public DateTime? EndTime { get; set; }

    }

}