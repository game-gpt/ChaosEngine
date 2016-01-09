using Chaos.Core.ValueObjects;

namespace Chaos.Karma.ValueObjects;

public readonly record struct KarmaVector(PlayerId PlayerId)
{
    public Dictionary<KarmaType, double> Values { get; } = new();

    public double GetValue(KarmaType type)
    {
        return Values.TryGetValue(type, out var value) ? value : 0.0;
    }

    public void SetValue(KarmaType type, double value)
    {
        Values[type] = value;
    }
}
