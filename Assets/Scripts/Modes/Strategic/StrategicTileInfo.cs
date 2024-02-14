using System;
using System.Collections.Generic;

public enum StrategicTileType
{
    Grass = 0,
    Forest = 1,
    Mountain = 2,
    Water = 3,
    Field = 4,
    Hills = 5,
    Desert = 6,
    Ocean = 7,
    Snow = 8,
    Ice = 9,
    Lava = 10,
    Volcanic = 11,
    PurpleSwamp = 12,
    SandHills = 13,
    SnowHills = 14,
    Swamp = 15,
    FieldSnow = 16,
    FieldDesert = 17,
    SnowTrees = 18,
    SnowMountain = 19,
    BrokenCliffs = 20,
}

public enum StrategicDoodadType
{
    None = 0,
    BridgeVertical = 1,
    BridgeHorizontal = 2,
    BridgeIntersection = 3,
    VirtualBridgeVertical = 4,
    VirtualBridgeHorizontal = 5,
    VirtualBridgeIntersection = 6,
    SpawnerVagrant = 1001,
    SpawnerSerpents = 1002,
    SpawnerWyvern = 1003,
    SpawnerCompy = 1004,
    SpawnerSharks = 1005,
    SpawnerFeralWolves = 1006,
    SpawnerCake = 1007,
    SpawnerHarvester = 1008,
    SpawnerVoilin = 1009,
    SpawnerBats = 1010,
    SpawnerFrogs = 1011,
    SpawnerDragon = 1012,
    SpawnerDragonfly = 1013,
    SpawnerTwistedVines = 1014,
    SpawnerFairy = 1015,
    SpawnerAnts = 1016,
    SpawnerGryphon = 1017,
    SpawnerSlugs = 1018,
    SpawnerSalamanders = 1019,
    SpawnerMantis = 1020,
    SpawnerEasternDragon = 1021,
    SpawnerCatfish = 1022,
    SpawnerGazelle = 1023,
    SpawnerEarthworm = 1024,
    SpawnerFeralLizards = 1025,
    SpawnerMonitor = 1026,
    SpawnerSchiwardez = 1027,
    SpawnerTerrorbird = 1028,
    SpawnerDratopyr = 1029,
    SpawnerFeralLions = 1030,
    SpawnerGoodra = 1031,
}

public enum MovementType
{
    Ground,
    Water,
    Air,
}

internal static class StrategicTileInfo
{
    private static int[] _grasses = new int[] { 0, 13, 14, 15 };
    private static int[] _forests = new int[] { 1, 16, 17, 18 };
    private static int[] _waters = new int[] { 3, 19, 20, 21 };
    private static int[] _fields = new int[] { 4, 22, 23, 24 };
    private static int[] _sands = new int[] { 6, 25, 27 };
    private static int[] _hills = new int[] { 5, 26 };
    private static int[] _swamps = new int[] { 30, 31, 32 };
    private static int[] _snowFields = new int[] { 33, 34, 35, 36 };
    private static int[] _desertFields = new int[] { 37, 38, 39, 40 };

    internal static List<StrategicTileType> SandFamily = new List<StrategicTileType>() { StrategicTileType.Desert, StrategicTileType.FieldDesert, StrategicTileType.SandHills, StrategicTileType.BrokenCliffs };
    internal static List<StrategicTileType> GrassFamily = new List<StrategicTileType>() { StrategicTileType.Grass, StrategicTileType.Forest, StrategicTileType.Mountain, StrategicTileType.Field, StrategicTileType.Hills };
    internal static List<StrategicTileType> SnowFamily = new List<StrategicTileType>() { StrategicTileType.Snow, StrategicTileType.SnowHills, StrategicTileType.FieldSnow, StrategicTileType.Ice, StrategicTileType.SnowTrees, StrategicTileType.SnowMountain };
    internal static List<StrategicTileType> WaterFamily = new List<StrategicTileType>() { StrategicTileType.Water, StrategicTileType.Ocean };


    private static Noise.OpenSimplexNoise _openSimplexNoise = new Noise.OpenSimplexNoise(155);

    internal static int GetTileType(StrategicTileType type, int x, int y)
    {
        Random rand = new Random((int)(65535 * _openSimplexNoise.Evaluate(16 * x, 16 * y)));

        switch (type)
        {
            case StrategicTileType.Grass:
                return _grasses[rand.Next(_grasses.Length)];
            case StrategicTileType.Forest:
                if (Config.SimpleForests) return _forests[rand.Next(2)];
                return _forests[rand.Next(_forests.Length)];
            case StrategicTileType.Water:
                return _waters[rand.Next(_waters.Length)];
            case StrategicTileType.Field:
                if (Config.SimpleFarms) return _fields[0];
                return _fields[rand.Next(_fields.Length)];
            case StrategicTileType.FieldSnow:
                if (Config.SimpleFarms) return _snowFields[0];
                return _snowFields[rand.Next(_snowFields.Length)];
            case StrategicTileType.FieldDesert:
                if (Config.SimpleFarms) return _desertFields[0];
                return _desertFields[rand.Next(_desertFields.Length)];
            case StrategicTileType.Hills:
                return _hills[rand.Next(_hills.Length)];
            case StrategicTileType.Desert:
                return _sands[rand.Next(_sands.Length)];
            case StrategicTileType.Swamp:
                return _swamps[rand.Next(_swamps.Length)];
            case StrategicTileType.SandHills:
                return 28;
            case StrategicTileType.SnowHills:
                return 29;
            case StrategicTileType.SnowTrees:
                return (int)StrategicTileType.Snow;
            case StrategicTileType.SnowMountain:
                return (int)StrategicTileType.Snow;
            case StrategicTileType.BrokenCliffs:
                return (int)StrategicTileType.Desert;
            default:
                return (int)type;
        }
    }

