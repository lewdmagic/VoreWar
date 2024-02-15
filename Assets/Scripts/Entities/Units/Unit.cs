using OdinSerializer;
using OdinSerializer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Races.Graphics.Implementations.Monsters;
using UnityEngine;

public enum AIClass
{
    Default,
    Melee,
    MeleeVore,
    Ranged,
    RangedVore,
    PureVore,
    MagicMelee,
    MagicRanged,
    Custom
}


// For future refactoring
public enum UnitAttribute
{
    HairColor = 0,
    HairStyle = 1,
    BeardStyle = 2,
    SkinColor = 3,
    AccessoryColor = 4,
    EyeColor = 5,
    ExtraColor1 = 6,
    ExtraColor2 = 7,
    ExtraColor3 = 8,
    ExtraColor4 = 9,
    EyeType = 10,
    MouthType = 11,
    BreastSize = 12,
    DickSize = 13,
    BodySize = 14,
    SpecialAccessoryType = 15,
    DefaultBreastSize = 16,
    ClothingType = 17,
    ClothingType2 = 18,
    ClothingHatType = 19,
    ClothingAccessoryType = 20,
    ClothingExtraType1 = 21,
    ClothingExtraType2 = 22,
    ClothingExtraType3 = 23,
    ClothingExtraType4 = 24,
    ClothingExtraType5 = 25,
    ClothingColor = 26,
    ClothingColor2 = 27,
    ClothingColor3 = 28,
    HeadType = 29,
    TailType = 30,
    FurType = 31,
    EarType = 32,
    BodyAccentType1 = 33,
    BodyAccentType2 = 34,
    BodyAccentType3 = 35,
    BodyAccentType4 = 36,
    BodyAccentType5 = 37,
    BallsSize = 38,
    VulvaType = 39,
    BasicMeleeWeaponType = 40,
    AdvancedMeleeWeaponType = 41,
    BasicRangedWeaponType = 42,
    AdvancedRangedWeaponType = 43
}


public class Unit : IUnitRead //, ISerializationCallbackReceiver
{
    // void ISerializationCallbackReceiver.OnAfterDeserialize()
    // {
    //     Debug.Log("load Items: " + Items);
    // }
    //
    // void ISerializationCallbackReceiver.OnBeforeSerialize()
    // {
    //     Debug.Log("Items: " + Items);
    // }
    //

    // For future refactoring
    //[OdinSerialize]
    //internal EnumIndexedArray<UnitAttribute, int> Attributes = new EnumIndexedArray<UnitAttribute, int>();


    //
    // void ISerializationCallbackReceiver.OnAfterDeserialize()
    // {
    //     SetUp();
    // }
    //
    // void ISerializationCallbackReceiver.OnBeforeSerialize()
    // {
    //
    // }
    //
    // private void SetUp()
    // {
    //     ReloadTraits();
    //     InitializeTraits();
    //     RefreshSecrecy();
    //     InitializeFixedSide(Side);
    // }
    //

    [OdinSerialize]
    private Side _side;

    public Side Side { get => _side; set => _side = value; }

    [OdinSerialize]
    private Unit _controller = null;

    internal Unit Controller
    {
        get
        {
            if (_controller != null)
                if (_controller.Controller != null)
                    return _controller.Controller;
            return _controller;
        }
        set
        {
            if (value == null)
                _controller = value;
            else if (_controller == null || _controller.GetStat(Stat.Mind) < value.GetStat(Stat.Mind))
                if (value == this || value.Controller == this)
                    return;
                else
                    _controller = value;
        }
    }

    [OdinSerialize]
    private Side _fixedSide = Side.TrueNoneSide;

    internal bool HasFixedSide() => RaceFuncs.IsNotNone(_fixedSide);

    public Side FixedSide
    {
        get
        {
            if (Controller != null) return _controller.FixedSide;
            return RaceFuncs.IsNone(_fixedSide) ? Side : _fixedSide;
        }
        set => _fixedSide = value;
    }

    [OdinSerialize]
    private bool _hiddenFixedSide = false;

    public bool HiddenFixedSide { get => _hiddenFixedSide; set => _hiddenFixedSide = value; }

    public static List<TraitType> SecretTags = new List<TraitType>()
    {
        TraitType.Infiltrator, TraitType.Corruption, TraitType.Parasite, TraitType.Metamorphosis,
        TraitType.Possession, TraitType.Changeling, TraitType.Reincarnation, TraitType.InfiniteReincarnation, TraitType.Transmigration, TraitType.InfiniteTransmigration,
        TraitType.Untamable, TraitType.GreaterChangeling, TraitType.SpiritPossession, TraitType.ForcedMetamorphosis
    };


    [OdinSerialize]
    private Race _race;

    public Race Race { get => _race; set => _race = value; }


    public Race GetRace => Race;

    [OdinSerialize]
    private int _health;

    public int Health { get => _health; set => _health = value; }


    [OdinSerialize]
    private int[] _stats;

    protected int[] Stats { get => _stats; set => _stats = value; }

    [OdinSerialize]
    private float _experience;

    internal float Experience { get => _experience; set => _experience = value; }

    [OdinSerialize]
    private int _level;

    public int Level { get => _level; set => _level = value; }

    [OdinSerialize]
    private double _baseScale = 1;

    internal double BaseScale
    {
        get
        {
            if (_baseScale < 1 || HasTrait(TraitType.Growth) == false) return 1;
            return _baseScale;
        }
        set => _baseScale = value;
    }

    [OdinSerialize]
    public float ExpMultiplier { get; protected set; } = 1;

    [OdinSerialize]
    internal int Mana { get; private set; }

    internal int MaxMana => (int)(GetStatBase(Stat.Mind) + GetStatBase(Stat.Will) * 2 * TraitBoosts.ManaMultiplier);

    private int _maxHealth = 99999;

    public int MaxHealth
    {
        get // Ah yes, a simple getter function u?u. Keeps health percentage consistent after gaining/losing stats mid battle, doesn't break Thrillseeker, doesn't cause prey orphans, doesn't break on save/load... etc.
        {
            if (Stats == null) return 1;
            if (!Config.StatBoostsAffectMaxHp)
            {
                _maxHealth = Stats[(int)Stat.Endurance] * 2 + Stats[(int)Stat.Strength];
                return _maxHealth;
            }

            int oldMax = _maxHealth;
            _maxHealth = GetStat(Stat.Endurance) * 2 + GetStat(Stat.Strength);
            if (oldMax > 1 && oldMax != _maxHealth && _healthPct > 0)
            {
                int healthChange = (int)Math.Round((_maxHealth - oldMax) * _healthPct);
                if (healthChange > 0) Health = Math.Min(_maxHealth, Math.Max(1, Health + healthChange));
            }

            return _maxHealth;
        }
        set => _maxHealth = value;
    }

    private int GetHealthBoosts()
    {
        throw new NotImplementedException();
    }

    [OdinSerialize]
    private AIClass _aIClass;

    internal AIClass AIClass { get => _aIClass; set => _aIClass = value; }

    [OdinSerialize]
    private StatWeights _statWeights;

    internal StatWeights StatWeights { get => _statWeights; set => _statWeights = value; }

    [OdinSerialize]
    private bool _autoLeveling;

    internal bool AutoLeveling { get => _autoLeveling; set => _autoLeveling = value; }

    #region Customizations

    [OdinSerialize]
    private int _hairColor;

    public int HairColor { get => _hairColor; set => _hairColor = value; }

    [OdinSerialize]
    private int _hairStyle;

    public int HairStyle { get => _hairStyle; set => _hairStyle = value; }

    [OdinSerialize]
    private int _beardStyle;

    public int BeardStyle { get => _beardStyle; set => _beardStyle = value; }

    [OdinSerialize]
    private int _skinColor;

    public int SkinColor { get => _skinColor; set => _skinColor = value; }

    [OdinSerialize]
    private int _accessoryColor;

    public int AccessoryColor { get => _accessoryColor; set => _accessoryColor = value; }

    [OdinSerialize]
    private int _eyeColor;

    public int EyeColor { get => _eyeColor; set => _eyeColor = value; }

    [OdinSerialize]
    private int _extraColor1;

    public int ExtraColor1 { get => _extraColor1; set => _extraColor1 = value; }

    [OdinSerialize]
    private int _extraColor2;

    public int ExtraColor2 { get => _extraColor2; set => _extraColor2 = value; }

    [OdinSerialize]
    private int _extraColor3;

    public int ExtraColor3 { get => _extraColor3; set => _extraColor3 = value; }

    [OdinSerialize]
    private int _extraColor4;

    public int ExtraColor4 { get => _extraColor4; set => _extraColor4 = value; }

    [OdinSerialize]
    private int _eyeType;

    public int EyeType { get => _eyeType; set => _eyeType = value; }

    [OdinSerialize]
    private int _mouthType;

    public int MouthType { get => _mouthType; set => _mouthType = value; }

    [OdinSerialize]
    private int _breastSize;

    public int BreastSize { get => _breastSize; set => _breastSize = value; }

    [OdinSerialize]
    private int _dickSize;

    public int DickSize { get => _dickSize; set => _dickSize = value; }

    [OdinSerialize]
    private bool _hasVagina;

    public bool HasVagina { get => _hasVagina; set => _hasVagina = value; }

    [OdinSerialize]
    private int _bodySize;

    public int BodySize { get => _bodySize; set => _bodySize = value; }

    [OdinSerialize]
    private int _specialAccessoryType;

    public int SpecialAccessoryType { get => _specialAccessoryType; set => _specialAccessoryType = value; }

    [OdinSerialize]
    private bool _bodySizeManuallyChanged;

    public bool BodySizeManuallyChanged { get => _bodySizeManuallyChanged; set => _bodySizeManuallyChanged = value; }

    [OdinSerialize]
    private int _defaultBreastSize;

    public int DefaultBreastSize { get => _defaultBreastSize; set => _defaultBreastSize = value; }

    [OdinSerialize]
    private int _clothingType;

    public int ClothingType { get => _clothingType; set => _clothingType = value; }

    [OdinSerialize]
    private int _clothingType2;

    public int ClothingType2 { get => _clothingType2; set => _clothingType2 = value; }

    [OdinSerialize]
    private int _clothingHatType;

    public int ClothingHatType { get => _clothingHatType; set => _clothingHatType = value; }

    [OdinSerialize]
    private int _clothingAccessoryType;

    public int ClothingAccessoryType { get => _clothingAccessoryType; set => _clothingAccessoryType = value; }

    [OdinSerialize]
    private int _clothingExtraType1;

    public int ClothingExtraType1 { get => _clothingExtraType1; set => _clothingExtraType1 = value; }

    [OdinSerialize]
    private int _clothingExtraType2;

    public int ClothingExtraType2 { get => _clothingExtraType2; set => _clothingExtraType2 = value; }

    [OdinSerialize]
    private int _clothingExtraType3;

    public int ClothingExtraType3 { get => _clothingExtraType3; set => _clothingExtraType3 = value; }

    [OdinSerialize]
    private int _clothingExtraType4;

    public int ClothingExtraType4 { get => _clothingExtraType4; set => _clothingExtraType4 = value; }

    [OdinSerialize]
    private int _clothingExtraType5;

    public int ClothingExtraType5 { get => _clothingExtraType5; set => _clothingExtraType5 = value; }

    [OdinSerialize]
    private int _clothingColor;

    public int ClothingColor { get => _clothingColor; set => _clothingColor = value; }

    [OdinSerialize]
    private int _clothingColor2;

    public int ClothingColor2 { get => _clothingColor2; set => _clothingColor2 = value; }

    [OdinSerialize]
    private int _clothingColor3;

    public int ClothingColor3 { get => _clothingColor3; set => _clothingColor3 = value; }

    [OdinSerialize]
    private bool _furry;

    public bool Furry { get => _furry; set => _furry = value; }

    [OdinSerialize]
    private int _headType;

    public int HeadType { get => _headType; set => _headType = value; }

    [OdinSerialize]
    private int _tailType;

    public int TailType { get => _tailType; set => _tailType = value; }

    [OdinSerialize]
    private int _furType;

    public int FurType { get => _furType; set => _furType = value; }

    [OdinSerialize]
    private int _earType;

    public int EarType { get => _earType; set => _earType = value; }

    [OdinSerialize]
    private int _bodyAccentType1;

    public int BodyAccentType1 { get => _bodyAccentType1; set => _bodyAccentType1 = value; }

    [OdinSerialize]
    private int _bodyAccentType2;

