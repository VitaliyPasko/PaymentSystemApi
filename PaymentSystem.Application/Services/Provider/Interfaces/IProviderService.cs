using Common.ResponseDtos;

namespace PaymentSystem.ApplicationLayer.Services.Provider.Interfaces
{
    public interface IProviderService
    {
        Response SendPayment(Payment payment);
    }
}