using OnlineShop.Catalog.Application.Products.Dtos;
using OnlineShop.Catalog.Domain.Products;

namespace OnlineShop.Catalog.Application.Common.Mapping;

public static class ProductMapping
{
    public static ProductDto ToDto(this Product entity) =>
        new(
            entity.Id,
            entity.SellerId,
            entity.Title,
            entity.Description,
            entity.Sku,
            entity.Price.Amount,
            entity.Price.Currency,
            entity.Status,
            entity.CreatedAt,
            entity.PublishedAt
        );
}
