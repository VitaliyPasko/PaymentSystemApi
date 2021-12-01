using System.Threading.Tasks;
using Common.ResponseDtos;
using Microsoft.Extensions.Primitives;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Dto;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Interfaces;

namespace PaymentSystem.ApplicationLayer.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        public Task<Response> CreatePayment(PaymentDto paymentDto, StringValues requestId)
        {
            
        }
    }
}