namespace ShoppingApp.Core.Abstractions.Repository;

public interface IUnitOfWork
{
    Task Commit(CancellationToken cancellationToken);
}
