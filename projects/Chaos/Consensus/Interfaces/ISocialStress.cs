using Chaos.Core.Enums;
using Chaos.Core.ValueObjects;

namespace Chaos.Consensus.Interfaces;

public interface ISocialStress
{
    SocialStressType Type { get; }
    double Value { get; }
    double Threshold { get; }
    bool IsThresholdExceeded { get; }
    void Accumulate(double delta, PlayerId contributor);
    void Decay(double factor);
}
