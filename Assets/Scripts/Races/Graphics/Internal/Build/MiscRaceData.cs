#region

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

public class MiscRaceData : IMiscRaceData
{
    public readonly List<IClothing> AllowedClothingAccessoryTypes = new List<IClothing>();
    public readonly List<IClothing> AllowedClothingHatTypes = new List<IClothing>();
    public readonly List<IClothing> AllowedMainClothingTypes = new List<IClothing>();
    public readonly List<IClothing> AllowedWaistTypes = new List<IClothing>();
    public readonly List<IClothing> ExtraMainClothing1Types = new List<IClothing>();
    public readonly List<IClothing> ExtraMainClothing2Types = new List<IClothing>();
    public readonly List<IClothing> ExtraMainClothing3Types = new List<IClothing>();
    public readonly List<IClothing> ExtraMainClothing4Types = new List<IClothing>();
    public readonly List<IClothing> ExtraMainClothing5Types = new List<IClothing>();

    
    internal readonly ExtraRaceInfo _extraRaceInfo = new ExtraRaceInfo();
    
    public MiscRaceData(
        Func<int> breastSizes,
        Func<int> dickSizes,
        bool furCapable,
        List<Gender> canBeGender,
        bool extendedBreastSprites,
        bool gentleAnimation,
        bool baseBody,
        bool weightGainDisabled,
        int hairColors,
        int hairStyles,
        int skinColors,
        int accessoryColors,
        int eyeTypes,
        int avoidedEyeTypes,
        int eyeColors,
        int secondaryEyeColors,
        int bodySizes,
        int specialAccessoryCount,
        int beardStyles,
        int mouthTypes,
        int avoidedMouthTypes,
        int extraColors1,
        int extraColors2,
        int extraColors3,
        int extraColors4,
        int headTypes,
        int tailTypes,
        int furTypes,
        int earTypes,
        int bodyAccentTypes1,
        int bodyAccentTypes2,
        int bodyAccentTypes3,
        int bodyAccentTypes4,
        int bodyAccentTypes5,
        int ballsSizes,
        int vulvaTypes,
        int basicMeleeWeaponTypes,
        int advancedMeleeWeaponTypes,
        int basicRangedWeaponTypes,
        int advancedRangedWeaponTypes,
        int avoidedMainClothingTypes,
        int clothingColors,
        Vector2 wholeBodyOffset,
        Vector3 clothingShift)
    {
        BreastSizes = breastSizes;
        DickSizes = dickSizes;
        FurCapable = furCapable;
        CanBeGender = canBeGender;
        ExtendedBreastSprites = extendedBreastSprites;
        GentleAnimation = gentleAnimation;
        BaseBody = baseBody;
        WeightGainDisabled = weightGainDisabled;
        HairColors = hairColors;
        HairStyles = hairStyles;
        SkinColors = skinColors;
        AccessoryColors = accessoryColors;
        EyeTypes = eyeTypes;
        AvoidedEyeTypes = avoidedEyeTypes;
        EyeColors = eyeColors;
        SecondaryEyeColors = secondaryEyeColors;
        BodySizes = bodySizes;
        SpecialAccessoryCount = specialAccessoryCount;
        BeardStyles = beardStyles;
        MouthTypes = mouthTypes;
        AvoidedMouthTypes = avoidedMouthTypes;
        ExtraColors1 = extraColors1;
        ExtraColors2 = extraColors2;
        ExtraColors3 = extraColors3;
        ExtraColors4 = extraColors4;
        HeadTypes = headTypes;
        TailTypes = tailTypes;
        FurTypes = furTypes;
        EarTypes = earTypes;
        BodyAccentTypes1 = bodyAccentTypes1;
        BodyAccentTypes2 = bodyAccentTypes2;
        BodyAccentTypes3 = bodyAccentTypes3;
        BodyAccentTypes4 = bodyAccentTypes4;
        BodyAccentTypes5 = bodyAccentTypes5;
        BallsSizes = ballsSizes;
        VulvaTypes = vulvaTypes;
        BasicMeleeWeaponTypes = basicMeleeWeaponTypes;
        AdvancedMeleeWeaponTypes = advancedMeleeWeaponTypes;
        BasicRangedWeaponTypes = basicRangedWeaponTypes;
        AdvancedRangedWeaponTypes = advancedRangedWeaponTypes;
        AvoidedMainClothingTypes = avoidedMainClothingTypes;
        ClothingColors = clothingColors;
        WholeBodyOffset = wholeBodyOffset;
        ClothingShift = clothingShift;
    }

