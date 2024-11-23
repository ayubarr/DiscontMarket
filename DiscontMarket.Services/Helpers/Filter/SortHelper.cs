using DiscontMarket.ApiModels.DTO.EntityDTOs.Product;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Domain.Models.Enums;

namespace DiscontMarket.Services.Helpers.Filter
{
    public static class SortHelper
    {
        public static IEnumerable<Product> SortProducts(IEnumerable<Product> products, SortTypes? sortType)
        {

            if(!sortType.HasValue) return products;

            Random random = new Random();


            products = sortType switch
            {
                SortTypes.Popularity => products.OrderBy(_ => random.Next()), // Сортировка в случайном порядке
                SortTypes.Price => products.OrderBy(p => p.Price), // Сортировка по цене (по возрастанию)
                SortTypes.Rating => products.OrderBy(_ => random.Next()), // Сортировка в случайном порядке
                SortTypes.Title => products.OrderBy(p => p.ProductName), // Сортировка по названию
                SortTypes.Quantity => products.OrderBy(p => p.Quantity), // Сортировка по количеству
                _ => products // Если тип неизвестен, возвращаем исходный список
            };


            return products;
        }

    }
}
