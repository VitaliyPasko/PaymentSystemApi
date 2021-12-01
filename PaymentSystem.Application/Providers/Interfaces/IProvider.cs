using Common.Enums;
using Common.ResponseDtos;
using PaymentSystem.ApplicationLayer.Services.Payment.Models;

namespace PaymentSystem.ApplicationLayer.Providers.Interfaces
{
    public interface IProvider
    {
        Response SendPayment(Payment payment);
        ProviderType ProviderType { get; init; }
    }
}