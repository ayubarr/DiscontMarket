using System.ComponentModel.DataAnnotations;

namespace DiscontMarket.ApiModels.Auth
{
    /// <summary>
    /// Класс модели, представляющий информацию для входа пользователя.
    /// Он включает свойства для имени пользователя и пароля.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required(ErrorMessage = "User is required")]
        public string? Username { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
