using OnlineShop.Catalog.Domain.Products.Enums;
using OnlineShop.Catalog.Domain.Products.ValueObjects;
using OnlineShop.SharedKernel.Entities;
using OnlineShop.SharedKernel.Events;

namespace OnlineShop.Catalog.Domain.Products;

public sealed class Product : BaseEntity<Guid>
{
    private Product() { } // EF

    public Guid SellerId { get; private set; }
    public string Title { get; private set; } = default!;
    public string? Description { get; private set; }
    public string Sku { get; private set; } = default!;
    public Money Price { get; private set; } = default!;
    public ProductStatus Status { get; private set; } = ProductStatus.Draft;
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? PublishedAt { get; private set; }
    public byte[] RowVersion { get; private set; } = default!; // concurrency

    public static Product Create(Guid sellerId, string title, string sku, Money price, string? description)
    {
        if (sellerId == Guid.Empty) throw new ArgumentException("sellerId");
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("title");
        if (string.IsNullOrWhiteSpace(sku)) throw new ArgumentException("sku");

        return new Product
        {
            Id = Guid.NewGuid(),
            SellerId = sellerId,
            Title = title.Trim(),
            Sku = sku.Trim().ToUpperInvariant(),
            Price = price,
            Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim(),
            Status = ProductStatus.Draft,
            CreatedAt = DateTimeOffset.UtcNow
        };
    }

    public void Publish()
    {
        if (Status == ProductStatus.Published) return;
        Status = ProductStatus.Published;
        PublishedAt = DateTimeOffset.UtcNow;
        Raise(new ProductPublishedDomainEvent(Id, SellerId, Title, Price.Amount, Price.Currency, PublishedAt.Value));
    }

    public void ChangePrice(Money newPrice)
    {
        if (newPrice.Amount == Price.Amount && newPrice.Currency == Price.Currency) return;
        Price = newPrice;
        Raise(new ProductPriceChangedDomainEvent(Id, newPrice.Amount, newPrice.Currency, DateTimeOffset.UtcNow));
    }
}

public sealed record ProductPublishedDomainEvent(Guid ProductId, Guid SellerId, string Title, decimal Price, string Currency, DateTimeOffset PublishedAt) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}

public sealed record ProductPriceChangedDomainEvent(Guid ProductId, decimal Price, string Currency, DateTimeOffset ChangedAt) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}
