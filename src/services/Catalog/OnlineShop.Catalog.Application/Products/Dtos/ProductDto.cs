using OnlineShop.Catalog.Domain.Products.Enums;

namespace OnlineShop.Catalog.Application.Products.Dtos;

public sealed record ProductDto(
    Guid Id,
    Guid SellerId,
    string Title,
    string? Description,
    string Sku,
    decimal Price,
    string Currency,
    ProductStatus Status,
    DateTimeOffset CreatedAt,
    DateTimeOffset? PublishedAt
);
