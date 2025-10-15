namespace OnlineShop.Messaging.Abstractions;

public interface IMessage
{
    Guid MessageId { get; }
    string Schema { get; }
    DateTimeOffset OccurredOn { get; }
    string? CorrelationId { get; }
    string? CausationId { get; }
}
