using Assets.Scripts.Modes.Strategic;
using Noise;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using UnityEngine;

public class WorldGenerator
{
    public struct MapGenArgs
    {
        internal bool UsingNewGenerator;
        internal float WaterPct;
        internal float Hilliness;
        internal float Swampiness;
        internal float ForestPct;
        internal float Temperature;
        internal int AbandonedVillages;
        internal bool Poles;
        internal bool ExcessBridges;
    }

    private StrategicTileType[,] _tiles;
    private Village[] _villages;
    private int[,] _grid;
    private VillageLocation[] _sites;

    private int _villageLocations;

    private List<Vec2I> _usedLocations = new List<Vec2I>();

    private const int ExtraPadding = 3;

    private MapGenArgs _genArgs;

    private class VillageLocation
    {
        public Vec2I Position;
        public int Index;
        public int UtilityScore;
        public Dictionary<Race, int> ScoreForEmpire;
    }

    private class EmpireBuilder
    {
        public Race Race;
        public Village Capital;
        public int RemainingVillages;

        public EmpireBuilder(Race race, Village capital, int remainingVillages)
        {
            Race = race;
            Capital = capital;
            RemainingVillages = remainingVillages;
        }
    }


    public void GenerateWorld(ref StrategicTileType[,] tilesRef, ref Village[] villagesRef, Dictionary<Race, int> teams, MapGenArgs mapGenArgs)
    {
        _villageLocations = Config.MaxVillages + ExtraPadding + mapGenArgs.AbandonedVillages; //Padding to avoid the villages way outside of territory issue
        _genArgs = mapGenArgs;
        GenerateTerrain();
        PlaceVillages();
        AssignVillagesForManyEmpires(teams, mapGenArgs.AbandonedVillages);
        tilesRef = _tiles;
        villagesRef = _villages;
    }

    public void GenerateOnlyTerrain(ref StrategicTileType[,] tilesRef)
    {
        GenerateTerrain();
        tilesRef = _tiles;
    }

    private string VillageName(Race race, int nameIndex) => State.NameGen.GetTownName(race, nameIndex);


    private void GenerateTerrain()
    {
        if (_genArgs.UsingNewGenerator)
        {
            StrategicTerrainGenerator gen = new StrategicTerrainGenerator(_genArgs);
            _tiles = gen.GenerateTerrain();
        }
        else
        {
            _tiles = new StrategicTileType[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];
            SimplexHeightMap();
        }

        //Heightmapper();
    }

    private void SimplexHeightMap()
    {
        double[,] heightmap = new double[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];
        OpenSimplexNoise noise = new OpenSimplexNoise();
        for (int i = 0; i < Config.StrategicWorldSizeX; i++)
        {
            for (int j = 0; j < Config.StrategicWorldSizeY; j++)
            {
                //	heightmap[i, j]=(m_random.nextFloat()+m_random.nextFloat())/2;                
                heightmap[i, j] = (1 + noise.Evaluate(i / 4f, j / 4f)) / 2;
                if (heightmap[i, j] > .35) //To keep water from randomizing too badly
                    heightmap[i, j] += State.Rand.NextDouble() / 4 - .125;
            }
        }

        for (int i = 0; i < Config.StrategicWorldSizeX; i++)
        {
            for (int j = 0; j < Config.StrategicWorldSizeY; j++)
            {
                if (heightmap[i, j] < 0.25)
                {
                    _tiles[i, j] = StrategicTileType.Water;
                }
                else if (heightmap[i, j] < .55)
                {
                    _tiles[i, j] = StrategicTileType.Grass;
                }
                else if (heightmap[i, j] < .56)
                {
                    _tiles[i, j] = StrategicTileType.Mountain;
                }
                else if (heightmap[i, j] < .70)
                {
                    _tiles[i, j] = StrategicTileType.Forest;
                }
                else if (heightmap[i, j] < .81)
                {
                    _tiles[i, j] = StrategicTileType.Grass;
                }
                else if (heightmap[i, j] < 0.85)
                {
                    _tiles[i, j] = StrategicTileType.Hills;
                }
                else if (heightmap[i, j] >= 0.85)
                {
                    _tiles[i, j] = StrategicTileType.Desert;
                }
            }
        }

        for (int i = 1; i < Config.StrategicWorldSizeX - 1; i++)
        {
            for (int j = 1; j < Config.StrategicWorldSizeY - 1; j++)
            {
                if (_tiles[i, j] == StrategicTileType.Desert)
                {
                    if (_tiles[i + 1, j] != StrategicTileType.Desert && _tiles[i - 1, j] != StrategicTileType.Desert && _tiles[i, j + 1] != StrategicTileType.Desert && _tiles[i, j - 1] != StrategicTileType.Desert)
                    {
                        _tiles[i, j] = StrategicTileType.Grass;
                    }
                }
            }
        }
    }


