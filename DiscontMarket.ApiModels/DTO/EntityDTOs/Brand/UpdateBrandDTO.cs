using DiscontMarket.ApiModels.DTO.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Brand
{
    public class UpdateBrandDTO : BaseDTO
    {
        public string? Name { get; set; }

        public string? Type { get; set; }

    }
}
