namespace Chaos.Karma.Interfaces;

public interface IKarmaSystem
{
    void AccumulateKarma(PlayerId playerId, KarmaType type, double value);
    KarmaVector GetKarmaVector(PlayerId playerId);
    bool CheckThreshold(PlayerId playerId, KarmaType type, double threshold);
}
