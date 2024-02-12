﻿using OdinSerializer;
using System.Collections.Generic;
using System.IO;
using System.Linq;

static class CustomizationDataStorer
{
    static string filename;
    static List<CustomizerData> customizations;

    static CustomizationDataStorer()
    {
        filename = $"{State.StorageDirectory}Customizations.cst";
        LoadData();
    }

    static void LoadData()
    {
        if (File.Exists(filename))
        {
            byte[] bytes = File.ReadAllBytes(filename);
            customizations = SerializationUtility.DeserializeValue<List<CustomizerData>>(bytes, DataFormat.Binary);
        }
        else
        {
            customizations = new List<CustomizerData>();
        }
    }

    static void SaveData()
    {
        byte[] bytes = SerializationUtility.SerializeValue(customizations, DataFormat.Binary);
        File.WriteAllBytes(filename, bytes);
    }

    internal static void Remove(CustomizerData unitCustomizer)
    {
        customizations.Remove(unitCustomizer);
        SaveData();
    }

    internal static void Add(CustomizerData unitCustomizer)
    {
        customizations.Add(unitCustomizer);
        SaveData();
    }

    static internal void ExternalCopyToUnit(CustomizerData data, Unit unit)
    {
        data.CopyToUnit(unit, true);
    }


    static internal List<CustomizerData> GetCompatibleCustomizations(Race race, UnitType type, bool includeOtherRaces)
    {
        if (includeOtherRaces)
            return customizations.Where(s => IsCompatibleWithGraphics(s)).ToList();
        if (type == UnitType.Leader)
            return customizations.Where(s => Equals(s.Race, race) && (s.Type == type) && IsCompatibleWithGraphics(s)).ToList();
        return customizations.Where(s => Equals(s.Race, race) && (s.Type == type || s.Type != UnitType.Leader) && IsCompatibleWithGraphics(s)).ToList();
    }

    static bool IsCompatibleWithGraphics(CustomizerData data)
    {
        if (!Equals(data.Race, Race.Imp) && !Equals(data.Race, Race.Lamia) && !Equals(data.Race, Race.Tiger))
        {
            if (data.NewGraphics != Config.NewGraphics)
                return false;
        }
        return true;
    }


}

