using System.Collections.Generic; 
namespace productdetails{ 

    public class Properties
    {
        public List<Key> key { get; set; }
        public List<All> all { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public string nameDescription { get; set; }
        public object valueDescription { get; set; }
        public int priority { get; set; }
        public string measure { get; set; }
        public object iconPath { get; set; }
    }

}