using DiscontMarket.Domain.Models.Abstractions.BaseEntities;

namespace DiscontMarket.Domain.Models.Entities
{
    public class Product : BaseEntity
    {
        public  string ProductName { get; set; }
        public decimal Cost { get; set; }
        public bool AvailStatus { get; set; }


        public List<Order>? Orders { get; set; }

        public uint? UserID { get; set; }
        public User? User { get; set; }
    }
}
