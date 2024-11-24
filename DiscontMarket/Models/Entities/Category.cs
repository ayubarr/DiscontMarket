using DiscontMarket.Domain.Models.Abstractions.BaseEntities;

namespace DiscontMarket.Domain.Models.Entities
{
    
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public uint? UserID { get; set; }
        public User? User { get; set; }
        
        public uint? ProductID { get; set; }
        public Product? Product { get; set; }
    }
}