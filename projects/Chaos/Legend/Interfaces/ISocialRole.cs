namespace Chaos.Legend.Interfaces;

public interface ISocialRole
{
    string Name { get; }
    string Title { get; }
    IReadOnlyDictionary<LegendDimension, double> RequiredThresholds { get; }
    bool IsEligible(ILegendMark mark);
    void ApplyBenefits(PlayerId playerId);
}
