using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Weighted<T>
{
    internal readonly double Weight;
    internal readonly T Value;

    internal Weighted(double weight, T value)
    {
        Weight = weight;
        Value = value;
    }
}


public class Gendered
{
    internal readonly string Text;
    internal readonly Gender? Gender;

    internal Gendered(string text, Gender? gender)
    {
        Text = text;
        Gender = gender;
    }
}

public interface ITexts : IEnumerable<Weighted<Gendered>>
{
}

internal class Texts : ITexts
{
    private readonly List<Weighted<Gendered>> _internalList = new List<Weighted<Gendered>>();
    public IEnumerator<Weighted<Gendered>> GetEnumerator() => _internalList.GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => _internalList.GetEnumerator();

    private Texts InternalAdd(string text, double weight, Gender? gender)
    {
        _internalList.Add(new Weighted<Gendered>(weight, new Gendered(text, gender)));
        return this;
    }

    internal void AddEntries(FlavorEntry[] entries)
    {
        foreach (var entry in entries)
        {
            _internalList.Add(entry.Value);
        }
    }

    internal void SetEntries(FlavorEntry[] entries)
    {
        _internalList.Clear();
        foreach (var entry in entries)
        {
            _internalList.Add(entry.Value);
        }
    }


    public Texts Add(string text) => InternalAdd(text, 1, null);
    public Texts Add(string text, double weight) => InternalAdd(text, weight, null);
    public Texts Add(string text, Gender gender, double weight) => InternalAdd(text, weight, gender);
    public Texts Add(string text, Gender gender) => InternalAdd(text, 1, gender);
}


public class FlavorEntry
{
    internal Weighted<Gendered> Value;

    public FlavorEntry(string text, double weight, Gender? gender)
    {
        Value = new Weighted<Gendered>(weight, new Gendered(text, gender));
    }

    public FlavorEntry(string text) : this(text, 1, null)
    {
    }

    public FlavorEntry(string text, double weight) : this(text, weight, null)
    {
    }

    public FlavorEntry(string text, Gender gender, double weight) : this(text, weight, gender)
    {
    }

    public FlavorEntry(string text, Gender gender) : this(text, 1, gender)
    {
    }
}

public static class Flavor
{
    public static FlavorEntry Make(string text)
    {
        return new FlavorEntry(text, 1, null);
    }

    public static FlavorEntry Make(string text, double weight)
    {
        return new FlavorEntry(text, weight, null);
    }

    public static FlavorEntry Make(string text, Gender gender)
    {
        return new FlavorEntry(text, 1, gender);
    }

    public static FlavorEntry Make(string text, Gender gender, double weight)
    {
        return new FlavorEntry(text, weight, gender);
    }
}


internal static class WeaponNames
{
    internal const string Mace = "Mace";
    internal const string Axe = "Axe";
    internal const string SimpleBow = "Simple Bow";
    internal const string CompoundBow = "Compound Bow";
    internal const string Claw = "Claw";
}

internal static class LogRaceData
{
    internal static readonly Dictionary<string, string> DefaultWeapons = new Dictionary<string, string>
    {
        [WeaponNames.Mace] = "Mace",
        [WeaponNames.Axe] = "Axe",
        [WeaponNames.SimpleBow] = "Simple Bow",
        [WeaponNames.CompoundBow] = "Compound Bow"
    };

    internal static readonly FlavorText DefaultFlavorText = new FlavorText(
        new Texts { "tasty" },
        new Texts { "strong" },
        new Texts { "creature" },
        DefaultWeapons
    );
}


public class FlavorText
{
    private static readonly Dictionary<string, FlavorType> WeaponToFlavorMap = new Dictionary<string, FlavorType>()
    {
        { WeaponNames.Claw, FlavorType.WeaponClaw },
        { WeaponNames.Mace, FlavorType.WeaponMelee1 },
        { WeaponNames.Axe, FlavorType.WeaponMelee2 },
        { WeaponNames.SimpleBow, FlavorType.WeaponRanged1 },
        { WeaponNames.CompoundBow, FlavorType.WeaponRanged2 }
    };


