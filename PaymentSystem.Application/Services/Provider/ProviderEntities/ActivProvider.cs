using Common.Enums;
using Common.ResponseDtos;
using PaymentSystem.ApplicationLayer.Services.Provider.ProviderEntities.Interfaces;

namespace PaymentSystem.ApplicationLayer.Services.Provider.ProviderEntities
{
    public class ActivProvider : IProvider
    {
        public ProviderType ProviderType { get; init; }
        public ActivProvider()
        {
            ProviderType = ProviderType.Activ;
        }
        public Response SendPayment(Payment.Models.Payment payment)
        {
            return new Response
            {
                Message = "Платеж пополнен успешно.",
                StatusCode = StatusCode.Success
            };
        }
    }
}