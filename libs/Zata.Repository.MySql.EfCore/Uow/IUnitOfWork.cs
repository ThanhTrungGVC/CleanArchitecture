using Microsoft.EntityFrameworkCore.Storage;

namespace Zata.Repository.MySql.EfCore.Uow
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<IDbContextTransaction> BeginTransactionAsync(bool isRequireNew = false, CancellationToken cancellationToken = default);

        Task CommitAsync(CancellationToken cancellationToken = default);

        Task RollbackAsync(CancellationToken cancellationToken = default);

        Task CreateSavepointAsync(string name, CancellationToken cancellationToken = default);
        Task RollbackToSavepointAsync(string name, CancellationToken cancellationToken = default);
        Task ReleaseSavepointAsync(string name, CancellationToken cancellationToken = default);
    }
}
