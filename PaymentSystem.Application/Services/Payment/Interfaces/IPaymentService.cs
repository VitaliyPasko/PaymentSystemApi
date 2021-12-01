using System.Threading.Tasks;
using Common.ResponseDtos;
using Microsoft.Extensions.Primitives;
using PaymentSystem.ApplicationLayer.Services.Payment.Dto;

namespace PaymentSystem.ApplicationLayer.Services.Payment.Interfaces
{
    public interface IPaymentService
    {
        Task<Response> CreatePayment(PaymentDto paymentDto, StringValues requestId);
    }
}