using System.Threading.Tasks;
using PaymentSystem.ApplicationLayer.Data;
using PaymentSystem.ApplicationLayer.Data.Interfaces;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Models;

namespace PaymentSystem.Infrastructure.Repository.PaymentRepository
{
    public class PaymentRepository : IApplicationRepository<Payment>
    {
        private readonly ApplicationDbContext _db;

        public PaymentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Add(Payment payment)
        {
            await _db.Payments.AddAsync(payment);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Payment payment)
        { 
            _db.Payments.Update(payment);
            await _db.SaveChangesAsync();
        }
    }
}