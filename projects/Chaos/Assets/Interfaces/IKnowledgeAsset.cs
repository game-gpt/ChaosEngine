using Chaos.Assets.Enums;
using Chaos.Assets.ValueObjects;
using Chaos.Core.ValueObjects;

namespace Chaos.Assets.Interfaces;

public interface IKnowledgeAsset
{
    AssetId AssetId { get; }
    string Name { get; }
    PlayerId DiscovererId { get; }
    AssetType Type { get; }
    byte[] GetData();
    bool Verify();
}
