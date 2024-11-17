using DiscontMarket.ApiModels.DTO.BaseDTOs;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.Domain.Models.Abstractions.BaseEntities;

namespace DiscontMarket.Services.Services.Interfaces
{
    public interface IBaseService<T>
        where T : BaseEntity
    {
        IBaseResponse<T> Create(T entity);  
        Task<IBaseResponse<T>> CreateAsync<Tmodel>(Tmodel entityDTO) where Tmodel : BaseDTO;

        IBaseResponse<IEnumerable<T>> GetAll();

        IBaseResponse<T> GetById(uint Id);

        Task<IBaseResponse<IEnumerable<T>>> GetAllAsync();

        Task<IBaseResponse<T>> GetByIdAsync(uint Id);

        Task<IBaseResponse<bool>> UpdateAsync<TModel>(TModel entitieDto) where TModel : BaseDTO;

        Task<IBaseResponse<bool>> DeleteAsync();

        Task<IBaseResponse<bool>> DeleteByIdAsync(uint Id);
    }
}
