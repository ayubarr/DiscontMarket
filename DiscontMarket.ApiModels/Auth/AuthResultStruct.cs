namespace DiscontMarket.ApiModels.Auth
{
    /// <summary>
    /// Структура, представляющая результат операции аутентификации.
    /// Она включает свойства для токена, токена обновления и даты истечения срока действия.
    /// </summary>
    public struct AuthResultStruct
    {
        /// <summary>
        /// Токен аунтификации
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Дата истечения срока действия токена аунтификации
        /// </summary>
        public DateTime Expiration { get; set; }

        /// <summary>
        /// Токен обновления сессии
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
