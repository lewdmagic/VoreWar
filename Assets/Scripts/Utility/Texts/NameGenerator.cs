using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class NameGenerator
{
    private List<string> femaleNames;
    private List<string> maleNames;
    private List<string> monsterNames;

    private Dictionary<Race, List<string>> RaceMaleNames;
    private Dictionary<Race, List<string>> RaceFemaleNames;
    private Dictionary<Race, List<string>> RaceMonsterNames;

    private Dictionary<Race, List<string>> ArmyNames;

    private Dictionary<Race, string> ArmyNameDefault;

    public NameGenerator()
    {

        Encoding encoding = Encoding.GetEncoding("iso-8859-1");

        if (File.Exists($"{State.StorageDirectory}males.txt"))
        {
            var logFile = File.ReadAllLines($"{State.StorageDirectory}males.txt", encoding);
            if (logFile.Any())
                maleNames = new List<string>(logFile);
        }

        if (File.Exists($"{State.StorageDirectory}females.txt"))
        {
            var logFile = File.ReadAllLines($"{State.StorageDirectory}females.txt", encoding);
            if (logFile.Any())
                femaleNames = new List<string>(logFile);
        }

        if (File.Exists($"{State.StorageDirectory}monsters.txt"))
        {
            var logFile = File.ReadAllLines($"{State.StorageDirectory}monsters.txt", encoding);
            if (logFile.Any())
                monsterNames = new List<string>(logFile);
        }

        ArmyNames = new Dictionary<Race, List<string>>();
        ArmyNameDefault = new Dictionary<Race, string>();

        if (File.Exists($"{State.StorageDirectory}armyNames.txt"))
        {
            var logFile = File.ReadAllLines($"{State.StorageDirectory}armyNames.txt", encoding);
            bool expectingdefault = false;
            Race currentRace = Race.Cat;
            foreach (string entry in logFile)
            {
                if (entry.Length > 0)
                {
                    if (entry.StartsWith("The first entry"))
                        continue;
                    if (RaceFuncs.TryParse(entry, out Race race))
                    {
                        currentRace = race;
                        expectingdefault = true;
                    }
                    else if (expectingdefault)
                    {
                        ArmyNameDefault[currentRace] = entry;
                        expectingdefault = false;
                    }
                    else
                    {
                        if (ArmyNames.ContainsKey(currentRace))
                        {
                            ArmyNames[currentRace].Add(entry);
                        }
                        else
                        {
                            ArmyNames[currentRace] = new List<string>() { entry };
                        }
                    }
                }
            }
        }

        RaceMaleNames = new Dictionary<Race, List<string>>();
        RaceFemaleNames = new Dictionary<Race, List<string>>();
        RaceMonsterNames = new Dictionary<Race, List<string>>();

        foreach (Race race in RaceFuncs.RaceEnumerable())
        {
            if (File.Exists($"{State.StorageDirectory}male{race}.txt"))
            {
                var logFile = File.ReadAllLines($"{State.StorageDirectory}male{race}.txt", encoding);
                var names = new List<string>(logFile);
                RaceMaleNames[race] = names;
            }
            if (File.Exists($"{State.StorageDirectory}female{race}.txt"))
            {
                var logFile = File.ReadAllLines($"{State.StorageDirectory}female{race}.txt", encoding);
                var names = new List<string>(logFile);
                RaceFemaleNames[race] = names;
            }
            if (File.Exists($"{State.StorageDirectory}{race}.txt"))
            {
                var logFile = File.ReadAllLines($"{State.StorageDirectory}{race}.txt", encoding);
                var names = new List<string>(logFile);
                RaceMonsterNames[race] = names;
            }
        }

    }

    public string GetArmyName(Race race, Village village)
    {
        if (State.Rand.Next(10) == 0)
        {
            if (race != null && ArmyNames.ContainsKey(race)) // race key is null because it has been changed from int to Race.
            {
                List<string> items = new List<string>();
                foreach (var item in ArmyNames[race])
                {
                    items.Add(item);
                }
                var pick = items[State.Rand.Next(items.Count)];
                if (StrategicUtilities.GetAllArmies().Where(s => s.Name == pick).Any() == false)
                {
                    return pick;
                }
            }
        }
        if (village != null && race != null && ArmyNameDefault.ContainsKey(race))
        {
            for (int i = 0; i < 9; i++)
            {
                var name = $"{AddOrdinal(State.Rand.Next(1, 100))} {village.Name} {ArmyNameDefault[race]}";
                if (StrategicUtilities.GetAllArmies().Where(s => s.Name == name).Any() == false)
                    return name;
            }
            return $"{AddOrdinal(State.Rand.Next(1, 100))} {village.Name} {ArmyNameDefault[race]}";
        }
        return "";

        string AddOrdinal(int num)
        {
            if (num <= 0) return num.ToString();

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }

        }
    }

    public string GetMonsterName(bool male, Race race)
    {
        List<string> list;
        if (male)
        {
            RaceMaleNames.TryGetValue(race, out list);
            if (list != null && list.Any())
            {
                return list[State.Rand.Next(list.Count)];
            }
        }
        else
        {
            RaceFemaleNames.TryGetValue(race, out list);
            if (list != null && list.Any())
            {
                return list[State.Rand.Next(list.Count)];
            }
        }
        RaceMonsterNames.TryGetValue(race, out list);
        if (list != null && list.Any())
        {
            return list[State.Rand.Next(list.Count)];
        }
        list = null;
        list = Races2.GetRace(race).ExtraRaceInfo().IndividualNames;
        
        if (list != null)
        {
            if (list.Count > 20)
                return list[State.Rand.Next(list.Count)];
            int rand = State.Rand.Next(20);
            if (rand < list.Count)
                return list[rand];
        }
        return monsterNames[State.Rand.Next(monsterNames.Count)];

    }

    public string GetName(bool male, Race race)
    {

        int r;
        if (male)
        {
            RaceMaleNames.TryGetValue(race, out var list);
            if (list != null && list.Any())
            {
                return list[State.Rand.Next(list.Count)];
            }
            r = State.Rand.Next(maleNames.Count);
            return maleNames[r];
        }
        else
        {
            RaceFemaleNames.TryGetValue(race, out var list);
            if (list != null && list.Any())
            {
                return list[State.Rand.Next(list.Count)];
            }
            r = State.Rand.Next(femaleNames.Count);
            return femaleNames[r];
        }


    }

    public string GetTownName(Race race, int i)
    {

        List<string> raceTownNames = Races2.GetRace(race).ExtraRaceInfo().TownNames;

        if (raceTownNames != null)
        {
            if (i >= 0 && i < raceTownNames.Count)
            {
                return raceTownNames[i];
            }
        }
        
        else if (Equals(race, Race.Vagrant))
        {
            return $"Abandoned town {i + 1}";
        }

        return $"{Races2.GetRace(race).SingularName(Gender.Male)} town {i + 1}";
    }

    public string GetAlternateTownName(Race race, int i)
    {
        List<string> racePreyTownNames = Races2.GetRace(race).ExtraRaceInfo().PreyTownNames;

        if (racePreyTownNames != null)
        {
            if (i >= 0 && i < racePreyTownNames.Count)
            {
                return racePreyTownNames[i];
            }
        }

        return $"{Races2.GetRace(race).SingularName(Gender.Male)} town {i + 1}";
    }

}
