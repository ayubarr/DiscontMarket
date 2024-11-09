

using DiscontMarket.Domain.Models.Entities;
using Attribute = System.Attribute;

namespace DiscontMarket.Domain.Models.Abstractions.LinkEntities
{

    public class ProductAttribute
    {

        public  Product? Product  { get; set; }
        
        public uint ProductID { get; set; }
        
        public Attribute? Attribute { get; set; }   
        
        public uint AttributeID { get; set; }
    }

}