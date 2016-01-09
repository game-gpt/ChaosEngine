namespace Chaos.Assets.Interfaces;

public interface IAssetRecord
{
    AssetId Id { get; }
    AssetType Type { get; }
    PlayerId OwnerId { get; }
    DateTime CreatedAt { get; }
    INFTMetadata Metadata { get; }
    void Transfer(PlayerId newOwner);
}
