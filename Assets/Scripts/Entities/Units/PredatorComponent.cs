using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

internal interface IVoreCallback
{
    int ProcessingPriority { get; } //lower runs first

    bool IsPredTrait { get; } //is the trait triggered from the pred or the prey side

    /*
     * use the boolean return to skip lower priority traits, eg. if the unit has already
     *  been removed and as part of a trait like digestive conversion, it shouldn't run the digestive rebirth trait it stole with extraction
     */
    bool OnSwallow(Prey preyUnit, ActorUnit predUnit, PreyLocation location); //any time prey is added (living or dead)
    bool OnRemove(Prey preyUnit, ActorUnit predUnit, PreyLocation location); //any time prey is removed (living or dead)
    bool OnDigestion(Prey preyUnit, ActorUnit predUnit, PreyLocation location); //any time living prey is digested but won't die yet
    bool OnFinishDigestion(Prey preyUnit, ActorUnit predUnit, PreyLocation location); //right before unit dies
    bool OnDigestionKill(Prey preyUnit, ActorUnit predUnit, PreyLocation location); //right after unit dies
    bool OnAbsorption(Prey preyUnit, ActorUnit predUnit, PreyLocation location); //any time a dead unit is being absorbed
    bool OnFinishAbsorption(Prey preyUnit, ActorUnit predUnit, PreyLocation location); //unit is done absorbing, but not removed yet
}

internal interface IVoreRestrictions
{
    /* Notes:
     * CheckVore needs to handle a null target
     * Only specialVore types can be affected, disableing Vore overall will take more work
     */
    bool CheckVore(ActorUnit actor, ActorUnit target, PreyLocation location);
}

public enum PreyLocation
{
    Breasts,
    Balls,
    Stomach,
    Stomach2,
    Womb,
    Tail,
    Anal,
    LeftBreast,
    RightBreast
}

internal static class PreyLocationMethods
{
    public static bool IsGenital(PreyLocation location)
    {
        switch (location)
        {
            case PreyLocation.Womb:
            case PreyLocation.Balls:
                return true;
            default:
                return false;
        }
    }
}

public class PredatorComponent
{
    [OdinSerialize]
    private List<Prey> _prey;

    private List<Prey> Prey { get => _prey; set => _prey = value; }

    [OdinSerialize]
    private List<Prey> _womb;

    private List<Prey> Womb { get => _womb; set => _womb = value; }

    [OdinSerialize]
    private List<Prey> _breasts;

    private List<Prey> Breasts { get => _breasts; set => _breasts = value; }

    [OdinSerialize]
    private List<Prey> _leftBreast;

    private List<Prey> LeftBreast { get => _leftBreast; set => _leftBreast = value; }

    [OdinSerialize]
    private List<Prey> _rightBreast;

    private List<Prey> RightBreast { get => _rightBreast; set => _rightBreast = value; }

    [OdinSerialize]
    private List<Prey> _balls;

    private List<Prey> Balls { get => _balls; set => _balls = value; }

    [OdinSerialize]
    private List<Prey> _stomach;

    private List<Prey> Stomach { get => _stomach; set => _stomach = value; }

    [OdinSerialize]
    private List<Prey> _stomach2;

    private List<Prey> Stomach2 { get => _stomach2; set => _stomach2 = value; }

    [OdinSerialize]
    private List<Prey> _tail;

    private List<Prey> Tail { get => _tail; set => _tail = value; }

    [OdinSerialize]
    private List<Prey> _deadPrey;

    private List<Prey> DeadPrey { get => _deadPrey; set => _deadPrey = value; }

    private Transition _stomachTransition;
    private Transition _ballsTransition;
    private Transition _leftBreastTransition;
    private Transition _rightBreastTransition;

    public static PreyLocation[] PreyLocationOrder =
    {
        PreyLocation.Stomach,
        PreyLocation.Stomach2,
        PreyLocation.Womb,
        PreyLocation.Balls,
        PreyLocation.Breasts,
        PreyLocation.LeftBreast,
        PreyLocation.RightBreast,
        PreyLocation.Tail,
        PreyLocation.Anal
    };

    internal struct Transition
    {
        internal float TransitionTime;
        internal float TransitionLength;
        internal float TransitionStart;
        internal float TransitionEnd;

        public Transition(float transitionLength, float transitionStart, float transitionEnd)
        {
            TransitionTime = 0;
            this.TransitionLength = transitionLength;
            this.TransitionStart = transitionStart;
            this.TransitionEnd = transitionEnd;
        }
    }

    [OdinSerialize]
    private float _visibleFullness;

    public float VisibleFullness { get => _visibleFullness; set => _visibleFullness = value; }

    [OdinSerialize]
    private float _fullness;

    public float Fullness { get => _fullness; set => _fullness = value; }

    [OdinSerialize]
    private float _breastFullness;

    public float BreastFullness { get => _breastFullness; set => _breastFullness = value; }

    [OdinSerialize]
    private float _ballsFullness;

    public float BallsFullness { get => _ballsFullness; set => _ballsFullness = value; }

    [OdinSerialize]
    private float _tailFullness;

    public float TailFullness { get => _tailFullness; set => _tailFullness = value; }

    [OdinSerialize]
    private float _stomach2NdFullness;

    public float Stomach2NdFullness { get => _stomach2NdFullness; set => _stomach2NdFullness = value; } // A second stomach fullness used for units with the trait DualStomach.

    [OdinSerialize]
    private float _combinedStomachFullness;

    public float CombinedStomachFullness { get => _combinedStomachFullness; set => _combinedStomachFullness = value; } // Used when DualStomach unit has no separate sprites for second one or they are turned off.

    [OdinSerialize]
    private float _leftBreastFullness;

    public float LeftBreastFullness { get => _leftBreastFullness; set => _leftBreastFullness = value; }

    [OdinSerialize]
    private float _rightBreastFullness;

    public float RightBreastFullness { get => _rightBreastFullness; set => _rightBreastFullness = value; }

    /// <summary>
    ///     Only includes the stomach, and not the womb
    /// </summary>
    [OdinSerialize]
    private float _exclusiveStomachFullness;

    public float ExclusiveStomachFullness { get => _exclusiveStomachFullness; set => _exclusiveStomachFullness = value; }

    /// <summary>
    ///     Only includes the womb and not the stomach
    /// </summary>
    [OdinSerialize]
    private float _wombFullness;

    public float WombFullness { get => _wombFullness; set => _wombFullness = value; }

    [OdinSerialize]
    private ActorUnit _actor;

    [OdinSerialize]
    private Unit _unit;

    private Unit Unit { get => _unit; set => _unit = value; }

    [OdinSerialize]
    private int _birthStatBoost;

    public int BirthStatBoost { get => _birthStatBoost; set => _birthStatBoost = value; }

    [OdinSerialize]
    private Prey _template = null;

    internal Prey Template { get => _template; set => _template = value; }


    [OdinSerialize]
    private List<IVoreRestrictions> _voreRestrictions = null;

    internal List<IVoreRestrictions> VoreRestrictions
    {
        get
        {
            if (_voreRestrictions == null)
            {
                _voreRestrictions = new List<IVoreRestrictions>();
                if (RaceFuncs.GetRace(Unit.Race) is IVoreRestrictions restriction1) _voreRestrictions.Add(restriction1);
                foreach (TraitType trait in Unit.GetTraits.Where(t => TraitList.GetTrait(t) != null && TraitList.GetTrait(t) is IVoreRestrictions))
                {
                    IVoreRestrictions restriction = (IVoreRestrictions)TraitList.GetTrait(trait);
                    _voreRestrictions.Add(restriction);
                }
            }

            return _voreRestrictions;
        }
        set
        {
            if (value == null) _voreRestrictions = null;
        }
    }

    internal bool CanVore(PreyLocation location, ActorUnit preyTarget = null)
    {
        if (!Unit.CanVore(location)) return false;
        foreach (IVoreRestrictions callback in VoreRestrictions)
            if (!callback.CheckVore(_actor, preyTarget, location))
                return false;
        return true;
    }

    internal bool CanUnbirth(ActorUnit preyTarget = null)
    {
        if (!Unit.CanUnbirth) return false;
        foreach (IVoreRestrictions callback in VoreRestrictions)
            if (!callback.CheckVore(_actor, preyTarget, PreyLocation.Womb))
                return false;
        return true;
    }

    internal bool CanCockVore(ActorUnit preyTarget = null)
    {
        if (!Unit.CanCockVore) return false;
        foreach (IVoreRestrictions callback in VoreRestrictions)
            if (!callback.CheckVore(_actor, preyTarget, PreyLocation.Balls))
                return false;
        return true;
    }

    internal bool CanBreastVore(ActorUnit preyTarget = null)
    {
        if (!Unit.CanBreastVore) return false;
        foreach (IVoreRestrictions callback in VoreRestrictions)
            if (!callback.CheckVore(_actor, preyTarget, PreyLocation.Breasts))
                return false;
        return true;
    }

    internal bool CanAnalVore(ActorUnit preyTarget = null)
    {
        if (!Unit.CanAnalVore) return false;
        foreach (IVoreRestrictions callback in VoreRestrictions)
            if (!callback.CheckVore(_actor, preyTarget, PreyLocation.Anal))
                return false;
        return true;
    }

    internal bool CanTailVore(ActorUnit preyTarget = null)
    {
        if (!Unit.CanTailVore) return false;
        foreach (IVoreRestrictions callback in VoreRestrictions)
            if (!callback.CheckVore(_actor, preyTarget, PreyLocation.Tail))
                return false;
        return true;
    }

    public int AlivePrey { get; set; }

    private List<IVoreCallback> PopulateCallbacks(Prey preyUnit)
    {
        List<IVoreCallback> callbacks = new List<IVoreCallback>();
        foreach (TraitType trait in Unit.GetTraits.Where(t => TraitList.GetTrait(t) != null && TraitList.GetTrait(t) is IVoreCallback))
        {
            IVoreCallback callback = (IVoreCallback)TraitList.GetTrait(trait);
            if (callback.IsPredTrait == true) callbacks.Add(callback);
        }

        foreach (TraitType trait in preyUnit.Unit.GetTraits.Where(t => TraitList.GetTrait(t) != null && TraitList.GetTrait(t) is IVoreCallback))
        {
            IVoreCallback callback = (IVoreCallback)TraitList.GetTrait(trait);
            if (callback.IsPredTrait == false) callbacks.Add(callback);
        }

        //future idea: create status effect classes that can ALSO implement vore related callbacks
        return callbacks;
    }

    // lumps prey in similar locations together

    public int PreyNearLocation(PreyLocation location, bool alive)
    {
        int prey = 0;
        switch (location)
        {
            case PreyLocation.Balls:
                foreach (Prey unit in Balls)
                {
                    if (unit.Unit.IsDead != alive) prey += 1;
                }

                break;
            case PreyLocation.Stomach:
            case PreyLocation.Stomach2:
            case PreyLocation.Womb:
            case PreyLocation.Anal:
            case PreyLocation.Tail:
                foreach (Prey unit in Stomach)
                {
                    if (unit.Unit.IsDead != alive) prey += 1;
                }

                foreach (Prey unit in Stomach2)
                {
                    if (unit.Unit.IsDead != alive) prey += 1;
                }

                foreach (Prey unit in Womb)
                {
                    if (unit.Unit.IsDead != alive) prey += 1;
                }

                break;
            case PreyLocation.LeftBreast:
                foreach (Prey unit in LeftBreast)
                {
                    if (unit.Unit.IsDead != alive) prey += 1;
                }

                break;
            case PreyLocation.RightBreast:
                foreach (Prey unit in RightBreast)
                {
                    if (unit.Unit.IsDead != alive) prey += 1;
                }

                break;
            case PreyLocation.Breasts:
                foreach (Prey unit in Breasts)
                {
                    if (unit.Unit.IsDead != alive) prey += 1;
                }

                break;
        }

        return prey;
    }

    public int PreyInLocation(PreyLocation location, bool alive)
    {
        int prey = 0;
        switch (location)
        {
            case PreyLocation.Balls:
                foreach (Prey unit in Balls)
                {
                    if (unit.Unit.IsDead != alive) prey += 1;
                }

                break;
            case PreyLocation.Stomach:
            case PreyLocation.Anal:
                foreach (Prey unit in Stomach)
                {
                    if (unit.Unit.IsDead != alive) prey += 1;
                }

                break;
            case PreyLocation.Stomach2:
                foreach (Prey unit in Stomach2)
                {
                    if (unit.Unit.IsDead != alive) prey += 1;
                }

                break;
            case PreyLocation.Tail:
                foreach (Prey unit in Tail)
                {
                    if (unit.Unit.IsDead != alive) prey += 1;
                }

                break;
            case PreyLocation.Womb:
                foreach (Prey unit in Womb)
                {
                    if (unit.Unit.IsDead != alive) prey += 1;
                }

                break;
            case PreyLocation.LeftBreast:
                foreach (Prey unit in LeftBreast)
                {
                    if (unit.Unit.IsDead != alive) prey += 1;
                }

                break;
            case PreyLocation.RightBreast:
                foreach (Prey unit in RightBreast)
                {
                    if (unit.Unit.IsDead != alive) prey += 1;
                }

                break;
            case PreyLocation.Breasts:
                foreach (Prey unit in Breasts)
                {
                    if (unit.Unit.IsDead != alive) prey += 1;
                }

                break;
        }

        return prey;
    }


    internal void UpdateAlivePrey()
    {
        AlivePrey = 0;
        foreach (Prey unit in Prey)
        {
            if (unit.Unit.IsDead == false) AlivePrey += 1;
        }
    }

    internal List<Prey> GetDirectPrey()
    {
        return Prey;
    }

    /// <summary>
    ///     Gets all prey in this unit, including all sub prey and on down
    /// </summary>
    internal List<Prey> GetAllPrey()
    {
        List<Prey> allPrey = new List<Prey>();
        List<Prey> tempPrey = new List<Prey>();
        tempPrey.AddRange(Prey);
        int counter = 0;
        while (tempPrey.Count > 0)
        {
            counter++;
            if (counter > 200)
            {
                Debug.LogWarning("Error Gathering all prey");
                break;
            }

            if (tempPrey[0].SubPrey != null) tempPrey.AddRange(tempPrey[0].SubPrey);
            allPrey.Add(tempPrey[0]);
            tempPrey.Remove(tempPrey[0]);
        }

        return allPrey;
    }

    public PredatorComponent(ActorUnit actor, Unit unit)
    {
        this._actor = actor;
        this.Unit = unit;
        Fullness = 0.0F;
        Prey = new List<Prey>();
        Womb = new List<Prey>();
        Breasts = new List<Prey>();
        Balls = new List<Prey>();
        Stomach = new List<Prey>();
        Stomach2 = new List<Prey>();
        Tail = new List<Prey>();
        LeftBreast = new List<Prey>();
        RightBreast = new List<Prey>();
        DeadPrey = new List<Prey>();
        BirthStatBoost = 0;
    }

    public void UpdateVersion()
    {
        if (Tail == null) Tail = new List<Prey>();
        if (Stomach2 == null) Stomach2 = new List<Prey>();
        if (LeftBreast == null) LeftBreast = new List<Prey>();
        if (RightBreast == null) RightBreast = new List<Prey>();
    }

    public int PreyCount => Prey.Count;

    public int DigestingUnitCount
    {
        get
        {
            if (Unit.HasTrait(TraitType.Endosoma)) return Prey.Where(s => !Equals(_actor.Unit.GetApparentSide(s.Unit), s.Unit.FixedSide) || s.Unit.IsDead).Count();
            return Prey.Count;
        }
    }

    public bool OnlyOnePreyAndLiving() => Prey.Count == 1 && Prey[0].Unit.IsDead == false;

    public bool IsActorInPrey(ActorUnit actor)
    {
        foreach (Prey p in Prey)
        {
            if (p.Actor == actor) return true;
        }

        return false;
    }

    /// <summary>
    ///     Checks if a specific Actor_Unit is in the specified part of the predator.
    /// </summary>
    /// <param name="unit">The Actor_Unit being looked for.</param>
    /// <param name="locations">The specified part of the predator to be checked. Can specify multiple.</param>
    /// <returns></returns>
    public bool IsUnitInPrey(ActorUnit unit, params PreyLocation[] locations)
    {
        if (locations.Contains(PreyLocation.Stomach))
            foreach (Prey p in Stomach)
                if (p.Actor == unit)
                    return true;
        if (locations.Contains(PreyLocation.Stomach2))
            foreach (Prey p in Stomach2)
                if (p.Actor == unit)
                    return true;
        if (locations.Contains(PreyLocation.Balls))
            foreach (Prey p in Balls)
                if (p.Actor == unit)
                    return true;
        if (locations.Contains(PreyLocation.Breasts))
            foreach (Prey p in Breasts)
                if (p.Actor == unit)
                    return true;
        if (locations.Contains(PreyLocation.Womb))
            foreach (Prey p in Womb)
                if (p.Actor == unit)
                    return true;
        if (locations.Contains(PreyLocation.Tail))
            foreach (Prey p in Tail)
                if (p.Actor == unit)
                    return true;
        if (locations.Contains(PreyLocation.LeftBreast))
            foreach (Prey p in LeftBreast)
                if (p.Actor == unit)
                    return true;
        if (locations.Contains(PreyLocation.RightBreast))
            foreach (Prey p in RightBreast)
                if (p.Actor == unit)
                    return true;

        return false;
    }

    /// <summary>
    /// Used to check for whether a unit of specified race is present in eaten prey.
    /// </summary>
    /// <param name="race">The race which is being looked for.</param>
    /// <returns></returns>
    //public bool IsUnitOfSpecificationInPrey(Race race)
    //{
    //    foreach (Prey p in prey)
    //    {
    //        if (p.Unit.Race == race)
    //            return true;
    //    }
    //    return false;
    //}

    /// <summary>
    ///     Used to check for whether a unit of specified race is present in eaten prey and either alive or dead depending on
    ///     the boolean given.
    /// </summary>
    /// <param name="race">The race which is being looked for.</param>
    /// <param name="alive">True = Checking for living creatures. False = Checking for dead creatures.</param>
    /// <returns></returns>
    public bool IsUnitOfSpecificationInPrey(Race race, bool alive)
    {
        if (Config.RaceSpecificVoreGraphicsDisabled) return false;
        foreach (Prey p in Prey)
        {
            if (Equals(p.Unit.Race, race))
                if (p.Unit.IsDead != alive)
                    return true;
        }

        return false;
    }

