using MediatR;
using OnlineShop.Catalog.Application.Products.Dtos;
using OnlineShop.Catalog.Domain.Products.ValueObjects;
using OnlineShop.Catalog.Domain.Products;
using OnlineShop.Catalog.Domain.Sellers;
using OnlineShop.Catalog.Application.Common.Mapping;
using OnlineShop.Persistence.Abstractions;

namespace OnlineShop.Catalog.Application.Products.Commands.CreateProduct;

public sealed class CreateProductHandler(IProductRepository products, ISellerRepository sellers, IUnitOfWork uow)
    : IRequestHandler<CreateProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken ct)
    {
        var seller = await sellers.GetByIdAsync(request.SellerId, ct)
            ?? throw new InvalidOperationException("Seller not found");

        var existing = await products.GetBySkuAsync(request.Sku, ct);
        if (existing is not null) throw new InvalidOperationException("SKU already exists");

        var product = Product.Create(seller.Id, request.Title, request.Sku, new Money(request.Price, request.Currency), request.Description);
        await products.AddAsync(product, ct);
        await uow.SaveChangesAsync(ct);

        return product.ToDto();
    }
}
