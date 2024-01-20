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

    static List<IClothingDataSimple> AllClothes = new List<IClothingDataSimple>();

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
            
            AllClothes = new List<IClothingDataSimple>();
            AllClothes.AddRange(CommonClothing.All);
            AllClothes.AddRange(RaceSpecificClothing.All);
            AllClothes.AddRange(TaurusClothes.TaurusClothingTypes.All);
            AllClothes.AddRange(CruxClothing.CruxClothingTypes.All);
            AllClothes.Add(Loincloth1.Loincloth1Instance);
            AllClothes.Add(Loincloth2.Loincloth2Instance); //3 and 4 are unneeded because they share with 2
            AllClothes.AddRange(BeesDiscardData);
            AllClothes.AddRange(Panthers.AllClothing); // TODO move from global
            AllClothes.AddRange(Races2.GetRace(Race.Hippos).MiscRaceData.AllowedMainClothingTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Hippos).MiscRaceData.AllowedWaistTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Merfolk).MiscRaceData.AllowedMainClothingTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Merfolk).MiscRaceData.AllowedWaistTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Vipers).MiscRaceData.AllowedWaistTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Demifrogs).MiscRaceData.AllowedMainClothingTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Demifrogs).MiscRaceData.AllowedWaistTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Imps).MiscRaceData.AllowedMainClothingTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Imps).MiscRaceData.AllowedWaistTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Imps).MiscRaceData.ExtraMainClothing1TypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Imps).MiscRaceData.ExtraMainClothing2TypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Imps).MiscRaceData.ExtraMainClothing3TypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Imps).MiscRaceData.ExtraMainClothing4TypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Imps).MiscRaceData.ExtraMainClothing5TypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Goblins).MiscRaceData.AllowedMainClothingTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Goblins).MiscRaceData.AllowedWaistTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Goblins).MiscRaceData.ExtraMainClothing1TypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Goblins).MiscRaceData.ExtraMainClothing2TypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Goblins).MiscRaceData.ExtraMainClothing3TypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Goblins).MiscRaceData.ExtraMainClothing4TypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Goblins).MiscRaceData.ExtraMainClothing5TypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Demisharks).MiscRaceData.AllowedMainClothingTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Demisharks).MiscRaceData.AllowedWaistTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Demisharks).MiscRaceData.ExtraMainClothing1TypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Komodos).MiscRaceData.AllowedMainClothingTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Komodos).MiscRaceData.AllowedWaistTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.DemiBats).MiscRaceData.AllowedMainClothingTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Cockatrice).MiscRaceData.AllowedMainClothingTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Cockatrice).MiscRaceData.AllowedWaistTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Deer).MiscRaceData.AllowedMainClothingTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Deer).MiscRaceData.AllowedWaistTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Deer).MiscRaceData.ExtraMainClothing1TypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Humans).MiscRaceData.AllowedMainClothingTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Humans).MiscRaceData.AllowedWaistTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Vargul).MiscRaceData.AllowedMainClothingTypesBasic);
            AllClothes.AddRange(Races2.GetRace(Race.Vargul).MiscRaceData.AllowedWaistTypesBasic);
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