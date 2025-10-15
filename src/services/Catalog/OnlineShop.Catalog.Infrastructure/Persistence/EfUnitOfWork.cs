using OnlineShop.Persistence.Abstractions;

namespace OnlineShop.Catalog.Infrastructure.Persistence;

public sealed class EfUnitOfWork(CatalogDbContext db) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => db.SaveChangesAsync(ct);
}
