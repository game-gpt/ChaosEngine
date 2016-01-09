namespace Chaos.Combat.Interfaces;

public interface ICombatSystem
{
    void ExecuteTechnique(PlayerId playerId, ITechnique technique);
    void ExecuteCombo(PlayerId playerId, ICombo combo);
    bool ValidateTechnique(PlayerId playerId, ITechnique technique);
}
