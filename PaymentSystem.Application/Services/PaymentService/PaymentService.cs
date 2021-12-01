using System.Threading.Tasks;
using Common.ResponseDtos;
using Microsoft.Extensions.Primitives;
using PaymentSystem.ApplicationLayer.Data.Interfaces;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Dto;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Interfaces;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Models;
using PaymentSystem.ApplicationLayer.Services.ProviderDeterminantService.Interfaces;

namespace PaymentSystem.ApplicationLayer.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IApplicationRepository<Payment> _paymentRepository;
        private readonly IProviderDeterminantService _providerDeterminantService;

        public PaymentService(IApplicationRepository<Payment> paymentRepository, IProviderDeterminantService providerDeterminantService)
        {
            _paymentRepository = paymentRepository;
            _providerDeterminantService = providerDeterminantService;
        }

        public Task<Response> CreatePayment(PaymentDto paymentDto, StringValues requestId)
        {
            
        }
    }
}