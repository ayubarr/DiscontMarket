using DiscontMarket.Domain.Models.Abstractions.BaseEntities;

namespace DiscontMarket.Domain.Models.Entities
{
    public class Brend : BaseEntity

    {
        public string BrendName { get; set; }
        public string Description { get; set; }
        
        public uint? ProductID { get; set; }
        public Product? Product { get; set; }
        
    }
}