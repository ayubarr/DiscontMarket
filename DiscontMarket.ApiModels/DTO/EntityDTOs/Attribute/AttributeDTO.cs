﻿using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute
{
    public class AttributeDTO : BaseDTO
    {
        public uint id { get; set; }
        public string Name { get; set; }
    }
}
