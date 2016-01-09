namespace Chaos.Consensus.ValueObjects;

public readonly record struct StressThreshold(double Value)
{
    public bool IsExceededBy(double stressValue) => Math.Abs(stressValue) >= Value;
}
