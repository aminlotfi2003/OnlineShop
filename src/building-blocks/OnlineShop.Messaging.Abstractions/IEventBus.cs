namespace OnlineShop.Messaging.Abstractions;

public interface IEventBus
{
    Task PublishAsync<TMessage>(TMessage message, IDictionary<string, string>? headers = null, CancellationToken ct = default)
        where TMessage : IMessage;
}
