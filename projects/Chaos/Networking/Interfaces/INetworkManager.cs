namespace Chaos.Networking.Interfaces;

public interface INetworkManager
{
    void Start();
    void Stop();
    IAreaNode GetAreaNode(RegionId regionId);
    ISessionNode GetSessionNode(PlayerId playerId);
    void BroadcastMessage(NetworkMessage message, RegionId regionId);
}
