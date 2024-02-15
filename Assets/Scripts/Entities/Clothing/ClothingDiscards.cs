using OdinSerializer;
using System.Collections.Generic;
using System.Linq;
using Races.Graphics.Implementations.MainRaces;
using UnityEngine;

internal class ClothingDiscards
{
    [OdinSerialize]
    private Vec2I _location;

    internal Vec2I Location { get => _location; set => _location = value; }

    [OdinSerialize]
    private ClothingId _type;

    internal ClothingId Type { get => _type; set => _type = value; }

    [OdinSerialize]
    private Race _race;

    internal Race Race { get => _race; set => _race = value; }

    [OdinSerialize]
    private int _color;

    internal int Color { get => _color; set => _color = value; }

    [OdinSerialize]
    private int _sortOrder;

    internal int SortOrder { get => _sortOrder; set => _sortOrder = value; }

    [OdinSerialize]
    private int _angle;

    private int Angle { get => _angle; set => _angle = value; }

    [OdinSerialize]
    private string _name;

    internal string Name { get => _name; set => _name = value; }

    private static List<IClothing> _allClothes = new List<IClothing>();

    public ClothingDiscards(Vec2I location, Race race, ClothingId type, int color, int sortOrder, string name)
    {
        this.Location = location;
        this.Race = race;
        this.Type = type;
        this.Color = color;
        this.Name = name;
        this.SortOrder = sortOrder;
        Angle = State.Rand.Next(40) - 20;
    }

    // TODO JESUS CHRIST this needs to be optimized
    internal void GenerateSpritePrefab(Transform folder)
    {
        var sprite = Object.Instantiate(State.GameManager.DiscardedClothing, new Vector3(Location.X - .5f + Random.Range(0, 1f), Location.Y - .5f + Random.Range(0, 1f)), Quaternion.Euler(0, 0, Angle), folder).GetComponent<SpriteRenderer>();

        if (_allClothes.Any() == false)
        {
            // TODO this while thing needs to be improved
            List<IClothing> beesDiscardData = new List<IClothing>
            {
                BeesClothing.Cuirass.CuirassInstance, BeesClothing.BeeLeaderInstance
            };

            _allClothes = new List<IClothing>();
            _allClothes.AddRange(CommonClothing.All);
            _allClothes.AddRange(RaceSpecificClothing.All);
            _allClothes.AddRange(TaurusClothes.TaurusClothingTypes.All);
            _allClothes.AddRange(CruxClothing.CruxClothingTypes.All);
            _allClothes.Add(Loincloth1.Loincloth1Instance);
            _allClothes.Add(Loincloth2.Loincloth2Instance); //3 and 4 are unneeded because they share with 2
            _allClothes.AddRange(beesDiscardData);
            _allClothes.AddRange(Panthers.AllClothing); // TODO move from global
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Hippo).SetupOutput.AllowedMainClothingTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Hippo).SetupOutput.AllowedWaistTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Merfolk).SetupOutput.AllowedMainClothingTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Merfolk).SetupOutput.AllowedWaistTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Viper).SetupOutput.AllowedWaistTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Frog).SetupOutput.AllowedMainClothingTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Frog).SetupOutput.AllowedWaistTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Imp).SetupOutput.AllowedMainClothingTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Imp).SetupOutput.AllowedWaistTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Imp).SetupOutput.ExtraMainClothing1Types);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Imp).SetupOutput.ExtraMainClothing2Types);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Imp).SetupOutput.ExtraMainClothing3Types);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Imp).SetupOutput.ExtraMainClothing4Types);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Imp).SetupOutput.ExtraMainClothing5Types);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Goblin).SetupOutput.AllowedMainClothingTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Goblin).SetupOutput.AllowedWaistTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Goblin).SetupOutput.ExtraMainClothing1Types);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Goblin).SetupOutput.ExtraMainClothing2Types);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Goblin).SetupOutput.ExtraMainClothing3Types);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Goblin).SetupOutput.ExtraMainClothing4Types);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Goblin).SetupOutput.ExtraMainClothing5Types);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Shark).SetupOutput.AllowedMainClothingTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Shark).SetupOutput.AllowedWaistTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Shark).SetupOutput.ExtraMainClothing1Types);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Komodo).SetupOutput.AllowedMainClothingTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Komodo).SetupOutput.AllowedWaistTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Bat).SetupOutput.AllowedMainClothingTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Cockatrice).SetupOutput.AllowedMainClothingTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Cockatrice).SetupOutput.AllowedWaistTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Deer).SetupOutput.AllowedMainClothingTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Deer).SetupOutput.AllowedWaistTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Deer).SetupOutput.ExtraMainClothing1Types);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Human).SetupOutput.AllowedMainClothingTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Human).SetupOutput.AllowedWaistTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Vargul).SetupOutput.AllowedMainClothingTypes);
            _allClothes.AddRange(RaceFuncs.GetRace(Race.Vargul).SetupOutput.AllowedWaistTypes);
            _allClothes = _allClothes.Distinct().ToList();
        }

        var clothingType = _allClothes.Where(s => s.FixedData.ClothingId == Type).FirstOrDefault();
        if (clothingType == null) return;
        if (clothingType.FixedData.FixedColor == false)
        {
            if (clothingType.FixedData.DiscardUsesPalettes)
                sprite.GetComponentInChildren<SpriteRenderer>().material = ColorPaletteMap.GetPalette(SwapType.Clothing, Color).ColorSwapMaterial;
            else
                sprite.color = ColorMap.GetClothingColor(Color);
        }

        sprite.sprite = clothingType.FixedData.DiscardSprite;
        sprite.sortingOrder = SortOrder;
    }
}