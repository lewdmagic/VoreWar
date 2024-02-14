using OdinSerializer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Actor_Unit : IActorUnit
{
    private enum DisplayMode
    {
        None,
        Attacking,
        OralVore,
        CockVore,
        TailVore,
        BreastVore,
        AnalVore,
        Unbirth,
        FrogPouncing,
        VoreSuccess,
        VoreFail,
        Digesting,
        Absorbing,
        Birthing,
        Suckling,
        Rubbing,
        Suckled,
        Rubbed,
        Injured,
        IdleAnimation,
    }

    private int animationStep = 4;
    private float animationUpdateTime;
    private DisplayMode mode;

    [OdinSerialize]
    public List<KeyValuePair<int, float>> modeQueue;

    private DisplayMode Mode
    {
        get => mode;
        set
        {
            if (State.GameManager.TacticalMode.turboMode && value >= DisplayMode.Attacking && value <= DisplayMode.FrogPouncing) HasAttackedThisCombat = true;
            mode = value;
        }
    }


    [OdinSerialize]
    IUnitRead IActorUnit.Unit => Unit;

    public Unit Unit { get; private set; }

    [OdinSerialize]
    public Vec2i Position { get; private set; }

    [OdinSerialize]
    private int _movement;

    public int Movement { get => _movement; set => _movement = value; }

    [OdinSerialize]
    private bool _visible;

    public bool Visible { get => _visible; set => _visible = value; }

    [OdinSerialize]
    private bool _targetable;

    public bool Targetable { get => _targetable; set => _targetable = value; }

    [OdinSerialize]
    private bool _receivedRub;

    public bool ReceivedRub { get => _receivedRub; set => _receivedRub = value; }

    [OdinSerialize]
    private bool _surrendered;

    public bool Surrendered { get => _surrendered; set => _surrendered = value; }

    public bool DefectedThisTurn;

    [OdinSerialize]
    private bool _wasJustFreed;

    public bool WasJustFreed { get => _wasJustFreed; set => _wasJustFreed = value; }

    public bool SurrenderedThisTurn = false; //Deliberately not saved

    internal bool Intimidated;

    [OdinSerialize]
    private Weapon _bestMelee;

    public Weapon BestMelee { get => _bestMelee; set => _bestMelee = value; }

    [OdinSerialize]
    private Weapon _bestRanged;

    public Weapon BestRanged { get => _bestRanged; set => _bestRanged = value; }

    [OdinSerialize]
    private bool _fled;

    public bool Fled { get => _fled; set => _fled = value; }

    // DayNight addition
    [OdinSerialize]
    private bool _hidden;

    public bool Hidden { get => _hidden; set => _hidden = value; }

    [OdinSerialize]
    private bool _inSight;

    public bool InSight { get => _inSight; set => _inSight = value; }

    [OdinSerialize]
    private Prey _selfPrey;

    internal Prey SelfPrey { get => _selfPrey; set => _selfPrey = value; }

    [OdinSerialize]
    private bool _slimed;

    public bool Slimed { get => _slimed; set => _slimed = value; }

    [OdinSerialize]
    private bool _paralyzed;

    public bool Paralyzed { get => _paralyzed; set => _paralyzed = value; }

    [OdinSerialize]
    private int _corruption;

    public int Corruption { get => _corruption; set => _corruption = value; }

    [OdinSerialize]
    private int _possessed;

    public int Possessed { get => _possessed; set => _possessed = value; }

    [OdinSerialize]
    private bool _infected;

    public bool Infected { get => _infected; set => _infected = value; }

    [OdinSerialize]
    private Race _infectedRace;

    public Race InfectedRace { get => _infectedRace; set => _infectedRace = value; }

    [OdinSerialize]
    private Side _infectedSide;

    public Side InfectedSide { get => _infectedSide; set => _infectedSide = value; }

    [OdinSerialize]
    private bool _killedByDigestion;

    public bool KilledByDigestion { get => _killedByDigestion; set => _killedByDigestion = value; }

    [OdinSerialize]
    private int _timesParalyzed;

    public int TimesParalyzed { get => _timesParalyzed; set => _timesParalyzed = value; }

    [OdinSerialize]
    private bool _goneBerserk;

    public bool GoneBerserk { get => _goneBerserk; set => _goneBerserk = value; }

    [OdinSerialize]
    private int _aIAvoidEat;

    internal int AIAvoidEat { get => _aIAvoidEat; set => _aIAvoidEat = value; }

    [OdinSerialize]
    private int _turnUsedShun = -5;

    public int TurnUsedShun { get => _turnUsedShun; set => _turnUsedShun = value; }

    [OdinSerialize]
    private int _turnsSinceLastDamage = 9999;

    internal int TurnsSinceLastDamage { get => _turnsSinceLastDamage; set => _turnsSinceLastDamage = value; }

    [OdinSerialize]
    private int _turnsSinceLastParalysis = 9999;

    internal int TurnsSinceLastParalysis { get => _turnsSinceLastParalysis; set => _turnsSinceLastParalysis = value; }

    [OdinSerialize]
    private int _spriteLayerOffset = 0;

    internal int spriteLayerOffset { get => _spriteLayerOffset; set => _spriteLayerOffset = value; }

    [OdinSerialize]
    private bool _hasAttackedThisCombat = false;

    public bool HasAttackedThisCombat { get => _hasAttackedThisCombat; set => _hasAttackedThisCombat = value; }

    [OdinSerialize]
    private bool _allowedToDefect = false;

    public bool allowedToDefect { get => _allowedToDefect; set => _allowedToDefect = value; }


    private UnitSprite _unitSprite;

    internal UnitSprite UnitSprite
    {
        get
        {
            if (_unitSprite == null) GenerateSpritePrefab(State.GameManager.TacticalMode.ActorFolder);
            return _unitSprite;
        }
        set => _unitSprite = value;
    }

    public bool SquishedBreasts { get; set; } = false;

    [OdinSerialize]
    private PredatorComponent _predatorComponent;

    public PredatorComponent PredatorComponent { get => _predatorComponent; set => _predatorComponent = value; }

    private Race animationControllerRace;

    [OdinSerialize]
    private AnimationController _animationController = new AnimationController();

    private AnimationController animationController { get => _animationController; set => _animationController = value; }

    public AnimationController AnimationController
    {
        get
        {
            if (animationController == null || !Equals(animationControllerRace, Unit.Race))
            {
                animationController = new AnimationController();
                animationControllerRace = Unit.Race;
            }

            return animationController;
        }
        set => animationController = value;
    }

    public void ClearMovement() => Movement = 0;
    public void DrainExp(int damage) => Unit.GiveExp(-1 * damage);
    public void SetPos(Vec2i p) => Position = p;

    private void RestoreMP()
    {
        if (WasJustFreed)
        {
            Movement = Mathf.Min(Mathf.Max(2, (int)(.2f * CurrentMaxMovement())), 5);
            WasJustFreed = false;
        }

        if (Paralyzed)
        {
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{Unit.Name}</b> was paralyzed and cannot move this turn.");
            Movement = 0;
            TimesParalyzed++;
            TurnsSinceLastParalysis = 0;
            Paralyzed = false;
            Slimed = false;
        }
        else if (Unit.GetStatusEffect(StatusEffectType.Petrify) != null)
        {
            Movement = 0;
            Slimed = false;
        }
        else if (Unit.GetStatusEffect(StatusEffectType.Glued) != null)
        {
            Movement = 2;
            Slimed = false;
        }
        else if (Unit.GetStatusEffect(StatusEffectType.Webbed) != null)
        {
            Movement = 1;
            Slimed = false;
        }
        else if (Unit.GetStatusEffect(StatusEffectType.Staggering) != null)
        {
            Movement = CurrentMaxMovement() / 2;
            Slimed = false;
        }
        else if (Unit.GetStatusEffect(StatusEffectType.Sleeping) != null)
        {
            Movement = 0;
            Slimed = false;
        }
        else if (Slimed)
        {
            Movement = CurrentMaxMovement() / 2;
            Slimed = false;
        }
        else
            Movement = CurrentMaxMovement();
    }


    public int MaxMovement()
    {
        int bonus = 0;
        if (Unit.HasTrait(TraitType.Charge) && State.GameManager.TacticalMode.currentTurn <= 2) bonus += 4;
        bonus += Unit.TraitBoosts.SpeedBonus;
        if (State.World?.ItemRepository != null && Unit.Items.Contains(State.World.ItemRepository.GetItem(ItemType.Shoes))) bonus += 1;
        int total = Mathf.Max(bonus + 3 + (int)Mathf.Pow(Unit.GetStat(Stat.Agility) / 4, .8f), 1);
        var speed = Unit.GetStatusEffect(StatusEffectType.Fast);
        if (speed != null)
        {
            total = (int)(total * (1 + speed.Strength));
        }

        total = (int)(total * Unit.TraitBoosts.SpeedMultiplier);
        if (total < Unit.TraitBoosts.MinSpeed) total = Unit.TraitBoosts.MinSpeed;
        return total;
    }

    public int CurrentMaxMovement()
    {
        int sizePenalty = (int)(PredatorComponent?.Fullness ?? 0);
        sizePenalty = (int)(sizePenalty * Unit.TraitBoosts.SpeedLossFromWeightMultiplier);
        int bonus = 0;
        if (State.World?.ItemRepository != null && Unit.Items.Contains(State.World.ItemRepository.GetItem(ItemType.Shoes))) bonus += 1;
        bonus += Unit.TraitBoosts.SpeedBonus;
        if (Unit.HasTrait(TraitType.Charge) && State.GameManager.TacticalMode.currentTurn <= 2) bonus += 4;
        int total = Mathf.Max(bonus + 3 + (int)Mathf.Pow(Unit.GetStat(Stat.Agility) / 4, .8f) - sizePenalty, 1);
        var speed = Unit.GetStatusEffect(StatusEffectType.Fast);
        if (speed != null)
        {
            total = (int)(total * (1 + speed.Strength));
        }

        total = (int)(total * Unit.TraitBoosts.SpeedMultiplier);
        if (Unit.HasTrait(TraitType.AllOutFirstStrike) && HasAttackedThisCombat) total /= 2;
        if (total < Unit.TraitBoosts.MinSpeed) total = Unit.TraitBoosts.MinSpeed;
        return total;
    }

    /// <summary>
    ///     This one is a dummy for some cases
    /// </summary>
    /// <param name="unit"></param>
    public Actor_Unit(Unit unit)
    {
        unit.SetBreastSize(-1); //Resets to default
        Mode = DisplayMode.None;
        modeQueue = new List<KeyValuePair<int, float>>();
        animationUpdateTime = 0;
        Position = new Vec2i(0, 0);
        Unit = unit;
        Visible = true;
        Targetable = true;
    }

    public Actor_Unit(Unit unit, Actor_Unit reciepient)
    {
        PredatorComponent = new PredatorComponent(this, unit);
        unit.SetBreastSize(-1); //Resets to default
        Mode = DisplayMode.None;
        modeQueue = new List<KeyValuePair<int, float>>();
        animationUpdateTime = 0;
        Position = reciepient.Position;
        Unit = unit;
        Visible = false;
        Targetable = false;
        RestoreMP();
        ReloadSpellTraits();
    }

    public Actor_Unit(Vec2i p, Unit unit)
    {
        PredatorComponent = new PredatorComponent(this, unit);
        unit.SetBreastSize(-1); //Resets to default
        Mode = DisplayMode.None;
        modeQueue = new List<KeyValuePair<int, float>>();
        animationUpdateTime = 0;
        Position = p;
        Unit = unit;
        Visible = true;
        Targetable = true;
        RestoreMP();
        ReloadSpellTraits();
    }

    public void ReloadSpellTraits()
    {
        Unit.SingleUseSpells = new List<SpellType>();
        Unit.MultiUseSpells = new List<SpellType>();
        if (Unit.HasTrait(TraitType.MadScience) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.SingleUseSpells.Add(((SpellBook)State.World.ItemRepository.GetRandomBook(1, 4)).ContainedSpell);
            Unit.UpdateSpells();
        }

        if (Unit.HasTrait(TraitType.PollenProjector) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.SingleUseSpells.Add(SpellList.AlraunePuff.SpellType);
            Unit.UpdateSpells();
        }

        if (Unit.HasTrait(TraitType.Webber) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.SingleUseSpells.Add(SpellList.Web.SpellType);
            Unit.UpdateSpells();
        }

        if (Unit.HasTrait(TraitType.GlueBomb) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.SingleUseSpells.Add(SpellList.GlueBomb.SpellType);
            Unit.UpdateSpells();
        }

        if (Unit.HasTrait(TraitType.PoisonSpit) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.SingleUseSpells.Add(SpellList.ViperPoisonStatus.SpellType);
            Unit.UpdateSpells();
        }

        if (Unit.HasTrait(TraitType.Petrifier) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.SingleUseSpells.Add(SpellList.Petrify.SpellType);
            Unit.UpdateSpells();
        }

        if (Unit.HasTrait(TraitType.Charmer) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.SingleUseSpells.Add(SpellList.Charm.SpellType);
            Unit.UpdateSpells();
        }

        if (Unit.HasTrait(TraitType.HypnoticGas) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.SingleUseSpells.Add(SpellList.HypnoGas.SpellType);
            Unit.UpdateSpells();
        }

        if (Unit.HasTrait(TraitType.Reanimator) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.SingleUseSpells.Add(SpellList.Reanimate.SpellType);
            Unit.UpdateSpells();
        }

        if (Unit.HasTrait(TraitType.Binder) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.SingleUseSpells.Add(SpellList.Bind.SpellType);
            Unit.UpdateSpells();
        }

        // Multi-use section
        if (Unit.HasTrait(TraitType.ForceFeeder) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.MultiUseSpells.Add(SpellList.ForceFeed.SpellType);
            Unit.UpdateSpells();
        }

        if (State.World?.ItemRepository != null) //protection for the create strat screen
        {
            foreach (TraitType trait in Unit.GetTraits.Where(t => TraitList.GetTrait(t) != null && TraitList.GetTrait(t) is IProvidesSingleSpell))
            {
                IProvidesSingleSpell callback = (IProvidesSingleSpell)TraitList.GetTrait(trait);
                foreach (SpellType spell in callback.GetSingleSpells(Unit)) Unit.SingleUseSpells.Add(spell);
            }

            foreach (TraitType trait in Unit.GetTraits.Where(t => TraitList.GetTrait(t) != null && TraitList.GetTrait(t) is IProvidesMultiSpell))
            {
                IProvidesMultiSpell callback = (IProvidesMultiSpell)TraitList.GetTrait(trait);
                foreach (SpellType spell in callback.GetMultiSpells(Unit)) Unit.MultiUseSpells.Add(spell);
            }

            Unit.UpdateSpells();
        }
    }

    public void GenerateSpritePrefab(Transform folder)
    {
        UnitSprite = Object.Instantiate(State.GameManager.UnitBase, new Vector3(Position.X, Position.Y), new Quaternion(), folder).GetComponent<UnitSprite>();
        UnitSprite.UpdateHealthBar(this);
    }

    public void UpdateBestWeapons()
    {
        if (Unit.HasTrait(TraitType.Feral))
        {
            BestMelee = State.World.ItemRepository.Claws;
            BestRanged = null;
            return;
        }


        BestMelee = Unit.GetBestMelee();
        BestRanged = Unit.GetBestRanged();
        Unit.UpdateSpells();
    }

    public void SetPredMode(PreyLocation preyType)
    {
        switch (preyType)
        {
            case PreyLocation.breasts:
                Mode = DisplayMode.BreastVore;
                break;
            case PreyLocation.balls:
                Mode = DisplayMode.CockVore;
                break;
            case PreyLocation.stomach:
                Mode = DisplayMode.OralVore;
                break;
            case PreyLocation.womb:
                Mode = DisplayMode.Unbirth;
                break;
            case PreyLocation.tail:
                Mode = DisplayMode.TailVore;
                break;
            case PreyLocation.anal:
                Mode = DisplayMode.AnalVore;
                break;
        }

        animationUpdateTime = 1.5F;
    }

    public void SetBurpMode()
    {
        Mode = DisplayMode.OralVore;
        animationUpdateTime = 1;
    }

    /// <summary>
    ///     Used for idle Animations - ignored if the unit is already animating something
    /// </summary>
    /// <param name="frameNum">The Number of Frames of animation, starts at this and counts down to 0</param>
    /// <param name="time">The seconds per step</param>
    public void SetAnimationMode(int frameNum, float time)
    {
        if (Mode == DisplayMode.None)
        {
            Mode = DisplayMode.IdleAnimation;
            animationStep = frameNum;
            animationUpdateTime = time;
        }
    }

    public void SetVoreSuccessMode()
    {
        DisplayMode displayMode = DisplayMode.VoreSuccess;
        float time = 1f;
        modeQueue.Add(new KeyValuePair<int, float>((int)displayMode, time));
    }

    public void SetVoreFailMode()
    {
        DisplayMode displayMode = DisplayMode.VoreFail;
        float time = 1f;
        modeQueue.Add(new KeyValuePair<int, float>((int)displayMode, time));
    }

    public void SetAbsorbtionMode()
    {
        Mode = DisplayMode.Absorbing;
        animationUpdateTime = 2f;
    }

    public void SetDigestionMode()
    {
        Mode = DisplayMode.Digesting;
        animationUpdateTime = 1.0f;
    }

    public void SetBirthMode()
    {
        Mode = DisplayMode.Birthing;
        animationUpdateTime = 1.0f;
    }

    public void SetSuckleMode()
    {
        Mode = DisplayMode.Suckling;
        animationUpdateTime = 1.0f;
    }

    public void SetSuckledMode()
    {
        Mode = DisplayMode.Suckled;
        animationUpdateTime = 1.0f;
    }

    public void SetRubMode()
    {
        Mode = DisplayMode.Rubbing;
        animationUpdateTime = 1.0f;
    }

    public void SetRubbedMode()
    {
        Mode = DisplayMode.Rubbed;
        animationUpdateTime = 1.0f;
    }

    public int CheckAnimationFrame()
    {
        if (Mode == DisplayMode.IdleAnimation) return animationStep;
        return 0;
    }

    public bool DamagedColors => Mode == DisplayMode.Injured;

    /// <summary>
    ///     Splits the chain of sprites evenly based on the ball size (Oral + Unbirth)
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetBallSize(int highestSprite, float multiplier = 1)
    {
        float v = (PredatorComponent?.BallsFullness ?? 0) * 4 * ((highestSprite + 1) / 16f) * multiplier;
        if (v > highestSprite) v = highestSprite;
        return (int)v;
    }

    /// <summary>
    ///     Splits the chain of sprites evenly based on the tail size (Oral + Unbirth)
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetTailSize(int highestSprite, float multiplier = 1)
    {
        float v = (PredatorComponent?.TailFullness ?? 0) * 4 * ((highestSprite + 1) / 16f) * multiplier;
        if (v > highestSprite) v = highestSprite;
        return (int)v;
    }


    /// <summary>
    ///     Splits the chain of sprites evenly based on the stomach size (Oral + Unbirth)
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetStomachSize(int highestSprite = 15, float multiplier = 1)
    {
        float v = (PredatorComponent?.VisibleFullness ?? 0) * 4 * ((highestSprite + 1) / 16f) * multiplier;
        if (v > highestSprite) v = highestSprite;
        return (int)v;
    }

    /// <summary>
    ///     Splits the chain of sprites evenly based on the stomach size (excludes womb)
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetExclusiveStomachSize(int highestSprite = 15, float multiplier = 1)
    {
        float v = (PredatorComponent?.ExclusiveStomachFullness ?? 0) * 4 * ((highestSprite + 1) / 16f) * multiplier;
        if (v > highestSprite) v = highestSprite;
        return (int)v;
    }

    /// <summary>
    ///     Splits the chain of sprites evenly based on the stomach size (excludes womb)
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetWombSize(int highestSprite = 15, float multiplier = 1)
    {
        float v = (PredatorComponent?.WombFullness ?? 0) * 4 * ((highestSprite + 1) / 16f) * multiplier;
        if (v > highestSprite) v = highestSprite;
        return (int)v;
    }

    /// <summary>
    ///     Splits the chain of sprites unevenly based on the stomach size (Oral + Unbirth)
    ///     This one causes the low sprites to be passed through quicker, and the high sprites to be passed through slower.
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetRootedStomachSize(int highestSprite = 15, float multiplier = 1)
    {
        highestSprite = highestSprite * highestSprite;
        float v = (PredatorComponent?.VisibleFullness ?? 0) * 4 * ((highestSprite + 1) / 16f) * multiplier;
        if (v > highestSprite) v = highestSprite;
        v = Mathf.Sqrt(v);
        return (int)v;
    }

    /// <summary>
    ///     Splits the chain of sprites evenly based on the left breast size
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetLeftBreastSize(int highestSprite = 15, float multiplier = 1)
    {
        float v = (PredatorComponent?.LeftBreastFullness ?? 0) * 4 * ((highestSprite + 1) / 16f) * multiplier;
        if (v > highestSprite) v = highestSprite;
        return (int)v;
    }

    /// <summary>
    ///     Splits the chain of sprites evenly based on the right breast size
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetRightBreastSize(int highestSprite = 15, float multiplier = 1)
    {
        float v = (PredatorComponent?.RightBreastFullness ?? 0) * 4 * ((highestSprite + 1) / 16f) * multiplier;
        if (v > highestSprite) v = highestSprite;
        return (int)v;
    }

    /// <summary>
    ///     Splits the chain of sprites evenly based on the second stomach's size.
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetStomach2Size(int highestSprite = 15, float multiplier = 1)
    {
        float v = (PredatorComponent?.Stomach2ndFullness ?? 0) * 4 * ((highestSprite + 1) / 16f) * multiplier;
        if (v > highestSprite) v = highestSprite;
        return (int)v;
    }

    /// <summary>
    ///     Splits the chain of sprites evenly based on the combined size of bellies 1 & 2. For units with DualStomach trait.
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetCombinedStomachSize(int highestSprite = 15, float multiplier = 1)
    {
        float v = (PredatorComponent?.CombinedStomachFullness ?? 0) * 4 * ((highestSprite + 1) / 16f) * multiplier;
        if (v > highestSprite) v = highestSprite;
        return (int)v;
    }

    /// <summary>
    ///     An alternate version of GetStomachSize used for combining all vore types into a single value
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4</param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetUniversalSize(int highestSprite = 15, float multiplier = 1)
    {
        float v = (PredatorComponent?.Fullness ?? 0) * 4 * multiplier;
        if (v > highestSprite) v = highestSprite;
        return (int)v;
    }

    public bool HasBelly => ((Config.LamiaUseTailAsSecondBelly || Unit.HasTrait(TraitType.DualStomach) == false) && (PredatorComponent?.VisibleFullness ?? 0) > 0) || (!Config.LamiaUseTailAsSecondBelly && (PredatorComponent?.CombinedStomachFullness ?? 0) > 0);

    public bool HasPreyInBreasts => PredatorComponent?.BreastFullness > 0 || PredatorComponent?.LeftBreastFullness > 0 || PredatorComponent?.RightBreastFullness > 0;
    public bool HasBodyWeight => !Equals(Unit.Race, Race.Lizard) && !Equals(Unit.Race, Race.Slime) && !Equals(Unit.Race, Race.Scylla) && !Equals(Unit.Race, Race.Harpy) && !Equals(Unit.Race, Race.Imp);

    public int GetBodyWeight() => Config.WeightGain || Unit.BodySizeManuallyChanged ? Unit.BodySize : Mathf.Min(Config.DefaultStartingWeight, Races2.GetRace(Unit).SetupOutput.BodySizes);

    /// <summary>
    ///     0: Melee 1 hold
    ///     1: Melee 1 attack
    ///     2: Melee 2 hold
    ///     3: Melee 2 attack
    ///     4: Ranged 1 hold
    ///     5: Ranged 1 attack
    ///     6: Ranged 2 hold
    ///     7: Ranged 2 attack
    /// </summary>
    /// <returns></returns>
    public int GetWeaponSprite()
    {
        if (BestRanged != null)
        {
            if (Mode != DisplayMode.Attacking)
                return BestRanged.Graphic;
            else
                return BestRanged.Graphic + 1;
        }
        else if (BestMelee != null && BestMelee != State.World.ItemRepository.Claws)
        {
            if (Mode != DisplayMode.Attacking)
                return BestMelee.Graphic;
            else
                return BestMelee.Graphic + 1;
        }

        return 0;
    }

    public int GetSimpleBodySprite()
    {
        if (Mode == DisplayMode.Attacking) return 1;
        if (Mode == DisplayMode.OralVore || Mode == DisplayMode.BreastVore || Mode == DisplayMode.CockVore || Mode == DisplayMode.Unbirth || Mode == DisplayMode.AnalVore) return 2;
        return 0;
    }


    public bool IsAttacking => Mode == DisplayMode.Attacking;

    /// <summary>
    ///     This one Covers all forms of consuming
    /// </summary>
    public bool IsEating => IsOralVoring || IsCockVoring || IsBreastVoring || IsUnbirthing || IsTailVoring || IsAnalVoring;

    public bool IsOralVoring => Mode == DisplayMode.OralVore;
    public bool IsOralVoringHalfOver => Mode == DisplayMode.OralVore && animationUpdateTime < .75f;

    public bool IsCockVoring => Mode == DisplayMode.CockVore;
    public bool IsBreastVoring => Mode == DisplayMode.BreastVore;
    public bool IsUnbirthing => Mode == DisplayMode.Unbirth;
    public bool IsTailVoring => Mode == DisplayMode.TailVore;
    public bool IsAnalVoring => Mode == DisplayMode.AnalVore;
    public bool IsPouncingFrog => Mode == DisplayMode.FrogPouncing;
    public bool HasJustVored => Mode == DisplayMode.VoreSuccess;
    public bool HasJustFailedToVore => Mode == DisplayMode.VoreFail;
    public bool IsDigesting => Mode == DisplayMode.Digesting;
    public bool IsAbsorbing => Mode == DisplayMode.Absorbing;
    public bool IsBirthing => Mode == DisplayMode.Birthing;
    public bool IsSuckling => Mode == DisplayMode.Suckling;
    public bool IsBeingSuckled => Mode == DisplayMode.Suckled;
    public bool IsRubbing => Mode == DisplayMode.Rubbing;
    public bool IsBeingRubbed => Mode == DisplayMode.Rubbed;

    [OdinSerialize]
    private List<Side> _sidesAttackedThisBattle;

    public List<Side> SidesAttackedThisBattle { get => _sidesAttackedThisBattle; set => _sidesAttackedThisBattle = value; }

    public float GetSpecialChance(SpecialAction action)
    {
        if (action == SpecialAction.CockVore)
            return .8f;
        else
            return 1f;
    }

    public float GetPureStatClashChance(int attackStat, int defenseStat, float shift) // generic AF
    {
        return (float)attackStat / (attackStat + defenseStat * (1 + shift));
    }

    internal float GetMagicChance(Actor_Unit attacker, Spell currentSpell, float modifier = 0, Stat stat = Stat.Mind)
    {
        if (TacticalUtilities.SneakAttackCheck(attacker.Unit, Unit)) // sneakAttack
        {
            modifier -= 0.3f;
        }

        int attackStat = attacker.Unit.GetStat(stat);
        int defenseStat = Unit.GetStat(Stat.Will);
        if (currentSpell?.Resistable ?? false) //Skips if no spell is specified since that only involves AI calcs
        {
            defenseStat = (int)(defenseStat * currentSpell.ResistanceMult);
        }

        float shift = Unit.TraitBoosts.Incoming.MagicShift + attacker.Unit.TraitBoosts.Outgoing.MagicShift + modifier;
        return (float)attackStat / (attackStat + defenseStat * (1 + shift));
    }

    //public float GetMeleeChance(Actor_Unit attacker, bool includeSecondaries = false)
    //{
    //    float attackerScore = 2 * attacker.Unit.GetStat(Stat.Strength) + attacker.Unit.GetStat(Stat.Dexterity);
    //    float defenderScore = 2 * Unit.GetStat(Stat.Agility) * (15 / (BodySize() + 5) / (1 + (PredatorComponent?.Fullness ?? 0) * Unit.TraitBoosts.DodgeLossFromWeightMultiplier * 5 / Unit.GetStat(Stat.Strength)));

    //    if (Unit.GetStatusEffect(StatusEffectType.Clumsiness) != null)
    //    {
    //        defenderScore *= Unit.GetStatusEffect(StatusEffectType.Clumsiness).Strength;
    //    }

    //    if (attacker.Intimidated)
    //        attackerScore *= .8f;

    //    foreach (IPhysicalDefenseOdds trait in Unit.PhysicalDefenseOdds)
    //    {
    //        trait.PhysicalDefense(this, ref defenderScore);
    //    }

    //    float odds = attackerScore / (attackerScore + defenderScore);

    //    odds *= Unit.TraitBoosts.FlatHitReduction;

    //    if (odds < 20)
    //        odds = 20;

    //    if (Config.BoostedAccuracy)
    //        odds = 100 - ((100 - odds) * .5f);

    //    if (includeSecondaries)
    //    {
    //        if (Unit.HasTrait(Traits.Dazzle))
    //        {
    //            odds *= 1 - WillCheckOdds(attacker, this);
    //        }
    //    }
    //    return odds / 100;
    //}


    public float GetAttackChance(Actor_Unit attacker, bool ranged, bool includeSecondaries = false, float mod = 0)
    {
        int attack;
        if (ranged)
        {
            attack = attacker.Unit.GetStat(Stat.Dexterity);
            attacker.UpdateBestWeapons();
        }
        else
        {
            attack = attacker.Unit.GetStat(Stat.Strength);
            attacker.UpdateBestWeapons();
        }

        int range = attacker.Position.GetNumberOfMovesDistance(Position);
        if (Surrendered || Unit.GetStatusEffect(StatusEffectType.Petrify) != null || Unit.GetStatusEffect(StatusEffectType.Sleeping) != null) return 1f;
        const int maximumBoost = 75;
        const int minimumOdds = 25;
        float defenderBonusShift = 0;
        int defense = Unit.GetStat(Stat.Agility);

        if (Unit.GetStatusEffect(StatusEffectType.Clumsiness) != null)
        {
            defenderBonusShift -= Unit.GetStatusEffect(StatusEffectType.Clumsiness).Strength;
        }

        attack += 15;
        defense += 15;

        if (attack <= 0) attack = 1;
        if (defense <= 0) defense = 1;

        if (range > 2) defenderBonusShift += .05f * (range - 2);

        if (ranged)
            defenderBonusShift += Unit.TraitBoosts.Incoming.RangedShift + attacker.Unit.TraitBoosts.Outgoing.RangedShift + mod;
        else
            defenderBonusShift += Unit.TraitBoosts.Incoming.MeleeShift + attacker.Unit.TraitBoosts.Outgoing.MeleeShift + mod;

        if (Unit.HasTrait(TraitType.AllOutFirstStrike))
        {
            if (HasAttackedThisCombat)
                defenderBonusShift -= 2;
            else
                defenderBonusShift += 4;
        }

        if (TacticalUtilities.SneakAttackCheck(attacker.Unit, Unit)) // sneakAttack
        {
            defenderBonusShift += 2;
        }

        defenderBonusShift -= Unit.TraitBoosts.DodgeLossFromWeightMultiplier * 0.1f * PredatorComponent?.Fullness ?? 0;


        if (BestRanged == null && Movement > 0 && ranged) defenderBonusShift += .2f;

        foreach (IPhysicalDefenseOdds trait in Unit.PhysicalDefenseOdds)
        {
            trait.PhysicalDefense(this, ref defenderBonusShift);
        }

        if (attacker.Intimidated) defenderBonusShift += .2f;

        if (ranged)
        {
            if (attacker.BestRanged?.AccuracyModifier > 1) attack = (int)(attack * attacker.BestRanged.AccuracyModifier);
            if (attacker.BestRanged?.Magic == true) attack = attack + attacker.Unit.GetStat(Stat.Mind);
        }
        else
        {
            if (attacker.BestMelee?.AccuracyModifier > 1) attack = (int)(attack * attacker.BestMelee.AccuracyModifier);
            if (attacker.BestMelee?.Magic == true) attack = attack + attacker.Unit.GetStat(Stat.Mind);
        }

        float ratio = (float)attack / defense;
        float adjustment;
        if (ratio > 1)
            adjustment = 1 - ratio;
        else
            adjustment = 1 / ratio - 1;

        float oddsReductionFactor = 3 * adjustment + defenderBonusShift; //Lower factor is increased odds
        float odds = minimumOdds + maximumBoost / (1 + Mathf.Pow(2, oddsReductionFactor));

        odds *= Unit.TraitBoosts.FlatHitReduction;

        if (Config.BoostedAccuracy) odds = 100 - (100 - odds) * .5f;

        if (includeSecondaries)
        {
            if (Unit.HasTrait(TraitType.Dazzle))
            {
                odds *= 1 - WillCheckOdds(attacker, this);
            }
        }

        return odds / 100;
    }

    public int WeaponDamageAgainstTarget(Actor_Unit target, bool ranged, float multiplier = 1, bool forceBite = false)
    {
        int damage;
        if (ranged)
        {
            float damageScalar = Unit.TraitBoosts.Outgoing.RangedDamage * target.Unit.TraitBoosts.Incoming.RangedDamage;
            damageScalar *= multiplier;
            if (Unit.HasTrait(TraitType.AllOutFirstStrike) && HasAttackedThisCombat == false) damageScalar *= 5;
            if (target.Unit.GetStatusEffect(StatusEffectType.Petrify) != null) damageScalar /= 2;
            if (Unit.GetStatusEffect(StatusEffectType.Valor) != null)
            {
                damageScalar *= 1.25f;
            }

            if (target.Unit.GetStatusEffect(StatusEffectType.Shielded) != null)
            {
                damageScalar *= 1 - target.Unit.GetStatusEffect(StatusEffectType.Shielded).Strength;
            }

            if (target.Unit.GetStatusEffect(StatusEffectType.DivineShield) != null)
            {
                damageScalar *= 1 - target.Unit.GetStatusEffect(StatusEffectType.DivineShield).Strength;
            }

            if (Unit.HasTrait(TraitType.SenseWeakness))
            {
                damageScalar *= 1.4f - target.Unit.HealthPct * .4f + 0.1f * target.Unit.GetNegativeStatusEffects();
            }

            int statBoost = Unit.GetStat(Stat.Dexterity) + (Unit.HasTrait(TraitType.SpellBlade) ? Unit.GetStat(Stat.Mind) / 2 : 0);
            damage = (int)(damageScalar * (BestRanged?.Damage ?? 2) * (60 + statBoost) / 60);
            if (target.Unit.HasTrait(TraitType.Resilient)) damage--;
            if (target.Unit.GetStatusEffect(StatusEffectType.Staggering) != null)
            {
                damage += (int)(damage * 0.2f);
            }
        }
        else
        {
            float damageScalar = Unit.TraitBoosts.Outgoing.MeleeDamage * target.Unit.TraitBoosts.Incoming.MeleeDamage;

            if (Unit.HasTrait(TraitType.AllOutFirstStrike) && HasAttackedThisCombat == false) damageScalar *= 5;
            damageScalar *= multiplier;

            if (target.Unit.GetStatusEffect(StatusEffectType.Petrify) != null) damageScalar /= 2;

            if (Unit.GetStatusEffect(StatusEffectType.Valor) != null)
            {
                damageScalar *= 1.25f;
            }

            if (target.Unit.GetStatusEffect(StatusEffectType.Shielded) != null)
            {
                damageScalar *= 1 - target.Unit.GetStatusEffect(StatusEffectType.Shielded).Strength;
            }

            if (target.Unit.GetStatusEffect(StatusEffectType.DivineShield) != null)
            {
                damageScalar *= 1 - target.Unit.GetStatusEffect(StatusEffectType.DivineShield).Strength;
            }

            if (Unit.HasTrait(TraitType.ForcefulBlow)) TacticalUtilities.CheckKnockBack(this, target, ref damageScalar);
            if (Unit.HasTrait(TraitType.SenseWeakness))
            {
                damageScalar *= 1.4f - target.Unit.HealthPct * .4f + 0.1f * target.Unit.GetNegativeStatusEffects();
            }

            if (Unit.HasTrait(TraitType.VenomShock) && target.Unit.GetStatusEffect(StatusEffectType.Poisoned) != null)
            {
                damageScalar *= 1.5f;
            }

            if (forceBite)
            {
                damage = (int)(damageScalar * State.World.ItemRepository.Bite.Damage * (60 + Unit.GetStat(Stat.Strength)) / 60);
            }
            else
            {
                if (Unit.HasTrait(TraitType.Feral) && Unit.GetBestMelee() == State.World.ItemRepository.Claws) damageScalar *= 3f;
                int statBoost = Unit.GetStat(Stat.Strength) + (Unit.HasTrait(TraitType.SpellBlade) ? Unit.GetStat(Stat.Mind) / 2 : 0);
                damage = (int)(damageScalar * BestMelee.Damage * (60 + statBoost) / 60);
            }


            if (target.Unit.HasTrait(TraitType.Resilient)) damage--;
            if (target.Unit.GetStatusEffect(StatusEffectType.Staggering) != null)
            {
                damage += (int)(damage * 0.2f);
            }
        }

        if (TacticalUtilities.SneakAttackCheck(Unit, target.Unit)) // sneakAttack
        {
            damage *= 3;
        }

        if (damage < 1) damage = 1;
        return damage;
    }

    ///// <summary>
    ///// Tells if the unit is able to move at all.   Attackers should use TacticalUtilities.FreeSpaceAroundTaget
    ///// </summary>
    //public bool Surrounded()
    //{
    //    Vec2i p = new Vec2i(0, 0);
    //    for (int i = 0; i < 8; i++)
    //    {
    //        p = GetPos(i);
    //        if (TacticalUtilities.OpenTile(p.x, p.y, this))
    //        {
    //            return false;

    //        }
    //    }
    //    return true;
    //}


    public Vec2i PounceAt(Actor_Unit target)
    {
        if (Movement < 2) return null;
        if (Position.GetNumberOfMovesDistance(target.Position) < 2 || Position.GetNumberOfMovesDistance(target.Position) > 4) return null;
        for (int i = 0; i < 8; i++)
        {
            Vec2i p = target.GetPos(i);
            if (TacticalUtilities.OpenTile(p.X, p.Y, this))
            {
                return p;
            }
        }

        return null;
    }

    public bool MeleePounce(Actor_Unit target)
    {
        if (Movement < 2 || Unit.HasTrait(TraitType.Pounce) == false) return false;
        var landingZone = PounceAt(target);
        if (landingZone != null)
        {
            State.GameManager.TacticalMode.Translator.SetTranslator(UnitSprite.transform, Position, landingZone, 0.5f, State.GameManager.TacticalMode.IsPlayerTurn);
            State.GameManager.TacticalMode.DirtyPack = true;
            SetPos(landingZone);
            float multiplier = 1;
            if (Unit.HasTrait(TraitType.HeavyPounce))
            {
                multiplier = Mathf.Min(1 + (PredatorComponent?.Fullness ?? 0) / 4, 2);
                Unit.ApplyStatusEffect(StatusEffectType.Clumsiness, Mathf.Min((PredatorComponent?.Fullness ?? 0) / 10, .8f), 1);
            }

            Attack(target, false, damageMultiplier: multiplier);
            if (Equals(Unit.Race, Race.FeralFrog)) Mode = DisplayMode.FrogPouncing;
            return true;
        }

        return false;
    }

    public bool VorePounce(Actor_Unit target, SpecialAction voreType = SpecialAction.None, bool AIAutoPick = false)
    {
        if (Movement < 2 || Unit.HasTrait(TraitType.Pounce) == false) return false;
        if (TacticalUtilities.AppropriateVoreTarget(this, target) == false) return false;
        if (PredatorComponent.FreeCap() < target.Bulk()) return false;
        var pounceLandingZone = PounceAt(target);
        if (pounceLandingZone != null)
        {
            Vec2i originalLoc = Position;
            SetPos(pounceLandingZone);
            if (AIAutoPick)
            {
                PredatorComponent.UsePreferredVore(target);
            }
            else
            {
                switch (voreType)
                {
                    case SpecialAction.BreastVore:
                        PredatorComponent.BreastVore(target);
                        break;
                    case SpecialAction.CockVore:
                        PredatorComponent.CockVore(target);
                        break;
                    case SpecialAction.Unbirth:
                        PredatorComponent.Unbirth(target);
                        break;
                    case SpecialAction.TailVore:
                        PredatorComponent.TailVore(target);
                        break;
                    case SpecialAction.AnalVore:
                        PredatorComponent.AnalVore(target);
                        break;
                    default:
                        PredatorComponent.Devour(target);
                        break;
                }
            }

            if (target.Visible == false)
            {
                //Done this way to avoid doubling up references                        
                pounceLandingZone.X = target.Position.X;
                pounceLandingZone.Y = target.Position.Y;
                SetPos(pounceLandingZone);
            }

            State.GameManager.TacticalMode.Translator.SetTranslator(UnitSprite.transform, originalLoc, pounceLandingZone, 0.5f, State.GameManager.TacticalMode.IsPlayerTurn);
            State.GameManager.TacticalMode.DirtyPack = true;
            if (Equals(Unit.Race, Race.FeralFrog)) Mode = DisplayMode.FrogPouncing;
            return true;
        }

        return false;
    }

    public bool ShunGokuSatsu(Actor_Unit target)
    {
        if (Movement < 1 || Unit.HasTrait(TraitType.ShunGokuSatsu) == false) return false;
        List<AbilityTargets> targetTypes = new List<AbilityTargets>();
        targetTypes.Add(AbilityTargets.Enemy);
        if (!TacticalUtilities.MeetsQualifier(targetTypes, this, target)) return false;
        if (target.Position.GetNumberOfMovesDistance(Position) > 1) return false;

        int damage = 2 * WeaponDamageAgainstTarget(target, false);
        if (damage >= target.Unit.Health) damage = target.Unit.Health - 1;
        if (target.Defend(this, ref damage, false, out float chance))
        {
            if (State.GameManager.TacticalMode.TacticalSoundBlocked() == false)
            {
                var obj = Object.Instantiate(State.GameManager.TacticalEffectPrefabList.ShunGokuSatsu);
                obj.transform.SetPositionAndRotation(new Vector3(target.Position.X, target.Position.Y), new Quaternion());
                MiscUtilities.DelayedInvoke(() => State.GameManager.SoundManager.PlayArrowHit(null), .06f);
                MiscUtilities.DelayedInvoke(() => State.GameManager.SoundManager.PlayMeleeHit(null), .12f);
                MiscUtilities.DelayedInvoke(() => State.GameManager.SoundManager.PlayArmorHit(null), .18f);
                MiscUtilities.DelayedInvoke(() => State.GameManager.SoundManager.PlayMeleeHit(null), .24f);
                State.GameManager.TacticalMode.CreateSwipeHitEffect(target.Position, 0);
                State.GameManager.TacticalMode.CreateSwipeHitEffect(target.Position, 90);
                State.GameManager.TacticalMode.CreateSwipeHitEffect(target.Position, 180);
                State.GameManager.TacticalMode.CreateSwipeHitEffect(target.Position, 270);
            }

            Unit.GiveScaledExp(2 * target.Unit.ExpMultiplier, Unit.Level - target.Unit.Level);
        }

        PredatorComponent?.Devour(target, .3f);
        TurnUsedShun = State.GameManager.TacticalMode.currentTurn;
        Movement = 0;

        return true;
    }

    // Hey, wanna see the hackiest code I'll ever cobble together?
    public bool Regurgitate(Actor_Unit a, Vec2i l)
    {
        Prey regurged = a.PredatorComponent.FreeRandomPreyToSquare(l);
        if (regurged != null)
        {
            return true;
        }
        else
            return false;
    }

    public bool TailStrike(Actor_Unit target)
    {
        if (Movement < 1 || Unit.HasTrait(TraitType.TailStrike) == false) return false;
        List<AbilityTargets> targetTypes = new List<AbilityTargets>();
        targetTypes.Add(AbilityTargets.Enemy);
        if (!TacticalUtilities.MeetsQualifier(targetTypes, this, target)) return false;
        if (target.Position.GetNumberOfMovesDistance(Position) != 1) return false;

        if (Equals(Unit.Race, Race.Zoey) && animationController?.frameLists != null && animationController.frameLists.Count() > 0)
        {
            animationController.frameLists[0].currentlyActive = true;
        }

        Attack(target, false, damageMultiplier: .66f);
        Actor_Unit tempTarget = TacticalUtilities.GetActorAt(target.Position + new Vec2(1, 0));
        TestAttack(tempTarget);
        tempTarget = TacticalUtilities.GetActorAt(target.Position + new Vec2(0, 1));
        TestAttack(tempTarget);
        tempTarget = TacticalUtilities.GetActorAt(target.Position + new Vec2(-1, 0));
        TestAttack(tempTarget);
        tempTarget = TacticalUtilities.GetActorAt(target.Position + new Vec2(0, -1));
        TestAttack(tempTarget);

        Movement = 0;

        return true;

        void TestAttack(Actor_Unit sideTarget)
        {
            if (sideTarget != null && sideTarget.Position.GetNumberOfMovesDistance(Position) == 1)
            {
                Movement = 1;
                Attack(sideTarget, false, damageMultiplier: .66f);
            }
        }
    }

    public bool Attack(Actor_Unit target, bool ranged, bool forceBite = false, float damageMultiplier = 1, bool canKill = true)
    {
        Weapon weapon;
        if (ranged)
            weapon = BestRanged;
        else
            weapon = BestMelee;
        if (forceBite) weapon = State.World.ItemRepository.Bite;
        if (Movement == 0 || weapon == null)
        {
            return false;
        }

        if (target.Unit.HasTrait(TraitType.Dazzle))
        {
            float chance = WillCheckOdds(this, target);
            if (State.Rand.NextDouble() < chance)
            {
                Movement = 0;
                UnitSprite.DisplayDazzle();
                Unit.ApplyStatusEffect(StatusEffectType.Shaken, 0.3f, 1);
                TacticalUtilities.Log.RegisterDazzle(Unit, target.Unit, chance);
                return false;
            }
        }

        float origDamageMult = damageMultiplier;
        bool grazebool = false;
        bool critbool = false;
        if (Config.CombatComplicationsEnabled)
        {
            // Graze Check
            float grazechance = Config.BaseGrazeChance;
            if (Config.StatGraze)
            {
                grazechance = GrazeCheck(this, target);
            }

            grazechance += Unit.TraitBoosts.Outgoing.GrazeRateShift - target.Unit.TraitBoosts.Incoming.GrazeRateShift;
            if (State.Rand.NextDouble() < grazechance)
            {
                float calculatedGrazeDamage = Unit.TraitBoosts.Outgoing.GrazeDamageMult * target.Unit.TraitBoosts.Incoming.GrazeDamageMult;
                damageMultiplier *= calculatedGrazeDamage * Config.GrazeDamageMod;
                grazebool = true;
            }

            //Crit check
            float critchance = Config.BaseCritChance;
            if (Config.StatCrit)
            {
                critchance = CritCheck(this, target);
            }

            critchance += Unit.TraitBoosts.Outgoing.CritRateShift - target.Unit.TraitBoosts.Incoming.CritRateShift;
            if (State.Rand.NextDouble() < critchance)
            {
                float calculatedCritDamage = Unit.TraitBoosts.Outgoing.CritDamageMult * target.Unit.TraitBoosts.Incoming.CritDamageMult;
                damageMultiplier *= calculatedCritDamage * Config.CritDamageMod;
                critbool = true;
            }

            // Crit and graze check (returns attack to normal state if both are true)
            if (critbool && grazebool)
            {
                damageMultiplier = origDamageMult;
                critbool = false;
                grazebool = false;
            }
        }

        //check range
        int targetRange = target.Position.GetNumberOfMovesDistance(Position);
        if (weapon.Range > 1)
        {
            if ((targetRange >= 2 || (targetRange >= 1 && weapon.Omni)) && targetRange <= weapon.Range)
            {
                if (Equals(Unit.Race, Race.Succubus)) TacticalGraphicalEffects.SuccubusSwordEffect(target.Position);
                animationUpdateTime = 1.0F;
                Mode = DisplayMode.Attacking;
                if (Unit.TraitBoosts.RangedAttacks > 1)
                {
                    int movementFraction = 1 + MaxMovement() / Unit.TraitBoosts.RangedAttacks;
                    if (Movement > movementFraction)
                        Movement -= movementFraction;
                    else
                        Movement = 0;
                }
                else
                    Movement = 0;

                int remainingHealth = target.Unit.Health;
                int damage = WeaponDamageAgainstTarget(target, true, multiplier: damageMultiplier);

                State.GameManager.SoundManager.PlaySwing(this);
                if (target.Defend(this, ref damage, true, out float chance, canKill))
                {
                    foreach (IAttackStatusEffect trait in Unit.AttackStatusEffects)
                    {
                        trait.ApplyStatusEffect(this, target, true, damage);
                    }

                    if (Unit.HasTrait(TraitType.Tenacious)) Unit.RemoveTenacious();
                    if (target.Unit.HasTrait(TraitType.Tenacious)) target.Unit.AddTenacious();
                    if (target.Unit.GetStatusEffect(StatusEffectType.Focus) != null) target.Unit.RemoveFocus();

                    TacticalGraphicalEffects.CreateProjectile(this, target);
                    State.GameManager.TacticalMode.TacticalStats.RegisterHit(BestRanged, Mathf.Min(damage, remainingHealth), Unit.Side);
                    TacticalUtilities.Log.RegisterHit(Unit, target.Unit, weapon, damage, chance);
                    if (Equals(Unit.FixedSide, TacticalUtilities.GetMindControlSide(target.Unit)))
                    {
                        StatusEffect charm = target.Unit.GetStatusEffect(StatusEffectType.Charmed);
                        if (charm != null)
                        {
                            target.Unit.StatusEffects.Remove(charm); // betrayal dispels charm
                        }
                    }

                    Unit.GiveScaledExp(2 * target.Unit.ExpMultiplier, Unit.Level - target.Unit.Level);
                    if (target.Unit.IsDead)
                    {
                        State.GameManager.TacticalMode.TacticalStats.RegisterKill(BestRanged, Unit.Side);
                        KillUnit(target, weapon);
                    }

                    if (critbool)
                        target.UnitSprite.DisplayCrit();
                    else if (grazebool) target.UnitSprite.DisplayGraze();
                    return true;
                }
                else
                {
                    Unit.GiveScaledExp(0.5f * target.Unit.ExpMultiplier, Unit.Level - target.Unit.Level);
                    State.GameManager.TacticalMode.TacticalStats.RegisterMiss(Unit.Side);
                    TacticalUtilities.Log.RegisterMiss(Unit, target.Unit, weapon, chance);
                    if (Unit.HasTrait(TraitType.Tenacious)) Unit.AddTenacious();
                }
            }
        }
        else
        {
            if (targetRange < 2)
            {
                animationUpdateTime = 1.0F;
                Mode = DisplayMode.Attacking;
                int meleeAttacks = Unit.TraitBoosts.MeleeAttacks;
                if (Unit.HasTrait(TraitType.LightFrame) && PredatorComponent?.PreyCount == 0) meleeAttacks++;
                if (meleeAttacks > 1)
                {
                    int movementFraction = 1 + MaxMovement() / meleeAttacks;
                    if (Movement > movementFraction)
                        Movement -= movementFraction;
                    else
                        Movement = 0;
                }
                else
                    Movement = 0;

                int remainingHealth = target.Unit.Health;
                int damage = WeaponDamageAgainstTarget(target, false, multiplier: damageMultiplier, forceBite);
                if (target.Defend(this, ref damage, false, out float chance, canKill))
                {
                    foreach (IAttackStatusEffect trait in Unit.AttackStatusEffects)
                    {
                        trait.ApplyStatusEffect(this, target, false, damage);
                    }

                    if (Unit.HasTrait(TraitType.BladeDance)) Unit.AddBladeDance();
                    if (target.Unit.HasTrait(TraitType.BladeDance)) target.Unit.RemoveBladeDance();
                    if (Unit.HasTrait(TraitType.Tenacious)) Unit.RemoveTenacious();
                    if (target.Unit.HasTrait(TraitType.Tenacious)) target.Unit.AddTenacious();
                    if (target.Unit.GetStatusEffect(StatusEffectType.Focus) != null) target.Unit.RemoveFocus();
                    if (target.Unit.HasTrait(TraitType.Toxic) && State.Rand.Next(8) == 0) Unit.ApplyStatusEffect(StatusEffectType.Poisoned, 2 + target.Unit.GetStat(Stat.Endurance) / 20, 3);
                    if (Unit.HasTrait(TraitType.ForcefulBlow)) TacticalUtilities.KnockBack(this, target);
                    State.GameManager.SoundManager.PlayMeleeHit(target);
                    State.GameManager.TacticalMode.TacticalStats.RegisterHit(BestMelee, Mathf.Min(damage, remainingHealth), Unit.Side);
                    TacticalUtilities.Log.RegisterHit(Unit, target.Unit, weapon, damage, chance);
                    if (Equals(Unit.FixedSide, TacticalUtilities.GetMindControlSide(target.Unit)))
                    {
                        StatusEffect charm = target.Unit.GetStatusEffect(StatusEffectType.Charmed);
                        if (charm != null)
                        {
                            target.Unit.StatusEffects.Remove(charm); // betrayal dispels charm
                        }
                    }

                    CreateHitEffects(target);
                    Unit.GiveScaledExp(2 * target.Unit.ExpMultiplier, Unit.Level - target.Unit.Level);
                    if (target.Unit.IsDead)
                    {
                        State.GameManager.TacticalMode.TacticalStats.RegisterKill(BestMelee, Unit.Side);
                        KillUnit(target, weapon);
                    }

                    if (critbool)
                        target.UnitSprite.DisplayCrit();
                    else if (grazebool) target.UnitSprite.DisplayGraze();
                    return true;
                }
                else
                {
                    Unit.GiveScaledExp(0.5f * target.Unit.ExpMultiplier, Unit.Level - target.Unit.Level);
                    State.GameManager.TacticalMode.TacticalStats.RegisterMiss(Unit.Side);
                    TacticalUtilities.Log.RegisterMiss(Unit, target.Unit, weapon, chance);
                    State.GameManager.SoundManager.PlaySwing(this);
                    if (Unit.HasTrait(TraitType.Tenacious)) Unit.AddTenacious();
                }
            }
        }

        return false;
    }

    private void CreateHitEffects(Actor_Unit target)
    {
        State.GameManager.TacticalMode.CreateBloodHitEffect(target.Position);
        if (Equals(Unit.Race, Race.Asura)) State.GameManager.TacticalMode.CreateSwipeHitEffect(target.Position);
    }

    private void KillUnit(Actor_Unit target, Weapon weapon)
    {
        TacticalUtilities.Log.RegisterKill(Unit, target.Unit, weapon);
        Unit.KilledUnits++;
        target.KilledByDigestion = false;
        if (Equals(target.Unit.Race, Race.Asura)) Unit.EarnedMask = true;
        Unit.EnemiesKilledThisBattle++;
        target.Unit.KilledBy = Unit;
        target.Unit.Kill();
        if (Unit.HasTrait(TraitType.KillerKnowledge) && Unit.KilledUnits % 4 == 0) Unit.GeneralStatIncrease(1);
        if (Unit.HasTrait(TraitType.TasteForBlood)) GiveRandomBoost();

        Unit.GiveScaledExp(4 * target.Unit.ExpMultiplier, Unit.Level - target.Unit.Level);
    }

    private void KillUnit(Actor_Unit target, Spell spell)
    {
        TacticalUtilities.Log.RegisterSpellKill(Unit, target.Unit, spell.SpellType);
        Unit.KilledUnits++;
        target.KilledByDigestion = false;
        if (Equals(target.Unit.Race, Race.Asura)) Unit.EarnedMask = true;
        Unit.EnemiesKilledThisBattle++;
        target.Unit.KilledBy = Unit;
        target.Unit.Kill();
        if (Unit.HasTrait(TraitType.KillerKnowledge) && Unit.KilledUnits % 4 == 0) Unit.GeneralStatIncrease(1);
        if (Unit.HasTrait(TraitType.TasteForBlood)) GiveRandomBoost();
        Unit.GiveScaledExp(4 * target.Unit.ExpMultiplier, Unit.Level - target.Unit.Level);
    }

    /// <summary>
    ///     Gives a random temporary boost, associated with the Taste for blood trait
    /// </summary>
    internal void GiveRandomBoost()
    {
        int rand = State.Rand.Next(5);
        switch (rand)
        {
            case 0:
                Unit.StatusEffects.Add(new StatusEffect(StatusEffectType.Valor, .25f, 5));
                break;
            case 1:
                Unit.StatusEffects.Add(new StatusEffect(StatusEffectType.Fast, 0.3f, 5));
                break;
            case 2:
                Unit.StatusEffects.Add(new StatusEffect(StatusEffectType.Mending, 24, 5));
                break;
            case 3:
                Unit.StatusEffects.Add(new StatusEffect(StatusEffectType.Shielded, .25f, 5));
                break;
            case 4:
                Unit.StatusEffects.Add(new StatusEffect(StatusEffectType.Predation, Unit.GetStat(Stat.Voracity) / 4, 5));
                break;
        }
    }

    internal bool DefendSpellCheck(Spell spell, Actor_Unit attacker, out float chance, float mod = 0, Stat stat = Stat.Mind)
    {
        State.GameManager.TacticalMode.AITimer = Config.TacticalAttackDelay;
        if (State.GameManager.CurrentScene == State.GameManager.TacticalMode && State.GameManager.TacticalMode.IsPlayerInControl == false && State.GameManager.TacticalMode.turboMode == false) State.GameManager.CameraCall(Position);
        chance = spell.Resistable ? GetMagicChance(attacker, spell, mod, stat) : 1;
        float r = (float)State.Rand.NextDouble();
        return r < chance;
    }

    internal bool DefendDamageSpell(DamageSpell spell, Actor_Unit attacker, int damage)
    {
        if (TacticalUtilities.SneakAttackCheck(attacker.Unit, Unit))
        {
            attacker.Unit.hiddenFixedSide = false;
            damage *= 3;
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<color=purple>{attacker.Unit.Name} sneak-attacked {Unit.Name}!</color>");
        }

        if (!Equals(attacker.Unit.GetApparentSide(), Unit.GetApparentSide()))
        {
            if (attacker.SidesAttackedThisBattle == null) attacker.SidesAttackedThisBattle = new List<Side>();
            attacker.SidesAttackedThisBattle.Add(Unit.GetApparentSide());
        }

        if (Unit.IsDead) return false;
        if (DefendSpellCheck(spell, attacker, out float chance))
        {
            damage = (int)(damage * attacker.Unit.TraitBoosts.Outgoing.MagicDamage * Unit.TraitBoosts.Incoming.MagicDamage);
            State.GameManager.TacticalMode.TacticalStats.RegisterHit(spell, Mathf.Min(damage, Unit.Health), attacker.Unit.Side);
            Damage(damage, true, damageType: spell.DamageType);
            State.GameManager.TacticalMode.Log.RegisterSpellHit(attacker.Unit, Unit, spell.SpellType, damage, chance);
            if (Equals(attacker.Unit.FixedSide, TacticalUtilities.GetMindControlSide(Unit)))
            {
                StatusEffect charm = Unit.GetStatusEffect(StatusEffectType.Charmed);
                if (charm != null)
                {
                    Unit.StatusEffects.Remove(charm); // betrayal dispels charm
                }
            }

            if (attacker.Unit.HasTrait(TraitType.ArcaneMagistrate))
            {
                attacker.Unit.AddFocus(Unit.IsDead ? 5 : 1);
            }

            attacker.Unit.GiveScaledExp(1 * Unit.ExpMultiplier, Unit.Level - Unit.Level);
            if (Unit.IsDead)
            {
                attacker.Unit.GiveScaledExp(4 * Unit.ExpMultiplier, Unit.Level - Unit.Level);
            }

            return true;
        }
        else
        {
            UnitSprite.DisplayDamage(0);
            State.GameManager.TacticalMode.Log.RegisterSpellMiss(attacker.Unit, Unit, spell.SpellType, chance);
            attacker.Unit.GiveScaledExp(.25f * Unit.ExpMultiplier, Unit.Level - Unit.Level);
        }

        return false;
    }

    internal bool DefendStatusSpell(StatusSpell spell, Actor_Unit attacker, Stat stat = Stat.Mind)
    {
        if (spell.Id == "hypno-fart" && Equals(Unit.FixedSide, attacker.Unit.FixedSide))
        {
            return false;
        }

        bool sneakAttack = false;
        if (TacticalUtilities.SneakAttackCheck(attacker.Unit, Unit) && !spell.AcceptibleTargets.Contains(AbilityTargets.Ally)) // Replace when there is an unresistable negative status
        {
            attacker.Unit.hiddenFixedSide = false;
            sneakAttack = true;
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<color=purple>{attacker.Unit.Name} sneak-attacked {Unit.Name}!</color>");
        }

        if (!Equals(attacker.Unit.GetApparentSide(), Unit.GetApparentSide()) && !spell.AcceptibleTargets.Contains(AbilityTargets.Ally))
        {
            if (attacker.SidesAttackedThisBattle == null) attacker.SidesAttackedThisBattle = new List<Side>();
            attacker.SidesAttackedThisBattle.Add(Unit.GetApparentSide());
        }

        if (DefendSpellCheck(spell, attacker, out float chance, sneakAttack ? -0.3f : 0f, stat))
        {
            State.GameManager.TacticalMode.Log.RegisterSpellHit(attacker.Unit, Unit, spell.SpellType, 0, chance);
            Unit.ApplyStatusEffect(spell.Type, spell.Effect(attacker, this), spell.Duration(attacker, this), spell.EffectSide?.Invoke(attacker, this));
            if (spell.Id == "charm")
            {
                UnitSprite.DisplayCharm();
                if (attacker.Unit.HasTrait(TraitType.Temptation))
                {
                    Unit.ApplyStatusEffect(StatusEffectType.Temptation, spell.Effect(attacker, this), spell.Duration(attacker, this), spell.EffectSide?.Invoke(attacker, this));
                }
            }

            if (spell.Id == "hypno-fart")
            {
                UnitSprite.DisplayHypno();
                if (attacker.Unit.HasTrait(TraitType.Temptation))
                {
                    Unit.ApplyStatusEffect(StatusEffectType.Temptation, spell.Effect(attacker, this), spell.Duration(attacker, this), spell.EffectSide?.Invoke(attacker, this));
                }
            }

            if (spell.Alraune)
            {
                if (Unit.HasTrait(TraitType.PollenProjector) == false)
                {
                    Unit.ApplyStatusEffect(StatusEffectType.Clumsiness, 1.5f, 3);
                    Unit.ApplyStatusEffect(StatusEffectType.Poisoned, 2 + attacker.Unit.GetStat(Stat.Mind) / 10f, 3);
                    Unit.ApplyStatusEffect(StatusEffectType.WillingPrey, 0, 3);
                }
            }

            if (spell.Id == "whispers-spell")
            {
                UnitSprite.DisplayCharm();
                Unit.ApplyStatusEffect(StatusEffectType.WillingPrey, 0, spell.Duration(attacker, this));
                Unit.ApplyStatusEffect(StatusEffectType.Temptation, 0, spell.Duration(attacker, this));
            }

            if (TacticalUtilities.MeetsQualifier(spell.AcceptibleTargets, attacker, this))
                attacker.Unit.GiveScaledExp(1, attacker.Unit.Level - Unit.Level);
            else
                attacker.Unit.GiveExp(1);
            return true;
        }
        else
        {
            UnitSprite.DisplayDamage(0);
            if (Equals(attacker.Unit.Side, Unit.Side))
                attacker.Unit.GiveScaledExp(.25f, attacker.Unit.Level - Unit.Level);
            else
            {
                attacker.Unit.GiveExp(.25f);
                UnitSprite.DisplayResist();
            }

            State.GameManager.TacticalMode.Log.RegisterSpellMiss(attacker.Unit, Unit, spell.SpellType, chance);
        }

        return false;
    }

    public bool Defend(Actor_Unit attacker, ref int damage, bool ranged, out float chance, bool canKill = true)
    {
        if (TacticalUtilities.SneakAttackCheck(attacker.Unit, Unit))
        {
            attacker.Unit.hiddenFixedSide = false;
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<color=purple>{attacker.Unit.Name} sneak-attacked {Unit.Name}!</color>");
        }

        if (!Equals(attacker.Unit.GetApparentSide(), Unit.GetApparentSide()))
        {
            if (attacker.SidesAttackedThisBattle == null) attacker.SidesAttackedThisBattle = new List<Side>();
            attacker.SidesAttackedThisBattle.Add(Unit.GetApparentSide());
        }

        State.GameManager.TacticalMode.AITimer = Config.TacticalAttackDelay;
        if (State.GameManager.CurrentScene == State.GameManager.TacticalMode && State.GameManager.TacticalMode.IsPlayerInControl == false && State.GameManager.TacticalMode.turboMode == false) State.GameManager.CameraCall(Position);
        chance = GetAttackChance(attacker, ranged);

        float r = (float)State.Rand.NextDouble();
        if (r < chance)
        {
            Damage(damage, canKill: canKill);
            if (canKill == false && attacker.Unit.HasTrait(TraitType.VenomousBite))
            {
                Unit.ApplyStatusEffect(StatusEffectType.Poisoned, 3, 3);
                Unit.ApplyStatusEffect(StatusEffectType.Shaken, .2f, 2);
            }

            return true;
        }
        else
            UnitSprite.DisplayDamage(0);

        return false;
    }

    //This is the chance to be devoured, so it belongs here and not in PredatorComponent
    public float GetDevourChance(Actor_Unit attacker, bool includeSecondaries = false, int skillBoost = 0, bool force = false)
    {
        if (attacker?.Unit.Predator == false && !force)
        {
            return 0;
        }

        StatusEffect hypnotizedEffect = Unit.GetStatusEffect(StatusEffectType.Hypnotized);
        if (Surrendered || (attacker.Unit.HasTrait(TraitType.Endosoma) && (Equals(Unit.FixedSide, attacker.Unit.GetApparentSide(Unit)) || Equals(hypnotizedEffect.Side, attacker.Unit.FixedSide)))) return 1f;

        float predVoracity = Mathf.Pow(15 + skillBoost + attacker.Unit.GetStat(Stat.Voracity), 1.5f);
        float preyStrength = Mathf.Pow(15 + Unit.GetStat(Stat.Strength), 1.5f);
        float preyWill = Mathf.Pow(15 + Unit.GetStat(Stat.Will), 1.5f);
        float attackerScore = predVoracity * (attacker.Unit.HealthPct + 1) * (30 + attacker.BodySize()) / 16 / (1 + 2 * Mathf.Pow(attacker.PredatorComponent.UsageFraction, 2));
        float defenderHealthPct = Unit.HealthPct;
        float defenderScore = 20 + 2 * (preyStrength + 2 * preyWill) * defenderHealthPct * defenderHealthPct * ((10 + BodySize()) / 2);

        if (Unit.GetStatusEffect(StatusEffectType.WillingPrey) != null || Unit.GetStatusEffect(StatusEffectType.Sleeping) != null)
        {
            defenderScore /= 2;
        }

        switch (Config.VoreRate)
        {
            case -1:
                attackerScore *= .4f;
                break;
            case 1:
                attackerScore *= 2;
                break;
            case 2:
                attackerScore *= 4;
                break;
        }

        defenderScore /= Unit.TraitBoosts.Incoming.VoreOddsMult;
        attackerScore *= attacker.Unit.TraitBoosts.Outgoing.VoreOddsMult;

        if (TacticalUtilities.SneakAttackCheck(attacker.Unit, Unit)) // sneakAttack
        {
            attackerScore *= 3;
        }

        if (attacker.Unit.HasTrait(TraitType.VenomShock) && Unit.GetStatusEffect(StatusEffectType.Poisoned) != null) attackerScore *= 1.5f;

        if (attacker.Unit.HasTrait(TraitType.AllOutFirstStrike) && attacker.HasAttackedThisCombat == false) attackerScore *= 3.25f;

        foreach (IVoreAttackOdds trait in attacker.Unit.VoreAttackOdds)
        {
            trait.VoreAttack(attacker, ref attackerScore);
        }

        foreach (IVoreDefenseOdds trait in Unit.VoreDefenseOdds)
        {
            trait.VoreDefense(this, ref defenderScore);
        }

        float odds = attackerScore / (attackerScore + defenderScore) * 100;

        odds *= Unit.TraitBoosts.FlatHitReduction;

        if (includeSecondaries)
        {
            if (Unit.HasTrait(TraitType.Dazzle))
            {
                odds *= 1 - WillCheckOdds(attacker, this);
            }
        }

        return odds / 100;
    }

    public bool BellyRub(Actor_Unit target)
    {
        Prey prey;
        if (target.Unit.Predator == false) return false;
        List<int> possible = new List<int>();
        int type;
        if (target.PredatorComponent.Fullness <= 0)
        {
            return false;
        }

        if (Position.GetNumberOfMovesDistance(target.Position) > 1)
        {
            return false;
        }

        if (target.PredatorComponent.BreastFullness > 0)
        {
            possible.Add(1);
        }

        if (target.PredatorComponent.LeftBreastFullness > 0)
        {
            possible.Add(1);
        }

        if (target.PredatorComponent.RightBreastFullness > 0)
        {
            possible.Add(1);
        }

        if (target.PredatorComponent.WombFullness > 0 || target.PredatorComponent.CombinedStomachFullness > 0)
        {
            possible.Add(0);
        }

        if (target.PredatorComponent.BallsFullness > 0)
        {
            possible.Add(2);
        }

        if (target.PredatorComponent.TailFullness > 0)
        {
            possible.Add(3);
        }

        if (possible.Count == 0) return false;
        if (target.ReceivedRub) return false;
        if (!Equals(target.Unit.GetApparentSide(), Unit.GetApparentSide()) && !Equals(target.Unit.GetApparentSide(), Unit.FixedSide) && !(Unit.HasTrait(TraitType.SeductiveTouch) || Config.CanUseStomachRubOnEnemies || !Equals(TacticalUtilities.GetMindControlSide(Unit), Side.TrueNoneSide))) return false;
        target.ReceivedRub = true;
        int index = Random.Range(0, possible.Count - 1);
        type = possible[index];
        switch (type)
        {
            case 0:
                prey = target.PredatorComponent.GetDirectPrey().FirstOrDefault(p => p.Location.Equals(PreyLocation.stomach) || p.Location.Equals(PreyLocation.stomach2) || p.Location.Equals(PreyLocation.anal) || p.Location.Equals(PreyLocation.womb));
                if (prey == null) break;
                TacticalUtilities.Log.RegisterBellyRub(Unit, target.Unit, prey.Unit, 1f);
                break;
            case 1:
                prey = target.PredatorComponent.GetDirectPrey().FirstOrDefault(p => p.Location.Equals(PreyLocation.breasts) || p.Location.Equals(PreyLocation.leftBreast) || p.Location.Equals(PreyLocation.rightBreast));
                if (prey == null) break;
                TacticalUtilities.Log.RegisterBreastRub(Unit, target.Unit, prey.Unit, 1f);
                break;
            case 2:
                prey = target.PredatorComponent.GetDirectPrey().FirstOrDefault(p => p.Location.Equals(PreyLocation.balls));
                if (prey == null) break;
                TacticalUtilities.Log.RegisterBallMassage(Unit, target.Unit, prey.Unit, 1f);
                break;
            case 3:
                prey = target.PredatorComponent.GetDirectPrey().FirstOrDefault(p => p.Location.Equals(PreyLocation.tail));
                if (prey == null) break;
                TacticalUtilities.Log.RegisterTailRub(Unit, target.Unit, prey.Unit, 1f);
                break;
            default:
                break;
        }

        if (!State.GameManager.TacticalMode.turboMode)
        {
            SetRubMode();
            target.SetRubbedMode();
            Object.Instantiate(State.GameManager.TacticalMode.HandPrefab, new Vector3(target.Position.X + Random.Range(-0.2F, 0.2F), target.Position.Y + 0.1F + Random.Range(-0.1F, 0.1F)), new Quaternion());
            State.GameManager.TacticalMode.AITimer = Config.TacticalVoreDelay;
        }

        target.DigestCheck();
        if (Unit.HasTrait(TraitType.PleasurableTouch)) target.DigestCheck();
        int thirdMovement = MaxMovement() / 3;
        if (Movement > thirdMovement)
            Movement -= thirdMovement;
        else
            Movement = 0;

        if (Unit.HasTrait(TraitType.SeductiveTouch) && !Equals(target.Unit.FixedSide, Unit.FixedSide) && target.TurnsSinceLastDamage > 1)
        {
            bool seduce = false;
            bool distract = false;
            if (!target.Unit.HasTrait(TraitType.Untamable))
                for (int i = 0; i < (Unit.HasTrait(TraitType.PleasurableTouch) ? 2 : 1); i++)
                {
                    float r = (float)State.Rand.NextDouble();
                    if (r < GetPureStatClashChance(Unit.GetStat(Stat.Dexterity), target.Unit.GetStat(Stat.Will), .1f))
                    {
                        if (target.TurnsSinceLastParalysis <= 0 || target.Paralyzed)
                        {
                            seduce = true;
                        }
                        else
                        {
                            distract = true;
                        }
                    }
                }

            if (seduce)
            {
                var strings = new[]
                {
                    $"<b>{target.Unit.Name}</b> decides to swap sides because <b>{Unit.Name}</b>'s rubs are just that sublime!",
                    $"<b>{target.Unit.Name}</b> joins <b>{Unit.Name}</b>'s side to get more of those irresistible scritches later!",
                    $"The way <b>{Unit.Name}</b> touches {LogUtilities.GPPHim(target.Unit)} moves something other than {LogUtilities.GPPHis(target.Unit)} prey-filled innards in <b>{target.Unit.Name}</b>, making {LogUtilities.GPPHim(target.Unit)} join {LogUtilities.GPPHim(Unit)}.",
                    $"<b>{Unit.Name}</b> makes <b>{target.Unit.Name}</b> feel incredible, rearranging {LogUtilities.GPPHis(target.Unit)} priorities in this conflict...",
                    $"<b>{target.Unit.Name}</b> slowly returns from a world of pure bliss and decides to stick with <b>{Unit.Name}</b> after all."
                };
                if (Equals(target.Unit.Race, Race.Dog)) strings.Append($"<b>{Unit.Name}</b>’s attentive massage of <b>{target.Unit.Name}</b>’s stuffed midsection convinces the voracious canine to make {LogUtilities.GPPHim(Unit)} {LogUtilities.GPPHis(target.Unit)} master no matter the cost.");
                target.UnitSprite.DisplaySeduce();
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous(
                    LogUtilities.GetRandomStringFrom(strings)
                );
                if (!Equals(target.Unit.Side, Unit.Side)) State.GameManager.TacticalMode.SwitchAlignment(target);
                target.Unit.FixedSide = Unit.FixedSide;
                target.Unit.hiddenFixedSide = true;
            }
            else if (distract)
            {
                target.UnitSprite.DisplayDistract();
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous(
                    LogUtilities.GetRandomStringFrom(
                        $"<b>{target.Unit.Name}</b> is so enthralled by <b>{Unit.Name}</b>'s touch that {LogUtilities.GPPHe(target.Unit)} forgot what {LogUtilities.GPPHeWas(target.Unit)} gonna do.",
                        $"Despite the battle, <b>{target.Unit.Name}</b> wants to spend {LogUtilities.GPPHis(target.Unit)} turn enjoying <b>{Unit.Name}</b>'s affection.",
                        $"It looks like <b>{Unit.Name}</b>'s rubs take up 100% of <b>{target.Unit.Name}</b>'s attention right now.",
                        $"<b>{target.Unit.Name}</b>'s aggression and tactics melt in <b>{Unit.Name}</b>'s hands",
                        $"<b>{Unit.Name}</b>'s massage really hits the spot, causing <b>{target.Unit.Name}</b> to close {LogUtilities.GPPHis(target.Unit)} eyes and forget about the battle for a moment."
                    )
                );
                target.Paralyzed = true;
            }
        }

        return true;
    }

    public float BodySize()
    {
        float size = State.RaceSettings.GetBodySize(Unit.Race);

        size *= Unit.GetScale(2);

        size *= Unit.TraitBoosts.BulkMultiplier;

        if (Unit.GetStatusEffect(StatusEffectType.Petrify) != null) size *= 3;

        return size;
    }

    public float Bulk(int count = 0)
    {
        if (Unit.HasTrait(TraitType.Inedible)) return float.MaxValue / 100;
        float bulk = 0;
        bulk += PredatorComponent?.GetBulkOfPrey(count) ?? 0;
        if (Unit.IsDead)
        {
            float myBulk = Unit.Health + Unit.MaxHealth;
            myBulk = myBulk / Unit.MaxHealth * BodySize();

            bulk += myBulk;
        }
        else
        {
            bulk += BodySize();
        }

        return bulk;
    }


    public bool Move(int direction, TacticalTileType[,] tiles)
    {
        //check if we can move in that direction
        switch (direction)
        {
            case 0:
                return Move(0, 1, tiles);
            case 1:
                return Move(1, 1, tiles);
            case 2:
                return Move(1, 0, tiles);
            case 3:
                return Move(1, -1, tiles);
            case 4:
                return Move(0, -1, tiles);
            case 5:
                return Move(-1, -1, tiles);
            case 6:
                return Move(-1, 0, tiles);
            case 7:
                return Move(-1, 1, tiles);
        }

        return false;
    }

    internal Vec2i GetTile(Vec2i start, int direction)
    {
        switch (direction)
        {
            case 0:
                return new Vec2i(start.X, start.Y + 1);
            case 1:
                return new Vec2i(start.X + 1, start.Y + 1);
            case 2:
                return new Vec2i(start.X + 1, start.Y);
            case 3:
                return new Vec2i(start.X + 1, start.Y - 1);
            case 4:
                return new Vec2i(start.X, start.Y - 1);
            case 5:
                return new Vec2i(start.X - 1, start.Y - 1);
            case 6:
                return new Vec2i(start.X - 1, start.Y);
            case 7:
                return new Vec2i(start.X - 1, start.Y + 1);
            default:
                return null;
        }
    }

    internal int DiffToDirection(int x, int y)
    {
        if (x == 0 && y > 0)
            return 0;
        else if (x > 0 && y > 0)
            return 1;
        else if (x > 0 && y == 0)
            return 2;
        else if (x > 0 && y < 0)
            return 3;
        else if (x == 0 && y < 0)
            return 4;
        else if (x < 0 && y < 0)
            return 5;
        else if (x < 0 && y == 0)
            return 6;
        else if (x < 0 && y > 0)
            return 7;
        else
            return 0;
    }


    internal bool MoveTo(Vec2i destination, TacticalTileType[,] tiles, float delay)
    {
        if (destination.X < 0 || destination.Y < 0 || destination.X > tiles.GetUpperBound(0) || destination.Y > tiles.GetUpperBound(1)) return false;
        int cost = TacticalTileInfo.TileCost(new Vec2(destination.X, destination.Y));
        if (Unit.HasTrait(TraitType.Flight)) cost = 1;
        if (Movement < cost) return false;
        if ((Unit.HasTrait(TraitType.Flight) && Movement > 1) || TacticalUtilities.OpenTile(destination, this))
        {
            State.GameManager.TacticalMode.Translator.SetTranslator(UnitSprite.transform, Position, destination, delay, State.GameManager.TacticalMode.IsPlayerTurn);
            State.GameManager.TacticalMode.AITimer = delay;
            State.GameManager.TacticalMode.DirtyPack = true;
            Position = destination;
            //State.GameManager.CameraCall(Position);
            Movement -= cost;
            return true;
        }

        return false;
    }


    private bool Move(int changeX, int changeY, TacticalTileType[,] tiles)
    {
        //float delay = AI ? Config.TacticalPlayerMovementDelay : Config.TacticalAIMovementDelay;
        Vec2i newLocation = new Vec2i(Position.X + changeX, Position.Y + changeY);
        return MoveTo(newLocation, tiles, Config.TacticalPlayerMovementDelay);
    }


    public void Update(float dt)
    {
        if (animationUpdateTime > 0)
        {
            animationUpdateTime -= dt;
            if (animationUpdateTime <= 0)
            {
                if (Mode == DisplayMode.IdleAnimation)
                {
                    animationUpdateTime = 0.25f;
                    animationStep--;
                    if (animationStep == 0)
                    {
                        if (Mode > DisplayMode.Attacking && Mode < DisplayMode.VoreSuccess && modeQueue.Count > 0)
                        {
                            animationUpdateTime = modeQueue.FirstOrDefault().Value;
                            Mode = (DisplayMode)modeQueue.FirstOrDefault().Key; // This casting back and forth saves dealing with accessibility hassles.
                            modeQueue.RemoveAt(0);
                        }
                        else
                        {
                            animationUpdateTime = 0;
                            Mode = 0;
                        }
                    }
                }
                else
                {
                    if (Mode >= DisplayMode.Attacking && Mode <= DisplayMode.FrogPouncing) HasAttackedThisCombat = true;
                    if (modeQueue.Count > 0)
                    {
                        animationUpdateTime = modeQueue.FirstOrDefault().Value;
                        Mode = (DisplayMode)modeQueue.FirstOrDefault().Key;
                        modeQueue.RemoveAt(0);
                    }
                    else
                    {
                        animationUpdateTime = 0;
                        Mode = 0;
                    }
                }
            }
        }

        if (Unit.Predator) PredatorComponent.UpdateTransition();
    }

    public void NewTurn()
    {
        if (Surrendered && Unit.HasTrait(TraitType.Fearless))
        {
            Surrendered = false;
        }
        else if (SurrenderedThisTurn)
        {
            SurrenderedThisTurn = false;
            Movement = 0;
        }

        AIAvoidEat--;
        if (Unit.HasTrait(TraitType.ManaAttuned))
        {
            if (!Unit.SpendMana(Unit.MaxMana / 10))
                if (Unit.Mana > 0)
                    Unit.SpendMana(Unit.Mana); //Zero out mana
                else
                    Unit.ApplyStatusEffect(StatusEffectType.Sleeping, 1, 2);
            if (Unit.GetStatusEffect(StatusEffectType.Sleeping) != null) Unit.RestoreMana(Unit.MaxMana / 2);
        }

        UnitSprite.UpdateHealthBar(this);
        TurnsSinceLastParalysis++;
        if (Targetable && Visible && Surrendered == false && Fled == false) RestoreMP();
        Unit.TickStatusEffects();
        Unit.Heal(Unit.TraitBoosts.HealthRegen);
        if (Unit.HasTrait(TraitType.Perseverance) && TurnsSinceLastDamage > 3)
        {
            Unit.HealPercentage(0.03f * TurnsSinceLastDamage);
        }

        ReceivedRub = false;
        TurnsSinceLastDamage++;
    }

    public void SubtractHealth(int damage)
    {
        Unit.Health -= damage;
        if (Unit.Health > Unit.MaxHealth) Unit.Health = Unit.MaxHealth;
        TurnsSinceLastDamage = -1;
    }

    public int CalculateDamageWithResistance(int damage, DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.Fire:
                damage = (int)Mathf.Round(damage * Unit.TraitBoosts.FireDamageTaken);
                break;
            case DamageType.Poison:
                if (Unit.HasTrait(TraitType.PoisonSpit)) damage = 0;
                break;
            default:
                break;
        }

        return damage;
    }

    public bool Damage(int damage, bool spellDamage = false, bool canKill = true, DamageType damageType = DamageType.Generic)
    {
        if (Unit.IsDead)
        {
            //These are thrown in as insurance incase of weirdness - there was a bug report of a unit that had negative health and was not flagged as dead, and didn't die when hit.
            Targetable = false;
            Surrendered = true;
            PredatorComponent?.FreeAnyAlivePrey();
            Debug.Log("Attack performed on target that was already dead");
            return false;
        }

        int modifiedDamage = CalculateDamageWithResistance(damage, damageType);
        UnitSprite.DisplayDamage(modifiedDamage, spellDamage);
        SubtractHealth(modifiedDamage);
        if (Unit.HasTrait(TraitType.Berserk) && GoneBerserk == false)
        {
            if (Unit.HealthPct < .5f)
            {
                GoneBerserk = true;
                Unit.ApplyStatusEffect(StatusEffectType.Berserk, 1, 3);
            }
        }

        if ((canKill == false && Unit.IsDead) || (Config.AutoSurrender && Unit.IsDead && State.Rand.NextDouble() < Config.AutoSurrenderChance && Surrendered == false && Unit.HasTrait(TraitType.Fearless) == false && !KilledByDigestion))
        {
            Unit.Health = 1;
            Surrendered = true;
            Movement = 0;
            if (Config.AutoSurrender && Config.SurrenderedCanConvert && Unit.CanBeConverted())
            {
                if (State.Rand.NextDouble() <= Config.AutoSurrenderDefectChance)
                {
                    State.GameManager.TacticalMode.SwitchAlignment(this);
                    AIAvoidEat = 2;
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{Unit.Name} switched sides when they surrendered");
                }
            }
        }

        if (Config.DamageNumbers == false && !State.GameManager.TacticalMode.turboMode)
        {
            Mode = DisplayMode.Injured;
            animationUpdateTime = 1.0F;
        }

        if (Unit.IsDead)
        {
            State.GameManager.TacticalMode.DirtyPack = true;
            Targetable = false;
            Surrendered = true;
            if (Config.VisibleCorpses && !Equals(Unit.Race, Race.Erin))
            {
                float angle = 40 + State.Rand.Next(280);
                UnitSprite.transform.rotation = Quaternion.Euler(0, 0, angle);
                spriteLayerOffset = State.GameManager.TacticalMode.GetNextCorpseLayer();
            }
            else
            {
                Visible = false;
            }

            PredatorComponent?.FreeAnyAlivePrey();
        }

        return true;
    }

    public void DigestCheck(string feedtype = "")
    {
        if (Unit.IsDead == false) PredatorComponent?.Digest(feedtype);
    }

    //public List<Actor_Unit> BirthCheck()
    //{
    //    List<Actor_Unit> released = null;
    //    if (Unit.IsDead == false)
    //        released = PredatorComponent?.Birth();
    //    if (released == null)
    //    {
    //        released = new List<Actor_Unit>();
    //    }
    //    return released;
    //}

    public Vec2i GetPos(int i)
    {
        switch (i)
        {
            case 0:
                return new Vec2i(Position.X, Position.Y + 1);
            case 1:
                return new Vec2i(Position.X + 1, Position.Y + 1);
            case 2:
                return new Vec2i(Position.X + 1, Position.Y);
            case 3:
                return new Vec2i(Position.X + 1, Position.Y - 1);
            case 4:
                return new Vec2i(Position.X, Position.Y - 1);
            case 5:
                return new Vec2i(Position.X - 1, Position.Y - 1);
            case 6:
                return new Vec2i(Position.X - 1, Position.Y);
            case 7:
                return new Vec2i(Position.X - 1, Position.Y + 1);
        }

        return Position;
    }


    public static bool CheckForActor(List<Actor_Unit> actors, Vec2i p)
    {
        for (int i = 0; i < actors.Count; i++)
        {
            if (actors[i].Position.Matches(p) && actors[i].Targetable == true)
            {
                return false;
            }
        }

        return true;
    }

    public bool IsErect()
    {
        if (Unit.DickSize < 0) return false;
        if (Config.ErectionsFromVore)
        {
            if (PredatorComponent?.Fullness > 0) return true;
        }

        if (Config.ErectionsFromCockVore)
        {
            if (PredatorComponent?.BallsFullness > 0) return true;
        }

        return false;
    }

    public float WillCheckOdds(Actor_Unit actor, Actor_Unit target)
    {
        if (target.Unit.IsDead)
        {
            return 0;
        }

        float ratio = (float)target.Unit.GetStat(Stat.Will) / actor.Unit.GetStat(Stat.Will);

        if (ratio > 5) ratio = 5;
        return ratio / 25;
    }

    public float CritCheck(Actor_Unit actor, Actor_Unit target)
    {
        if (target.Unit.IsDead)
        {
            return 0;
        }

        float ratio = 0;
        ratio = (float)((actor.Unit.GetStat(Stat.Dexterity) + actor.Unit.GetStat(Stat.Strength)) /
                        (target.Unit.GetStat(Stat.Endurance) * target.Unit.GetStat(Stat.Endurance) + target.Unit.GetStat(Stat.Will)));

        if (Config.BaseCritChance > ratio) ratio = Config.BaseCritChance;

        return ratio;
    }

    public float GrazeCheck(Actor_Unit actor, Actor_Unit target)
    {
        if (target.Unit.IsDead)
        {
            return 0;
        }

        int actor_stats = (actor.Unit.GetStat(Stat.Dexterity) + actor.Unit.GetStat(Stat.Strength)) / 2;
        float ratio = target.Unit.GetStat(Stat.Agility) / (actor_stats * actor_stats);

        if (Config.BaseGrazeChance > ratio) ratio = Config.BaseGrazeChance;

        return ratio;
    }

    internal bool CastSpell(Spell spell, Actor_Unit target)
    {
        if (Unit.SpendMana(spell.ManaCost) == false && spell.IsFree != true) return false;
        Unit.GiveExp(1);
        //if (target != null && target.DefendSpellCheck(spell, this, out float chance) == false)
        //    return false;

        State.GameManager.SoundManager.PlaySpellCast(spell, this);
        if (target?.UnitSprite != null) State.GameManager.SoundManager.PlaySpellHit(spell, target.UnitSprite.transform.position);

        if (Unit.TraitBoosts.SpellAttacks > 1)
        {
            int movementFraction = 1 + MaxMovement() / Unit.TraitBoosts.SpellAttacks;
            if (Movement > movementFraction)
                Movement -= movementFraction;
            else
                Movement = 0;
        }
        else
            Movement = 0;

        return true;
    }

    internal void CastMawWithLocation(Spell spell, Actor_Unit target, Vec2i targetArea = null)
    {
        PreyLocation preyLocation = PreyLocation.stomach;
        var possibilities = new Dictionary<string, PreyLocation>();
        possibilities.Add("Maw", PreyLocation.stomach);
        if (PredatorComponent.CanAnalVore()) possibilities.Add("Anus", PreyLocation.anal);
        if (PredatorComponent.CanBreastVore()) possibilities.Add("Breast", PreyLocation.breasts);
        if (PredatorComponent.CanCockVore()) possibilities.Add("Cock", PreyLocation.balls);
        if (PredatorComponent.CanUnbirth()) possibilities.Add("Pussy", PreyLocation.womb);

        if (State.GameManager.TacticalMode.IsPlayerInControl && State.GameManager.CurrentScene == State.GameManager.TacticalMode && possibilities.Count > 1)
        {
            var box = State.GameManager.CreateOptionsBox();
            box.SetData($"Where do you want to put your prey?", "Maw", () => CastMaw(spell, target, PreyLocation.stomach, targetArea), possibilities.Keys.ElementAtOrDefault(1), () => CastMaw(spell, target, possibilities.Values.ElementAtOrDefault(1), targetArea), possibilities.Keys.ElementAtOrDefault(2), () => CastMaw(spell, target, possibilities.Values.ElementAtOrDefault(2), targetArea), possibilities.Keys.ElementAtOrDefault(3), () => CastMaw(spell, target, possibilities.Values.ElementAtOrDefault(3), targetArea), possibilities.Keys.ElementAtOrDefault(4), () => CastMaw(spell, target, possibilities.Values.ElementAtOrDefault(4), targetArea));
        }
        else
        {
            preyLocation = possibilities.Values.ToList()[State.Rand.Next(possibilities.Count)];
            CastMaw(spell, target, preyLocation, targetArea);
        }
    }

    internal void CastMaw(Spell spell, Actor_Unit target, PreyLocation preyLocation, Vec2i targetArea = null)
    {
        if (Unit.Predator == false) return;
        if (Unit.SpendMana(spell.ManaCost) == false && spell.IsFree != true) return;

        State.GameManager.SoundManager.PlaySpellCast(spell, this);
        if (target != null)
        {
            if (spell.AreaOfEffect > 0)
            {
                foreach (var splashTarget in TacticalUtilities.UnitsWithinTiles(target.Position, spell.AreaOfEffect))
                {
                    if (PredatorComponent.MagicConsume(spell, splashTarget, preyLocation))
                    {
                        State.GameManager.SoundManager.PlaySpellHit(spell, splashTarget.UnitSprite.transform.position);
                    }
                }
            }
            else
            {
                if (PredatorComponent.MagicConsume(spell, target, preyLocation))
                {
                    State.GameManager.SoundManager.PlaySpellHit(spell, target.UnitSprite.transform.position);
                }
            }
        }
        else if (targetArea != null && spell.AreaOfEffect > 0)
        {
            foreach (var splashTarget in TacticalUtilities.UnitsWithinTiles(targetArea, spell.AreaOfEffect))
            {
                if (PredatorComponent.MagicConsume(spell, splashTarget, preyLocation))
                {
                    State.GameManager.SoundManager.PlaySpellHit(spell, splashTarget.UnitSprite.transform.position);
                }
            }
        }

        if (Unit.TraitBoosts.SpellAttacks > 1)
        {
            int movementFraction = 1 + MaxMovement() / Unit.TraitBoosts.SpellAttacks;
            if (Movement > movementFraction)
                Movement -= movementFraction;
            else
                Movement = 0;
        }
        else
            Movement = 0;
    }

    internal void CastOffensiveSpell(DamageSpell spell, Actor_Unit target, Vec2i targetArea = null)
    {
        if (Unit.SpendMana(spell.ManaCost) == false && spell.IsFree != true) return;
        State.GameManager.SoundManager.PlaySpellCast(spell, this);

        if (target != null)
        {
            if (spell.AreaOfEffect > 0)
            {
                foreach (var splashTarget in TacticalUtilities.UnitsWithinTiles(target.Position, spell.AreaOfEffect).Where(s => s.Unit.IsDead == false))
                {
                    splashTarget.DefendDamageSpell(spell, this, spell.Damage(this, splashTarget));
                    CheckDead(splashTarget);
                }

                State.GameManager.SoundManager.PlaySpellHit(spell, target.UnitSprite.transform.position);
            }
            else
            {
                if (target.DefendDamageSpell(spell, this, spell.Damage(this, target)))
                {
                    State.GameManager.SoundManager.PlaySpellHit(spell, target.UnitSprite.transform.position);
                }

                CheckDead(target);
            }
        }
        else if (targetArea != null && spell.AreaOfEffect > 0)
        {
            foreach (var splashTarget in TacticalUtilities.UnitsWithinTiles(targetArea, spell.AreaOfEffect).Where(s => s.Unit.IsDead == false))
            {
                splashTarget.DefendDamageSpell(spell, this, spell.Damage(this, splashTarget));
                CheckDead(splashTarget);
            }

            State.GameManager.SoundManager.PlaySpellHit(spell, targetArea);
        }

        if (Unit.TraitBoosts.SpellAttacks > 1)
        {
            int movementFraction = 1 + MaxMovement() / Unit.TraitBoosts.SpellAttacks;
            if (Movement > movementFraction)
                Movement -= movementFraction;
            else
                Movement = 0;
        }
        else
            Movement = 0;

        void CheckDead(Actor_Unit hitTarget)
        {
            if (hitTarget.Unit.IsDead)
            {
                State.GameManager.TacticalMode.TacticalStats.RegisterKill(spell, Unit.Side);
                KillUnit(hitTarget, spell);
            }
        }
    }

    internal bool CastStatusSpell(StatusSpell spell, Actor_Unit target, Vec2i targetArea = null, Stat stat = Stat.Mind)
    {
        if (Unit.SpendMana(spell.ManaCost) == false && spell.IsFree != true) return false;

        bool hit = false;

        State.GameManager.SoundManager.PlaySpellCast(spell, this);

        if (target != null)
        {
            if (spell.AreaOfEffect > 0)
            {
                hit = true;
                foreach (var splashTarget in TacticalUtilities.UnitsWithinTiles(target.Position, spell.AreaOfEffect).Where(s => s.Unit.IsDead == false))
                {
                    splashTarget.DefendStatusSpell(spell, this);
                }

                State.GameManager.SoundManager.PlaySpellHit(spell, target.UnitSprite.transform.position);
            }
            else
            {
                if (target.DefendStatusSpell(spell, this))
                {
                    hit = true;
                    State.GameManager.SoundManager.PlaySpellHit(spell, target.UnitSprite.transform.position);
                }
            }
        }
        else if (targetArea != null && spell.AreaOfEffect > 0)
        {
            foreach (var splashTarget in TacticalUtilities.UnitsWithinTiles(targetArea, spell.AreaOfEffect).Where(s => s.Unit.IsDead == false))
            {
                hit = true;
                splashTarget.DefendStatusSpell(spell, this);
            }

            State.GameManager.SoundManager.PlaySpellHit(spell, targetArea);
        }

        if (Unit.TraitBoosts.SpellAttacks > 1)
        {
            int movementFraction = 1 + MaxMovement() / Unit.TraitBoosts.SpellAttacks;
            if (Movement > movementFraction)
                Movement -= movementFraction;
            else
                Movement = 0;
        }
        else
            Movement = 0;

        return hit;
    }

    internal bool CastBind(Actor_Unit t)
    {
        var spell = SpellList.Bind;
        if (t.Unit.Type != UnitType.Summon) return false;
        var binder = TacticalUtilities.Units.Where(a => a.Unit.BoundUnit?.Unit == t.Unit).FirstOrDefault();
        if (Equals(binder?.Unit.FixedSide, Unit.FixedSide)) return false;
        if (Unit.SpendMana(spell.ManaCost) == false && spell.IsFree != true) return false;


        if (binder != null)
        {
            if (binder.Unit.GetStat(Stat.Mind) > Unit.GetStat(Stat.Mind))
            {
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{Unit.Name}</b> tries to bind <b>{LogUtilities.ApostrophizeWithOrWithoutS(binder.Unit.Name)}</b> bound summon, <b>{t.Unit.Name}</b>, but <b>{LogUtilities.ApostrophizeWithOrWithoutS(binder.Unit.Name)}</b> magic is stronger");
            }
            else if (binder.Unit.GetStat(Stat.Mind) < Unit.GetStat(Stat.Mind))
            {
                State.GameManager.SoundManager.PlaySpellCast(spell, this);
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"With {LogUtilities.GPPHis(Unit)} superior magic <b>{Unit.Name}</b> wrests control over the summoned {InfoPanel.RaceSingular(t.Unit)} from <b>{binder.Unit.Name}</b>.");
                binder.Unit.BoundUnit = null;
                Unit.BoundUnit = t;

                if (!Equals(t.Unit.Side, Unit.Side)) State.GameManager.TacticalMode.SwitchAlignment(t);
                if (!t.Unit.HasTrait(TraitType.Untamable)) t.Unit.FixedSide = Unit.FixedSide;
                t.Movement = t.CurrentMaxMovement();
                var actorCharm = Unit.GetStatusEffect(StatusEffectType.Charmed) ?? Unit.GetStatusEffect(StatusEffectType.Hypnotized);
                if (actorCharm != null)
                {
                    t.Unit.ApplyStatusEffect(StatusEffectType.Charmed, actorCharm.Strength, actorCharm.Duration);
                }
            }
            else
            {
                // TODO this can't work with the changes to Sides and Races as it creates integer sides. Can be fixed later
                /*
                int outcome = State.Rand.Next(4);
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{Unit.Name}</b> and <b>{binder.Unit.Name}</b> both exert their magic for control over the summoned <b>{t.Unit.Name}</b>, but they are evenly matched! The energy crackles...");
                if (outcome == 3)
                {
                    State.GameManager.SoundManager.PlayMisc("unbound", this);
                    var obj = Object.Instantiate(State.GameManager.TacticalEffectPrefabList.ShunGokuSatsu);
                    obj.transform.SetPositionAndRotation(new Vector3(t.Position.x, t.Position.y), new Quaternion());
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"Suddenly, there is a flash of light and both casters stagger for a moment. What happened?.");
                    t.Unit.Type = UnitType.Adventurer;
                    binder.Unit.BoundUnit = null;
                    t.Unit.Name += " The Unbound";
                    t.Unit.AddPermanentTrait(Traits.PeakCondition);
                    int unusedSide = 703;
                    while (State.World.AllActiveEmpires?.Any(emp => emp.Side == unusedSide) ?? false)
                    {
                        unusedSide++;
                    }
                    t.Unit.FixedSide = unusedSide;
                    State.GameManager.TacticalMode.SwitchAlignment(t);
                    t.Unit.AddPermanentTrait(Traits.Untamable);
                    t.Unit.AddPermanentTrait(Traits.Large);
                    t.Unit.GiveRawExp((int)(binder.Unit.Experience * 0.50 + Unit.Experience * 0.50));
                    StrategicUtilities.SpendLevelUps(t.Unit);
                    t.Unit.Health = t.Unit.MaxHealth;
                    t.Unit.RestoreMana(t.Unit.MaxMana);
                    if (binder.sidesAttackedThisBattle == null) binder.sidesAttackedThisBattle = new List<Side>();
                    binder.sidesAttackedThisBattle.Add(unusedSide);
                    if (this.sidesAttackedThisBattle == null) this.sidesAttackedThisBattle = new List<Side>();
                    this.sidesAttackedThisBattle.Add(unusedSide);
                }
                else if (outcome == 2)
                {
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"...But it looks like the Binding spell worked after all.");
                    State.GameManager.SoundManager.PlaySpellCast(spell, this);
                    binder.Unit.BoundUnit = null;
                    Unit.BoundUnit = t;

                    if (t.Unit.Side != Unit.Side) State.GameManager.TacticalMode.SwitchAlignment(t);
                    if (!t.Unit.HasTrait(Traits.Untamable))
                        t.Unit.FixedSide = Unit.FixedSide;
                    t.Movement = t.CurrentMaxMovement();
                    var actorCharm = Unit.GetStatusEffect(StatusEffectType.Charmed) ?? Unit.GetStatusEffect(StatusEffectType.Hypnotized);
                    if (actorCharm != null)
                    {
                        t.Unit.ApplyStatusEffect(StatusEffectType.Charmed, actorCharm.Strength, actorCharm.Duration);
                    }
                }
                else if (outcome == 1)
                {
                    State.GameManager.SoundManager.PlaySpellCast(spell, this);
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"...But it looks nothing's changed...");
                }
                else if (outcome == 0)
                {
                    State.GameManager.SoundManager.PlayMisc("unbound", this);
                    var obj = Object.Instantiate(State.GameManager.TacticalEffectPrefabList.ShunGokuSatsu);
                    obj.transform.SetPositionAndRotation(new Vector3(t.Position.x, t.Position.y), new Quaternion());
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"Suddenly, there is a flash of light and both casters stagger for a moment. What happened?.");
                    t.Unit.Type = UnitType.Adventurer;
                    t.Surrendered = true;
                    t.Damage(9999999, true, true);
                    t.Visible = false;
                    t.Unit.Name += " The Banished";
                }
                */
            }
        }
        else
        {
            State.GameManager.SoundManager.PlaySpellCast(spell, this);

            Unit.BoundUnit = t;

            if (!Equals(t.Unit.Side, Unit.Side)) State.GameManager.TacticalMode.SwitchAlignment(t);
            if (!t.Unit.HasTrait(TraitType.Untamable)) t.Unit.FixedSide = Unit.FixedSide;
            t.Movement = t.CurrentMaxMovement();
            var actorCharm = Unit.GetStatusEffect(StatusEffectType.Charmed) ?? Unit.GetStatusEffect(StatusEffectType.Hypnotized);
            if (actorCharm != null)
            {
                t.Unit.ApplyStatusEffect(StatusEffectType.Charmed, actorCharm.Strength, actorCharm.Duration);
            }

            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{Unit.Name}</b> has bound <b>{t.Unit.Name}</b> to {LogUtilities.GPPHis(Unit)} will.");
        }

        if (Unit.TraitBoosts.SpellAttacks > 1)
        {
            int movementFraction = 1 + MaxMovement() / Unit.TraitBoosts.SpellAttacks;
            if (Movement > movementFraction)
                Movement -= movementFraction;
            else
                Movement = 0;
        }
        else
            Movement = 0;

        return true;
    }

    internal bool SummonBound(Vec2i l)
    {
        var spell = SpellList.Bind;
        if (Unit.BoundUnit == null) return false;

        if (TacticalUtilities.Units.Contains(Unit.BoundUnit) && !Unit.BoundUnit.Unit.IsDead) return false;
        if (Unit.SpendMana(spell.ManaCost) == false && spell.IsFree != true) return false;

        State.GameManager.SoundManager.PlaySpellCast(SpellList.Summon, this);

        if (TacticalUtilities.Units.Contains(Unit.BoundUnit))
        {
            TacticalUtilities.Reanimate(l, Unit.BoundUnit, Unit);
            Unit.BoundUnit.Unit.Health = Unit.BoundUnit.Unit.MaxHealth;
        }
        else
        {
            StrategicUtilities.SpendLevelUps(Unit.BoundUnit.Unit);
            var target = State.GameManager.TacticalMode.AddUnitToBattle(Unit.BoundUnit.Unit, l);
            target.Visible = true;
            target.Targetable = true;
            target.SelfPrey = null;
            target.Surrendered = false;
            target.Unit.Health = target.Unit.MaxHealth;
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{Unit.Name}</b> re-summoned <b>{Unit.BoundUnit.Unit.Name}</b>.");
        }

        if (Unit.TraitBoosts.SpellAttacks > 1)
        {
            int movementFraction = 1 + MaxMovement() / Unit.TraitBoosts.SpellAttacks;
            if (Movement > movementFraction)
                Movement -= movementFraction;
            else
                Movement = 0;
        }
        else
            Movement = 0;

        return true;
    }

    internal void ShareTrait(TraitType traitType, TraitType maxTraitType = TraitType.Infiltrator)
    {
        if (traitType < maxTraitType && !TraitsMethods.IsRaceModifying(traitType))
        {
            if (!Unit.HasTrait(traitType)) Unit.AddSharedTrait(traitType);
        }
    }

    public bool ChangeRaceAny(Unit template, bool permanent, bool isPrey)
    {
        if (Equals(Unit.HiddenRace, Unit.Race))
        {
            TacticalGraphicalEffects.CreateSmokeCloud(Position, Unit.GetScale() / 2);
            Unit.HideRace(template.Race, template);
            foreach (TraitType trait in template.GetTraits)
            {
                if ((!Unit.HasTrait(trait) || Unit.HasSharedTrait(trait)) && !TraitsMethods.IsRaceModifying(trait))
                    if (permanent)
                        Unit.AddPermanentTrait(trait);
                    else
                        ShareTrait(trait, TraitsMethods.LastTrait());
            }

            AnimationController = new AnimationController();
            Unit.ReloadTraits();
            Unit.InitializeTraits();
            ReloadSpellTraits();
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{Unit.Name} shifted form to resemble {template.Name}");
            Unit.FixedSide = Unit.Side;
            Unit.Side = template.Side;
            Unit.hiddenFixedSide = true;
            PredatorComponent?.UpdateFullness();
            return true;
        }

        return false;
    }

    public void ChangeRacePrey()
    {
        if (Equals(Unit.HiddenRace, Unit.Race))
        {
            bool isDead = true;
            if (Unit.HasTrait(TraitType.GreaterChangeling))
            {
                isDead = false;
            }

            PredatorComponent?.ChangeRaceAuto(isDead, true);
        }
    }

    public void RevertRace()
    {
        if (!Equals(Unit.HiddenRace, Unit.Race))
        {
            TacticalGraphicalEffects.CreateSmokeCloud(Position, Unit.GetScale() / 2);
            Unit.UnhideRace();
            Unit.SpawnRace = Race.TrueNone;
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{Unit.Name} shifted back to normal");
            Unit.Side = Unit.FixedSide;
            Unit.FixedSide = Side.TrueNoneSide;
            Unit.hiddenFixedSide = false;
            PredatorComponent?.ResetTemplate();
            Unit.ResetPersistentSharedTraits();
            AnimationController = new AnimationController();
            Unit.ReloadTraits();
            Unit.InitializeTraits();
            ReloadSpellTraits();
            PredatorComponent?.UpdateFullness();
        }
    }

    internal void AddCorruption(int amount, Side side)
    {
        if (!Unit.HasTrait(TraitType.Corruption))
        {
            Corruption += amount;
            if (Corruption >= Unit.GetStatTotal() + Unit.GetStat(Stat.Will))
            {
                Unit.AddPermanentTrait(TraitType.Corruption);
                Corruption = 0;
                if (!Unit.HasTrait(TraitType.Untamable)) Unit.FixedSide = side;
                Unit.hiddenFixedSide = true;
                SidesAttackedThisBattle = new List<Side>();
            }
        }
        else
            Corruption = 0;
    }

    internal void AddPossession(Actor_Unit possessor)
    {
        if (!Equals(Unit.FixedSide, possessor.Unit.FixedSide))
        {
            Possessed = Possessed + possessor.Unit.GetStatTotal() + possessor.Unit.GetStat(Stat.Mind);
        }

        CheckPossession(possessor);
    }

    internal void RemovePossession(Actor_Unit possessor)
    {
        Possessed = Possessed - possessor.Unit.GetStatTotal() - possessor.Unit.GetStat(Stat.Mind);
        if (Possessed <= 0) Possessed = 0;
        CheckPossession(possessor);
    }

    internal bool CheckPossession(Actor_Unit possessor)
    {
        if (Possessed + Corruption >= Unit.GetStatTotal() + Unit.GetStat(Stat.Will))
        {
            if (Unit.Controller != possessor.Unit)
            {
                if (!Unit.HasTrait(TraitType.Untamable)) Unit.Controller = possessor.Unit;
                Unit.hiddenFixedSide = true;
                SidesAttackedThisBattle = new List<Side>();
            }

            return true;
        }
        else
        {
            if (Unit.Controller == possessor.Unit)
            {
                Unit.Controller = null;
                Unit.hiddenFixedSide = false;
            }
        }

        return false;
    }

    internal void Shapeshift(Unit shape)
    {
        if (Unit == shape) return;
        TacticalGraphicalEffects.CreateSmokeCloud(Position, Unit.GetScale() / 2);
        Unit.UpdateShapeExpAndItems();
        MiscUtilities.DelayedInvoke(() =>
        {
            shape.ShifterShapes = Unit.ShifterShapes;
            shape.Side = Unit.Side;
            Unit = shape;
            Unit.hiddenFixedSide = false;

            UnitSprite.UpdateSprites(this, true);
            AnimationController = new AnimationController();
            Unit.ReloadTraits();
            Unit.InitializeTraits();
        }, 0.4f);
    }
}