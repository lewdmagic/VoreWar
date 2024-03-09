using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using MoonSharp.Interpreter;
using UnityEngine;

public class CustomManager
{

    private readonly bool _autoScan = false;
    
    internal CustomManager(bool autoScan)
    {
        _autoScan = autoScan;
    }
    
    
    internal class FsClothingData
    {
        internal readonly string RaceId;
        internal readonly string ClothingId;
        internal readonly string ClothingFolderName;
        internal readonly string ClothingLuaCode;
        internal readonly CachedFileInfo[] Sprites;

        public FsClothingData(string raceId, string clothingFolderName, string clothingId, string clothingLuaCode, CachedFileInfo[] sprites)
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
        internal readonly CachedFileInfo PaletteImage;

        public FsPaletteData(string paletteId, CachedFileInfo paletteImage)
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
        internal readonly CachedFileInfo[] Sprites;
        internal readonly List<FsClothingData> Clothing = new List<FsClothingData>();
        internal readonly List<FsPaletteData> Palettes = new List<FsPaletteData>();

        public FsRaceData(string raceId, string raceFolderName, string raceLuaCode, CachedFileInfo[] sprites)
        {
            RaceId = raceId;
            RaceFolderName = raceFolderName;
            RaceLuaCode = raceLuaCode;
            Sprites = sprites;
        }
    }

    internal class CachedFileInfo
    {
        internal readonly string Path;
        internal readonly string Name;
        internal readonly string NameNoExtension;
        internal readonly DateTime LastModifiedTime;
        internal readonly long Size;

        public CachedFileInfo(string path, string name, string nameNoExtension, DateTime lastModifiedTime, long size)
        {
            Path = path;
            Name = name;
            NameNoExtension = nameNoExtension;
            LastModifiedTime = lastModifiedTime;
            Size = size;
        }

