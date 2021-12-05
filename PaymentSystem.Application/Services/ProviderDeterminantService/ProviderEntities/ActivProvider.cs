using Common.Enums;
using Common.ResponseDtos;
using Microsoft.Extensions.Localization;
using PaymentSystem.ApplicationLayer.Services.ProviderService.ProviderEntities.Interfaces;
using SharedResourceLibrary;

namespace PaymentSystem.ApplicationLayer.Services.ProviderDeterminantService.ProviderEntities
{
    public class ActivProvider : IProvider
    {
        public ProviderType ProviderType { get; init; }
        private readonly IStringLocalizer<SharedResource> _localizer;
        public ActivProvider(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
            ProviderType = ProviderType.Activ;
        }
        public Response SendPayment(PaymentService.Models.Payment payment)
        {
            return new Response
            {
                Message = _localizer["ResponseSuccess"]!,
                StatusCode = StatusCode.Success
            };
        }
    }
}