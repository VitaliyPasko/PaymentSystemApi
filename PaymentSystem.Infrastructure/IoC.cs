using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentSystem.ApplicationLayer.Data;
using PaymentSystem.ApplicationLayer.Data.Interfaces;
using PaymentSystem.ApplicationLayer.Services.PaymentService;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Interfaces;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Models;
using PaymentSystem.ApplicationLayer.Services.ProviderDeterminantService;
using PaymentSystem.ApplicationLayer.Services.ProviderDeterminantService.Interfaces;
using PaymentSystem.ApplicationLayer.Services.ProviderService;
using PaymentSystem.ApplicationLayer.Services.ProviderService.Interfaces;
using PaymentSystem.Infrastructure.Extensions;
using PaymentSystem.Infrastructure.Repository.PaymentRepository;

namespace PaymentSystem.Infrastructure
{
    public static class IoC
    {
        public static void AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("DefaultConnectionString")));
            
            //Repositories
            services.AddTransient<IApplicationRepository<Payment>, PaymentRepository>();
            
            //Services
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IProviderService, ProviderService>();
            services.AddTransient<IProviderDeterminantService, ProviderDeterminantService>();
            
            //providers
            services.AddProvidersCollection(configuration);

        }
    }
}