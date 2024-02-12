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

    StrategicTileType[,] tiles;
    Village[] villages;
    int[,] grid;
    VillageLocation[] sites;

    int villageLocations;

    List<Vec2i> usedLocations = new List<Vec2i>();

    const int ExtraPadding = 3;

    MapGenArgs genArgs;

    class VillageLocation
    {
        public Vec2i Position;
        public int Index;
        public int UtilityScore;
        public Dictionary<Race, int> ScoreForEmpire;
    }

    class EmpireBuilder
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
        villageLocations = Config.MaxVillages + ExtraPadding + mapGenArgs.AbandonedVillages; //Padding to avoid the villages way outside of territory issue
        genArgs = mapGenArgs;
        GenerateTerrain();
        PlaceVillages();
        AssignVillagesForManyEmpires(teams, mapGenArgs.AbandonedVillages);
        tilesRef = tiles;
        villagesRef = villages;
    }

    public void GenerateOnlyTerrain(ref StrategicTileType[,] tilesRef)
    {
        GenerateTerrain();
        tilesRef = tiles;
    }

    string VillageName(Race race, int nameIndex) => State.NameGen.GetTownName(race, nameIndex);


    void GenerateTerrain()
    {
        if (genArgs.UsingNewGenerator)
        {
            StrategicTerrainGenerator gen = new StrategicTerrainGenerator(genArgs);
            tiles = gen.GenerateTerrain();
        }
        else
        {
            tiles = new StrategicTileType[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];
            SimplexHeightMap();
        }

        //Heightmapper();
    }

    void SimplexHeightMap()
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
                    heightmap[i, j] += (State.Rand.NextDouble() / 4) - .125;
            }
        }
        for (int i = 0; i < Config.StrategicWorldSizeX; i++)
        {
            for (int j = 0; j < Config.StrategicWorldSizeY; j++)
            {
                if (heightmap[i, j] < 0.25)
                {
                    tiles[i, j] = StrategicTileType.water;
                }
                else if (heightmap[i, j] < .55)
                {
                    tiles[i, j] = StrategicTileType.grass;
                }
                else if (heightmap[i, j] < .56)
                {
                    tiles[i, j] = StrategicTileType.mountain;
                }
                else if (heightmap[i, j] < .70)
                {
                    tiles[i, j] = StrategicTileType.forest;
                }
                else if (heightmap[i, j] < .81)
                {
                    tiles[i, j] = StrategicTileType.grass;
                }
                else if (heightmap[i, j] < 0.85)
                {
                    tiles[i, j] = StrategicTileType.hills;
                }
                else if (heightmap[i, j] >= 0.85)
                {
                    tiles[i, j] = StrategicTileType.desert;
                }
            }
        }

        for (int i = 1; i < Config.StrategicWorldSizeX - 1; i++)
        {
            for (int j = 1; j < Config.StrategicWorldSizeY - 1; j++)
            {
                if (tiles[i, j] == StrategicTileType.desert)
                {
                    if (tiles[i + 1, j] != StrategicTileType.desert && tiles[i - 1, j] != StrategicTileType.desert && tiles[i, j + 1] != StrategicTileType.desert && tiles[i, j - 1] != StrategicTileType.desert)
                    {
                        tiles[i, j] = StrategicTileType.grass;
                    }
                }
            }
        }
    }



    void PlaceVillages()
    {
        //The original had a bug where only the diagonal farms counted, so it would place cities with the sides blocked, and it didn't matter
        //I corrected the bug, and had to do a little tweaking to make an occupied farm rare.
        grid = new int[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];
        for (int i = 0; i < Config.StrategicWorldSizeX; i++)
        {
            for (int j = 0; j < Config.StrategicWorldSizeY; j++)
            {
                if (i != 0 && i != Config.StrategicWorldSizeX - 1 && j != 0 && j != Config.StrategicWorldSizeY - 1)
                {
                    grid[i, j] = PotentialFarmlandSquares(i, j);
                }
                else
                {
                    grid[i, j] = 0;
                }

            }
        }
        sites = new VillageLocation[villageLocations];

        // need to fill it because it was changed from struct to class 
        for (int i = 0; i < villageLocations; i++)
        {
            sites[i] = new VillageLocation();
        }
        
        
        for (int i = 0; i < villageLocations; i++)
        {
            int attempts = 0;
            while (true)
            {
                int value = 8;
                int reduction = attempts / 100 / (i + 1);
                value = 8 - reduction;
                if (value < 4) { value = 4; }
                if (attempts > 4800)
                    value = -100;
                //pick a random spot
                Vec2i newPos = new Vec2i(Random.Range(0, Config.StrategicWorldSizeX - 4) + 2, Random.Range(0, Config.StrategicWorldSizeY - 4) + 2);

                if (grid[newPos.x, newPos.y] >= value)
                {
                    bool pass = true;
                    if (i > 0)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if (sites[j].Position.GetDistance(newPos) < ((Config.StrategicWorldSizeX + Config.StrategicWorldSizeY) / 16) - Mathf.Floor(attempts / 200f))
                            {
                                pass = false;
                            }
                        }
                    }
                    if (pass == true)
                    {
                        sites[i].UtilityScore = grid[newPos.x, newPos.y];
                        sites[i].Position = newPos;
                        sites[i].Index = i;
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
            for (int j = sites[i].Position.x - 2; j < sites[i].Position.x + 3; j++)
            {
                for (int k = sites[i].Position.y - 2; k < sites[i].Position.y + 3; k++)
                {
                    if (k != sites[i].Position.y && j != sites[i].Position.x)
                    {
                        grid[j, k] = 0;
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
        
        villages = new Village[villageLocations];
        bool[] placed = new bool[villageLocations];
        VillageLocation site;
        int xMax = Config.StrategicWorldSizeX;
        int yMax = Config.StrategicWorldSizeY;
        Vec2i[] capitalRegions = GetStartingPositions();

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
            Vec2i[] newRegions = new Vec2i[capitalRegions.Length];
            int nextSlot = 0;
            List<int> usedTeams = new List<int>();
            foreach (Race race in usedRaces)
            {
                if (Config.World.VillagesPerEmpire[race] > 0 && Config.CenteredEmpire[race] == false)
                    usedTeams.Add(teams[race]);
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
                Vec2i temp = capitalRegions[i];
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
                    site = sites.OrderBy(v => capitalRegions[region].GetDistance(v.Position)).Where(v => placed[v.Index] == false).FirstOrDefault();
                    region++;
                }
                else
                {
                    site = sites.OrderBy(v => new Vec2i(xMax / 2, yMax / 2).GetDistance(v.Position)).Where(v => placed[v.Index] == false).FirstOrDefault();
                }

                villages[site.Index] = new Village(VillageName(race, 0), site.Position, site.UtilityScore, race, true);
                placed[site.Index] = true;

                builders[race] = new EmpireBuilder(race, villages[site.Index], Config.World.VillagesPerEmpire[race] - 1);
            }
            else
            {
                builders[race] = new EmpireBuilder(null, null, 0);
            }
        }

        List<VillageLocation> remainingVillages = new List<VillageLocation>();
        for (int i = 0; i < villageLocations; i++)
        {
            sites[i].ScoreForEmpire = new Dictionary<Race, int>();
            if (placed[i] == false)
            {
                foreach (Race race in usedRaces)
                {
                    if (active[race])
                    {
                        sites[i].ScoreForEmpire[race] = 400 - (int)Mathf.Pow(sites[i].Position.GetDistance(builders[race].Capital.Position), 2);
                    }
                    else
                    {
                        sites[i].ScoreForEmpire[race] = 0;
                    }
                }
                Dictionary<Race, int> tempScore = new Dictionary<Race, int>();
                foreach (Race race in usedRaces)
                {
                    tempScore[race] = 0;
                    if (active[race])
                    {
                        tempScore[race] = sites[i].ScoreForEmpire[race] * usedRaces.Count;
                        foreach (Race race2 in usedRaces)
                        {
                            if (!Equals(race, race2))
                            {
                                VillageLocation loc = sites[i];
                                int score = loc.ScoreForEmpire[race2];
                                tempScore[race] = tempScore[race] - Mathf.Max(score, 0);
                            }
                        }
                    }
                }
                foreach (Race race in usedRaces)
                {
                    sites[i].ScoreForEmpire[race] = tempScore[race];
                }
                remainingVillages.Add(sites[i]);
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
                villages[index] = new Village(VillageName(builders[race].Race, nameIndex[race]), sites[index].Position, sites[index].UtilityScore, builders[race].Race, false);
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
            villages[index] = new Village($"Abandoned town {i + 1}", sites[index].Position, sites[index].UtilityScore, Race.Vagrant, false);
            villages[index].SubtractPopulation(999999);
            CreateFarmland(index);
        }


        villages = villages.Where(s => s != null).ToArray();
    }

    Vec2i[] GetStartingPositions()
    {
        int nonCentralActiveSides = 0;

        foreach (Race race in RaceFuncs.MainRaceEnumerable())
        {
            if (Config.World.VillagesPerEmpire[race] > 0 && Config.CenteredEmpire[race] == false)
                nonCentralActiveSides++;
        }
        return DrawCirclePoints(nonCentralActiveSides);
    }

    Vec2i[] DrawCirclePoints(int points)
    {
        float radius = 0.5f * Mathf.Max(Config.StrategicWorldSizeX, Config.StrategicWorldSizeY);
        Vec2i center = new Vec2i(Config.StrategicWorldSizeX / 2, Config.StrategicWorldSizeY / 2);
        Vec2i[] point = new Vec2i[points];
        float slice = 2 * Mathf.PI / points;
        for (int i = 0; i < points; i++)
        {
            float angle = slice * i;
            int newX = (int)(center.x + radius * Mathf.Cos(angle));
            int newY = (int)(center.y + radius * Mathf.Sin(angle));
            point[i] = new Vec2i(newX, newY);
        }
        return point;
    }

    int PotentialFarmlandSquares(int x, int y)
    {
        if (StrategicTileInfo.CanWalkInto(tiles[x, y]) == false)
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
                    if (StrategicTileInfo.CanWalkInto(tiles[i, j]))
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
        tiles[sites[i].Position.x, sites[i].Position.y] = StrategicTileType.grass;
        for (int j = sites[i].Position.x - 1; j < sites[i].Position.x + 2; j++)
        {
            for (int k = sites[i].Position.y - 1; k < sites[i].Position.y + 2; k++)
            {
                if (j == sites[i].Position.x && k == sites[i].Position.y)
                {

                }
                else
                {
                    if (StrategicTileInfo.CanWalkInto(tiles[j, k]))
                    {
                        var type = tiles[j, k];
                        if (type == StrategicTileType.snow || type == StrategicTileType.snowHills || type == StrategicTileType.ice)
                            tiles[j, k] = StrategicTileType.fieldSnow;
                        else if (type == StrategicTileType.desert || type == StrategicTileType.sandHills)
                            tiles[j, k] = StrategicTileType.fieldDesert;
                        else
                            tiles[j, k] = StrategicTileType.field;
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
            Vec2i center = GrabGoodMercLocation(Config.StrategicWorldSizeX / 2, Config.StrategicWorldSizeY / 2);
            usedLocations.Add(center);
            State.World.Tiles[center.x, center.y] = StrategicTileType.grass;
            State.World.MercenaryHouses[0] = new MercenaryHouse(center);
            currHouse++;
        }

        for (int i = currHouse; i < houses; i++)
        {
            Vec2i point = GrabGoodMercLocation(State.Rand.Next(Config.StrategicWorldSizeX), State.Rand.Next(Config.StrategicWorldSizeY));
            usedLocations.Add(point);
            State.World.Tiles[point.x, point.y] = StrategicTileType.grass;
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
            Vec2i point = GrabGoodMercLocation(State.Rand.Next(Config.StrategicWorldSizeX), State.Rand.Next(Config.StrategicWorldSizeY));
            usedLocations.Add(point);
            State.World.Tiles[point.x, point.y] = StrategicTileType.grass;
            State.World.Claimables[i] = new GoldMine(point);
        }

    }

    private Vec2i GrabGoodMercLocation(int x, int y)
    {
        for (int newX = x - 1; newX < x + 2; newX++)
        {
            for (int newY = y - 1; newY < y + 2; newY++)
            {
                if (newX < 0 || newX >= Config.StrategicWorldSizeX || newY < 0 || newY >= Config.StrategicWorldSizeY)
                    continue;
                if (usedLocations.Where(s => s.Matches(newX, newY)).Any())
                    continue;
                if (StrategicTileInfo.CanWalkInto(State.World.Tiles[newX, newY]) && State.World.Tiles[newX, newY] != StrategicTileType.field && State.World.Tiles[newX, newY] != StrategicTileType.fieldSnow &&
                    State.World.Tiles[newX, newY] != StrategicTileType.fieldDesert && StrategicUtilities.GetVillageAt(new Vec2i(newX, newY)) == null)
                    return new Vec2i(newX, newY);
            }
        }

        for (int newX = x - 3; newX < x + 4; newX++)
        {
            for (int newY = y - 3; newY < y + 4; newY++)
            {
                if (newX < 0 || newX >= Config.StrategicWorldSizeX || newY < 0 || newY >= Config.StrategicWorldSizeY)
                    continue;
                if (usedLocations.Where(s => s.Matches(newX, newY)).Any())
                    continue;
                if (StrategicTileInfo.CanWalkInto(State.World.Tiles[newX, newY]) && State.World.Tiles[newX, newY] != StrategicTileType.field && State.World.Tiles[newX, newY] != StrategicTileType.fieldSnow &&
                    State.World.Tiles[newX, newY] != StrategicTileType.fieldDesert && StrategicUtilities.GetVillageAt(new Vec2i(newX, newY)) == null)
                    return new Vec2i(newX, newY);
            }
        }
        return new Vec2i(x, y);
    }

    public static void ClearVillagePaths(MapGenArgs args)
    {
        if (StrategicConnectedChecker.AreAllConnected(State.World.Villages, null, false) == true)
            return;
        Vec2i center = GrabFreeSquareNear(Config.StrategicWorldSizeX / 2, Config.StrategicWorldSizeY / 2);

        foreach (Village village in State.World.Villages)
        {
            var pseudoArmy = new Army(village.Empire, village.Position, village.Side);
            var path = StrategyPathfinder.GetPath(null, pseudoArmy, center, 0, false);
            if (path == null)
            {
                Vec2i currentLoc = new Vec2i(village.Position.x, village.Position.y);
                while (currentLoc.x != center.x || currentLoc.y != center.y)
                {
                    if (currentLoc.x != center.x)
                    {
                        if (currentLoc.x > center.x)
                            currentLoc.x -= 1;
                        else
                            currentLoc.x += 1;
                    }
                    if (currentLoc.y != center.y)
                    {
                        if (currentLoc.y > center.y)
                            currentLoc.y -= 1;
                        else
                            currentLoc.y += 1;
                    }

                    if (StrategicTileInfo.CanWalkInto(State.World.Tiles[currentLoc.x, currentLoc.y]) == false)
                        State.World.Tiles[currentLoc.x, currentLoc.y] = StrategicTileType.grass;
                }
                if (args.ExcessBridges == false)
                    StrategyPathfinder.Initialized = false;
            }
        }
        StrategyPathfinder.Initialized = false;
    }

    private static Vec2i GrabFreeSquareNear(int x, int y)
    {
        for (int newX = x - 2; newX < x + 3; newX++)
        {
            for (int newY = y - 2; newY < y + 3; newY++)
            {
                if (StrategicTileInfo.CanWalkInto(State.World.Tiles[newX, newY]))
                    return new Vec2i(newX, newY);
            }
        }
        return new Vec2i(x, y);
    }



}
