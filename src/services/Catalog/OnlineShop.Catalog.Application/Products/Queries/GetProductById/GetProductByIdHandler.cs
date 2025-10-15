using MediatR;
using OnlineShop.Catalog.Application.Common.Mapping;
using OnlineShop.Catalog.Application.Products.Dtos;
using OnlineShop.Catalog.Domain.Products;

namespace OnlineShop.Catalog.Application.Products.Queries.GetProductById;

public sealed class GetProductByIdHandler(IProductRepository repo)
    : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken ct)
    {
        var e = await repo.GetByIdAsync(request.Id, ct) ?? throw new KeyNotFoundException();
        return e.ToDto();
    }
}
