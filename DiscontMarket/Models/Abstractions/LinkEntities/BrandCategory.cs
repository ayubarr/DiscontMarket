using DiscontMarket.Domain.Models.Entities;

namespace DiscontMarket.Domain.Models.Abstractions.LinkEntities
{
    public class BrandCategory
    {
        public uint? CategoryID { get; set; }
        public Category? Category { get; set; }

        public uint? BrandID { get; set; }
        public Brand? Brand { get; set; }
    }
}
