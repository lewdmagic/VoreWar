using OdinSerializer;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class UniformObject
{
    [OdinSerialize]
    internal Dictionary<Race, List<UniformData>> Uniforms;

    [OdinSerialize]
    internal Dictionary<Race, float> Odds;
}

internal static class UniformDataStorer
{
    private static string _filename;
    private static UniformObject _data;

    internal static float GetUniformOdds(Race race)
    {
        if (_data.Odds.TryGetValue(race, out var value))
        {
            return value;
        }
        else
        {
            return 1;
        }
    }

    internal static void SetUniformOdds(Race race, float value)
    {
        _data.Odds[race] = value;
        SaveData();
    }

    static UniformDataStorer()
    {
        _filename = $"{State.StorageDirectory}UniformData.cst";
        LoadData();
    }

    private static void LoadData()
    {
        if (File.Exists(_filename))
        {
            byte[] bytes = File.ReadAllBytes(_filename);
            _data = SerializationUtility.DeserializeValue<UniformObject>(bytes, DataFormat.Binary);
        }
        else
        {
            _data = new UniformObject
            {
                Uniforms = new Dictionary<Race, List<UniformData>>(),
                Odds = new Dictionary<Race, float>()
            };
        }
    }

    private static void SaveData()
    {
        byte[] bytes = SerializationUtility.SerializeValue(_data, DataFormat.Binary);
        File.WriteAllBytes(_filename, bytes);
    }

    internal static void Remove(UniformData unitCustomizer)
    {
        if (_data.Uniforms.TryGetValue(unitCustomizer.Race, out var value))
        {
            value.Remove(unitCustomizer);
            SaveData();
        }
    }

    internal static void Add(UniformData unitCustomizer)
    {
        if (_data.Uniforms.TryGetValue(unitCustomizer.Race, out var value))
        {
            var replace = value.Where(s => s.Name == unitCustomizer.Name).FirstOrDefault();
            if (replace != null) value.Remove(replace);
            value.Add(unitCustomizer);
        }
        else
        {
            var newList = new List<UniformData>();
            newList.Add(unitCustomizer);
            _data.Uniforms[unitCustomizer.Race] = newList;
        }

        SaveData();
    }

    internal static void ExternalCopyToUnit(UniformData data, Unit unit)
    {
        data.CopyToUnit(unit);
    }

    internal static List<UniformData> GetCompatibleCustomizations(Unit unit)
    {
        return GetCompatibleCustomizations(unit.Race, unit.Type, unit.GetGender());
    }

    internal static List<UniformData> GetCompatibleCustomizations(Race race, UnitType type, Gender gender)
    {
        if (_data.Uniforms.TryGetValue(race, out var values))
        {
            if (type == UnitType.Leader) return values.Where(s => s.Type == type && GenderOkay(s.Gender, gender)).ToList();
            ;
            return values.Where(s => GenderOkay(s.Gender, gender) && (s.Type == type || s.Type != UnitType.Leader)).ToList();
        }

        return null;

        bool GenderOkay(Gender person, Gender uniform)
        {
            if (person == Gender.Male && uniform == Gender.Male) return true;
            if (person != Gender.Male && uniform != Gender.Male) return true;
            return false;
        }
    }

    internal static List<UniformData> GetIncompatibleCustomizations(Unit unit)
    {
        return GetIncompatibleCustomizations(unit.Race, unit.Type, unit.GetGender());
    }

    internal static List<UniformData> GetIncompatibleCustomizations(Race race, UnitType type, Gender gender)
    {
        if (_data.Uniforms.TryGetValue(race, out var values))
        {
            if (type == UnitType.Leader) return values.Where(s => !(s.Type == type && GenderOkay(s.Gender, gender))).ToList();
            return values.Where(s => !(GenderOkay(s.Gender, gender) && (s.Type == type || s.Type != UnitType.Leader))).ToList();
        }

        return null;

        bool GenderOkay(Gender person, Gender uniform)
        {
            if (person == Gender.Male && uniform == Gender.Male) return true;
            if (person != Gender.Male && uniform != Gender.Male) return true;
            return false;
        }
    }
}