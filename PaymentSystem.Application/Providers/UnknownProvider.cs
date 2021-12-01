using Common.Enums;
using Common.ResponseDtos;
using PaymentSystem.ApplicationLayer.Exceptions;
using PaymentSystem.ApplicationLayer.Providers.Interfaces;
using PaymentSystem.ApplicationLayer.Services.Payment.Models;

namespace PaymentSystem.ApplicationLayer.Providers
{
    public class UnknownProvider : IProvider
    {
        public UnknownProvider()
        {
            ProviderType = ProviderType.UnknownProvider;
        }

        public Response SendPayment(Payment payment)
        {
            throw new ProviderNotFoundException(payment.Phone[..3]);
        }

        public ProviderType ProviderType { get; init; }
    }
}