    /// <summary>
    ///     Used to check for whether a unit of specified race is present inside specified part of the pred and either alive or
    ///     dead depending on the boolean given.
    /// </summary>
    /// <param name="race">The race which is being looked for.</param>
    /// <param name="alive">True = Checking for living creatures. False = Checking for dead creatures.</param>
    /// <param name="locations">The specified part of the predator to be checked. Can specify multiple.</param>
    /// <returns></returns>
    public bool IsUnitOfSpecificationInPrey(Race race, bool alive, params PreyLocation[] locations)
    {
        if (Config.RaceSpecificVoreGraphicsDisabled) return false;
        if (locations.Contains(PreyLocation.Stomach))
            foreach (Prey p in Stomach)
            {
                if (Equals(p.Unit.Race, race))
                    if (p.Unit.IsDead != alive)
                        return true;
            }

        if (locations.Contains(PreyLocation.Balls))
            foreach (Prey p in Balls)
            {
                if (Equals(p.Unit.Race, race))
                    if (p.Unit.IsDead != alive)
                        return true;
            }

        if (locations.Contains(PreyLocation.Womb))
            foreach (Prey p in Womb)
            {
                if (Equals(p.Unit.Race, race))
                    if (p.Unit.IsDead != alive)
                        return true;
            }

        if (locations.Contains(PreyLocation.Breasts))
            foreach (Prey p in Breasts)
            {
                if (Equals(p.Unit.Race, race))
                    if (p.Unit.IsDead != alive)
                        return true;
            }

        if (locations.Contains(PreyLocation.Tail))
            foreach (Prey p in Tail)
            {
                if (Equals(p.Unit.Race, race))
                    if (p.Unit.IsDead != alive)
                        return true;
            }

        if (locations.Contains(PreyLocation.Stomach2))
            foreach (Prey p in Stomach2)
            {
                if (Equals(p.Unit.Race, race))
                    if (p.Unit.IsDead != alive)
                        return true;
            }

        if (locations.Contains(PreyLocation.LeftBreast))
            foreach (Prey p in LeftBreast)
            {
                if (Equals(p.Unit.Race, race))
                    if (p.Unit.IsDead != alive)
                        return true;
            }

        if (locations.Contains(PreyLocation.RightBreast))
            foreach (Prey p in RightBreast)
            {
                if (Equals(p.Unit.Race, race))
                    if (p.Unit.IsDead != alive)
                        return true;
            }

        return false;
    }

    public bool IsUnitOfSpecificationInPrey(Race race, params PreyLocation[] locations)
    {
        if (Config.RaceSpecificVoreGraphicsDisabled) return false;
        if (locations.Contains(PreyLocation.Stomach))
            foreach (Prey p in Stomach)
            {
                if (Equals(p.Unit.Race, race)) return true;
            }

        if (locations.Contains(PreyLocation.Balls))
            foreach (Prey p in Balls)
            {
                if (Equals(p.Unit.Race, race)) return true;
            }

        if (locations.Contains(PreyLocation.Womb))
            foreach (Prey p in Womb)
            {
                if (Equals(p.Unit.Race, race)) return true;
            }

        if (locations.Contains(PreyLocation.Breasts))
            foreach (Prey p in Breasts)
            {
                if (Equals(p.Unit.Race, race)) return true;
            }

        if (locations.Contains(PreyLocation.Tail))
            foreach (Prey p in Tail)
            {
                if (Equals(p.Unit.Race, race)) return true;
            }

        if (locations.Contains(PreyLocation.Stomach2))
            foreach (Prey p in Stomach2)
            {
                if (Equals(p.Unit.Race, race)) return true;
            }

        if (locations.Contains(PreyLocation.LeftBreast))
            foreach (Prey p in LeftBreast)
            {
                if (Equals(p.Unit.Race, race)) return true;
            }

        if (locations.Contains(PreyLocation.RightBreast))
            foreach (Prey p in RightBreast)
            {
                if (Equals(p.Unit.Race, race)) return true;
            }

        return false;
    }

    public void FreeAnyAlivePrey()
    {
        List<Prey> preyUnits = new List<Prey>();
        preyUnits.AddRange(Prey);
        while (preyUnits.Any())
        {
            if (preyUnits[0].Unit.IsDead == false)
            {
                State.GameManager.TacticalMode.TacticalStats.RegisterFreed(Unit.Side);
                TacticalUtilities.Log.RegisterFreed(Unit, preyUnits[0].Unit, Location(preyUnits[0]));
                preyUnits[0].Predator.PredatorComponent.FreePrey(preyUnits[0], true);
                preyUnits.RemoveAt(0);
            }
            else
            {
                if (preyUnits[0].SubPrey != null)
                    foreach (Prey subUnit in preyUnits[0].SubPrey)
                    {
                        preyUnits.Add(subUnit);
                    }

                preyUnits.RemoveAt(0);
            }
        }
    }

    internal void PurgePrey()
    {
        FreeAnyAlivePrey();
        Prey.Clear();
        Womb.Clear();
        Breasts.Clear();
        LeftBreast.Clear();
        RightBreast.Clear();
        Balls.Clear();
        Stomach.Clear();
        Stomach2.Clear();
        Tail.Clear();
    }

    public Vec2I GetCurrentLocation()
    {
        ActorUnit located = _actor;
        for (int i = 0; i < 200; i++)
        {
            ActorUnit newActor = TacticalUtilities.FindPredator(located);
            if (newActor != null)
            {
                located = newActor;
            }
            else
                break;
        }

        return located?.Position;
    }

    public float GetBulkOfPrey(int count = 0)
    {
        count++;
        if (count > 300)
        {
            Debug.LogWarning("Infinite prey chain seemingly detected, handling it but you should check out the source");
            return 0;
        }

        float ret = 0;
        for (int i = 0; i < Prey.Count; i++)
        {
            if (Prey[i].Unit == _actor.Unit)
            {
                Debug.Log("Prey inside of itself!");
                FreePrey(Prey[i], true);
                continue;
            }

            ret += Prey[i].Actor.Bulk(count);
        }

        return ret;
    }

    public float TotalCapacity()
    {
        float c = State.RaceSettings.GetStomachSize(Unit.Race);

        c *= Unit.GetStat(Stat.Stomach) / 12f * Unit.TraitBoosts.CapacityMult;

        //c *= unit.GetScale(3);  It may be more realistic, but having huge resulting in 81 times the base stomach capacity is just overkill

        return c;
    }

    public float FreeCap()
    {
        float totalBulk = 0;
        for (int i = 0; i < Prey.Count; i++)
        {
            totalBulk += Prey[i].Actor.Bulk();
        }

        return TotalCapacity() - totalBulk;
    }

    /// <summary>
    ///     Designed to be used for manual regurgitation.
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    internal Prey FreeRandomPreyToSquare(Vec2I location)
    {
        if (_actor.Position.GetNumberOfMovesDistance(location) > 1) return null;
        if (_actor.Movement == 0) return null;
        if (_actor.Unit.Predator == false) return null;
        var alives = Prey.Where(s => s.Unit.IsDead == false && !TacticalUtilities.TreatAsHostile(_actor, s.Actor)).ToArray();
        if (alives.Length == 0) alives = Prey.Where(s => s.Unit.IsDead == false).ToArray();
        if (alives.Length == 0) return null;
        var target = alives[State.Rand.Next(alives.Length)];
        if (TacticalUtilities.OpenTile(location, target.Actor) == false) return null;
        if (!Unit.HasTrait(TraitType.Endosoma) || !Equals(target.Unit.FixedSide, Unit.GetApparentSide(target.Unit))) Unit.GiveScaledExp(-4, Unit.Level - target.Unit.Level, true);
        target.Actor.SetPos(location);
        target.Actor.Visible = true;
        target.Actor.Targetable = true;
        TacticalUtilities.UpdateActorLocations();
        target.Actor.UnitSprite.DisplayEscape();
        TacticalUtilities.Log.RegisterRegurgitate(_actor.Unit, target.Actor.Unit, _actor.PredatorComponent.Location(target));
        RemovePrey(target);
        UpdateFullness();
        return target;
    }

    internal void FreeGreatEscapePrey(Prey preyUnit)
    {
        TacticalUtilities.Log.LogGreatEscapeFlee(Unit, preyUnit.Unit, Location(preyUnit));
        RemovePrey(preyUnit);
        preyUnit.Actor.SelfPrey = null;
    }

    internal void FreePrey(Prey preyUnit, bool forcePop)
    {
        if (_actor.Visible || forcePop || _actor.Fled)
        {
            preyUnit.Actor.SelfPrey = null;
            if (PlacedPrey(preyUnit.Actor))
            {
                preyUnit.Actor.Visible = true;
                preyUnit.Actor.Targetable = true;

                if (Config.DisorientedPrey)
                {
                    preyUnit.Actor.WasJustFreed = true;
                    preyUnit.Actor.Movement = Mathf.Min(Mathf.Max(2, (int)(.2f * preyUnit.Actor.CurrentMaxMovement())), 5);
                }
                else
                    preyUnit.Actor.Movement = preyUnit.Actor.CurrentMaxMovement();


                TacticalUtilities.UpdateActorLocations();
                preyUnit.Actor.UnitSprite.DisplayEscape();
            }
            else
            {
                Debug.Log("Couldn't place prey anywhere in a 5x5 ... killing unit");
                preyUnit.Unit.Health = -99;
                return;
            }
        }
        else
        {
            ActorUnit predator = _actor.SelfPrey?.Predator;
            if (predator != null)
            {
                var loc = _actor.SelfPrey.Location;
                Prey prey = new Prey(preyUnit.Actor, predator, preyUnit.SubPrey);
                predator.PredatorComponent.AddPrey(prey, loc);
                preyUnit.Actor.SelfPrey = prey;
            }
            else
            {
                preyUnit.Actor.SelfPrey = null;
                Debug.Log($"Couldn't find predator for {Unit.Name}, freeing sub prey");
                preyUnit.Actor.Visible = true;
                preyUnit.Actor.Targetable = true;
                TacticalUtilities.UpdateActorLocations();
                preyUnit.Actor.UnitSprite.DisplayEscape();
            }
        }

        RemovePrey(preyUnit);
        UpdateFullness();
    }

    internal void ShareTrait(TraitType traitType, Prey preyUnit, TraitType maxTraitType = TraitType.Infiltrator)
    {
        if (traitType < maxTraitType && !TraitsMethods.IsRaceModifying(traitType))
        {
            if (!Unit.HasTrait(traitType)) Unit.AddSharedTrait(traitType);
            if (!preyUnit.SharedTraits.Contains(traitType) && Unit.HasSharedTrait(traitType)) preyUnit.SharedTraits.Add(traitType);
        }
    }

    public void RefreshSharedTraits()
    {
        Unit.ResetSharedTraits();
        foreach (Prey preyUnit in Prey)
        foreach (TraitType trait in preyUnit.SharedTraits)
            if (!Unit.HasTrait(trait))
                Unit.AddSharedTrait(trait);
    }

    internal void OnSwallowCallbacks(Prey preyUnit)
    {
        var location = preyUnit.Location;
        foreach (IVoreCallback callback in PopulateCallbacks(preyUnit).OrderBy((vt) => vt.ProcessingPriority))
        {
            if (!callback.OnSwallow(preyUnit, _actor, location)) return;
        }
    }

    private void AddPrey(Prey preyUnit, PreyLocation location)
    {
        OnSwallowCallbacks(preyUnit);
        switch (location)
        {
            case PreyLocation.Breasts:
                Breasts.Add(preyUnit);
                break;
            case PreyLocation.Balls:
                Balls.Add(preyUnit);
                break;
            case PreyLocation.Stomach:
                Stomach.Add(preyUnit);
                break;
            case PreyLocation.Stomach2:
                Stomach2.Add(preyUnit);
                break;
            case PreyLocation.Womb:
                Womb.Add(preyUnit);
                break;
            case PreyLocation.Tail:
                Tail.Add(preyUnit);
                break;
            case PreyLocation.Anal:
                Stomach.Add(preyUnit);
                break;
            case PreyLocation.LeftBreast:
                LeftBreast.Add(preyUnit);
                break;
            case PreyLocation.RightBreast:
                RightBreast.Add(preyUnit);
                break;
        }

        Prey.Add(preyUnit);
        _actor.UnitSprite.UpdateSprites(_actor, true);
        UpdateAlivePrey();
    }

    private void AddPrey(Prey preyUnit)
    {
        OnSwallowCallbacks(preyUnit);
        Prey.Add(preyUnit);
        UpdateAlivePrey();
    }

    internal PreyLocation Location(Prey preyUnit)
    {
        if (Womb.Contains(preyUnit) && Unit.CanUnbirth)
        {
            return PreyLocation.Womb;
        }

        if (Breasts.Contains(preyUnit) && Unit.CanBreastVore)
        {
            return PreyLocation.Breasts;
        }

        if (Balls.Contains(preyUnit) && Unit.CanCockVore)
        {
            return PreyLocation.Balls;
        }

        if (Tail.Contains(preyUnit) && Unit.CanTailVore)
        {
            return PreyLocation.Tail;
        }

        if (Stomach2.Contains(preyUnit) && Unit.HasTrait(TraitType.DualStomach))
        {
            return PreyLocation.Stomach2;
        }

        if (LeftBreast.Contains(preyUnit) && Unit.CanBreastVore)
        {
            return PreyLocation.LeftBreast;
        }

        if (RightBreast.Contains(preyUnit) && Unit.CanBreastVore)
        {
            return PreyLocation.RightBreast;
        }
        else
        {
            return PreyLocation.Stomach;
        }
    }

    internal void OnRemoveCallbacks(Prey preyUnit, bool remove = true)
    {
        if (Prey.Contains(preyUnit))
        {
            var location = preyUnit.Location;
            if (remove) Prey.Remove(preyUnit);
            foreach (IVoreCallback callback in PopulateCallbacks(preyUnit).OrderBy((vt) => vt.ProcessingPriority))
            {
                if (!callback.OnRemove(preyUnit, _actor, location)) return;
            }
        }
    }

    private void RemovePrey(Prey preyUnit)
    {
        OnRemoveCallbacks(preyUnit);
        Womb.Remove(preyUnit);
        Breasts.Remove(preyUnit);
        Balls.Remove(preyUnit);
        Stomach.Remove(preyUnit);
        Stomach2.Remove(preyUnit);
        Tail.Remove(preyUnit);
        LeftBreast.Remove(preyUnit);
        RightBreast.Remove(preyUnit);
        UpdateAlivePrey();
    }

    private bool PreyCanAutoEscape(Prey preyUnit)
    {
        foreach (TraitType trait in preyUnit.Unit.GetTraits.Where(t => TraitList.GetTrait(t) != null && TraitList.GetTrait(t) is INoAutoEscape))
        {
            INoAutoEscape callback = (INoAutoEscape)TraitList.GetTrait(trait);
            if (!callback.CanEscape(preyUnit, _actor)) return false;
        }

        return true;
    }

    public void Digest(string feedType = "")
    {
        AlivePrey = 0;
        int totalHeal = 0;
        foreach (Prey preyUnit in Prey.ToList())
        {
            if ((preyUnit.Location != PreyLocation.Breasts && feedType == "breastfeed") || (preyUnit.Location != PreyLocation.Balls && feedType == "cumfeed"))
            {
                break;
            }

            StatusEffect hypnotizedEffect = preyUnit.Unit.GetStatusEffect(StatusEffectType.Hypnotized);
            if (Unit.HasTrait(TraitType.EnthrallingDepths) || (hypnotizedEffect != null && Equals(hypnotizedEffect.Side, Unit.FixedSide)))
            {
                preyUnit.Unit.ApplyStatusEffect(StatusEffectType.WillingPrey, 0, 3);
            }

            int preyDamage = CalculateDigestionDamage(preyUnit);
            if (preyUnit.Predator.Unit.HasTrait(TraitType.Honeymaker) && preyUnit.Unit.IsDead && (preyUnit.Location == PreyLocation.Breasts || preyUnit.Location == PreyLocation.LeftBreast || preyUnit.Location == PreyLocation.RightBreast)) preyDamage /= 2;
            if (preyUnit.TurnsDigested < preyUnit.Unit.TraitBoosts.DigestionImmunityTurns) preyDamage = 0;
            if (Tail.Contains(preyUnit))
            {
                if (preyUnit.TurnsBeingSwallowed >= 1)
                {
                    Tail.Remove(preyUnit);
                    Stomach.Add(preyUnit);
                    preyUnit.TurnsBeingSwallowed = 0;
                }
                else
                {
                    preyUnit.TurnsBeingSwallowed++;
                    preyDamage = 0;
                }
            }

            if (Unit.HasTrait(TraitType.DualStomach))
            {
                if (Stomach.Contains(preyUnit))
                {
                    if (preyUnit.TurnsBeingSwallowed >= 2)
                    {
                        Stomach.Remove(preyUnit);
                        Stomach2.Add(preyUnit);
                    }
                    else
                    {
                        preyUnit.TurnsBeingSwallowed++;
                    }
                }
            }

            if (_actor.PredatorComponent.IsUnitInPrey(preyUnit.Actor, PreyLocation.Womb) && _actor.PredatorComponent.BirthStatBoost > 0 && Equals(preyUnit.Unit.GetApparentSide(Unit), Unit.FixedSide) && Config.KuroTenkoEnabled && Config.CumGestation)
            {
                while (_actor.PredatorComponent.BirthStatBoost > 0)
                {
                    int stat = UnityEngine.Random.Range(0, 8);
                    preyUnit.Unit.ModifyStat(stat, 1);
                    if (stat == 6)
                    {
                        preyUnit.Unit.Health += 2;
                    }

                    if (stat == 0)
                    {
                        preyUnit.Unit.Health += 1;
                    }

                    _actor.PredatorComponent.BirthStatBoost--;
                }
            }

            if (TacticalUtilities.IsPreyEndoTargetForUnit(preyUnit, Unit))
            {
                if (Unit.HasTrait(TraitType.HealingBelly))
                    preyDamage = Math.Min(Unit.MaxHealth / -10, -1);
                else
                    preyDamage = 0;
                if (Unit.HasTrait(TraitType.Extraction))
                {
                    var possibleTraits = Unit.GetTraits.Where(s => !preyUnit.Unit.GetTraits.Contains(s) && State.AssimilateList.CanGet(s)).ToArray();

                    if (possibleTraits.Any())
                    {
                        var trait = possibleTraits[State.Rand.Next(possibleTraits.Length)];
                        preyUnit.Unit.AddPermanentTrait(trait);
                    }
                }

                if (Unit.HasTrait(TraitType.InfiniteAssimilation) && !preyUnit.Unit.HasTrait(TraitType.InfiniteAssimilation) && Config.KuroTenkoEnabled) preyUnit.Unit.AddPermanentTrait(TraitType.InfiniteAssimilation);
                if (Unit.HasTrait(TraitType.Assimilate) && !preyUnit.Unit.HasTrait(TraitType.Assimilate) && Config.KuroTenkoEnabled) preyUnit.Unit.AddPermanentTrait(TraitType.Assimilate);
                if (Unit.HasTrait(TraitType.AdaptiveBiology) && !preyUnit.Unit.HasTrait(TraitType.AdaptiveBiology) && Config.KuroTenkoEnabled) preyUnit.Unit.AddPermanentTrait(TraitType.AdaptiveBiology);
                if (Unit.HasTrait(TraitType.Corruption) && !preyUnit.Unit.HasTrait(TraitType.Corruption))
                {
                    preyUnit.Unit.AddPermanentTrait(TraitType.Corruption);
                    if (!preyUnit.Unit.HasTrait(TraitType.Untamable)) preyUnit.Unit.FixedSide = Unit.FixedSide;
                    preyUnit.Unit.HiddenFixedSide = true;
                    preyUnit.Actor.SidesAttackedThisBattle = new List<Side>();
                }

                //if (preyUnit.Unit.HasTrait(Traits.Shapeshifter) || preyUnit.Unit.HasTrait(Traits.Skinwalker))
                //{
                //    preyUnit.Unit.AcquireShape(unit);
                //}
                preyUnit.Unit.ReloadTraits();
                preyUnit.Unit.InitializeTraits();
            }
            else if (Config.FriendlyRegurgitation && Unit.HasTrait(TraitType.Greedy) == false && Equals(preyUnit.Unit.GetApparentSide(_actor.Unit), _actor.Unit.FixedSide) && Equals(TacticalUtilities.GetMindControlSide(preyUnit.Unit), Side.TrueNoneSide) && preyUnit.Unit.Health > 0 && preyUnit.Actor.Surrendered == false && PreyCanAutoEscape(preyUnit))
            {
                State.GameManager.TacticalMode.TacticalStats.RegisterRegurgitation(Unit.Side);
                TacticalUtilities.Log.RegisterRegurgitated(Unit, preyUnit.Unit, Location(preyUnit));
                FreePrey(preyUnit, false);
                continue;
            }

            if (preyDamage > 0)
                totalHeal += DigestOneUnit(preyUnit, preyDamage);
            else
                DigestOneUnit(preyUnit, preyDamage);
            if (Unit.HasTrait(TraitType.Growth))
            {
                Unit.BaseScale += (float)totalHeal / preyUnit.Unit.MaxHealth * .2d * CalculateGrowthValue(preyUnit);
                if (Unit.BaseScale > Config.GrowthCap) Unit.BaseScale = Config.GrowthCap;
            }
        }

        if (!(Unit.Health < Unit.MaxHealth))
        {
            totalHeal = 0;
        }

        if (totalHeal > 0)
        {
            Unit.Heal(totalHeal);
            _actor.UnitSprite.DisplayDamage(-totalHeal);
            TacticalUtilities.Log.RegisterHeal(Unit, new int[] { totalHeal, 0 });
        }

        UpdateFullness();
    }

