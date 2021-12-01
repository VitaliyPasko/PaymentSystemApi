using Common.Enums;
using Common.ResponseDtos;
using PaymentSystem.ApplicationLayer.Exceptions;
using PaymentSystem.ApplicationLayer.Services.Provider.ProviderEntities.Interfaces;

namespace PaymentSystem.ApplicationLayer.Services.Provider.ProviderEntities
{
    public class UnknownProvider : IProvider
    {
        public UnknownProvider()
        {
            ProviderType = ProviderType.UnknownProvider;
        }

        public Response SendPayment(Payment.Models.Payment payment)
        {
            throw new ProviderNotFoundException(payment.Phone[..3]);
        }

        public ProviderType ProviderType { get; init; }
    }
}