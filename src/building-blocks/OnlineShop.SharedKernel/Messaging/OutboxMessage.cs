namespace OnlineShop.SharedKernel.Messaging;

public sealed class OutboxMessage
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Type { get; init; } = default!;
    public string Payload { get; init; } = default!;
    public DateTimeOffset OccurredOn { get; init; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? PublishedOn { get; set; }
    public string? LastError { get; set; }
    public int RetryCount { get; set; }
    public string? CorrelationId { get; init; }
    public string? CausationId { get; init; }
    public string? HeadersJson { get; init; }
}
