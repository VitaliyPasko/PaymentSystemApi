﻿using PaymentSystem.ApplicationLayer.Services.ProviderDeterminantService.Interfaces;
using PaymentSystem.ApplicationLayer.Services.ProviderService.ProviderEntities.Interfaces;
using PaymentSystem.IntegrationTests.Fakes.FakeProviders;

namespace PaymentSystem.IntegrationTests.Fakes.FakeServices
{
    public class FakeProviderDeterminantServiceGeneratingDeclining : IProviderDeterminantService
    {
        public IProvider GetProvider(string phone)
            => new FakeDecliningProvider();
    }
}