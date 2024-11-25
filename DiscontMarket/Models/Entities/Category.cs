using DiscontMarket.Domain.Models.Abstractions.BaseEntities;
using DiscontMarket.Domain.Models.Abstractions.LinkEntities;

namespace DiscontMarket.Domain.Models.Entities
{
    
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        
        public List<Product>? Products { get; set; }
        public List<BrandCategory>? BrandCategories { get; set; }
        public List<AttributeCategory>? AttributeCategories { get; set; }
    }
}