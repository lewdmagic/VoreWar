using OdinSerializer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class ClothingDiscards
{
    [OdinSerialize]
    internal Vec2i location;
    [OdinSerialize]
    internal int type;
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

    public ClothingDiscards(Vec2i location, Race race, int type, int color, int sortOrder, string name)
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
            AllClothes = new List<IClothingDataSimple>();
            AllClothes.AddRange(ClothingTypes.All);
            AllClothes.AddRange(RaceSpecificClothing.All);
            AllClothes.AddRange(TaurusClothes.TaurusClothingTypes.All);
            AllClothes.AddRange(CruxClothing.CruxClothingTypes.All);
            AllClothes.Add(KangarooClothes.Loincloth1.Loincloth1Instance);
            AllClothes.Add(KangarooClothes.Loincloth2.Loincloth2Instance); //3 and 4 are unneeded because they share with 2
            AllClothes.AddRange(Bees.DiscardData);
            AllClothes.AddRange(Panthers.AllClothing); // TODO move from global
            AllClothes.AddRange(Races.GetRace(Race.Hippos).MiscRaceData.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.GetRace(Race.Hippos).MiscRaceData.AllowedWaistTypes);
            AllClothes.AddRange(Races.GetRace(Race.Merfolk).MiscRaceData.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.GetRace(Race.Merfolk).MiscRaceData.AllowedWaistTypes);
            AllClothes.AddRange(Races.GetRace(Race.Vipers).MiscRaceData.AllowedWaistTypes);
            AllClothes.AddRange(Races.GetRace(Race.Frogs).MiscRaceData.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.GetRace(Race.Frogs).MiscRaceData.AllowedWaistTypes);
            AllClothes.AddRange(Races.GetRace(Race.Imps).MiscRaceData.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.GetRace(Race.Imps).MiscRaceData.AllowedWaistTypes);
            AllClothes.AddRange(Races.GetRace(Race.Imps).MiscRaceData.ExtraMainClothing1Types);
            AllClothes.AddRange(Races.GetRace(Race.Imps).MiscRaceData.ExtraMainClothing2Types);
            AllClothes.AddRange(Races.GetRace(Race.Imps).MiscRaceData.ExtraMainClothing3Types);
            AllClothes.AddRange(Races.GetRace(Race.Imps).MiscRaceData.ExtraMainClothing4Types);
            AllClothes.AddRange(Races.GetRace(Race.Imps).MiscRaceData.ExtraMainClothing5Types);
            AllClothes.AddRange(Races.GetRace(Race.Goblins).MiscRaceData.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.GetRace(Race.Goblins).MiscRaceData.AllowedWaistTypes);
            AllClothes.AddRange(Races.GetRace(Race.Goblins).MiscRaceData.ExtraMainClothing1Types);
            AllClothes.AddRange(Races.GetRace(Race.Goblins).MiscRaceData.ExtraMainClothing2Types);
            AllClothes.AddRange(Races.GetRace(Race.Goblins).MiscRaceData.ExtraMainClothing3Types);
            AllClothes.AddRange(Races.GetRace(Race.Goblins).MiscRaceData.ExtraMainClothing4Types);
            AllClothes.AddRange(Races.GetRace(Race.Goblins).MiscRaceData.ExtraMainClothing5Types);
            AllClothes.AddRange(Races.GetRace(Race.Sharks).MiscRaceData.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.GetRace(Race.Sharks).MiscRaceData.AllowedWaistTypes);
            AllClothes.AddRange(Races.GetRace(Race.Sharks).MiscRaceData.ExtraMainClothing1Types);
            AllClothes.AddRange(Races.GetRace(Race.Komodos).MiscRaceData.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.GetRace(Race.Komodos).MiscRaceData.AllowedWaistTypes);
            AllClothes.AddRange(Races.GetRace(Race.Bats).MiscRaceData.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.GetRace(Race.Cockatrice).MiscRaceData.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.GetRace(Race.Cockatrice).MiscRaceData.AllowedWaistTypes);
            AllClothes.AddRange(Races.GetRace(Race.Deer).MiscRaceData.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.GetRace(Race.Deer).MiscRaceData.AllowedWaistTypes);
            AllClothes.AddRange(Races.GetRace(Race.Deer).MiscRaceData.ExtraMainClothing1Types);
            AllClothes.AddRange(Races.GetRace(Race.Humans).MiscRaceData.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.GetRace(Race.Humans).MiscRaceData.AllowedWaistTypes);
            AllClothes.AddRange(Races.GetRace(Race.Vargul).MiscRaceData.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.GetRace(Race.Vargul).MiscRaceData.AllowedWaistTypes);
            AllClothes = AllClothes.Distinct().ToList();
        }
        var clothingType = AllClothes.Where(s => s.FixedData.Type == type).FirstOrDefault();
        if (clothingType == null)
            return;
        if (clothingType.FixedData.FixedColor == false)
        {
            if (clothingType.FixedData.DiscardUsesPalettes)
                sprite.GetComponentInChildren<SpriteRenderer>().material = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, color).colorSwapMaterial;
            else
                sprite.color = ColorMap.GetClothingColor(color);
        }

        sprite.sprite = clothingType.FixedData.DiscardSprite;
        sprite.sortingOrder = sortOrder;
    }
}