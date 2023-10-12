namespace Zata.Repository.MySql.EfCore.Uow
{
    public interface IUnitOfWorkManager
    {
        Task<IUnitOfWork> BeginTransactionAsync(bool isRequireNew = false, CancellationToken cancellationToken = default);
    }
}
