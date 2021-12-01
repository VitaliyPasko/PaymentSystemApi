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

        public async Task Add(Payment entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
        }
    }
}