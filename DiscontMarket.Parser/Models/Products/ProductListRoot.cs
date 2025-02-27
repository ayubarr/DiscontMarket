using System.Collections.Generic; 
namespace ProductsList{ 

    public class ProductListRoot
    {
        public bool success { get; set; }
        public List<object> messages { get; set; }
        public Body? body { get; set; }
    }

}