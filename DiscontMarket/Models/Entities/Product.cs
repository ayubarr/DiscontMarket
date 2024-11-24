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
        public string ImagePath { get; set; }
        public Availability Availability { get; set; }
        public ProductStatus Status { get; set; }


        public List<Order>? Orders { get; set; }
        public List<Category>? Categories { get; set; }            
        public List<ProductAttribute>? ProductAttributes { get; set; }  
        public List<ProductTag>? ProductTags { get; set; }

        public uint? BrandId { get; set; }
        public Brand? Brand { get; set; }
        public uint? UserID { get; set; }
        public User? User { get; set; }
    }
}
