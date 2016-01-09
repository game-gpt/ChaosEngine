namespace Chaos.Crafting.ValueObjects;

public readonly record struct CraftParameter(string Name, double Value, double MinValue, double MaxValue)
{
    public double NormalizedValue => (Value - MinValue) / (MaxValue - MinValue);
}
