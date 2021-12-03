using System;
using System.Threading.Tasks;
using Common.Enums;
using Common.ResponseDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using PaymentSystem.ApplicationLayer.Data.Interfaces;
using PaymentSystem.ApplicationLayer.Exceptions;
using PaymentSystem.ApplicationLayer.Extensions;
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
        private readonly IErrorIdentifierService<PaymentDto> _errorIdentifierService;
        private readonly IProviderService _providerService;
        private readonly ILogger<PaymentService> _logger;
        private readonly Type _type;

        public PaymentService(IApplicationRepository<
            Payment> paymentRepository, 
            IProviderDeterminantService providerDeterminantService, 
            IPaymentValidationService paymentValidationService, 
            IErrorIdentifierService<PaymentDto> identifierService, 
            IProviderService providerService, ILogger<PaymentService> logger)
        {
            _paymentRepository = paymentRepository;
            _providerDeterminantService = providerDeterminantService;
            _paymentValidationService = paymentValidationService;
            _errorIdentifierService = identifierService;
            _providerService = providerService;
            _logger = logger;
            _type = GetType();
        }

        public async Task<Response> AddPayment(PaymentDto paymentDto, StringValues requestId)
        {
            try
            {
                var isValidAmount = _paymentValidationService.ValidateAmount(paymentDto.Amount);
                var isValidPhone = _paymentValidationService.ValidatePhone(paymentDto.Phone);

                if (isValidAmount && isValidPhone)
                {
                    var provider = _providerDeterminantService.GetProvider(paymentDto.Phone);
                    var payment = paymentDto.MapToPayment(provider.ProviderType);
                    await _paymentRepository.Add(payment);
                    var result = provider.SendPayment(payment);
                    _logger.LogInformation("{@Service}. Ответ от провайдера: {@Result}. RequestId: {@RequestId}", 
                        _type, result, requestId);
                    
                    if (result.StatusCode is not StatusCode.Success && result.StatusCode is not StatusCode.ServiceUnavailable)
                        return result;
                    if (result.StatusCode == StatusCode.ServiceUnavailable)
                        payment.Status = result.StatusCode.MapToPaymentStatus();

                    await _paymentRepository.Update(payment);
                    _logger.LogWarning("{@Service}. Результат обработки платежа: {@Payment}.  Данные: {@Result}.  {@RequestId}", 
                        _type,  payment, result, requestId);
                    return result;
                }
                
                _logger.LogWarning("{@Service}. Валидация провалилась. IsValidAmount: {@IsValidAmount}. isValidPhone: {@IsValidPhone} Данные: {@Payment}. {@RequestId}", 
                    _type, isValidAmount, isValidPhone, paymentDto, requestId);
                
                return new Response
                {
                    Message = "Валидация провалилась.",
                    StatusCode = StatusCode.ValidationProblem
                };
            }
            catch (ProviderNotFoundException e)
            {
                _logger.LogTrace(e, e.Message);
                _logger.LogError("{@Service}. Ошибка: {@Message}. Данные: {@Data}. RequestId: {@RequestId}",
                    _type, e.Message, paymentDto, requestId);
                return new Response()
                {
                    Message = $"Провайдера с таким префиксом не найдено: {paymentDto.Phone[..3]}",
                    StatusCode = StatusCode.ProviderNotFound
                };
            }
            catch (DbUpdateException e)
            {
                _logger.LogTrace(e, e.Message);
                _logger.LogError("{@Service}. Ошибка добавления записи в базу данных: {@Message}. Данные: {@Data}. RequestId: {@RequestId}",
                    _type, e.Message, paymentDto, requestId);
                
                var result = new Response();
                var isExternalNumberDuplicateError = _errorIdentifierService.IdentifyErrorForExternalNumber(e, paymentDto);
                if (isExternalNumberDuplicateError)
                {
                    result.Message = "Платеж не удался. Повторяющийся ExternalNumber";
                    result.StatusCode = StatusCode.DuplicateExternalNumber;
                }
                else
                {
                    result.Message = "Платеж не удался";
                    result.StatusCode = StatusCode.UnableError;
                }
                
                return result;
            }
            catch (Exception e)
            {
                _logger.LogTrace(e, e.Message);
                _logger.LogError("{@Service}. Ошибка: {@Message}. Данные: {@Data}. RequestId: {@RequestId}",
                    _type, e.Message, paymentDto, requestId);
                return new Response
                {
                    Message = "Платеж не удался.",
                    StatusCode = StatusCode.UnableError
                };
            }
        }
    }
}