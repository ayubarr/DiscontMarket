using DiscontMarket.Domain.Models.Abstractions.BaseEntities;
using DiscontMarket.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscontMarket.Domain.Models.Entities
{
    public class Order : BaseEntity
    {
        public DateTime CreationDate { get; set; }
        public Condition Condition { get; set; } = Condition.Pending;
        public decimal TotalCost { get; set; }


        //Связь с сущностью Session
        public uint? SessionID { get; set; }
        public Session? Session { get; set; }
    }
}
