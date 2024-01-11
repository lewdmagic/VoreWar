
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class CustomManager
{


    internal class FSClothingData
    {
        internal string RaceId;
        internal string ClothingId;
        internal string ClothingLuaCode;
        internal string[] Sprites;

        public FSClothingData(string raceId, string clothingId, string clothingLuaCode, string[] sprites)
        {
            RaceId = raceId;
            ClothingId = clothingId;
            ClothingLuaCode = clothingLuaCode;
            Sprites = sprites;
        }
    }

    internal class FSRaceData
    {
        internal string RaceId;
        internal string RaceLuaCode;
        internal string[] Sprites;
        internal Dictionary<string, FSClothingData> Clothing = new Dictionary<string, FSClothingData>();

        public FSRaceData(string raceId, string raceLuaCode, string[] sprites)
        {
            RaceId = raceId;
            RaceLuaCode = raceLuaCode;
            Sprites = sprites;
        }
    }


    private static string[] LoadSpriteNames(string path)
    {
        return new DirectoryInfo(path)
            .GetFiles("*.png")
            .Select(it => it.Name.Substring(0, it.Name.Length - 4))
            .ToArray();
    }

    internal void LoadAllCustom()
    {
        List<FSRaceData> races = new List<FSRaceData>();
    
        string[] customRaceFolders = Directory.GetDirectories("GameData/CustomRaces","*", SearchOption.AllDirectories);

        foreach (string customRaceFolderName in customRaceFolders)
        {
            string raceId = customRaceFolderName.ToLower();
            string raceCode = File.ReadAllText($"GameData/CustomRaces/{customRaceFolderName}/race.lua");

            string[] raceSpriteNames = LoadSpriteNames($"GameData/CustomRaces/{customRaceFolderName}/Sprites");

            FSRaceData fsRaceData = new FSRaceData(raceId, raceCode, raceSpriteNames);
            
            
            string[] clothingFolders = Directory.GetDirectories($"GameData/CustomRaces/{customRaceFolderName}/Clothing","*", SearchOption.AllDirectories);

            foreach (string clothingFolderName in clothingFolders)
            {
                string clothingId = clothingFolderName.ToLower();
                string clothingCode = File.ReadAllText($"GameData/CustomRaces/{customRaceFolderName}/Clothing/{clothingFolderName}/clothing.lua");
                string[] clothingSpriteNames = LoadSpriteNames($"GameData/CustomRaces/{customRaceFolderName}/Clothing/{clothingFolderName}/Sprites");
                
                FSClothingData fsClothingData = new FSClothingData(raceId, clothingId, clothingCode, clothingSpriteNames);
                fsRaceData.Clothing[clothingId] = fsClothingData;
            }

            races.Add(fsRaceData);
        }

        process(races);
    }

    private void process(List<FSRaceData> races)
    {
        
        // first do all the sprites
        // then do clothing
        // races are last
        // ACTUALLY MAKES NO DIFFERENCE. NOTHING IS REFERENCED DURING INIT

        foreach (FSRaceData fsRaceData in races)
        {
            IRaceData raceData = RaceFromFSData(fsRaceData);
            _races[fsRaceData.RaceId] = raceData;

            foreach (string spriteName in fsRaceData.Sprites)
            {
                
            }
            
            
            
        }
        
        
        
    }


    private IRaceData RaceFromFSData(FSRaceData fsRaceData)
    {
        throw new NotImplementedException();
    }


    private Dictionary<string, ClothingCollection> _raceClothingCollections = new Dictionary<string, ClothingCollection>();
    private Dictionary<string, SpriteCollection> _raceSpriteCollections = new Dictionary<string, SpriteCollection>();
    private Dictionary<(string, string), SpriteCollection> _clothingSpriteCollection = new Dictionary<(string, string), SpriteCollection>();

    private Dictionary<string, IRaceData> _races = new Dictionary<string, IRaceData>();

    internal ClothingCollection GetClothingCollectionForRace(string raceId)
    {
        return _raceClothingCollections.GetOrNull(raceId);
    }


    internal SpriteCollection GetRaceSpriteCollection(string raceId)
    {
        return _raceSpriteCollections.GetOrNull(raceId);
    }

    internal SpriteCollection GetClothingSpriteCollection(string raceId, string clothingId)
    {
        return _clothingSpriteCollection.GetOrNull((raceId, clothingId));
    }
    
    
    
    
    

    private Dictionary<string, SpriteCollection> _collections = new Dictionary<string, SpriteCollection>();


    public SpriteCollection GetCollection(string[] path)
    {
        string key = String.Join("*", path);

        if (_collections.TryGetValue(key, out SpriteCollection collection))
        {
            return collection;
        }
        else
        {
            return null;
        }
    }
    





}