using Chaos.Core.Enums;
using Chaos.Core.ValueObjects;

namespace Chaos.Fortune.ValueObjects;

public readonly record struct FortuneState(PlayerId PlayerId)
{
    public FortuneLevel Level { get; init; } = FortuneLevel.Normal;
    public int SuspicionLevel { get; init; } = 0;
    public DateTime LastUpdated { get; init; } = DateTime.UtcNow;
}
