using Common.Enums;

namespace PaymentSystem.ApplicationLayer.Services.Payment.Models
{
    public class Payment
    {
        public string Id { get; set; }
        public string Phone { get; set; }
        public decimal Amount { get; set; }
        public string ExternalNumber { get; set; }
        public ProviderType ProviderType { get; set; }
        public PaymentStatus Status { get; set; }
    }
}