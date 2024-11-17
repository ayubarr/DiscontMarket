using DiscontMarket.Domain.Models.Abstractions.BaseEntities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DiscontMarket.Services.Services.Abstractions
{
    public abstract class BaseTokenConstructor<TModel> where TModel : ApplicationUser
    {
        public BaseTokenConstructor()
        {

        }
        /// <summary>
        /// Создает токен доступа, используя предоставленный список утверждений пользователя
        /// </summary>
        /// <param name="claims"> список утверждений пользователя </param>
        /// <returns> Токен доступа </returns>
        protected abstract JwtSecurityToken GenerateToken(List<Claim> claims);

        /// <summary>
        /// Генерирует токен обновления.
        /// </summary>
        /// <returns> Токен обновления </returns>
        protected abstract string GenerateRefreshToken();

        /// <summary>
        /// Возвращает основные утверждения из истекшего JWT токена
        /// </summary>
        /// <param name="token"> токен доступа</param>
        /// <returns> Основные утверждения токена </returns>
        protected abstract ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string? token);
    }
}