    private float CalculateGrowthValue(Prey preyUnit)
    {
        float preyMass = preyUnit.Unit.TraitBoosts.BulkMultiplier * State.RaceSettings.GetBodySize(preyUnit.Unit.Race);
        float predMass = Unit.TraitBoosts.BulkMultiplier * State.RaceSettings.GetBodySize(Unit.Race);
        float sizeDiff = preyUnit.Unit.GetScale(2) * preyMass / (Unit.GetScale(2) * predMass);
        float preyBoosts = ((preyUnit.Unit.TraitBoosts.Outgoing.Nutrition - 1) * .2f + 1f) * preyUnit.Unit.TraitBoosts.Outgoing.GrowthRate;
        float predBoosts = ((Unit.TraitBoosts.Incoming.Nutrition - 1) * .2f + 1f) * Unit.TraitBoosts.Incoming.GrowthRate * Config.GrowthMod;
        return sizeDiff * preyBoosts * predBoosts;
    }

    private int CalculateDigestionDamage(Prey preyUnit)
    {
        if (preyUnit.TurnsDigested < preyUnit.Unit.TraitBoosts.DigestionImmunityTurns || preyUnit.Unit.HasTrait(TraitType.TheGreatEscape)) return 0;

        float usedCapacity = TotalCapacity() - FreeCap();
        float predStomach = Mathf.Pow(Unit.GetStat(Stat.Stomach) + 15, 1.5f);
        float predVoracity = Mathf.Pow(Unit.GetStat(Stat.Voracity) + 15, 1.5f);
        float preyEndurance = Mathf.Pow(preyUnit.Unit.GetStat(Stat.Endurance) + 15, 1.5f);
        float preyWill = Mathf.Pow(preyUnit.Unit.GetStat(Stat.Will) + 15, 1.5f);
        float predScore = 2 * (40 + predStomach * 3 + predVoracity) / (1f + (usedCapacity - preyUnit.Actor.Bulk()) / usedCapacity);
        float preyScore = ((preyEndurance + preyWill * .75f) / (1 + preyUnit.Actor.BodySize() / 40) + 300) / (1 + preyUnit.TurnsDigested / 5f);
        predScore *= Unit.TraitBoosts.Outgoing.DigestionRate;
        preyScore /= preyUnit.Unit.TraitBoosts.Incoming.DigestionRate;
        int damage = (int)Math.Round(predScore / preyScore);
        if (damage < 1) damage = 1;

        return damage;
    }

    /// <summary>
    ///     Returns the % of the stomach used.  i.e. 50% is .5, can be over 100%
    /// </summary>
    internal float UsageFraction => (TotalCapacity() - FreeCap()) / TotalCapacity();

    internal void FreeUnit(ActorUnit target, bool forcePop = false)
    {
        var preyUnit = Prey.Where(s => s.Actor == target).FirstOrDefault();
        if (preyUnit != null)
        {
            RemovePrey(preyUnit);
            FreePrey(preyUnit, forcePop);
        }
    }

    public void CreateSpawn(Race race, Side side, float experience, bool forced = false)
    {
        Spawn spawnUnit = new Spawn(side, race, (int)experience);
        ActorUnit spawnActor = State.GameManager.TacticalMode.AddUnitToBattle(spawnUnit, _actor);
        State.GameManager.TacticalMode.DirtyPack = true;
        if (forced)
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{InfoPanel.RaceSingular(spawnUnit)} Spawn <b>{spawnUnit.Name}</b> bursts from <b>{_actor.Unit.Name}</b>'s body");
        else
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{_actor.Unit.Name}</b> has created a {InfoPanel.RaceSingular(spawnUnit)} Spawn <b>{spawnUnit.Name}</b>.");
        Prey preyref = new Prey(spawnActor, _actor, spawnActor.PredatorComponent?.Prey);
        spawnActor.UnitSprite.UpdateSprites(spawnActor, false);
        AddPrey(preyref);
        FreeUnit(preyref.Actor);
        if (!State.GameManager.TacticalMode.TurboMode) _actor.SetBirthMode();
        RemovePrey(preyref);
    }

    public void ResetTemplate()
    {
        if (Template == null) return;
        foreach (TraitType trait in Template.SharedTraits) Unit.RemoveSharedTrait(trait);
        RefreshSharedTraits();
        Template = null;
    }

    internal bool CheckChangeRace(Prey preyUnit)
    {
        if (Equals(Unit.HiddenRace, Unit.Race) && Template == null)
        {
            return true;
        }

        if (!Prey.Contains(Template))
        {
            Template = null;
            return true;
        }

        return false;
    }

    private Prey SelectPrey(bool isDead, bool random)
    {
        Prey bestPrey = null;
        List<Prey> preyList;
        if (isDead)
            preyList = DeadPrey;
        else
            preyList = Prey;
        if (preyList.Count > 0)
        {
            bestPrey = preyList[State.Rand.Next(preyList.Count)];
            if (random) return bestPrey;
            foreach (Prey preyUnit in preyList)
                if (preyUnit.Unit.Health > bestPrey.Unit.Health)
                    bestPrey = preyUnit;
        }

        return bestPrey;
    }

    public bool ChangeRaceAuto(bool isDead, bool random)
    {
        Prey preyUnit = SelectPrey(isDead, random);
        if (preyUnit != null)
        {
            return TryChangeRace(preyUnit);
        }
        else
        {
            _actor.RevertRace();
            return false;
        }
    }

    internal bool TryChangeRace(Prey preyUnit)
    {
        if (CheckChangeRace(preyUnit))
        {
            if (Unit.HasTrait(TraitType.Changeling) && !preyUnit.Unit.IsDead) return false;
            _actor.ChangeRaceAny(preyUnit.Unit, false, true);
            Template = preyUnit;
            foreach (TraitType trait in preyUnit.Unit.GetTraits)
            {
                ShareTrait(trait, preyUnit);
            }

            return true;
        }

        return false;
    }

    private int DigestOneUnit(Prey preyUnit, int preyDamage)
    {
        List<IVoreCallback> callbacks = PopulateCallbacks(preyUnit).OrderBy((vt) => vt.ProcessingPriority).ToList();
        var location = preyUnit.Location;
        int totalHeal = 0;
        bool freshKill = false;
        if (Unit.HasTrait(TraitType.Extraction) && !TacticalUtilities.IsPreyEndoTargetForUnit(preyUnit, Unit))
        {
            var possibleTraits = preyUnit.Unit.GetTraits.Where(s => Unit.GetTraits.Contains(s) == false && State.AssimilateList.CanGet(s)).ToArray();

            if (possibleTraits.Any())
            {
                var trait = possibleTraits[State.Rand.Next(possibleTraits.Length)];
                Unit.AddPermanentTrait(trait);
                //process vore traits appropriately
                if (TraitList.GetTrait(trait) is IVoreCallback callback)
                {
                    if (callback.IsPredTrait == true)
                        callback.OnSwallow(preyUnit, _actor, location);
                    else
                        callback.OnRemove(preyUnit, _actor, location);
                }

                preyUnit.Unit.RemoveTrait(trait);
            }
            else if (preyUnit.Unit.GetTraits.Any())
            {
                var trait = preyUnit.Unit.GetTraits[State.Rand.Next(preyUnit.Unit.GetTraits.Count)];
                preyUnit.Unit.RemoveTrait(trait);
                Unit.GiveRawExp(5);
                _actor.UnitSprite.DisplayDamage(5, false, true);
            }
        }

        if (Unit.HasTrait(TraitType.Annihilation) && !TacticalUtilities.IsPreyEndoTargetForUnit(preyUnit, Unit))
        {
        }

        if (preyUnit.Unit.IsThisCloseToDeath(preyDamage))
        {
            if (preyUnit.Unit.HasTrait(TraitType.Corruption))
            {
                _actor.AddCorruption(preyUnit.Unit.GetStatTotal(), preyUnit.Unit.FixedSide);
                preyUnit.Unit.RemoveTrait(TraitType.Corruption);
            }

            foreach (IVoreCallback callback in callbacks)
            {
                if (!callback.OnFinishDigestion(preyUnit, _actor, location)) return 0;
            }

            if (preyUnit.Unit.CanBeConverted() &&
                (Location(preyUnit) == PreyLocation.Womb || Config.KuroTenkoConvertsAllTypes) &&
                ((Config.KuroTenkoEnabled && (Config.UbConversion == UBConversion.Both || Config.UbConversion == UBConversion.ConversionOnly)) || Unit.HasTrait(TraitType.PredConverter)) &&
                !Unit.HasTrait(TraitType.PredRebirther) &&
                !Unit.HasTrait(TraitType.PredGusher))
            {
                preyUnit.Unit.Health = preyUnit.Unit.MaxHealth / 2;
                preyUnit.Actor.Movement = 0;
                preyUnit.ChangeSide(Unit.Side);
                FreeUnit(preyUnit.Actor);
                TacticalUtilities.Log.RegisterBirth(Unit, preyUnit.Unit, 1f);
                if (!State.GameManager.TacticalMode.TurboMode) _actor.SetBirthMode();
                return 0;
            }

            State.GameManager.TacticalMode.TacticalStats.RegisterDigestion(Unit.Side);
            TacticalUtilities.Log.RegisterDigest(Unit, preyUnit.Unit, Location(preyUnit));
            if (!State.GameManager.TacticalMode.TurboMode) _actor.SetDigestionMode();
            if (State.GameManager.TacticalMode.TurboMode == false && Config.DigestionSkulls) Object.Instantiate(State.GameManager.TacticalMode.SkullPrefab, new Vector3(_actor.Position.X + UnityEngine.Random.Range(-0.2F, 0.2F), _actor.Position.Y + 0.1F + UnityEngine.Random.Range(-0.1F, 0.1F)), new Quaternion());
            ActorUnit existingPredator = _actor;
            freshKill = true;
            if (Unit.HasTrait(TraitType.DigestionConversion) && State.Rand.Next(2) == 0 && preyUnit.Unit.CanBeConverted())
            {
                preyUnit.Unit.Health = preyUnit.Unit.MaxHealth / 2;
                preyUnit.Actor.Movement = 0;

                preyUnit.ChangeSide(Unit.Side);
                State.GameManager.TacticalMode.TacticalStats.RegisterRegurgitation(Unit.Side);
                FreeUnit(preyUnit.Actor);

                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{preyUnit.Unit.Name} converted from one side to another thanks to {Unit.Name}'s digestion conversion trait.");
                return 0;
            }

            if (Unit.HasTrait(TraitType.DigestionRebirth) && State.Rand.Next(2) == 0 && preyUnit.Unit.CanBeConverted() && (Config.SpecialMercsCanConvert || RaceFuncs.IsNotUniqueMerc(Unit.DetermineConversionRace())))
            {
                //HandleShapeshifterRebirth(preyUnit);
                Race conversionRace = Unit.DetermineConversionRace();
                if (Unit.HasTrait(TraitType.DigestionRebirth) && !Unit.HasSharedTrait(TraitType.DigestionRebirth)) conversionRace = Unit.HiddenUnit.DetermineConversionRace();
                // use source race IF changeling already had this ability before transforming
                preyUnit.Unit.Health = preyUnit.Unit.MaxHealth / 2;
                preyUnit.ChangeSide(Unit.Side);
                preyUnit.ChangeRace(conversionRace);
                TacticalUtilities.Log.RegisterBirth(Unit, preyUnit.Unit, 0.5f);
                FreeUnit(preyUnit.Actor);
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{preyUnit.Unit.Name} converted from one side to another and changed race thanks to {Unit.Name}'s converting digestion rebirth trait.");
                return 0;
            }

            preyUnit.Actor.KilledByDigestion = true;
            if (preyUnit.Unit.HasTrait(TraitType.CursedMark))
            {
                Unit.AddPermanentTrait(TraitType.CursedMark);
                preyUnit.Unit.RemoveTrait(TraitType.CursedMark);
            }

            Unit.DigestedUnits++;
            if (Unit.HasTrait(TraitType.EssenceAbsorption) && Unit.DigestedUnits % 4 == 0) Unit.GeneralStatIncrease(1);
            if (Equals(preyUnit.Unit.Race, Race.Asura)) Unit.EarnedMask = true;
            if (Unit.HasTrait(TraitType.TasteForBlood)) _actor.GiveRandomBoost();
            Unit.EnemiesKilledThisBattle++;
            preyUnit.Unit.KilledBy = Unit;
            preyUnit.Unit.Kill();
            for (int i = 0; i < 20; i++)
            {
                ActorUnit next = TacticalUtilities.FindPredator(existingPredator);
                if (next == null) break;
                existingPredator = next;
            }

            DigestionEffect(preyUnit, existingPredator);

            if (Unit.HasTrait(TraitType.MetabolicSurge))
            {
                Unit.ApplyStatusEffect(StatusEffectType.Empowered, 1.0f, 5);
            }
        }

        if (preyUnit.Unit.IsDead == false)
        {
            preyUnit.Actor.SubtractHealth(preyDamage);
            preyUnit.TurnsSinceLastDamage = 0;
        }

        if (freshKill)
        {
            preyUnit.Actor.Unit.Health = 0;
            foreach (IVoreCallback callback in callbacks)
            {
                if (!callback.OnDigestionKill(preyUnit, _actor, location)) return 0;
            }
        }


        if (preyUnit.Unit.IsThisCloseToDeath(preyDamage))
        {
            TacticalUtilities.Log.RegisterNearDigestion(Unit, preyUnit.Unit, Location(preyUnit));
        }
        else if (preyUnit.Unit.IsDead == false && State.Rand.Next(6) == 0 && preyDamage > 0)
        {
            TacticalUtilities.Log.LogDigestionRandom(Unit, preyUnit.Unit, Location(preyUnit));
        }
        else if (preyUnit.Unit.IsDead == false && State.Rand.Next(6) == 0 && preyDamage == 0 && preyUnit.Unit.HasTrait(TraitType.TheGreatEscape))
        {
            TacticalUtilities.Log.LogGreatEscapeKeep(Unit, preyUnit.Unit, Location(preyUnit));
        }

        if (preyUnit.Unit.IsDead)
        {
            float speedFactor;
            speedFactor = (float)Math.Sqrt(_actor.BodySize() / preyUnit.Actor.BodySize());

            speedFactor *= Unit.TraitBoosts.Outgoing.AbsorptionRate;
            speedFactor *= preyUnit.Unit.TraitBoosts.Incoming.AbsorptionRate;

            if (speedFactor > 4f && speedFactor < 1000) speedFactor = 4f;
            int healthReduction = (int)Math.Max(Math.Round(preyUnit.Unit.MaxHealth * speedFactor / 15), 1);
            if (healthReduction >= preyUnit.Unit.MaxHealth + preyUnit.Unit.Health) healthReduction = preyUnit.Unit.MaxHealth + preyUnit.Unit.Health + 1;
            preyUnit.Actor.SubtractHealth(healthReduction);
            totalHeal += Math.Max((int)(healthReduction / 2 * preyUnit.Unit.TraitBoosts.Outgoing.Nutrition * Unit.TraitBoosts.Incoming.Nutrition), 1);
            var baseManaGain = healthReduction * (preyUnit.Unit.TraitBoosts.Outgoing.ManaAbsorbHundreths + Unit.TraitBoosts.Incoming.ManaAbsorbHundreths);
            var totalManaGain = baseManaGain / 100 + (State.Rand.Next(100) < baseManaGain % 100 ? 1 : 0);
            Unit.RestoreMana(totalManaGain);
            foreach (IVoreCallback callback in callbacks)
            {
                if (!callback.OnAbsorption(preyUnit, _actor, location)) return 0;
            }

            if (preyUnit.Unit.IsDeadAndOverkilledBy(healthReduction * 2) && preyUnit.SubPrey?.Count() > 0)
            {
                Prey[] aliveSubUnits = preyUnit.GetAliveSubPrey();
                for (int i = 0; i < aliveSubUnits.Length; i++)
                {
                    Prey newPrey = new Prey(aliveSubUnits[i].Actor, _actor, aliveSubUnits[i].SubPrey);
                    AddPrey(newPrey, Location(preyUnit));
                    preyUnit.SubPrey.Remove(aliveSubUnits[i]);
                }
            }

            if (preyUnit.Unit.IsDeadAndOverkilledBy(preyUnit.Unit.MaxHealth))
            {
                if (_actor.Infected)
                {
                    _actor.Damage(totalHeal * 2);
                    totalHeal = 0;
                    CreateSpawn(_actor.InfectedRace, _actor.InfectedSide, Unit.Experience / 2, true);
                }

                foreach (IVoreCallback callback in callbacks)
                {
                    if (!callback.OnFinishAbsorption(preyUnit, _actor, location)) return 0;
                }

                if (preyUnit.Unit.CanBeConverted() &&
                    (Location(preyUnit) == PreyLocation.Womb || Config.KuroTenkoConvertsAllTypes) &&
                    ((Config.KuroTenkoEnabled && (Config.UbConversion == UBConversion.Both || Config.UbConversion == UBConversion.RebirthOnly)) || Unit.HasTrait(TraitType.PredRebirther)) &&
                    (Config.SpecialMercsCanConvert || RaceFuncs.IsNotUniqueMerc(Unit.DetermineConversionRace())) &&
                    !Unit.HasTrait(TraitType.PredGusher))
                {
                    Race conversionRace = Unit.DetermineConversionRace();
                    if (Unit.HasTrait(TraitType.PredRebirther) && !Unit.HasSharedTrait(TraitType.PredRebirther)) conversionRace = Unit.HiddenUnit.DetermineConversionRace();
                    // use source race IF changeling already had this ability before transforming
                    preyUnit.Unit.Health = preyUnit.Unit.MaxHealth / 2;
                    preyUnit.ChangeSide(Unit.Side);
                    if (!Equals(preyUnit.Unit.Race, conversionRace))
                    {
                        //HandleShapeshifterRebirth(preyUnit);
                        preyUnit.ChangeRace(conversionRace);
                    }

                    FreeUnit(preyUnit.Actor);
                    TacticalUtilities.Log.RegisterBirth(Unit, preyUnit.Unit, 1f);
                    if (!State.GameManager.TacticalMode.TurboMode) _actor.SetBirthMode();
                    RemovePrey(preyUnit);
                    return 0;
                }
                else
                {
                    TacticalUtilities.Log.RegisterAbsorb(Unit, preyUnit.Unit, Location(preyUnit));
                }

                Unit.GiveScaledExp(8 * preyUnit.Unit.ExpMultiplier, Unit.Level - preyUnit.Unit.Level, true);
                if (Config.WeightGain)
                {
                    if (Location(preyUnit) == PreyLocation.Balls)
                    {
                        if (Unit.HasDick)
                        {
                            Unit.DickSize = Math.Min(Unit.DickSize + 1, RaceFuncs.GetRace(Unit).SetupOutput.DickSizes() - 1);
                            if (Config.RaceSizeLimitsWeightGain && State.RaceSettings.GetOverrideDick(Unit.Race)) Unit.DickSize = Math.Min(Unit.DickSize, State.RaceSettings.Get(Unit.Race).MaxDick);
                        }
                    }
                    else if (Location(preyUnit) == PreyLocation.Breasts || Location(preyUnit) == PreyLocation.LeftBreast || Location(preyUnit) == PreyLocation.RightBreast)
                    {
                        if (Unit.HasBreasts)
                        {
                            Unit.SetDefaultBreastSize(Math.Min(Unit.DefaultBreastSize + 1, RaceFuncs.GetRace(Unit).SetupOutput.BreastSizes() - 1), Unit.BreastSize == Unit.DefaultBreastSize);
                            if (Config.RaceSizeLimitsWeightGain && State.RaceSettings.GetOverrideBreasts(Unit.Race)) Unit.SetDefaultBreastSize(Math.Min(Unit.DefaultBreastSize, State.RaceSettings.Get(Unit.Race).MaxBoob));
                        }
                    }
                    else
                    {
                        if (Config.AltVoreOralGain && State.Rand.NextDouble() < .4)
                        {
                            if (Unit.HasDick)
                            {
                                Unit.DickSize = Math.Min(Unit.DickSize + 1, RaceFuncs.GetRace(Unit).SetupOutput.DickSizes() - 1);
                                if (Config.RaceSizeLimitsWeightGain && State.RaceSettings.GetOverrideDick(Unit.Race)) Unit.DickSize = Math.Min(Unit.DickSize, State.RaceSettings.Get(Unit.Race).MaxDick);
                            }
                        }

                        if (Config.AltVoreOralGain && State.Rand.NextDouble() < .4)
                        {
                            if (Unit.HasBreasts)
                            {
                                Unit.SetDefaultBreastSize(Math.Min(Unit.DefaultBreastSize + 1, RaceFuncs.GetRace(Unit).SetupOutput.BreastSizes() - 1), Unit.BreastSize == Unit.DefaultBreastSize);
                                if (Config.RaceSizeLimitsWeightGain && State.RaceSettings.GetOverrideBreasts(Unit.Race)) Unit.SetDefaultBreastSize(Math.Min(Unit.DefaultBreastSize, State.RaceSettings.Get(Unit.Race).MaxBoob));
                            }
                        }

                        if (State.Rand.NextDouble() < .5 && RaceFuncs.GetRace(Unit).SetupOutput.WeightGainDisabled == false)
                        {
                            Unit.BodySize = Math.Max(Math.Min(Unit.BodySize + 1, RaceFuncs.GetRace(Unit).SetupOutput.BodySizes - 1), 0);
                            if (Config.RaceSizeLimitsWeightGain && State.RaceSettings.GetOverrideWeight(Unit.Race)) Unit.BodySize = Math.Min(Unit.BodySize, State.RaceSettings.Get(Unit.Race).MaxWeight);
                        }
                    }
                }

                AbsorptionEffect(preyUnit, Location(preyUnit));
                if (!State.GameManager.TacticalMode.TurboMode) _actor.SetAbsorbtionMode();
                CheckPredTraitAbsorption(preyUnit);
                //if (unit.HasTrait(Traits.Shapeshifter) || unit.HasTrait(Traits.Skinwalker))
                //{
                //    unit.AcquireShape(preyUnit.Unit);
                //}

                if (preyUnit.SubPrey?.Count() > 0) //Catches any dead prey that weren't already properly moved
                {
                    Prey[] subUnits = preyUnit.SubPrey.ToArray();
                    for (int i = 0; i < subUnits.Length; i++)
                    {
                        Prey newPrey = new Prey(subUnits[i].Actor, _actor, subUnits[i].SubPrey);
                        AddPrey(newPrey, Location(preyUnit));
                        preyUnit.SubPrey.Remove(subUnits[i]);
                    }
                }

                RemovePrey(preyUnit);
            }
            else if (_actor.Infected)
            {
                _actor.Damage(totalHeal, false, false);
                totalHeal = 0;
            }
        }
        else
        {
            foreach (IVoreCallback callback in callbacks)
            {
                if (!callback.OnDigestion(preyUnit, _actor, location)) return 0;
            }

            preyUnit.TurnsDigested++;
            AlivePrey++;
            preyUnit.UpdateEscapeRate();
            float escapeMult = 1;
            if (FreeCap() < 0)
            {
                float cap = TotalCapacity();
                escapeMult = 1.4f + 2 * (Fullness / cap - 1);
                _actor.Damage((int)(FreeCap() * -1) / 2);
            }

            if (State.Rand.NextDouble() < preyUnit.EscapeRate * escapeMult && preyUnit.Actor.Surrendered == false)
            {
                if (Stomach2.Contains(preyUnit))
                {
                    Stomach.Add(preyUnit);
                    Stomach2.Remove(preyUnit);
                    TacticalUtilities.Log.RegisterPartialEscape(Unit, preyUnit.Unit, Location(preyUnit));
                    preyUnit.TurnsBeingSwallowed = -5; //Gets 7 turns before it gets forced down again, so it needs to escape twice in 7 turns
                }
                else
                {
                    State.GameManager.TacticalMode.TacticalStats.RegisterEscape(Unit.Side);
                    AlivePrey--;
                    TacticalUtilities.Log.RegisterEscape(Unit, preyUnit.Unit, Location(preyUnit));
                    FreePrey(preyUnit, false);
                }
            }
        }

        return totalHeal;
    }

