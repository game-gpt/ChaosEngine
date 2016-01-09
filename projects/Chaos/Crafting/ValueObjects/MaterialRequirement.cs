namespace Chaos.Crafting.ValueObjects;

public readonly record struct MaterialRequirement(string MaterialType, double MinQuality, int Quantity)
{
}
