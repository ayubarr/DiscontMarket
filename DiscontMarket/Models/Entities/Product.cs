using DiscontMarket.Domain.Models.Abstractions.BaseEntities;
using DiscontMarket.Domain.Models.Abstractions.LinkEntities;

namespace DiscontMarket.Domain.Models.Entities
{
    public class Product : BaseEntity
    {
        public  string ProductName { get; set; }
        public decimal Cost { get; set; }
        public bool AvailStatus { get; set; }


        public List<Order>? Orders { get; set; }

        public List<Category>? Categories { get; set; }
        
        public List<Brend>? Brends { get; set; }
        
        public List<ProductAttribute>? ProductAttributes { get; set; }
        
        public List<ProductTag>? ProductTags { get; set; }
        

        
        public uint? UserID { get; set; }
        public User? User { get; set; }
    }
}
