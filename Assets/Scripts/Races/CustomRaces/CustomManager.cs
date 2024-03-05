using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using MoonSharp.Interpreter;
using UnityEngine;

public class CustomManager
{
    internal class FsClothingData
    {
        internal readonly string RaceId;
        internal readonly string ClothingId;
        internal readonly string ClothingFolderName;
        internal readonly string ClothingLuaCode;
        internal readonly FileInfo[] Sprites;

        public FsClothingData(string raceId, string clothingFolderName, string clothingId, string clothingLuaCode, FileInfo[] sprites)
        {
            RaceId = raceId;
            ClothingId = clothingId;
            ClothingFolderName = clothingFolderName;
            ClothingLuaCode = clothingLuaCode;
            Sprites = sprites;
        }
    }

    internal class FsPaletteData
    {
        internal readonly string PaletteId;
        internal readonly FileInfo PaletteImage;

        public FsPaletteData(string paletteId, FileInfo paletteImage)
        {
            PaletteId = paletteId;
            PaletteImage = paletteImage;
        }
    }

    internal class FsRaceData
    {
        internal readonly string RaceId;
        internal readonly string RaceFolderName;
        internal readonly string RaceLuaCode;
        internal readonly FileInfo[] Sprites;
        internal readonly List<FsClothingData> Clothing = new List<FsClothingData>();
        internal readonly List<FsPaletteData> Palettes = new List<FsPaletteData>();

        public FsRaceData(string raceId, string raceFolderName, string raceLuaCode, FileInfo[] sprites)
        {
            RaceId = raceId;
            RaceFolderName = raceFolderName;
            RaceLuaCode = raceLuaCode;
            Sprites = sprites;
        }
    }

    internal class CachedFileInfo
    {
        readonly string Path;
        readonly string Name;
        readonly string NameNoExtension;
        readonly DateTime LastModifiedTime;
        readonly long Size;

        public CachedFileInfo(string path, string name, string nameNoExtension, DateTime lastModifiedTime, long size)
        {
            Path = path;
            Name = name;
            NameNoExtension = nameNoExtension;
            LastModifiedTime = lastModifiedTime;
            Size = size;
        }
    }


    private class FsGameData
    {
        internal readonly List<FsRaceData> Races;
        internal readonly List<FsPaletteData> Palettes;

        public FsGameData(List<FsRaceData> races, List<FsPaletteData> palettes)
        {
            Races = races;
            Palettes = palettes;
        }
    }
    

    private static FileInfo[] LoadSpriteNames2(string path)
    {
        return new DirectoryInfo(path).GetFiles("*.png");
    }

    internal void StartChangeScanning()
    {

        Thread myThread = new Thread(MyFunction);
        myThread.Start();
    }
    
    void MyFunction()
    {
        
        // Some work here
    }


    private static FsGameData LoadFsGameData()
    {
        List<FsRaceData> races = new List<FsRaceData>();

        //string[] customRaceFolders = Directory.GetDirectories("GameData/CustomRaces","*", SearchOption.TopDirectoryOnly);
        string[] customRaceFolders = new DirectoryInfo("GameData/CustomRaces").GetDirectories().Select(it => it.Name).ToArray();


        foreach (string customRaceFolderName in customRaceFolders)
        {
            string raceId = customRaceFolderName.ToLower();
            string raceCode = File.ReadAllText($"GameData/CustomRaces/{customRaceFolderName}/race.lua");

            FileInfo[] raceSpriteNames = new DirectoryInfo($"GameData/CustomRaces/{customRaceFolderName}/Sprites").GetFiles("*.png");

            FsRaceData fsRaceData = new FsRaceData(raceId, customRaceFolderName, raceCode, raceSpriteNames);


            //string[] clothingFolders = Directory.GetDirectories($"GameData/CustomRaces/{customRaceFolderName}/Clothing","*", SearchOption.AllDirectories);
            string[] clothingFolders = new DirectoryInfo($"GameData/CustomRaces/{customRaceFolderName}/Clothing").GetDirectories().Select(it => it.Name).ToArray();
            

            foreach (string clothingFolderName in clothingFolders)
            {
                string clothingId = clothingFolderName.ToLower();
                string clothingCode = File.ReadAllText($"GameData/CustomRaces/{customRaceFolderName}/Clothing/{clothingFolderName}/clothing.lua");
                FileInfo[] clothingSpriteNames = new DirectoryInfo($"GameData/CustomRaces/{customRaceFolderName}/Clothing/{clothingFolderName}/Sprites").GetFiles("*.png");

                FsClothingData fsClothingData = new FsClothingData(raceId, clothingFolderName, clothingId, clothingCode, clothingSpriteNames);
                fsRaceData.Clothing.Add(fsClothingData);
            }
            
            string paletteFolderPath = $"GameData/CustomRaces/{customRaceFolderName}/Palettes";
            if (Directory.Exists(paletteFolderPath))
            {
                FileInfo[] paletteImages = new DirectoryInfo(paletteFolderPath).GetFiles("*.png");

                foreach (FileInfo paletteImage in paletteImages)
                {
                    FsPaletteData fsPaletteData = new FsPaletteData(paletteImage.NameNoExtension().ToLower(), paletteImage);
                    fsRaceData.Palettes.Add(fsPaletteData);
                }
            }
            
            races.Add(fsRaceData);
        }


        List<FsPaletteData> commonPalettes = new List<FsPaletteData>();
        string commonPaletteFolderPath = $"GameData/Palettes";
        if (Directory.Exists(commonPaletteFolderPath))
        {
            FileInfo[] paletteImages = new DirectoryInfo(commonPaletteFolderPath).GetFiles("*.png");

            foreach (FileInfo paletteImage in paletteImages)
            {
                FsPaletteData fsPaletteData = new FsPaletteData(paletteImage.NameNoExtension().ToLower(), paletteImage);
                commonPalettes.Add(fsPaletteData);
            }
        }

        return new FsGameData(races, commonPalettes);
    }


