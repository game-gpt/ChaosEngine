namespace Chaos.Crafting.Interfaces;

public interface ICraftingSystem
{
    IBlueprint? DiscoverBlueprint(PlayerId playerId, IReadOnlyList<IMaterial> materials, IReadOnlyDictionary<string, double> parameters);
    IBlueprint? GetBlueprint(Guid blueprintId);
    bool CraftItem(PlayerId playerId, IBlueprint blueprint, IReadOnlyList<IMaterial> materials);
}
