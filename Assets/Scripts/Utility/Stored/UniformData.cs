using OdinSerializer;

class UniformData
{
    [OdinSerialize]
    private string _name;
    public string Name { get => _name; set => _name = value; }
    [OdinSerialize]
    private Race _race;
    public Race Race { get => _race; set => _race = value; }
    [OdinSerialize]
    private Gender _gender;
    public Gender Gender { get => _gender; set => _gender = value; }
    [OdinSerialize]
    private int _clothingType;
    internal int ClothingType { get => _clothingType; set => _clothingType = value; }
    [OdinSerialize]
    private int _clothingColor;
    internal int ClothingColor { get => _clothingColor; set => _clothingColor = value; }
    [OdinSerialize]
    private int _clothingColor2;
    internal int ClothingColor2 { get => _clothingColor2; set => _clothingColor2 = value; }
    [OdinSerialize]
    private int _clothingColor3;
    internal int ClothingColor3 { get => _clothingColor3; set => _clothingColor3 = value; }

    [OdinSerialize]
    private UnitType _type;
    public UnitType Type { get => _type; set => _type = value; }

    [OdinSerialize]
    private bool _hatSaved;
    internal bool HatSaved { get => _hatSaved; set => _hatSaved = value; }
    [OdinSerialize]
    private bool _clothingAccesorySaved;
    internal bool ClothingAccesorySaved { get => _clothingAccesorySaved; set => _clothingAccesorySaved = value; }

    [OdinSerialize]
    private int _clothingAccesoryType;
    internal int ClothingAccesoryType { get => _clothingAccesoryType; set => _clothingAccesoryType = value; }
    [OdinSerialize]
    private int _clothingHatType;
    internal int ClothingHatType { get => _clothingHatType; set => _clothingHatType = value; }
    [OdinSerialize]
    private int _clothingType2;
    internal int ClothingType2 { get => _clothingType2; set => _clothingType2 = value; }

    [OdinSerialize]
    private int _clothingExtraType1;
    internal int ClothingExtraType1 { get => _clothingExtraType1; set => _clothingExtraType1 = value; }
    [OdinSerialize]
    private int _clothingExtraType2;
    internal int ClothingExtraType2 { get => _clothingExtraType2; set => _clothingExtraType2 = value; }
    [OdinSerialize]
    private int _clothingExtraType3;
    internal int ClothingExtraType3 { get => _clothingExtraType3; set => _clothingExtraType3 = value; }
    [OdinSerialize]
    private int _clothingExtraType4;
    internal int ClothingExtraType4 { get => _clothingExtraType4; set => _clothingExtraType4 = value; }
    [OdinSerialize]
    private int _clothingExtraType5;
    internal int ClothingExtraType5 { get => _clothingExtraType5; set => _clothingExtraType5 = value; }


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
