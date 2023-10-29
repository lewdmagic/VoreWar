#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

internal interface IMiscRaceData
{
    Func<int> BreastSizes { get; set; }
    Func<int> DickSizes { get; set; }

    /// <summary>Whether a unit can spawn as furry, and adds the furry option to the customizer</summary>
    bool FurCapable { get; set; }

    List<Gender> CanBeGender { get; set; }

    /// <summary>Whether a unit has the breast vore system, with extended sizes and the two sides being independent.</summary>
    bool ExtendedBreastSprites { get; set; }

    /// <summary>Whether a unit uses the gentler version of the stomach wobble (1/2 to 1/3rd the motion)</summary>
    bool GentleAnimation { get; set; }

    bool BaseBody { get; set; }
    bool WeightGainDisabled { get; set; }
    int HairColors { get; set; }
    int HairStyles { get; set; }
    int SkinColors { get; set; }
    int AccessoryColors { get; set; }
    int EyeTypes { get; set; }
    int AvoidedEyeTypes { get; set; }
    int EyeColors { get; set; }
    int SecondaryEyeColors { get; set; }
    int BodySizes { get; set; }
    int SpecialAccessoryCount { get; set; }
    int BeardStyles { get; set; }
    int MouthTypes { get; set; }
    int AvoidedMouthTypes { get; set; }
    int ExtraColors1 { get; set; }
    int ExtraColors2 { get; set; }
    int ExtraColors3 { get; set; }
    int ExtraColors4 { get; set; }
    int HeadTypes { get; set; }
    int TailTypes { get; set; }
    int FurTypes { get; set; }
    int EarTypes { get; set; }
    int BodyAccentTypes1 { get; set; }
    int BodyAccentTypes2 { get; set; }
    int BodyAccentTypes3 { get; set; }
    int BodyAccentTypes4 { get; set; }
    int BodyAccentTypes5 { get; set; }
    int BallsSizes { get; set; }
    int VulvaTypes { get; set; }
    int BasicMeleeWeaponTypes { get; set; }
    int AdvancedMeleeWeaponTypes { get; set; }
    int BasicRangedWeaponTypes { get; set; }
    int AdvancedRangedWeaponTypes { get; set; }

    /// <summary>The total number of main clothing types plus one additional number for the blank clothing slot</summary>
    int MainClothingTypesCount { get; }

    /// <summary>The number of clothing slots that are excluded from the random generator</summary>
    int AvoidedMainClothingTypes { get; set; }

    int ClothingColors { get; set; }
    Vector2 WholeBodyOffset { get; set; }
    Vector3 ClothingShift { get; set; }

    /// <summary>The total number of waist clothing types plus one additional number for the blank clothing slot</summary>
    int WaistClothingTypesCount { get; }

    /// <summary>The total number of hat types plus one additional number for the blank clothing slot</summary>
    int ClothingHatTypesCount { get; }

    /// <summary>The total number of accessory types plus one additional number for the blank clothing slot</summary>
    int ClothingAccessoryTypesCount { get; }

    int ExtraMainClothing1Count { get; }
    int ExtraMainClothing2Count { get; }
    int ExtraMainClothing3Count { get; }
    int ExtraMainClothing4Count { get; }
    int ExtraMainClothing5Count { get; }

    IReadOnlyList<IClothingDataSimple> AllowedMainClothingTypes { get; }
    IReadOnlyList<IClothingDataSimple> AllowedWaistTypes { get; }
    IReadOnlyList<IClothingDataSimple> AllowedClothingHatTypes { get; }
    IReadOnlyList<IClothingDataSimple> AllowedClothingAccessoryTypes { get; }
    IReadOnlyList<IClothingDataSimple> ExtraMainClothing1Types { get; }
    IReadOnlyList<IClothingDataSimple> ExtraMainClothing2Types { get; }
    IReadOnlyList<IClothingDataSimple> ExtraMainClothing3Types { get; }
    IReadOnlyList<IClothingDataSimple> ExtraMainClothing4Types { get; }
    IReadOnlyList<IClothingDataSimple> ExtraMainClothing5Types { get; }
}

internal interface IMiscRaceData<out T> : IMiscRaceData where T : IParameters
{
    new IWriteOnlyList<IClothing<T>> AllowedMainClothingTypes { get; }
    new IWriteOnlyList<IClothing<T>> AllowedWaistTypes { get; }
    new IWriteOnlyList<IClothing<T>> AllowedClothingHatTypes { get; }
    new IWriteOnlyList<IClothing<T>> AllowedClothingAccessoryTypes { get; }
    new IWriteOnlyList<IClothing<T>> ExtraMainClothing1Types { get; }
    new IWriteOnlyList<IClothing<T>> ExtraMainClothing2Types { get; }
    new IWriteOnlyList<IClothing<T>> ExtraMainClothing3Types { get; }
    new IWriteOnlyList<IClothing<T>> ExtraMainClothing4Types { get; }
    new IWriteOnlyList<IClothing<T>> ExtraMainClothing5Types { get; }
}

internal interface IMiscRaceDataReadable<in T> : IMiscRaceData where T : IParameters
{
    new IReadOnlyList<IClothing<T>> AllowedMainClothingTypes { get; }
    new IReadOnlyList<IClothing<T>> AllowedWaistTypes { get; }
    new IReadOnlyList<IClothing<T>> AllowedClothingHatTypes { get; }
    new IReadOnlyList<IClothing<T>> AllowedClothingAccessoryTypes { get; }
    new IReadOnlyList<IClothing<T>> ExtraMainClothing1Types { get; }
    new IReadOnlyList<IClothing<T>> ExtraMainClothing2Types { get; }
    new IReadOnlyList<IClothing<T>> ExtraMainClothing3Types { get; }
    new IReadOnlyList<IClothing<T>> ExtraMainClothing4Types { get; }
    new IReadOnlyList<IClothing<T>> ExtraMainClothing5Types { get; }
}