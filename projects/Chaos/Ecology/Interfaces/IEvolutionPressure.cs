namespace Chaos.Ecology.Interfaces;

public interface IEvolutionPressure
{
    string Name { get; }
    double Intensity { get; }
    void Apply(IPopulationGenePool genePool);
}