    public int TestVal { get; set; }
    
    public void Names(string singularName, string pluralName)
    {
        _extraRaceInfo.SingularName = (input) => singularName;
        _extraRaceInfo.PluralName = (input) => pluralName;
    }

    public void Names(string singularName, Func<INameInput, string> pluralName)
    {
        _extraRaceInfo.SingularName = (input) => singularName;
        _extraRaceInfo.PluralName = pluralName;
    }

    public void Names(Func<INameInput, string> singularName, string pluralName)
    {
        _extraRaceInfo.SingularName = singularName;
        _extraRaceInfo.PluralName = (input) => pluralName;
    }
    
    public void Names(Func<INameInput, string> singularName, Func<INameInput, string> pluralName)
    {
        _extraRaceInfo.SingularName = singularName;
        _extraRaceInfo.PluralName = pluralName;
    }    
    
    public void WallType(WallType wallType)
    {
        _extraRaceInfo.WallType = wallType;
    }

    public void BonesInfo(Func<Unit, List<BoneInfo>> boneTypesGen)
    {
        _extraRaceInfo.BoneTypesGen = boneTypesGen;
    }

    public void FlavorText(FlavorText flavorText)
    {
        _extraRaceInfo.FlavorText = flavorText;
    }

    public void RaceTraits(RaceTraits raceTraits)
    {
        _extraRaceInfo.RaceTraits = raceTraits;
    }

    public void SetRaceTraits(Action<RaceTraits> setRaceTraits)
    {
        RaceTraits traits = new RaceTraits();
        setRaceTraits.Invoke(traits);
        _extraRaceInfo.RaceTraits = traits;
    }

    public void CustomizeButtons(Action<Unit, EnumIndexedArray<ButtonType, CustomizerButton>> action)
    {
        _extraRaceInfo.CustomizeButtonsAction = action;
    }

    public void CustomizeButtons(Action<Unit, ButtonCustomizer> action)
    {
        _extraRaceInfo.CustomizeButtonsAction2 = action;
    }
    
    public void TownNames(List<string> nameList)
    {
        _extraRaceInfo.TownNames = nameList;
    }

    public void PreyTownNames(List<string> nameList)
    {
        _extraRaceInfo.PreyTownNames = nameList;
    }

    public void IndividualNames(List<string> nameList)
    {
        _extraRaceInfo.IndividualNames = nameList;
    }

    public Func<int> BreastSizes { get; set; }
    public Func<int> DickSizes { get; set; }


    /// <summary>Whether a unit can spawn as furry, and adds the furry option to the customizer</summary>
    public bool FurCapable { get; set; }

    public List<Gender> CanBeGender { get; set; }

    /// <summary>Whether a unit has the breast vore system, with extended sizes and the two sides being independent.</summary>
    public bool ExtendedBreastSprites { get; set; }

    /// <summary>Whether a unit uses the gentler version of the stomach wobble (1/2 to 1/3rd the motion)</summary>
    public bool GentleAnimation { get; set; }

    public bool BaseBody { get; set; }

    public bool WeightGainDisabled { get; set; }

    public int HairColors { get; set; }
    public int HairStyles { get; set; }
    public int SkinColors { get; set; }
    public int AccessoryColors { get; set; }
    public int EyeTypes { get; set; }
    public int AvoidedEyeTypes { get; set; }
    public int EyeColors { get; set; }
    public int SecondaryEyeColors { get; set; }
    public int BodySizes { get; set; }
    public int SpecialAccessoryCount { get; set; }
    public int BeardStyles { get; set; }

