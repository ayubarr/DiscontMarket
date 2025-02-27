using System.Collections.Generic; 
namespace ProductPrices{ 

    public class PricesRoot
    {
        public bool success { get; set; }
        public List<object> messages { get; set; }
        public Body body { get; set; }
    }

}