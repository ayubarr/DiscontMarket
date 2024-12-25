using DiscontMarket.ApiModels.Responce.Interfaces;

namespace DiscontMarket.Services.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<IBaseResponse<bool>> SendToTelegramAsync(string phoneNumber);
        public IBaseResponse<bool> SendToEmail(string phoneNumber);

    }
}
