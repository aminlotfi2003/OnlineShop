using MediatR;
using OnlineShop.Catalog.Application.Products.Dtos;
using OnlineShop.Catalog.Domain.Products.ValueObjects;
using OnlineShop.Catalog.Domain.Products;
using OnlineShop.Persistence.Abstractions;
using OnlineShop.Catalog.Application.Common.Mapping;

namespace OnlineShop.Catalog.Application.Products.Commands.ChangePrice;

public sealed class ChangePriceHandler(IProductRepository products, IUnitOfWork uow)
    : IRequestHandler<ChangePriceCommand, ProductDto>
{
    public async Task<ProductDto> Handle(ChangePriceCommand request, CancellationToken ct)
    {
        var entity = await products.GetByIdAsync(request.ProductId, ct) ?? throw new KeyNotFoundException();
        entity.ChangePrice(new Money(request.Price, request.Currency));
        await products.UpdateAsync(entity, ct);
        await uow.SaveChangesAsync(ct);
        return entity.ToDto();
    }
}
