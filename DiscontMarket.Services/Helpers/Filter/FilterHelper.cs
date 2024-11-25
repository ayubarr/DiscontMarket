using DiscontMarket.ApiModels.DTO.EntityDTOs.Product;
using DiscontMarket.Domain.Models.Entities;
using LinqKit;

namespace DiscontMarket.Services.Helpers.Filter
{
    public static class FilterHelper
    {
        public static ExpressionStarter<Product> CreateProductFilter(FilterProductDTO? productFilterDto)
        {
            // Базовый фильтр — отображаем все карточки выбранной категории
            var filter = PredicateBuilder.New<Product>(true);

            if (productFilterDto is null)
                return filter;

            // Категория (категория обязательно выбрана)
            if (productFilterDto.CategoryDTO is not null)
            {
                filter = filter.And(p => p.Category.Name.ToLower()
                    .Equals(productFilterDto.CategoryDTO.CategoryName.ToLower()));
            }

            // Фильтр по цене (всегда применяется, если указаны значения)
            if (productFilterDto.MinPrice.HasValue)
                filter = filter.And(p => p.Price >= productFilterDto.MinPrice.Value);

            if (productFilterDto.MaxPrice.HasValue)
                filter = filter.And(p => p.Price <= productFilterDto.MaxPrice.Value);

            // Фильтр по статусу и доступности (если выбраны статусы или доступность)
            if ((productFilterDto.Status is not null && productFilterDto.Status.Any()) ||
                (productFilterDto.Availability is not null && productFilterDto.Availability.Any()))
            {
                var statusFilter = PredicateBuilder.False<Product>();

                if (productFilterDto.Status is not null && productFilterDto.Status.Any())
                {
                    foreach (var status in productFilterDto.Status)
                    {
                        statusFilter = statusFilter.Or(p => p.Status.ToString()
                            .Equals(status, StringComparison.OrdinalIgnoreCase));
                    }
                }

                if (productFilterDto.Availability is not null && productFilterDto.Availability.Any())
                {
                    foreach (var availability in productFilterDto.Availability)
                    {
                        statusFilter = statusFilter.Or(p => p.Availability.ToString()
                            .Equals(availability, StringComparison.OrdinalIgnoreCase));
                    }
                }

                filter = filter.And(statusFilter);
            }

            // Фильтр по атрибутам (например, HD, Full HD)
            if (productFilterDto.AttributeDTOs is not null && productFilterDto.AttributeDTOs.Any())
            {
                var attributeFilter = PredicateBuilder.False<Product>();

                foreach (var attribute in productFilterDto.AttributeDTOs)
                {
                    attributeFilter = attributeFilter.Or(p => p.ProductAttributes.Any(pa =>
                        pa.Attribute.Name.Equals(attribute.Name, StringComparison.OrdinalIgnoreCase) &&
                        pa.Attribute.Type.Equals(attribute.Type, StringComparison.OrdinalIgnoreCase)));
                }

                filter = filter.And(attributeFilter);
            }

            // Фильтр по бренду (например, LG)
            if (productFilterDto.Brand is not null && productFilterDto.Brand.Any())
            {
                var brandFilter = PredicateBuilder.False<Product>();

                foreach (var brand in productFilterDto.Brand)
                {
                    brandFilter = brandFilter.Or(p => p.Brand.Name
                        .Equals(brand.Name, StringComparison.OrdinalIgnoreCase));
                }

                filter = filter.And(brandFilter);
            }

            return filter;
        }
    }
}
