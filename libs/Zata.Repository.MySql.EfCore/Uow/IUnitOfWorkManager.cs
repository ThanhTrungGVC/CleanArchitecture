using Microsoft.EntityFrameworkCore;

namespace Zata.Repository.MySql.EfCore.Uow
{
    public interface IUnitOfWorkManager<TContext> where TContext : DbContext
    {
        Task<IUnitOfWork<TContext>> BeginTransactionAsync(bool isRequireNew = false, CancellationToken cancellationToken = default);
    }
}
