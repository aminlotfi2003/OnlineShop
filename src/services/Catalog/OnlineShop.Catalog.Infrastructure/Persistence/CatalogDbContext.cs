using Microsoft.EntityFrameworkCore;
using OnlineShop.Catalog.Domain.Products;
using OnlineShop.Catalog.Domain.Sellers;
using OnlineShop.Catalog.Infrastructure.Messaging.Outbox;
using OnlineShop.SharedKernel.Messaging;

namespace OnlineShop.Catalog.Infrastructure.Persistence;

public sealed partial class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options)
{
    public DbSet<Seller> Sellers => Set<Seller>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.HasDefaultSchema("catalog");

        b.Entity<Seller>(cfg =>
        {
            cfg.ToTable("Sellers");
            cfg.HasKey(x => x.Id);
            cfg.Property(x => x.DisplayName).IsRequired().HasMaxLength(200);
            cfg.Property(x => x.IsActive).IsRequired();
        });

        b.Entity<Product>(cfg =>
        {
            cfg.ToTable("Products");
            cfg.HasKey(x => x.Id);
            cfg.Property(x => x.SellerId).IsRequired();
            cfg.Property(x => x.Title).IsRequired().HasMaxLength(200);
            cfg.Property(x => x.Description).HasMaxLength(4000);
            cfg.Property(x => x.Sku).IsRequired().HasMaxLength(64);
            cfg.HasIndex(x => x.Sku).IsUnique();

            cfg.Property<byte[]>("RowVersion").IsRowVersion();

            cfg.OwnsOne(x => x.Price, p =>
            {
                p.Property(m => m.Amount).HasColumnName("PriceAmount").HasColumnType("decimal(18,2)");
                p.Property(m => m.Currency).HasColumnName("PriceCurrency").HasMaxLength(3);
            });
        });

        b.Entity<OutboxMessage>(cfg =>
        {
            cfg.ToTable("Outbox");
            cfg.HasKey(x => x.Id);
            cfg.Property(x => x.Type).IsRequired().HasMaxLength(256);
            cfg.Property(x => x.Payload).IsRequired();
            cfg.Property(x => x.CorrelationId);
            cfg.Property(x => x.CausationId);
            cfg.Property(x => x.HeadersJson);
            cfg.HasIndex(x => x.PublishedOn);
            cfg.HasIndex(x => x.OccurredOn);
        });
    }

    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        await DomainEventsToOutbox.EnqueueAsync(this, ct);
        return await base.SaveChangesAsync(ct);
    }
}
