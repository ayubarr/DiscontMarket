using System.Collections.Generic; 
namespace productdetails{ 

    public class Body
    {
        public string productId { get; set; }
        public string name { get; set; }
        public string nameTranslit { get; set; }
        public string brandName { get; set; }
        public string materialSource { get; set; }
        public string productType { get; set; }
        public List<string> images { get; set; }
        public object vendorCatalog { get; set; }
        public object usedProductType { get; set; }
        public Category category { get; set; }
        public string materialCisNumber { get; set; }
        public string description { get; set; }
        public string modelName { get; set; }
        public Properties properties { get; set; }
        public string image { get; set; }
        public List<string> images3d { get; set; }
        public List<string> imagesBig { get; set; }
        public string brandNameTranslit { get; set; }
        public string brandId { get; set; }
        public object brandUrl { get; set; }
        public object brandLogoUrl { get; set; }
        public bool isBrandVisible { get; set; }
        public List<Group> groups { get; set; }
        public List<string> certificates { get; set; }
        public List<string> instructions { get; set; }
        public List<string> videos { get; set; }
        public Status status { get; set; }
        public object supplier { get; set; }
        public Rating rating { get; set; }
        public bool sublicense { get; set; }
        public List<object> customNotifications { get; set; }
        public List<object> variants { get; set; }
        public bool isWatchOnlineBtnVisible { get; set; }
        public object watchOnlineBtnId { get; set; }
        public bool isMprimeSubscription { get; set; }
        public bool isPreorder { get; set; }
        public bool saleLegal { get; set; }
        public BlocksOrder blocksOrder { get; set; }
        public object multiofferInfo { get; set; }
    }

}