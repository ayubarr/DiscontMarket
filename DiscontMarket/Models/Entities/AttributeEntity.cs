using DiscontMarket.Domain.Models.Abstractions.BaseEntities;
using DiscontMarket.Domain.Models.Abstractions.LinkEntities;

namespace DiscontMarket.Domain.Models.Entities
{
    public class AttributeEntity : BaseEntity
    {       
        public string Name { get; set; }
        public string NameTranslate { get; set; }
        public string Type { get; set; }


        public List<ProductAttribute>? ProductAttributes { get; set; }
        public List<AttributeCategory>? AttributeCategories { get; set; }
    }
}