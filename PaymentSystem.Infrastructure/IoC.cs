using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentSystem.ApplicationLayer.Services.Interfaces;
using PaymentSystem.Infrastructure.Data;
using PaymentSystem.Infrastructure.Models;
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
            
            
        }
    }
}