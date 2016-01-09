namespace Chaos.Fortune.Interfaces;

public interface IFortuneSystem
{
    FortuneLevel GetFortuneLevel(PlayerId playerId);
    void DecreaseFortune(PlayerId playerId, int levels);
    void IncreaseFortune(PlayerId playerId, int levels);
    void ApplyFortuneEffects(PlayerId playerId);
}
