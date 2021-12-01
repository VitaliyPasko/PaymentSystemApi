using Common.ResponseDtos;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Models;

namespace PaymentSystem.ApplicationLayer.Services.ProviderService.Interfaces
{
    public interface IProviderService
    {
        Response SendPayment(Payment payment);
    }
}