    private void PlaceVillages()
    {
        //The original had a bug where only the diagonal farms counted, so it would place cities with the sides blocked, and it didn't matter
        //I corrected the bug, and had to do a little tweaking to make an occupied farm rare.
        _grid = new int[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];
        for (int i = 0; i < Config.StrategicWorldSizeX; i++)
        {
            for (int j = 0; j < Config.StrategicWorldSizeY; j++)
            {
                if (i != 0 && i != Config.StrategicWorldSizeX - 1 && j != 0 && j != Config.StrategicWorldSizeY - 1)
                {
                    _grid[i, j] = PotentialFarmlandSquares(i, j);
                }
                else
                {
                    _grid[i, j] = 0;
                }
            }
        }

        _sites = new VillageLocation[_villageLocations];

        // need to fill it because it was changed from struct to class 
        for (int i = 0; i < _villageLocations; i++)
        {
            _sites[i] = new VillageLocation();
        }


        for (int i = 0; i < _villageLocations; i++)
        {
            int attempts = 0;
            while (true)
            {
                int value = 8;
                int reduction = attempts / 100 / (i + 1);
                value = 8 - reduction;
                if (value < 4)
                {
                    value = 4;
                }

                if (attempts > 4800) value = -100;
                //pick a random spot
                Vec2I newPos = new Vec2I(Random.Range(0, Config.StrategicWorldSizeX - 4) + 2, Random.Range(0, Config.StrategicWorldSizeY - 4) + 2);

                if (_grid[newPos.X, newPos.Y] >= value)
                {
                    bool pass = true;
                    if (i > 0)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if (_sites[j].Position.GetDistance(newPos) < (Config.StrategicWorldSizeX + Config.StrategicWorldSizeY) / 16 - Mathf.Floor(attempts / 200f))
                            {
                                pass = false;
                            }
                        }
                    }

                    if (pass == true)
                    {
                        _sites[i].UtilityScore = _grid[newPos.X, newPos.Y];
                        _sites[i].Position = newPos;
                        _sites[i].Index = i;
                        break;
                    }
                }

                attempts++;

                if (attempts > 6400)
                {
                    Debug.Log("infinite loop detected, breaking");
                    break;
                }
            }

