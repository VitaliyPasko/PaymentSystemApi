using System.Threading.Tasks;
using PaymentSystem.ApplicationLayer.Services.Interfaces;
using PaymentSystem.Infrastructure.Data;
using PaymentSystem.Infrastructure.Models;

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