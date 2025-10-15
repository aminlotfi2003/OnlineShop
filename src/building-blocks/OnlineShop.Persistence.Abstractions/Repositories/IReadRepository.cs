namespace OnlineShop.Persistence.Abstractions.Repositories;

public interface IReadRepository<T, in TId>
{
    Task<T?> GetByIdAsync(TId id, CancellationToken ct = default);
    Task<bool> AnyAsync(Func<IQueryable<T>, IQueryable<T>> scope, CancellationToken ct = default);
    Task<List<T>> ListAsync(Func<IQueryable<T>, IQueryable<T>> scope, CancellationToken ct = default);
}
