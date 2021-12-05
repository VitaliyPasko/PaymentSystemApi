using Common.Enums;
using Common.ResponseDtos;
using Microsoft.Extensions.Localization;
using PaymentSystem.ApplicationLayer.Services.ProviderService.ProviderEntities.Interfaces;
using SharedResourceLibrary;

namespace PaymentSystem.ApplicationLayer.Services.ProviderDeterminantService.ProviderEntities
{
    public class BeelineProvider : IProvider
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        public ProviderType ProviderType { get; init; }

        public BeelineProvider(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
            ProviderType = ProviderType.Beeline;
        }
        
        public Response SendPayment(PaymentService.Models.Payment payment)
        {
            return new Response
            {
                Message = _localizer["ResponseSuccess"],
                StatusCode = StatusCode.Success
            };
        }
    }
}