using PaymentSystem.ApplicationLayer.Services.ProviderService.ProviderEntities.Interfaces;

namespace PaymentSystem.ApplicationLayer.Services.ProviderDeterminantService.Interfaces
{
    public interface IProviderDeterminantService
    {
        IProvider GetProvider(string phone);
    }
}