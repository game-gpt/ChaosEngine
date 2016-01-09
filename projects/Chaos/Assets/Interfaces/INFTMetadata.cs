namespace Chaos.Assets.Interfaces;

public interface INFTMetadata
{
    string Name { get; }
    string Description { get; }
    string? ImageUri { get; }
    IReadOnlyDictionary<string, string> Attributes { get; }
    string ToJson();
}
