namespace Chaos.Legend.Interfaces;

public interface ILegendMark
{
    PlayerId PlayerId { get; }
    IReadOnlyDictionary<LegendDimension, double> Dimensions { get; }
    void UpdateDimension(LegendDimension dimension, double delta);
    double GetDimensionValue(LegendDimension dimension);
}
