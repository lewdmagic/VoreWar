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

public class TextsBasic : ITexts
{
    private readonly List<Weighted<Gendered>> internalList = new List<Weighted<Gendered>>();
    public IEnumerator<Weighted<Gendered>> GetEnumerator() => internalList.GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => internalList.GetEnumerator();

    private void InternalAdd(string text, double weight, Gender? gender)
    {
        internalList.Add(new Weighted<Gendered>(weight, new Gendered(text, gender)));
    }

    public TextsBasic Add(string text)
    {
        InternalAdd(text, 1, null);
        return this;
    }

    public TextsBasic Add(string text, double weight)
    {
        InternalAdd(text, weight, null);
        return this;
    }

    public TextsBasic Add(string text, double weight, Gender gender)
    {
        InternalAdd(text, weight, gender);
        return this;
    }

    public TextsBasic Add(string text, Gender gender)
    {
        InternalAdd(text, 1, gender);
        return this;
    }
}

    
internal class Texts : ITexts
{
    private readonly List<Weighted<Gendered>> internalList = new List<Weighted<Gendered>>();
    public IEnumerator<Weighted<Gendered>> GetEnumerator() => internalList.GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => internalList.GetEnumerator();

    private void InternalAdd(string text, double weight, Gender? gender)
    {
        internalList.Add(new Weighted<Gendered>(weight, new Gendered(text, gender)));
    }

    internal void Add(string text) => InternalAdd(text, 1, null);
    internal void Add(string text, double weight) => InternalAdd(text, weight, null);
    internal void Add(string text, double weight, Gender gender) => InternalAdd(text, weight, gender);
    internal void Add(string text, Gender gender) => InternalAdd(text, 1, gender);
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
    
 internal class FlavorText
{
    private readonly ITexts _preyDescriptions;
    private readonly ITexts _predDescriptions;
    private readonly ITexts _raceSingleDescriptions;
    private readonly Dictionary<string, string> _weaponNames;
        
        
    // internal readonly Func<(Weapon weapon, Unit unit), string> getString;

    private static readonly ITexts DefaultPreyDescriptions = new Texts { "tasty" };
    private static readonly ITexts DefaultPredDescriptions = new Texts { "strong" };
    private static readonly ITexts DefaultRaceSingleDescriptions = new Texts { "creature" };

    
    internal FlavorText(
        ITexts preyDescriptions,
        ITexts predDescriptions,
        ITexts raceSingleDescriptions,
        Dictionary<string, string> weaponNames
    )
    {
        this._preyDescriptions = preyDescriptions.Any() ? preyDescriptions : DefaultPreyDescriptions;
        this._predDescriptions = predDescriptions.Any() ? predDescriptions : DefaultPredDescriptions;
        this._raceSingleDescriptions = raceSingleDescriptions.Any() ? raceSingleDescriptions : DefaultRaceSingleDescriptions;
        this._weaponNames = weaponNames ?? LogRaceData.DefaultWeapons;
    }

    internal FlavorText(
        ITexts preyDescriptions,
        ITexts predDescriptions,
        ITexts raceSingleDescriptions,
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
        ITexts preyDescriptions,
        ITexts predDescriptions,
        ITexts raceSingleDescriptions
    ) : this (preyDescriptions, predDescriptions, raceSingleDescriptions, LogRaceData.DefaultWeapons)
    {
            
    }

     internal string GetPreyDescription(Unit unit)
    {
        List<Weighted<Gendered>> filtered = _preyDescriptions.Where(description => description.Value.Gender == null || description.Value.Gender == unit.GetGender()).ToList();
        if (filtered.Count == 0) throw new Exception($"{unit.Race} has no GetPreyDescription");
        return PickWeighedFull(filtered).Text;
    }

     internal string GetPredDescription(Unit unit)
    {
        List<Weighted<Gendered>> filtered = _predDescriptions.Where(description => description.Value.Gender == null || description.Value.Gender == unit.GetGender()).ToList();
        if (filtered.Count == 0) throw new Exception($"{unit.Race} has no GetPredDescription");
        return PickWeighedFull(filtered).Text;
    }

     internal string GetRaceSingleDescription(Unit unit)
    {
        List<Weighted<Gendered>> filtered = _raceSingleDescriptions.Where(description => description.Value.Gender == null || description.Value.Gender == unit.GetGender()).ToList();
        if (filtered.Count == 0) throw new Exception($"{unit.Race} has no GetRaceSingleDescription");
        return PickWeighedFull(filtered).Text;
    }

     internal string GetWeaponTrueName(Weapon weapon, Unit unit)
    {
        if (_weaponNames.TryGetValue(weapon.Name, out string usedName))
        {
            return usedName;
        }

        Debug.LogWarning($"Missing name entry for {weapon.Name}, defaulting to the provided name {weapon.Name}. Not a critical error but should not happen. Race: ${unit.Race.Id}");
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
        var dfdf = weights.Select(x =>
        {
            return (x.Item1 + " " + x.Item2);
        }).Aggregate((a, b) => a + ", " + b);
        Debug.Log(dfdf);
        Debug.Log(num);
        return weights.First().Item2;
    }
}