    internal Dictionary<FlavorType, Texts> ByType = new Dictionary<FlavorType, Texts>();


    // internal readonly Func<(Weapon weapon, Unit unit), string> getString;

    private static readonly Texts DefaultPreyDescriptions = new Texts { "tasty" };
    private static readonly Texts DefaultPredDescriptions = new Texts { "strong" };
    private static readonly Texts DefaultRaceSingleDescriptions = new Texts { "creature" };


    internal void AddEntries(FlavorType type, FlavorEntry[] entries)
    {
        ByType[type].AddEntries(entries);
    }

    internal void SetEntries(FlavorType type, FlavorEntry[] entries)
    {
        ByType[type].SetEntries(entries);
    }

    internal FlavorText() : this(DefaultPreyDescriptions, DefaultPredDescriptions, DefaultRaceSingleDescriptions, LogRaceData.DefaultWeapons)
    {
    }

    internal FlavorText(
        Texts preyDescriptions,
        Texts predDescriptions,
        Texts raceSingleDescriptions,
        Dictionary<string, string> weaponNames
    )
    {
        foreach (FlavorType key in EnumUtil.GetValues<FlavorType>())
        {
            ByType[key] = new Texts();
        }

        ByType[FlavorType.PreyAdjectives] = preyDescriptions;
        ByType[FlavorType.PredAdjectives] = predDescriptions;
        ByType[FlavorType.RaceSingleDescription] = raceSingleDescriptions;

        if (weaponNames != null)
        {
            if (weaponNames.Count == 0)
            {
                Debug.Log("No weapon names");
            }

            string name;
            if (weaponNames.TryGetValue(WeaponNames.Mace, out name))
            {
                ByType[FlavorType.WeaponMelee1].AddEntries(new[] { new FlavorEntry(name) });
            }

            if (weaponNames.TryGetValue(WeaponNames.Axe, out name))
            {
                ByType[FlavorType.WeaponMelee2].AddEntries(new[] { new FlavorEntry(name) });
            }

            if (weaponNames.TryGetValue(WeaponNames.SimpleBow, out name))
            {
                ByType[FlavorType.WeaponRanged1].AddEntries(new[] { new FlavorEntry(name) });
            }

            if (weaponNames.TryGetValue(WeaponNames.CompoundBow, out name))
            {
                ByType[FlavorType.WeaponRanged2].AddEntries(new[] { new FlavorEntry(name) });
            }

            if (weaponNames.TryGetValue(WeaponNames.Claw, out name))
            {
                ByType[FlavorType.WeaponClaw].AddEntries(new[] { new FlavorEntry(name) });
            }
        }
    }

    internal FlavorText(
        Texts preyDescriptions,
        Texts predDescriptions,
        Texts raceSingleDescriptions,
        string weaponName
    ) : this(
        preyDescriptions,
        predDescriptions,
        raceSingleDescriptions,
        new Dictionary<string, string>
        {
            [WeaponNames.Mace] = weaponName,
            [WeaponNames.Axe] = weaponName,
            [WeaponNames.SimpleBow] = weaponName,
            [WeaponNames.CompoundBow] = weaponName,
            [WeaponNames.Claw] = weaponName,
        })
    {
    }

    internal FlavorText(
        Texts preyDescriptions,
        Texts predDescriptions,
        Texts raceSingleDescriptions
    ) : this(preyDescriptions, predDescriptions, raceSingleDescriptions, LogRaceData.DefaultWeapons)
    {
    }

    internal string GetPreyDescription(Unit unit)
    {
        List<Weighted<Gendered>> filtered = ByType[FlavorType.PreyAdjectives].Where(description => description.Value.Gender == null || description.Value.Gender == unit.GetGender()).ToList();
        if (filtered.Count == 0) throw new Exception($"{unit.Race} has no GetPreyDescription");
        return PickWeighedFull(filtered).Text;
    }

    internal string GetPredDescription(Unit unit)
    {
        List<Weighted<Gendered>> filtered = ByType[FlavorType.PredAdjectives].Where(description => description.Value.Gender == null || description.Value.Gender == unit.GetGender()).ToList();
        if (filtered.Count == 0)
        {
            // TODO Defaults are not working correctly. 
            Debug.LogWarning($"{unit.Race} has no GetPredDescription");
            return "strong";
        }
        return PickWeighedFull(filtered).Text;
    }

