namespace Chaos.Karma.Interfaces;

public interface IKarmaAccumulator
{
    KarmaType Type { get; }
    double CurrentValue { get; }
    void Accumulate(double value);
    void Decay(double factor);
}
