﻿using DiscontMarket.ApiModels.DTO.BaseDTOs;



namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Product
{
    public class CreateSessionDTO : BaseDTO
    {
        public DateTime InitialTime { get; set; }
        public DateTime EndTime { get; set; }

    }

}