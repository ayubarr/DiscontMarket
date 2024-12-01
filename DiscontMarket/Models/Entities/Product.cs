using DiscontMarket.Domain.Models.Abstractions.BaseEntities;
using DiscontMarket.Domain.Models.Abstractions.LinkEntities;
using DiscontMarket.Domain.Models.Enums;

namespace DiscontMarket.Domain.Models.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public uint Quantity { get; set; }
        public string IconPath { get; set; }
        public int Rating { get; set; }

        public string Description { get; set; }
        public string FullDescription { get; set; }

        public Availability Availability { get; set; }
        public ProductStatus Status { get; set; }

        public List<Order>? Orders { get; set; }


        public List<ProductAttribute>? ProductAttributes { get; set; }
        public List<ProductTag>? ProductTags { get; set; }
        public List<Image>? Images { get; set; }


        public uint? CategoryID { get; set; }
        public Category? Category { get; set; }
        public uint? BrandID { get; set; }
        public Brand? Brand { get; set; }
        public uint? UserID { get; set; }
        public User? User { get; set; }
    }
}
