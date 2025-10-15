using OnlineShop.Persistence.Abstractions.Repositories;

namespace OnlineShop.Catalog.Domain.Sellers;

public interface ISellerRepository : IReadRepository<Seller, Guid>, IWriteRepository<Seller, Guid>
{
}
