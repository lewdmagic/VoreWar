
using System.Collections.Generic;

public interface IUnitRead
{
    int HairColor { get; set; }
    int HairStyle { get; set; }
    int BeardStyle { get; set; }
    int SkinColor { get; set; }
    int AccessoryColor { get; set; }
    int EyeColor { get; set; }
    int ExtraColor1 { get; set; }
    int ExtraColor2 { get; set; }
    int ExtraColor3 { get; set; }
    int ExtraColor4 { get; set; }
    int EyeType { get; set; }
    int MouthType { get; set; }
    int BreastSize { get; set; }
    int DickSize { get; set; }
    bool HasVagina { get; set; }
    int BodySize { get; set; }
    int SpecialAccessoryType { get; set; }
    bool BodySizeManuallyChanged { get; set; }
    int DefaultBreastSize { get; set; }
    int ClothingType { get; set; }
    int ClothingType2 { get; set; }
    int ClothingHatType { get; set; }
    int ClothingAccessoryType { get; set; }
    int ClothingExtraType1 { get; set; }
    int ClothingExtraType2 { get; set; }
    int ClothingExtraType3 { get; set; }
    int ClothingExtraType4 { get; set; }
    int ClothingExtraType5 { get; set; }
    int ClothingColor { get; set; }
    int ClothingColor2 { get; set; }
    int ClothingColor3 { get; set; }
    bool Furry { get; set; }
    int HeadType { get; set; }
    int TailType { get; set; }
    int FurType { get; set; }
    int EarType { get; set; }
    int BodyAccentType1 { get; set; }
    int BodyAccentType2 { get; set; }
    int BodyAccentType3 { get; set; }
    int BodyAccentType4 { get; set; }
    int BodyAccentType5 { get; set; }
    int BallsSize { get; set; }
    int VulvaType { get; set; }
    int BasicMeleeWeaponType { get; set; }
    int AdvancedMeleeWeaponType { get; set; }
    int BasicRangedWeaponType { get; set; }
    int AdvancedRangedWeaponType { get; set; }

    int DigestedUnits { get; set; }
    int KilledUnits { get; set; }

    int TimesKilled { get; set; }
    
    bool IsDead { get; }
    Item[] Items { get; }
    
    bool HasDick { get; }
    bool HasBreasts { get; }
    
    bool HasWeapon { get; }

    Gender GetGender();
    
    int Level { get; }

    UnitType Type { get; }
    
    bool Predator { get; }
    
    Race GetRace { get; }
}