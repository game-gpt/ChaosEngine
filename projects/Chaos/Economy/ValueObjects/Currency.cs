namespace Chaos.Economy.ValueObjects;

public readonly record struct Currency(long Gold, long Silver, long Copper)
{
    public long TotalCopper => Gold * 10000 + Silver * 100 + Copper;

    public static Currency FromCopper(long totalCopper)
    {
        var gold = totalCopper / 10000;
        var remainder = totalCopper % 10000;
        var silver = remainder / 100;
        var copper = remainder % 100;
        return new Currency(gold, silver, copper);
    }

    public static Currency operator +(Currency a, Currency b) => FromCopper(a.TotalCopper + b.TotalCopper);
    public static Currency operator -(Currency a, Currency b) => FromCopper(a.TotalCopper - b.TotalCopper);
}
