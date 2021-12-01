using System.Threading.Tasks;
using Common.ResponseDtos;
using Microsoft.Extensions.Primitives;
using PaymentSystem.ApplicationLayer.Services.Payment.Dto;
using PaymentSystem.ApplicationLayer.Services.Payment.Interfaces;

namespace PaymentSystem.ApplicationLayer.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        public Task<Response> CreatePayment(PaymentDto paymentDto, StringValues requestId)
        {
            
        }
    }
}