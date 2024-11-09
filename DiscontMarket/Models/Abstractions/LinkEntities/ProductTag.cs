using DiscontMarket.Domain.Models.Entities;

namespace DiscontMarket.Domain.Models.Abstractions.LinkEntities;

public class ProductTag
{
    public  Product? Product  { get; set; }
        
    public uint ProductID { get; set; }
        
    public Tag? Tag { get; set; }   
        
    public uint TagID { get; set; }


}