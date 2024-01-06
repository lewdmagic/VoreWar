#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

public interface IMiscRaceData
{
    
    void Names(string singularName, string pluralName);
    void Names(string singularName, Func<INameInput, string> pluralName);
    void Names(Func<INameInput, string> singularName, string pluralName);
    void Names(Func<INameInput, string> singularName, Func<INameInput, string> pluralName);

    void TownNames(List<string> nameList);
    void PreyTownNames(List<string> nameList);
    void IndividualNames(List<string> nameList);
    
    void WallType(WallType wallType);

    void BonesInfo(Func<Unit, List<BoneInfo>> boneTypesGen);

    void FlavorText(FlavorText flavorText);
    void RaceTraits(RaceTraits raceTraits);
    void SetRaceTraits(Action<RaceTraits> setRaceTraits);

    //void CustomizeButtons(Action<Unit, EnumIndexedArray<ButtonType, CustomizerButton>> action);
    void CustomizeButtons(Action<Unit, ButtonCustomizer> action);
    
    
    
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

    IReadOnlyList<IClothingDataSimple> AllowedMainClothingTypesBasic { get; }
    IReadOnlyList<IClothingDataSimple> AllowedWaistTypesBasic { get; }
    IReadOnlyList<IClothingDataSimple> AllowedClothingHatTypesBasic { get; }
    IReadOnlyList<IClothingDataSimple> AllowedClothingAccessoryTypesBasic { get; }
    IReadOnlyList<IClothingDataSimple> ExtraMainClothing1TypesBasic { get; }
    IReadOnlyList<IClothingDataSimple> ExtraMainClothing2TypesBasic { get; }
    IReadOnlyList<IClothingDataSimple> ExtraMainClothing3TypesBasic { get; }
    IReadOnlyList<IClothingDataSimple> ExtraMainClothing4TypesBasic { get; }
    IReadOnlyList<IClothingDataSimple> ExtraMainClothing5TypesBasic { get; }
}

///
/// To anyone reading this, yes it's a total mess of nonsense that should be improved
/// It's the result of the absolute pain of dealing with covarience and contravarience
/// The reason it exists is because it works, kinda.
/// 

internal interface IMiscRaceDataWritable<out T> : IMiscRaceData where T : IParameters
{
    IWriteOnlyList<IClothing<T>> AllowedMainClothingTypes { get; }
    IWriteOnlyList<IClothing<T>> AllowedWaistTypes { get; }
    IWriteOnlyList<IClothing<T>> AllowedClothingHatTypes { get; }
    IWriteOnlyList<IClothing<T>> AllowedClothingAccessoryTypes { get; }
    IWriteOnlyList<IClothing<T>> ExtraMainClothing1Types { get; }
    IWriteOnlyList<IClothing<T>> ExtraMainClothing2Types { get; }
    IWriteOnlyList<IClothing<T>> ExtraMainClothing3Types { get; }
    IWriteOnlyList<IClothing<T>> ExtraMainClothing4Types { get; }
    IWriteOnlyList<IClothing<T>> ExtraMainClothing5Types { get; }
}

internal interface IMiscRaceDataReadable<in T> : IMiscRaceData where T : IParameters
{
    IReadOnlyList<IClothing<T>> AllowedMainClothingTypesRead { get; }
    IReadOnlyList<IClothing<T>> AllowedWaistTypesRead { get; }
    IReadOnlyList<IClothing<T>> AllowedClothingHatTypesRead { get; }
    IReadOnlyList<IClothing<T>> AllowedClothingAccessoryTypesRead { get; }
    IReadOnlyList<IClothing<T>> ExtraMainClothing1TypesRead { get; }
    IReadOnlyList<IClothing<T>> ExtraMainClothing2TypesRead { get; }
    IReadOnlyList<IClothing<T>> ExtraMainClothing3TypesRead { get; }
    IReadOnlyList<IClothing<T>> ExtraMainClothing4TypesRead { get; }
    IReadOnlyList<IClothing<T>> ExtraMainClothing5TypesRead { get; }
}