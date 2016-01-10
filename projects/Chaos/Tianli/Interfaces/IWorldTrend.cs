using Chaos.Tianli.Enums;

namespace Chaos.Tianli.Interfaces;

public interface IWorldTrend
{
    TianliParameterType Type { get; }
    double CurrentValue { get; }
    double MinValue { get; }
    double MaxValue { get; }
    void Update(double delta);
}
