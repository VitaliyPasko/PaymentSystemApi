using Common.Enums;

namespace PaymentSystem.ApplicationLayer.Extensions
{
    public static class StatusCodeMap
    {
        public static PaymentStatus MapToPaymentStatus(this StatusCode code)
        {
            return code switch
            {
                StatusCode.Success => PaymentStatus.New,
                StatusCode.ServiceUnavailable => PaymentStatus.Pending,
                StatusCode.UnableError => PaymentStatus.Error,
                StatusCode.DuplicateExternalNumber => PaymentStatus.Error,
                StatusCode.ProviderNotFound => PaymentStatus.Error,
                StatusCode.ValidationProblem => PaymentStatus.Error,
                _ => PaymentStatus.Error
            };
        }
    }
}