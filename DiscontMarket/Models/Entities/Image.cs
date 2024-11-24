using DiscontMarket.Domain.Models.Abstractions.BaseEntities;

namespace DiscontMarket.Domain.Models.Entities
{
    public class Image : BaseEntity

    {
        public string PathFoto { get; set; }

        public uint? ProductID { get; set; }
        public Product? Product { get; set; }

    }
}
