﻿using DiscontMarket.Domain.Models.Abstractions.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscontMarket.Domain.Models.Entities
{
    public class Session : BaseEntity
    {
        public DateTime InitialTime { get; set; }
        public DateTime EndTime { get; set; }



        //Связь с сущностью Order
        public List<Order> Orders { get; set; }
    }
}