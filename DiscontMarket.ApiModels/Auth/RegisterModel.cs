using System.ComponentModel.DataAnnotations;

namespace DiscontMarket.ApiModels.Auth
{
    /// <summary>
    /// Класс модели, представляющий регистрационную информацию для пользователя.
    /// Он включает свойства для имени пользователя, электронной почты и пароля.
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required(ErrorMessage = "User is required")]
        public string? UserName { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        /// <summary>
        /// Почта пользователя, для авторизации
        /// </summary>
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
    }
}
