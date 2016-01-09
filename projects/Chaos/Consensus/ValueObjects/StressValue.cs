namespace Chaos.Consensus.ValueObjects;

public readonly record struct StressValue(double Value)
{
    public static readonly StressValue Zero = new(0.0);
    public static readonly StressValue Max = new(1.0);
    public static readonly StressValue Min = new(-1.0);

    public StressValue Clamp() => new(Math.Clamp(Value, -1.0, 1.0));
    public bool IsPositive => Value > 0;
    public bool IsNegative => Value < 0;
}
