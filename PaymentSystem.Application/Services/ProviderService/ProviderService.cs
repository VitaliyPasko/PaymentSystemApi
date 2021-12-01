using Common.Enums;
using Common.ResponseDtos;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Models;
using PaymentSystem.ApplicationLayer.Services.ProviderDeterminantService.Interfaces;
using PaymentSystem.ApplicationLayer.Services.ProviderService.Interfaces;

namespace PaymentSystem.ApplicationLayer.Services.ProviderService
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderDeterminantService _determinantService;

        public ProviderService(IProviderDeterminantService determinantService)
        {
            _determinantService = determinantService;
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