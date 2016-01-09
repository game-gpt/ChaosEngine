namespace Chaos.Tianli.ValueObjects;

public readonly record struct WorldTrendParameter(TianliParameterType Type, double Value)
{
    public double NormalizedValue => (Value + 1.0) / 2.0;
}