    public int MouthTypes { get; set; }
    public int AvoidedMouthTypes { get; set; }

    public int ExtraColors1 { get; set; }
    public int ExtraColors2 { get; set; }
    public int ExtraColors3 { get; set; }
    public int ExtraColors4 { get; set; }

    public int HeadTypes { get; set; }
    public int TailTypes { get; set; }
    public int FurTypes { get; set; }
    public int EarTypes { get; set; }
    public int BodyAccentTypes1 { get; set; }
    public int BodyAccentTypes2 { get; set; }
    public int BodyAccentTypes3 { get; set; }
    public int BodyAccentTypes4 { get; set; }
    public int BodyAccentTypes5 { get; set; }
    public int BallsSizes { get; set; }
    public int VulvaTypes { get; set; }
    public int BasicMeleeWeaponTypes { get; set; }
    public int AdvancedMeleeWeaponTypes { get; set; }
    public int BasicRangedWeaponTypes { get; set; }
    public int AdvancedRangedWeaponTypes { get; set; }


    /// <summary>The number of clothing slots that are excluded from the random generator</summary>
    public int AvoidedMainClothingTypes { get; set; }

    public int ClothingColors { get; set; }

    public Vector2 WholeBodyOffset { get; set; }
    public Vector3 ClothingShift { get; set; }

    IReadOnlyList<IClothingDataSimple> IMiscRaceData.AllowedMainClothingTypesBasic => AllowedMainClothingTypes;

    /// <summary>The total number of main clothing types plus one additional number for the blank clothing slot</summary>
    public int MainClothingTypesCount => AllowedMainClothingTypes.Count() + 1;

    IReadOnlyList<IClothingDataSimple> IMiscRaceData.AllowedWaistTypesBasic => AllowedWaistTypes;

    /// <summary>The total number of waist clothing types plus one additional number for the blank clothing slot</summary>
    public int WaistClothingTypesCount => AllowedWaistTypes.Count() + 1;

    IReadOnlyList<IClothingDataSimple> IMiscRaceData.AllowedClothingHatTypesBasic => AllowedClothingHatTypes;

    /// <summary>The total number of hat types plus one additional number for the blank clothing slot</summary>
    public int ClothingHatTypesCount => AllowedClothingHatTypes.Count() + 1;

    IReadOnlyList<IClothingDataSimple> IMiscRaceData.AllowedClothingAccessoryTypesBasic => AllowedClothingAccessoryTypes;

    /// <summary>The total number of accessory types plus one additional number for the blank clothing slot</summary>
    public int ClothingAccessoryTypesCount => AllowedClothingAccessoryTypes.Count() + 1;

    IReadOnlyList<IClothingDataSimple> IMiscRaceData.ExtraMainClothing1TypesBasic => ExtraMainClothing1Types;
    public int ExtraMainClothing1Count => ExtraMainClothing1Types.Count() + 1;
    IReadOnlyList<IClothingDataSimple> IMiscRaceData.ExtraMainClothing2TypesBasic => ExtraMainClothing2Types;
    public int ExtraMainClothing2Count => ExtraMainClothing2Types.Count() + 1;
    IReadOnlyList<IClothingDataSimple> IMiscRaceData.ExtraMainClothing3TypesBasic => ExtraMainClothing3Types;
    public int ExtraMainClothing3Count => ExtraMainClothing3Types.Count() + 1;
    IReadOnlyList<IClothingDataSimple> IMiscRaceData.ExtraMainClothing4TypesBasic => ExtraMainClothing4Types;
    public int ExtraMainClothing4Count => ExtraMainClothing4Types.Count() + 1;
    IReadOnlyList<IClothingDataSimple> IMiscRaceData.ExtraMainClothing5TypesBasic => ExtraMainClothing5Types;
    public int ExtraMainClothing5Count => ExtraMainClothing5Types.Count() + 1;
}