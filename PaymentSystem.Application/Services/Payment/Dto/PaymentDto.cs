using Common.Enums;

namespace PaymentSystem.ApplicationLayer.Services.Payment.Dto
{
    public class PaymentDto
    {
        public string Phone { get; set; }
        public decimal Amount { get; set; }
        public string ExternalNumber { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.New;
    }
}