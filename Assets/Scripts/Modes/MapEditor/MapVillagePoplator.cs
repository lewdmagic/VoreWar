using MapObjects;
using System.Collections.Generic;
using System.Linq;


internal class MapVillagePopulator
{
    private readonly StrategicTileType[,] _tiles;

    public MapVillagePopulator(StrategicTileType[,] tiles)
    {
        this._tiles = tiles;
    }

    internal void PopulateVillages(Map map, ref Village[] villages)
    {
        List<Village> newVillages = new List<Village>();
        Dictionary<Race, int> nameIndex = new Dictionary<Race, int>();

        for (int i = 0; i < map.StoredVillages.Length; i++)
        {
            if (Equals(map.StoredVillages[i].Race, Race.Vagrant))
            {
                Race race = Race.Vagrant;
                if (nameIndex.ContainsKey(race) == false) nameIndex[race] = 0;
                Village vill = new Village(State.NameGen.GetTownName(race, nameIndex[race]), map.StoredVillages[i].Position, FarmSquares(map.StoredVillages[i].Position), race, false);
                vill.SubtractPopulation(999999);
                newVillages.Add(vill);
                nameIndex[race] = nameIndex[race] + 1;
                continue;
            }

            // if (Config.World.VillagesPerEmpire.Count < RaceFuncs.RaceToInt(map.storedVillages[i].Race))
            //     continue; // TODO check is probably not needed with a dictionary
            if (Config.World.VillagesPerEmpire[map.StoredVillages[i].Race] == 0) continue;

            if (map.StoredVillages[i].Capital == true)
            {
                Race race = map.StoredVillages[i].Race;
                newVillages.Add(new Village(State.NameGen.GetTownName(race, 0), map.StoredVillages[i].Position, FarmSquares(map.StoredVillages[i].Position), race, true));
            }
            else
            {
                Race race = map.StoredVillages[i].Race;
                if (nameIndex.ContainsKey(race) == false) nameIndex[race] = 0;
                newVillages.Add(new Village(State.NameGen.GetTownName(race, nameIndex[race] + 1), map.StoredVillages[i].Position, FarmSquares(map.StoredVillages[i].Position), race, false));
                nameIndex[race] = nameIndex[race] + 1;
            }
        }

        villages = newVillages.ToArray();
    }


    // internal void PopulateVillages(Map map, ref Village[] villages)
    // {
    //     List<Village> newVillages = new List<Village>();
    //     Dictionary<int, int> nameIndex = new Dictionary<int, int>();
    //
    //     for (int i = 0; i < map.storedVillages.Length; i++)
    //     {
    //         if (map.storedVillages[i].Race == Race.Vagrants)
    //         {
    //             Race race = Race.Vagrants;
    //             if (nameIndex.ContainsKey(RaceFuncs.RaceToInt(race)) == false)
    //                 nameIndex[RaceFuncs.RaceToInt(race)] = 0;
    //             Village vill = new Village(State.NameGen.GetTownName(race, nameIndex[RaceFuncs.RaceToInt(race)]), map.storedVillages[i].Position, FarmSquares(map.storedVillages[i].Position), race, false);
    //             vill.SubtractPopulation(999999);
    //             newVillages.Add(vill);
    //             nameIndex[RaceFuncs.RaceToInt(race)] = nameIndex[RaceFuncs.RaceToInt(race)] + 1;
    //             continue;
    //         }
    //         if (Config.World.VillagesPerEmpire.Count < RaceFuncs.RaceToInt(map.storedVillages[i].Race))
    //             continue; // TODO check is probably not needed with a dictionary
    //         if (Config.World.VillagesPerEmpire[map.storedVillages[i].Race] == 0)
    //             continue;
    //
    //         if (map.storedVillages[i].Capital == true)
    //         {
    //             Race race = map.storedVillages[i].Race;
    //             newVillages.Add(new Village(State.NameGen.GetTownName(race, 0), map.storedVillages[i].Position, FarmSquares(map.storedVillages[i].Position), race, true));
    //         }
    //         else
    //         {
    //             Race race = map.storedVillages[i].Race;
    //             if (nameIndex.ContainsKey(RaceFuncs.RaceToInt(race)) == false)
    //                 nameIndex[RaceFuncs.RaceToInt(race)] = 0;
    //             newVillages.Add(new Village(State.NameGen.GetTownName(race, nameIndex[RaceFuncs.RaceToInt(race)] + 1), map.storedVillages[i].Position, FarmSquares(map.storedVillages[i].Position), race, false));
    //             nameIndex[RaceFuncs.RaceToInt(race)] = nameIndex[RaceFuncs.RaceToInt(race)] + 1;
    //         }
    //
    //     }
    //     villages = newVillages.ToArray();
    // }

    internal void PopulateMercenaryHouses(Map map, ref MercenaryHouse[] houses)
    {
        if (map.MercLocations == null)
        {
            houses = new MercenaryHouse[0];
            return;
        }

        List<MercenaryHouse> newHouses = new List<MercenaryHouse>();


        for (int i = 0; i < map.MercLocations.Length; i++)
        {
            newHouses.Add(new MercenaryHouse(map.MercLocations[i]));
        }

        houses = newHouses.ToArray();
    }

    internal void PopulateClaimables(Map map, ref ClaimableBuilding[] claimables)
    {
        if (map.Claimables == null)
        {
            claimables = new ClaimableBuilding[0];
            return;
        }

        List<ClaimableBuilding> newClaimables = new List<ClaimableBuilding>();


        for (int i = 0; i < map.Claimables.Length; i++)
        {
            if (map.Claimables[i].Type == ClaimableType.GoldMine) newClaimables.Add(new GoldMine(map.Claimables[i].Position));
        }

        claimables = newClaimables.ToArray();
    }

    private int FarmSquares(Vec2I pos)
    {
        int t = 0;
        for (int i = pos.X - 1; i < pos.X + 2; i++)
        {
            for (int j = pos.Y - 1; j < pos.Y + 2; j++)
            {
                if (!(i == pos.X && pos.Y == j))
                {
                    if (_tiles[i, j] == StrategicTileType.Field || _tiles[i, j] == StrategicTileType.FieldDesert || _tiles[i, j] == StrategicTileType.FieldSnow)
                    {
                        t++;
                    }
                }
            }
        }

        return t;
    }
}