using System.Collections.Generic; 
namespace ProductsList{ 

    public class PropertiesPortion
    {
        public int id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public string nameDescription { get; set; }
        public object valueDescription { get; set; }
        public int priority { get; set; }
        public string measure { get; set; }
    }

}