    public int BodyAccentType2 { get => _bodyAccentType2; set => _bodyAccentType2 = value; }

    [OdinSerialize]
    private int _bodyAccentType3;

    public int BodyAccentType3 { get => _bodyAccentType3; set => _bodyAccentType3 = value; }

    [OdinSerialize]
    private int _bodyAccentType4;

    public int BodyAccentType4 { get => _bodyAccentType4; set => _bodyAccentType4 = value; }

    [OdinSerialize]
    private int _bodyAccentType5;

    public int BodyAccentType5 { get => _bodyAccentType5; set => _bodyAccentType5 = value; }

    [OdinSerialize]
    private int _ballsSize;

    public int BallsSize { get => _ballsSize; set => _ballsSize = value; }

    [OdinSerialize]
    private int _vulvaType;

    public int VulvaType { get => _vulvaType; set => _vulvaType = value; }

    [OdinSerialize]
    private int _basicMeleeWeaponType;

    public int BasicMeleeWeaponType { get => _basicMeleeWeaponType; set => _basicMeleeWeaponType = value; }

    [OdinSerialize]
    private int _advancedMeleeWeaponType;

    public int AdvancedMeleeWeaponType { get => _advancedMeleeWeaponType; set => _advancedMeleeWeaponType = value; }

    [OdinSerialize]
    private int _basicRangedWeaponType;

    public int BasicRangedWeaponType { get => _basicRangedWeaponType; set => _basicRangedWeaponType = value; }

    [OdinSerialize]
    private int _advancedRangedWeaponType;

    public int AdvancedRangedWeaponType { get => _advancedRangedWeaponType; set => _advancedRangedWeaponType = value; }

    #endregion

    [OdinSerialize]
    private int _digestedUnits;

    public int DigestedUnits { get => _digestedUnits; set => _digestedUnits = value; }

    [OdinSerialize]
    private int _killedUnits;

    public int KilledUnits { get => _killedUnits; set => _killedUnits = value; }

    [OdinSerialize]
    private int _timesKilled;

    public int TimesKilled { get => _timesKilled; set => _timesKilled = value; }


    [OdinSerialize]
    private Item[] _items;

    public Item[] Items { get => _items; set => _items = value; }

    [OdinSerialize]
    private string _name;

    public string Name { get => _name; set => _name = value; }

    [OdinSerialize]
    private List<string> _pronouns;

    public List<string> Pronouns { get => _pronouns; set => _pronouns = value; }

    [OdinSerialize]
    private Action _onDiscard;

    public Action OnDiscard { get => _onDiscard; set => _onDiscard = value; }

    public string GetPronoun(int num)
    {
        if (Pronouns == null) GeneratePronouns();
        return Pronouns[num];
    }

    [OdinSerialize]
    private UnitType _type;

    public UnitType Type { get => _type; set => _type = value; }

    [OdinSerialize]
    private bool _predator;

    public bool Predator { get => _predator; set => _predator = value; }

    [OdinSerialize]
    private bool _immuneToDefections;

    public bool ImmuneToDefections { get => _immuneToDefections; set => _immuneToDefections = value; }

    [OdinSerialize]
    private bool _fixedGear;

    public bool FixedGear { get => _fixedGear; set => _fixedGear = value; }

    [OdinSerialize]
    private Unit _attractedTo;

    public Unit AttractedTo { get => _attractedTo; set => _attractedTo = value; }

    [OdinSerialize]
    private VoreType _preferredVoreType;

    public VoreType PreferredVoreType { get => _preferredVoreType; set => _preferredVoreType = value; }

    [OdinSerialize]
    private Unit _savedCopy;

    internal Unit SavedCopy { get => _savedCopy; set => _savedCopy = value; }

    [OdinSerialize]
    private Village _savedVillage;

    internal Village SavedVillage { get => _savedVillage; set => _savedVillage = value; }

    [OdinSerialize]
    private List<StatusEffect> _statusEffects;

    [OdinSerialize]
    private bool _earnedMask = false;

    public bool EarnedMask { get => _earnedMask; set => _earnedMask = value; }

    [OdinSerialize]
    private Unit _killedBy;

    public Unit KilledBy { get => _killedBy; set => _killedBy = value; }

    [OdinSerialize]
    private List<Unit> _shifterShapes;

    public List<Unit> ShifterShapes { get => _shifterShapes; set => _shifterShapes = value; }

    public override string ToString() => Name;

    /// <summary>
    ///     Unit was manually changed to/from pred so it should not be overwritten
    /// </summary>
    [OdinSerialize]
    private bool _fixedPredator;

    public bool FixedPredator { get => _fixedPredator; set => _fixedPredator = value; }

    internal List<StatusEffect> StatusEffects
    {
        get
        {
            if (_statusEffects == null) _statusEffects = new List<StatusEffect>();
            return _statusEffects;
        }
        set => _statusEffects = value;
    }

    [OdinSerialize]
    private List<SpellType> _innateSpells;

    public List<SpellType> InnateSpells { get => _innateSpells; set => _innateSpells = value; }

    private List<Spell> _useableSpells;

    [OdinSerialize]
    private List<SpellType> _singleUseSpells = new List<SpellType>();

    internal List<SpellType> SingleUseSpells { get => _singleUseSpells; set => _singleUseSpells = value; }

    [OdinSerialize]
    private List<SpellType> _multiUseSpells = new List<SpellType>();

    internal List<SpellType> MultiUseSpells { get => _multiUseSpells; set => _multiUseSpells = value; } // This is so much more straightforward than adding Special Actions

    [OdinSerialize]
    private Unit _hiddenUnit = null;

    public Unit HiddenUnit => _hiddenUnit == null ? this : _hiddenUnit;

    public Race HiddenRace => _hiddenUnit == null ? Race : _hiddenUnit.Race;

    public int[] HiddenStats => _hiddenUnit == null ? Stats : _hiddenUnit.Stats;

    [OdinSerialize]
    private Race _spawnRace;

    public Race SpawnRace { get { return Equals(_spawnRace, Race.TrueNone) ? Race : _spawnRace; } set => _spawnRace = value; }

    [OdinSerialize]
    private Race _conversionRace;

    public Race ConversionRace { get { return Equals(_conversionRace, Race.TrueNone) ? Race : _conversionRace; } set => _conversionRace = value; }

    internal List<Spell> UseableSpells
    {
        get
        {
            if (_useableSpells == null)
            {
                _useableSpells = new List<Spell>();
            }

            return _useableSpells;
        }
        set => _useableSpells = value;
    }

    public bool HasDick => DickSize > -1;
    public bool HasBreasts => DefaultBreastSize > -1;

    public bool IsInfiltratingSide(Side side)
    {
        return Equals(side, Side) && !Equals(Side, FixedSide) && HiddenFixedSide;
    }

    public Gender GetGender()
    {
        if (HasBreasts && HasDick && (HasVagina || Config.HermsCanUb == false))
            return Gender.Hermaphrodite;
        else if (HasBreasts && HasDick && !HasVagina)
            return Gender.Gynomorph;
        else if (HasBreasts && !HasDick && HasVagina)
            return Gender.Female;
        else if (HasBreasts && !HasDick && !HasVagina)
            return Gender.Agenic;
        else if (!HasBreasts && HasDick && HasVagina)
            return Gender.Maleherm;
        else if (!HasBreasts && HasDick && !HasVagina)
            return Gender.Male;
        else if (!HasBreasts && !HasDick && HasVagina) return Gender.Andromorph;
        return Gender.None;
    }

    internal bool CanBeConverted()
    {
        return Type != UnitType.Summon && Type != UnitType.Leader && Type != UnitType.SpecialMercenary && HasTrait(TraitType.Eternal) == false && SavedCopy == null;
    }

    internal bool CanUnbirth => Config.Unbirth && HasVagina;
    internal bool CanCockVore => Config.CockVore && HasDick;
    internal bool CanBreastVore => Config.BreastVore && HasBreasts;
    internal bool CanAnalVore => Config.AnalVore;
    internal bool CanTailVore => Config.TailVore;

    public bool CanVore(PreyLocation location)
    {
        switch (location)
        {
            case PreyLocation.Womb:
                return CanUnbirth;
            case PreyLocation.Balls:
                return CanCockVore;
            case PreyLocation.Breasts:
                return CanBreastVore;
            case PreyLocation.Anal:
                return CanAnalVore;
            case PreyLocation.Tail:
                return CanTailVore;
            default:
                return true;
        }
    }

