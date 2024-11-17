namespace DiscontMarket.ApiModels.Auth
{
    /// <summary>
    /// Класс модели, представляющий токен.
    /// Он включает свойства для токена доступа и токена обновления.
    /// </summary>
    public class TokenModel
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
