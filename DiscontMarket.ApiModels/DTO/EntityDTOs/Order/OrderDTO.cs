using DiscontMarket.ApiModels.DTO.BaseDTOs;
using System.ComponentModel.DataAnnotations;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Product
{
    public class OrderDTO : BaseDTO
    {
        public uint id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Condition { get; set; }
        public decimal TotalCost { get; set; }

    }

}
