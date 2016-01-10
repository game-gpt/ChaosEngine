using Chaos.Tianli.Enums;

namespace Chaos.Tianli.Interfaces;

public interface ITianliSystem
{
    IReadOnlyDictionary<TianliParameterType, double> GetParameters();
    void UpdateParameter(TianliParameterType type, double delta);
    void ApplyGlobalEffects();
}
