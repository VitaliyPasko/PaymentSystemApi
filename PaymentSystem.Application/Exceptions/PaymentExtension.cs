using Common.Enums;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Dto;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Models;

namespace PaymentSystem.ApplicationLayer.Exceptions
{
    public static class PaymentExtension
    {
        public static Payment MapToPayment(this PaymentDto paymentDto, ProviderType providerType)
        {
            return new Payment
            {
                Amount = paymentDto.Amount,
                Phone = paymentDto.Phone,
                ExternalNumber = paymentDto.ExternalNumber,
                ProviderType = providerType,
                Status = paymentDto.Status
            };
        }
    }
}