using DiscontMarket.ApiModels.DTO.EntityDTOs.Product;
using DiscontMarket.Domain.Models.Entities;
using LinqKit;

namespace DiscontMarket.Services.Helpers.Filter
{
    public static class FilterHelper
    {
        public static ExpressionStarter<Product> CreateProductFilter(FilterProductDTO? productFilterDto)
        {
            
            var filter = PredicateBuilder.New<Product>(true);

            if (productFilterDto is null) return filter;

            if (productFilterDto.MaxPrice.HasValue)
                filter = filter.And(p => p.Price <= productFilterDto.MaxPrice.Value);

            if (productFilterDto.MinPrice.HasValue)
                filter = filter.And(p => p.Price >= productFilterDto.MinPrice);

            //if (productFilterDto.CategoryDTO is not null)       
            //    filter = filter.And(p => p.Categories.Any(c => c.Name.ToLower()
            //        .Contains(productFilterDto.CategoryDTO.CategoryName.ToLower())));

            if (productFilterDto.Status is not null)
            {
                foreach (var status in productFilterDto.Status)
                    filter = filter.And(p => p.Status.ToString()
                        .Equals(status, StringComparison.OrdinalIgnoreCase));
            }

            if (productFilterDto.Availability is not null)
            {
                foreach (var availability in productFilterDto.Availability)
                    filter = filter.And(p => p.Availability.ToString()
                        .Equals(availability, StringComparison.OrdinalIgnoreCase));
            }       

            if (productFilterDto.AttributeDTOs is not null)
            {
                foreach (var attribute in productFilterDto.AttributeDTOs)
                {
                    filter = filter.And(p => p.ProductAttributes.Any(c => c.Attribute.Name.ToLower()
                        .Contains(attribute.Name.ToLower()) && c.Attribute.Name.ToLower()
                        .Contains(attribute.Type.ToLower())));
                }
            }

            if (productFilterDto.Brand is not null)
            {
                foreach (var brand in productFilterDto.Brand)
                    filter = filter.And(p => p.Brand.Name
                        .Equals(brand.Name, StringComparison.OrdinalIgnoreCase));
            }
            
            return filter;
        }
    }
}
