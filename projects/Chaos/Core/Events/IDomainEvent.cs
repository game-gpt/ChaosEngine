using Chaos.Core.ValueObjects;

namespace Chaos.Core.Events;

public interface IDomainEvent
{
    EntityId AggregateId { get; }
    Timestamp OccurredOn { get; }
}
