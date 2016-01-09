namespace Chaos.Crafting.Interfaces;

public interface IBlueprint
{
    Guid BlueprintId { get; }
    string Name { get; }
    PlayerId AuthorId { get; }
    IReadOnlyList<MaterialRequirement> MaterialRequirements { get; }
    IReadOnlyDictionary<string, double> CraftParameters { get; }
    IReadOnlyDictionary<string, double> ExpectedOutput { get; }
}