        internal static CachedFileInfo FromFileInfo(FileInfo fileInfo)
        {
            return new CachedFileInfo(
                fileInfo.FullName,
                fileInfo.Name,
                fileInfo.NameNoExtension(),
                fileInfo.LastWriteTimeUtc,
                fileInfo.Length
            );
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


    private static CachedFileInfo[] GetFileInfos(string path, string filter)
    {
        return new DirectoryInfo(path).GetFiles(filter).Select(it => CachedFileInfo.FromFileInfo(it)).ToArray();
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

            CachedFileInfo[] raceSpriteNames = GetFileInfos($"GameData/CustomRaces/{customRaceFolderName}/Sprites", "*.png");
            FsRaceData fsRaceData = new FsRaceData(raceId, customRaceFolderName, raceCode, raceSpriteNames);


            //string[] clothingFolders = Directory.GetDirectories($"GameData/CustomRaces/{customRaceFolderName}/Clothing","*", SearchOption.AllDirectories);
            string[] clothingFolders = new DirectoryInfo($"GameData/CustomRaces/{customRaceFolderName}/Clothing").GetDirectories().Select(it => it.Name).ToArray();
            

            foreach (string clothingFolderName in clothingFolders)
            {
                string clothingId = clothingFolderName.ToLower();
                string clothingCode = File.ReadAllText($"GameData/CustomRaces/{customRaceFolderName}/Clothing/{clothingFolderName}/clothing.lua");
                CachedFileInfo[] clothingSpriteNames = GetFileInfos($"GameData/CustomRaces/{customRaceFolderName}/Clothing/{clothingFolderName}/Sprites", "*.png");

                FsClothingData fsClothingData = new FsClothingData(raceId, clothingFolderName, clothingId, clothingCode, clothingSpriteNames);
                fsRaceData.Clothing.Add(fsClothingData);
            }
            
            string paletteFolderPath = $"GameData/CustomRaces/{customRaceFolderName}/Palettes";
            if (Directory.Exists(paletteFolderPath))
            {
                FileInfo[] paletteImages = new DirectoryInfo(paletteFolderPath).GetFiles("*.png");

                foreach (FileInfo paletteImage in paletteImages)
                {
                    FsPaletteData fsPaletteData = new FsPaletteData(paletteImage.NameNoExtension().ToLower(), CachedFileInfo.FromFileInfo(paletteImage));
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
                FsPaletteData fsPaletteData = new FsPaletteData(paletteImage.NameNoExtension().ToLower(), CachedFileInfo.FromFileInfo(paletteImage));
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
    private bool _needToReloadPalettes = false;
    private FsGameData _previosFsGameData = null;

    // Note: Incomplete
    private bool CachedFileInfoEquals(CachedFileInfo one, CachedFileInfo two)
    {
        if (one.LastModifiedTime != two.LastModifiedTime) return false;
        return true;
    }
    
    // Note: Incomplete
    private bool FsClothingEquals(FsClothingData one, FsClothingData two)
    {
        if (!CachedFileArraysEquals(one.Sprites, two.Sprites))
        {
            return false;
        }
        return true;
    }
    
    // Note: Incomplete
    private bool FsRaceEquals(FsRaceData one, FsRaceData two)
    {
        if (!CachedFileArraysEquals(one.Sprites, two.Sprites))
        {
            return false;
        }

        if (one.Clothing.Count != two.Clothing.Count) return false;

        for (int i = 0; i < one.Clothing.Count; i++)
        {
            var oneClothing = one.Clothing[i];
            var twoClothing = two.Clothing[i];
            if (!FsClothingEquals(oneClothing, twoClothing)) return false;
        }
        
        
        return true;
    }

    private bool CachedFileArraysEquals(CachedFileInfo[] one, CachedFileInfo[] two)
    {
        if (one.Length != two.Length) return false;

        for (int i = 0; i < one.Length; i++)
        {
            var oneRace = one[i];
            var twoRace = two[i];
            if (!CachedFileInfoEquals(oneRace, twoRace)) return false;
        }

        return true;
    }

    // Note: Incomplete
    private bool FsDataEquals(FsGameData one, FsGameData two)
    {
        if (one == two) return true;
        if (one == null || two == null) return false;
        
        if (one.Races.Count != two.Races.Count) return false;
        for (int i = 0; i < one.Races.Count; i++)
        {
            var oneRace = one.Races[i];
            var twoRace = two.Races[i];
            if (!FsRaceEquals(oneRace, twoRace)) return false;
        }
        return true;
    }
    
    private bool FsDataEqualsPalette(FsGameData one, FsGameData two)
    {
        if (one == two) return true;
        if (one == null || two == null) return false;
        
        if (one.Races.Count != two.Races.Count) return false;
        for (int i = 0; i < one.Races.Count; i++)
        {
            var oneRace = one.Races[i];
            var twoRace = two.Races[i];

            if (oneRace.Palettes.Count != twoRace.Palettes.Count) return false;
            for (int paletteIndex = 0; paletteIndex < oneRace.Palettes.Count; paletteIndex++)
            {
                var onePalette = oneRace.Palettes[paletteIndex].PaletteImage;
                var twoPalette = twoRace.Palettes[paletteIndex].PaletteImage;

                if (!CachedFileInfoEquals(onePalette, twoPalette))
                {
                    return false;
                }
            }
        }
        
        // Common
        for (int i = 0; i < one.Palettes.Count; i++)
        {
            var onePalette = one.Palettes[i].PaletteImage;
            var twoPalette = two.Palettes[i].PaletteImage;

            if (!CachedFileInfoEquals(onePalette, twoPalette))
            {
                return false;
            }
        }
        
        return true;
    }
    
    internal void CheckIfRefreshNeeded()
    {
        if (!_autoScan) return;
        FsGameData replacementData = null;
        
        if (!_needToReloadSprites)
        {
            FsGameData fsGameData = LoadFsGameData();
            if (!FsDataEquals(fsGameData, _previosFsGameData))
            {
                _needToReloadSprites = true;
                replacementData = fsGameData;
            }
        }

        if (!_needToReloadPalettes)
        {
            FsGameData fsGameData = LoadFsGameData();
            if (!FsDataEqualsPalette(fsGameData, _previosFsGameData))
            {
                _needToReloadPalettes = true;
                replacementData = fsGameData;
            }
        }

        if (replacementData != null) _previosFsGameData = replacementData;
    }

    internal void RefreshIfNeeded()
    {
        if (!_autoScan) return;
        if (_needToReloadSprites)
        {
            Debug.Log("Auto refreshing Sprites");
            ProcessSprites(_previosFsGameData);
            _needToReloadSprites = false;
        }
        if (_needToReloadPalettes)
        {
            // This creates a memory leak because the old Palettes are not destroyed. 
            // However, the palettes are tiny, and the number of updates is unlikely to
            // reach very high numbers
            Debug.Log("Auto refreshing Palettes");
            ProcessPalettes(_previosFsGameData);
            _needToReloadPalettes = false;
        }
    }
    
    
    internal void LoadAllCustom()
    {
        FsGameData fsGameData = LoadFsGameData();

        ProcessPalettes(fsGameData);
        ProcessSprites(fsGameData);
        ProcessRacesAndClothing(fsGameData);

        _previosFsGameData = fsGameData;
    }

    private void ProcessPalettes(FsGameData gameData)
    {
        foreach (FsPaletteData fsPaletteData in gameData.Palettes)
        {
            var loader = new WWW("file:///" + fsPaletteData.PaletteImage.Path);
            RegisterCommonPalette(fsPaletteData.PaletteId, loader.texture);
        }
        
        foreach (FsRaceData fsRaceData in gameData.Races)
        {
            foreach (FsPaletteData fsPaletteData in fsRaceData.Palettes)
            {
                var loader = new WWW("file:///" + fsPaletteData.PaletteImage.Path);
                RegisterPalette(fsRaceData.RaceId, fsPaletteData.PaletteId, loader.texture);
            }
        }
    }

    private void ProcessSprites(FsGameData gameData)
    {
        List<SpriteToLoad> spriteToLoadList = new List<SpriteToLoad>();

        foreach (FsRaceData fsRaceData in gameData.Races)
        {
            foreach (CachedFileInfo raceSpriteFileInfo in fsRaceData.Sprites)
            {
                string pureName = raceSpriteFileInfo.NameNoExtension.ToLower();
                string key = $"race/{fsRaceData.RaceId}/{pureName}";
                string path = $"GameData/CustomRaces/{fsRaceData.RaceFolderName}/Sprites/{raceSpriteFileInfo.Name}";
                spriteToLoadList.Add(new SpriteToLoad(key, path, raceSpriteFileInfo.LastModifiedTime.ToFileTimeUtc()));
            }

            foreach (FsClothingData fsClothingData in fsRaceData.Clothing)
            {
                foreach (CachedFileInfo clothingSpriteFileInfo in fsClothingData.Sprites)
                {
                    string pureName = clothingSpriteFileInfo.NameNoExtension.ToLower();
                    string key = $"clothing/{fsRaceData.RaceId}/{fsClothingData.ClothingId}/{pureName}";
                    string path = $"GameData/CustomRaces/{fsRaceData.RaceFolderName}/Clothing/{fsClothingData.ClothingFolderName}/Sprites/{clothingSpriteFileInfo.Name}";
                    spriteToLoadList.Add(new SpriteToLoad(key, path, clothingSpriteFileInfo.LastModifiedTime.ToFileTimeUtc()));
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
        _racePalettes[(raceId, paletteId)] = TextureToPalettes(map);
    }
    
    private void RegisterCommonPalette(string paletteId, Texture2D map)
    {
        _commonPalettes[paletteId] = TextureToPalettes(map);
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

        List<ColorSwapPalette> res;
        
        if (_racePalettes.TryGetValue((raceId, paletteId), out res))
        {
            return res.Count;
        }
        
        if (_commonPalettes.TryGetValue(paletteId, out res))
        {
            return res.Count;
        }
        
        throw new Exception($"Palette for {(raceId, paletteId)} does not exist");
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