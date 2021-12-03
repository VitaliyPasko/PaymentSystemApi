using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentSystem.ApplicationLayer.Collections;

namespace PaymentSystem.Infrastructure.Extensions
{
    public static class ProvidersExtensions
    {
        public static void AddProvidersCollection(this IServiceCollection services, IConfiguration configuration)
        {
            var providersDictionary = configuration.GetSection("Providers").GetChildren().ToDictionary(x => x.Key, x => x.Value);
            services.AddSingleton(_ => new ProviderCollection(providersDictionary));
        }
    }
}