            //for each, pick the most valuable position
            //reduce the value of all tiles in a 5x5 square centered on the village to 0
            for (int j = _sites[i].Position.X - 2; j < _sites[i].Position.X + 3; j++)
            {
                for (int k = _sites[i].Position.Y - 2; k < _sites[i].Position.Y + 3; k++)
                {
                    if (k != _sites[i].Position.Y && j != _sites[i].Position.X)
                    {
                        _grid[j, k] = 0;
                    }
                }
            }
        }
    }

    private void AssignVillagesForManyEmpires(IReadOnlyDictionary<Race, int> teams, int abandonedVillages)
    {
        //int sides = Config.NumberOfRaces;
        Dictionary<Race, EmpireBuilder> builders = new Dictionary<Race, EmpireBuilder>();
        IReadOnlyList<Race> usedRaces = RaceFuncs.MainRaceEnumerable();

        _villages = new Village[_villageLocations];
        bool[] placed = new bool[_villageLocations];
        VillageLocation site;
        int xMax = Config.StrategicWorldSizeX;
        int yMax = Config.StrategicWorldSizeY;
        Vec2I[] capitalRegions = GetStartingPositions();

        if (Config.PutTeamsTogether)
        {
            Dictionary<Race, int> remapped = new Dictionary<Race, int>();
            int temp = 0;
            foreach (Race race in usedRaces)
            {
                if (Config.CenteredEmpire[race] == false && Config.World.VillagesPerEmpire[race] > 0)
                {
                    remapped[race] = temp;
                    temp++;
                }
            }

            Vec2I[] newRegions = new Vec2I[capitalRegions.Length];
            int nextSlot = 0;
            List<int> usedTeams = new List<int>();
            foreach (Race race in usedRaces)
            {
                if (Config.World.VillagesPerEmpire[race] > 0 && Config.CenteredEmpire[race] == false) usedTeams.Add(teams[race]);
            }

            usedTeams = usedTeams.Distinct().OrderBy(s => s).ToList();
            foreach (int team in usedTeams)
            {
                foreach (Race race in usedRaces)
                {
                    if (team == teams[race] && Config.World.VillagesPerEmpire[race] > 0 && Config.CenteredEmpire[race] == false)
                    {
                        newRegions[remapped[race]] = capitalRegions[nextSlot];
                        nextSlot++;
                    }
                }
            }

            capitalRegions = newRegions;
        }
        else
        {
            for (int i = 0; i < capitalRegions.GetUpperBound(0); i++) //Randomize the order
            {
                int j = State.Rand.Next(i, capitalRegions.GetUpperBound(0) + 1);
                Vec2I temp = capitalRegions[i];
                capitalRegions[i] = capitalRegions[j];
                capitalRegions[j] = temp;
            }
        }


        Dictionary<Race, bool> active = new Dictionary<Race, bool>();
        int region = 0;
        foreach (Race race in usedRaces)
        {
            active[race] = false;
            if (Config.World.VillagesPerEmpire[race] > 0)
            {
                active[race] = true;
                if (Config.CenteredEmpire[race] == false)
                {
                    site = _sites.OrderBy(v => capitalRegions[region].GetDistance(v.Position)).Where(v => placed[v.Index] == false).FirstOrDefault();
                    region++;
                }
                else
                {
                    site = _sites.OrderBy(v => new Vec2I(xMax / 2, yMax / 2).GetDistance(v.Position)).Where(v => placed[v.Index] == false).FirstOrDefault();
                }

                _villages[site.Index] = new Village(VillageName(race, 0), site.Position, site.UtilityScore, race, true);
                placed[site.Index] = true;

                builders[race] = new EmpireBuilder(race, _villages[site.Index], Config.World.VillagesPerEmpire[race] - 1);
            }
            else
            {
                builders[race] = new EmpireBuilder(null, null, 0);
            }
        }

        List<VillageLocation> remainingVillages = new List<VillageLocation>();
        for (int i = 0; i < _villageLocations; i++)
        {
            _sites[i].ScoreForEmpire = new Dictionary<Race, int>();
            if (placed[i] == false)
            {
                foreach (Race race in usedRaces)
                {
                    if (active[race])
                    {
                        _sites[i].ScoreForEmpire[race] = 400 - (int)Mathf.Pow(_sites[i].Position.GetDistance(builders[race].Capital.Position), 2);
                    }
                    else
                    {
                        _sites[i].ScoreForEmpire[race] = 0;
                    }
                }

                Dictionary<Race, int> tempScore = new Dictionary<Race, int>();
                foreach (Race race in usedRaces)
                {
                    tempScore[race] = 0;
                    if (active[race])
                    {
                        tempScore[race] = _sites[i].ScoreForEmpire[race] * usedRaces.Count;
                        foreach (Race race2 in usedRaces)
                        {
                            if (!Equals(race, race2))
                            {
                                VillageLocation loc = _sites[i];
                                int score = loc.ScoreForEmpire[race2];
                                tempScore[race] = tempScore[race] - Mathf.Max(score, 0);
                            }
                        }
                    }
                }

                foreach (Race race in usedRaces)
                {
                    _sites[i].ScoreForEmpire[race] = tempScore[race];
                }

                remainingVillages.Add(_sites[i]);
            }
            else
                CreateFarmland(i);
        }

        Dictionary<Race, int> nameIndex = new Dictionary<Race, int>();
        foreach (Race race in usedRaces)
        {
            nameIndex[race] = 1;
        }

        int side = 0;
        // Iterate over races in a loop
        while (remainingVillages.Count > ExtraPadding)
        {
            Race race = usedRaces[side];
            if (builders.Values.Sum(s => s.RemainingVillages) == 0)
            {
                Debug.Log("Couldn't properly place all the villages");
                break;
            }

            if (builders[race].RemainingVillages > 0)
            {
                VillageLocation newVillage = remainingVillages.OrderByDescending(s => s.ScoreForEmpire[race]).FirstOrDefault();
                int index = newVillage.Index;
                _villages[index] = new Village(VillageName(builders[race].Race, nameIndex[race]), _sites[index].Position, _sites[index].UtilityScore, builders[race].Race, false);
                nameIndex[race] = nameIndex[race] + 1;
                builders[race].RemainingVillages -= 1;
                remainingVillages.Remove(newVillage);
                CreateFarmland(index);
            }
            //Debug.Log(side);

            side = (side + 1) % usedRaces.Count;
        }

        // int side = 0;
        // while (remainingVillages.Count > ExtraPadding)
        // {
        //     if (builders.Values.Sum(s => s.RemainingVillages) == 0)
        //     {
        //         Debug.Log("Couldn't properly place all the villages");
        //         break;
        //     }
        //
        //     if (builders[side].RemainingVillages > 0)
        //     {
        //         VillageLocation newVillage = remainingVillages.OrderByDescending(s => s.ScoreForEmpire[side]).FirstOrDefault();
        //         int index = newVillage.Index;
        //         villages[index] = new Village(VillageName(builders[side].Race, nameIndex[side]), sites[index].Position, sites[index].UtilityScore, builders[side].Race, false);
        //         nameIndex[side] = nameIndex[side] + 1;
        //         builders[side].RemainingVillages -= 1;
        //         remainingVillages.Remove(newVillage);
        //         CreateFarmland(index);
        //     }
        //
        //
        //     side = (side + 1) % sides;
        // }
        for (int i = 0; i < abandonedVillages; i++)
        {
            VillageLocation newVillage = remainingVillages[i];
            int index = newVillage.Index;
            _villages[index] = new Village($"Abandoned town {i + 1}", _sites[index].Position, _sites[index].UtilityScore, Race.Vagrant, false);
            _villages[index].SubtractPopulation(999999);
            CreateFarmland(index);
        }


        _villages = _villages.Where(s => s != null).ToArray();
    }

    private Vec2I[] GetStartingPositions()
    {
        int nonCentralActiveSides = 0;

        foreach (Race race in RaceFuncs.MainRaceEnumerable())
        {
            if (Config.World.VillagesPerEmpire[race] > 0 && Config.CenteredEmpire[race] == false) nonCentralActiveSides++;
        }

        return DrawCirclePoints(nonCentralActiveSides);
    }

    private Vec2I[] DrawCirclePoints(int points)
    {
        float radius = 0.5f * Mathf.Max(Config.StrategicWorldSizeX, Config.StrategicWorldSizeY);
        Vec2I center = new Vec2I(Config.StrategicWorldSizeX / 2, Config.StrategicWorldSizeY / 2);
        Vec2I[] point = new Vec2I[points];
        float slice = 2 * Mathf.PI / points;
        for (int i = 0; i < points; i++)
        {
            float angle = slice * i;
            int newX = (int)(center.X + radius * Mathf.Cos(angle));
            int newY = (int)(center.Y + radius * Mathf.Sin(angle));
            point[i] = new Vec2I(newX, newY);
        }

        return point;
    }

    private int PotentialFarmlandSquares(int x, int y)
    {
        if (StrategicTileInfo.CanWalkInto(_tiles[x, y]) == false)
        {
            return 0;
        }

        int t = 0;
        for (int i = x - 1; i < x + 2; i++)
        {
            for (int j = y - 1; j < y + 2; j++)
            {
                if (!(i == x && y == j))
                {
                    if (StrategicTileInfo.CanWalkInto(_tiles[i, j]))
                    {
                        t++;
                    }
                }
            }
        }

        return t;
    }

    private void CreateFarmland(int i)
    {
        _tiles[_sites[i].Position.X, _sites[i].Position.Y] = StrategicTileType.Grass;
        for (int j = _sites[i].Position.X - 1; j < _sites[i].Position.X + 2; j++)
        {
            for (int k = _sites[i].Position.Y - 1; k < _sites[i].Position.Y + 2; k++)
            {
                if (j == _sites[i].Position.X && k == _sites[i].Position.Y)
                {
                }
                else
                {
                    if (StrategicTileInfo.CanWalkInto(_tiles[j, k]))
                    {
                        var type = _tiles[j, k];
                        if (type == StrategicTileType.Snow || type == StrategicTileType.SnowHills || type == StrategicTileType.Ice)
                            _tiles[j, k] = StrategicTileType.FieldSnow;
                        else if (type == StrategicTileType.Desert || type == StrategicTileType.SandHills)
                            _tiles[j, k] = StrategicTileType.FieldDesert;
                        else
                            _tiles[j, k] = StrategicTileType.Field;
                    }
                }
            }
        }
    }

    public void PlaceMercenaryHouses(int houses)
    {
        if (houses < 0)
        {
            State.World.MercenaryHouses = new MercenaryHouse[0];
            return;
        }

        State.World.MercenaryHouses = new MercenaryHouse[houses];
        int currHouse = 0;
        if (houses == 1 || houses > 5)
        {
            Vec2I center = GrabGoodMercLocation(Config.StrategicWorldSizeX / 2, Config.StrategicWorldSizeY / 2);
            _usedLocations.Add(center);
            State.World.Tiles[center.X, center.Y] = StrategicTileType.Grass;
            State.World.MercenaryHouses[0] = new MercenaryHouse(center);
            currHouse++;
        }

        for (int i = currHouse; i < houses; i++)
        {
            Vec2I point = GrabGoodMercLocation(State.Rand.Next(Config.StrategicWorldSizeX), State.Rand.Next(Config.StrategicWorldSizeY));
            _usedLocations.Add(point);
            State.World.Tiles[point.X, point.Y] = StrategicTileType.Grass;
            State.World.MercenaryHouses[i] = new MercenaryHouse(point);
        }
    }

    public void PlaceGoldMines(int mines)
    {
        if (mines < 0)
        {
            State.World.Claimables = new ClaimableBuilding[0];
            return;
        }

        State.World.Claimables = new ClaimableBuilding[mines];

        for (int i = 0; i < mines; i++)
        {
            Vec2I point = GrabGoodMercLocation(State.Rand.Next(Config.StrategicWorldSizeX), State.Rand.Next(Config.StrategicWorldSizeY));
            _usedLocations.Add(point);
            State.World.Tiles[point.X, point.Y] = StrategicTileType.Grass;
            State.World.Claimables[i] = new GoldMine(point);
        }
    }

    private Vec2I GrabGoodMercLocation(int x, int y)
    {
        for (int newX = x - 1; newX < x + 2; newX++)
        {
            for (int newY = y - 1; newY < y + 2; newY++)
            {
                if (newX < 0 || newX >= Config.StrategicWorldSizeX || newY < 0 || newY >= Config.StrategicWorldSizeY) continue;
                if (_usedLocations.Where(s => s.Matches(newX, newY)).Any()) continue;
                if (StrategicTileInfo.CanWalkInto(State.World.Tiles[newX, newY]) && State.World.Tiles[newX, newY] != StrategicTileType.Field && State.World.Tiles[newX, newY] != StrategicTileType.FieldSnow &&
                    State.World.Tiles[newX, newY] != StrategicTileType.FieldDesert && StrategicUtilities.GetVillageAt(new Vec2I(newX, newY)) == null)
                    return new Vec2I(newX, newY);
            }
        }

        for (int newX = x - 3; newX < x + 4; newX++)
        {
            for (int newY = y - 3; newY < y + 4; newY++)
            {
                if (newX < 0 || newX >= Config.StrategicWorldSizeX || newY < 0 || newY >= Config.StrategicWorldSizeY) continue;
                if (_usedLocations.Where(s => s.Matches(newX, newY)).Any()) continue;
                if (StrategicTileInfo.CanWalkInto(State.World.Tiles[newX, newY]) && State.World.Tiles[newX, newY] != StrategicTileType.Field && State.World.Tiles[newX, newY] != StrategicTileType.FieldSnow &&
                    State.World.Tiles[newX, newY] != StrategicTileType.FieldDesert && StrategicUtilities.GetVillageAt(new Vec2I(newX, newY)) == null)
                    return new Vec2I(newX, newY);
            }
        }

        return new Vec2I(x, y);
    }

    public static void ClearVillagePaths(MapGenArgs args)
    {
        if (StrategicConnectedChecker.AreAllConnected(State.World.Villages, null, false) == true) return;
        Vec2I center = GrabFreeSquareNear(Config.StrategicWorldSizeX / 2, Config.StrategicWorldSizeY / 2);

        foreach (Village village in State.World.Villages)
        {
            var pseudoArmy = new Army(village.Empire, village.Position, village.Side);
            var path = StrategyPathfinder.GetPath(null, pseudoArmy, center, 0, false);
            if (path == null)
            {
                Vec2I currentLoc = new Vec2I(village.Position.X, village.Position.Y);
                while (currentLoc.X != center.X || currentLoc.Y != center.Y)
                {
                    if (currentLoc.X != center.X)
                    {
                        if (currentLoc.X > center.X)
                            currentLoc.X -= 1;
                        else
                            currentLoc.X += 1;
                    }

                    if (currentLoc.Y != center.Y)
                    {
                        if (currentLoc.Y > center.Y)
                            currentLoc.Y -= 1;
                        else
                            currentLoc.Y += 1;
                    }

                    if (StrategicTileInfo.CanWalkInto(State.World.Tiles[currentLoc.X, currentLoc.Y]) == false) State.World.Tiles[currentLoc.X, currentLoc.Y] = StrategicTileType.Grass;
                }

                if (args.ExcessBridges == false) StrategyPathfinder.Initialized = false;
            }
        }

        StrategyPathfinder.Initialized = false;
    }

    private static Vec2I GrabFreeSquareNear(int x, int y)
    {
        for (int newX = x - 2; newX < x + 3; newX++)
        {
            for (int newY = y - 2; newY < y + 3; newY++)
            {
                if (StrategicTileInfo.CanWalkInto(State.World.Tiles[newX, newY])) return new Vec2I(newX, newY);
            }
        }

        return new Vec2I(x, y);
    }
}