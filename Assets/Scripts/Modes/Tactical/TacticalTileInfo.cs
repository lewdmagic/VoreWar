public enum TacticalTileType
{
    Grass,
    Tree1,
    Tree2,
    Wall,
    House1,
    House2,
    House3,
    CobbleComplete,
    CobbleHorizontal,
    CobbleVertical,
    CobbleIntersection,
    CobbleVerticalLeftEdge,
    CobbleVerticalRightEdge,
    CobbleIntersectionLeftEdge,
    CobbleIntersectionRightEdge,
    WallStart,

    RockOverTar = 80,
    RockOverSand = 81,
    GrassOverWater = 82,

    VolcanicOverGravel = 83,
    VolcanicOverLava = 84,

    Greengrass = 100,
    GrassBush,
    GrassFlower,
    GrassMediumRock,
    GrassSmallRock,
}

internal static class TacticalTileInfo
{
    internal static int TileCost(Vec2 location)
    {
        var effects = State.GameManager.TacticalMode.ActiveEffects;
        if (effects != null)
        {
            if (effects.TryGetValue(location, out var effect))
            {
                if (effect.Type == TileEffectType.IcePatch) return 3;
            }
        }

        return 1;
    }

    internal static bool CanWalkInto(TacticalTileType type, ActorUnit actor)
    {
        if (type >= (TacticalTileType)2300 && type < (TacticalTileType)2399) return true;
        if (type >= (TacticalTileType)2000 && type < (TacticalTileType)2099) return true;
        if (type >= (TacticalTileType)500 && type < (TacticalTileType)599) return true;
        if (type >= (TacticalTileType)400 && type < (TacticalTileType)499) return true;
        if (type >= (TacticalTileType)300 && type < (TacticalTileType)399) return true;
        if (type >= (TacticalTileType)200 && type < (TacticalTileType)299) return true;
        if (type >= (TacticalTileType)100 && type < (TacticalTileType)199) return true;

        switch (type)
        {
            case TacticalTileType.Grass:
                return true;
            case TacticalTileType.Tree1:
                return actor?.Unit.HasTrait(TraitType.NimbleClimber) ?? false;
            case TacticalTileType.Tree2:
                return actor?.Unit.HasTrait(TraitType.NimbleClimber) ?? false;
            case TacticalTileType.CobbleComplete:
                return true;
            case TacticalTileType.CobbleVertical:
                return true;
            case TacticalTileType.CobbleHorizontal:
                return true;
            case TacticalTileType.CobbleIntersection:
                return true;
            case TacticalTileType.CobbleVerticalLeftEdge:
                return true;
            case TacticalTileType.CobbleVerticalRightEdge:
                return true;
            case TacticalTileType.CobbleIntersectionLeftEdge:
                return true;
            case TacticalTileType.CobbleIntersectionRightEdge:
                return true;
            case TacticalTileType.GrassBush:
            case TacticalTileType.GrassFlower:
            case TacticalTileType.GrassSmallRock:
                return true;
            default:
                return false;
        }
    }
}