using System;
using Common.Enums;
using PaymentSystem.ApplicationLayer.Collections;
using PaymentSystem.ApplicationLayer.Services.ProviderDeterminantService.Interfaces;
using PaymentSystem.ApplicationLayer.Services.ProviderService.ProviderEntities;
using PaymentSystem.ApplicationLayer.Services.ProviderService.ProviderEntities.Interfaces;

namespace PaymentSystem.ApplicationLayer.Services.ProviderDeterminantService
{
    public class ProviderDeterminantService : IProviderDeterminantService
    {
        private readonly ProviderCollection _providerCollection;

        public ProviderDeterminantService(
            ProviderCollection providerCollection)
        {
            _providerCollection = providerCollection;
        }

        private ProviderType DetermineProvider(string phone)
        {
            try
            {
                return _providerCollection[phone[..3]];
            }
            catch (Exception)
            {
                return ProviderType.UnknownProvider;
            }
        }
        
        public IProvider GetProvider(string phone)
        {
            var providerType = DetermineProvider(phone);
            
            return providerType switch
            {
                ProviderType.Beeline => new BeelineProvider(),
                ProviderType.TeleTwo => new TeleTwoProvider(),
                ProviderType.Activ => new ActivProvider(),
                ProviderType.Altel => new AltelProvider(),
                _ => new UnknownProvider()
            };
        }
    }
}