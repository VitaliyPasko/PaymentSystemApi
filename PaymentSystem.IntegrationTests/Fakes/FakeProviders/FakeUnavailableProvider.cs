using Common.Enums;
using Common.ResponseDtos;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Models;
using PaymentSystem.ApplicationLayer.Services.ProviderService.ProviderEntities.Interfaces;

namespace PaymentSystem.IntegrationTests.Fakes.FakeProviders
{
    public class FakeUnavailableProvider : IProvider
    {
        public Response SendPayment(Payment payment)
        {
            return new Response
            {
                Message = "Сервис недоступен.",
                StatusCode = StatusCode.ServiceUnavailable
            };
        }

        public ProviderType ProviderType { get; init; }
    }
}