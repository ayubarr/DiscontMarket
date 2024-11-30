using DiscontMarket.ApiModels.DTO.BaseDTOs;



namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Product
{
    public class SessionDTO : BaseDTO
    {
        public uint id { get; set; }
        public DateTime InitialTime { get; set; }
        public DateTime EndTime { get; set; }

    }

}