    internal string GetRaceSingleDescription(Unit unit)
    {
        List<Weighted<Gendered>> filtered = ByType[FlavorType.RaceSingleDescription].Where(description => description.Value.Gender == null || description.Value.Gender == unit.GetGender()).ToList();
        if (filtered.Count == 0) throw new Exception($"{unit.Race} has no GetRaceSingleDescription");
        return PickWeighedFull(filtered).Text;
    }

    internal string GetWeaponTrueName(Weapon weapon, Unit unit)
    {
        if (WeaponToFlavorMap.TryGetValue(weapon.Name, out var type))
        {
            List<Weighted<Gendered>> filtered = ByType[type].Where(description => description.Value.Gender == null || description.Value.Gender == unit.GetGender()).ToList();
            if (filtered.Count != 0)
            {
                return PickWeighedFull(filtered).Text;
            }
            else
            {
                //TODO probably Not needed
                //throw new Exception($"{unit.Race} has no GetWeaponTrueName for {weapon.Name}");
            }
        }

        //TODO probably Not needed
        //Debug.LogWarning($"Missing name entry for {weapon.Name}, defaulting to the provided name {weapon.Name}. Not a critical error but should not happen. Race: ${unit.Race.Id}");
        return weapon.Name;
    }


    private static T PickWeighedFull<T>(List<Weighted<T>> list)
    {
        return PickWeighted(CreateWeights(list));
    }

    private static List<(double, T)> CreateWeights<T>(List<Weighted<T>> list)
    {
        List<(double, T)> result = new List<(double, T)>();
        double sum = list.Sum(entry => entry.Weight);

        double accumulator = 0;
        foreach (Weighted<T> entry in list)
        {
            double normalized = entry.Weight / sum;
            accumulator += normalized;
            result.Add((accumulator, entry.Value));
        }

        return result;
    }

