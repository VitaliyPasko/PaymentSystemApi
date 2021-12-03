using System;
using System.Threading.Tasks;
using Common.ResponseDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Dto;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Interfaces;

namespace PaymentSystem.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        private readonly Type _type;

        public PaymentController(
            IPaymentService paymentService, 
            ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
            _type = GetType();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PaymentDto payment)
        {
            try
            {
                StringValues requestId = Request.Headers["RequestId"].ToString();
                _logger.LogInformation("{@Controller}.Create Запрос {@Payment}. RequestId: {@RequestId}", 
                    _type, payment, requestId);
                
                var result = await _paymentService.AddPayment(payment, requestId);
            
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogTrace(e, e.Message);
                _logger.LogError("{@Controller}.Create Ошибка: {@Message}. Данные: {@Data}", _type, e.Message, payment);
                
                return Ok(new Response
                {
                    Message = "Неизвестная ошибка.",
                    StatusCode = Common.Enums.StatusCode.UnableError
                });
            }
        }
    }
}