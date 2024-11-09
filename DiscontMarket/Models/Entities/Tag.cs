using DiscontMarket.Domain.Models.Abstractions.LinkEntities;

namespace DiscontMarket.Domain.Models.Entities
{
    public class Tag
    {
        public string TagName { get; set; }

        public List<ProductTag>? ProductTags { get; set; }
    }
}