    private static T PickWeighted<T>(List<(double, T)> weights)
    {
        double num = State.Rand.NextDouble();
        foreach ((double, T) entry in weights)
        {
            if (entry.Item1 >= num)
            {
                return entry.Item2;
            }
        }

        Debug.LogError("Something went very wrong with pickWeighted");
        var dfdf = weights.Select(x => { return x.Item1 + " " + x.Item2; }).Aggregate((a, b) => a + ", " + b);
        Debug.Log(dfdf);
        Debug.Log(num);
        return weights.First().Item2;
    }
}
//
//     
// public class FlavorText
// {
//
//
//     internal Dictionary<FlavorType, ITexts> ByType = new Dictionary<FlavorType, ITexts>();
//     
//     
//     internal readonly ITexts PreyDescriptions;
//     internal readonly ITexts PredDescriptions;
//     internal readonly ITexts RaceSingleDescriptions;
//     internal readonly Dictionary<string, string> WeaponNames;
//         
//         
//     // internal readonly Func<(Weapon weapon, Unit unit), string> getString;
//
//     private static readonly ITexts DefaultPreyDescriptions = new Texts { "tasty" };
//     private static readonly ITexts DefaultPredDescriptions = new Texts { "strong" };
//     private static readonly ITexts DefaultRaceSingleDescriptions = new Texts { "creature" };
//
//     
//     internal FlavorText()
//     {
//         PreyDescriptions = DefaultPreyDescriptions;
//         PredDescriptions = DefaultPredDescriptions;
//         RaceSingleDescriptions = DefaultRaceSingleDescriptions;
//         WeaponNames = LogRaceData.DefaultWeapons;
//     }
//     
//     internal FlavorText(
//         ITexts preyDescriptions,
//         ITexts predDescriptions,
//         ITexts raceSingleDescriptions,
//         Dictionary<string, string> weaponNames
//     )
//     {
//         this.PreyDescriptions = preyDescriptions.Any() ? preyDescriptions : DefaultPreyDescriptions;
//         this.PredDescriptions = predDescriptions.Any() ? predDescriptions : DefaultPredDescriptions;
//         this.RaceSingleDescriptions = raceSingleDescriptions.Any() ? raceSingleDescriptions : DefaultRaceSingleDescriptions;
//         this.WeaponNames = weaponNames ?? LogRaceData.DefaultWeapons;
//     }
//
//     internal FlavorText(
//         ITexts preyDescriptions,
//         ITexts predDescriptions,
//         ITexts raceSingleDescriptions,
//         string weaponName
//     ) : this(
//         preyDescriptions,
//         predDescriptions,
//         raceSingleDescriptions,
//         new Dictionary<string, string>
//         {
//             [global::WeaponNames.Mace] = weaponName,
//             [global::WeaponNames.Axe] = weaponName,
//             [global::WeaponNames.SimpleBow] = weaponName,
//             [global::WeaponNames.CompoundBow] = weaponName,
//             [global::WeaponNames.Claw] = weaponName,
//         })
//     {
//             
//     }
//         
//     internal FlavorText(
//         ITexts preyDescriptions,
//         ITexts predDescriptions,
//         ITexts raceSingleDescriptions
//     ) : this (preyDescriptions, predDescriptions, raceSingleDescriptions, LogRaceData.DefaultWeapons)
//     {
//             
//     }
//
//     internal string GetPreyDescription(Unit unit)
//     {
//         List<Weighted<Gendered>> filtered = PreyDescriptions.Where(description => description.Value.Gender == null || description.Value.Gender == unit.GetGender()).ToList();
//         if (filtered.Count == 0) throw new Exception($"{unit.Race} has no GetPreyDescription");
//         return PickWeighedFull(filtered).Text;
//     }
//
//     internal string GetPredDescription(Unit unit)
//     {
//         List<Weighted<Gendered>> filtered = PredDescriptions.Where(description => description.Value.Gender == null || description.Value.Gender == unit.GetGender()).ToList();
//         if (filtered.Count == 0) throw new Exception($"{unit.Race} has no GetPredDescription");
//         return PickWeighedFull(filtered).Text;
//     }
//
//     internal string GetRaceSingleDescription(Unit unit)
//     {
//         List<Weighted<Gendered>> filtered = RaceSingleDescriptions.Where(description => description.Value.Gender == null || description.Value.Gender == unit.GetGender()).ToList();
//         if (filtered.Count == 0) throw new Exception($"{unit.Race} has no GetRaceSingleDescription");
//         return PickWeighedFull(filtered).Text;
//     }
//
//     internal string GetWeaponTrueName(Weapon weapon, Unit unit)
//     {
//         if (WeaponNames.TryGetValue(weapon.Name, out string usedName))
//         {
//             return usedName;
//         }
//
//         Debug.LogWarning($"Missing name entry for {weapon.Name}, defaulting to the provided name {weapon.Name}. Not a critical error but should not happen. Race: ${unit.Race.Id}");
//         return weapon.Name;
//     }
//     
//     
//
//     private static T PickWeighedFull<T>(List<Weighted<T>> list)
//     {
//         return PickWeighted(CreateWeights(list));
//     }
//
//     private static List<(double, T)> CreateWeights<T>(List<Weighted<T>> list)
//     {
//         List<(double, T)> result = new List<(double, T)>();
//         double sum = list.Sum(entry => entry.Weight);
//
//         double accumulator = 0;
//         foreach (Weighted<T> entry in list)
//         {
//             double normalized = entry.Weight / sum;
//             accumulator += normalized;
//             result.Add((accumulator, entry.Value));
//         }
//
//         return result;
//     }
//
//     private static T PickWeighted<T>(List<(double, T)> weights)
//     {
//         double num = State.Rand.NextDouble();
//         foreach ((double, T) entry in weights)
//         {
//             if (entry.Item1 >= num)
//             {
//                 return entry.Item2;
//             }
//         }
//         Debug.LogError("Something went very wrong with pickWeighted");
//         var dfdf = weights.Select(x =>
//         {
//             return (x.Item1 + " " + x.Item2);
//         }).Aggregate((a, b) => a + ", " + b);
//         Debug.Log(dfdf);
//         Debug.Log(num);
//         return weights.First().Item2;
//     }
// }