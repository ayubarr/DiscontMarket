using System.Collections.Generic; 
namespace ProductPrices{ 

    public class BonusRubles
    {
        public string productId { get; set; }
        public int total { get; set; }
        public List<Breakdown> breakdown { get; set; }
    }

}