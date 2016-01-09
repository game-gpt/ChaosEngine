namespace Chaos.Legend.ValueObjects;

public readonly record struct LegendTitle(string Name, string DisplayName, string Description)
{
    public Dictionary<LegendDimension, double> RequiredThresholds { get; init; } = new();
}
