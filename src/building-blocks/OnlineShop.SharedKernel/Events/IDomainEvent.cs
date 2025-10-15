namespace OnlineShop.SharedKernel.Events;

public interface IDomainEvent
{
    DateTimeOffset OccurredOn { get; }
}
