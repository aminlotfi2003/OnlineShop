using MediatR;
using OnlineShop.Catalog.Application.Common.Mapping;
using OnlineShop.Catalog.Application.Products.Dtos;
using OnlineShop.Catalog.Domain.Products;
using OnlineShop.Persistence.Abstractions;

namespace OnlineShop.Catalog.Application.Products.Commands.PublishProduct;

public sealed class PublishProductHandler(IProductRepository products, IUnitOfWork uow)
    : IRequestHandler<PublishProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(PublishProductCommand request, CancellationToken ct)
    {
        var entity = await products.GetByIdAsync(request.ProductId, ct) ?? throw new KeyNotFoundException();
        entity.Publish();
        await products.UpdateAsync(entity, ct);
        await uow.SaveChangesAsync(ct);
        return entity.ToDto();
    }
}
