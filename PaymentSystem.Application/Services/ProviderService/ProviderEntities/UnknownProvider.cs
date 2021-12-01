using Common.Enums;
using Common.ResponseDtos;
using PaymentSystem.ApplicationLayer.Exceptions;
using PaymentSystem.ApplicationLayer.Services.ProviderService.ProviderEntities.Interfaces;

namespace PaymentSystem.ApplicationLayer.Services.ProviderService.ProviderEntities
{
    public class UnknownProvider : IProvider
    {
        public UnknownProvider()
        {
            ProviderType = ProviderType.UnknownProvider;
        }

        public Response SendPayment(PaymentService.Models.Payment payment)
        {
            throw new ProviderNotFoundException(payment.Phone[..3]);
        }

        public ProviderType ProviderType { get; init; }
    }
}