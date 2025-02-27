using System.Collections.Generic; 
namespace ProductsList{ 

    public class Product
    {
        public string productId { get; set; }
        public string name { get; set; }
        public string nameTranslit { get; set; }
        public string productType { get; set; }
        public object usedProductType { get; set; }
        public string materialCisNumber { get; set; }
        public string materialSource { get; set; }
        public string modelName { get; set; }
        public Rating rating { get; set; }
        public object supplier { get; set; }
        public object vendorCatalog { get; set; }
        public bool sublicense { get; set; }
        public bool saleLegal { get; set; }
        public bool isMprimeSubscription { get; set; }
        public bool isPreorder { get; set; }
        public string image { get; set; }
        public string brandId { get; set; }
        public string brandName { get; set; }
        public Status status { get; set; }
        public Category category { get; set; }
        public object properties { get; set; }
        public List<string> images { get; set; }
        public List<PropertiesPortion> propertiesPortion { get; set; }
    }

}