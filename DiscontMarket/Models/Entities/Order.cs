using DiscontMarket.Domain.Models.Abstractions.BaseEntities;
using DiscontMarket.Domain.Models.Enums;


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

        //Связь с сущностью Product
        public uint? ProductID { get; set; }
        public Product? Product { get; set; }
    }
}
