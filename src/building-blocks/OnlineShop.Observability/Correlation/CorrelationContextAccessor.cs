namespace OnlineShop.Observability.Correlation;

public sealed class CorrelationContextAccessor : ICorrelationContextAccessor
{
    public string? CorrelationId { get; set; }
}
