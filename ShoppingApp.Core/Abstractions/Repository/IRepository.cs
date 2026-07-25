namespace ShoppingApp.Core.Abstractions.Repository;

public interface IRepository<T>
{
    Task<T?> GetById(object id, CancellationToken cancellationToken);
    void Add(T entity);
    void Update(T entity);
    Task Delete(object id, CancellationToken cancellationToken);
}
