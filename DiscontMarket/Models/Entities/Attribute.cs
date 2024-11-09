using DiscontMarket.Domain.Models.Abstractions.BaseEntities;
using DiscontMarket.Domain.Models.Abstractions.LinkEntities;

namespace DiscontMarket.Domain.Models.Entities
{
    
    public class Attribute : BaseEntity
    {
       
        public string AttributeName { get; set; }
       
       public List <ProductAttribute>? ProductAttributes { get; set; }
        
    }
}