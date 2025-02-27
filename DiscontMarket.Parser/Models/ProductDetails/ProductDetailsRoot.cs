using System.Collections.Generic; 
namespace productdetails{ 

    public class ProductDetailsRoot
    {
        public bool success { get; set; }
        public List<object> messages { get; set; }
        public Body body { get; set; }
    }

}