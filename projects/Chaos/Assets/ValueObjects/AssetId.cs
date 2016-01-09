namespace Chaos.Assets.ValueObjects;

public readonly record struct AssetId(Guid Value)
{
    public static AssetId New() => new(Guid.NewGuid());
    public static readonly AssetId Empty = new(Guid.Empty);
}
