using System;
using System.Collections.Generic;
using UnityEngine;

public class AssimilateList
{
    public bool Initialized = false;

    [AllowEditing]
    internal Dictionary<TraitType, bool> CanAssimilate;


    internal bool CanGet(TraitType traitType)
    {
        if (Initialized == false) Initialize();
        if (CanAssimilate.TryGetValue(traitType, out bool value))
        {
            return value;
        }

        return false;
    }

    internal void Initialize()
    {
        Initialized = true;
        CanAssimilate = new Dictionary<TraitType, bool>();
        foreach (TraitType trait in (TraitType[])Enum.GetValues(typeof(TraitType)))
        {
            if (trait == TraitType.Prey) continue;
            CanAssimilate[trait] = PlayerPrefs.GetInt($"A{trait}", 1) == 1;
        }
    }

    internal void Save()
    {
        foreach (var entry in CanAssimilate)
        {
            PlayerPrefs.SetInt($"A{entry.Key}", entry.Value ? 1 : 0);
        }
    }
}