    internal static int GetObjectTileType(StrategicTileType type, int x, int y)
    {
        Random rand = new Random((int)(65535 * _openSimplexNoise.Evaluate(16 * x, 16 * y)));

        switch (type)
        {
            case StrategicTileType.Forest:
                if (Config.SimpleForests) return 4 + rand.Next(2);
                return 4 + rand.Next(4);
            //case StrategicTileType.field:
            //    if (Config.SimpleFarms)
            //        return fields[0];
            //    return fields[rand.Next(fields.Length)];
            //case StrategicTileType.fieldSnow:
            //    if (Config.SimpleFarms)
            //        return snowFields[0];
            //    return snowFields[rand.Next(snowFields.Length)];
            //case StrategicTileType.fieldDesert:
            //    if (Config.SimpleFarms)
            //        return desertFields[0];
            //    return desertFields[rand.Next(desertFields.Length)];
            case StrategicTileType.Field:
                if (Config.SimpleFarms) return 16;
                return 16 + rand.Next(4);
            case StrategicTileType.FieldSnow:
                if (Config.SimpleFarms) return 8;
                return 8 + rand.Next(4);
            case StrategicTileType.FieldDesert:
                if (Config.SimpleFarms) return 12;
                return 12 + rand.Next(4);
            case StrategicTileType.Hills:
                return 3 - rand.Next(2);
            case StrategicTileType.SandHills:
                return 1;
            case StrategicTileType.SnowHills:
                return 0;
            case StrategicTileType.SnowTrees:
                return 20 + rand.Next(2);
            case StrategicTileType.SnowMountain:
                return 22;
            case StrategicTileType.BrokenCliffs:
                return 23;
            default:
                return -1;
        }
    }

    /// <summary>
    ///     Don't call from within Map editor, use the local
    /// </summary>
    internal static bool CanWalkInto(int x, int y)
    {
        if ((x >= 0 && x < Config.StrategicWorldSizeX && y >= 0 && y < Config.StrategicWorldSizeY) == false) return false;
        if (CanWalkInto(State.World.Tiles[x, y]) == true) return true;
        if (State.World.Doodads != null && State.World.Doodads[x, y] >= StrategicDoodadType.BridgeVertical && State.World.Doodads[x, y] <= StrategicDoodadType.VirtualBridgeIntersection) return true;
        return false;
    }

    internal static bool CanWalkInto(StrategicTileType type)
    {
        switch (type)
        {
            case StrategicTileType.Grass:
            case StrategicTileType.Forest:
            case StrategicTileType.Field:
            case StrategicTileType.FieldDesert:
            case StrategicTileType.FieldSnow:
            case StrategicTileType.Hills:
            case StrategicTileType.Desert:
            case StrategicTileType.Snow:
            case StrategicTileType.Ice:
            case StrategicTileType.Volcanic:
            case StrategicTileType.Swamp:
            case StrategicTileType.PurpleSwamp:
            case StrategicTileType.SandHills:
            case StrategicTileType.SnowHills:
            case StrategicTileType.SnowTrees:
                return true;

            //case StrategicTileType.mountain:
            //case StrategicTileType.water:
            //case StrategicTileType.ocean:
            //case StrategicTileType.lava:
            //case StrategicTileType.brokenCliffs:
            //case StrategicTileType.snowMountain:
            //    return false;
            default:
                return false;
        }
    }

    internal static int WalkCost(int x, int y)
    {
        if ((x >= 0 && x < Config.StrategicWorldSizeX && y >= 0 && y < Config.StrategicWorldSizeY) == false) return 9999;
        if (State.World.Doodads != null && State.World.Doodads[x, y] >= StrategicDoodadType.BridgeVertical && State.World.Doodads[x, y] <= StrategicDoodadType.VirtualBridgeIntersection) return 1;
        return WalkCost(State.World.Tiles[x, y]);
    }

    internal static int WalkCost(StrategicTileType type)
    {
        switch (type)
        {
            case StrategicTileType.Grass:
            case StrategicTileType.Field:
            case StrategicTileType.Desert:
            case StrategicTileType.Snow:
            case StrategicTileType.Volcanic:
            case StrategicTileType.FieldSnow:
            case StrategicTileType.FieldDesert:

                return 1;

            case StrategicTileType.Forest:
            case StrategicTileType.Hills:
            case StrategicTileType.Ice:
            case StrategicTileType.Swamp:
            case StrategicTileType.PurpleSwamp:
            case StrategicTileType.SandHills:
            case StrategicTileType.SnowHills:
            case StrategicTileType.SnowTrees:
                return 2;
            default:
                return 1;
        }
    }
}