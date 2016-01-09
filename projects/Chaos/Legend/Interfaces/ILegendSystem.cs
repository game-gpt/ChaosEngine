namespace Chaos.Legend.Interfaces;

public interface ILegendSystem
{
    void RecordAction(PlayerId playerId, LegendDimension dimension, double weight);
    LegendMark GetMark(PlayerId playerId);
    IEnumerable<LegendTitle> GetEligibleTitles(PlayerId playerId);
}
