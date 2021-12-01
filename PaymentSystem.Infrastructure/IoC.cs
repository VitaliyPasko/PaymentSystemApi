using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentSystem.ApplicationLayer.Data;
using PaymentSystem.ApplicationLayer.Data.Interfaces;
using PaymentSystem.ApplicationLayer.Services.Payment;
using PaymentSystem.ApplicationLayer.Services.Payment.Interfaces;
using PaymentSystem.ApplicationLayer.Services.Payment.Models;
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

        }
    }
}