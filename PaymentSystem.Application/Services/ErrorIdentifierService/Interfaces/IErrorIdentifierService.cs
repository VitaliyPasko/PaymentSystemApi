using Microsoft.EntityFrameworkCore;

namespace PaymentSystem.ApplicationLayer.Services.ErrorIdentifierService.Interfaces
{
    public interface IErrorIdentifierService<T>
    {
        bool IdentifyErrorForExternalNumber(DbUpdateException e, T entity);
    }
}