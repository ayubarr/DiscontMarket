﻿using DiscontMarket.Domain.Models.Abstractions.BaseEntities;

namespace DiscontMarket.DAL.Repository.Interfaces
{
    public interface IBrandRepository<T> where T : BaseEntity
    {
        public Task CreateAsync(T entity);

        public void Create(T entity);

        public IQueryable<T> GetAll();

        public T GetById(uint id);

        public Task<IQueryable<T>> GetAllAsync();

        public Task<T> GetByIdAsync(uint id);

        public Task Update(T entity);

        public Task Delete(T entity);

        public Task DeleteById(uint id);

    }
}
