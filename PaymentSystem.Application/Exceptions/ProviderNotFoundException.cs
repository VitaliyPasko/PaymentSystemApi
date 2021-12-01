using System;

namespace PaymentSystem.ApplicationLayer.Exceptions
{
    public class ProviderNotFoundException : ApplicationException
    {
        public ProviderNotFoundException(string prefix)
            : base($"Провайдер с таким префиксом: '{prefix}' не найден.")
        {
        }
    }
}