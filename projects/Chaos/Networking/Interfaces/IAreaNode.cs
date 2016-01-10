using Chaos.Core.ValueObjects;

namespace Chaos.Networking.Interfaces;

public interface IAreaNode
{
    RegionId RegionId { get; }
    void ProcessAction(PlayerId playerId, byte[] actionData);
    void UpdateConsensusField();
    void CheckEmergenceThreshold();
}
