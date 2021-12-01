using Common.Enums;
using Common.ResponseDtos;
using PaymentSystem.ApplicationLayer.Services.ProviderService.ProviderEntities.Interfaces;

namespace PaymentSystem.ApplicationLayer.Services.ProviderService.ProviderEntities
{
    public class AltelProvider : IProvider
    {
        public ProviderType ProviderType { get; init; }
        
        public AltelProvider()
        {
            ProviderType = ProviderType.Altel;
        }
        
        public Response SendPayment(PaymentService.Models.Payment payment)
        {
            return new Response
            {
                Message = "Платеж пополнен успешно.",
                StatusCode = StatusCode.Success
            };
        }
    }
}