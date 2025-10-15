namespace OnlineShop.Observability.Correlation;

public interface ICorrelationContextAccessor
{
    string? CorrelationId { get; set; }
}
