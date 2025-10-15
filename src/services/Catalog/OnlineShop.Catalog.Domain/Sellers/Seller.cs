using OnlineShop.SharedKernel.Entities;

namespace OnlineShop.Catalog.Domain.Sellers;

public sealed class Seller : BaseEntity<Guid>
{
    private Seller() { } // EF
    public string DisplayName { get; private set; } = default!;
    public bool IsActive { get; private set; }

    public static Seller Create(string displayName)
    {
        var s = new Seller
        {
            Id = Guid.NewGuid(),
            DisplayName = displayName.Trim(),
            IsActive = true
        };
        return s;
    }

    public void Deactivate() => IsActive = false;
}
