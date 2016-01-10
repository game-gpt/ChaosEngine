using Chaos.Tianli.Enums;

namespace Chaos.Tianli.Interfaces;

public interface ICatalystEvent
{
    string Name { get; }
    string Description { get; }
    IReadOnlyDictionary<TianliParameterType, double> ParameterImpacts { get; }
    void Execute();
}
