using System.Threading.Tasks;
using Common.ResponseDtos;
using Microsoft.Extensions.Primitives;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Dto;

namespace PaymentSystem.ApplicationLayer.Services.PaymentService.Interfaces
{
    public interface IPaymentService
    {
        Task<Response> CreatePayment(PaymentDto paymentDto, StringValues requestId);
    }
}