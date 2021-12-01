namespace PaymentSystem.ApplicationLayer.Services.ValidationService.Interfaces
{
    public interface IPaymentValidationService
    {
        bool ValidatePhone(string phone);
        bool ValidateAmount(decimal amount);
    }
}