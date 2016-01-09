using Chaos.Core.ValueObjects;

namespace Chaos.Consensus.Interfaces;

public interface IConsensusField
{
    RegionId RegionId { get; }
    IReadOnlyDictionary<SocialStressType, double> StressValues { get; }
    void UpdateStress(SocialStressType type, double delta);
    double GetStress(SocialStressType type);
}
