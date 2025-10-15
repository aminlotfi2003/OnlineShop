using OnlineShop.SharedKernel.Entities;

namespace OnlineShop.Catalog.Domain.Products.ValueObjects;

public sealed class Money : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; } = default!;

    private Money() { } // EF
    public Money(decimal amount, string currency)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        Currency = string.IsNullOrWhiteSpace(currency) ? "IRR" : currency.Trim().ToUpperInvariant();
        Amount = decimal.Round(amount, 2, MidpointRounding.ToZero);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public override string ToString() => $"{Amount:0.##} {Currency}";
}
