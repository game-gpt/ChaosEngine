namespace Chaos.Karma.ValueObjects;

public readonly record struct KarmaThreshold(KarmaType Type, double Value)
{
    public bool IsExceededBy(double karmaValue) => Math.Abs(karmaValue) >= Value;
}
