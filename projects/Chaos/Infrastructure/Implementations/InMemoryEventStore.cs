using Chaos.Core.Events;
using Chaos.Infrastructure.Interfaces;

namespace Chaos.Infrastructure.Implementations;

public class InMemoryEventStore : IEventStore
{
    private readonly List<IDomainEvent> _events = new();

    public Task AppendAsync(IDomainEvent @event)
    {
        _events.Add(@event);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<IDomainEvent>> GetEventsAsync(Guid aggregateId)
    {
        var events = _events.Where(e => e.AggregateId.Value == aggregateId);
        return Task.FromResult(events);
    }

    public Task<IEnumerable<IDomainEvent>> GetAllEventsAsync()
    {
        return Task.FromResult(_events.AsEnumerable());
    }
}
