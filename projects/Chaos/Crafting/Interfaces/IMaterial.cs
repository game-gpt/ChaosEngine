namespace Chaos.Crafting.Interfaces;

public interface IMaterial
{
    Guid MaterialId { get; }
    string MaterialType { get; }
    IReadOnlyDictionary<string, double> Attributes { get; }
    double Quality { get; }
}
