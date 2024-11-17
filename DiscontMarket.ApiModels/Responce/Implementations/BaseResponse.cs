using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.Validation;

namespace DiscontMarket.ApiModels.Responce.Implementations
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        private T _data;
        public T Data
        {
            get
            {
                return _data;
            }

            set
            {
                try
                {
                    ObjectValidator<T>.CheckIsNotNullObject(value);
                    _data = value;
                }
                catch (ArgumentNullException exception)
                {
                    _message += $"\nERROR: {exception}";
                }
            }
        }

        private int _statusCode;
        public int StatusCode
        {
            get
            {
                return _statusCode;
            }
            set
            {
                try
                {
                    NumberValidator<int>.IsPositive(value);
                    _statusCode = value;
                }
                catch (ArgumentException exception)
                {
                    _message += $"\nERROR: {exception}";

                }
            }
        }


        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                try
                {
                    StringValidator.CheckIsNotNull(value);
                    _message = value;
                }
                catch (ArgumentNullException exception)
                {
                    _message += $"\nERROR: {exception}";
                }
            }
        }

        public bool IsSuccess { get; set; }  
    }
}
