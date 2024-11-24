using DiscontMarket.Domain.Models.Entities;

namespace DiscontMarket.Domain.Models.Abstractions.LinkEntities
{
    public class AttributeCategory
    {
        public uint? CategoryID { get; set; }
        public Category? Category { get; set; }

        public uint? AttributeID { get; set; }
        public AttributeEntity? Attribute { get; set; }
    }
}
