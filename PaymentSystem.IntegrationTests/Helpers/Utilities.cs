using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaymentSystem.ApplicationLayer.Data;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Dto;
using PaymentSystem.ApplicationLayer.Services.ProviderDeterminantService.Interfaces;
using PaymentSystem.IntegrationTests.Fakes.FakeServices;
using PaymentSystemApi;

namespace PaymentSystem.IntegrationTests.Helpers
{
    public static class Utilities
    {
        private static Random _random = new Random();
        private static string _connectionString =
            "Host=localhost;Port=5432;Database=pay-sys-test;Username=postgres;Password=8524";
        public static WebApplicationFactory<Startup> SubstituteOnFakeProviderDeterminantGeneratingDecliningRequestProvider()
        {
            return new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseNpgsql(_connectionString);
                    });
                    
                    var providerDeterminant =
                        services.SingleOrDefault(s => s.ServiceType == typeof(IProviderDeterminantService));
                    services.Remove(providerDeterminant);
                    services
                        .AddTransient<IProviderDeterminantService, FakeProviderDeterminantServiceGeneratingDeclining>();
                });
            });
        }

        public static PaymentDto GetValidPaymentDto()
        {
            return new PaymentDto
            {
                Amount = 777,
                Phone = "7598989885",
                ExternalNumber = Guid.NewGuid().ToString()
            };
        }
    }
}