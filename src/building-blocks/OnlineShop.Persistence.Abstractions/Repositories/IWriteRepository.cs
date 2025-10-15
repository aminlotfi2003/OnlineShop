namespace OnlineShop.Persistence.Abstractions.Repositories;

public interface IWriteRepository<T, in TId>
{
    Task AddAsync(T entity, CancellationToken ct = default);
    Task UpdateAsync(T entity, CancellationToken ct = default);
    Task DeleteAsync(T entity, CancellationToken ct = default);
}
