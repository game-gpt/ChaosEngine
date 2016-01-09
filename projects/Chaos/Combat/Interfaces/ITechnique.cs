namespace Chaos.Combat.Interfaces;

public interface ITechnique
{
    string Name { get; }
    PlayerId DiscovererId { get; }
    IReadOnlyList<ActionPrimitive> ActionSequence { get; }
    ElementalEffect? RequiredElement { get; }
    double ExecutionWindowMs { get; }
    void Execute();
}
