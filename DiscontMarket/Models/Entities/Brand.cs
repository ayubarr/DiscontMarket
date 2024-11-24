using DiscontMarket.Domain.Models.Abstractions.BaseEntities;
using DiscontMarket.Domain.Models.Abstractions.LinkEntities;

namespace DiscontMarket.Domain.Models.Entities
{
    public class Brand : BaseEntity

    {
        public string Name { get; set; }
        
        public uint? ProductID { get; set; }
        public Product? Product { get; set; }

        public List<BrandCategory>? BrandCategories { get; set; }
    }
}