using Chaos.Core.ValueObjects;

namespace Chaos.Networking.Interfaces;

public interface ISessionNode
{
    PlayerId PlayerId { get; }
    bool IsConnected { get; }
    void SendToClient(byte[] data);
    void HandlePlayerMovement(Position newPosition);
    void TransferToRegion(RegionId newRegionId);
}
