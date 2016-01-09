using Chaos.Core.ValueObjects;

namespace Chaos.Consensus.Interfaces;

public interface IContributorGraph
{
    void AddContribution(PlayerId playerId, double weight);
    IReadOnlyDictionary<PlayerId, double> GetTopContributors(int count);
    double GetContribution(PlayerId playerId);
}