    private void HandleShapeshifterRebirth(Prey preyUnit)
    {
        //if (preyUnit.Unit.HasTrait(Traits.Shapeshifter) || preyUnit.Unit.HasTrait(Traits.Skinwalker)) // preserve the unit as it is and rebirth a copy of it instead
        //{
        //    Unit clone = preyUnit.Unit.Clone();
        //    preyUnit.Unit.ShifterShapes.Add(clone);
        //    clone.ShifterShapes = preyUnit.Unit.ShifterShapes;
        //    preyUnit.Unit = clone;
        //}
    }

    //public List<Actor_Unit> Birth()
    //{
    //    List<Actor_Unit> released = new List<Actor_Unit>();
    //    foreach (Prey preyUnit in womb.ToList())
    //    {
    //        if (preyUnit.Unit.Health < (preyUnit.Unit.Level * -10) && Config.KuroTenkoEnabled)
    //        {
    //            int offset = 0 - (int)preyUnit.Unit.Experience;
    //            preyUnit.Unit.GiveExp(offset);
    //            Prey newPrey = null;
    //            if (!(preyUnit.Unit.ImmuneToDefections || preyUnit.Unit.Type == UnitType.Leader) && preyUnit.Unit.IsDead == false)
    //            {
    //                preyUnit.Unit.Side = unit.Side;
    //            }
    //            else
    //            {
    //                Unit newUnit = new Unit(unit.Side, unit.Race, 0, unit.Predator);
    //                Actor_Unit newActor = new Actor_Unit(preyUnit.Actor.Position, newUnit);
    //                newActor.UpdateBestWeapons();
    //                newActor.Visible = false;
    //                newActor.Targetable = false;
    //                State.GameManager.TacticalMode.DirtyPack = true;
    //                State.GameManager.TacticalMode.AddUnit(newActor);
    //                newActor.UnitSprite.UpdateSprites(newActor, false);

    //                newPrey = new Prey(newActor, actor, newActor.PredatorComponent?.prey);
    //                while (actor.PredatorComponent.birthStatBoost > 0)
    //                {
    //                    int stat = UnityEngine.Random.Range(0, 8);
    //                    newPrey.Unit.ModifyStat(stat, 1);
    //                    if (stat == 6)
    //                    {
    //                        preyUnit.Unit.Health += 2;
    //                    }
    //                    if (stat == 0)
    //                    {
    //                        preyUnit.Unit.Health += 1;
    //                    }
    //                    actor.PredatorComponent.birthStatBoost--;
    //                }
    //                AddPrey(preyUnit);
    //            }
    //            if (newPrey != null)
    //            {
    //                preyUnit.Actor.SubtractHealth(999);
    //                RemovePrey(preyUnit);
    //                FreePrey(newPrey, true);
    //                released.Add(newPrey.Actor);
    //            }
    //            else
    //            {
    //                preyUnit.Actor.Surrendered = false;
    //                FreePrey(preyUnit, true);
    //                released.Add(preyUnit.Actor);
    //            }
    //            TacticalUtilities.Log.RegisterBirth(unit, preyUnit.Unit, 1f);
    //        }
    //    }
    //    UpdateFullness();
    //    return released;
    //}

    private void CheckPredTraitAbsorption(Prey preyUnit)
    {
        bool updated = false;
        bool raceUpdated = true;
        if (Unit.HasTrait(TraitType.Extraction))
        {
            var possibleTraits = preyUnit.Unit.GetTraits.Where(s => Unit.GetTraits.Contains(s) == false && State.AssimilateList.CanGet(s)).ToArray();

            foreach (TraitType trait in possibleTraits)
            {
                Unit.AddPermanentTrait(trait);
                preyUnit.Unit.RemoveTrait(trait);
                updated = true;
            }

            foreach (TraitType trait in preyUnit.Unit.GetTraits)
            {
                preyUnit.Unit.RemoveTrait(trait);
                Unit.GiveRawExp(5);
            }
        }

        if (preyUnit.Unit.HasTrait(TraitType.Donor))
        {
            int donorIndex = preyUnit.Unit.GetTraits.IndexOf(TraitType.Donor);
            var donorTraits = preyUnit.Unit.GetTraits.SkipWhile((t, index) => index <= donorIndex);
            var possibleTraits = donorTraits.Where(s => Unit.GetTraits.Contains(s) == false && State.AssimilateList.CanGet(s)).ToArray();

            foreach (TraitType trait in possibleTraits)
            {
                Unit.AddPermanentTrait(trait);
                preyUnit.Unit.RemoveTrait(trait);
                updated = true;
            }
        }

        if (Unit.HasTrait(TraitType.InfiniteAssimilation))
        {
            var possibleTraits = preyUnit.Unit.GetTraits.Where(s => Unit.GetTraits.Contains(s) == false && State.AssimilateList.CanGet(s)).ToArray();

            if (possibleTraits.Any())
            {
                if (Unit.HasTrait(TraitType.SynchronizedEvolution))
                {
                    RaceSettingsItem item = State.RaceSettings.Get(Unit.Race);
                    item.RaceTraits.Add(possibleTraits[State.Rand.Next(possibleTraits.Length)]);
                    raceUpdated = true;
                }
                else
                {
                    Unit.AddPermanentTrait(possibleTraits[State.Rand.Next(possibleTraits.Length)]);
                    updated = true;
                }
            }
        }
        else if (Unit.HasTrait(TraitType.Assimilate))
        {
            if (Unit.BaseTraitsCount < 5)
            {
                var possibleTraits = preyUnit.Unit.GetTraits.Where(s => Unit.GetTraits.Contains(s) == false && State.AssimilateList.CanGet(s)).ToArray();

                if (possibleTraits.Any())
                {
                    if (Unit.HasTrait(TraitType.SynchronizedEvolution))
                    {
                        RaceSettingsItem item = State.RaceSettings.Get(Unit.Race);
                        item.RaceTraits.Add(possibleTraits[State.Rand.Next(possibleTraits.Length)]);
                        raceUpdated = true;
                    }
                    else
                    {
                        Unit.AddPermanentTrait(possibleTraits[State.Rand.Next(possibleTraits.Length)]);
                        updated = true;
                    }
                }
            }
            else if (Unit.BaseTraitsCount == 5)
            {
                var possibleTraits = preyUnit.Unit.GetTraits.Where(s => Unit.GetTraits.Contains(s) == false && State.AssimilateList.CanGet(s)).ToArray();

                if (possibleTraits.Any())
                {
                    Unit.RemoveTrait(TraitType.Assimilate);
                    if (Unit.HasTrait(TraitType.SynchronizedEvolution))
                    {
                        RaceSettingsItem item = State.RaceSettings.Get(Unit.Race);
                        item.RaceTraits.Add(possibleTraits[State.Rand.Next(possibleTraits.Length)]);
                        raceUpdated = true;
                    }
                    else
                    {
                        Unit.AddPermanentTrait(possibleTraits[State.Rand.Next(possibleTraits.Length)]);
                        updated = true;
                    }
                }
            }
        }

        if (Unit.HasTrait(TraitType.AdaptiveBiology) && updated == false)
        {
            var possibleTraits = preyUnit.Unit.GetTraits.Where(s => Unit.GetTraits.Contains(s) == false && State.AssimilateList.CanGet(s)).ToArray();

            if (possibleTraits.Any())
            {
                Unit.AddTemporaryTrait(possibleTraits[State.Rand.Next(possibleTraits.Length)]);
                updated = true;
            }
        }

        if (updated)
        {
            Unit.ReloadTraits();
            Unit.InitializeTraits();
        }

        if (raceUpdated)
        {
            if (State.World.Villages != null)
            {
                var units = StrategicUtilities.GetAllUnits();
                foreach (Unit unit in units)
                {
                    unit.ReloadTraits();
                }
            }
            else
            {
                foreach (ActorUnit actor in TacticalUtilities.Units)
                {
                    actor.Unit.ReloadTraits();
                    actor.Unit.InitializeTraits();
                    actor.Unit.UpdateSpells();
                }
            }

            if (State.World.AllActiveEmpires != null)
            {
                foreach (Empire emp in State.World.AllActiveEmpires)
                {
                    if (RaceFuncs.IsRebelOrBandit4(emp.Side)) continue;
                    var raceFlags = State.RaceSettings.GetRaceTraits(emp.ReplacedRace);
                    if (raceFlags != null)
                    {
                        if (raceFlags.Contains(TraitType.Prey))
                            emp.CanVore = false;
                        else
                            emp.CanVore = true;
                    }
                }
            }
        }
    }

