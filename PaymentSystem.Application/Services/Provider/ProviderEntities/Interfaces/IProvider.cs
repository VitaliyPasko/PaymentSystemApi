using Common.Enums;
using Common.ResponseDtos;

namespace PaymentSystem.ApplicationLayer.Services.Provider.ProviderEntities.Interfaces
{
    public interface IProvider
    {
        Response SendPayment(Payment.Models.Payment payment);
        ProviderType ProviderType { get; init; }
    }
}