using DiscontMarket.ApiModels.DTO.EntityDTOs.Order;
using DiscontMarket.ApiModels.Responce.Helpers;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.DAL.Migrations;
using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Services.Helpers.Constants;
using DiscontMarket.Services.Services.Interfaces;
using DiscontMarket.Validation;
using System.Globalization;
using System.Net.Mail;
using System.Web;

namespace DiscontMarket.Services.Services.Implementations
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IOrderRepository _orderRepository;
        private readonly IBaseRepository<Session> _sessionRepository;
        private readonly IProductRepository _productRepository;



        public OrderService(IOrderRepository orderRepository, 
            IBaseRepository<Session> sessionRepository, 
            IProductRepository productRepository) : base(orderRepository) 
        {
            _httpClient = new HttpClient();
            _orderRepository = orderRepository;
            _sessionRepository = sessionRepository;
            _productRepository = productRepository;
        }

        public async Task<IBaseResponse<bool>> SendToTelegramAsync(OrderRequest orderInfo)
        {
            try
            {
                string botToken = "7533367208:AAF3QhldFUt8tlYIaCNB-GyXYKjUh7_Gq-Y"; // Замените на токен вашего бота
             // string chatId = "1106336448"; // Замените на ID вашего чата https://api.telegram.org/bot7533367208:AAF3QhldFUt8tlYIaCNB-GyXYKjUh7_Gq-Y/getUpdates
                string chatId = "379382151";
                string ClientName = string.IsNullOrWhiteSpace(orderInfo.name) ? "НЕ УКАЗАНО КЛИЕНТОМ" : orderInfo.name;
                string message = $"Новый заказ:\n" +
                $"- Время заказа: {orderInfo.datetime}\n" +
                $"- ФИО клиента: {ClientName}\n" +
                $"- Телефон клиента: {orderInfo.phoneNumber}\n" +
                $"- Ссылка на карточку: {orderInfo.url}\n";



                string url = $"https://api.telegram.org/bot{botToken}/sendMessage?chat_id={chatId}&text={message}";


                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }


        public IBaseResponse<bool> SendToEmail(OrderRequest orderInfo)
        {
            try 
            {
                string fromEmail = EmailInfo.Email;
                string fromPassword = EmailInfo.Password;
                string toEmail = orderInfo.email;
                string subject = "Новый заказ";

                string ClientName = string.IsNullOrWhiteSpace(orderInfo.name) ? "НЕ УКАЗАНО КЛИЕНТОМ" : orderInfo.name;
                string body = $"Новый заказ:\n" +
                $"- Время заказа: {orderInfo.datetime}\n" +
                $"- ФИО клиента: {ClientName}\n" +
                $"- Телефон клиента: {orderInfo.phoneNumber}\n" +
                $"- Ссылка на карточку: {orderInfo.url}\n";


                using (var smtp = new SmtpClient("smtp.gmail.com", 587)) // Укажите SMTP-сервер и порт
                {
                    smtp.Credentials = new System.Net.NetworkCredential(fromEmail, fromPassword);
                    smtp.EnableSsl = true;

                    var mailMessage = new MailMessage(fromEmail, toEmail, subject, body);
                    smtp.Send(mailMessage);
                }
                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }

        public IBaseResponse<bool> SetOrder(OrderRequest order)
        {
            try
            {
                ObjectValidator<OrderRequest>.CheckIsNotNullObject(order);

                string datetimeString = order.datetime;
                string format = "dd.MM.yyyy HH:mm"; // Ожидаемый формат даты и времени

                if (!DateTime.TryParseExact(datetimeString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime creationDate))
                {
                    throw new Exception("Incorrect datetime format");
                }

                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
                DateTime creationDateInUtcPlus3 = TimeZoneInfo.ConvertTimeFromUtc(creationDate.ToUniversalTime(), timeZone);

                creationDateInUtcPlus3 = DateTime.SpecifyKind(creationDateInUtcPlus3, DateTimeKind.Utc);

                DateTime utcDateTime = DateTime.UtcNow;
                DateTime endTimeInUtcPlus3 = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timeZone);

                endTimeInUtcPlus3 = DateTime.SpecifyKind(endTimeInUtcPlus3, DateTimeKind.Utc);

                var session = new Session
                {
                    InitialTime = creationDateInUtcPlus3,  // Время начала с корректным типом UTC
                    EndTime = endTimeInUtcPlus3,           // Время окончания с корректным типом UTC
                };

                _sessionRepository.Create(session);

                Uri uri = new Uri(order.url);
                var queryParameters = HttpUtility.ParseQueryString(uri.Query);

                // Извлечение параметра id
                string productIdStr = queryParameters["id"];
                uint productId = 0;

                if (string.IsNullOrEmpty(productIdStr) && uint.TryParse(productIdStr, out productId))
                {
                    throw new Exception("Incorrect product id format");
                }

                productId = uint.Parse(productIdStr);   

                var product = _productRepository.GetById(productId);
                var sessionEntity = _sessionRepository.GetAll().Where(s => s.InitialTime.Equals(creationDateInUtcPlus3)).FirstOrDefault();

                var entity = new Order               
                {
                    CreationDate = creationDate,
                    Condition = Domain.Models.Enums.Condition.Confirmed,
                    PhoneNumber = order.phoneNumber,
                    ProductAddress = order.url,
                    ClientsName = string.IsNullOrWhiteSpace(order.name) ? "НЕ УКАЗАНО КЛИЕНТОМ" : order.name,
                    Session = sessionEntity,
                    SessionID = sessionEntity.ID,
                    ProductID = productId,
                    Product = product,
                };

                _orderRepository.Create(entity);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }

        public IBaseResponse<List<OrderRequest>> GetOrders()
        {
            try
            {
               List<OrderRequest> orders =  _orderRepository.GetAllOrders()
                    .Take(1000)
                    .ToList();
               ObjectValidator<List<OrderRequest>>.CheckIsNotNullObject(orders);

                return ResponseFactory<List<OrderRequest>>.CreateSuccessResponse(orders);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<List<OrderRequest>>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<List<OrderRequest>>.CreateErrorResponse(exception);
            }
        }
    }
}
