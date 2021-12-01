using Common.Enums;
using Common.ResponseDtos;

namespace PaymentSystem.ApplicationLayer.Services.ProviderService.ProviderEntities.Interfaces
{
    public interface IProvider
    {
        Response SendPayment(PaymentService.Models.Payment payment);
        ProviderType ProviderType { get; init; }
    }
}