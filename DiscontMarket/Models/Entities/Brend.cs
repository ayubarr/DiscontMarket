using DiscontMarket.Domain.Models.Abstractions.BaseEntities;

namespace DiscontMarket.Domain.Models.Entities
{
    public class Brend : BaseEntity

    {
        public string Name { get; set; }
        public string Type { get; set; }
        
        public uint? ProductID { get; set; }
        public Product? Product { get; set; }
        
    }
}