    public bool HasWeapon
    {
        get
        {
            if (Items == null) return false;
            if (HasTrait(TraitType.Feral)) return false;
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] is Weapon) return true;
            }

            return false;
        }
    }

    internal bool HasSpecificWeapon(params ItemType[] types)
    {
        if (Items == null) return false;
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null) continue;
            if (types.Contains(State.World.ItemRepository.GetItemType(Items[i]))) return true;
        }

        return false;
    }

    internal bool HasBook
    {
        get
        {
            if (Items == null) return false;
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] is SpellBook) return true;
            }

            return false;
        }
    }

    public bool BestSuitedForRanged() => Stats[(int)Stat.Dexterity] * TraitBoosts.VirtualDexMult > Stats[(int)Stat.Strength] * TraitBoosts.VirtualStrMult;

    protected void SetLevel(int level) => this.Level = level;

    internal bool SpendMana(int amount)
    {
        int modifiedManaCost = amount + amount * (GetStatusEffect(StatusEffectType.SpellForce) != null ? GetStatusEffect(StatusEffectType.SpellForce).Duration / 10 : 0);
        if (Mana >= modifiedManaCost)
        {
            Mana -= modifiedManaCost;
            return true;
        }

        return false;
    }

    internal void RestoreManaPct(float pct)
    {
        Mana += (int)(MaxMana * pct);
        if (Mana > MaxMana) Mana = MaxMana;
    }

    internal void RestoreMana(int amt)
    {
        Mana += amt;
        if (Mana > MaxMana) Mana = MaxMana;
    }


    internal int NearbyFriendlies = 0;
    internal bool Harassed = false;

    public bool IsDead => Health < 1;
    private PermanentBoosts _traitBoosts;

    internal PermanentBoosts TraitBoosts
    {
        get
        {
            if (_traitBoosts == null) InitializeTraits();
            return _traitBoosts;
        }
        set => _traitBoosts = value;
    }

    [OdinSerialize]
    private List<TraitType> _tags;

    protected List<TraitType> Tags { get => _tags; set => _tags = value; } //For some reason, renaming this to anything else results in an infinite loop in serialization, so it is staying tags for now

    [OdinSerialize]
    private List<TraitType> _temporaryTraits;

    protected List<TraitType> TemporaryTraits { get => _temporaryTraits; set => _temporaryTraits = value; }

    [OdinSerialize]
    private List<TraitType> _sharedTraits;

    protected List<TraitType> SharedTraits { get => _sharedTraits; set => _sharedTraits = value; }

    [OdinSerialize]
    private List<TraitType> _persistentSharedTraits;

    protected List<TraitType> PersistentSharedTraits { get => _persistentSharedTraits; set => _persistentSharedTraits = value; }

    /// <summary>
    ///     Traits that are considered to be permanent, i.e. do not disappear during refreshes
    /// </summary>
    [OdinSerialize]
    private List<TraitType> _permanentTraits;

    protected List<TraitType> PermanentTraits { get => _permanentTraits; set => _permanentTraits = value; }

    /// <summary>
    ///     Traits that are explicitly removed, done so that they aren't added back in at some point by version changes or the
    ///     like.
    /// </summary>
    [OdinSerialize]
    private List<TraitType> _removedTraits;

    protected List<TraitType> RemovedTraits { get => _removedTraits; set => _removedTraits = value; }

    //internal List<Trait> TraitsList = new List<Trait>();
    internal List<IStatBoost> StatBoosts;
    internal List<IAttackStatusEffect> AttackStatusEffects;
    internal List<IVoreAttackOdds> VoreAttackOdds;
    internal List<IVoreDefenseOdds> VoreDefenseOdds;
    internal List<IPhysicalDefenseOdds> PhysicalDefenseOdds;

    [OdinSerialize]
    private int _enemiesKilledThisBattle;

    internal int EnemiesKilledThisBattle { get => _enemiesKilledThisBattle; set => _enemiesKilledThisBattle = value; }

    internal Unit CurrentLeader;

    [OdinSerialize]
    public ActorUnit BoundUnit;

    /// <summary>
    ///     Creates an empty unit for various purposes
    /// </summary>
    public Unit(Race race)
    {
        Race = race;
        Stats = new int[(int)Stat.None];
        Stats[(int)Stat.Strength] = 6 + State.Rand.Next(9);
        Stats[(int)Stat.Dexterity] = 6 + State.Rand.Next(9);
        Stats[(int)Stat.Endurance] = 8 + State.Rand.Next(6);
        Stats[(int)Stat.Mind] = 6 + State.Rand.Next(8);
        Stats[(int)Stat.Will] = 6 + State.Rand.Next(8);
        Stats[(int)Stat.Agility] = 6 + State.Rand.Next(5);
        Stats[(int)Stat.Voracity] = 5 + State.Rand.Next(7);
        Stats[(int)Stat.Stomach] = 12 + State.Rand.Next(4);
        Health = MaxHealth;
        Mana = MaxMana;
    }

    public Unit(Side side, Race race, int startingXp, bool predator, UnitType type = UnitType.Soldier, bool immuneToDefectons = false)
    {
        Stats = new int[(int)Stat.None];
        Race = race;
        Side = side;
        Experience = startingXp;
        Level = 1;
        Tags = new List<TraitType>();
        PermanentTraits = new List<TraitType>();
        RemovedTraits = new List<TraitType>();
        Type = type;

        Predator = predator;
        if (predator == false) FixedPredator = true;
        ImmuneToDefections = immuneToDefectons;


        var raceData = RaceFuncs.GetRace(this);
        RandomizeNameAndGender(race, raceData, true);

        DefaultBreastSize = BreastSize;
        Items = new Item[Config.ItemSlots];


        ReloadTraits();
        raceData.RandomCustomCall(this);
        InitializeTraits();
        RefreshSecrecy();
        InitializeFixedSide(side);

        if (HasTrait(TraitType.Resourceful))
        {
            Items = new Item[3];
        }

        SetGear(race);

        InnateSpells = new List<SpellType>();
        ShifterShapes = new List<Unit>();

        if (ReferenceEquals(race, Race.Dragon))
        {
            int rand = State.Rand.Next(3);
            if (rand == 0) InnateSpells.Add(SpellType.IceBlast);
            if (rand == 1) InnateSpells.Add(SpellType.Pyre);
            if (rand == 2) InnateSpells.Add(SpellType.LightningBolt);
        }

        if (Equals(race, Race.Fairy))
        {
            FairyUtil.SetSeason(this, FairyUtil.GetSeason(this)); //To establish the spell properly
        }

        RandomSkills();
        Health = MaxHealth;
        Mana = MaxMana;

        if (UniformDataStorer.GetUniformOdds(race) >= State.Rand.NextDouble())
        {
            var available = UniformDataStorer.GetCompatibleCustomizations(this);
            if (available != null && available.Any())
            {
                UniformDataStorer.ExternalCopyToUnit(available[State.Rand.Next(available.Count)], this);
            }
        }

        _spawnRace = Race.TrueNone;
        _conversionRace = Race.TrueNone;

        ReincarnateCheck();
        //if (HasTrait(Traits.Shapeshifter) || HasTrait(Traits.Skinwalker))
        //{
        //    AcquireShape(this, true);
        //}
        SetForcedPermanentTraits();
    }

    private void SetForcedPermanentTraits()
    {
        if (HasTrait(TraitType.InfiniteAssimilation))
        {
            RemoveTrait(TraitType.InfiniteAssimilation);
            AddPermanentTrait(TraitType.InfiniteAssimilation);
        }

        if (HasTrait(TraitType.InfiniteReincarnation))
        {
            RemoveTrait(TraitType.InfiniteReincarnation);
            AddPermanentTrait(TraitType.InfiniteReincarnation);
        }

        if (HasTrait(TraitType.InfiniteTransmigration))
        {
            RemoveTrait(TraitType.InfiniteTransmigration);
            AddPermanentTrait(TraitType.InfiniteTransmigration);
        }

        //if (HasTrait(Traits.Shapeshifter))
        //{
        //    RemoveTrait(Traits.Shapeshifter);
        //    AddPermanentTrait(Traits.Shapeshifter);
        //}
        //if (HasTrait(Traits.Skinwalker))
        //{
        //    RemoveTrait(Traits.Skinwalker);
        //    AddPermanentTrait(Traits.Skinwalker);
        //}
        if (HasTrait(TraitType.Extraction))
        {
            RemoveTrait(TraitType.Extraction);
            AddPermanentTrait(TraitType.Extraction);
        }
    }

    private void ReincarnateCheck()
    {
        if (State.World != null && State.World.Reincarnators != null && State.World.Reincarnators?.Count > 0 && Type != UnitType.SpecialMercenary && Type != UnitType.Summon)
        {
            if (State.World.Reincarnators.Where(r => Equals(r.Race, Race)).Count() > 0 && State.Rand.Next(3) == 0) Reincarnate(State.World.Reincarnators.Where(r => Equals(r.Race, Race)).First());
        }
    }

    private void Reincarnate(Reincarnator reinc)
    {
        Unit unit = reinc.PastLife;
        Name = unit.Name;
        Experience = unit.Experience;
        foreach (TraitType trait in unit.PermanentTraits)
        {
            AddPermanentTrait(trait);
        }

        InnateSpells.AddRange(unit.InnateSpells);
        ShifterShapes = unit.ShifterShapes;
        FixedSide = unit.FixedSide;
        HiddenFixedSide = true;
        SavedCopy = unit.SavedCopy;
        SavedVillage = unit.SavedVillage;
        Race race = reinc.Race;
        OnDiscard = () =>
        {
            Race targetRace = race;
            if (!reinc.RaceLocked)
            {
                List<Race> activeRaces = StrategicUtilities.GetAllUnits(false).ConvertAll(u => u.Race)
                    .Distinct().ToList();
                if (activeRaces.Any())
                {
                    targetRace = activeRaces[State.Rand.Next(activeRaces.Count)];
                }
            }

            State.World.Reincarnators?.Add(new Reincarnator(unit, targetRace, reinc.RaceLocked));
            Debug.Log(unit.Name + " is re-reincarnating");
        };
        State.World.Reincarnators?.Remove(reinc);
        Debug.Log(unit.Name + " reincarnated as a " + Race);
        StrategicUtilities.SpendLevelUps(this);
    }

    internal void SetGear(Race race, bool skipTraitItems = false)
    {
        if (State.World == null) return;

        if (RaceFuncs.IsMonster(race))
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetMonsterItem(Race);
        }
        else if (Equals(race, Race.Selicia))
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.SeliciaWeapon);
        }
        else if (Equals(race, Race.Vision))
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.VisionWeapon);
        }
        else if (Equals(race, Race.Ki))
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.KiWeapon);
        }
        else if (Equals(race, Race.Scorch))
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.ScorchWeapon);
        }
        else if (Equals(race, Race.Succubus))
        {
            World world = State.World;
            ItemRepository itemRepository = world.ItemRepository;
            Item value = itemRepository.GetSpecialItem(SpecialItems.SuccubusWeapon);
            Items[0] = value;
        }
        else if (Equals(race, Race.Asura))
        {
            Items[0] = State.World.ItemRepository.GetItem(ItemType.Axe);
        }
        else if (Equals(race, Race.Draco))
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.DracoWeapon);
        }
        else if (Equals(race, Race.Zoey))
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.ZoeyWeapon);
        }
        else if (Equals(race, Race.Abakhanskya))
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.AbakWeapon);
        }
        else if (Equals(race, Race.Zera))
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.ZeraWeapon);
        }
        else if (Equals(race, Race.Auri))
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.AurilikaWeapon);
        }
        else if (Equals(race, Race.Salix))
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.SalixWeapon);
        }
        else if (Equals(race, Race.Erin))
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.ErinWeapon);
            Items[1] = State.World.ItemRepository.GetSpecialItem(SpecialItems.ErinWings);
        }
        else
        {
            FixedGear = false;
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] != null && State.World.ItemRepository.ItemIsUnique(Items[i])) Items[i] = null;
            }

            if (RaceParameters.GetRaceTraits(race).CanUseRangedWeapons == false)
            {
                for (int i = 0; i < Items.Length; i++)
                {
                    if (Items[i] != null && State.World.ItemRepository.ItemIsRangedWeapon(Items[i]))
                    {
                        if (Items[i] is Weapon weapon)
                        {
                            if (weapon.Damage > 4)
                                Items[i] = State.World.ItemRepository.GetItem(ItemType.Axe);
                            else
                                Items[i] = State.World.ItemRepository.GetItem(ItemType.Mace);
                        }
                    }
                }
            }

            if (!skipTraitItems) GiveTraitBooks();
        }
    }

    internal void GiveTraitBooks()
    {
        if (State.World?.ItemRepository == null) return;
        var tiers = new List<int>();
        if (HasTrait(TraitType.BookWormI))
        {
            tiers.Add(1);
        }

        if (HasTrait(TraitType.BookWormIi))
        {
            tiers.Add(2);
        }

        if (HasTrait(TraitType.BookWormIii))
        {
            tiers.Add(3);
        }

        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] != null && Items[i] is SpellBook)
            {
                if (((SpellBook)Items[i]).Tier == 4)
                    tiers.Remove(3);
                else
                    tiers.Remove(((SpellBook)Items[i]).Tier);
            }
        }

        for (int i = 0; i < Items.Length; i++)
        {
            if (tiers.Count > 0)
            {
                if (Items[i] == null)
                {
                    int t = tiers.Count > 1 ? tiers[State.Rand.Next(tiers.Count - 1)] : tiers[0];
                    Items[i] = State.World.ItemRepository.GetRandomBook(t, t == 3 ? 4 : t, true);
                    tiers.Remove(t);
                }
            }
        }

        if (tiers.Count > 0 && HasTrait(TraitType.BookEater))
        {
            foreach (int t in tiers)
            {
                SpellType spell = ((SpellBook)State.World.ItemRepository.GetRandomBook(t, t == 3 ? 4 : t, true)).ContainedSpell;
                if (!InnateSpells.Contains(spell)) InnateSpells.Add(spell);
            }
        }
    }

    internal Unit Clone()
    {
        var clone = (Unit)MemberwiseClone();
        clone.Stats = new int[(int)Stat.None];
        for (int i = 0; i < Stats.Length; i++)
        {
            clone.Stats[i] = Stats[i];
        }

        //clone.Items = (Item[])Items.Clone();
        //for (int i = 0; i < Items.Length; i++)
        //{
        //    clone.SetItem(null, i);
        //}
        return clone;
    }

    internal void CopyAppearance(Unit appearance)
    {
        HairColor = appearance.HairColor;
        HairStyle = appearance.HairStyle;
        BeardStyle = appearance.HairStyle;
        SkinColor = appearance.SkinColor;
        AccessoryColor = appearance.AccessoryColor;
        EyeColor = appearance.EyeColor;
        ExtraColor1 = appearance.ExtraColor1;
        ExtraColor2 = appearance.ExtraColor2;
        ExtraColor3 = appearance.ExtraColor3;
        ExtraColor4 = appearance.ExtraColor4;
        EyeType = appearance.EyeType;
        MouthType = appearance.MouthType;
        BreastSize = appearance.BreastSize;
        DickSize = appearance.DickSize;
        HasVagina = appearance.HasVagina;
        BodySize = appearance.BodySize;
        SpecialAccessoryType = appearance.SpecialAccessoryType;
        BodySizeManuallyChanged = appearance.BodySizeManuallyChanged;
        DefaultBreastSize = appearance.DefaultBreastSize;
        ClothingType = appearance.ClothingType;
        ClothingType2 = appearance.ClothingType2;
        ClothingHatType = appearance.ClothingHatType;
        ClothingAccessoryType = appearance.ClothingAccessoryType;
        ClothingExtraType1 = appearance.ClothingExtraType1;
        ClothingExtraType2 = appearance.ClothingExtraType2;
        ClothingExtraType3 = appearance.ClothingExtraType3;
        ClothingExtraType4 = appearance.ClothingExtraType4;
        ClothingExtraType5 = appearance.ClothingExtraType5;
        ClothingColor = appearance.ClothingColor;
        ClothingColor2 = appearance.ClothingColor2;
        ClothingColor3 = appearance.ClothingColor3;
        Furry = appearance.Furry;
        HeadType = appearance.HeadType;
        TailType = appearance.TailType;
        FurType = appearance.FurType;
        EarType = appearance.EarType;
        BodyAccentType1 = appearance.BodyAccentType1;
        BodyAccentType2 = appearance.BodyAccentType2;
        BodyAccentType3 = appearance.BodyAccentType3;
        BodyAccentType4 = appearance.BodyAccentType4;
        BodyAccentType5 = appearance.BodyAccentType5;
        BallsSize = appearance.BallsSize;
        VulvaType = appearance.VulvaType;
        BasicMeleeWeaponType = appearance.BasicMeleeWeaponType;
        AdvancedMeleeWeaponType = appearance.AdvancedMeleeWeaponType;
        BasicRangedWeaponType = appearance.BasicRangedWeaponType;
        AdvancedRangedWeaponType = appearance.AdvancedRangedWeaponType;
    }

    internal void SetGenderRandomizeName(Race race, Gender gender)
    {
        var raceData = RaceFuncs.GetRace(this);
        var isMale = false;
        if (raceData.SetupOutput.CanBeGender.Count == 0 || (raceData.SetupOutput.CanBeGender.Contains(Gender.None) && raceData.SetupOutput.CanBeGender.Count == 1))
        {
            DickSize = 0;
            SetDefaultBreastSize(0);
            isMale = State.Rand.NextDouble() > Config.HermNameFraction;
        }
        else if (gender == Gender.Hermaphrodite && raceData.SetupOutput.CanBeGender.Contains(Gender.Hermaphrodite))
        {
            DickSize = 0;
            SetDefaultBreastSize(0);
            isMale = State.Rand.NextDouble() > Config.HermNameFraction;
        }
        else if ((gender == Gender.Male && raceData.SetupOutput.CanBeGender.Contains(Gender.Male)) || raceData.SetupOutput.CanBeGender.Contains(Gender.Female) == false)
        {
            DickSize = 0;
            SetDefaultBreastSize(-1);
            isMale = true;
        }
        else
        {
            SetDefaultBreastSize(0);
            DickSize = -1;
            isMale = false;
        }

        if (RaceFuncs.IsMosnterOrUniqueMerc(race))
        {
            Name = State.NameGen.GetMonsterName(isMale, race);
        }
        else
        {
            Name = State.NameGen.GetName(isMale, race);
        }


        ReloadTraits();
        raceData.RandomCustomCall(this);
        InitializeTraits();
    }

    internal void TotalRandomizeAppearance()
    {
        var raceData = RaceFuncs.GetRace(this);
        RandomizeNameAndGender(Race, raceData, true);
        raceData.RandomCustomCall(this);
    }

    internal void RandomizeAppearance()
    {
        var raceData = RaceFuncs.GetRace(this);
        raceData.RandomCustomCall(this);
    }

    internal void RandomizeNameAndGender(Race race, IRaceData raceData, bool skipTraits = false)
    {
        var raceStats = State.RaceSettings.Get(race);
        float maleFraction;
        float hermFraction;
        bool isMale = false;
        if (raceStats.OverrideGender)
        {
            maleFraction = raceStats.MaleFraction;
            hermFraction = raceStats.HermFraction;
        }
        else
        {
            maleFraction = Config.MaleFraction;
            hermFraction = Config.HermFraction;
        }

        if (raceData.SetupOutput.CanBeGender.Count == 0 || (raceData.SetupOutput.CanBeGender.Contains(Gender.None) && raceData.SetupOutput.CanBeGender.Count == 1))
        {
            DickSize = 0;
            SetDefaultBreastSize(0);
            HasVagina = false;
            Pronouns = new List<string> { "they", "them", "their", "theirs", "themself", "plural" };
            isMale = State.Rand.NextDouble() > Config.HermNameFraction;
        }
        else if (State.Rand.NextDouble() < hermFraction && raceData.SetupOutput.CanBeGender.Contains(Gender.Hermaphrodite))
        {
            DickSize = 0;
            SetDefaultBreastSize(0);
            HasVagina = Config.HermsCanUb;
            Pronouns = new List<string> { "they", "them", "their", "theirs", "themself", "plural" };
            isMale = State.Rand.NextDouble() > Config.HermNameFraction;
        }
        else if ((State.Rand.NextDouble() < maleFraction && raceData.SetupOutput.CanBeGender.Contains(Gender.Male)) || raceData.SetupOutput.CanBeGender.Contains(Gender.Female) == false)
        {
            DickSize = 0;
            SetDefaultBreastSize(-1);
            HasVagina = false;
            Pronouns = new List<string> { "he", "him", "his", "his", "himself", "singular" };
            isMale = true;
        }
        else
        {
            SetDefaultBreastSize(0);
            DickSize = -1;
            HasVagina = true;
            Pronouns = new List<string> { "she", "her", "her", "hers", "herself", "singular" };
            isMale = false;
        }

        if (RaceFuncs.IsMosnterOrUniqueMerc(race))
        {
            Name = State.NameGen.GetMonsterName(isMale, race);
        }
        else
        {
            Name = State.NameGen.GetName(isMale, race);
        }

        if (skipTraits == false)
        {
            ReloadTraits();
            InitializeTraits();
        }
    }

    internal void GeneratePronouns()
    {
        if (GetGender() == Gender.Female)
            Pronouns = new List<string> { "she", "her", "her", "hers", "herself", "singular" };
        else if (GetGender() == Gender.Male)
            Pronouns = new List<string> { "he", "him", "his", "his", "himself", "singular" };
        else
            Pronouns = new List<string> { "they", "them", "their", "theirs", "themself", "plural" };
    }

    internal void RandomizeGender(Race race, IRaceData raceData, bool skipTraits = false)
    {
        var raceStats = State.RaceSettings.Get(race);
        float maleFraction;
        float hermFraction;
        if (raceStats.OverrideGender)
        {
            maleFraction = raceStats.MaleFraction;
            hermFraction = raceStats.HermFraction;
        }
        else
        {
            maleFraction = Config.MaleFraction;
            hermFraction = Config.HermFraction;
        }

        if (raceData.SetupOutput.CanBeGender.Count == 0 || (raceData.SetupOutput.CanBeGender.Contains(Gender.None) && raceData.SetupOutput.CanBeGender.Count == 1))
        {
            DickSize = 0;
            SetDefaultBreastSize(0);
            HasVagina = false;
            Pronouns = new List<string> { "they", "them", "their", "theirs", "themself", "plural" };
        }
        else if (State.Rand.NextDouble() < hermFraction && raceData.SetupOutput.CanBeGender.Contains(Gender.Hermaphrodite))
        {
            DickSize = 0;
            SetDefaultBreastSize(0);
            HasVagina = true;
            Pronouns = new List<string> { "they", "them", "their", "theirs", "themself", "plural" };
        }
        else if ((State.Rand.NextDouble() < maleFraction && raceData.SetupOutput.CanBeGender.Contains(Gender.Male)) || raceData.SetupOutput.CanBeGender.Contains(Gender.Female) == false)
        {
            DickSize = 0;
            SetDefaultBreastSize(-1);
            HasVagina = false;
            Pronouns = new List<string> { "he", "him", "his", "his", "himself", "singular" };
        }
        else
        {
            SetDefaultBreastSize(0);
            DickSize = -1;
            HasVagina = true;
            Pronouns = new List<string> { "she", "her", "her", "hers", "herself", "singular" };
        }

        if (skipTraits == false)
        {
            ReloadTraits();
            InitializeTraits();
        }
    }

    public void GiveExp(float exp, bool voreSource = false, bool isKill = false)
    {
        exp *= TraitBoosts.ExpGain;

        if (State.World.GetEmpireOfSide(Side)?.StrategicAI is StrategicAI ai)
        {
            if (ai.CheatLevel > 0) exp *= 1 + .25f * ai.CheatLevel;
        }

        if (voreSource) exp *= TraitBoosts.ExpGainFromVore;
        if (voreSource && isKill) exp *= TraitBoosts.ExpGainFromAbsorption;

        Experience += exp;
    }

    public void GiveRawExp(int exp) => Experience += exp;

    public bool IsDeadAndOverkilledBy(int overkill)
    {
        return Health < 1 - overkill;
    }

    public bool IsThisCloseToDeath(int lessThanThisDamageAwayFromDeath)
    {
        return Health < 1 + lessThanThisDamageAwayFromDeath && !IsDead;
    }


    public void Kill()
    {
        TimesKilled++;
        if (SavedCopy != null) SavedCopy.TimesKilled++;
    }

    public void DrainExp(float exp)
    {
        Experience -= exp;
    }

    public void GiveScaledExp(float exp, int attackerLevelAdvantage, bool voreSource = false, bool isKill = false)
    {
        if (Config.FlatExperience)
        {
            GiveExp(exp, voreSource, isKill);
            return;
        }

        if (State.World.GetEmpireOfSide(Side)?.StrategicAI is StrategicAI ai)
        {
            if (ai.CheatLevel > 0) exp *= 1 + .25f * ai.CheatLevel;
        }

        exp *= TraitBoosts.ExpGain;
        if (voreSource) exp *= TraitBoosts.ExpGainFromVore;
        if (voreSource && isKill) exp *= TraitBoosts.ExpGainFromAbsorption;

        if (attackerLevelAdvantage > 0)
            exp = Math.Max(exp * (1 - (float)Math.Pow(attackerLevelAdvantage, 1.2) / 24f), .3f * exp);
        else if (attackerLevelAdvantage < 0)
        {
            exp = Math.Min(exp * (1 + (float)Math.Pow(-attackerLevelAdvantage, 1.2) / 12f), 6f * exp);
        }

        Experience += exp;
    }

    public static int GetExperienceRequiredForLevel(int level, float expRequiredMod)
    {
        if (level >= Config.HardLevelCap) return 99999999;
        return (int)(expRequiredMod * level * Config.ExperiencePerLevel + ((level >= Config.SoftLevelCap ? 8 : 0) + level * Config.AdditionalExperiencePerLevel * (level - 1) / 2) *
            (level >= Config.SoftLevelCap ? (int)Math.Pow(2, level + 1 - Config.SoftLevelCap) : 1));
    }

    public static int GetLevelFromExperience(int experience)
    {
        for (int i = 0; i < 200; i++)
        {
            if (GetExperienceRequiredForLevel(i, 1) < experience) return i;
        }

        return 200;
    }

    public int ExperienceRequiredForNextLevel => GetExperienceRequiredForLevel(Level);
    public int GetExperienceRequiredForLevel(int lvl) => GetExperienceRequiredForLevel(lvl, ExpRequiredMod);
    private float ExpRequiredMod => TraitBoosts.ExpRequired;
    public bool HasEnoughExpToLevelUp() => Experience >= ExperienceRequiredForNextLevel;

    private float _healthPct = 100f;

    public float HealthPct
    {
        get
        {
            _healthPct = (float)Health / MaxHealth;
            return _healthPct;
        }
    }

    internal float GetHealthPctWithoutUpdating() // Important for calculating stat boosts that depend on health percentages, otherwise it's circular.
    {
        return _healthPct;
    }

    internal void ClearAllTraits()
    {
        Tags = new List<TraitType>();
        InitializeTraits();
    }

    internal List<TraitType> GetTraits
    {
        get
        {
            if (PermanentTraits == null) return Tags.ToList();
            if (SharedTraits == null) return Tags.Concat(PermanentTraits).ToList();
            return Tags.Concat(PermanentTraits).ToList().Concat(SharedTraits).ToList();
        }
    }

    internal int BaseTraitsCount => Tags.Count + PermanentTraits.Count;

    public int GetStatBase(Stat stat) => Stats[(int)stat];
    public void SetStatBase(Stat stat, int value) => Stats[(int)stat] = value;

    public int GetLeaderBonus()
    {
        if (CurrentLeader == null) return 0;
        if (CurrentLeader.IsDead) return 0;
        return CurrentLeader.Stats[(int)Stat.Leadership] / 10;
    }

    public int GetTraitBonus(Stat stat)
    {
        if (StatBoosts == null) InitializeTraits();
        int bonus = 0;

        if (Harassed) bonus -= (int)(GetStatBase(stat) * 0.08f);

        foreach (IStatBoost trait in StatBoosts)
        {
            bonus += trait.StatBoost(this, stat);
        }

        return bonus;
    }

    public int GetEffectBonus(Stat stat)
    {
        float bonus = 0;
        if (stat == Stat.Voracity)
        {
            var effect = GetStatusEffect(StatusEffectType.Predation);
            if (effect != null)
            {
                bonus += GetStatBase(Stat.Voracity) * .25f;
            }
        }
        else if (stat == Stat.Stomach)
        {
            var effect = GetStatusEffect(StatusEffectType.Predation);
            if (effect != null)
            {
                bonus += effect.Strength;
            }
        }

        // todo: store the original duration of the effect, maybe?
        // can have it wear off over time, no matter the total duration
        if (GetStatusEffect(StatusEffectType.Empowered) != null)
        {
            StatusEffect eff = GetStatusEffect(StatusEffectType.Empowered);
            bonus += GetStatBase(stat) * eff.Strength * eff.Duration / 5;
        }

        if (GetStatusEffect(StatusEffectType.Berserk) != null)
        {
            if (stat == Stat.Voracity)
            {
                bonus += GetStatBase(Stat.Voracity);
            }

            if (stat == Stat.Strength)
            {
                bonus += GetStatBase(Stat.Strength);
            }
        }

        if (stat == Stat.Strength && GetStatusEffect(StatusEffectType.BladeDance) != null) bonus += 2 * GetStatusEffect(StatusEffectType.BladeDance).Duration;
        if (stat == Stat.Agility && GetStatusEffect(StatusEffectType.BladeDance) != null) bonus += 1 * GetStatusEffect(StatusEffectType.BladeDance).Duration;

        if (stat == Stat.Strength && GetStatusEffect(StatusEffectType.Tenacious) != null) bonus += GetStatBase(Stat.Strength) * .1f * GetStatusEffect(StatusEffectType.Tenacious).Duration;
        if (stat == Stat.Agility && GetStatusEffect(StatusEffectType.Tenacious) != null) bonus += GetStatBase(Stat.Agility) * .1f * GetStatusEffect(StatusEffectType.Tenacious).Duration;
        if (stat == Stat.Voracity && GetStatusEffect(StatusEffectType.Tenacious) != null) bonus += GetStatBase(Stat.Voracity) * .1f * GetStatusEffect(StatusEffectType.Tenacious).Duration;
        if (stat == Stat.Mind && GetStatusEffect(StatusEffectType.Focus) != null)
        {
            int stacks = GetStatusEffect(StatusEffectType.Focus).Duration;
            bonus += stacks + GetStatBase(Stat.Mind) * (stacks / 100);
        }

        if (stat == Stat.Mind && GetStatusEffect(StatusEffectType.SpellForce) != null)
        {
            int stacks = GetStatusEffect(StatusEffectType.SpellForce).Duration;
            bonus += stacks + GetStatBase(Stat.Mind) * (stacks / 10);
        }

        bonus -= GetStatBase(stat) * (GetStatusEffect(StatusEffectType.Shaken)?.Strength ?? 0);

        if (GetStatusEffect(StatusEffectType.Webbed) != null) bonus -= GetStatBase(stat) * .3f;


        return Mathf.RoundToInt(bonus);
    }

    public int GetStat(Stat stat)
    {
        float total = GetStatBase(stat) + GetLeaderBonus() + GetTraitBonus(stat) + GetEffectBonus(stat);

        total *= GetScale();

        total *= TraitBoosts.StatMult;

        if (total < 1) return 1;

        return Mathf.RoundToInt(total);
    }

    public float GetScale(int power = 1)
    {
        float scale = (float)BaseScale;

        scale *= 1.0f + (GetStatusEffect(StatusEffectType.Enlarged)?.Strength ?? 0f);
        scale *= 1.0f - (GetStatusEffect(StatusEffectType.Diminished)?.Strength ?? 0f);

        scale *= TraitBoosts.Scale;

        return Mathf.Pow(scale, power);
    }

    public string GetStatInfo(Stat stat)
    {
        int modStat = GetStat(stat);
        int baseStat = GetStatBase(stat);
        if (modStat > baseStat)
        {
            if (Config.HideBaseStats) return $"<color=#007000ff>{modStat}</color>";
            return $"<color=#007000ff>{modStat}</color> ({baseStat})";
        }
        else if (modStat < baseStat)
        {
            if (Config.HideBaseStats) return $"<color=red>{modStat}</color>";
            return $"<color=red>{modStat}</color> ({baseStat})";
        }
        else
            return $"{modStat}";
    }

    public void SetExp(float exp) => Experience = exp;

    internal void ModifyStat(int stat, int amount) => ModifyStat((Stat)stat, amount);

    internal void ModifyStat(Stat stat, int amount)
    {
        Stats[(int)stat] += amount;
        if (stat == Stat.Endurance) Health += 2 * amount;
        if (stat == Stat.Strength) Health += 1 * amount;
    }

    internal void InitializeTraits()
    {
        //foreach (IAttackStatusEffect trait in Unit.TraitsList.Where(s => s is IAttackStatusEffect))
        TraitBoosts = new PermanentBoosts();
        //TraitsList = new List<Trait>();
        StatBoosts = new List<IStatBoost>();
        VoreAttackOdds = new List<IVoreAttackOdds>();
        VoreDefenseOdds = new List<IVoreDefenseOdds>();
        AttackStatusEffects = new List<IAttackStatusEffect>();
        PhysicalDefenseOdds = new List<IPhysicalDefenseOdds>();

        RecalculateStatBoosts();
    }

    internal void RefreshSecrecy()
    {
        if (HasTrait(TraitType.Infiltrator) || HasTrait(TraitType.Corruption)) HiddenFixedSide = true;
    }

    internal void InitializeFixedSide(Side side)
    {
        if (State.World?.ItemRepository == null) return; //protection for the create strat screen
        if (RaceFuncs.IsNotNone(_fixedSide)) return;
        if (HasTrait(TraitType.Untamable))
        {
            FixedSide = State.World.GetEmpireOfRace(Race)?.Side ?? side;
            return;
        }

        if (HasTrait(TraitType.Infiltrator) || HasTrait(TraitType.Corruption))
        {
            FixedSide = side;
            return;
        }

        FixedSide = Side.TrueNoneSide;
    }

    public bool HasTrait(TraitType tag)
    {
        if (tag == TraitType.TheGreatEscape && Equals(Race, Race.Erin)) return true;
        if (Tags != null) return Tags.Contains(tag) || (PermanentTraits?.Contains(tag) ?? false);
        return false;
    }

    public void HealPercentageCap(float rate, int cap)
    {
        int heal = (int)(MaxHealth * rate);
        if (heal <= 0)
        {
            heal = 1;
        }

        heal = Math.Min(heal, cap);
        if (heal > MaxHealth - Health) heal = MaxHealth - Health;
        var actor = TacticalUtilities.Units.FirstOrDefault(s => s.Unit == this);
        if (actor != null && heal != 0) actor.UnitSprite.DisplayDamage(-heal);
        Health += heal;
    }

    public void HealPercentage(float rate)
    {
        rate *= TraitBoosts.PassiveHeal;
        int h = (int)(MaxHealth * rate);
        if (h <= 0)
        {
            h = 1;
        }

        Health += h;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    public int Heal(int amount)
    {
        int diff = MaxHealth - Health;
        Health += amount;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }

        int actualHeal = Math.Min(diff, amount);
        State.GameManager.TacticalMode?.TacticalStats?.RegisterHealing(actualHeal, Side);
        return actualHeal;
    }

    private void NonFatalDamage(int amount, string type)
    {
        if (Health <= 0) return;
        int actual = Math.Min(Health - 1, amount);
        Health -= actual;
        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{Name}</b> took <color=red>{actual}</color> points of {type} damage");
    }

    internal string ListTraits(bool hideSecret = false)
    {
        if (Tags.Count == 0 && (PermanentTraits == null || PermanentTraits.Count == 0)) return "";
        string ret = "";
        for (int i = 0; i < Tags.Count; i++)
        {
            if (!(hideSecret && SecretTags.Contains(Tags[i])))
            {
                if (ret != "") ret += "\n";
                if (TemporaryTraits != null && TemporaryTraits.Count > 0 && TemporaryTraits.Contains(Tags[i]))
                {
                    if (PermanentTraits != null && PermanentTraits.Count > 0 && !PermanentTraits.Contains(Tags[i])) ret += "<color=#402B8Dff>" + Tags[i].ToString() + "</color>";
                }
                else
                    ret += Tags[i].ToString();
            }
        }

        if (PermanentTraits != null && PermanentTraits.Count > 0)
        {
            for (int i = 0; i < PermanentTraits.Count; i++)
            {
                if (Tags.Contains(PermanentTraits[i])) continue;
                if (!(hideSecret && SecretTags.Contains(PermanentTraits[i])))
                {
                    if (ret != "") ret += "\n";
                    ret += PermanentTraits[i].ToString();
                }
            }
        }

        return ret;
    }

    public bool IsEnemyOfSide(Side side)
    {
        return !Equals(Side, side);
    }

    public void AddTraits(List<TraitType> traitIdsToAdd)
    {
        foreach (var traitId in traitIdsToAdd)
        {
            AddTrait(traitId);
        }

        return;
    }

    public void AddTrait(TraitType traitTypeToAdd)
    {
        if (Tags == null) Tags = new List<TraitType>();

        if (HasTrait(traitTypeToAdd)) return;

        Tags.Add(traitTypeToAdd);
        RecalculateStatBoosts();
    }

    public bool AddPermanentTrait(TraitType traitTypeToAdd)
    {
        if (PermanentTraits == null) PermanentTraits = new List<TraitType>();

        if (traitTypeToAdd == TraitType.Prey)
        {
            Predator = false;
            FixedPredator = true;
        }

        if (HasTrait(traitTypeToAdd)) return false;

        PermanentTraits.Add(traitTypeToAdd);
        RecalculateStatBoosts();
        return true;
    }

    public void RemoveTrait(TraitType traitTypeToRemove)
    {
        if (Tags == null) return;

        if (RemovedTraits == null) RemovedTraits = new List<TraitType>();

        if (traitTypeToRemove == TraitType.Prey)
        {
            if (RaceParameters.GetTraitData(this).AllowedVoreTypes.Any())
            {
                Predator = true;
                FixedPredator = true;
            }
            else //Cancel removal in this case
                return;
        }

        RemovedTraits.Add(traitTypeToRemove);

        if (HasTrait(traitTypeToRemove))
        {
            Tags.Remove(traitTypeToRemove);
            PermanentTraits?.Remove(traitTypeToRemove);
            RecalculateStatBoosts();
        }
    }

    protected void RecalculateStatBoosts()
    {
        float healthBefore = HealthPct;
        RefreshSecrecy();
        InitializeFixedSide(Side);
        if (Tags == null) return;

        TraitBoosts = new PermanentBoosts();
        StatBoosts.Clear();
        VoreAttackOdds.Clear();
        VoreDefenseOdds.Clear();
        PhysicalDefenseOdds.Clear();
        AttackStatusEffects.Clear();
        if (PermanentTraits == null)
        {
            foreach (var tag in Tags)
            {
                Trait trait = TraitList.GetTrait(tag);
                if (trait is IStatBoost boost) StatBoosts.Add(boost);
                if (trait is IVoreAttackOdds voreAttackOdds) VoreAttackOdds.Add(voreAttackOdds);
                if (trait is IVoreDefenseOdds voreDefenseOdds) VoreDefenseOdds.Add(voreDefenseOdds);
                if (trait is IPhysicalDefenseOdds physicalDefenseOdds) PhysicalDefenseOdds.Add(physicalDefenseOdds);
                if (trait is IAttackStatusEffect attackStatusEffect) AttackStatusEffects.Add(attackStatusEffect);
                if (trait is AbstractBooster booster) booster.Boost(TraitBoosts);
            }
        }

        if (PermanentTraits != null)
        {
            foreach (var tag in Tags.Concat(PermanentTraits).Distinct())
            {
                Trait trait = TraitList.GetTrait(tag);
                if (trait is IStatBoost boost) StatBoosts.Add(boost);
                if (trait is IVoreAttackOdds voreAttackOdds) VoreAttackOdds.Add(voreAttackOdds);
                if (trait is IVoreDefenseOdds voreDefenseOdds) VoreDefenseOdds.Add(voreDefenseOdds);
                if (trait is IPhysicalDefenseOdds physicalDefenseOdds) PhysicalDefenseOdds.Add(physicalDefenseOdds);
                if (trait is IAttackStatusEffect attackStatusEffect) AttackStatusEffects.Add(attackStatusEffect);
                if (trait is AbstractBooster booster) booster.Boost(TraitBoosts);
            }
        }

        if (SharedTraits != null)
        {
            foreach (var tag in SharedTraits)
            {
                Trait trait = TraitList.GetTrait(tag);
                if (trait is IStatBoost boost) StatBoosts.Add(boost);
                if (trait is IVoreAttackOdds voreAttackOdds) VoreAttackOdds.Add(voreAttackOdds);
                if (trait is IVoreDefenseOdds voreDefenseOdds) VoreDefenseOdds.Add(voreDefenseOdds);
                if (trait is IPhysicalDefenseOdds physicalDefenseOdds) PhysicalDefenseOdds.Add(physicalDefenseOdds);
                if (trait is IAttackStatusEffect attackStatusEffect) AttackStatusEffects.Add(attackStatusEffect);
                if (trait is AbstractBooster booster) booster.Boost(TraitBoosts);
            }
        }
    }


    internal void SetMaxItems()
    {
        Items = new Item[0];
        if (HasTrait(TraitType.Resourceful) == false)
        {
            if (Items.Count() == 3) SetItem(null, 2);
            if (Items.Count() == 2) return;
            if (Items.Count() == 0)
            {
                Items = new Item[2];
                return;
            }

            Item[] tempItems = new Item[]
            {
                Items[0],
                Items[1],
            };
            Items = tempItems;
        }
        else
        {
            if (Items.Count() == 3) return;
            if (Items.Count() == 2)
            {
                Item[] tempItems = new Item[]
                {
                    Items[0],
                    Items[1],
                    null
                };
                Items = tempItems;
            }
            else
                Items = new Item[3];
        }
    }

    internal void AddTemporaryTrait(TraitType traitType)
    {
        if (TemporaryTraits == null) TemporaryTraits = new List<TraitType>();
        if (TemporaryTraits.Contains(traitType) == false) TemporaryTraits.Add(traitType);
        if (TemporaryTraits.Count >= 4) TemporaryTraits.RemoveAt(0);
    }

    public bool HasSharedTrait(TraitType traitType)
    {
        if (SharedTraits == null) SharedTraits = new List<TraitType>();
        return SharedTraits.Contains(traitType);
    }

    public void AddSharedTrait(TraitType traitType)
    {
        if (SharedTraits == null) SharedTraits = new List<TraitType>();
        if (!SharedTraits.Contains(traitType) && !HasTrait(traitType)) SharedTraits.Add(traitType);
        AddTrait(traitType);
    }

    public void ResetSharedTraits()
    {
        if (SharedTraits == null) SharedTraits = new List<TraitType>();
        foreach (TraitType trait in SharedTraits)
        {
            RemoveTrait(trait);
        }

        SharedTraits.Clear();
    }

    public void RemoveSharedTrait(TraitType traitType)
    {
        if (SharedTraits == null) SharedTraits = new List<TraitType>();
        if (SharedTraits.Contains(traitType) && HasTrait(traitType)) SharedTraits.Remove(traitType);
        RemoveTrait(traitType);
    }

    public bool HasPersistentSharedTrait(TraitType traitType)
    {
        if (PersistentSharedTraits == null) PersistentSharedTraits = new List<TraitType>();
        return PersistentSharedTraits.Contains(traitType);
    }

    public void AddPersistentSharedTrait(TraitType traitType)
    {
        if (PersistentSharedTraits == null) PersistentSharedTraits = new List<TraitType>();
        if (!PersistentSharedTraits.Contains(traitType) && !HasTrait(traitType)) PersistentSharedTraits.Add(traitType);
        AddTrait(traitType);
    }

    public void ResetPersistentSharedTraits()
    {
        if (PersistentSharedTraits == null) PersistentSharedTraits = new List<TraitType>();
        foreach (TraitType trait in PersistentSharedTraits)
        {
            RemoveTrait(trait);
        }

        PersistentSharedTraits.Clear();
    }

    public void RemovePersistentSharedTrait(TraitType traitType)
    {
        if (PersistentSharedTraits == null) PersistentSharedTraits = new List<TraitType>();
        if (PersistentSharedTraits.Contains(traitType) && HasTrait(traitType)) PersistentSharedTraits.Remove(traitType);
        RemoveTrait(traitType);
    }

    internal void ReloadTraits()
    {
        Tags = new List<TraitType>();
        if (Config.RaceTraitsEnabled)
        {
            var traits = State.RaceSettings.GetRaceTraits(HiddenUnit.Race);
            // TODO it's possible that this should never need a null check *if* it's working correctly
            // So it might not be working correctly right now, and fixing it would require removing this
            // This could be hiding an exception that *should* actually happen
            if (traits != null) Tags.AddRange(traits);
        }

        if (HiddenUnit.HasBreasts && HiddenUnit.HasDick == false)
        {
            var femaleTraits = State.RaceSettings.GetFemaleRaceTraits(HiddenUnit.Race);
            if (femaleTraits != null) Tags.AddRange(femaleTraits);
            femaleTraits = Config.FemaleTraits;
            if (femaleTraits != null) Tags.AddRange(femaleTraits);
        }
        else if (!HiddenUnit.HasBreasts && HiddenUnit.HasDick)
        {
            var maleTraits = State.RaceSettings.GetMaleRaceTraits(HiddenUnit.Race);
            if (maleTraits != null) Tags.AddRange(maleTraits);
            maleTraits = Config.MaleTraits;
            if (maleTraits != null) Tags.AddRange(maleTraits);
        }
        else
        {
            var hermTraits = State.RaceSettings.GetHermRaceTraits(HiddenUnit.Race);
            if (hermTraits != null) Tags.AddRange(hermTraits);
            hermTraits = Config.HermTraits;
            if (hermTraits != null) Tags.AddRange(hermTraits);
        }

        if (Type == UnitType.Leader)
        {
            var leaderTraits = State.RaceSettings.GetLeaderRaceTraits(HiddenUnit.Race);
            if (leaderTraits != null) Tags.AddRange(leaderTraits);
            if (Config.LeaderTraits != null) Tags.AddRange(Config.LeaderTraits);
        }
        else if (Type == UnitType.Spawn)
        {
            var spawnTraits = State.RaceSettings.GetSpawnRaceTraits(HiddenUnit.Race);
            if (spawnTraits != null) Tags.AddRange(spawnTraits);
            spawnTraits = Config.SpawnTraits;
            if (spawnTraits != null) Tags.AddRange(spawnTraits);
        }

        if (TemporaryTraits != null) Tags.AddRange(TemporaryTraits);
        if (SharedTraits != null) Tags.AddRange(SharedTraits);
        if (PersistentSharedTraits != null) Tags.AddRange(PersistentSharedTraits);
        if (RemovedTraits != null)
        {
            foreach (TraitType trait in RemovedTraits)
            {
                Tags.Remove(trait);
            }
        }

        if (!State.TutorialMode) RandomizeTraits();
        Tags = Tags.Distinct().ToList();
        if (HasTrait(TraitType.Prey))
            Predator = false;
        else if (FixedPredator == false) Predator = State.World?.GetEmpireOfRace(HiddenUnit.Race)?.CanVore ?? true;
        Tags.RemoveAll(s => s == TraitType.Prey);
        if (RaceParameters.GetTraitData(HiddenUnit).AllowedVoreTypes.Any() == false) Predator = false;
        if (HiddenUnit.Predator == false && !HasTrait(TraitType.Prey)) Tags.Add(TraitType.Prey);
        SetMaxItems();
        //if (HasTrait(Traits.Shapeshifter) || HasTrait(Traits.Skinwalker))
        //{
        //    if (ShifterShapes == null)
        //        ShifterShapes = new List<Unit>();
        //    if (!ShifterShapes.Contains(this))
        //        AcquireShape(this, true);
        //}
    }

    public void ChangeRace(Race race)
    {
        Race = race;
        FixedPredator = false;
    }

    public void HideRace(Race race, Unit appearance = null)
    {
        _hiddenUnit = Clone();
        Race = race;
        FixedPredator = false;
        if (appearance != null)
            CopyAppearance(appearance);
        else
        {
            var newRace = RaceFuncs.GetRace(race);
            newRace.RandomCustomCall(this);
        }
    }

    public void UnhideRace()
    {
        Race = HiddenUnit.Race;
        FixedPredator = false;
        CopyAppearance(HiddenUnit);
        _hiddenUnit = null;
    }

    private void RandomizeTraits()
    {
        while (true)
        {
            var customs = Tags.Where(t => State.RandomizeLists.Any(rl => (TraitType)rl.ID == t)).ToList();
            customs.AddRange(PermanentTraits.Where(t => State.RandomizeLists.Any(rl => (TraitType)rl.ID == t)));
            if (!customs.Any()) break;
            customs.ForEach(ct =>
            {
                RandomizeList randomizeList = State.RandomizeLists.Single(rl => (TraitType)rl.ID == ct);
                var chance = randomizeList.Chance;
                while (chance > 0 && State.Rand.NextDouble() < randomizeList.Chance)
                {
                    List<TraitType> gainable = randomizeList.RandomTraits.Where(rt => !Tags.Contains(rt) && !PermanentTraits.Contains(rt)).ToList();
                    if (gainable.Count() > 0)
                    {
                        var randomPick = gainable[State.Rand.Next(gainable.Count())];
                        PermanentTraits.Add(randomPick);
                        RemovedTraits?.Remove(randomPick); // Even if manually removed before, rng-sus' word is law
                        gainable.Remove(randomPick);
                        GivePrerequisiteTraits(randomPick);
                    }

                    chance -= 1;
                }

                if (RemovedTraits == null) RemovedTraits = new List<TraitType>();
                RemovedTraits.Add(ct);
                foreach (TraitType trait in RemovedTraits)
                {
                    Tags.Remove(trait);
                    PermanentTraits.Remove(trait);
                }
            });
        }
    }

    private void GivePrerequisiteTraits(TraitType randomPick)
    {
        TraitType prereq = (TraitType)(-1);
        if (randomPick > TraitType.Growth && randomPick <= TraitType.FleetingGrowth)
        {
            prereq = TraitType.Growth;
        }

        if (randomPick == TraitType.HealingBelly)
        {
            prereq = TraitType.Endosoma;
        }

        if (randomPick == TraitType.VenomousBite)
        {
            prereq = TraitType.Biter;
        }

        if (randomPick == TraitType.SynchronizedEvolution)
        {
            prereq = TraitType.Assimilate;
        }

        if (randomPick == TraitType.PredConverter || randomPick == TraitType.PredRebirther || randomPick == TraitType.PredGusher)
        {
            if (RaceParameters.GetRaceTraits(Race).AllowedVoreTypes.Contains(VoreType.Unbirth)) HasVagina = true;
        }

        if (randomPick == TraitType.HeavyPounce)
        {
            prereq = TraitType.Pounce;
        }

        if (prereq != (TraitType)(-1) && !Tags.Contains(prereq) && !PermanentTraits.Contains(prereq))
        {
            PermanentTraits.Add(prereq);
            RemovedTraits?.Remove(prereq);
        }
    }

    public void SetSizeToDefault() => BreastSize = DefaultBreastSize;

    public void SetDefaultBreastSize(int size, bool update = true)
    {
        DefaultBreastSize = size;
        if (update) BreastSize = DefaultBreastSize;
    }

    public void SetBreastSize(int size)
    {
        if (size > RaceFuncs.GetRace(this).SetupOutput.BreastSizes() - 1) size = RaceFuncs.GetRace(this).SetupOutput.BreastSizes() - 1;
        if (size <= DefaultBreastSize) size = DefaultBreastSize;
        BreastSize = size;
    }

    public Stat[] GetLevelUpPossibilities(bool canVore)
    {
        if (Type == UnitType.Leader) return Leader.GetLevelUpPossibilities();
        int[] stats = new int[(int)Stat.None - 1];
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] = i;
        }


        if (canVore == false)
        {
            stats[(int)Stat.Voracity] = -1;
            stats[(int)Stat.Stomach] = -1;
        }

        //stats[(int)Stat.Leadership] = -1; unneeded as the stats already cuts it
        if (TraitBoosts.OnLevelUpAllowAnyStat)
        {
            Stat[] ret2 = new Stat[stats.Length];
            for (int i = 0; i < ret2.Length; i++)
            {
                ret2[i] = (Stat)i;
            }

            return ret2;
        }

        var traits = RaceParameters.GetTraitData(this);
        var favored = State.RaceSettings.GetFavoredStat(Race);

        if (favored != Stat.None) stats[(int)favored] = -1;

        stats = stats.Where(s => s >= 0).ToArray();

        for (int i = 0; i < stats.GetUpperBound(0); i++) //Randomize the order
        {
            int j = State.Rand.Next(i, stats.GetUpperBound(0) + 1);
            int temp = stats[i];
            stats[i] = stats[j];
            stats[j] = temp;
        }

        if (favored == Stat.None) //If no favored stat
        {
            Stat[] ret3 = new Stat[3];

            for (int i = 0; i < 3; i++)
            {
                ret3[i] = (Stat)stats[i];
            }

            return ret3;
        }

        Stat[] ret = new Stat[4];

        ret[0] = favored;
        for (int i = 1; i < 4; i++)
        {
            ret[i] = (Stat)stats[i];
        }

        return ret;
    }

    public void GeneralStatIncrease(int amount)
    {
        for (int x = 0; x < Stats.Length; x++)
        {
            if (Stats[x] > 0) Stats[x] += amount;
        }

        Health += 3 * amount;
    }

    public void LevelUp(Stat stat)
    {
        GeneralStatIncrease(1);
        if (TraitBoosts.OnLevelUpBonusToAllStats > 0)
        {
            GeneralStatIncrease(TraitBoosts.OnLevelUpBonusToAllStats);
        }

        if (TraitBoosts.OnLevelUpBonusToGiveToTwoRandomStats > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                int bonusStat = State.Rand.Next((int)Stat.None - 1);
                ModifyStat(bonusStat, TraitBoosts.OnLevelUpBonusToGiveToTwoRandomStats);
            }
        }

        if (Config.LeadersAutoGainLeadership)
        {
            ModifyStat((int)Stat.Leadership, 2);
        }

        Level++;
        Stats[(int)stat] += 4;
        if (stat == Stat.Endurance)
        {
            Health += 8;
        }

        if (stat == Stat.Strength)
        {
            Health += 4;
        }
    }

    public void LeaderLevelDown()
    {
        SubtractLevels(Config.LeaderLossLevels);
        SubtractFraction(Config.LeaderLossExpPct);
        if (Config.WeightGain)
        {
            if (DefaultBreastSize > 0) DefaultBreastSize -= 1;
            if (DickSize > 0) DickSize -= 1;
            if (BodySize > 0) BodySize -= 1;
            if (BodySize > 0) BodySize -= 1;
        }
    }

    /// <summary>
    ///     Subtracts this fraction from the total unit experience, leveling down as needed (i.e. .25 would cause a unit to
    ///     lose 25% of its total XP)
    /// </summary>
    internal void SubtractFraction(float fraction)
    {
        if (fraction > 1 || fraction < 0)
        {
            Debug.LogWarning("Invalid amount passed to subtractFraction");
            return;
        }

        if (fraction == 0) return;
        int startingLevel = Level;
        if (Config.LeaderLossExpPct > 0)
        {
            Experience *= 1 - fraction;

            for (int i = 0; i < startingLevel - 1; i++)
            {
                if (Experience < GetExperienceRequiredForLevel(Level - 1))
                {
                    LevelDown();
                }
            }
        }
    }

    /// <summary>
    ///     Subtracts this many levels from the unit while keeping its % of xp to the next level constant
    /// </summary>
    internal void SubtractLevels(int levelsLost)
    {
        if (levelsLost < 0)
        {
            Debug.LogWarning("Invalid amount passed to subtractFraction");
            return;
        }

        if (levelsLost == 0) return;
        if (levelsLost >= 1 && Level > 1)
        {
            for (int i = 0; i < levelsLost; i++)
            {
                Experience -= GetExperienceRequiredForLevel(Level) - GetExperienceRequiredForLevel(Level - 1);
                LevelDown();
                if (Level <= 1) break;
            }

            if (Experience < 0) Experience = 0;
        }
    }

    public virtual int GetStatTotal()
    {
        return GetStat(Stat.Agility) + GetStat(Stat.Will) + GetStat(Stat.Mind)
               + GetStat(Stat.Dexterity) + GetStat(Stat.Endurance) + GetStat(Stat.Strength)
               + GetStat(Stat.Voracity) + GetStat(Stat.Stomach);
    }

    public void LevelDown()
    {
        if (Level == 1) return;
        int highestType = 0;
        for (int i = 0; i < Stats.Length; i++)
        {
            if (Stats[i] > Stats[highestType]) highestType = i;
        }

        LevelDown((Stat)highestType);
    }

    public void LevelDown(Stat stat)
    {
        if (Level == 1) return;
        GeneralStatIncrease(-1);
        if (TraitBoosts.OnLevelUpBonusToAllStats > 0)
        {
            GeneralStatIncrease(-1 * TraitBoosts.OnLevelUpBonusToAllStats);
        }

        Level--;
        int loweredStat = (int)stat;

        if (TraitBoosts.OnLevelUpBonusToGiveToTwoRandomStats > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                int bonusStat = State.Rand.Next((int)Stat.None - 1);
                ModifyStat(bonusStat, -1 * TraitBoosts.OnLevelUpBonusToGiveToTwoRandomStats);
            }
        }

        Stats[loweredStat] -= 4;
        if (loweredStat == (int)Stat.Endurance)
        {
            Health -= 8;
        }

        if (loweredStat == (int)Stat.Strength)
        {
            Health -= 4;
        }
    }

    public void Feed()
    {
        GiveExp(1, true);
        Health += 10;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    private void RandomSkills()
    {
        var raceStats = State.RaceSettings.GetRaceStats(Race);
        if (raceStats != null)
        {
            Stats[(int)Stat.Strength] = raceStats.Strength.Minimum + State.Rand.Next(raceStats.Strength.Roll);
            Stats[(int)Stat.Dexterity] = raceStats.Dexterity.Minimum + State.Rand.Next(raceStats.Dexterity.Roll);
            Stats[(int)Stat.Endurance] = raceStats.Endurance.Minimum + State.Rand.Next(raceStats.Endurance.Roll);
            Stats[(int)Stat.Mind] = raceStats.Mind.Minimum + State.Rand.Next(raceStats.Mind.Roll);
            Stats[(int)Stat.Will] = raceStats.Will.Minimum + State.Rand.Next(raceStats.Will.Roll);
            Stats[(int)Stat.Agility] = raceStats.Agility.Minimum + State.Rand.Next(raceStats.Agility.Roll);
            Stats[(int)Stat.Voracity] = raceStats.Voracity.Minimum + State.Rand.Next(raceStats.Voracity.Roll);
            Stats[(int)Stat.Stomach] = raceStats.Stomach.Minimum + State.Rand.Next(raceStats.Stomach.Roll);
        }
        else
        {
            //These should not trigger under almost all circumstances, it uses the data at the bottom of RaceParameters now
            Stats[(int)Stat.Strength] = 6 + State.Rand.Next(9);
            Stats[(int)Stat.Dexterity] = 6 + State.Rand.Next(9);
            Stats[(int)Stat.Endurance] = 8 + State.Rand.Next(6);
            Stats[(int)Stat.Mind] = 6 + State.Rand.Next(8);
            Stats[(int)Stat.Will] = 6 + State.Rand.Next(8);
            Stats[(int)Stat.Agility] = 6 + State.Rand.Next(5);
            Stats[(int)Stat.Voracity] = 5 + State.Rand.Next(7);
            Stats[(int)Stat.Stomach] = 12 + State.Rand.Next(4);
        }
    }

    public Weapon GetBestMelee()
    {
        if (HasTrait(TraitType.Feral))
        {
            return State.World.ItemRepository.Claws;
        }

        Weapon best = null;
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] is Weapon weapon)
            {
                if (weapon.Range <= 1)
                {
                    if (best == null)
                    {
                        best = weapon;
                    }
                    else if (weapon.Damage > best.Damage) best = weapon;
                }
            }
        }

        if (best == null)
        {
            best = State.World.ItemRepository.Claws;
        }

        return best;
    }

    public Weapon GetBestRanged()
    {
        if (HasTrait(TraitType.Feral))
        {
            return null;
        }

        Weapon best = null;

        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] is Weapon weapon)
            {
                if (weapon.Range > 1)
                {
                    if (best == null)
                    {
                        best = weapon;
                    }
                    else if (weapon.Range > best.Range) best = weapon;
                }
            }
        }

        return best;
    }

    public Item GetItem(int i) => Items[i];

    public int GetItemSlot(Item item)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == item) return i;
        }

        return -1;
    }

    public bool HasFreeItemSlot()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null) return true;
        }

        return false;
    }

    public void UpdateItems(ItemRepository newRepository)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] != null) SetItem(newRepository.GetNewItemType(Items[i]), i);
        }
    }

    private void AddAccessory(Accessory accessory)
    {
        Stats[accessory.ChangedStat] += accessory.StatBonus;
        if (accessory.ChangedStat == (int)Stat.Endurance) Health += 2 * accessory.StatBonus;
    }

    private void RemoveAccessory(Accessory accessory)
    {
        Stats[accessory.ChangedStat] -= accessory.StatBonus;
        if (accessory.ChangedStat == (int)Stat.Endurance)
        {
            Health -= 2 * accessory.StatBonus;
            if (Health < 1) Health = 1;
        }
    }

    public void SetItem(Item item, int i, bool fromUnitEditor = false)
    {
        if (item == null && (ShifterShapes?.Any() ?? false))
        {
            ShifterShapes.Where(shape => shape.GetItem(i) == GetItem(i)).ForEach(s => // test what happens if main has resourceful but shape has not
            {
                s.Items[i] = null;
            });
        }

        if (Items.Length <= i)
        {
            Debug.LogWarning("Tried to assign item to a non-existent slot!");
            return;
        }

        if (item is SpellBook && HasTrait(TraitType.BookEater) && !fromUnitEditor)
        {
            InnateSpells.Add(((SpellBook)item).ContainedSpell);
            return;
        }

        if (Items[i] != null)
        {
            if (Items[i] is Accessory)
            {
                RemoveAccessory((Accessory)Items[i]);
            }
        }

        Items[i] = item;
        if (Items[i] != null)
        {
            if (item is Accessory)
            {
                AddAccessory((Accessory)item);
            }
        }
    }

    public void UpdateSpells()
    {
        UseableSpells = new List<Spell>();
        if (InnateSpells != null)
        {
            foreach (SpellType type in InnateSpells)
            {
                if (SpellList.SpellDict.TryGetValue(type, out Spell spell))
                {
                    UseableSpells.Add(spell);
                }
            }
        }

        if (SingleUseSpells?.Any() ?? false)
        {
            foreach (var spellType in SingleUseSpells)
            {
                if (SpellList.SpellDict.TryGetValue(spellType, out Spell spell))
                {
                    UseableSpells.Add(spell);
                }
            }
        }

        if (MultiUseSpells?.Any() ?? false)
        {
            foreach (var spellType in MultiUseSpells)
            {
                if (SpellList.SpellDict.TryGetValue(spellType, out Spell spell))
                {
                    UseableSpells.Add(spell);
                }
            }
        }

        var racePar = RaceParameters.GetTraitData(this);

        if (racePar.InnateSpells?.Any() ?? false)
        {
            foreach (SpellType type in racePar.InnateSpells)
            {
                var thisType = type;
                if (thisType > SpellType.Resurrection) thisType = thisType - SpellType.Resurrection + SpellType.AlraunePuff - 1;
                if (SpellList.SpellDict.TryGetValue(thisType, out Spell spell))
                {
                    UseableSpells.Add(spell);
                }
            }
        }

        var grantedSpell = State.RaceSettings.GetInnateSpell(Race);
        if (grantedSpell != SpellType.None)
        {
            if (SpellList.SpellDict.TryGetValue(grantedSpell, out Spell spell))
            {
                UseableSpells.Add(spell);
            }
        }

        foreach (Item item in Items)
        {
            if (item == null) continue;
            if (item is SpellBook book)
            {
                if (SpellList.SpellDict.TryGetValue(book.ContainedSpell, out Spell spell))
                {
                    UseableSpells.Add(spell);
                }
            }
        }
    }

    public void ApplyStatusEffect(StatusEffectType type, float strength, int duration)
    {
        ApplyStatusEffect(type, strength, duration, null);
    }

    public void ApplyStatusEffect(StatusEffectType type, float strength, int duration, Side side)
    {
        if (type == StatusEffectType.Poisoned && HasTrait(TraitType.PoisonSpit)) return;
        StatusEffects.Remove(GetStatusEffect(type)); // if null, nothing happens, otherwise status is effectively overwritten
        StatusEffects.Add(new StatusEffect(type, strength, duration, side));
    }

    internal StatusEffect GetStatusEffect(StatusEffectType type)
    {
        if (type == StatusEffectType.WillingPrey && HasTrait(TraitType.WillingRace)) return new StatusEffect(StatusEffectType.WillingPrey, 0, 100);
        return StatusEffects.Where(s => s.Type == type).OrderByDescending(s => s.Strength).ThenByDescending(s => s.Duration).FirstOrDefault();
    }

    internal int GetNegativeStatusEffects()
    {
        int ret = 0;
        if (HasEffect(StatusEffectType.Clumsiness)) ret++;
        if (HasEffect(StatusEffectType.Diminished)) ret++;
        if (HasEffect(StatusEffectType.Glued)) ret++;
        if (HasEffect(StatusEffectType.Poisoned)) ret++;
        if (HasEffect(StatusEffectType.Shaken)) ret++;
        if (HasEffect(StatusEffectType.Webbed)) ret++;
        if (HasEffect(StatusEffectType.WillingPrey)) ret++;
        if (HasEffect(StatusEffectType.Charmed)) ret++;
        if (HasEffect(StatusEffectType.Hypnotized)) ret++;
        if (HasEffect(StatusEffectType.Sleeping)) ret++;
        if (HasEffect(StatusEffectType.Staggering)) ret++;
        if (HasEffect(StatusEffectType.Virus)) ret++;

        bool HasEffect(StatusEffectType type)
        {
            return GetStatusEffect(type) != null;
        }

        return ret;
    }

    public Side GetApparentSide(Unit viewer = null)
    {
        if (viewer != null && TacticalUtilities.UnitCanSeeTrueSideOfTarget(viewer, this)) return FixedSide;
        return HiddenFixedSide ? Side : FixedSide;
    }

    public void CreateRaceShape(Race race)
    {
        var shape = new Unit(Side, race, (int)Experience, true, Type, ImmuneToDefections);
        foreach (TraitType trait in ShifterShapes[0].PermanentTraits)
        {
            shape.AddPermanentTrait(trait);
        }

        if (RaceFuncs.GetRace(shape.Race).SetupOutput.CanBeGender.Contains(GetGender()))
        {
            shape.SetGenderRandomizeName(race, GetGender());
        }

        shape.Name = Name;
        shape.InnateSpells.AddRange(InnateSpells);
        HiddenFixedSide = false;
        shape._fixedSide = ShifterShapes[0]._fixedSide;
        shape.SavedCopy = ShifterShapes[0].SavedCopy;
        shape.SavedVillage = ShifterShapes[0].SavedVillage;
        shape.BoundUnit = ShifterShapes[0].BoundUnit;
        ShifterShapes[0].ShifterShapes.Add(shape);
        shape.ShifterShapes = ShifterShapes[0].ShifterShapes;
    }

    internal void AddBladeDance()
    {
        var dance = GetStatusEffect(StatusEffectType.BladeDance);
        if (dance != null)
        {
            dance.Duration++;
            dance.Strength++;
        }
        else
        {
            ApplyStatusEffect(StatusEffectType.BladeDance, 1, 1);
        }
    }

    internal void RemoveBladeDance()
    {
        var dance = GetStatusEffect(StatusEffectType.BladeDance);
        if (dance != null)
        {
            dance.Duration--;
            dance.Strength--;
            if (dance.Duration == 0) StatusEffects.Remove(dance);
        }
    }

    internal void AddTenacious()
    {
        var ten = GetStatusEffect(StatusEffectType.Tenacious);
        if (ten != null)
        {
            ten.Duration++;
            ten.Strength++;
        }
        else
        {
            ApplyStatusEffect(StatusEffectType.Tenacious, 1, 1);
        }
    }

    internal void RemoveTenacious()
    {
        var ten = GetStatusEffect(StatusEffectType.Tenacious);
        if (ten != null)
        {
            ten.Duration -= 5;
            ten.Strength -= 5;
            if (ten.Duration <= 0) StatusEffects.Remove(ten);
        }
    }

    internal void AddSpellForce()
    {
        var force = GetStatusEffect(StatusEffectType.SpellForce);
        if (force != null)
        {
            force.Duration++;
            force.Strength++;
        }
        else
        {
            ApplyStatusEffect(StatusEffectType.SpellForce, 1, 1);
        }
    }

    internal void AddFocus(int amount)
    {
        var foc = GetStatusEffect(StatusEffectType.Focus);
        if (foc != null)
        {
            foc.Duration += amount;
            foc.Strength += amount;
        }
        else
        {
            ApplyStatusEffect(StatusEffectType.Focus, amount, amount);
        }
    }

    internal void RemoveFocus()
    {
        var foc = GetStatusEffect(StatusEffectType.Focus);
        if (foc != null)
        {
            foc.Duration -= 3;
            foc.Strength -= 3;
            if (foc.Duration == 0) StatusEffects.Remove(foc);
        }
    }

    internal void AddStagger()
    {
        var stag = GetStatusEffect(StatusEffectType.Staggering);
        if (stag != null)
        {
            stag.Duration++;
            stag.Strength++;
        }
        else
        {
            ApplyStatusEffect(StatusEffectType.SpellForce, 1, 1);
        }
    }

    internal void RemoveStagger()
    {
        var stag = GetStatusEffect(StatusEffectType.Staggering);
        if (stag != null)
        {
            stag.Duration--;
            stag.Strength--;
            if (stag.Duration == 0) StatusEffects.Remove(stag);
        }
    }

    internal StatusEffect GetLongestStatusEffect(StatusEffectType type)
    {
        return StatusEffects.Where(s => s.Type == type).OrderByDescending(s => s.Duration).FirstOrDefault();
    }

    internal void TickStatusEffects()
    {
        var effect = GetStatusEffect(StatusEffectType.Mending);
        if (effect != null) HealPercentageCap(.2f, (int)(2 + effect.Strength / 4));
        effect = GetStatusEffect(StatusEffectType.Poisoned);
        if (effect != null) NonFatalDamage((int)effect.Strength, "poison");
        effect = GetStatusEffect(StatusEffectType.Virus);
        if (effect != null) NonFatalDamage((int)effect.Strength, "virus");
        foreach (var eff in StatusEffects.ToList())
        {
            if (eff.Type == StatusEffectType.BladeDance || eff.Type == StatusEffectType.Tenacious || eff.Type == StatusEffectType.Focus) continue;
            var actor = TacticalUtilities.Units.Where(s => s.Unit == this).FirstOrDefault();
            var pred = actor.SelfPrey?.Predator;
            if (pred != null && eff.Type == StatusEffectType.Diminished)
            {
                if (pred.Unit.HasTrait(TraitType.TightNethers) && (actor.SelfPrey.Location == PreyLocation.Balls || actor.SelfPrey.Location == PreyLocation.Womb))
                {
                    continue;
                }
            }

            if (eff.Type == StatusEffectType.Staggering || eff.Type == StatusEffectType.SpellForce) StatusEffects.Remove(eff);
            eff.Duration -= 1;
            if (eff.Duration <= 0)
            {
                StatusEffects.Remove(eff);
                if (eff.Type == StatusEffectType.Diminished)
                {
                    var still = GetStatusEffect(StatusEffectType.Diminished);
                    if (still == null)
                    {
                        if (actor != null)
                        {
                            if (pred != null)
                            {
                                State.GameManager.TacticalMode.Log.RegisterDiminishmentExpiration(pred.Unit, this, actor.SelfPrey.Location);
                            }
                        }
                    }
                }

                if (eff.Type == StatusEffectType.WillingPrey)
                {
                    var still = GetStatusEffect(StatusEffectType.WillingPrey);
                    if (still == null)
                    {
                        if (actor != null)
                        {
                            if (pred != null)
                            {
                                State.GameManager.TacticalMode.Log.RegisterCurseExpiration(pred.Unit, this, actor.SelfPrey.Location);
                            }
                        }
                    }
                }
            }
        }
    }

    internal List<TraitType> RandomizeOne(RandomizeList randomizeList)
    {
        var chance = randomizeList.Chance;
        var traitsToAdd = new List<TraitType>();
        List<TraitType> gainable = randomizeList.RandomTraits.Where(rt => !Tags.Contains(rt) && !PermanentTraits.Contains(rt)).ToList();
        while (State.Rand.NextDouble() < chance)
        {
            if (gainable.Count() > 0)
            {
                var randomPick = gainable[State.Rand.Next(gainable.Count())];
                GivePrerequisiteTraits(randomPick);
                if (randomPick >= (TraitType)1000)
                {
                    RandomizeList recursiveRl = State.RandomizeLists.Find(re => (TraitType)re.ID == randomPick);
                    if (recursiveRl != null)
                    {
                        traitsToAdd.AddRange(RandomizeOne(recursiveRl));
                    }
                }
                else
                    traitsToAdd.Add(randomPick);

                gainable.Remove(randomPick);
            }

            chance -= 1;
        }

        return traitsToAdd;
    }

    internal void AcquireShape(Unit unit, bool forceDirect = false)
    {
        //if (ShifterShapes.Any(shape => shape.Race == unit.Race) && !forceDirect) return;
        //if (HasTrait(Traits.Skinwalker) || forceDirect)
        //{
        //    Unit referenceUnit = ShifterShapes.Count > 0 ? ShifterShapes[0] : this;
        //    Unit shape = unit.Clone();
        //    shape.Side = Side;
        //    shape._fixedSide = referenceUnit._fixedSide;
        //    if (referenceUnit.HasTrait(Traits.Skinwalker))
        //        shape.AddPermanentTrait(Traits.Skinwalker);
        //    shape.hiddenFixedSide = referenceUnit.hiddenFixedSide;
        //    shape.SavedCopy = referenceUnit.SavedCopy;
        //    shape.SavedVillage = referenceUnit.SavedVillage; 
        //    shape.BoundUnit = referenceUnit.BoundUnit;
        //    referenceUnit.ShifterShapes.Add(shape);
        //    shape.ShifterShapes = ShifterShapes[0].ShifterShapes;
        //}
        //else if (HasTrait(Traits.Shapeshifter))
        //{
        //    CreateRaceShape(unit.Race);
        //}
    }

    internal List<TraitType> GetPermanentTraits()
    {
        return PermanentTraits;
    }

    internal void UpdateShapeExpAndItems()
    {
        //if (!HasTrait(Traits.Skinwalker))
        //{
        //    ShifterShapes.ForEach(shape =>
        //    {
        //        shape.SetExp(experience);
        //        StrategicUtilities.SpendLevelUps(shape);
        //    });
        //}
        //ShifterShapes.ForEach(shape =>
        //{
        //    if (!shape.FixedGear)
        //    {
        //        shape.Items.ForEach((slot, index) =>
        //        {
        //           if( slot == null)
        //            {
        //                slot = GetItem(index);
        //            }
        //        });
        //    }
        //});
    }

    public void Shrink()
    {
        if (HasTrait(TraitType.Titanic) && HasTrait(TraitType.Colossal) && HasTrait(TraitType.Huge) && HasTrait(TraitType.Large))
            RemoveTrait(TraitType.Large);
        else if (HasTrait(TraitType.Titanic) && HasTrait(TraitType.Colossal) && HasTrait(TraitType.Huge))
            RemoveTrait(TraitType.Huge);
        else if (HasTrait(TraitType.Titanic) && HasTrait(TraitType.Colossal))
        {
            RemoveTrait(TraitType.Titanic);
            AddTrait(TraitType.Huge);
        }
        else if (HasTrait(TraitType.Colossal) && HasTrait(TraitType.Huge))
        {
            RemoveTrait(TraitType.Colossal);
            RemoveTrait(TraitType.Huge);
            AddTrait(TraitType.Titanic);
        }
        else if (HasTrait(TraitType.Titanic))
        {
            AddTrait(TraitType.Colossal);
            RemoveTrait(TraitType.Titanic);
        }
        else if (HasTrait(TraitType.Colossal))
        {
            RemoveTrait(TraitType.Colossal);
            AddTrait(TraitType.Huge);
        }
        else if (HasTrait(TraitType.Huge))
        {
            RemoveTrait(TraitType.Huge);
            AddTrait(TraitType.Large);
        }
        else if (HasTrait(TraitType.Large))
            RemoveTrait(TraitType.Large);
        else if (HasTrait(TraitType.Small))
        {
            RemoveTrait(TraitType.Small);
            AddTrait(TraitType.Tiny);
        }
        else
            AddTrait(TraitType.Small);
    }

    public Race DetermineSpawnRace()
    {
        if (!Equals(SpawnRace, Race))
        {
            return SpawnRace;
        }

        else
            return State.RaceSettings.GetSpawnRace(Race);
    }

    public Race DetermineConversionRace()
    {
        if (!Equals(ConversionRace, Race))
            return ConversionRace;
        else
            return State.RaceSettings.GetConversionRace(Race);
    }
}