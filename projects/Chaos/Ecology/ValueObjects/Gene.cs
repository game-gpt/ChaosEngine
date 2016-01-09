namespace Chaos.Ecology.ValueObjects;

public readonly record struct Gene(string Name, string Allele, double Frequency)
{
    public string FullName => $"{Name}:{Allele}";
}