    private void DigestionEffect(Prey preyUnit, ActorUnit existingPredator)
    {
        if (Config.BurpOnDigest)
        {
            if (Stomach.Contains(preyUnit) || Stomach2.Contains(preyUnit))
            {
                if (State.Rand.NextDouble() < Config.BurpFraction)
                {
                    _actor.SetBurpMode();
                    State.GameManager.SoundManager.PlayBurp(_actor);
                }
                else
                    State.GameManager.SoundManager.PlayDigest(Location(preyUnit), _actor);
            }
            else
                State.GameManager.SoundManager.PlayDigest(Location(preyUnit), _actor);
        }
        else
        {
            State.GameManager.SoundManager.PlayDigest(Location(preyUnit), _actor);
        }

        IRaceData preyRace = RaceFuncs.GetRace(preyUnit.Unit);

        if (Config.ClothingDiscards)
        {
            if (preyRace.SetupOutput.AllowedMainClothingTypes.Count >= preyUnit.Unit.ClothingType && preyUnit.Unit.ClothingType > 0)
            {
                ClothingId clothingType = preyRace.SetupOutput.AllowedMainClothingTypes[preyUnit.Unit.ClothingType - 1].FixedData.ClothingId;
                int color;
                if (preyRace.SetupOutput.AllowedMainClothingTypes[preyUnit.Unit.ClothingType - 1].FixedData.DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType, color, preyUnit.Unit.Name);
            }

            if (preyRace.SetupOutput.AllowedWaistTypes.Count >= preyUnit.Unit.ClothingType2 && preyUnit.Unit.ClothingType2 > 0)
            {
                ClothingId clothingType2 = preyRace.SetupOutput.AllowedWaistTypes[preyUnit.Unit.ClothingType2 - 1].FixedData.ClothingId;
                int color;
                if (preyRace.SetupOutput.AllowedWaistTypes[preyUnit.Unit.ClothingType2 - 1].FixedData.DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType2, color, preyUnit.Unit.Name);
            }

            if (preyRace.SetupOutput.ExtraMainClothing1Types.Count >= preyUnit.Unit.ClothingExtraType1 && preyUnit.Unit.ClothingExtraType1 > 0)
            {
                ClothingId clothingType = preyRace.SetupOutput.ExtraMainClothing1Types[preyUnit.Unit.ClothingExtraType1 - 1].FixedData.ClothingId;
                int color;
                if (preyRace.SetupOutput.ExtraMainClothing1Types[preyUnit.Unit.ClothingExtraType1 - 1].FixedData.DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType, color, preyUnit.Unit.Name);
            }

            if (preyRace.SetupOutput.ExtraMainClothing2Types.Count >= preyUnit.Unit.ClothingExtraType2 && preyUnit.Unit.ClothingExtraType2 > 0)
            {
                ClothingId clothingType = preyRace.SetupOutput.ExtraMainClothing2Types[preyUnit.Unit.ClothingExtraType2 - 1].FixedData.ClothingId;
                int color;
                if (preyRace.SetupOutput.ExtraMainClothing2Types[preyUnit.Unit.ClothingExtraType2 - 1].FixedData.DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType, color, preyUnit.Unit.Name);
            }

            if (preyRace.SetupOutput.ExtraMainClothing3Types.Count >= preyUnit.Unit.ClothingExtraType3 && preyUnit.Unit.ClothingExtraType3 > 0)
            {
                ClothingId clothingType = preyRace.SetupOutput.ExtraMainClothing3Types[preyUnit.Unit.ClothingExtraType3 - 1].FixedData.ClothingId;
                int color;
                if (preyRace.SetupOutput.ExtraMainClothing3Types[preyUnit.Unit.ClothingExtraType3 - 1].FixedData.DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType, color, preyUnit.Unit.Name);
            }

            if (preyRace.SetupOutput.ExtraMainClothing4Types.Count >= preyUnit.Unit.ClothingExtraType4 && preyUnit.Unit.ClothingExtraType4 > 0)
            {
                ClothingId clothingType = preyRace.SetupOutput.ExtraMainClothing4Types[preyUnit.Unit.ClothingExtraType4 - 1].FixedData.ClothingId;
                int color;
                if (preyRace.SetupOutput.ExtraMainClothing4Types[preyUnit.Unit.ClothingExtraType4 - 1].FixedData.DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType, color, preyUnit.Unit.Name);
            }

            if (preyRace.SetupOutput.ExtraMainClothing5Types.Count >= preyUnit.Unit.ClothingExtraType5 && preyUnit.Unit.ClothingExtraType5 > 0)
            {
                ClothingId clothingType = preyRace.SetupOutput.ExtraMainClothing5Types[preyUnit.Unit.ClothingExtraType5 - 1].FixedData.ClothingId;
                int color;
                if (preyRace.SetupOutput.ExtraMainClothing5Types[preyUnit.Unit.ClothingExtraType5 - 1].FixedData.DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType, color, preyUnit.Unit.Name);
            }
        }
    }

    private void AbsorptionEffect(Prey preyUnit, PreyLocation location)
    {
        if (location == PreyLocation.Stomach || location == PreyLocation.Stomach2)
        {
            if (!Config.BurpOnDigest && State.Rand.NextDouble() < Config.BurpFraction)
            {
                _actor.SetBurpMode();
                State.GameManager.SoundManager.PlayBurp(_actor);
            }
            else
            {
                State.GameManager.SoundManager.PlayAbsorb(location, _actor);
            }

            if (Config.FartOnAbsorb && (location == PreyLocation.Stomach || location == PreyLocation.Stomach2 || location == PreyLocation.Anal) && State.Rand.NextDouble() < Config.FartFraction)
            {
                State.GameManager.SoundManager.PlayFart(_actor);
            }

            if (Config.Scat && preyUnit.ScatDisabled == false)
            {
                State.GameManager.SoundManager.PlayAbsorb(location, _actor);
                if (Equals(preyUnit.Unit.Race, Race.Slime))
                {
                    State.GameManager.TacticalMode.CreateMiscDiscard(GetCurrentLocation(), BoneType.SlimePile, preyUnit.Unit.Name, preyUnit.Unit.AccessoryColor);
                }
                else
                    State.GameManager.TacticalMode.CreateScat(GetCurrentLocation(), new ScatInfo(Unit, preyUnit));
            }
            else
            {
                GenerateBones(preyUnit);
            }
        }
        else if (location == PreyLocation.Balls)
        {
            State.GameManager.SoundManager.PlayAbsorb(location, _actor);
            if (Config.Cumstains)
            {
                if (Equals(Unit.Race, Race.Selicia))
                    State.GameManager.TacticalMode.CreateMiscDiscard(GetCurrentLocation(), BoneType.CumPuddle, preyUnit.Unit.Name, 0);
                else if (Config.CondomsForCv)
                    State.GameManager.TacticalMode.CreateMiscDiscard(GetCurrentLocation(), BoneType.DisposedCondom, preyUnit.Unit.Name);
                else
                    State.GameManager.TacticalMode.CreateMiscDiscard(GetCurrentLocation(), BoneType.CumPuddle, preyUnit.Unit.Name);
            }
        }
        else if (location == PreyLocation.Womb || location == PreyLocation.Breasts || location == PreyLocation.LeftBreast || location == PreyLocation.RightBreast)
        {
            State.GameManager.SoundManager.PlayAbsorb(location, _actor);
            if (Config.Cumstains)
            {
                if (Equals(Unit.Race, Race.Selicia))
                    State.GameManager.TacticalMode.CreateMiscDiscard(GetCurrentLocation(), BoneType.CumPuddle, preyUnit.Unit.Name, 0);
                else
                    State.GameManager.TacticalMode.CreateMiscDiscard(GetCurrentLocation(), BoneType.CumPuddle, preyUnit.Unit.Name);
            }
        }
    }

    private void GenerateBones(Prey preyUnit)
    {
        if (Config.Bones)
        {
            List<BoneInfo> bonesInfos = preyUnit.GetBoneTypes();
            foreach (BoneInfo bonesInfo in bonesInfos)
            {
                State.GameManager.TacticalMode.CreateMiscDiscard(GetCurrentLocation(), bonesInfo.BoneType, bonesInfo.Name, bonesInfo.AccessoryColor);
            }
        }
    }

    internal void UpdateTransition()
    {
        if (_stomachTransition.TransitionTime < _stomachTransition.TransitionLength)
        {
            if (Equals(Unit.Race, Race.FeralFrog) && _actor.IsOralVoring && _actor.IsOralVoringHalfOver == false) return;
            _stomachTransition.TransitionTime += Time.deltaTime;
            VisibleFullness = Mathf.Lerp(_stomachTransition.TransitionStart, _stomachTransition.TransitionEnd, _stomachTransition.TransitionTime / _stomachTransition.TransitionLength);
        }

        if (_ballsTransition.TransitionTime < _ballsTransition.TransitionLength)
        {
            _ballsTransition.TransitionTime += Time.deltaTime;
            BallsFullness = Mathf.Lerp(_ballsTransition.TransitionStart, _ballsTransition.TransitionEnd, _ballsTransition.TransitionTime / _ballsTransition.TransitionLength);
        }

        if (_leftBreastTransition.TransitionTime < _leftBreastTransition.TransitionLength)
        {
            _leftBreastTransition.TransitionTime += Time.deltaTime;
            LeftBreastFullness = Mathf.Lerp(_leftBreastTransition.TransitionStart, _leftBreastTransition.TransitionEnd, _leftBreastTransition.TransitionTime / _leftBreastTransition.TransitionLength);
        }

        if (_rightBreastTransition.TransitionTime < _rightBreastTransition.TransitionLength)
        {
            _rightBreastTransition.TransitionTime += Time.deltaTime;
            RightBreastFullness = Mathf.Lerp(_rightBreastTransition.TransitionStart, _rightBreastTransition.TransitionEnd, _rightBreastTransition.TransitionTime / _rightBreastTransition.TransitionLength);
        }
    }

    internal void UpdateFullness()
    {
        float fullnessFactor = 2.25f / Unit.GetScale(2);
        float fullness = 0;
        float stomachFullness = 0;
        float breastFullness = 0;
        float leftBreastFullness = 0;
        float rightBreastFullness = 0;
        float ballsFullness = 0;
        float tailFullness = 0;
        float stomach2NdFullness = 0;
        float wombFullness = 0;
        float exclusiveStomachFullness = 0;
        foreach (Prey preyUnit in Prey.ToList()) //ToList to cover the rare case it needs to do the pop unit out of itself condition in the bulk function. (It has happened, once at least)
        {
            var location = Location(preyUnit);
            fullness += preyUnit.Actor.Bulk();
            if (location == PreyLocation.Breasts)
            {
                breastFullness += preyUnit.Actor.Bulk();
            }
            else if (location == PreyLocation.Balls)
            {
                ballsFullness += preyUnit.Actor.Bulk();
            }
            else if (location == PreyLocation.Stomach2)
            {
                stomach2NdFullness += preyUnit.Actor.Bulk();
            }
            else if (location == PreyLocation.Tail)
            {
                tailFullness += preyUnit.Actor.Bulk();
            }
            else if (location == PreyLocation.LeftBreast)
            {
                leftBreastFullness += preyUnit.Actor.Bulk();
            }
            else if (location == PreyLocation.RightBreast)
            {
                rightBreastFullness += preyUnit.Actor.Bulk();
            }
            else
            {
                if (location == PreyLocation.Womb)
                    wombFullness += preyUnit.Actor.Bulk();
                else
                    exclusiveStomachFullness += preyUnit.Actor.Bulk();
                stomachFullness += preyUnit.Actor.Bulk();
            }
        }

        float stomachSize = State.RaceSettings.GetStomachSize(Unit.Race);
        if (State.RaceSettings.Exists(Unit.Race))
        {
            stomachSize = State.RaceSettings.GetStomachSize(Unit.Race);
        }

        float newStomach = fullnessFactor * stomachFullness / stomachSize;
        if (newStomach > 0)
            _stomachTransition = new Transition(Math.Abs(newStomach - VisibleFullness) / 4, VisibleFullness, newStomach);
        else
        {
            _stomachTransition = new Transition(0, 0, 0);
            VisibleFullness = 0;
        }

        float newBalls = fullnessFactor * ballsFullness / stomachSize;
        if (newBalls > 0)
            _ballsTransition = new Transition(Math.Abs(newBalls - BallsFullness) / 4, BallsFullness, newBalls);
        else
        {
            _ballsTransition = new Transition(0, 0, 0);
            BallsFullness = 0;
        }

        Fullness = fullnessFactor * fullness / stomachSize;

        TailFullness = fullnessFactor * tailFullness / stomachSize;


        WombFullness = fullnessFactor * wombFullness / stomachSize;

        ExclusiveStomachFullness = fullnessFactor * exclusiveStomachFullness / stomachSize;


        Stomach2NdFullness = fullnessFactor * stomach2NdFullness / stomachSize;
        CombinedStomachFullness = fullnessFactor * (stomach2NdFullness + stomachFullness) / stomachSize;
        if (breastFullness <= 0) breastFullness = -1;
        BreastFullness = breastFullness;

        float newLeftBreast = fullnessFactor * leftBreastFullness / stomachSize;
        float newRightBreast = fullnessFactor * rightBreastFullness / stomachSize;
        if (Config.FairyBvType == FairyBVType.Shared)
        {
            newLeftBreast = (newLeftBreast + newRightBreast) / 2;
            newRightBreast = newLeftBreast;
        }

        if (newLeftBreast > 0)
            _leftBreastTransition = new Transition(Math.Abs(newLeftBreast - LeftBreastFullness) / 4, LeftBreastFullness, newLeftBreast);
        else
        {
            _leftBreastTransition = new Transition(0, 0, 0);
            LeftBreastFullness = 0;
        }

        if (newRightBreast > 0)
            _rightBreastTransition = new Transition(Math.Abs(newRightBreast - RightBreastFullness) / 4, RightBreastFullness, newRightBreast);
        else
        {
            _rightBreastTransition = new Transition(0, 0, 0);
            RightBreastFullness = 0;
        }

        var data = RaceFuncs.GetRace(Unit.Race);
        if (data.SetupOutput.ExtendedBreastSprites == false) _actor.Unit.SetBreastSize(Unit.DefaultBreastSize + (int)(BreastFullness * 8));
        //actor.Unit.SetDickSize(unit.DefaultDickSize + (int)(BallsFullness * 8));
    }

    private bool PlacedPrey(ActorUnit prey)
    {
        Vec2I p = new Vec2I(0, 0);
        for (int i = 0; i < 8; i++)
        {
            p = _actor.GetPos(i);
            if (TacticalUtilities.OpenTile(p.X, p.Y, _actor))
            {
                prey.SetPos(p);
                return true;
            }
        }

        for (int x = -2; x <= 2; x++) //Inefficient but should be rarely needed
        {
            for (int y = -2; y <= 2; y++)
            {
                p.X = _actor.Position.X + x;
                p.Y = _actor.Position.Y + y;
                if (TacticalUtilities.OpenTile(p.X, p.Y, _actor))
                {
                    prey.SetPos(p);
                    return true;
                }
            }
        }

        return false;
    }

    public string GetPreyInformation()
    {
        string ret = "";
        for (int x = 0; x < Prey.Count; x++)
        {
            AddPreyInformation(ref ret, Prey[x], 0);
        }

        return ret;
    }

    private string AddPreyInformation(ref string ret, Prey prey, int indent)
    {
        if (indent > 12) return "";
        if (prey.Unit == null) return "";
        var loc = prey.Location.ToString();
        if (Equals(Unit.Race, Race.Terrorbird) && prey.Location == PreyLocation.Tail) loc = "Crop";
        if (prey.Unit.IsDead == false && Unit.HasTrait(TraitType.DualStomach) && Stomach.Contains(prey))
        {
            if (indent > 0) ret += $"L:{indent} ";
            ret += $"Pushing {prey.Unit.Name} deeper\n";
            if (Config.ExtraTacticalInfo)
            {
                prey.UpdateEscapeRate();
                ret += $" loc: {loc}\n escape: {Math.Round(prey.EscapeRate * 100, 2)}%\n health: {Math.Round(prey.Unit.HealthPct * 100, 1)}%\n";
            }
        }
        else if (prey.Unit.IsDead == false)
        {
            if (indent > 0) ret += $"L:{indent} ";
            ret += $"Digesting {prey.Unit.Name}\n";
            if (Config.ExtraTacticalInfo)
            {
                prey.UpdateEscapeRate();
                ret += $" loc: {loc}\n escape: {Math.Round(prey.EscapeRate * 100, 2)}%\n health: {Math.Round(prey.Unit.HealthPct * 100, 1)}%\n";
            }
        }
        else
        {
            if (indent > 0) ret += $"L:{indent}";
            ret += $"Absorbing {prey.Unit.Name}\n";
            if (Config.ExtraTacticalInfo) ret += $" loc: {loc}\n absorbed: {Math.Round((float)-prey.Unit.Health / prey.Unit.MaxHealth * 100, 1)}%\n";
        }

        if (prey.SubPrey != null)
        {
            foreach (Prey sub in prey.SubPrey)
            {
                AddPreyInformation(ref ret, sub, indent + 1);
            }
        }

        return ret;
    }

    private void AddToStomach(Prey preyref, float v)
    {
        Stomach.Add(preyref);
        if (_actor.UnitSprite != null)
        {
            _actor.UnitSprite.UpdateSprites(_actor, true);
            _actor.UnitSprite.AnimateBellyEnter();
        }
    }

    public bool UsePreferredVore(ActorUnit target)
    {
        List<VoreType> allowedVoreTypes = State.RaceSettings.GetVoreTypes(Unit.Race);

        switch (Unit.PreferredVoreType)
        {
            case VoreType.All:
                break;
            case VoreType.Oral:
                if (allowedVoreTypes.Contains(VoreType.Oral)) return Consume(target, Devour, PreyLocation.Stomach);
                break;
            case VoreType.Unbirth:
                if (allowedVoreTypes.Contains(VoreType.Unbirth) && CanUnbirth(target)) return Consume(target, Unbirth, PreyLocation.Womb);
                break;
            case VoreType.CockVore:
                if (allowedVoreTypes.Contains(VoreType.CockVore) && CanCockVore(target)) return Consume(target, CockVore, PreyLocation.Balls);
                break;
            case VoreType.BreastVore:
                if (allowedVoreTypes.Contains(VoreType.BreastVore) && CanBreastVore(target)) return Consume(target, BreastVore, PreyLocation.Breasts);
                break;
            case VoreType.TailVore:
                if (allowedVoreTypes.Contains(VoreType.TailVore) && CanTailVore(target)) return Consume(target, TailVore, PreyLocation.Tail);
                break;
            case VoreType.Anal:
                if (allowedVoreTypes.Contains(VoreType.Anal) && CanAnalVore(target)) return Consume(target, AnalVore, PreyLocation.Anal);
                break;
        }

        if (State.GameManager.TacticalMode.TurboMode) //When turboing, just pick the fast solution.
            return Consume(target, Devour, PreyLocation.Stomach);

        WeightedList<VoreType> options = new WeightedList<VoreType>();

        List<VoreType> voreTypes = new List<VoreType>();
        if (allowedVoreTypes.Contains(VoreType.Oral) && Config.OralWeight > 0) options.Add(VoreType.Oral, Config.OralWeight);
        if (allowedVoreTypes.Contains(VoreType.Unbirth) && CanUnbirth(target) && Config.UnbirthWeight > 0 && (_actor.BodySize() >= target.BodySize() * 3 || !_actor.Unit.HasTrait(TraitType.TightNethers))) options.Add(VoreType.Unbirth, Config.UnbirthWeight);
        if (allowedVoreTypes.Contains(VoreType.CockVore) && CanCockVore(target) && Config.CockWeight > 0 && (_actor.BodySize() >= target.BodySize() * 3 || !_actor.Unit.HasTrait(TraitType.TightNethers))) options.Add(VoreType.CockVore, Config.CockWeight);
        if (allowedVoreTypes.Contains(VoreType.BreastVore) && CanBreastVore(target) && Config.BreastWeight > 0) options.Add(VoreType.BreastVore, Config.BreastWeight);
        if (allowedVoreTypes.Contains(VoreType.TailVore) && CanTailVore(target) && Config.TailWeight > 0) options.Add(VoreType.TailVore, Config.TailWeight);
        if (allowedVoreTypes.Contains(VoreType.Anal) && CanAnalVore(target) && Config.AnalWeight > 0) options.Add(VoreType.Anal, Config.AnalWeight);

        var type = options.GetResult();

        if (type == VoreType.All || type == VoreType.Oral) return Consume(target, Devour, PreyLocation.Stomach);

        if (type == VoreType.Unbirth) return Consume(target, Unbirth, PreyLocation.Womb);
        if (type == VoreType.CockVore) return Consume(target, CockVore, PreyLocation.Balls);
        if (type == VoreType.BreastVore) return Consume(target, BreastVore, PreyLocation.Breasts);
        if (type == VoreType.TailVore) return Consume(target, TailVore, PreyLocation.Tail);
        if (type == VoreType.Anal) return Consume(target, AnalVore, PreyLocation.Anal);
        return Consume(target, Devour, PreyLocation.Stomach);
    }

