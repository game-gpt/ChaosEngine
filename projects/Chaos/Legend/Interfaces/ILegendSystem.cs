using Chaos.Core.Enums;
using Chaos.Core.ValueObjects;
using Chaos.Legend.ValueObjects;

namespace Chaos.Legend.Interfaces;

public interface ILegendSystem
{
    void RecordAction(PlayerId playerId, LegendDimension dimension, double weight);
    LegendMark GetMark(PlayerId playerId);
    IEnumerable<LegendTitle> GetEligibleTitles(PlayerId playerId);
}
