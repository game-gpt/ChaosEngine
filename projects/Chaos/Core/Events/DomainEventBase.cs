using Chaos.Core.ValueObjects;

namespace Chaos.Core.Events;

public abstract record DomainEventBase(EntityId AggregateId) : IDomainEvent
{
    public Timestamp OccurredOn { get; } = Timestamp.Now;
}