    internal bool Devour(ActorUnit target, float delay = 0)
    {
        if (delay == 0)
            return Consume(target, Devour, PreyLocation.Stomach);
        else
            return Consume(target, Devour, PreyLocation.Stomach, delay);
    }

    internal bool Unbirth(ActorUnit target)
    {
        return Consume(target, Unbirth, PreyLocation.Womb);
    }

    internal bool BreastVore(ActorUnit target)
    {
        return Consume(target, BreastVore, PreyLocation.Breasts);
    }

    internal bool CockVore(ActorUnit target)
    {
        return Consume(target, CockVore, PreyLocation.Balls);
    }

    internal bool TailVore(ActorUnit target)
    {
        return Consume(target, TailVore, PreyLocation.Tail);
    }

    internal bool AnalVore(ActorUnit target)
    {
        return Consume(target, AnalVore, PreyLocation.Anal);
    }

    internal bool MagicConsume(Spell spell, ActorUnit target, PreyLocation preyLocation = PreyLocation.Stomach)
    {
        bool sneakAttack = false;
        if (TacticalUtilities.SneakAttackCheck(_actor.Unit, target.Unit) && !_actor.Unit.HasTrait(TraitType.Endosoma))
        {
            _actor.Unit.HiddenFixedSide = false;
            sneakAttack = true;
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<color=purple>{_actor.Unit.Name} sneak-attacked {target.Unit.Name}!</color>");
        }

        if (!Equals(_actor.Unit.GetApparentSide(), target.Unit.GetApparentSide()))
        {
            if (_actor.SidesAttackedThisBattle == null) _actor.SidesAttackedThisBattle = new List<Side>();
            _actor.SidesAttackedThisBattle.Add(target.Unit.GetApparentSide());
        }

        State.GameManager.TacticalMode.AITimer = Config.TacticalVoreDelay;
        if (State.GameManager.CurrentScene == State.GameManager.TacticalMode && State.GameManager.TacticalMode.IsPlayerInControl == false && State.GameManager.TacticalMode.TurboMode == false) State.GameManager.CameraCall(target.Position);
        if (target.Unit == Unit) return false;
        if (target.DefendSpellCheck(spell, _actor, out float chance, mod: sneakAttack ? -0.3f : 0f) == false)
        {
            State.GameManager.TacticalMode.Log.RegisterSpellMiss(Unit, target.Unit, spell.SpellType, chance);
            return false;
        }

        if (target.Bulk() <= FreeCap())
        {
            //actor.SetPredMode(preyType);
            float r = (float)State.Rand.NextDouble();
            float v = target.GetDevourChance(_actor, skillBoost: _actor.Unit.GetStat(Stat.Mind));
            if (r < v)
            {
                if (target.Unit.IsDead == false) AlivePrey++;
                State.GameManager.TacticalMode.TacticalStats.RegisterVore(Unit.Side);

                if (Equals(target.Unit.Side, Unit.Side)) State.GameManager.TacticalMode.TacticalStats.RegisterAllyVore(Unit.Side);
                if (!Equals(target.Unit.FixedSide, Unit.GetApparentSide(target.Unit)) || !Unit.HasTrait(TraitType.Endosoma))
                {
                    Unit.GiveScaledExp(4 * target.Unit.ExpMultiplier, Unit.Level - target.Unit.Level, true);
                }

                target.Visible = false;
                target.Targetable = false;
                State.GameManager.TacticalMode.Log.RegisterSpellHit(Unit, target.Unit, spell.SpellType, 0, chance);
                State.GameManager.TacticalMode.DirtyPack = true;

                target.Movement = 0;
                Prey preyref = new Prey(target, _actor, target.PredatorComponent?.Prey);
                MagicDevour(target, v, preyref, preyLocation);
                AddPrey(preyref);
                UpdateFullness();
                return true;
            }
            else
            {
                State.GameManager.TacticalMode.Log.RegisterSpellMiss(Unit, target.Unit, spell.SpellType, chance);
                return false;
            }
        }

        return false;
    }

    private bool Consume(ActorUnit target, Action<ActorUnit, float, Prey, float> action, PreyLocation preyType, float delay = 0)
    {
        State.GameManager.TacticalMode.AITimer = Config.TacticalVoreDelay;
        int boost = 0;
        if (State.GameManager.CurrentScene == State.GameManager.TacticalMode && State.GameManager.TacticalMode.IsPlayerInControl == false && State.GameManager.TacticalMode.TurboMode == false) State.GameManager.CameraCall(target.Position);
        if (TacticalUtilities.AppropriateVoreTarget(_actor, target) == false)
        {
            return false;
        }

        if (Unit.HasTrait(TraitType.RangedVore))
        {
            int dist = target.Position.GetNumberOfMovesDistance(_actor.Position);
            if (dist > 4) return false;
            boost = -3 * (dist - 1);
        }
        else if (target.Position.GetNumberOfMovesDistance(_actor.Position) > 1)
        {
            return false;
        }

        if (_actor.Movement == 0)
        {
            return false;
        }

        if (target.Unit == Unit) return false;
        if (Unit.CanVore(preyType) != CanVore(preyType, target)) return false;
        if (target.Bulk() <= FreeCap() && (_actor.BodySize() >= target.BodySize() * 3 || !_actor.Unit.HasTrait(TraitType.TightNethers) || (preyType != PreyLocation.Womb && preyType != PreyLocation.Balls)))
        {
            bool bit = false;
            if (target.Unit.HasTrait(TraitType.Dazzle) && target.Surrendered == false)
            {
                float chance = _actor.WillCheckOdds(_actor, target);
                if (State.Rand.NextDouble() < chance)
                {
                    _actor.Movement = 0;
                    _actor.UnitSprite.DisplayDazzle();
                    _actor.Unit.ApplyStatusEffect(StatusEffectType.Shaken, 0.3f, 1);
                    TacticalUtilities.Log.RegisterDazzle(_actor.Unit, target.Unit, chance);
                    return false;
                }
            }

            _actor.SetPredMode(preyType);
            if (TacticalUtilities.SneakAttackCheck(Unit, target.Unit) && !_actor.Unit.HasTrait(TraitType.Endosoma))
            {
                _actor.Unit.HiddenFixedSide = false;
                boost += 3;
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<color=purple>{_actor.Unit.Name} sneak-attacked {target.Unit.Name}!</color>");
            }

            if (!Equals(_actor.Unit.GetApparentSide(), target.Unit.GetApparentSide()))
            {
                if (_actor.SidesAttackedThisBattle == null) _actor.SidesAttackedThisBattle = new List<Side>();
                _actor.SidesAttackedThisBattle.Add(target.Unit.GetApparentSide());
            }

            float r = (float)State.Rand.NextDouble();
            float v = target.GetDevourChance(_actor, skillBoost: boost);
            if (r < v)
            {
                PerformConsume(target, action, preyType, v, delay);
                if (Equals(_actor.Unit.FixedSide, TacticalUtilities.GetMindControlSide(target.Unit)))
                {
                    StatusEffect charm = target.Unit.GetStatusEffect(StatusEffectType.Charmed);
                    if (charm != null)
                    {
                        target.Unit.StatusEffects.Remove(charm); // betrayal dispels charm
                    }
                }

                if (!State.GameManager.TacticalMode.TurboMode) _actor.SetVoreSuccessMode();
                if (Unit.HasTrait(TraitType.Tenacious)) Unit.RemoveTenacious();
                if (Unit.HasTrait(TraitType.FearsomeAppetite))
                {
                    foreach (ActorUnit victim in TacticalUtilities.UnitsWithinTiles(_actor.Position, 3).Where(s => s.Unit.IsEnemyOfSide(Unit.Side)))
                    {
                        victim.Unit.ApplyStatusEffect(StatusEffectType.Shaken, 0.2f, 3);
                    }
                }
            }
            else
            {
                if (!State.GameManager.TacticalMode.TurboMode) _actor.SetVoreFailMode();
                if (_actor.Unit.HasTrait(TraitType.Biter))
                {
                    int oldMp = _actor.Movement;
                    _actor.Attack(target, false, true, canKill: false);
                    _actor.Movement = oldMp;
                    bit = true; //Used because killed units are considered to be surrendered, so this prevents a bite that kills a unit being counted as 2 mp
                }
                else
                {
                    target.UnitSprite.DisplayResist();
                    if (Unit.HasTrait(TraitType.Tenacious)) Unit.AddTenacious();
                }
            }

            StatusEffect hypnotized = target.Unit.GetStatusEffect(StatusEffectType.Hypnotized);
            if (bit == false && (target.Surrendered || (Unit.HasTrait(TraitType.Endosoma) && Equals(target.Unit.FixedSide, Unit.GetApparentSide(target.Unit))) || (hypnotized != null && Equals(hypnotized.Side, _actor.Unit.FixedSide))))
                _actor.Movement = Math.Max(_actor.Movement - 2, 0);
            else
            {
                SpendVoreMp();
            }


            return r < v;
        }

        return false;
    }

    private void SpendVoreMp()
    {
        if (Unit.TraitBoosts.VoreAttacks > 1)
        {
            int movementFraction = 1 + _actor.MaxMovement() / Unit.TraitBoosts.VoreAttacks;
            if (_actor.Movement > movementFraction)
                _actor.Movement -= movementFraction;
            else
                _actor.Movement = 0;
        }
        else
            _actor.Movement = 0;
    }

    private void PerformConsume(ActorUnit target, Action<ActorUnit, float, Prey, float> action, PreyLocation preyType, float odds = 1f, float delay = 0f)
    {
        if (target.Unit.IsDead == false) AlivePrey++;
        State.GameManager.TacticalMode.TacticalStats.RegisterVore(Unit.Side);

        if (ReferenceEquals(target.Unit.Side, Unit.Side)) State.GameManager.TacticalMode.TacticalStats.RegisterAllyVore(Unit.Side);
        target.Visible = false;
        target.Targetable = false;
        State.GameManager.TacticalMode.DirtyPack = true;
        if (!Equals(target.Unit.FixedSide, Unit.GetApparentSide(target.Unit)) || !Unit.HasTrait(TraitType.Endosoma))
        {
            Unit.GiveScaledExp(4 * target.Unit.ExpMultiplier, Unit.Level - target.Unit.Level, true);
        }

        target.Movement = 0;
        Prey preyref = new Prey(target, _actor, target.PredatorComponent?.Prey);
        action(target, odds, preyref, delay);
        AddPrey(preyref);
        UpdateFullness();
    }

    private void MagicDevour(ActorUnit target, float v, Prey preyref, PreyLocation preyLocation)
    {
        if (Equals(_actor.Unit.Side, target.Unit.GetApparentSide()) && !_actor.Unit.HasTrait(TraitType.Endosoma))
        {
            _actor.Unit.HiddenFixedSide = false;
        }

        //State.GameManager.SoundManager.PlaySwallow(PreyLocation.stomach, actor);
        //TacticalUtilities.Log.RegisterVore(unit, target.Unit, v);
        if (Equals(_actor.Unit.FixedSide, TacticalUtilities.GetMindControlSide(target.Unit)))
        {
            StatusEffect charm = target.Unit.GetStatusEffect(StatusEffectType.Charmed);
            if (charm != null)
            {
                target.Unit.StatusEffects.Remove(charm); // betrayal dispels charm
            }
        }

        switch (preyLocation)
        {
            case PreyLocation.Womb:
                AddToWomb(preyref, 1f);
                break;
            case PreyLocation.Balls:
                AddToBalls(preyref, 1f);
                break;
            case PreyLocation.Anal:
                AddToStomach(preyref, 1f);
                break;
            case PreyLocation.Breasts:
                var data = RaceFuncs.GetRace(Unit.Race);
                if (data.SetupOutput.ExtendedBreastSprites)
                {
                    if (Config.FairyBvType == FairyBVType.Picked && State.GameManager.TacticalMode.IsPlayerInControl)
                    {
                        var box = State.GameManager.CreateDialogBox();
                        box.SetData(() =>
                        {
                            RightBreast.Add(preyref);
                            UpdateFullness();
                        }, "Right", "Left", "Which breast should the prey be put in? (from your pov)", () =>
                        {
                            LeftBreast.Add(preyref);
                            UpdateFullness();
                        });
                        return;
                    }

                    if (LeftBreastFullness < RightBreastFullness || State.Rand.Next(2) == 0)
                        LeftBreast.Add(preyref);
                    else
                        RightBreast.Add(preyref);
                }
                else
                    Breasts.Add(preyref);

                break;
            default:
                AddToStomach(preyref, 1f);
                break;
        }

        AddToStomach(preyref, v);
    }

    private void Devour(ActorUnit target, float v, Prey preyref, float delay)
    {
        if (delay > 0)
            MiscUtilities.DelayedInvoke(() => State.GameManager.SoundManager.PlaySwallow(PreyLocation.Stomach, _actor), delay);
        else
            State.GameManager.SoundManager.PlaySwallow(PreyLocation.Stomach, _actor);
        TacticalUtilities.Log.RegisterVore(Unit, target.Unit, v);
        AddToStomach(preyref, v);
    }

    private void Unbirth(ActorUnit target, float v, Prey preyref, float delay)
    {
        State.GameManager.SoundManager.PlaySwallow(PreyLocation.Womb, _actor);
        TacticalUtilities.Log.RegisterUnbirth(Unit, target.Unit, v);
        AddToWomb(preyref, v);
    }

    private void BreastVore(ActorUnit target, float v, Prey preyref, float delay)
    {
        State.GameManager.SoundManager.PlaySwallow(PreyLocation.Breasts, _actor);
        TacticalUtilities.Log.RegisterBreastVore(Unit, target.Unit, v);
        var data = RaceFuncs.GetRace(Unit.Race);
        if (data.SetupOutput.ExtendedBreastSprites)
        {
            if (Config.FairyBvType == FairyBVType.Picked && State.GameManager.TacticalMode.IsPlayerInControl)
            {
                var box = State.GameManager.CreateDialogBox();
                box.SetData(() =>
                {
                    RightBreast.Add(preyref);
                    UpdateFullness();
                }, "Right", "Left", "Which breast should the prey be put in? (from your pov)", () =>
                {
                    LeftBreast.Add(preyref);
                    UpdateFullness();
                });
                return;
            }

            if (LeftBreastFullness < RightBreastFullness || State.Rand.Next(2) == 0)
                LeftBreast.Add(preyref);
            else
                RightBreast.Add(preyref);
        }
        else
            Breasts.Add(preyref);
    }

    private void CockVore(ActorUnit target, float v, Prey preyref, float delay)
    {
        State.GameManager.SoundManager.PlaySwallow(PreyLocation.Balls, _actor);
        TacticalUtilities.Log.RegisterCockVore(Unit, target.Unit, v);
        Balls.Add(preyref);
    }

    private void TailVore(ActorUnit target, float v, Prey preyref, float delay)
    {
        State.GameManager.SoundManager.PlaySwallow(PreyLocation.Stomach, _actor);
        TacticalUtilities.Log.RegisterTailVore(Unit, target.Unit, v);
        Tail.Add(preyref);
    }

    private void AnalVore(ActorUnit target, float v, Prey preyref, float delay)
    {
        State.GameManager.SoundManager.PlaySwallow(PreyLocation.Anal, _actor);
        TacticalUtilities.Log.RegisterAnalVore(Unit, target.Unit, v);
        Stomach.Add(preyref);
    }

    public bool CanFeed()
    {
        if (Config.KuroTenkoEnabled && (Config.FeedingType == FeedingType.Both || Config.FeedingType == FeedingType.BreastfeedOnly))
        {
            var race = RaceFuncs.GetRace(_actor.Unit.Race);
            if (race.SetupOutput.ExtendedBreastSprites)
            {
                foreach (Prey preyUnit in LeftBreast)
                {
                    if (preyUnit.Unit.IsDead) return true;
                }

                foreach (Prey preyUnit in RightBreast)
                {
                    if (preyUnit.Unit.IsDead) return true;
                }
            }
            else
            {
                foreach (Prey preyUnit in Breasts)
                {
                    if (preyUnit.Unit.IsDead) return true;
                }
            }
        }

        return false;
    }