    internal void Refresh()
    {
        Debug.Log("Refreshing Custom");
        FsGameData fsGameData = LoadFsGameData();
        ProcessSprites(fsGameData);
    }

    private bool _needToReloadSprites = false;
    private FsGameData _previosFsGameData = null;

    private bool FsRaceEquals(FsRaceData one, FsRaceData two)
    {
        return false;
    } 
    
    private bool FsDataEquals(FsGameData one, FsGameData two)
    {
        if (one.Races.Count != two.Races.Count) return false;
        for (int i = 0; i < one.Races.Count; i++)
        {
            var oneRace = one.Races[i];
            var twoRace = two.Races[i];
            
            
        }
        return false;
    }
    
    internal void CheckIfRefreshNeeded()
    {
        if (!_needToReloadSprites)
        {
            FsGameData fsGameData = LoadFsGameData();
            
        }
    }

    internal void RefreshIfNeeded()
    {
        
    }
    
    
    internal void LoadAllCustom()
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        FsGameData fsGameData = LoadFsGameData();
        watch.Stop();

        Debug.Log($"Execution Time: {watch.ElapsedMilliseconds} ms");

        ProcessPalettes(fsGameData);
        ProcessSprites(fsGameData);
        ProcessRacesAndClothing(fsGameData);
    }

    private void ProcessPalettes(FsGameData gameData)
    {
        foreach (FsPaletteData fsPaletteData in gameData.Palettes)
        {
            var loader = new WWW("file:///" + fsPaletteData.PaletteImage.FullName);
            RegisterCommonPalette(fsPaletteData.PaletteId, loader.texture);
        }
        
        foreach (FsRaceData fsRaceData in gameData.Races)
        {
            foreach (FsPaletteData fsPaletteData in fsRaceData.Palettes)
            {
                var loader = new WWW("file:///" + fsPaletteData.PaletteImage.FullName);
                RegisterPalette(fsRaceData.RaceId, fsPaletteData.PaletteId, loader.texture);
            }
        }
    }

    private void ProcessSprites(FsGameData gameData)
    {
        List<SpriteToLoad> spriteToLoadList = new List<SpriteToLoad>();

        foreach (FsRaceData fsRaceData in gameData.Races)
        {
            foreach (FileInfo raceSpriteFileInfo in fsRaceData.Sprites)
            {
                string pureName = raceSpriteFileInfo.NameNoExtension().ToLower();
                string key = $"race/{fsRaceData.RaceId}/{pureName}";
                string path = $"GameData/CustomRaces/{fsRaceData.RaceFolderName}/Sprites/{raceSpriteFileInfo.Name}";
                spriteToLoadList.Add(new SpriteToLoad(key, path, raceSpriteFileInfo.LastWriteTimeUtc.ToFileTimeUtc()));
            }

            foreach (FsClothingData fsClothingData in fsRaceData.Clothing)
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

        foreach (var (key, sprite) in sprites)
        {
            string[] split = key.Split('/');
            switch (split[0])
            {
                case "race":
                {
                    string raceId = split[1];
                    string spriteId = split[2];

                    SpriteCollection spriteCollection = _raceSpriteCollections.GetOrSet(raceId, () => new SpriteCollection($"Race sprite collection for {raceId}"));
                    spriteCollection.Set(spriteId, sprite);
                    break;
                }
                case "clothing":
                {
                    string raceId = split[1];
                    string clothingId = split[2];
                    string spriteId = split[3];

                    SpriteCollection spriteCollection = _clothingSpriteCollection.GetOrSet((raceId, clothingId), () => new SpriteCollection($"Clothing sprite collection for {raceId}/{clothingId}"));
                    spriteCollection.Set(spriteId, sprite);
                    break;
                }
                default:
                    throw new Exception($"unknown sprite category {split[0]}");
            }
        }
    }
    
    
    private void ProcessRacesAndClothing(FsGameData gameData)
    {
        foreach (FsRaceData fsRaceData in gameData.Races)
        {
            foreach (FsClothingData fsClothingData in fsRaceData.Clothing)
            {
                LuaBindableClothing clothing = ClothingFromFsData(fsClothingData, fsRaceData);
                _clothings[(fsClothingData.RaceId, fsClothingData.ClothingId)] = clothing;
            }

            RaceFromFsData(fsRaceData);
        }
    }
    
    
    
    private void RegisterPalette(string raceId, string paletteId, Texture2D map)
    {
        List<ColorSwapPalette> palettes = _racePalettes.GetOrSet((raceId, paletteId), () => new List<ColorSwapPalette>());
        palettes.AddRange(TextureToPalettes(map));
    }
    
    private void RegisterCommonPalette(string paletteId, Texture2D map)
    {
        List<ColorSwapPalette> palettes = _commonPalettes.GetOrSet(paletteId, () => new List<ColorSwapPalette>());
        palettes.AddRange(TextureToPalettes(map));
    }

    private static List<ColorSwapPalette> TextureToPalettes(Texture2D map)
    {
        List<ColorSwapPalette> palettes = new List<ColorSwapPalette>();
        
        // Start from 1 because the first row is used for toReplace colors
        for (int pixelY = 1; pixelY < map.height; pixelY++)
        {
            // Unity indexes textures from BOTTOM left. We flip it here
            int topIndex = map.height - 1;
            int pixelYUnity = topIndex - pixelY;

            List<(int, Color)> colors = new List<(int, Color)>();
            for (int i = 0; i < map.width; i++)
            {
                int red = ((Color32)map.GetPixel(i, topIndex)).r; // You can cast Color to Color32 freely
                Color color = map.GetPixel(i, pixelYUnity);
                colors.Add((red, color));
            }
            
            palettes.Add(new ColorSwapPalette(colors));
        }

        return palettes;
    }


    private void RaceFromFsData(FsRaceData fsRaceData)
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

    private LuaBindableClothing ClothingFromFsData(FsClothingData fsClothingData, FsRaceData fsRaceData)
    {
        ClothingScriptUsable clothingScriptUsable = LuaBridge.ScriptPrepClothingFromCode(fsClothingData.ClothingLuaCode, fsClothingData.ClothingId, fsRaceData.RaceId);
        return new LuaBindableClothing(clothingScriptUsable.SetMisc, clothingScriptUsable.CompleteGen);
    }


    // raceId
    private readonly Dictionary<string, SpriteCollection> _raceSpriteCollections = new();

    // raceId, clothingId
    private readonly Dictionary<(string, string), SpriteCollection> _clothingSpriteCollection = new();

    private readonly Dictionary<(string, string), LuaBindableClothing> _clothings = new();
    
    private readonly Dictionary<(string, string), List<ColorSwapPalette>> _racePalettes = new();
    private readonly Dictionary<string, List<ColorSwapPalette>> _commonPalettes = new();


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
        if (_raceSpriteCollections.TryGetValue(raceId, out var res))
        {
            return res;
        }
        else
        {
            return null;
        }
    }


    internal ColorSwapPalette GetRacePalette(Race race, string paletteId, int index) => GetRacePalette(race.Id, paletteId, index);

    internal ColorSwapPalette GetRacePalette(string raceId, string paletteId, int index)
    {
        List<ColorSwapPalette> res;
        if (_racePalettes.TryGetValue((raceId, paletteId), out res))
        {
            //Debug.Log($"Palette for {(raceId, paletteId)} count: {res.Count}, index: {index}");
            return res[index];
        }
        
        // Grab common if race specific isn't defined.
        if (_commonPalettes.TryGetValue(paletteId, out res))
        {
            return res[index];
        }
        
        throw new Exception($"Palette for {(raceId, paletteId)} does not exist");
    }

    internal int GetRacePaletteCount(string raceId, string paletteId)
    {
        if (_racePalettes.TryGetValue((raceId, paletteId), out var res))
        {
            return res.Count;
        }
        else
        {
            throw new Exception($"Palette for {(raceId, paletteId)} does not exist");
        }
    }

    internal SpriteCollection GetClothingSpriteCollection(string raceId, string clothingId)
    {
        if (_clothingSpriteCollection.TryGetValue((raceId, clothingId), out var res))
        {
            return res;
        }
        else
        {
            return null;
        }
    }
}