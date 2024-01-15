using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                IClothing clothing = ClothingFromFSData(fsClothingData);
                _clothings[(fsClothingData.RaceId, fsClothingData.ClothingId)] = clothing;
                
                foreach (FileInfo clothingSpriteFileInfo in fsRaceData.Sprites)
                {
                    string pureName = clothingSpriteFileInfo.Name.Substring(0, clothingSpriteFileInfo.Name.Length - clothingSpriteFileInfo.Extension.Length).ToLower();
                    string key = $"clothing/{fsRaceData.RaceId}/{fsClothingData.ClothingId}/{pureName}";
                    string path = $"GameData/CustomRaces/{fsRaceData.RaceFolderName}/Clothing/{fsClothingData.ClothingFolderName}/Sprites/{clothingSpriteFileInfo.Name}";
                    spriteToLoadList.Add(new SpriteToLoad(key, path, clothingSpriteFileInfo.LastWriteTimeUtc.ToFileTimeUtc()));
                }
            }
            
            RaceFromFSData(fsRaceData);
        }

        (string, Sprite)[] sprites = SpritePacker.LoadOrUpdateTextures(spriteToLoadList);

        foreach ((string, Sprite) namedSprite in sprites)
        {
            string key = namedSprite.Item1;
            Sprite sprite = namedSprite.Item2;
            string[] split = key.Split('/');
            
            if (split[0] == "race")
            {
                string raceId = split[1];
                string spriteId = split[2];

                SpriteCollection spriteCollection = _raceSpriteCollections.GetOrSet(raceId, () => new SpriteCollection());
                spriteCollection.Add(spriteId, sprite);
            }
            else if (split[0] == "clothing")
            {
                string raceId = split[1];
                string clothingId = split[2];
                string spriteId = split[3];
                
                SpriteCollection spriteCollection = _clothingSpriteCollection.GetOrSet((raceId, clothingId), () => new SpriteCollection());
                spriteCollection.Add(spriteId, sprite);
            }
            else
            {
                throw new Exception($"unknown sprite category {split[0]}");
            }
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
        IRaceData raceData = RaceBuilder.CreateV2(Defaults.Blank, builder =>
        {
            ScriptHelper.ScriptPrep2FromCode(fsRaceData.RaceLuaCode, fsRaceData.RaceId, builder);
        });

        Race.RegisterRace(fsRaceData.RaceId, () => raceData, new[] { RaceTag.MainRace });
    }
    private IClothing ClothingFromFSData(FSClothingData fsClothingData)
    {
        return ClothingBuilder.Create(builder =>
            {
                ScriptHelper.ScriptPrepClothingFromCode(fsClothingData.ClothingLuaCode, builder);
            }
        );
    }


    private Dictionary<string, ClothingCollection> _raceClothingCollections = new Dictionary<string, ClothingCollection>();
    private Dictionary<string, SpriteCollection> _raceSpriteCollections = new Dictionary<string, SpriteCollection>();
    private Dictionary<(string, string), SpriteCollection> _clothingSpriteCollection = new Dictionary<(string, string), SpriteCollection>();

    private Dictionary<(string, string), IClothing> _clothings = new Dictionary<(string, string), IClothing>();

    internal ClothingCollection GetClothingCollectionForRace(string raceId)
    {
        return _raceClothingCollections.GetOrNull(raceId);
    }
    
    internal IClothing GetRaceClothing(string raceId, string clothingId)
    {
        
        return _clothings.GetOrNull((raceId, clothingId));
    }


    internal SpriteCollection GetRaceSpriteCollection(string raceId)
    {
        return _raceSpriteCollections.GetOrNull(raceId);
    }

    internal SpriteCollection GetClothingSpriteCollection(string raceId, string clothingId)
    {
        return _clothingSpriteCollection.GetOrNull((raceId, clothingId));
    }
    




}