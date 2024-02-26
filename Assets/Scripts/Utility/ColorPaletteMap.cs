using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ColorPaletteMap
{
    private static Dictionary<SwapType, List<ColorSwapPalette>> _swaps;

    internal static ColorSwapPalette Default;

    private static List<Color> _slimeBaseColor;
    private static List<Color> _clothingBaseColor;

    internal static int MixedHairColors;

    internal static ColorSwapPalette FurryBellySwap;

    internal static ColorSwapPalette GetPalette(SwapType swap, int index)
    {
        _swaps.TryGetValue(swap, out var list);
        if (list == null) return Default;
        if (index < list.Count) return list[index];
        return list[0];
    }

    internal static int GetPaletteCount(SwapType swap)
    {
        _swaps.TryGetValue(swap, out var list);
        if (list == null) return 0;
        return list.Count;
    }

    /// <summary>
    ///     Used to get the base slime color for slime scat
    /// </summary>
    internal static Color GetSlimeBaseColor(int index)
    {
        return _slimeBaseColor[index];
    }

    internal static Color GetClothingBaseColor(int index)
    {
        return _clothingBaseColor[index];
    }


    static ColorPaletteMap()
    {
        Default = new ColorSwapPalette(new Dictionary<int, Color>());

        _swaps = new Dictionary<SwapType, List<ColorSwapPalette>>();

        List<ColorSwapPalette> normalHairSwaps = WireUp(SwapType.NormalHair);
        List<ColorSwapPalette> hairRedKeyStrictSwaps = WireUp(SwapType.HairRedKeyStrict);
        List<ColorSwapPalette> clothingSwaps = WireUp(SwapType.Clothing);
        List<ColorSwapPalette> clothing50SpacedSwaps = WireUp(SwapType.Clothing50Spaced);
        List<ColorSwapPalette> clothingSwapsStrict = WireUp(SwapType.ClothingStrict);
        List<ColorSwapPalette> clothingSwapsStrictRedKey = WireUp(SwapType.ClothingStrictRedKey);
        List<ColorSwapPalette> furSwaps = WireUp(SwapType.Fur);
        List<ColorSwapPalette> furStrictSwaps = WireUp(SwapType.FurStrict);
        List<ColorSwapPalette> wildHairSwaps = WireUp(SwapType.WildHair);
        List<ColorSwapPalette> universalHairSwaps = WireUp(SwapType.UniversalHair);
        List<ColorSwapPalette> skinColorSwaps = WireUp(SwapType.Skin);
        List<ColorSwapPalette> redFurColorSwaps = WireUp(SwapType.RedFur);
        List<ColorSwapPalette> redSkinColorSwaps = WireUp(SwapType.RedSkin);
        List<ColorSwapPalette> skinToClothingSwaps = WireUp(SwapType.SkinToClothing);
        List<ColorSwapPalette> mouthColorSwaps = WireUp(SwapType.Mouth);
        List<ColorSwapPalette> eyeColorSwaps = WireUp(SwapType.EyeColor);
        List<ColorSwapPalette> lizardMainSwaps = WireUp(SwapType.LizardMain);
        List<ColorSwapPalette> lizardLightSwaps = WireUp(SwapType.LizardLight);
        List<ColorSwapPalette> slimeMainSwaps = WireUp(SwapType.SlimeMain);
        List<ColorSwapPalette> slimeSubPalettes = WireUp(SwapType.SlimeSub);
        List<ColorSwapPalette> impSwaps = WireUp(SwapType.Imp);
        List<ColorSwapPalette> impRedKey = WireUp(SwapType.ImpRedKey);
        List<ColorSwapPalette> impDarkSwaps = WireUp(SwapType.ImpDark);
        List<ColorSwapPalette> oldImpSwaps = WireUp(SwapType.OldImp);
        List<ColorSwapPalette> oldImpDarkSwaps = WireUp(SwapType.OldImpDark);
        List<ColorSwapPalette> goblinSwaps = WireUp(SwapType.Goblins);
        List<ColorSwapPalette> kangarooSwaps = WireUp(SwapType.Kangaroo);
        List<ColorSwapPalette> feralWolfMane = WireUp(SwapType.FeralWolfMane);
        List<ColorSwapPalette> feralWolfFur = WireUp(SwapType.FeralWolfFur);
        List<ColorSwapPalette> alligatorSwaps = WireUp(SwapType.Alligator);
        List<ColorSwapPalette> cruxSwaps = WireUp(SwapType.Crux);
        List<ColorSwapPalette> beeNewSkinSwaps = WireUp(SwapType.BeeNewSkin);
        List<ColorSwapPalette> driderSkinSwaps = WireUp(SwapType.DriderSkin);
        List<ColorSwapPalette> driderEyesSwaps = WireUp(SwapType.DriderEyes);
        List<ColorSwapPalette> alrauneSkinSwaps = WireUp(SwapType.AlrauneSkin);
        List<ColorSwapPalette> alrauneHairSwaps = WireUp(SwapType.AlrauneHair);
        List<ColorSwapPalette> alrauneFoliageSwaps = WireUp(SwapType.AlrauneFoliage);
        List<ColorSwapPalette> demibatSkinSwaps = WireUp(SwapType.DemibatSkin);
        List<ColorSwapPalette> demibatHumanSkinSwaps = WireUp(SwapType.DemibatHumanSkin);
        List<ColorSwapPalette> mermenSkinSwaps = WireUp(SwapType.MermenSkin);
        List<ColorSwapPalette> mermenHairSwaps = WireUp(SwapType.MermenHair);
        List<ColorSwapPalette> aviansSkinSwaps = WireUp(SwapType.AviansSkin);
        List<ColorSwapPalette> demiantSkinSwaps = WireUp(SwapType.DemiantSkin);
        List<ColorSwapPalette> demifrogSkinSwaps = WireUp(SwapType.DemifrogSkin);
        List<ColorSwapPalette> sharkSkinSwaps = WireUp(SwapType.SharkSkin);
        List<ColorSwapPalette> sharkReversedSwaps = WireUp(SwapType.SharkReversed);
        List<ColorSwapPalette> deerSkinSwaps = WireUp(SwapType.DeerSkin);
        List<ColorSwapPalette> deerLeafSwaps = WireUp(SwapType.DeerLeaf);
        List<ColorSwapPalette> pucaSwaps = WireUp(SwapType.Puca);
        List<ColorSwapPalette> pucaBallSwaps = WireUp(SwapType.PucaBalls);
        List<ColorSwapPalette> hippoSkinSwaps = WireUp(SwapType.HippoSkin);
        List<ColorSwapPalette> viperSkinSwaps = WireUp(SwapType.ViperSkin);
        List<ColorSwapPalette> komodosSkinSwaps = WireUp(SwapType.KomodosSkin);
        List<ColorSwapPalette> komodosReversedSwaps = WireUp(SwapType.KomodosReversed);
        List<ColorSwapPalette> cockatriceSkinSwaps = WireUp(SwapType.CockatriceSkin);
        List<ColorSwapPalette> harvesterSwaps = WireUp(SwapType.Harvester);
        List<ColorSwapPalette> crypterWeaponSwap = WireUp(SwapType.CrypterWeapon);
        List<ColorSwapPalette> batSwaps = WireUp(SwapType.Bat);
        List<ColorSwapPalette> koboldSwaps = WireUp(SwapType.Kobold);
        List<ColorSwapPalette> frogSwaps = WireUp(SwapType.Frog);
        List<ColorSwapPalette> dragonSwaps = WireUp(SwapType.Dragon);
        List<ColorSwapPalette> dragonflySwaps = WireUp(SwapType.Dragonfly);
        List<ColorSwapPalette> fairySpringSkin = WireUp(SwapType.FairySpringSkin);
        List<ColorSwapPalette> fairySpringClothes = WireUp(SwapType.FairySpringClothes);
        List<ColorSwapPalette> fairySummerSkin = WireUp(SwapType.FairySummerSkin);
        List<ColorSwapPalette> fairySummerClothes = WireUp(SwapType.FairySummerClothes);
        List<ColorSwapPalette> fairyFallSkin = WireUp(SwapType.FairyFallSkin);
        List<ColorSwapPalette> fairyFallClothes = WireUp(SwapType.FairyFallClothes);
        List<ColorSwapPalette> fairyWinterSkin = WireUp(SwapType.FairyWinterSkin);
        List<ColorSwapPalette> fairyWinterClothes = WireUp(SwapType.FairyWinterClothes);
        List<ColorSwapPalette> antSwaps = WireUp(SwapType.Ant);
        List<ColorSwapPalette> gryphonSkinSwaps = WireUp(SwapType.GryphonSkin);
        List<ColorSwapPalette> slugSkinSwaps = WireUp(SwapType.SlugSkin);
        List<ColorSwapPalette> pantherSkinSwaps = WireUp(SwapType.PantherSkin);
        List<ColorSwapPalette> pantherHairSwaps = WireUp(SwapType.PantherHair);
        List<ColorSwapPalette> pantherBodyPaintSwaps = WireUp(SwapType.PantherBodyPaint);
        List<ColorSwapPalette> pantherClothesSwaps = WireUp(SwapType.PantherClothes);
        List<ColorSwapPalette> salamanderSkinSwaps = WireUp(SwapType.SalamanderSkin);
        List<ColorSwapPalette> mantisSkinSwaps = WireUp(SwapType.MantisSkin);
        List<ColorSwapPalette> easternDragon = WireUp(SwapType.EasternDragon);
        List<ColorSwapPalette> catfishSkinSwaps = WireUp(SwapType.CatfishSkin);
        List<ColorSwapPalette> gazelleSkinSwaps = WireUp(SwapType.GazelleSkin);
        List<ColorSwapPalette> earthwormSkinSwaps = WireUp(SwapType.EarthwormSkin);
        List<ColorSwapPalette> horseSkinSwaps = WireUp(SwapType.HorseSkin);
        List<ColorSwapPalette> terrorbirdSkinSwaps = WireUp(SwapType.TerrorbirdSkin);
        List<ColorSwapPalette> vargulSkinSwaps = WireUp(SwapType.VargulSkin);
        List<ColorSwapPalette> feralLionsFurSwaps = WireUp(SwapType.FeralLionsFur);
        List<ColorSwapPalette> feralLionsEyesSwaps = WireUp(SwapType.FeralLionsEyes);
        List<ColorSwapPalette> feralLionsManeSwaps = WireUp(SwapType.FeralLionsMane);
        List<ColorSwapPalette> goodraSkinSwaps = WireUp(SwapType.GoodraSkin);
        List<ColorSwapPalette> aabayxSkinSwaps = WireUp(SwapType.AabayxSkin);

        int[] normalIndexes = { 81, 153, 198, 229, 255 };
        Texture2D map = State.GameManager.PaletteDictionary.SimpleHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = NormalReversed(normalIndexes, map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            normalHairSwaps.Add(swap);
            swapDict = RedReversed(map, pixelY);
            swap = new ColorSwapPalette(swapDict, null, 3);
            hairRedKeyStrictSwaps.Add(swap);
            MixedHairColors = normalHairSwaps.Count();
        }

        map = State.GameManager.PaletteDictionary.WildHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = NormalReversed(normalIndexes, map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            normalHairSwaps.Add(swap);
            swapDict = RedReversed(map, pixelY);
            swap = new ColorSwapPalette(swapDict, null, 3);
            hairRedKeyStrictSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.SimpleHair;
        bool[] clear = new bool[256];
        clear[84] = true;
        clear[142] = true;
        clear[158] = true;
        clear[196] = true;
        clear[203] = true;
        clear[254] = true;
        clear[0] = true;
        clear[50] = true;
        clear[100] = true;
        clear[150] = true;
        clear[200] = true;
        clear[250] = true;

        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = NormalReversed(normalIndexes, map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, clear);
            furSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.WildHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = NormalReversed(normalIndexes, map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, clear);
            furSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.SimpleHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = NormalReversed(normalIndexes, map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, null, 2);
            furStrictSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.WildHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = NormalReversed(normalIndexes, map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, null, 2);
            furStrictSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.WildHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = NormalReversed(normalIndexes, map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            wildHairSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.UniversalHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(4, pixelY),
                [100] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 0);
            universalHairSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Skin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = NormalReversed(normalIndexes, map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            skinColorSwaps.Add(swap);
            clear = new bool[256];
            clear[84] = true;
            clear[142] = true;
            clear[196] = true;
            clear[255] = true;
            swap = new ColorSwapPalette(swapDict, clear);
            mouthColorSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Skin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(4, pixelY),
                [100] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 0);
            redSkinColorSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.SimpleHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(4, pixelY),
                [100] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 0);
            redFurColorSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Skin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(4, pixelY),
                [100] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 0);
            redSkinColorSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Eyes;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = Color.clear,
                [normalIndexes[0]] = map.GetPixel(3, pixelY),
                [normalIndexes[1]] = map.GetPixel(2, pixelY),
                [normalIndexes[2]] = map.GetPixel(1, pixelY),
                [normalIndexes[3]] = map.GetPixel(0, pixelY),
                [normalIndexes[4]] = Color.clear,
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            eyeColorSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Lizards;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = NormalReversed(normalIndexes, map, pixelY);
            clear = new bool[256];
            clear[84] = true;
            clear[142] = true;
            clear[196] = true;
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, clear);
            lizardMainSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Lizards;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [normalIndexes[0]] = map.GetPixel(9, pixelY),
                [normalIndexes[1]] = map.GetPixel(8, pixelY),
                [normalIndexes[2]] = map.GetPixel(7, pixelY),
                [normalIndexes[3]] = map.GetPixel(6, pixelY),
                [normalIndexes[4]] = map.GetPixel(5, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            lizardLightSwaps.Add(swap);
        }


        map = State.GameManager.PaletteDictionary.Lizards;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [63] = map.GetPixel(8, pixelY),
                [91] = map.GetPixel(7, pixelY),
                [95] = map.GetPixel(5, pixelY),
                [99] = map.GetPixel(6, pixelY),
                [152] = map.GetPixel(4, pixelY),
                [225] = map.GetPixel(3, pixelY),
                [237] = map.GetPixel(2, pixelY),
                [245] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, null, 2);
            koboldSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Lizards;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [152] = map.GetPixel(4, pixelY),
                [215] = map.GetPixel(6, pixelY),
                [225] = map.GetPixel(3, pixelY),
                [237] = map.GetPixel(2, pixelY),
                [242] = map.GetPixel(5, pixelY),
                [245] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            clear = new bool[256];
            clear[246] = true;
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, clear, 2);
            dragonSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Lizards;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            if (pixelY == 0) //Skip the peachy one
                continue;
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(4, pixelY),
                [158] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            frogSwaps.Add(swap);
        }

        _slimeBaseColor = new List<Color>();
        map = State.GameManager.PaletteDictionary.Slimes;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [normalIndexes[0]] = map.GetPixel(0, pixelY),
                [normalIndexes[1]] = map.GetPixel(1, pixelY),
                [normalIndexes[2]] = map.GetPixel(2, pixelY),
                [normalIndexes[3]] = map.GetPixel(3, pixelY),
                [normalIndexes[4]] = map.GetPixel(4, pixelY),
            };
            Dictionary<int, Color> subSwapDict1 = new Dictionary<int, Color>
            {
                [normalIndexes[0]] = map.GetPixel(5, pixelY),
                [normalIndexes[1]] = map.GetPixel(6, pixelY),
                [normalIndexes[2]] = map.GetPixel(7, pixelY),
                [normalIndexes[3]] = map.GetPixel(8, pixelY),
                [normalIndexes[4]] = map.GetPixel(9, pixelY),
            };
            Dictionary<int, Color> subSwapDict2 = new Dictionary<int, Color>
            {
                [normalIndexes[0]] = map.GetPixel(10, pixelY),
                [normalIndexes[1]] = map.GetPixel(11, pixelY),
                [normalIndexes[2]] = map.GetPixel(12, pixelY),
                [normalIndexes[3]] = map.GetPixel(13, pixelY),
                [normalIndexes[4]] = map.GetPixel(14, pixelY),
            };
            Dictionary<int, Color> subSwapDict3 = new Dictionary<int, Color>
            {
                [normalIndexes[0]] = map.GetPixel(15, pixelY),
                [normalIndexes[1]] = map.GetPixel(16, pixelY),
                [normalIndexes[2]] = map.GetPixel(17, pixelY),
                [normalIndexes[3]] = map.GetPixel(18, pixelY),
                [normalIndexes[4]] = map.GetPixel(19, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            ColorSwapPalette swap2 = new ColorSwapPalette(subSwapDict1);
            ColorSwapPalette swap3 = new ColorSwapPalette(subSwapDict2);
            ColorSwapPalette swap4 = new ColorSwapPalette(subSwapDict3);
            slimeMainSwaps.Add(swap);
            slimeSubPalettes.Add(swap2);
            slimeSubPalettes.Add(swap3);
            slimeSubPalettes.Add(swap4);
            _slimeBaseColor.Add(map.GetPixel(3, pixelY));
        }

        map = State.GameManager.PaletteDictionary.Imps;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(4, pixelY),
                [100] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            clear = new bool[256];
            clear[232] = true;
            clear[239] = true;
            clear[251] = true;
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, null, 0);
            impSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Imps;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [81] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            oldImpSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.ImpsDark;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(4, pixelY),
                [100] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            impDarkSwaps.Add(swap);
            swapDict = RedReversed(map, pixelY);
            swap = new ColorSwapPalette(swapDict);
            impRedKey.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.ImpsDark;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [81] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            oldImpDarkSwaps.Add(swap);
        }

        {
            map = State.GameManager.PaletteDictionary.FurryBelly;
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [normalIndexes[0]] = map.GetPixel(4, 0),
                [normalIndexes[1]] = map.GetPixel(3, 0),
                [normalIndexes[2]] = map.GetPixel(2, 0),
                [normalIndexes[3]] = map.GetPixel(1, 0),
                [normalIndexes[4]] = map.GetPixel(0, 0),
            };
            FurryBellySwap = new ColorSwapPalette(swapDict);
        }

        map = State.GameManager.PaletteDictionary.FairySkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = RedReversed(map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, null, 2);
            if (pixelY < 2)
                fairySpringSkin.Add(swap);
            else if (pixelY < 5)
                fairySummerSkin.Add(swap);
            else if (pixelY < 8)
                fairyFallSkin.Add(swap);
            else
                fairyWinterSkin.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.FairyClothes;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [123] = map.GetPixel(3, pixelY),
                [189] = map.GetPixel(2, pixelY),
                [233] = map.GetPixel(1, pixelY),
                [244] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, null, 2);
            if (pixelY < 5)
                fairyWinterClothes.Add(swap);
            else if (pixelY < 10)
                fairyFallClothes.Add(swap);
            else if (pixelY < 14)
                fairySummerClothes.Add(swap);
            else
                fairySpringClothes.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Goblins;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(4, pixelY),
                [100] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            goblinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Lizards;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [152] = map.GetPixel(4, pixelY),
                [225] = map.GetPixel(2, pixelY),
                [237] = map.GetPixel(1, pixelY),
                [245] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, null, 3);
            crypterWeaponSwap.Add(swap);
        }

        _clothingBaseColor = new List<Color>();
        map = State.GameManager.PaletteDictionary.SimpleHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = RedReversed(map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            _clothingBaseColor.Add(map.GetPixel(1, pixelY));
            clothingSwaps.Add(swap);
            swap = new ColorSwapPalette(swapDict, null, 1);
            clothingSwapsStrict.Add(swap);
            swapDict = RedReversed(map, pixelY);
            clear = new bool[256];
            clear[251] = true; //This is to avoid the succbus yellow buckles that are 251
            swap = new ColorSwapPalette(swapDict, clear, 1);
            clothingSwapsStrictRedKey.Add(swap);
            swapDict = NormalReversed(normalIndexes, map, pixelY);
            swap = new ColorSwapPalette(swapDict);
            skinToClothingSwaps.Add(swap);
            swapDict = new Dictionary<int, Color>()
            {
                [50] = map.GetPixel(4, pixelY),
                [100] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            swap = new ColorSwapPalette(swapDict, null, 0);
            clothing50SpacedSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Lizards;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = RedReversed(map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            _clothingBaseColor.Add(map.GetPixel(1, pixelY));
            clothingSwaps.Add(swap);
            swap = new ColorSwapPalette(swapDict, null, 1);
            clothingSwapsStrict.Add(swap);
            swapDict = RedReversed(map, pixelY);
            swap = new ColorSwapPalette(swapDict, clear, 1);
            clothingSwapsStrictRedKey.Add(swap);
            swapDict = NormalReversed(normalIndexes, map, pixelY);
            swap = new ColorSwapPalette(swapDict);
            skinToClothingSwaps.Add(swap);
            swapDict = new Dictionary<int, Color>()
            {
                [50] = map.GetPixel(4, pixelY),
                [100] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            swap = new ColorSwapPalette(swapDict, null, 0);
            clothing50SpacedSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Slimes;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = Red(map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            _clothingBaseColor.Add(map.GetPixel(1, pixelY));
            clothingSwaps.Add(swap);
            swap = new ColorSwapPalette(swapDict, null, 1);
            clothingSwapsStrict.Add(swap);
            swapDict = Red(map, pixelY);
            swap = new ColorSwapPalette(swapDict, clear, 1);
            clothingSwapsStrictRedKey.Add(swap);
            swapDict = Normal(normalIndexes, map, pixelY);
            swap = new ColorSwapPalette(swapDict);
            skinToClothingSwaps.Add(swap);
            swapDict = new Dictionary<int, Color>()
            {
                [50] = map.GetPixel(0, pixelY),
                [100] = map.GetPixel(1, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(3, pixelY),
                [250] = map.GetPixel(4, pixelY),
            };
            swap = new ColorSwapPalette(swapDict, null, 0);
            clothing50SpacedSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.WildHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = RedReversed(map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            _clothingBaseColor.Add(map.GetPixel(1, pixelY));
            clothingSwaps.Add(swap);
            swap = new ColorSwapPalette(swapDict, null, 1);
            clothingSwapsStrict.Add(swap);
            swapDict = RedReversed(map, pixelY);
            swap = new ColorSwapPalette(swapDict, clear, 1);
            clothingSwapsStrictRedKey.Add(swap);
            swapDict = NormalReversed(normalIndexes, map, pixelY);
            swap = new ColorSwapPalette(swapDict);
            skinToClothingSwaps.Add(swap);
            swapDict = new Dictionary<int, Color>()
            {
                [50] = map.GetPixel(4, pixelY),
                [100] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            swap = new ColorSwapPalette(swapDict, null, 0);
            clothing50SpacedSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Kangaroo;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [81] = map.GetPixel(10, pixelY),
                [89] = map.GetPixel(5, pixelY),
                [102] = map.GetPixel(9, pixelY),
                [128] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(8, pixelY),
                [168] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(7, pixelY),
                [217] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(6, pixelY),
                [242] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            kangarooSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.FeralWolfMane;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [91] = map.GetPixel(2, pixelY),
                [95] = map.GetPixel(0, pixelY),
                [99] = map.GetPixel(1, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 4);
            feralWolfMane.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.FeralWolfFur;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [152] = map.GetPixel(3, pixelY),
                [225] = map.GetPixel(2, pixelY),
                [236] = map.GetPixel(1, pixelY),
                [244] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            feralWolfFur.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Alligators;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [28] = map.GetPixel(4, pixelY),
                [51] = map.GetPixel(8, pixelY),
                [57] = map.GetPixel(3, pixelY),
                [92] = map.GetPixel(2, pixelY),
                [128] = map.GetPixel(7, pixelY),
                [166] = map.GetPixel(6, pixelY),
                [179] = map.GetPixel(1, pixelY),
                [204] = map.GetPixel(5, pixelY),
                [217] = map.GetPixel(0, pixelY),
                [256] = Color.white
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            alligatorSwaps.Add(swap);
        }


        map = State.GameManager.PaletteDictionary.Crux;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [19] = map.GetPixel(5, pixelY),
                [33] = map.GetPixel(6, pixelY),
                [38] = map.GetPixel(0, pixelY),
                [64] = map.GetPixel(7, pixelY),
                [77] = map.GetPixel(1, pixelY),
                [96] = map.GetPixel(8, pixelY),
                [115] = map.GetPixel(9, pixelY),
                [128] = map.GetPixel(2, pixelY),
                [135] = map.GetPixel(10, pixelY),
                [191] = map.GetPixel(3, pixelY),
                [196] = map.GetPixel(11, pixelY),
                [217] = map.GetPixel(4, pixelY),
                [230] = map.GetPixel(12, pixelY)
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            cruxSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.BeeNewSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(11, pixelY),
                [22] = map.GetPixel(10, pixelY),
                [42] = map.GetPixel(9, pixelY),
                [62] = map.GetPixel(8, pixelY),
                [92] = map.GetPixel(7, pixelY),
                [113] = map.GetPixel(6, pixelY),
                [133] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [175] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            beeNewSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.DriderSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [33] = map.GetPixel(7, pixelY),
                [46] = map.GetPixel(13, pixelY),
                [51] = map.GetPixel(6, pixelY),
                [74] = map.GetPixel(12, pixelY),
                [81] = map.GetPixel(5, pixelY),
                [98] = map.GetPixel(11, pixelY),
                [113] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [166] = map.GetPixel(10, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [235] = map.GetPixel(9, pixelY),
                [240] = map.GetPixel(8, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            driderSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.DriderEyes;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [33] = map.GetPixel(4, pixelY),
                [51] = map.GetPixel(3, pixelY),
                [113] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            driderEyesSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.AlrauneSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [81] = map.GetPixel(4, pixelY),
                [84] = map.GetPixel(7, pixelY),
                [142] = map.GetPixel(6, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [196] = map.GetPixel(5, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            alrauneSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.AlrauneHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [81] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            alrauneHairSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.AlrauneFoliage;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [25] = map.GetPixel(8, pixelY),
                [33] = map.GetPixel(7, pixelY),
                [42] = map.GetPixel(6, pixelY),
                [81] = map.GetPixel(5, pixelY),
                [113] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            alrauneFoliageSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.DemibatSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(13, pixelY),
                [32] = map.GetPixel(12, pixelY),
                [47] = map.GetPixel(11, pixelY),
                [64] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(9, pixelY),
                [96] = map.GetPixel(8, pixelY),
                [109] = map.GetPixel(7, pixelY),
                [123] = map.GetPixel(6, pixelY),
                [138] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            demibatSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Skin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [153] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            demibatHumanSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.MermenSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(13, pixelY),
                [25] = map.GetPixel(12, pixelY),
                [33] = map.GetPixel(11, pixelY),
                [42] = map.GetPixel(10, pixelY),
                [62] = map.GetPixel(9, pixelY),
                [81] = map.GetPixel(8, pixelY),
                [97] = map.GetPixel(7, pixelY),
                [113] = map.GetPixel(6, pixelY),
                [133] = map.GetPixel(5, pixelY),
                [143] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            mermenSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.MermenHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [81] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            mermenHairSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.AviansSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(16, pixelY),
                [17] = map.GetPixel(15, pixelY),
                [32] = map.GetPixel(14, pixelY),
                [47] = map.GetPixel(13, pixelY),
                [62] = map.GetPixel(12, pixelY),
                [81] = map.GetPixel(11, pixelY),
                [86] = map.GetPixel(10, pixelY),
                [92] = map.GetPixel(9, pixelY),
                [103] = map.GetPixel(8, pixelY),
                [113] = map.GetPixel(7, pixelY),
                [133] = map.GetPixel(6, pixelY),
                [153] = map.GetPixel(5, pixelY),
                [175] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            aviansSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.DemiantSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(10, pixelY),
                [17] = map.GetPixel(9, pixelY),
                [32] = map.GetPixel(8, pixelY),
                [47] = map.GetPixel(7, pixelY),
                [62] = map.GetPixel(6, pixelY),
                [81] = map.GetPixel(5, pixelY),
                [120] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            demiantSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.DemifrogSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(13, pixelY),
                [17] = map.GetPixel(12, pixelY),
                [32] = map.GetPixel(11, pixelY),
                [40] = map.GetPixel(20, pixelY),
                [47] = map.GetPixel(10, pixelY),
                [55] = map.GetPixel(19, pixelY),
                [62] = map.GetPixel(9, pixelY),
                [70] = map.GetPixel(18, pixelY),
                [81] = map.GetPixel(4, pixelY),
                [96] = map.GetPixel(8, pixelY),
                [109] = map.GetPixel(7, pixelY),
                [123] = map.GetPixel(6, pixelY),
                [138] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [175] = map.GetPixel(17, pixelY),
                [186] = map.GetPixel(16, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [213] = map.GetPixel(15, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [242] = map.GetPixel(14, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            demifrogSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.SharkSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(12, pixelY),
                [32] = map.GetPixel(11, pixelY),
                [47] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(9, pixelY),
                [96] = map.GetPixel(8, pixelY),
                [109] = map.GetPixel(7, pixelY),
                [123] = map.GetPixel(6, pixelY),
                [138] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            sharkSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.SharkSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(12, pixelY),
                [32] = map.GetPixel(11, pixelY),
                [47] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(4, pixelY),
                [96] = map.GetPixel(3, pixelY),
                [109] = map.GetPixel(2, pixelY),
                [123] = map.GetPixel(1, pixelY),
                [138] = map.GetPixel(0, pixelY),
                [153] = map.GetPixel(9, pixelY),
                [198] = map.GetPixel(8, pixelY),
                [214] = map.GetPixel(7, pixelY),
                [229] = map.GetPixel(6, pixelY),
                [255] = map.GetPixel(5, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            sharkReversedSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.DeerSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(12, pixelY),
                [32] = map.GetPixel(11, pixelY),
                [47] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(9, pixelY),
                [96] = map.GetPixel(8, pixelY),
                [109] = map.GetPixel(7, pixelY),
                [123] = map.GetPixel(6, pixelY),
                [138] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            deerSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.DeerLeaf;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [100] = map.GetPixel(2, pixelY),
                [150] = map.GetPixel(1, pixelY),
                [200] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            deerLeafSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Puca;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [5] = map.GetPixel(7, pixelY),
                [23] = map.GetPixel(6, pixelY),
                [70] = map.GetPixel(11, pixelY),
                [84] = map.GetPixel(5, pixelY),
                [105] = map.GetPixel(10, pixelY),
                [140] = map.GetPixel(9, pixelY),
                [142] = map.GetPixel(4, pixelY),
                [152] = map.GetPixel(3, pixelY),
                [191] = map.GetPixel(8, pixelY),
                [225] = map.GetPixel(2, pixelY),
                [236] = map.GetPixel(1, pixelY),
                [244] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 4);
            pucaSwaps.Add(swap);

            swapDict = new Dictionary<int, Color>
            {
                [normalIndexes[0]] = map.GetPixel(7, pixelY),
                [normalIndexes[1]] = map.GetPixel(6, pixelY),
                [normalIndexes[2]] = map.GetPixel(5, pixelY),
                [normalIndexes[3]] = map.GetPixel(4, pixelY),
            };
            swap = new ColorSwapPalette(swapDict);
            pucaBallSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.HippoSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [33] = map.GetPixel(11, pixelY),
                [42] = map.GetPixel(10, pixelY),
                [62] = map.GetPixel(9, pixelY),
                [81] = map.GetPixel(8, pixelY),
                [84] = map.GetPixel(7, pixelY),
                [142] = map.GetPixel(6, pixelY),
                [153] = map.GetPixel(5, pixelY),
                [178] = map.GetPixel(4, pixelY),
                [196] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            hippoSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.ViperSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(13, pixelY),
                [22] = map.GetPixel(12, pixelY),
                [42] = map.GetPixel(11, pixelY),
                [62] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(9, pixelY),
                [97] = map.GetPixel(8, pixelY),
                [113] = map.GetPixel(7, pixelY),
                [133] = map.GetPixel(6, pixelY),
                [153] = map.GetPixel(5, pixelY),
                [175] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            viperSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.KomodosSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(12, pixelY),
                [32] = map.GetPixel(11, pixelY),
                [47] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(9, pixelY),
                [96] = map.GetPixel(8, pixelY),
                [109] = map.GetPixel(7, pixelY),
                [123] = map.GetPixel(6, pixelY),
                [138] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            komodosSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.CockatriceSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [81] = map.GetPixel(9, pixelY),
                [96] = map.GetPixel(8, pixelY),
                [109] = map.GetPixel(7, pixelY),
                [123] = map.GetPixel(6, pixelY),
                [138] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            cockatriceSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.KomodosSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(12, pixelY),
                [32] = map.GetPixel(11, pixelY),
                [47] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(4, pixelY),
                [96] = map.GetPixel(3, pixelY),
                [109] = map.GetPixel(2, pixelY),
                [123] = map.GetPixel(1, pixelY),
                [138] = map.GetPixel(0, pixelY),
                [153] = map.GetPixel(9, pixelY),
                [198] = map.GetPixel(8, pixelY),
                [214] = map.GetPixel(7, pixelY),
                [229] = map.GetPixel(6, pixelY),
                [255] = map.GetPixel(5, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            komodosReversedSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.VargulSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [32] = map.GetPixel(11, pixelY),
                [47] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(9, pixelY),
                [96] = map.GetPixel(8, pixelY),
                [109] = map.GetPixel(7, pixelY),
                [123] = map.GetPixel(6, pixelY),
                [138] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            vargulSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Harvester;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(0, pixelY),
                [5] = map.GetPixel(1, pixelY),
                [9] = map.GetPixel(2, pixelY),
                [16] = map.GetPixel(3, pixelY),
                [61] = map.GetPixel(4, pixelY),
                [68] = map.GetPixel(5, pixelY),
                [94] = map.GetPixel(6, pixelY),
                [97] = map.GetPixel(7, pixelY),
                [122] = map.GetPixel(8, pixelY),
                [129] = map.GetPixel(9, pixelY),
                [172] = map.GetPixel(10, pixelY),
                [176] = map.GetPixel(11, pixelY),
                [204] = map.GetPixel(12, pixelY),
                [217] = map.GetPixel(13, pixelY),
                [255] = map.GetPixel(14, pixelY)
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            harvesterSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Bats;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(0, pixelY),
                [5] = map.GetPixel(1, pixelY),
                [9] = map.GetPixel(2, pixelY),
                [16] = map.GetPixel(3, pixelY),
                [61] = map.GetPixel(4, pixelY),
                [68] = map.GetPixel(5, pixelY),
                [94] = map.GetPixel(6, pixelY),
                [97] = map.GetPixel(7, pixelY),
                [122] = map.GetPixel(8, pixelY),
                [129] = map.GetPixel(9, pixelY),
                [172] = map.GetPixel(10, pixelY),
                [176] = map.GetPixel(11, pixelY),
                [204] = map.GetPixel(12, pixelY),
                [217] = map.GetPixel(13, pixelY),
                [255] = map.GetPixel(14, pixelY)
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            batSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Dragonfly;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(0, pixelY),
                [10] = map.GetPixel(1, pixelY),
                [20] = map.GetPixel(2, pixelY),
                [30] = map.GetPixel(3, pixelY),
                [120] = map.GetPixel(4, pixelY),
                [150] = map.GetPixel(5, pixelY),
                [160] = map.GetPixel(6, pixelY),
                [180] = map.GetPixel(7, pixelY),
                [200] = map.GetPixel(8, pixelY),
                [255] = map.GetPixel(9, pixelY)
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            dragonflySwaps.Add(swap);
        }


        map = State.GameManager.PaletteDictionary.Ant;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(0, pixelY),
                [10] = map.GetPixel(1, pixelY),
                [20] = map.GetPixel(2, pixelY),
                [30] = map.GetPixel(3, pixelY),
                [200] = map.GetPixel(4, pixelY),
                [230] = map.GetPixel(5, pixelY),
                [255] = map.GetPixel(6, pixelY)
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            antSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.GryphonSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [25] = map.GetPixel(11, pixelY),
                [33] = map.GetPixel(10, pixelY),
                [42] = map.GetPixel(9, pixelY),
                [81] = map.GetPixel(8, pixelY),
                [113] = map.GetPixel(7, pixelY),
                [133] = map.GetPixel(6, pixelY),
                [153] = map.GetPixel(5, pixelY),
                [198] = map.GetPixel(4, pixelY),
                [206] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            gryphonSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.SlugSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [25] = map.GetPixel(15, pixelY),
                [33] = map.GetPixel(14, pixelY),
                [42] = map.GetPixel(13, pixelY),
                [62] = map.GetPixel(12, pixelY),
                [81] = map.GetPixel(11, pixelY),
                [97] = map.GetPixel(10, pixelY),
                [113] = map.GetPixel(9, pixelY),
                [133] = map.GetPixel(8, pixelY),
                [153] = map.GetPixel(7, pixelY),
                [168] = map.GetPixel(6, pixelY),
                [183] = map.GetPixel(5, pixelY),
                [198] = map.GetPixel(4, pixelY),
                [206] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            slugSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Panthers;
        for (int pixelY = 0; pixelY < 8; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(0, pixelY),
                [100] = map.GetPixel(1, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(3, pixelY),
                [250] = map.GetPixel(4, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 0);
            pantherClothesSwaps.Add(swap);
        }

        for (int pixelY = 8; pixelY < 11; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(0, pixelY),
                [100] = map.GetPixel(1, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(3, pixelY),
                [250] = map.GetPixel(4, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 0);
            pantherBodyPaintSwaps.Add(swap);
        }

        for (int pixelY = 11; pixelY < 16; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(0, pixelY),
                [100] = map.GetPixel(1, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(3, pixelY),
                [250] = map.GetPixel(4, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 0);
            pantherHairSwaps.Add(swap);
        }

        for (int pixelY = 16; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(0, pixelY),
                [100] = map.GetPixel(1, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(3, pixelY),
                [250] = map.GetPixel(4, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 0);
            pantherSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.SalamanderSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(14, pixelY),
                [25] = map.GetPixel(13, pixelY),
                [33] = map.GetPixel(12, pixelY),
                [42] = map.GetPixel(11, pixelY),
                [62] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(9, pixelY),
                [113] = map.GetPixel(8, pixelY),
                [133] = map.GetPixel(7, pixelY),
                [153] = map.GetPixel(6, pixelY),
                [183] = map.GetPixel(5, pixelY),
                [198] = map.GetPixel(4, pixelY),
                [206] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            salamanderSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Horsepalettes;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(0, pixelY),
                [100] = map.GetPixel(1, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(3, pixelY),
                [250] = map.GetPixel(4, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 0);
            horseSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.MantisSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(14, pixelY),
                [17] = map.GetPixel(13, pixelY),
                [32] = map.GetPixel(12, pixelY),
                [47] = map.GetPixel(11, pixelY),
                [62] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(9, pixelY),
                [92] = map.GetPixel(8, pixelY),
                [103] = map.GetPixel(7, pixelY),
                [113] = map.GetPixel(6, pixelY),
                [133] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [175] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [214] = map.GetPixel(1, pixelY),
                [229] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            mantisSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.EasternDragon;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(0, pixelY),
                [20] = map.GetPixel(5, pixelY),
                [30] = map.GetPixel(1, pixelY),
                [40] = map.GetPixel(6, pixelY),
                [50] = map.GetPixel(2, pixelY),
                [60] = map.GetPixel(7, pixelY),
                [75] = map.GetPixel(3, pixelY),
                [80] = map.GetPixel(8, pixelY),
                [90] = map.GetPixel(10, pixelY),
                [100] = map.GetPixel(4, pixelY),
                [110] = map.GetPixel(9, pixelY),
                [130] = map.GetPixel(11, pixelY),
                [140] = map.GetPixel(14, pixelY),
                [160] = map.GetPixel(15, pixelY),
                [180] = map.GetPixel(12, pixelY),
                [220] = map.GetPixel(13, pixelY)
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            easternDragon.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.CatfishSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(11, pixelY),
                [17] = map.GetPixel(10, pixelY),
                [32] = map.GetPixel(9, pixelY),
                [47] = map.GetPixel(8, pixelY),
                [62] = map.GetPixel(7, pixelY),
                [81] = map.GetPixel(6, pixelY),
                [103] = map.GetPixel(5, pixelY),
                [133] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [175] = map.GetPixel(2, pixelY),
                [198] = map.GetPixel(1, pixelY),
                [229] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            catfishSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.GazelleSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(12, pixelY),
                [22] = map.GetPixel(11, pixelY),
                [42] = map.GetPixel(10, pixelY),
                [62] = map.GetPixel(9, pixelY),
                [92] = map.GetPixel(8, pixelY),
                [103] = map.GetPixel(7, pixelY),
                [113] = map.GetPixel(6, pixelY),
                [133] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [175] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            gazelleSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.EarthwormSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(6, pixelY),
                [47] = map.GetPixel(5, pixelY),
                [81] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            earthwormSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.TerrorbirdSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [32] = map.GetPixel(11, pixelY),
                [47] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(4, pixelY),
                [96] = map.GetPixel(3, pixelY),
                [109] = map.GetPixel(2, pixelY),
                [123] = map.GetPixel(1, pixelY),
                [138] = map.GetPixel(0, pixelY),
                [153] = map.GetPixel(9, pixelY),
                [198] = map.GetPixel(8, pixelY),
                [214] = map.GetPixel(7, pixelY),
                [229] = map.GetPixel(6, pixelY),
                [255] = map.GetPixel(5, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            terrorbirdSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.GoodraSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [34] = map.GetPixel(26, pixelY),
                [42] = map.GetPixel(25, pixelY),
                [43] = map.GetPixel(18, pixelY),
                [52] = map.GetPixel(24, pixelY),
                [53] = map.GetPixel(17, pixelY),
                [66] = map.GetPixel(16, pixelY),
                [75] = map.GetPixel(22, pixelY),
                [80] = map.GetPixel(9, pixelY),
                [89] = map.GetPixel(23, pixelY),
                [91] = map.GetPixel(14, pixelY),
                [93] = map.GetPixel(21, pixelY),
                [99] = map.GetPixel(8, pixelY),
                [103] = map.GetPixel(15, pixelY),
                [112] = map.GetPixel(34, pixelY),
                [113] = map.GetPixel(13, pixelY),
                [115] = map.GetPixel(20, pixelY),
                [123] = map.GetPixel(7, pixelY),
                [130] = map.GetPixel(5, pixelY),
                [135] = map.GetPixel(12, pixelY),
                [139] = map.GetPixel(33, pixelY),
                [140] = map.GetPixel(11, pixelY),
                [147] = map.GetPixel(19, pixelY),
                [154] = map.GetPixel(6, pixelY),
                [162] = map.GetPixel(4, pixelY),
                [168] = map.GetPixel(10, pixelY),
                [173] = map.GetPixel(32, pixelY),
                [194] = map.GetPixel(31, pixelY),
                [201] = map.GetPixel(3, pixelY),
                [202] = map.GetPixel(29, pixelY),
                [205] = map.GetPixel(1, pixelY),
                [215] = map.GetPixel(2, pixelY),
                [251] = map.GetPixel(28, pixelY),
                [252] = map.GetPixel(27, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            goodraSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.AabayxSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [1] = map.GetPixel(0, pixelY),
                [27] = map.GetPixel(1, pixelY),
                [113] = map.GetPixel(2, pixelY),
                [164] = map.GetPixel(3, pixelY),
                [255] = map.GetPixel(4, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            aabayxSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.FeralLionsFur;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = Color.black,
                [132] = map.GetPixel(8, pixelY),
                [136] = map.GetPixel(7, pixelY),
                [203] = map.GetPixel(6, pixelY),
                [219] = map.GetPixel(5, pixelY),
                [224] = map.GetPixel(4, pixelY),
                [235] = map.GetPixel(3, pixelY),
                [236] = map.GetPixel(2, pixelY),
                [240] = map.GetPixel(1, pixelY),
                [251] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 0);
            feralLionsFurSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.FeralLionsEyes;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [253] = map.GetPixel(0, pixelY),
                [140] = map.GetPixel(1, pixelY),
                [254] = map.GetPixel(2, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 5);
            feralLionsEyesSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.FeralLionsMane;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [87] = map.GetPixel(2, pixelY),
                [91] = map.GetPixel(1, pixelY),
                [108] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            feralLionsManeSwaps.Add(swap);
        }
    }

    private static Dictionary<int, Color> Red(Texture2D map, int pixelY)
    {
        return new Dictionary<int, Color>
        {
            [152] = map.GetPixel(0, pixelY),
            [225] = map.GetPixel(1, pixelY),
            [236] = map.GetPixel(2, pixelY),
            [244] = map.GetPixel(3, pixelY),
            [250] = map.GetPixel(4, pixelY),
        };
    }

    private static Dictionary<int, Color> RedReversed(Texture2D map, int pixelY)
    {
        return new Dictionary<int, Color>
        {
            [152] = map.GetPixel(4, pixelY),
            [225] = map.GetPixel(3, pixelY),
            [237] = map.GetPixel(2, pixelY),
            [245] = map.GetPixel(1, pixelY),
            [250] = map.GetPixel(0, pixelY),
        };
    }

    private static Dictionary<int, Color> Normal(int[] normalIndexes, Texture2D map, int pixelY)
    {
        return new Dictionary<int, Color>
        {
            [normalIndexes[0]] = map.GetPixel(0, pixelY),
            [normalIndexes[1]] = map.GetPixel(1, pixelY),
            [normalIndexes[2]] = map.GetPixel(2, pixelY),
            [normalIndexes[3]] = map.GetPixel(3, pixelY),
            [normalIndexes[4]] = map.GetPixel(4, pixelY)
        };
    }

    private static Dictionary<int, Color> NormalReversed(int[] normalIndexes, Texture2D map, int pixelY)
    {
        return new Dictionary<int, Color>
        {
            [normalIndexes[0]] = map.GetPixel(4, pixelY),
            [normalIndexes[1]] = map.GetPixel(3, pixelY),
            [normalIndexes[2]] = map.GetPixel(2, pixelY),
            [normalIndexes[3]] = map.GetPixel(1, pixelY),
            [normalIndexes[4]] = map.GetPixel(0, pixelY)
        };
    }

    private static List<ColorSwapPalette> WireUp(SwapType swapType)
    {
        List<ColorSwapPalette> palette = new List<ColorSwapPalette>();
        _swaps[swapType] = palette;
        return palette;
    }
}