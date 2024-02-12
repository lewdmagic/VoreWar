﻿using OdinSerializer;
using System.Collections.Generic;
using System.Linq;
using Races.Graphics.Implementations.MainRaces;
using UnityEngine;

internal class ClothingDiscards
{
    [OdinSerialize]
    internal Vec2i location;
    [OdinSerialize]
    internal ClothingId type;
    [OdinSerialize]
    internal Race race;
    [OdinSerialize]
    internal int color;
    [OdinSerialize]
    internal int sortOrder;
    [OdinSerialize]
    int angle;
    [OdinSerialize]
    internal string name;

    static List<IClothing> AllClothes = new List<IClothing>();

    public ClothingDiscards(Vec2i location, Race race, ClothingId type, int color, int sortOrder, string name)
    {
        this.location = location;
        this.race = race;
        this.type = type;
        this.color = color;
        this.name = name;
        this.sortOrder = sortOrder;
        angle = State.Rand.Next(40) - 20;
    }

    // TODO JESUS CHRIST this needs to be optimized
    internal void GenerateSpritePrefab(Transform folder)
    {
        var sprite = Object.Instantiate(State.GameManager.DiscardedClothing, new Vector3(location.x - .5f + Random.Range(0, 1f), location.y - .5f + Random.Range(0, 1f)), Quaternion.Euler(0, 0, angle), folder).GetComponent<SpriteRenderer>();

        if (AllClothes.Any() == false)
        {
            // TODO this while thing needs to be improved
            List<IClothing> BeesDiscardData = new List<IClothing>
            {
                BeesClothing.Cuirass.CuirassInstance, BeesClothing.BeeLeaderInstance
            };
            
            AllClothes = new List<IClothing>();
            AllClothes.AddRange(CommonClothing.All);
            AllClothes.AddRange(RaceSpecificClothing.All);
            AllClothes.AddRange(TaurusClothes.TaurusClothingTypes.All);
            AllClothes.AddRange(CruxClothing.CruxClothingTypes.All);
            AllClothes.Add(Loincloth1.Loincloth1Instance);
            AllClothes.Add(Loincloth2.Loincloth2Instance); //3 and 4 are unneeded because they share with 2
            AllClothes.AddRange(BeesDiscardData);
            AllClothes.AddRange(Panthers.AllClothing); // TODO move from global
            AllClothes.AddRange(Races2.GetRace(Race.Hippo).SetupOutput.AllowedMainClothingTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Hippo).SetupOutput.AllowedWaistTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Merfolk).SetupOutput.AllowedMainClothingTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Merfolk).SetupOutput.AllowedWaistTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Viper).SetupOutput.AllowedWaistTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Frog).SetupOutput.AllowedMainClothingTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Frog).SetupOutput.AllowedWaistTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Imp).SetupOutput.AllowedMainClothingTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Imp).SetupOutput.AllowedWaistTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Imp).SetupOutput.ExtraMainClothing1Types);
            AllClothes.AddRange(Races2.GetRace(Race.Imp).SetupOutput.ExtraMainClothing2Types);
            AllClothes.AddRange(Races2.GetRace(Race.Imp).SetupOutput.ExtraMainClothing3Types);
            AllClothes.AddRange(Races2.GetRace(Race.Imp).SetupOutput.ExtraMainClothing4Types);
            AllClothes.AddRange(Races2.GetRace(Race.Imp).SetupOutput.ExtraMainClothing5Types);
            AllClothes.AddRange(Races2.GetRace(Race.Goblin).SetupOutput.AllowedMainClothingTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Goblin).SetupOutput.AllowedWaistTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Goblin).SetupOutput.ExtraMainClothing1Types);
            AllClothes.AddRange(Races2.GetRace(Race.Goblin).SetupOutput.ExtraMainClothing2Types);
            AllClothes.AddRange(Races2.GetRace(Race.Goblin).SetupOutput.ExtraMainClothing3Types);
            AllClothes.AddRange(Races2.GetRace(Race.Goblin).SetupOutput.ExtraMainClothing4Types);
            AllClothes.AddRange(Races2.GetRace(Race.Goblin).SetupOutput.ExtraMainClothing5Types);
            AllClothes.AddRange(Races2.GetRace(Race.Shark).SetupOutput.AllowedMainClothingTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Shark).SetupOutput.AllowedWaistTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Shark).SetupOutput.ExtraMainClothing1Types);
            AllClothes.AddRange(Races2.GetRace(Race.Komodo).SetupOutput.AllowedMainClothingTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Komodo).SetupOutput.AllowedWaistTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Bat).SetupOutput.AllowedMainClothingTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Cockatrice).SetupOutput.AllowedMainClothingTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Cockatrice).SetupOutput.AllowedWaistTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Deer).SetupOutput.AllowedMainClothingTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Deer).SetupOutput.AllowedWaistTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Deer).SetupOutput.ExtraMainClothing1Types);
            AllClothes.AddRange(Races2.GetRace(Race.Human).SetupOutput.AllowedMainClothingTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Human).SetupOutput.AllowedWaistTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Vargul).SetupOutput.AllowedMainClothingTypes);
            AllClothes.AddRange(Races2.GetRace(Race.Vargul).SetupOutput.AllowedWaistTypes);
            AllClothes = AllClothes.Distinct().ToList();
        }
        var clothingType = AllClothes.Where(s => s.FixedData.ClothingId == type).FirstOrDefault();
        if (clothingType == null)
            return;
        if (clothingType.FixedData.FixedColor == false)
        {
            if (clothingType.FixedData.DiscardUsesPalettes)
                sprite.GetComponentInChildren<SpriteRenderer>().material = ColorPaletteMap.GetPalette(SwapType.Clothing, color).colorSwapMaterial;
            else
                sprite.color = ColorMap.GetClothingColor(color);
        }

        sprite.sprite = clothingType.FixedData.DiscardSprite;
        sprite.sortingOrder = sortOrder;
    }
}