    public bool CanFeedCum()
    {
        if (Config.KuroTenkoEnabled && (Config.FeedingType == FeedingType.Both || Config.FeedingType == FeedingType.CumFeedOnly))
        {
            foreach (Prey preyUnit in Balls)
            {
                if (preyUnit.Unit.IsDead)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool CanSuckle()
    {
        return Config.KuroTenkoEnabled && Config.FeedingType != FeedingType.None;
    }

    public bool CanTransfer()
    {
        if (Config.TransferAllowed == false || Config.KuroTenkoEnabled == false) return false;
        return GetTransfer() != null;
    }

    public bool CanVoreSteal(ActorUnit target)
    {
        if (Config.TransferAllowed == false || Config.KuroTenkoEnabled == false) return false;
        return GetVoreSteal(target) != null;
    }

    private Prey GetTransfer()
    {
        foreach (Prey preyUnit in Balls)
        {
            //Testing was done with isDead being required, transferring live prey may have its own issues
            //if (preyUnit.Unit.IsDead)
            //{
            return preyUnit;
            //}
        }

        return null;
    }

    public float GetTransferBulk()
    {
        foreach (Prey preyUnit in Balls)
        {
            //Testing was done with isDead being required, transferring live prey may have its own issues
            //if (preyUnit.Unit.IsDead)
            //{
            return preyUnit.Actor.Bulk();
            //}
        }

        return 0;
    }

    public float GetSuckleChance(ActorUnit target, bool includeSecondaries = false)
    {
        if (target.Surrendered || Equals(_actor.Unit.GetApparentSide(), target.Unit.FixedSide)) return 1f;
        float predVoracity = Mathf.Pow(15 + _actor.Unit.GetStat(Stat.Voracity), 1.5f);
        float predStrength = Mathf.Pow(15 + _actor.Unit.GetStat(Stat.Strength), 1.5f);
        float preyStrength = Mathf.Pow(15 + target.Unit.GetStat(Stat.Strength), 1.5f);
        float preyWill = Mathf.Pow(15 + target.Unit.GetStat(Stat.Will), 1.5f);
        float attackerScore = (predVoracity + 2 * predStrength) * (_actor.Unit.HealthPct + 1) * (30 + _actor.BodySize()) / 16 / (1 + 2 * Mathf.Pow(_actor.PredatorComponent.UsageFraction, 2));
        float defenderScore = 20 + 2 * (preyStrength + 2 * preyWill) * target.Unit.HealthPct * target.Unit.HealthPct * ((10 + target.BodySize()) / 2);

        float odds = attackerScore / (attackerScore + defenderScore) * 100;

        odds *= target.Unit.TraitBoosts.FlatHitReduction;

        if (includeSecondaries)
        {
            if (target.Unit.HasTrait(TraitType.Dazzle))
            {
                odds *= 1 - target.WillCheckOdds(_actor, target);
            }
        }

        return odds / 100;
    }

    public int[] GetSuckle(ActorUnit target)
    {
        if (_actor.Unit.Predator == false || target.Unit.Predator == false) return new int[] { 0, 0 };
        if (Config.KuroTenkoEnabled && Config.FeedingType != FeedingType.None)
        {
            if (target.PredatorComponent.PreyInLocation(PreyLocation.Breasts, false) + target.PredatorComponent.PreyInLocation(PreyLocation.LeftBreast, false) + target.PredatorComponent.PreyInLocation(PreyLocation.RightBreast, false) + target.PredatorComponent.PreyInLocation(PreyLocation.Balls, false) == 0) return new int[] { 0, 0 };
            if (Config.SucklingPermission == SucklingPermission.AlwaysBreast || Config.FeedingType == FeedingType.BreastfeedOnly) return target.PredatorComponent.CalcFeedValue(_actor, "breast").Item1;
            if (Config.SucklingPermission == SucklingPermission.AlwaysCock || Config.FeedingType == FeedingType.CumFeedOnly) return target.PredatorComponent.CalcFeedValue(_actor, "cock").Item1;
            if (Config.SucklingPermission == SucklingPermission.Auto || Config.SucklingPermission == SucklingPermission.Random) return target.PredatorComponent.CalcFeedValue(_actor, "any").Item1;
        }

        return new int[] { 0, 0, 0 };
    }

    public bool Suckle(ActorUnit target)
    {
        if (target.Position.GetNumberOfMovesDistance(_actor.Position) > 1 || target.Unit.Predator == false)
        {
            return false;
        }

        float r = (float)State.Rand.NextDouble();
        float v = GetSuckleChance(target);
        int[] suckle = { 0, 0, 0 };
        if (Config.SucklingPermission == SucklingPermission.AlwaysBreast)
            suckle = target.PredatorComponent.CalcFeedValue(_actor, "breast").Item1;
        else if (Config.SucklingPermission == SucklingPermission.AlwaysCock)
            suckle = target.PredatorComponent.CalcFeedValue(_actor, "cock").Item1;
        else if (Config.SucklingPermission == SucklingPermission.Random)
            if (State.Rand.Next(2) == 0)
                suckle = target.PredatorComponent.CalcFeedValue(_actor, "breast").Item1;
            else
                suckle = target.PredatorComponent.CalcFeedValue(_actor, "cock").Item1;
        else
            suckle = target.PredatorComponent.CalcFeedValue(_actor, "any").Item1;
        if (suckle.Length < 3) return false;
        int halfMovement = 1 + _actor.MaxMovement() / 2;
        if (_actor.Movement > halfMovement)
            _actor.Movement -= halfMovement;
        else
            _actor.Movement = 0;
        int actorMaxHeal = _actor.Unit.MaxHealth - _actor.Unit.Health;
        if (r > v)
        {
            switch (suckle[2])
            {
                case 0:
                    TacticalUtilities.Log.RegisterSuckleFail(Unit, target.Unit, PreyLocation.Breasts, v);
                    break;
                case 1:
                    TacticalUtilities.Log.RegisterSuckleFail(Unit, target.Unit, PreyLocation.LeftBreast, v);
                    break;
                case 2:
                    TacticalUtilities.Log.RegisterSuckleFail(Unit, target.Unit, PreyLocation.RightBreast, v);
                    break;
                case 3:
                    TacticalUtilities.Log.RegisterSuckleFail(Unit, target.Unit, PreyLocation.Balls, v);
                    break;
            }

            return false;
        }
        else
        {
            _actor.Unit.GiveRawExp(suckle[1]);
            _actor.UnitSprite.DisplayDamage(suckle[1], false, true);
            switch (suckle[2])
            {
                case 0:
                    TacticalUtilities.Log.RegisterSuckle(Unit, target.Unit, PreyLocation.Breasts, v);
                    TacticalUtilities.Log.RegisterHeal(_actor.Unit, suckle, "breastfeeding", target.Unit.HasTrait(TraitType.Honeymaker) ? "honey" : "none");
                    target.DigestCheck("breastfeed");
                    break;
                case 1:
                    TacticalUtilities.Log.RegisterSuckle(Unit, target.Unit, PreyLocation.LeftBreast, v);
                    TacticalUtilities.Log.RegisterHeal(_actor.Unit, suckle, "breastfeeding", target.Unit.HasTrait(TraitType.Honeymaker) ? "honey" : "none");
                    target.DigestCheck("breastfeed");
                    break;
                case 2:
                    TacticalUtilities.Log.RegisterSuckle(Unit, target.Unit, PreyLocation.RightBreast, v);
                    TacticalUtilities.Log.RegisterHeal(_actor.Unit, suckle, "breastfeeding", target.Unit.HasTrait(TraitType.Honeymaker) ? "honey" : "none");
                    target.DigestCheck("breastfeed");
                    break;
                case 3:
                    TacticalUtilities.Log.RegisterSuckle(Unit, target.Unit, PreyLocation.Balls, v);
                    TacticalUtilities.Log.RegisterHeal(_actor.Unit, suckle, "cumfeeding");
                    target.DigestCheck("cumfeed");
                    break;
            }

            _actor.Unit.Heal(suckle[0]);
            if (target.Unit.HasTrait(TraitType.Corruption))
            {
                _actor.AddCorruption(Unit.GetStatTotal() / 10, Unit.FixedSide);
            }

            if (suckle[0] != 0) _actor.UnitSprite.DisplayDamage(-suckle[0]);
            _actor.UnitSprite.UpdateSprites(target, true);
            target.PredatorComponent.UpdateFullness();
            return true;
        }
    }

    public float GetVoreStealChance(ActorUnit attacker, bool includeSecondaries = false)
    {
        Prey prey = GetVoreSteal(_actor);
        if (prey == null)
        {
            return 0f;
        }

        if (attacker.Unit.Predator == false)
        {
            Debug.Log("This shouldn't have happened");
            return 0;
        }

        if (_actor.Surrendered || Equals(_actor.Unit.FixedSide, attacker.Unit.GetApparentSide(_actor.Unit))) return 1f;

        if (prey.Unit.HasTrait(TraitType.Irresistable)) return 1f;

        float recipientVoracity = Mathf.Pow(15 + attacker.Unit.GetStat(Stat.Voracity), 1.5f);
        float recipientStrength = Mathf.Pow(15 + attacker.Unit.GetStat(Stat.Strength), 1.5f);
        float donorVoracity = Mathf.Pow(15 + _actor.Unit.GetStat(Stat.Voracity), 1.5f);
        float donorStrength = Mathf.Pow(15 + _actor.Unit.GetStat(Stat.Strength), 1.5f);
        float donorWill = Mathf.Pow(15 + _actor.Unit.GetStat(Stat.Will), 1.5f);
        float attackerScore = (recipientVoracity + recipientStrength) * (attacker.Unit.HealthPct + 1) * (30 + attacker.BodySize()) / 16 / (1 + 2 * Mathf.Pow(attacker.PredatorComponent.UsageFraction, 2)) - prey.Actor.Bulk();
        float defenderHealthPct = _actor.Unit.HealthPct;
        float defenderScore = 20 + 2 * (donorStrength + 1.5f * donorVoracity + 2 * donorWill) * defenderHealthPct * defenderHealthPct * ((10 + _actor.BodySize()) / 2);

        defenderScore /= _actor.Unit.TraitBoosts.Incoming.VoreOddsMult;
        attackerScore *= attacker.Unit.TraitBoosts.Outgoing.VoreOddsMult;

        if (prey.Unit.HasTrait(TraitType.GelatinousBody)) attackerScore *= 1.15f;

        if (prey.Unit.HasTrait(TraitType.MetalBody)) attackerScore *= .7f;

        if (prey.Unit.HasTrait(TraitType.Slippery)) attackerScore *= 1.5f;

        if (attacker.Unit.HasTrait(TraitType.AllOutFirstStrike) && attacker.HasAttackedThisCombat == false) attackerScore *= 3.25f;

        float odds = attackerScore / (attackerScore + defenderScore) * 100;

        odds *= _actor.Unit.TraitBoosts.FlatHitReduction;

        if (includeSecondaries)
        {
            if (_actor.Unit.HasTrait(TraitType.Dazzle))
            {
                odds *= 1 - _actor.WillCheckOdds(attacker, _actor);
            }
        }

        return odds / 100;
    }

    private Prey GetVoreSteal(ActorUnit target)
    {
        if (_actor.Unit.Predator == false || target.Unit.Predator == false) return null;

        if (target.Position.GetNumberOfMovesDistance(_actor.Position) > 1)
        {
            return null;
        }

        if (State.RaceSettings.GetVoreTypes(_actor.Unit.Race).Contains(VoreType.Oral) || State.RaceSettings.GetVoreTypes(_actor.Unit.Race).Contains(VoreType.Unbirth))
        {
            foreach (Prey preyUnit in target.PredatorComponent.Balls)
            {
                if (!preyUnit.Unit.IsDead)
                {
                    return preyUnit;
                }
            }
        }

        var data = RaceFuncs.GetRace(target.Unit.Race);
        if (State.RaceSettings.GetVoreTypes(_actor.Unit.Race).Contains(VoreType.Oral))
        {
            if (data.SetupOutput.ExtendedBreastSprites)
            {
                foreach (Prey preyUnit in target.PredatorComponent.LeftBreast)
                {
                    if (!preyUnit.Unit.IsDead)
                    {
                        return preyUnit;
                    }
                }

                foreach (Prey preyUnit in target.PredatorComponent.RightBreast)
                {
                    if (!preyUnit.Unit.IsDead)
                    {
                        return preyUnit;
                    }
                }
            }
            else
            {
                foreach (Prey preyUnit in target.PredatorComponent.Breasts)
                {
                    if (!preyUnit.Unit.IsDead)
                    {
                        return preyUnit;
                    }
                }
            }
        }

        return null;
    }

    private void TransferFail(Prey transfer)
    {
        FreePrey(transfer, true);
        foreach (Prey preyUnit in Balls)
        {
            if (preyUnit.Unit.IsDead == false)
            {
                FreePrey(preyUnit, true);
                break;
            }
        }

        TacticalUtilities.Log.RegisterTransferFail(Unit, transfer.Unit, .3f);
    }

    public float TransferBulk()
    {
        Prey transfer = GetTransfer();
        if (transfer == null)
        {
            return 999f;
        }

        return transfer.Actor.Bulk();
    }

    public float StealBulk()
    {
        Prey transfer = GetVoreSteal(_actor);
        if (transfer == null)
        {
            return 999f;
        }

        return transfer.Actor.Bulk();
    }

    private Tuple<int[], List<Prey>> CalcFeedValue(ActorUnit target, string feedType)
    {
        int healthUp = Math.Max(_actor.Unit.MaxHealth / 16, 1);
        int healthUpCap = target.Unit.MaxHealth - target.Unit.Health;
        int expUp = 0;
        int location = 0;
        int[] tempVals = { 0, 0, 0 };
        var deadPrey = new List<Prey>();
        if (feedType == "breast" || feedType == "any")
        {
            var data = RaceFuncs.GetRace(Unit.Race);
            if (data.SetupOutput.ExtendedBreastSprites)
            {
                int leftBreastHealth = 0;
                int rightBreastHealth = 0;
                int leftBreastExp = 0;
                int rightBreastExp = 0;
                foreach (Prey preyUnit in _actor.PredatorComponent.LeftBreast.ToList())
                    if (preyUnit.Unit.IsDead)
                    {
                        deadPrey.Add(preyUnit);
                        leftBreastHealth += HeatlhBoostCalc(preyUnit, target);
                        leftBreastExp += ExpBoostCalc(preyUnit);
                    }

                foreach (Prey preyUnit in _actor.PredatorComponent.RightBreast.ToList())
                    if (preyUnit.Unit.IsDead)
                    {
                        deadPrey.Add(preyUnit);
                        rightBreastHealth += HeatlhBoostCalc(preyUnit, target);
                        rightBreastExp += ExpBoostCalc(preyUnit);
                    }

                if (target.Unit.HealthPct < 1.0f)
                {
                    if (leftBreastHealth >= rightBreastHealth)
                    {
                        healthUp += leftBreastHealth;
                        expUp += leftBreastExp;
                        if (feedType == "any") location = 1;
                    }
                    else
                    {
                        healthUp += rightBreastHealth;
                        expUp += rightBreastExp;
                        if (feedType == "any") location = 2;
                    }
                }
                else
                {
                    if (leftBreastExp >= rightBreastExp)
                    {
                        healthUp += leftBreastHealth;
                        expUp += leftBreastExp;
                        if (feedType == "any") location = 1;
                    }
                    else
                    {
                        healthUp += rightBreastHealth;
                        expUp += rightBreastExp;
                        if (feedType == "any") location = 2;
                    }
                }
            }
            else
                foreach (Prey preyUnit in _actor.PredatorComponent.Breasts.ToList())
                    if (preyUnit.Unit.IsDead)
                    {
                        deadPrey.Add(preyUnit);
                        healthUp += HeatlhBoostCalc(preyUnit, target);
                        expUp += ExpBoostCalc(preyUnit);
                    }
        }

        if (feedType == "any")
        {
            tempVals[0] = healthUp;
            tempVals[1] = expUp;
            tempVals[2] = location;
        }

        if (feedType == "cock" || feedType == "any")
        {
            foreach (Prey preyUnit in _actor.PredatorComponent.Balls.ToList())
                if (preyUnit.Unit.IsDead)
                {
                    deadPrey.Add(preyUnit);
                    healthUp += HeatlhBoostCalc(preyUnit, target);
                    expUp += ExpBoostCalc(preyUnit);
                }

            if (feedType == "any") location = 3;
        }

        if (feedType == "any")
        {
            if (target.Unit.HealthPct < 1.0f)
            {
                if (tempVals[0] >= healthUp)
                {
                    healthUp = tempVals[0];
                    expUp = tempVals[1];
                    location = tempVals[2];
                }
            }
            else
            {
                if (tempVals[1] >= expUp)
                {
                    healthUp = tempVals[0];
                    expUp = tempVals[1];
                    location = tempVals[2];
                }
            }
        }

        healthUp = Math.Min(healthUp, healthUpCap);
        if (!Config.OverhealExp) expUp = 0;
        return new Tuple<int[], List<Prey>>(new int[] { healthUp, expUp, location }, deadPrey);
    }

    private int HeatlhBoostCalc(Prey preyUnit, ActorUnit target)
    {
        return Mathf.RoundToInt(preyUnit.Unit.MaxHealth / 10 + preyUnit.Unit.MaxHealth / 10 * 9 / 20 * Math.Max(0, preyUnit.Unit.Level - target.Unit.Level / (_actor.Unit.HasTrait(TraitType.WetNurse) ? 2 : 1)) * preyUnit.Unit.TraitBoosts.Outgoing.Nutrition);
    }

    private int ExpBoostCalc(Prey preyUnit)
    {
        int cap = State.RaceSettings.GetBodySize(preyUnit.Actor.Unit.Race);
        int bonus = Mathf.Clamp(preyUnit.Unit.Level, 1, cap);
        if (preyUnit.Predator.Unit.HasTrait(TraitType.Honeymaker)) bonus = Mathf.RoundToInt(bonus * (1f + Mathf.Min((float)preyUnit.Unit.Level, (float)cap) / cap));
        return bonus;
        // return Mathf.RoundToInt(Mathf.Clamp(preyUnit.Unit.Experience / 20, 0, (State.RaceSettings.GetBodySize(preyUnit.Actor.Unit.Race) * ((preyUnit.Predator.Unit.HasTrait(Traits.Honeymaker) ? 1 : 0) + 1))) * preyUnit.Unit.TraitBoosts.Outgoing.Nutrition);
    }

    public bool Feed(ActorUnit target)
    {
        if (!Equals(target.Unit.Side, _actor.Unit.Side) || target.Surrendered || target.Position.GetNumberOfMovesDistance(_actor.Position) > 1 || _actor.Movement == 0 || !CanFeed()) return false;
        Tuple<int[], List<Prey>> digestion = CalcFeedValue(target, "breast");
        int[] nurseHeal = digestion.Item1;
        Prey deadPrey = digestion.Item2[0];
        TacticalUtilities.Log.RegisterFeed(Unit, target.Unit, deadPrey.Unit, 1f);
        target.Unit.GiveRawExp(nurseHeal[1]);
        if (nurseHeal[0] > 0)
        {
            target.Unit.Heal(nurseHeal[0]);
            target.UnitSprite.DisplayDamage(-nurseHeal[0]);
        }
        else if (nurseHeal[1] > 0)
        {
            target.UnitSprite.DisplayDamage(nurseHeal[1], false, true);
        }

        target.UnitSprite.UpdateSprites(_actor, true);
        if (nurseHeal[0] + nurseHeal[1] > 0) TacticalUtilities.Log.RegisterHeal(target.Unit, nurseHeal, "breastfeeding", _actor.Unit.HasTrait(TraitType.Honeymaker) ? "honey" : "none");
        _actor.DigestCheck("breastfeed");
        if (Unit.HasTrait(TraitType.Corruption))
        {
            target.AddCorruption(Unit.GetStatTotal() / 10, Unit.FixedSide);
        }

        int halfMovement = 1 + _actor.MaxMovement() / (2 + (_actor.Unit.HasTrait(TraitType.WetNurse) ? 1 : 0));
        if (_actor.Movement > halfMovement)
            _actor.Movement -= halfMovement;
        else
            _actor.Movement = 0;
        UpdateFullness();
        return true;
    }

    public bool FeedCum(ActorUnit target)
    {
        if (!Equals(target.Unit.Side, _actor.Unit.Side) || target.Surrendered || target.Position.GetNumberOfMovesDistance(_actor.Position) > 1 || _actor.Movement == 0 || !CanFeedCum()) return false;
        Tuple<int[], List<Prey>> digestion = CalcFeedValue(target, "cock");
        int[] nurseHeal = digestion.Item1;
        Prey deadPrey = digestion.Item2[0];
        TacticalUtilities.Log.RegisterCumFeed(Unit, target.Unit, deadPrey.Unit, 1f);
        target.Unit.GiveRawExp(nurseHeal[1]);
        if (nurseHeal[0] > 0)
        {
            target.Unit.Heal(nurseHeal[0]);
            target.UnitSprite.DisplayDamage(-nurseHeal[0]);
        }
        else if (nurseHeal[1] > 0)
        {
            target.UnitSprite.DisplayDamage(nurseHeal[1], false, true);
        }

        target.UnitSprite.UpdateSprites(_actor, true);
        if (nurseHeal[0] + nurseHeal[1] > 0) TacticalUtilities.Log.RegisterHeal(target.Unit, nurseHeal, "cumfeeding");
        _actor.DigestCheck("cumfeed");
        if (Unit.HasTrait(TraitType.Corruption))
        {
            target.AddCorruption(Unit.GetStatTotal() / 10, Unit.FixedSide);
        }

        int quarterMovement = 1 + _actor.MaxMovement() / (4 + (_actor.Unit.HasTrait(TraitType.WetNurse) ? 1 : 0));
        if (_actor.Movement > quarterMovement)
            _actor.Movement -= quarterMovement;
        else
            _actor.Movement = 0;
        UpdateFullness();
        return true;
    }

    private bool Transfer(ActorUnit target, Prey preyUnit)
    {
        float r = (float)State.Rand.NextDouble();
        float v = target.GetSpecialChance(SpecialAction.CockVore);
        if (target.Position.GetNumberOfMovesDistance(_actor.Position) > 1)
        {
            return false;
        }

        if (r > v && !preyUnit.Unit.IsDead)
        {
            return false;
        }

        Prey preyref = new Prey(preyUnit.Actor, target, preyUnit.Actor.PredatorComponent?.Prey);
        if (target.Unit.HasVagina && RaceParameters.GetTraitData(target.Unit).AllowedVoreTypes.Contains(VoreType.Unbirth))
        {
            var box = State.GameManager.CreateDialogBox();
            box.SetData(() => TransferFinalize(target, preyUnit, preyref, PreyLocation.Stomach), "Stomach", "Womb", "Transfer the prey to the womb or stomach?", () => TransferFinalize(target, preyUnit, preyref, PreyLocation.Womb));
        }
        // Hopefully there's no circumstance where a vaginal trasfer is possible but an oral isn't
        //else if (target.Unit.HasBreasts && !target.Unit.HasDick)
        //{
        //    TransferFinalize(target, preyUnit, preyref, PreyLocation.womb);
        //}
        else
        {
            TransferFinalize(target, preyUnit, preyref, PreyLocation.Stomach);
        }

        if (target.UnitSprite != null)
        {
            target.UnitSprite.UpdateSprites(_actor, true);
            target.UnitSprite.AnimateBellyEnter();
        }

        return true;
    }

    private bool TransferFinalize(ActorUnit recipient, Prey preyUnit, Prey preyref, PreyLocation destination)
    {
        // Empowerment handling
        if (destination == PreyLocation.Womb && recipient.Unit.HasVagina && Config.CumGestation)
        {
            bool alreadyPregnant = false;
            ActorUnit alreadyChild = null;
            foreach (Prey existingPrey in recipient.PredatorComponent.Womb)
            {
                if ((existingPrey.Unit.IsDead && !Equals(existingPrey.Unit.Side, recipient.Unit.Side)) || (!existingPrey.Unit.IsDead && Equals(existingPrey.Unit.Side, recipient.Unit.Side)))
                {
                    alreadyPregnant = true;
                    alreadyChild = existingPrey.Actor;
                }
            }

            if (alreadyPregnant)
            {
                recipient.PredatorComponent.BirthStatBoost += preyUnit.Unit.Level;
                recipient.PredatorComponent.RemovePrey(preyUnit);
                _actor.PredatorComponent.RemovePrey(preyUnit);
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{_actor.Unit.Name}</b> pumps what remains of <b>{preyUnit.Unit.Name}</b> into <b>{recipient.Unit.Name}</b>'s womb, providing nutrients to strengthen <b>{alreadyChild.Unit.Name}</b>.");
                if (Unit.HasTrait(TraitType.Corruption) || preyUnit.Unit.HasTrait(TraitType.Corruption))
                {
                    alreadyChild.Unit.HiddenFixedSide = true;
                    alreadyChild.SidesAttackedThisBattle = new List<Side>();
                    alreadyChild.Unit.RemoveTrait(TraitType.Corruption);
                    alreadyChild.Unit.AddPermanentTrait(TraitType.Corruption);
                }

                if (!alreadyChild.Unit.HasTrait(TraitType.Corruption)) alreadyChild.Unit.FixedSide = Unit.FixedSide;
                UpdateFullness();
                Unit.UpdateSpells();
                return true;
            }
        }

        if (preyUnit.Unit.IsDead == false)
        {
            recipient.PredatorComponent.AlivePrey++;
            _actor.PredatorComponent.AlivePrey--;
        }
        else if (Unit.HasTrait(TraitType.Corruption))
        {
            recipient.AddCorruption(preyUnit.Unit.GetStatTotal() / 2, Unit.FixedSide);
        }
        else if (preyUnit.Unit.HasTrait(TraitType.Corruption)) recipient.AddCorruption(preyUnit.Unit.GetStatTotal() / 2, preyUnit.Unit.FixedSide);

        switch (destination)
        {
            case PreyLocation.Womb:
                recipient.PredatorComponent?.AddToWomb(preyref, 1.0f);
                TacticalUtilities.Log.RegisterTransferSuccess(Unit, recipient.Unit, preyUnit.Unit, 1.0f, PreyLocation.Womb);
                break;
            case PreyLocation.Stomach:
                recipient.PredatorComponent.AddToStomach(preyref, 1.0f);
                TacticalUtilities.Log.RegisterTransferSuccess(Unit, recipient.Unit, preyUnit.Unit, 1.0f, PreyLocation.Stomach);
                break;
            default:
                recipient.PredatorComponent.AddToStomach(preyref, 1.0f);
                TacticalUtilities.Log.RegisterTransferSuccess(Unit, recipient.Unit, preyUnit.Unit, 1.0f, PreyLocation.Stomach);
                break;
        }

        recipient.PredatorComponent.AddPrey(preyref);
        recipient.Unit.GiveScaledExp(4 * preyUnit.Unit.ExpMultiplier, recipient.Unit.Level - preyUnit.Unit.Level, true);
        if (preyUnit.Unit.IsDead && Config.NoScatForDeadTransfers) preyUnit.ScatDisabled = true;
        recipient.PredatorComponent.UpdateFullness();
        _actor.PredatorComponent.RemovePrey(preyUnit);
        UpdateFullness();
        return true;
    }

    public bool TransferAttempt(ActorUnit target)
    {
        if (_actor.Unit.Predator == false || target.Unit.Predator == false) return false;
        if (!Equals(target.Unit.Side, _actor.Unit.Side) || target.Surrendered)
        {
            return false;
        }

        if (target.Position.GetNumberOfMovesDistance(_actor.Position) > 1)
        {
            return false;
        }

        if (_actor.Movement == 0)
        {
            return false;
        }

        if (!CanTransfer())
        {
            return false;
        }

        Prey transfer = GetTransfer();
        if (target.PredatorComponent.FreeCap() < transfer.Actor.Bulk() && !(target.Unit == _actor.Unit))
        {
            return false;
        }

        if (!Transfer(target, transfer)) TransferFail(transfer);
        int thirdMovement = (int)Math.Ceiling(_actor.MaxMovement() / 3.0f);
        if (_actor.Movement > thirdMovement)
            _actor.Movement -= thirdMovement;
        else
            _actor.Movement = 0;
        return true;
    }

    public bool VoreStealAttempt(ActorUnit target)
    {
        if (target.Position.GetNumberOfMovesDistance(_actor.Position) > 1)
        {
            return false;
        }

        if (_actor.Movement == 0)
        {
            return false;
        }

        if (!CanVoreSteal(target))
        {
            return false;
        }

        Prey transfer = GetVoreSteal(target);
        if (transfer == null) return false;
        if (FreeCap() < transfer.Actor.Bulk() && target.Unit != _actor.Unit)
        {
            return false;
        }

        if (!VoreSteal(target, transfer, transfer.Location)) VoreStealFail(target, transfer, transfer.Location);
        int thirdMovement = (int)Math.Ceiling(_actor.MaxMovement() / 3.0f);
        if (_actor.Movement > thirdMovement)
            _actor.Movement -= thirdMovement;
        else
            _actor.Movement = 0;
        return true;
    }

    private bool VoreSteal(ActorUnit target, Prey preyUnit, PreyLocation oldLocation)
    {
        float r = (float)State.Rand.NextDouble();
        float v = target.PredatorComponent.GetVoreStealChance(_actor);
        if (r >= v)
        {
            return false;
        }

        Prey preyref = new Prey(preyUnit.Actor, _actor, preyUnit.Actor.PredatorComponent?.Prey);
        if (_actor.Unit.HasVagina && RaceParameters.GetTraitData(_actor.Unit).AllowedVoreTypes.Contains(VoreType.Unbirth) && oldLocation == PreyLocation.Balls)
        {
            var box = State.GameManager.CreateDialogBox();
            box.SetData(() => VoreStealFinalize(target, preyUnit, preyref, PreyLocation.Stomach, oldLocation), "Stomach", "Womb", "Transfer the prey to the womb or stomach?", () => VoreStealFinalize(target, preyUnit, preyref, PreyLocation.Womb, oldLocation));
        }
        else
        {
            VoreStealFinalize(target, preyUnit, preyref, PreyLocation.Stomach, oldLocation);
        }

        if (_actor.UnitSprite != null)
        {
            _actor.UnitSprite.UpdateSprites(_actor, true);
            _actor.UnitSprite.AnimateBellyEnter();
        }

        return true;
    }

    private void VoreStealFail(ActorUnit target, Prey preyUnit, PreyLocation oldLocation)
    {
        TacticalUtilities.Log.RegisterVoreStealFail(target.Unit, _actor.Unit, preyUnit.Unit, oldLocation);
    }

    private bool VoreStealFinalize(ActorUnit donor, Prey preyUnit, Prey preyref, PreyLocation destination, PreyLocation oldLocation)
    {
        _actor.PredatorComponent.AlivePrey++;
        donor.PredatorComponent.AlivePrey--;
        switch (destination)
        {
            case PreyLocation.Womb:
                _actor.PredatorComponent?.AddToWomb(preyref, 1.0f);
                TacticalUtilities.Log.RegisterVoreStealSuccess(donor.Unit, _actor.Unit, preyUnit.Unit, 1.0f, PreyLocation.Womb, oldLocation);
                break;
            case PreyLocation.Stomach:
                _actor.PredatorComponent.AddToStomach(preyref, 1.0f);
                TacticalUtilities.Log.RegisterVoreStealSuccess(donor.Unit, _actor.Unit, preyUnit.Unit, 1.0f, PreyLocation.Stomach, oldLocation);
                break;
            default:
                _actor.PredatorComponent.AddToStomach(preyref, 1.0f);
                TacticalUtilities.Log.RegisterVoreStealSuccess(donor.Unit, _actor.Unit, preyUnit.Unit, 1.0f, PreyLocation.Stomach, oldLocation);
                break;
        }

        _actor.PredatorComponent.AddPrey(preyref);
        _actor.Unit.GiveScaledExp(4 * preyUnit.Unit.ExpMultiplier, _actor.Unit.Level - preyUnit.Unit.Level, true);
        UpdateFullness();
        donor.PredatorComponent.RemovePrey(preyUnit);
        donor.PredatorComponent.UpdateFullness();
        return true;
    }

    private void AddToWomb(Prey preyref, float v)
    {
        Womb.Add(preyref);
        if (_actor.UnitSprite != null)
        {
            _actor.UnitSprite.UpdateSprites(_actor, true);
            _actor.UnitSprite.AnimateBellyEnter();
        }
    }

    private void AddToBalls(Prey preyref, float v)
    {
        Balls.Add(preyref);
        if (_actor.UnitSprite != null)
        {
            _actor.UnitSprite.UpdateSprites(_actor, true);
            _actor.UnitSprite.AnimateBalls(1f);
        }
    }

    internal void ForceConsume(ActorUnit forcePrey, PreyLocation preyLocation)
    {
        if (forcePrey.Unit.IsDead == false) AlivePrey++;
        State.GameManager.TacticalMode.TacticalStats.RegisterVore(Unit.Side);

        if (Equals(forcePrey.Unit.Side, Unit.Side)) State.GameManager.TacticalMode.TacticalStats.RegisterAllyVore(Unit.Side);
        forcePrey.Visible = false;
        forcePrey.Targetable = false;
        State.GameManager.TacticalMode.DirtyPack = true;
        if (!Equals(forcePrey.Unit.FixedSide, Unit.GetApparentSide(forcePrey.Unit)) || !Unit.HasTrait(TraitType.Endosoma))
        {
            Unit.GiveScaledExp(4 * forcePrey.Unit.ExpMultiplier, Unit.Level - forcePrey.Unit.Level, true);
        }

        forcePrey.Movement = 0;
        Prey preyref = new Prey(forcePrey, _actor, forcePrey.PredatorComponent?.Prey);
        switch (preyLocation)
        {
            case PreyLocation.Womb:
                State.GameManager.SoundManager.PlaySwallow(PreyLocation.Womb, _actor);
                TacticalUtilities.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> pries apart <b>{LogUtilities.ApostrophizeWithOrWithoutS(Unit.Name)}</b> vulva using {LogUtilities.GppHis(forcePrey.Unit)} face, grabbing onto any bodypart {LogUtilities.GppHe(forcePrey.Unit)} can find to slip {LogUtilities.GppHimself(forcePrey.Unit)} all the way in, aided by the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(Unit))} contractions of sudden arousal.");
                AddToWomb(preyref, 1f);
                break;
            case PreyLocation.Balls:
                State.GameManager.SoundManager.PlaySwallow(PreyLocation.Balls, _actor);
                AddToBalls(preyref, 1f);
                TacticalUtilities.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> sucks <b>{LogUtilities.ApostrophizeWithOrWithoutS(Unit.Name)}</b> tip. As soon as {LogUtilities.GppHe(forcePrey.Unit)} start{LogUtilities.SIfSingular(forcePrey.Unit)} sticking {LogUtilities.GppHis(forcePrey.Unit)} tongue inside, however, it's more like the {LogUtilities.ApostrophizeWithOrWithoutS(InfoPanel.RaceSingular(Unit))} throbbing member is doing the sucking, allowing <b>{forcePrey.Unit.Name}</b> to wiggle all the way into {LogUtilities.GppHis(Unit)} sack.");
                break;
            case PreyLocation.Anal:
                State.GameManager.SoundManager.PlaySwallow(PreyLocation.Anal, _actor);
                TacticalUtilities.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> starts by shoving one {(LogUtilities.ActorHumanoid(forcePrey.Unit) ? "arm" : "forelimb")} up <b>{LogUtilities.ApostrophizeWithOrWithoutS(Unit.Name)}</b> ass, then another. Inch by inch {LogUtilities.GppHe(forcePrey.Unit)} vigorously squeez{LogUtilities.EsIfSingular(forcePrey.Unit)} {LogUtilities.GppHimself(forcePrey.Unit)} into the anal depths.");
                AddToStomach(preyref, 1f);
                break;
            case PreyLocation.Breasts:
                TacticalUtilities.Log.RegisterMiscellaneous($"In just a few deft movements, <b>{forcePrey.Unit.Name}</b> crams {LogUtilities.GppHimself(forcePrey.Unit)} into <b>{LogUtilities.ApostrophizeWithOrWithoutS(Unit.Name)}</b> tits.");
                var data = RaceFuncs.GetRace(Unit.Race);
                if (data.SetupOutput.ExtendedBreastSprites)
                {
                    if (Config.FairyBvType == FairyBVType.Picked && State.GameManager.TacticalMode.IsPlayerInControl)
                    {
                        var box = State.GameManager.CreateDialogBox();
                        box.SetData(() =>
                        {
                            RightBreast.Add(preyref);
                            UpdateFullness();
                        }, "Right", "Left", "Which breast should the prey be put in? (from your pov)", () =>
                        {
                            LeftBreast.Add(preyref);
                            UpdateFullness();
                        });
                        State.GameManager.SoundManager.PlaySwallow(PreyLocation.Breasts, _actor);
                        break;
                    }

                    State.GameManager.SoundManager.PlaySwallow(PreyLocation.Breasts, _actor);
                    if (LeftBreastFullness < RightBreastFullness || State.Rand.Next(2) == 0)
                        LeftBreast.Add(preyref);
                    else
                        RightBreast.Add(preyref);
                    break;
                }

                State.GameManager.SoundManager.PlaySwallow(PreyLocation.Breasts, _actor);
                Breasts.Add(preyref);
                break;
            default:
                State.GameManager.SoundManager.PlaySwallow(PreyLocation.Stomach, _actor);
                TacticalUtilities.Log.RegisterMiscellaneous($"At {LogUtilities.GppHis(forcePrey.Unit)} first glimpse of the {(LogUtilities.ActorHumanoid(Unit) ? "warrior's" : "beast's")} maw, <b>{forcePrey.Unit.Name}</b> dives right down {LogUtilities.GppHis(Unit)} gullet. One swallow reflex later, <b>{LogUtilities.ApostrophizeWithOrWithoutS(Unit.Name)}</b> belly has been filled.");
                AddToStomach(preyref, 1f);
                break;
        }

        AddPrey(preyref);
        _actor.SetPredMode(preyLocation);
        _actor.SetVoreSuccessMode();
        UpdateFullness();
    }
}