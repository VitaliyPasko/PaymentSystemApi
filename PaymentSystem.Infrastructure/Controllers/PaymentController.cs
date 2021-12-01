using System;
using System.Threading.Tasks;
using Common.ResponseDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Dto;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Interfaces;

namespace PaymentSystem.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PaymentDto payment)
        {
            try
            {
                StringValues requestId = Request.Headers["RequestId"].ToString();
                var result = await _paymentService.CreatePayment(payment, requestId);
            
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new Response
                {
                    Message = "Неизвестная ошибка.",
                    StatusCode = Common.Enums.StatusCode.UnableError
                });
            }
        }
    }
}