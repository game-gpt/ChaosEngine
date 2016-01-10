using Chaos.Core.ValueObjects;
using Chaos.Ecology.ValueObjects;

namespace Chaos.Ecology.Interfaces;

public interface IPopulationGenePool
{
    string SpeciesName { get; }
    RegionId RegionId { get; }
    IReadOnlyList<Gene> Genes { get; }
    void UpdateGeneFrequency(Gene gene, double newFrequency);
    Gene SampleRandomGene();
}
