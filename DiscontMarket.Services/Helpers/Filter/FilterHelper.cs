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

            if (productFilterDto.ProductAvailability.HasValue)
                filter = filter.And(p => p.ProductAvailability.ToString()
                    .Contains(productFilterDto.ProductAvailability.ToString().ToLower()));

            if (productFilterDto.ProductStatus.HasValue)
                filter = filter.And(p => p.ProductStatus.ToString()
                    .Contains(productFilterDto.ProductStatus.ToString().ToLower()));

            if(productFilterDto.categoryDTOs.Count  > 0)
            {
                foreach(var category in productFilterDto.categoryDTOs)
                {
                    filter = filter.And(p => p.Categories.Any(c => c.Name.ToLower()
                        .Contains(category.Name.ToLower()) && c.Type.ToLower()
                        .Contains(category.Type.ToLower())));
                }

            }

            return filter;
        }
    }
}
