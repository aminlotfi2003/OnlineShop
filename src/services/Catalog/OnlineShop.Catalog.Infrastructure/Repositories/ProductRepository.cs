using Microsoft.EntityFrameworkCore;
using OnlineShop.Catalog.Domain.Products;
using OnlineShop.Catalog.Infrastructure.Persistence;

namespace OnlineShop.Catalog.Infrastructure.Repositories;

public sealed class ProductRepository(CatalogDbContext db)
    : IProductRepository
{
    public async Task AddAsync(Product entity, CancellationToken ct = default) => await db.Products.AddAsync(entity, ct);
    public Task UpdateAsync(Product entity, CancellationToken ct = default) { db.Products.Update(entity); return Task.CompletedTask; }
    public Task DeleteAsync(Product entity, CancellationToken ct = default) { db.Products.Remove(entity); return Task.CompletedTask; }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await db.Products.FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<Product?> GetBySkuAsync(string sku, CancellationToken ct = default)
        => await db.Products.FirstOrDefaultAsync(x => x.Sku == sku, ct);

    public async Task<bool> AnyAsync(Func<IQueryable<Product>, IQueryable<Product>> scope, CancellationToken ct = default)
        => await scope(db.Products.AsQueryable()).AnyAsync(ct);

    public async Task<List<Product>> ListAsync(Func<IQueryable<Product>, IQueryable<Product>> scope, CancellationToken ct = default)
        => await scope(db.Products.AsQueryable()).ToListAsync(ct);
}
