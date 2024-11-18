using DiscontMarket.Domain.Models.Abstractions.BaseEntities;
using DiscontMarket.Domain.Models.Abstractions.LinkEntities;

namespace DiscontMarket.Domain.Models.Entities
{
    public class Tag : BaseEntity
    {
        public string TagName { get; set; }

        public List<ProductTag>? ProductTags { get; set; }
    }
}