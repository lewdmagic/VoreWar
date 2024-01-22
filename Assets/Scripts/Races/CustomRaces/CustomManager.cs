using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoonSharp.Interpreter;
using UnityEngine;

public class CustomManager
{


    internal class FSClothingData
    {
        internal string RaceId;
        internal string ClothingId;
        internal string ClothingFolderName;
        internal string ClothingLuaCode;
        internal FileInfo[] Sprites;

        public FSClothingData(string raceId, string clothingFolderName, string clothingId, string clothingLuaCode, FileInfo[] sprites)
        {
            RaceId = raceId;
            ClothingId = clothingId;
            ClothingFolderName = clothingFolderName;
            ClothingLuaCode = clothingLuaCode;
            Sprites = sprites;
        }
    }

    internal class FSRaceData
    {
        internal string RaceId;
        internal string RaceFolderName;
        internal string RaceLuaCode;
        internal FileInfo[] Sprites;
        internal List<FSClothingData> Clothing = new List<FSClothingData>();

        public FSRaceData(string raceId, string raceFolderName, string raceLuaCode, FileInfo[] sprites)
        {
            RaceId = raceId;
            RaceFolderName = raceFolderName;
            RaceLuaCode = raceLuaCode;
            Sprites = sprites;
        }
    }
    
    private static FileInfo[] LoadSpriteNames2(string path)
    {
        return new DirectoryInfo(path).GetFiles("*.png");
    }

    internal void LoadAllCustom()
    {
        List<FSRaceData> races = new List<FSRaceData>();
    
        //string[] customRaceFolders = Directory.GetDirectories("GameData/CustomRaces","*", SearchOption.TopDirectoryOnly);
        string[] customRaceFolders = new DirectoryInfo("GameData/CustomRaces").GetDirectories().Select(it => it.Name).ToArray();
        
        
        
        foreach (string customRaceFolderName in customRaceFolders)
        {
            string raceId = customRaceFolderName.ToLower();
            string raceCode = File.ReadAllText($"GameData/CustomRaces/{customRaceFolderName}/race.lua");

            FileInfo[] raceSpriteNames = new DirectoryInfo($"GameData/CustomRaces/{customRaceFolderName}/Sprites").GetFiles("*.png");

            FSRaceData fsRaceData = new FSRaceData(raceId, customRaceFolderName, raceCode, raceSpriteNames);
            
            
            //string[] clothingFolders = Directory.GetDirectories($"GameData/CustomRaces/{customRaceFolderName}/Clothing","*", SearchOption.AllDirectories);
            string[] clothingFolders = new DirectoryInfo($"GameData/CustomRaces/{customRaceFolderName}/Clothing").GetDirectories().Select(it => it.Name).ToArray();;

            foreach (string clothingFolderName in clothingFolders)
            {
                string clothingId = clothingFolderName.ToLower();
                string clothingCode = File.ReadAllText($"GameData/CustomRaces/{customRaceFolderName}/Clothing/{clothingFolderName}/clothing.lua");
                FileInfo[] clothingSpriteNames = new DirectoryInfo($"GameData/CustomRaces/{customRaceFolderName}/Clothing/{clothingFolderName}/Sprites").GetFiles("*.png");
                
                FSClothingData fsClothingData = new FSClothingData(raceId, clothingFolderName, clothingId, clothingCode, clothingSpriteNames);
                fsRaceData.Clothing.Add(fsClothingData);
            }

            races.Add(fsRaceData);
        }

        process(races);
    }

