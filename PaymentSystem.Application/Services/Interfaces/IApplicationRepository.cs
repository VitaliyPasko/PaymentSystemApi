using System.Threading.Tasks;

namespace PaymentSystem.ApplicationLayer.Services.Interfaces
{
    public interface IApplicationRepository<T> where T : class
    {
        Task Add(T entity);
    }
}