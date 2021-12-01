using Common.Enums;
using Common.ResponseDtos;
using PaymentSystem.ApplicationLayer.Services.Provider.ProviderEntities.Interfaces;

namespace PaymentSystem.ApplicationLayer.Services.Provider.ProviderEntities
{
    public class BeelineProvider : IProvider
    {
        public ProviderType ProviderType { get; init; }

        public BeelineProvider()
        {
            ProviderType = ProviderType.Beeline;
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