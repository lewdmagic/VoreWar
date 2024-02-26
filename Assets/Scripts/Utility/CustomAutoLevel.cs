using OdinSerializer;
using System.Collections.Generic;
using System.IO;


internal class StoredClassWeight
{
    [OdinSerialize]
    private StatWeights _weights;

    internal StatWeights Weights { get => _weights; set => _weights = value; }

    [OdinSerialize]
    private string _name;

    internal string Name { get => _name; set => _name = value; }
}

internal static class CustomAutoLevel
{
    static CustomAutoLevel()
    {
        _filename = $"{State.StorageDirectory}AutoLevels.cst";
        LoadData();
    }

    private static string _filename;
    private static List<StoredClassWeight> _weightsList;

    internal static string[] GetAllNames()
    {
        string[] names = new string[_weightsList.Count];
        for (int i = 0; i < names.Length; i++)
        {
            names[i] = _weightsList[i].Name;
        }

        return names;
    }


    private static void LoadData()
    {
        if (File.Exists(_filename))
        {
            byte[] bytes = File.ReadAllBytes(_filename);
            _weightsList = SerializationUtility.DeserializeValue<List<StoredClassWeight>>(bytes, DataFormat.JSON);
        }
        else
        {
            _weightsList = new List<StoredClassWeight>();
        }
    }

    private static void SaveData()
    {
        try
        {
            byte[] bytes = SerializationUtility.SerializeValue(_weightsList, DataFormat.JSON);
            File.WriteAllBytes(_filename, bytes);
        }
        catch
        {
            State.GameManager.CreateMessageBox("Couldn't save Custom Auto Levels to file for some reason");
        }
    }

    internal static StoredClassWeight GetByName(string name)
    {
        foreach (StoredClassWeight entry in _weightsList)
        {
            if (entry.Name == name) return entry;
        }

        return null;
    }

    internal static void Remove(StoredClassWeight data)
    {
        _weightsList.Remove(data);
        SaveData();
    }

    internal static void Add(StoredClassWeight data)
    {
        _weightsList.Add(data);
        SaveData();
    }
}