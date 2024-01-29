﻿using OdinSerializer;

class UniformData
{
    [OdinSerialize]
    public string Name;
    [OdinSerialize]
    public Race Race;
    [OdinSerialize]
    public Gender Gender;
    [OdinSerialize]
    internal int ClothingType;
    [OdinSerialize]
    internal int ClothingColor;
    [OdinSerialize]
    internal int ClothingColor2;
    [OdinSerialize]
    internal int ClothingColor3;

    [OdinSerialize]
    public UnitType Type;

    [OdinSerialize]
    internal bool HatSaved;
    [OdinSerialize]
    internal bool ClothingAccesorySaved;

    [OdinSerialize]
    internal int ClothingAccesoryType;
    [OdinSerialize]
    internal int ClothingHatType;
    [OdinSerialize]
    internal int ClothingType2;

    [OdinSerialize]
    internal int ClothingExtraType1;
    [OdinSerialize]
    internal int ClothingExtraType2;
    [OdinSerialize]
    internal int ClothingExtraType3;
    [OdinSerialize]
    internal int ClothingExtraType4;
    [OdinSerialize]
    internal int ClothingExtraType5;


    public void CopyFromUnit(Unit unit, bool hat, bool clothingAcc)
    {
        Race = unit.Race;
        Gender = unit.GetGender();
        ClothingType = unit.ClothingType;
        ClothingType2 = unit.ClothingType2;

        ClothingColor = unit.ClothingColor;
        ClothingColor2 = unit.ClothingColor2;
        ClothingColor3 = unit.ClothingColor3;

        ClothingExtraType1 = unit.ClothingExtraType1;
        ClothingExtraType2 = unit.ClothingExtraType2;
        ClothingExtraType3 = unit.ClothingExtraType3;
        ClothingExtraType4 = unit.ClothingExtraType4;
        ClothingExtraType5 = unit.ClothingExtraType5;

        HatSaved = hat;
        ClothingAccesorySaved = clothingAcc;

        if (HatSaved)
            ClothingHatType = unit.ClothingHatType;
        if (ClothingAccesorySaved)
            ClothingAccesoryType = unit.ClothingAccessoryType;


        Type = unit.Type;


    }

    public void CopyToUnit(Unit unit)
    {

        unit.ClothingType = ClothingType;
        unit.ClothingType2 = ClothingType2;

        unit.ClothingColor = ClothingColor;
        unit.ClothingColor2 = ClothingColor2;
        unit.ClothingColor3 = ClothingColor3;

        //unit.SpecialAccessoryType = SpecialAccessoryType;

        if (HatSaved)
            unit.ClothingHatType = ClothingHatType;
        if (ClothingAccesorySaved)
            unit.ClothingAccessoryType = ClothingAccesoryType;


        unit.ClothingExtraType1 = ClothingExtraType1;
        unit.ClothingExtraType2 = ClothingExtraType2;
        unit.ClothingExtraType3 = ClothingExtraType3;
        unit.ClothingExtraType4 = ClothingExtraType4;
        unit.ClothingExtraType5 = ClothingExtraType5;


        var race = Races2.GetRace(unit);
        if (ClothingType > race.SetupOutput.AllowedMainClothingTypes.Count) unit.ClothingType = 0;
        if (ClothingType2 > race.SetupOutput.AllowedWaistTypes.Count) unit.ClothingType2 = 0;
        if (ClothingExtraType1 > race.SetupOutput.ExtraMainClothing1Types.Count) unit.ClothingExtraType1 = 0;
        if (ClothingExtraType2 > race.SetupOutput.ExtraMainClothing2Types.Count) unit.ClothingExtraType2 = 0;
        if (ClothingExtraType3 > race.SetupOutput.ExtraMainClothing3Types.Count) unit.ClothingExtraType3 = 0;
        if (ClothingExtraType4 > race.SetupOutput.ExtraMainClothing4Types.Count) unit.ClothingExtraType4 = 0;
        if (ClothingExtraType5 > race.SetupOutput.ExtraMainClothing5Types.Count) unit.ClothingExtraType5 = 0;
    }
}
