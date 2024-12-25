using DiscontMarket.ApiModels.Responce.Helpers;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.Services.Services.Interfaces;
using System.Net.Mail;

namespace DiscontMarket.Services.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<IBaseResponse<bool>> SendToTelegramAsync(string phoneNumber)
        {
            try
            {
                string botToken = "7533367208:AAF3QhldFUt8tlYIaCNB-GyXYKjUh7_Gq-Y"; // Замените на токен вашего бота
                string chatId = "1106336448"; // Замените на ID вашего чата https://api.telegram.org/bot7533367208:AAF3QhldFUt8tlYIaCNB-GyXYKjUh7_Gq-Y/getUpdates
                string message = $"Новый заказ. Телефон клиента: {phoneNumber}";

                string url = $"https://api.telegram.org/bot{botToken}/sendMessage?chat_id={chatId}&text={message}";


                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                SendToEmail(phoneNumber);

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


        public IBaseResponse<bool> SendToEmail(string phoneNumber)
        {
            try 
            { 
                string fromEmail = "discont.marketfirst@gmail.com";
                string fromPassword = "zfznzxorqpkszdva";
                string toEmail = "ayub.arbiev@yandex.ru";
                string subject = "Новый заказ";
                string body = $"Новый заказ. Телефон клиента: {phoneNumber}";

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
    }
}
