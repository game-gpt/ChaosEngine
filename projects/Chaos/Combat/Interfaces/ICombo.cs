using Chaos.Core.ValueObjects;

namespace Chaos.Combat.Interfaces;

public interface ICombo
{
    string Name { get; }
    PlayerId DiscovererId { get; }
    IReadOnlyList<ITechnique> Techniques { get; }
    double DamageMultiplier { get; }
    void Execute();
}
