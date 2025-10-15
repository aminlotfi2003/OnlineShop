using OnlineShop.Persistence.Abstractions.Repositories;

namespace OnlineShop.Catalog.Domain.Products;

public interface IProductRepository : IReadRepository<Product, Guid>, IWriteRepository<Product, Guid>
{
    Task<Product?> GetBySkuAsync(string sku, CancellationToken ct = default);
}
