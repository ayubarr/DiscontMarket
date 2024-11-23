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



            if (!string.IsNullOrEmpty(productFilterDto.ProductAvailability))
                filter = filter.And(p => p.ProductAvailability.ToString()
                    .Equals(productFilterDto.ProductAvailability, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(productFilterDto.ProductStatus))
                filter = filter.And(p => p.ProductStatus.ToString()
                    .Equals(productFilterDto.ProductStatus, StringComparison.OrdinalIgnoreCase));


            if (productFilterDto.categoryDTOs is not null)
            {
                foreach(var category in productFilterDto.categoryDTOs)
                {
                    filter = filter.And(p => p.Categories.Any(c => c.Name.ToLower()
                        .Contains(category.Name.ToLower()) && c.Type.ToLower()
                        .Contains(category.Type.ToLower())));
                }
            }

            if (productFilterDto.attributeDTOs is not null)
            {
                foreach (var attribute in productFilterDto.attributeDTOs)
                {
                    filter = filter.And(p => p.ProductAttributes.Any(c => c.Attribute.Name.ToLower()
                        .Contains(attribute.Name.ToLower()) && c.Attribute.Name.ToLower()
                        .Contains(attribute.Type.ToLower())));
                }
            }

            if (productFilterDto.Brend is not null)
                filter = filter.And(p => p.Brend.Name.Contains(productFilterDto.Brend.Type)
                    && p.Brend.Type.Contains(productFilterDto.Brend.Name));

            return filter;
        }
    }
}
