using Common.Enums;
using Common.ResponseDtos;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Models;
using PaymentSystem.ApplicationLayer.Services.ProviderService.ProviderEntities.Interfaces;

namespace PaymentSystem.IntegrationTests.Fakes.FakeProviders
{
    public class FakeDecliningProvider : IProvider
    {
        public Response SendPayment(Payment payment)
        {
            return new Response
            {
                Message = "Платеж отклонен.",
                StatusCode = StatusCode.UnableError
            };
        }

        public ProviderType ProviderType { get; init; }
    }
}