using Chaos.Core.ValueObjects;
using Chaos.Fortune.Enums;

namespace Chaos.Fortune.Interfaces;

public interface ICurseMechanism
{
    CurseType Type { get; }
    string NarrativeName { get; }
    void Apply(PlayerId playerId);
    void Remove(PlayerId playerId);
    bool IsActive(PlayerId playerId);
}
