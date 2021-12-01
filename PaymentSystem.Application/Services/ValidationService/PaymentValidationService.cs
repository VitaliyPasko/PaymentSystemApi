using System.Text.RegularExpressions;
using PaymentSystem.ApplicationLayer.Services.ValidationService.Interfaces;

namespace PaymentSystem.ApplicationLayer.Services.ValidationService
{
    public class PaymentValidationService : IPaymentValidationService
    {
        private const string PhoneValidationPattern = @"^[0-9]*[0-9][0-9]*$";
        
        public bool ValidatePhone(string phone)
            => Regex.IsMatch(phone, PhoneValidationPattern);

        public bool ValidateAmount(decimal amount)
            => amount > 0;
    }
}