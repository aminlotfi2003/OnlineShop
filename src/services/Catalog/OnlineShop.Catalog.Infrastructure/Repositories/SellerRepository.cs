using Microsoft.EntityFrameworkCore;
using OnlineShop.Catalog.Domain.Sellers;
using OnlineShop.Catalog.Infrastructure.Persistence;

namespace OnlineShop.Catalog.Infrastructure.Repositories;

public sealed class SellerRepository(CatalogDbContext db) : ISellerRepository
{
    public async Task AddAsync(Seller entity, CancellationToken ct = default) => await db.Sellers.AddAsync(entity, ct);
    public Task UpdateAsync(Seller entity, CancellationToken ct = default) { db.Sellers.Update(entity); return Task.CompletedTask; }
    public Task DeleteAsync(Seller entity, CancellationToken ct = default) { db.Sellers.Remove(entity); return Task.CompletedTask; }

    public async Task<Seller?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await db.Sellers.FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<bool> AnyAsync(Func<IQueryable<Seller>, IQueryable<Seller>> scope, CancellationToken ct = default)
        => await scope(db.Sellers.AsQueryable()).AnyAsync(ct);

    public async Task<List<Seller>> ListAsync(Func<IQueryable<Seller>, IQueryable<Seller>> scope, CancellationToken ct = default)
        => await scope(db.Sellers.AsQueryable()).ToListAsync(ct);
}
