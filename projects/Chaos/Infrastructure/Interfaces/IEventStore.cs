using Chaos.Core.Events;

namespace Chaos.Infrastructure.Interfaces;

public interface IEventStore
{
    Task AppendAsync(IDomainEvent @event);
    Task<IEnumerable<IDomainEvent>> GetEventsAsync(Guid aggregateId);
    Task<IEnumerable<IDomainEvent>> GetAllEventsAsync();
}
