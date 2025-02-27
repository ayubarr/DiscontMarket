using System.Collections.Generic; 
namespace ProductPrices{ 

    public class Price
    {
        public string productId { get; set; }
        public int basePrice { get; set; }
        public int salePrice { get; set; }
        public int basePromoPrice { get; set; }
        public List<Discount> discounts { get; set; }
        public bool applyPersonalPrice { get; set; }
        public bool isFinalPrice { get; set; }
    }

}