    private void process(List<FSRaceData> races)
    {
        List<SpriteToLoad> spriteToLoadList = new List<SpriteToLoad>();
        
        foreach (FSRaceData fsRaceData in races)
        {
            foreach (FileInfo raceSpriteFileInfo in fsRaceData.Sprites)
            {
                string pureName = raceSpriteFileInfo.Name.Substring(0, raceSpriteFileInfo.Name.Length - raceSpriteFileInfo.Extension.Length).ToLower();
                string key = $"race/{fsRaceData.RaceId}/{pureName}";
                string path = $"GameData/CustomRaces/{fsRaceData.RaceFolderName}/Sprites/{raceSpriteFileInfo.Name}";
                spriteToLoadList.Add(new SpriteToLoad(key, path, raceSpriteFileInfo.LastWriteTimeUtc.ToFileTimeUtc()));
            }

            foreach (FSClothingData fsClothingData in fsRaceData.Clothing)
            {
                foreach (FileInfo clothingSpriteFileInfo in fsClothingData.Sprites)
                {
                    string pureName = clothingSpriteFileInfo.Name.Substring(0, clothingSpriteFileInfo.Name.Length - clothingSpriteFileInfo.Extension.Length).ToLower();
                    string key = $"clothing/{fsRaceData.RaceId}/{fsClothingData.ClothingId}/{pureName}";
                    string path = $"GameData/CustomRaces/{fsRaceData.RaceFolderName}/Clothing/{fsClothingData.ClothingFolderName}/Sprites/{clothingSpriteFileInfo.Name}";
                    spriteToLoadList.Add(new SpriteToLoad(key, path, clothingSpriteFileInfo.LastWriteTimeUtc.ToFileTimeUtc()));
                }
            }
        }

        (string, Sprite)[] sprites = SpritePacker.LoadOrUpdateTextures(spriteToLoadList);

        foreach ((string, Sprite) namedSprite in sprites)
        {
            string key = namedSprite.Item1;
            Sprite sprite = namedSprite.Item2;
            string[] split = key.Split('/');
            
            //Debug.Log(key);
            
            if (split[0] == "race")
            {
                string raceId = split[1];
                string spriteId = split[2];

                SpriteCollection spriteCollection = _raceSpriteCollections.GetOrSet(raceId, () => new SpriteCollection($"Race sprite collection for {raceId}"));
                spriteCollection.Add(spriteId, sprite);
            }
            else if (split[0] == "clothing")
            {
                string raceId = split[1];
                string clothingId = split[2];
                string spriteId = split[3];
                
                SpriteCollection spriteCollection = _clothingSpriteCollection.GetOrSet((raceId, clothingId), () => new SpriteCollection($"Clothing sprite collection for {raceId}/{clothingId}"));
                spriteCollection.Add(spriteId, sprite);
            }
            else
            {
                throw new Exception($"unknown sprite category {split[0]}");
            }
        }
        
        foreach (FSRaceData fsRaceData in races)
        {
            foreach (FSClothingData fsClothingData in fsRaceData.Clothing)
            {
                LuaBindableClothing clothing = ClothingFromFSData(fsClothingData);
                _clothings[(fsClothingData.RaceId, fsClothingData.ClothingId)] = clothing;
            }
            RaceFromFSData(fsRaceData);
        }
    }
    
    public struct RaceClothingKey
    {
        public readonly string RaceId;
        public readonly string ClothingID;

        public RaceClothingKey(string raceId, string clothingID)
        {
            RaceId = raceId;
            ClothingID = clothingID;
        }
    }


    private void RaceFromFSData(FSRaceData fsRaceData)
    {
        RaceScriptUsable raceScriptUsable = LuaBridge.RacePrep(fsRaceData.RaceLuaCode, fsRaceData.RaceId);
        RaceDataMaker raceData = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.RenderAll(raceScriptUsable.Generator);
            builder.Setup(raceScriptUsable.SetupFunc);
            builder.RandomCustom(raceScriptUsable.Value);
        });

        Race.CreateRace(fsRaceData.RaceId, raceData, new[] { RaceTag.MainRace });
    }
    
    private LuaBindableClothing ClothingFromFSData(FSClothingData fsClothingData)
    {
        ClothingScriptUsable clothingScriptUsable = LuaBridge.ScriptPrepClothingFromCode(fsClothingData.ClothingLuaCode, fsClothingData.ClothingId);
        return new LuaBindableClothing(clothingScriptUsable.SetMisc, clothingScriptUsable.CompleteGen);
    }


    // raceId
    private Dictionary<string, SpriteCollection> _raceSpriteCollections = new Dictionary<string, SpriteCollection>();
    
    // raceId, clothingId
    private Dictionary<(string, string), SpriteCollection> _clothingSpriteCollection = new Dictionary<(string, string), SpriteCollection>();

    private Dictionary<(string, string), LuaBindableClothing> _clothings = new Dictionary<(string, string), LuaBindableClothing>();

    
    internal IClothing GetRaceClothing(string raceId, string clothingId)
    {
        return GetRaceClothing(raceId, clothingId, (it) => null);
    }
    
    internal IClothing GetRaceClothing(string raceId, string clothingId, Func<IClothingRenderInput, Table> calcFunc)
    {
        SpriteCollection spriteCollection = GetClothingSpriteCollection(raceId, clothingId);
        return _clothings.GetOrNull((raceId, clothingId))?.Create(calcFunc, spriteCollection);
    }

    internal SpriteCollection GetRaceSpriteCollection(string raceId)
    {
        if (_raceSpriteCollections.TryGetValue(raceId, out var Res))
        {
            return Res;
        }
        else
        {
            return null;
        }
    }

    internal SpriteCollection GetClothingSpriteCollection(string raceId, string clothingId)
    {
        if (_clothingSpriteCollection.TryGetValue((raceId, clothingId), out var Res))
        {
            return Res;
        }
        else
        {
            Debug.Log("dfdf");
            return null;
        }
    }
    




}