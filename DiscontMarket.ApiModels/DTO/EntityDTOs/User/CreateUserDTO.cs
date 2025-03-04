﻿using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.User
{
    public class CreateUserDTO : BaseDTO
    {
        public string UserName { get; set; }

        public string Email { get; set; }
        public string ClientsVk { get; set; }
        public string ClientsTelegram { get; set; }
        public string ClientsWhatsapp { get; set; }
        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime? HireDate { get; set; }
    }
}
