using DiscontMarket.ApiModels.DTO.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute
{
    public class FilterAttributeDTO : BaseDTO
    {
        public uint ID { get; set; }
        public string Type { get; set; }
    }
}
