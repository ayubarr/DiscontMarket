﻿using DiscontMarket.Domain.Models.Entities;
using System.Linq.Expressions;

namespace DiscontMarket.DAL.Repository.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>  
    {
        public IEnumerable<Product> GetFilteredProductsAsync(Expression<Func<Product, bool>> filter);

    }
}