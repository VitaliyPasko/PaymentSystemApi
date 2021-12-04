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
        private static Random _random = new ();
        private static string _connectionString =
            "Host=localhost;Port=5432;Database=pay-sys-test;Username=postgres;Password=8524";
        
        public static WebApplicationFactory<Startup> SubstituteDbOnTestDb()
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
                });
            });
        }
        
        public static WebApplicationFactory<Startup> SubstituteOnFakeDecliningRequestProvider()
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
        
        public static WebApplicationFactory<Startup> SubstituteOnFakeUnavailableRequestProvider()
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
                        .AddTransient<IProviderDeterminantService, FakeProviderDeterminantServiceGeneratingUnavailable>();
                });
            });
        }

        public static PaymentDto GetValidPaymentDto()
        {
            return new PaymentDto
            {
                Amount = 777,
                Phone = "7055555555",
                ExternalNumber = Guid.NewGuid().ToString()
            };
        }
        
        public static PaymentDto GetPaymentWithInvalidValidProvider()
        {
            return new PaymentDto
            {
                Amount = 777,
                Phone = "9998989885",
                ExternalNumber = Guid.NewGuid().ToString()
            };
        }
        
        public static PaymentDto GetPaymentWithInvalidValidPhone()
        {
            return new PaymentDto
            {
                Amount = 777,
                Phone = "+77778989885",
                ExternalNumber = Guid.NewGuid().ToString()
            };
        }
        
        public static PaymentDto GetPaymentWithInvalidValidAmount()
        {
            return new PaymentDto
            {
                
                Amount = _random.Next(int.MinValue, 1),
                Phone = "77778989885",
                ExternalNumber = Guid.NewGuid().ToString()
            };
        }
        
        public static PaymentDto GetPaymentWith777Prefix()
        {
            return new PaymentDto
            {
                Amount = 555.5M,
                Phone = "7775555555",
                ExternalNumber = Guid.NewGuid().ToString()
            };
        }
        
        public static PaymentDto GetPaymentWith705Prefix()
        {
            return new PaymentDto
            {
                Amount = 555.5M,
                Phone = "7055555555",
                ExternalNumber = Guid.NewGuid().ToString()
            };
        }
        
        public static PaymentDto GetPaymentWith701Prefix()
        {
            return new PaymentDto
            {
                Amount = 555.5M,
                Phone = "7015555555",
                ExternalNumber = Guid.NewGuid().ToString()
            };
        }
        
        public static PaymentDto GetPaymentWith708Prefix()
        {
            return new PaymentDto
            {
                Amount = 555.5M,
                Phone = "7085555555",
                ExternalNumber = Guid.NewGuid().ToString()
            };
        }
        
        public static PaymentDto GetPaymentWith700Prefix()
        {
            return new PaymentDto
            {
                Amount = 555.5M,
                Phone = "7005555555",
                ExternalNumber = Guid.NewGuid().ToString()
            };
        }
        
        public static PaymentDto GetPaymentWith707Prefix()
        {
            return new PaymentDto
            {
                Amount = 555.5M,
                Phone = "7075555555",
                ExternalNumber = Guid.NewGuid().ToString()
            };
        }
        
        public static PaymentDto GetPaymentWith747Prefix()
        {
            return new PaymentDto
            {
                Amount = 555.5M,
                Phone = "7475555555",
                ExternalNumber = Guid.NewGuid().ToString()
            };
        }
    }
}