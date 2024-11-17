namespace DiscontMarket.ApiModels.Responce.Interfaces
{
    public interface IBaseResponse<T>
    {
        /// <summary>
        /// Gets or sets the data associated with the response.
        /// </summary>
        T Data { get; set; }

        /// <summary>
        /// Gets or sets the status code of the response.
        /// </summary>
        int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the message describing the response.
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the response is a success or not.
        /// </summary>
        bool IsSuccess { get; set; }
    }
}
