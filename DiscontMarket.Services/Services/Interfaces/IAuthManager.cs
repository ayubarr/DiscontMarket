using DiscontMarket.ApiModels.Auth;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.Domain.Models.Abstractions.BaseEntities;

namespace DiscontMarket.Services.Services.Interfaces
{
    /// <summary>
    /// Определяет методы для управления аутентификацией и авторизацией пользователей.
    /// </summary>
    /// <typeparam name="Tmodel">Тип модели пользователя, наследующей ApplicationUser.</typeparam>
    public interface IAuthManager<Tmodel> where Tmodel : ApplicationUser
    {
        /// <summary>
        /// Выполняет процесс входа пользователя в систему.
        /// </summary>
        /// <param name="model">Модель, содержащая учетные данные для входа.</param>
        /// <returns>Объект, представляющий результат операции аутентификации.</returns>
        Task<IBaseResponse<AuthResultStruct>> Login(LoginModel model);

        /// <summary>
        /// Выполняет процесс регистрации нового пользователя.
        /// </summary>
        /// <param name="model">Модель, содержащая данные нового пользователя для регистрации.</param>
        /// <returns>Объект, представляющий результат операции регистрации.</returns>
        Task<IBaseResponse<uint>> Register(RegisterModel model);
    }
}
