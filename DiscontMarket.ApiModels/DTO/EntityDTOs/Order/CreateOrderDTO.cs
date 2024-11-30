using DiscontMarket.ApiModels.DTO.BaseDTOs;
using DiscontMarket.Domain.Models.Enums;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Product
{
    public class CreateOrderDTO : BaseDTO
    {
        public uint id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Condition { get; set; }
        public decimal TotalCost { get; set; }

    }

}

