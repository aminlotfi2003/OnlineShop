using OnlineShop.Catalog.Infrastructure.Persistence;
using OnlineShop.SharedKernel.Entities;
using OnlineShop.SharedKernel.Messaging;
using System.Text.Json;

namespace OnlineShop.Catalog.Infrastructure.Messaging.Outbox;

public static class DomainEventsToOutbox
{
    public static async Task EnqueueAsync(CatalogDbContext db, CancellationToken ct = default)
    {
        var entries = db.ChangeTracker.Entries<BaseEntity<Guid>>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        foreach (var entity in entries)
        {
            foreach (var ev in entity.DomainEvents)
            {
                var msg = new OutboxMessage
                {
                    Type = ev.GetType().FullName!,
                    Payload = JsonSerializer.Serialize(ev),
                    OccurredOn = ev.OccurredOn
                };
                await db.OutboxMessages.AddAsync(msg, ct);
            }
            entity.ClearDomainEvents();
        }
    }
}
