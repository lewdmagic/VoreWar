using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class NameGenerator
{
    private List<string> _femaleNames;
    private List<string> _maleNames;
    private List<string> _monsterNames;

    private Dictionary<Race, List<string>> _raceMaleNames;
    private Dictionary<Race, List<string>> _raceFemaleNames;
    private Dictionary<Race, List<string>> _raceMonsterNames;

    private Dictionary<Race, List<string>> _armyNames;

    private Dictionary<Race, string> _armyNameDefault;

    public NameGenerator()
    {
        Encoding encoding = Encoding.GetEncoding("iso-8859-1");

        if (File.Exists($"{State.StorageDirectory}males.txt"))
        {
            var logFile = File.ReadAllLines($"{State.StorageDirectory}males.txt", encoding);
            if (logFile.Any()) _maleNames = new List<string>(logFile);
        }

        if (File.Exists($"{State.StorageDirectory}females.txt"))
        {
            var logFile = File.ReadAllLines($"{State.StorageDirectory}females.txt", encoding);
            if (logFile.Any()) _femaleNames = new List<string>(logFile);
        }

        if (File.Exists($"{State.StorageDirectory}monsters.txt"))
        {
            var logFile = File.ReadAllLines($"{State.StorageDirectory}monsters.txt", encoding);
            if (logFile.Any()) _monsterNames = new List<string>(logFile);
        }

        _armyNames = new Dictionary<Race, List<string>>();
        _armyNameDefault = new Dictionary<Race, string>();

        if (File.Exists($"{State.StorageDirectory}armyNames.txt"))
        {
            var logFile = File.ReadAllLines($"{State.StorageDirectory}armyNames.txt", encoding);
            bool expectingdefault = false;
            Race currentRace = Race.Cat;
            foreach (string entry in logFile)
            {
                if (entry.Length > 0)
                {
                    if (entry.StartsWith("The first entry")) continue;
                    if (RaceFuncs.TryParse(entry, out Race race))
                    {
                        currentRace = race;
                        expectingdefault = true;
                    }
                    else if (expectingdefault)
                    {
                        _armyNameDefault[currentRace] = entry;
                        expectingdefault = false;
                    }
                    else
                    {
                        if (_armyNames.ContainsKey(currentRace))
                        {
                            _armyNames[currentRace].Add(entry);
                        }
                        else
                        {
                            _armyNames[currentRace] = new List<string>() { entry };
                        }
                    }
                }
            }
        }

        _raceMaleNames = new Dictionary<Race, List<string>>();
        _raceFemaleNames = new Dictionary<Race, List<string>>();
        _raceMonsterNames = new Dictionary<Race, List<string>>();

        foreach (Race race in RaceFuncs.RaceEnumerable())
        {
            if (File.Exists($"{State.StorageDirectory}male{race}.txt"))
            {
                var logFile = File.ReadAllLines($"{State.StorageDirectory}male{race}.txt", encoding);
                var names = new List<string>(logFile);
                _raceMaleNames[race] = names;
            }

            if (File.Exists($"{State.StorageDirectory}female{race}.txt"))
            {
                var logFile = File.ReadAllLines($"{State.StorageDirectory}female{race}.txt", encoding);
                var names = new List<string>(logFile);
                _raceFemaleNames[race] = names;
            }

            if (File.Exists($"{State.StorageDirectory}{race}.txt"))
            {
                var logFile = File.ReadAllLines($"{State.StorageDirectory}{race}.txt", encoding);
                var names = new List<string>(logFile);
                _raceMonsterNames[race] = names;
            }
        }
    }

    public string GetArmyName(Race race, Village village)
    {
        if (State.Rand.Next(10) == 0)
        {
            if (race != null && _armyNames.ContainsKey(race)) // race key is null because it has been changed from int to Race.
            {
                List<string> items = new List<string>();
                foreach (var item in _armyNames[race])
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

        if (village != null && race != null && _armyNameDefault.ContainsKey(race))
        {
            for (int i = 0; i < 9; i++)
            {
                var name = $"{AddOrdinal(State.Rand.Next(1, 100))} {village.Name} {_armyNameDefault[race]}";
                if (StrategicUtilities.GetAllArmies().Where(s => s.Name == name).Any() == false) return name;
            }

            return $"{AddOrdinal(State.Rand.Next(1, 100))} {village.Name} {_armyNameDefault[race]}";
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
            _raceMaleNames.TryGetValue(race, out list);
            if (list != null && list.Any())
            {
                return list[State.Rand.Next(list.Count)];
            }
        }
        else
        {
            _raceFemaleNames.TryGetValue(race, out list);
            if (list != null && list.Any())
            {
                return list[State.Rand.Next(list.Count)];
            }
        }

        _raceMonsterNames.TryGetValue(race, out list);
        if (list != null && list.Any())
        {
            return list[State.Rand.Next(list.Count)];
        }

        list = null;
        list = RaceFuncs.GetRace(race).ExtraRaceInfo().IndividualNames;

        if (list != null)
        {
            if (list.Count > 20) return list[State.Rand.Next(list.Count)];
            int rand = State.Rand.Next(20);
            if (rand < list.Count) return list[rand];
        }

        return _monsterNames[State.Rand.Next(_monsterNames.Count)];
    }

    public string GetName(bool male, Race race)
    {
        int r;
        if (male)
        {
            _raceMaleNames.TryGetValue(race, out var list);
            if (list != null && list.Any())
            {
                return list[State.Rand.Next(list.Count)];
            }

            r = State.Rand.Next(_maleNames.Count);
            return _maleNames[r];
        }
        else
        {
            _raceFemaleNames.TryGetValue(race, out var list);
            if (list != null && list.Any())
            {
                return list[State.Rand.Next(list.Count)];
            }

            r = State.Rand.Next(_femaleNames.Count);
            return _femaleNames[r];
        }
    }

    public string GetTownName(Race race, int i)
    {
        List<string> raceTownNames = RaceFuncs.GetRace(race).ExtraRaceInfo().TownNames;

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

        return $"{RaceFuncs.GetRace(race).SingularName(Gender.Male)} town {i + 1}";
    }

    public string GetAlternateTownName(Race race, int i)
    {
        List<string> racePreyTownNames = RaceFuncs.GetRace(race).ExtraRaceInfo().PreyTownNames;

        if (racePreyTownNames != null)
        {
            if (i >= 0 && i < racePreyTownNames.Count)
            {
                return racePreyTownNames[i];
            }
        }

        return $"{RaceFuncs.GetRace(race).SingularName(Gender.Male)} town {i + 1}";
    }
}