using Chaos.Core.ValueObjects;
using Chaos.Ecology.Enums;

namespace Chaos.Ecology.Interfaces;

public interface IEcologySystem
{
    IPopulationGenePool GetGenePool(RegionId regionId, string speciesName);
    void ApplyEvolutionPressure(RegionId regionId, string speciesName, IEvolutionPressure pressure);
    void TriggerMutation(RegionId regionId, string speciesName, MutationType type);
}
