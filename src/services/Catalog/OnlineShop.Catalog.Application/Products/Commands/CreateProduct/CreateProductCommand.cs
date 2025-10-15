using MediatR;
using OnlineShop.Catalog.Application.Products.Dtos;

namespace OnlineShop.Catalog.Application.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(Guid SellerId, string Title, string? Description, string Sku, decimal Price, string Currency = "IRR")
    : IRequest<ProductDto>;
