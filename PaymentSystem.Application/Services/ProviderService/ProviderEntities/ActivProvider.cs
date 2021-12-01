using Common.Enums;
using Common.ResponseDtos;
using PaymentSystem.ApplicationLayer.Services.ProviderService.ProviderEntities.Interfaces;

namespace PaymentSystem.ApplicationLayer.Services.ProviderService.ProviderEntities
{
    public class ActivProvider : IProvider
    {
        public ProviderType ProviderType { get; init; }
        public ActivProvider()
        {
            ProviderType = ProviderType.Activ;
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