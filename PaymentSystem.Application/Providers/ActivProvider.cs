using Common.Enums;
using Common.ResponseDtos;
using PaymentSystem.ApplicationLayer.Providers.Interfaces;
using PaymentSystem.ApplicationLayer.Services.Payment.Models;

namespace PaymentSystem.ApplicationLayer.Providers
{
    public class ActivProvider : IProvider
    {
        public ProviderType ProviderType { get; init; }
        public ActivProvider()
        {
            ProviderType = ProviderType.Activ;
        }
        public Response SendPayment(Payment payment)
        {
            return new Response
            {
                Message = "Платеж пополнен успешно.",
                StatusCode = StatusCode.Success
            };
        }
    }
}