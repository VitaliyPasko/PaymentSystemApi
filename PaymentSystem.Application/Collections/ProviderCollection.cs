using System;
using System.Collections.Generic;
using Common.Enums;

namespace PaymentSystem.ApplicationLayer.Collections
{
    public class ProviderCollection
    {
        private readonly Dictionary<string, string> _providers;

        public ProviderCollection(Dictionary<string, string> providers)
        {
            _providers = providers;
        }

        /// <summary>
        /// Передавая в коллекцию префикс, получаем название провайдера
        /// </summary>
        /// <param name="key">префикс провайдера</param>
        public ProviderType this[string key] => (ProviderType) Enum.Parse(typeof(ProviderType), _providers[key], true);
    }
}