using Common.Enums;
using Common.ResponseDtos;
using Microsoft.Extensions.Localization;
using PaymentSystem.ApplicationLayer.Services.ProviderService.ProviderEntities.Interfaces;
using SharedResourceLibrary;

namespace PaymentSystem.ApplicationLayer.Services.ProviderDeterminantService.ProviderEntities
{
    public class TeleTwoProvider : IProvider
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        public ProviderType ProviderType { get; init; }

        public TeleTwoProvider(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
            ProviderType = ProviderType.TeleTwo;
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