using System;
using System.Threading.Tasks;
using Common.Enums;
using Common.ResponseDtos;
using Microsoft.Extensions.Primitives;
using PaymentSystem.ApplicationLayer.Data.Interfaces;
using PaymentSystem.ApplicationLayer.Exceptions;
using PaymentSystem.ApplicationLayer.Services.ErrorIdentifierService.Interfaces;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Dto;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Interfaces;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Models;
using PaymentSystem.ApplicationLayer.Services.ProviderDeterminantService.Interfaces;
using PaymentSystem.ApplicationLayer.Services.ProviderService.Interfaces;
using PaymentSystem.ApplicationLayer.Services.ProviderService.ProviderEntities.Interfaces;
using PaymentSystem.ApplicationLayer.Services.ValidationService.Interfaces;

namespace PaymentSystem.ApplicationLayer.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IApplicationRepository<Payment> _paymentRepository;
        private readonly IProviderDeterminantService _providerDeterminantService;
        private readonly IPaymentValidationService _paymentValidationService;
        private readonly IErrorIdentifierService<PaymentDto> _identifierService;
        private readonly IProviderService _providerService;

        public PaymentService(IApplicationRepository<
            Payment> paymentRepository, 
            IProviderDeterminantService providerDeterminantService, 
            IPaymentValidationService paymentValidationService, 
            IErrorIdentifierService<PaymentDto> identifierService, 
            IProviderService providerService)
        {
            _paymentRepository = paymentRepository;
            _providerDeterminantService = providerDeterminantService;
            _paymentValidationService = paymentValidationService;
            _identifierService = identifierService;
            _providerService = providerService;
        }

        public async Task<Response> CreatePayment(PaymentDto paymentDto, StringValues requestId)
        {
            try
            {
                var isValidAmount = _paymentValidationService.ValidateAmount(paymentDto.Amount);
                var isValidPhone = _paymentValidationService.ValidatePhone(paymentDto.Phone);

                if (isValidAmount && isValidPhone)
                {
                    IProvider provider = _providerDeterminantService.GetProvider(paymentDto.Phone);
                    var payment = paymentDto.MapToPayment(provider.ProviderType);
                    var result = provider.SendPayment(payment);
                    
                    if (result.StatusCode is not StatusCode.Success && result.StatusCode is not StatusCode.ServiceUnavailable)
                        return result;
                    if (result.StatusCode == StatusCode.ServiceUnavailable)
                        payment.Status = result.StatusCode.MapToPaymentStatus();
                    await _paymentRepository.Add(payment);
                }
                return new Response
                {
                    Message = "Валидация провалилась.",
                    StatusCode = StatusCode.ValidationProblem
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}