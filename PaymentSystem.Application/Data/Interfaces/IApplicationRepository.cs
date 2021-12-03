using System.Threading.Tasks;

namespace PaymentSystem.ApplicationLayer.Data.Interfaces
{
    public interface IApplicationRepository<T> where T : class
    {
        Task Add(T entity);
        Task Update(T entity);
    }
}