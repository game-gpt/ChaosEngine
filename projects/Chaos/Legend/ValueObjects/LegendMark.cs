using Chaos.Core.Enums;
using Chaos.Core.ValueObjects;

namespace Chaos.Legend.ValueObjects;

public readonly record struct LegendMark(PlayerId PlayerId)
{
    public Dictionary<LegendDimension, double> Dimensions { get; } = new();

    public double GetDimensionValue(LegendDimension dimension)
    {
        return Dimensions.TryGetValue(dimension, out var value) ? value : 0.0;
    }

    public void UpdateDimension(LegendDimension dimension, double delta)
    {
        var current = GetDimensionValue(dimension);
        Dimensions[dimension] = Math.Clamp(current + delta, -1.0, 1.0);
    }
}
