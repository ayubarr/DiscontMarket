using DiscontMarket.Domain.Models.Entities;

namespace DiscontMarket.Domain.Models.Abstractions.LinkEntities
{

    public class ProductAttribute
    {

        public  Product? Product  { get; set; }
        
        public uint? ProductID { get; set; }
        
        public AttributeEntity? Attribute { get; set; }   
        
        public uint? AttributeID { get; set; }
    }

}