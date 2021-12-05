using System;
using Common.Enums;
using Microsoft.Extensions.Localization;
using PaymentSystem.ApplicationLayer.Collections;
using PaymentSystem.ApplicationLayer.Services.ProviderDeterminantService.Interfaces;
using PaymentSystem.ApplicationLayer.Services.ProviderDeterminantService.ProviderEntities;
using PaymentSystem.ApplicationLayer.Services.ProviderService.ProviderEntities.Interfaces;
using SharedResourceLibrary;

namespace PaymentSystem.ApplicationLayer.Services.ProviderDeterminantService
{
    public class ProviderDeterminantService : IProviderDeterminantService
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly ProviderCollection _providerCollection;

        public ProviderDeterminantService(
            ProviderCollection providerCollection, 
            IStringLocalizer<SharedResource> localizer)
        {
            _providerCollection = providerCollection;
            _localizer = localizer;
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
                ProviderType.Beeline => new BeelineProvider(_localizer),
                ProviderType.TeleTwo => new TeleTwoProvider(_localizer),
                ProviderType.Activ => new ActivProvider(_localizer),
                ProviderType.Altel => new AltelProvider(_localizer),
                _ => new UnknownProvider()
            };
        }
    }
}