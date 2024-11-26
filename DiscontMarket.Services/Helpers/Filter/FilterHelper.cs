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

            if (productFilterDto.Attributes is not null)
                filter = filter.And(BuildSubFilter(
                    productFilterDto.Attributes,
                    (p, attribute) => p.ProductAttributes.Any(c =>
                        c.Attribute.Name.Equals(attribute, StringComparison.OrdinalIgnoreCase))
                ));

            if (productFilterDto.Brands is not null)
                filter = filter.And(BuildSubFilter(
                    productFilterDto.Brands,
                    (p, brand) => p.Brand != null &&
                                  p.Brand.Name.Equals(brand, StringComparison.OrdinalIgnoreCase)
                ));

            if (productFilterDto.Status is not null)
                filter = filter.And(BuildSubFilter(
                    productFilterDto.Status,
                    (p, status) => p.Status.ToString().Equals(status, StringComparison.OrdinalIgnoreCase)
                ));

            if (productFilterDto.Availability is not null)
                filter = filter.And(BuildSubFilter(
                    productFilterDto.Availability,
                    (p, availability) => p.Availability.ToString().Equals(availability, StringComparison.OrdinalIgnoreCase)
                ));

            if (!string.IsNullOrWhiteSpace(productFilterDto.CategoryDTO?.CategoryName))
                filter = filter.And(p => p.Category != null &&
                                         p.Category.Name.ToLowerInvariant()
                                         .Contains(productFilterDto.CategoryDTO.CategoryName.ToLowerInvariant())
                );

            if (productFilterDto.MinPrice.HasValue && productFilterDto.MaxPrice.HasValue)
                filter = filter.And(p => p.Price >= productFilterDto.MinPrice.Value &&
                                         p.Price <= productFilterDto.MaxPrice.Value);
            else
            {
                if (productFilterDto.MinPrice.HasValue)
                    filter = filter.And(p => p.Price >= productFilterDto.MinPrice.Value);

                if (productFilterDto.MaxPrice.HasValue)
                    filter = filter.And(p => p.Price <= productFilterDto.MaxPrice.Value);
            }

            return filter;
        }

        private static ExpressionStarter<Product> BuildSubFilter<T>(
            IEnumerable<T> items,
            Func<Product, T, bool> predicate)
        {
            var subFilter = PredicateBuilder.New<Product>(true);
            foreach (var item in items)
            {
                subFilter = subFilter.Or(p => predicate(p, item));
            }
            return subFilter;
        }
    }
}
