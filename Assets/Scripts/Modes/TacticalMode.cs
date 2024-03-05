using LegacyAI;
using System;
using System.Collections.Generic;
using System.Linq;
using TacticalDecorations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TacticalMode : SceneBase
{
    internal static readonly WallType DefaultType = WallType.Stone;

    private enum NextUnitType
    {
        Any,
        Melee,
        Ranged
    }

    private WallType _wallType;

    private List<ActorUnit> _units;
    private List<MiscDiscard> _miscDiscards;
    private List<ClothingDiscards> _discardedClothing;
    private Army[] _armies;
    private Village _village;

    private List<ActorUnit> _retreatedDigestors;

    internal FogSystemTactical FogSystem;
    public Translator Translator;

    public TacticalTileDictionary TileDictionary;

    public Tilemap FogOfWar;
    public TileBase FogTile;
    public Tilemap Tilemap;
    public Tilemap UnderTilemap;
    public Tilemap FrontTilemap;
    public Tilemap FrontColorTilemap;
    public Tilemap EffectTileMap;
    public Tilemap FrontSpriteTilemap;

    private Tile[] _buildingTileTypes;


    public AnimatedTile Pyre;
    public Tile Ice;

    private bool[,] _blockedTile;
    private bool[,] _blockedClimberTile;

    internal void SetBlockedTiles(bool[,] tiles) => _blockedTile = tiles;

    private Dictionary<Vec2, TileEffect> _activeEffects;

    internal Dictionary<Vec2, TileEffect> ActiveEffects
    {
        get
        {
            if (_activeEffects == null) _activeEffects = new Dictionary<Vec2, TileEffect>();
            return _activeEffects;
        }
        set => _activeEffects = value;
    }

    public Transform TerrainFolder;

    public Tilemap MovementGrid;
    public TileBase[] MovementGridTileTypes;

    public Transform SelectionBox;
    public Transform ActorFolder;

    public TacticalStatusPanel StatusUI;
    public UnitInfoPanel InfoUI;
    public SkipBattleUI SkipUI;
    public HirePanel UnitPickerUI;
    public AdvancedUnitCommands CommandsUI;
    public GameObject EnemyTurnText;
    public GameObject AutoAdvanceText;
    public GameObject PausedText;
    public GameObject BattleReviewText;

    public GameObject ArrowPrefab;
    public GameObject HitEffectPrefab;
    public GameObject SkullPrefab;
    public GameObject HandPrefab;

    public GameObject SwipeEffectPrefab;

    public GameObject SpellHelperText;

    public TacticalMessageLog Log;
    public MessageLogPanel LogUI;

    public RightClickMenu RightClickMenu;

    private bool _manualSkip = false;

    public TacticalStats TacticalStats;

    internal InfoPanel InfoPanel;

    private TacticalTileType[,] _tiles;
    internal TacticalBuildings.TacticalBuilding[] Buildings;

    internal DecorationStorage[] DecorationStorage;
    internal PlacedDecoration[] Decorations;

    private Side _defenderSide;
    private Side _attackerSide; // because sides just got a lot more complex.


    internal bool DirtyPack = true;

    internal bool TacticalLogUpdated;

    internal ChoiceOption FledReturn;
    private bool _waitingForDialog;

    public bool PseudoTurn = false;
    public bool IgnorePseudo = false;
    public bool SkipPseudo = false;

    internal int CurrentTurn = 1;

    private int _lastDiscard = 5;

    private int _corpseCount = 0;

    private bool _repeatingTurn = false;
    internal Vector2Int Wins;

    private Spell _currentSpell;

    private bool _attackersTurn;
    internal bool IsPlayerTurn;
    internal bool IsPlayerInControl => PseudoTurn || (IsPlayerTurn && RunningFriendlyAI == false && _foreignAI == null && !SpectatorMode);
    private Side _activeSide;

    public bool AIAttacker;

    public bool CheatAttackerControl;

    public bool AIDefender;

    public bool CheatDefenderControl;

    internal bool SpectatorMode;

    internal string AttackerName = null;
    internal string DefenderName = null;

    private bool _paused;

    private float _remainingLockedPanelTime = 0;

    internal float AITimer;
    private ITacticalAI _currentAI;
    private ITacticalAI _attackerAI;
    private ITacticalAI _defenderAI;
    private ITacticalAI _foreignAI;

    public double StartingAttackerPower;
    public double StartingDefenderPower;

    internal bool RunningFriendlyAI;

    private bool[,] _walkable;

    private bool _reviewingBattle = false;

    private Vec2I _currentPathDestination;

    private float _autoAdvanceTimer;
    private bool _autoAdvancing;
    private const float AutoAdvanceRate = .4f;

    private PathNodeManager _arrowManager;
    private List<PathNode> _queuedPath;

    private Vec2I _startingLocation;
    private int _startingMp;

    internal bool TurboMode;

    public List<ActorUnit> ExtraAttackers;
    public List<ActorUnit> ExtraDefenders;

    private List<Unit> _retreatedAttackers;
    private List<Unit> _retreatedDefenders;

    private List<ActorUnit> _garrison;

    private SpecialAction _specialType;
    private SpecialAction _lastSpecial;
    private int _mode;

    public int ActionMode
    {
        get { return _mode; }
        set
        {
            if (value != 0 && _tiles != null && SelectedUnit != null)
            {
                if (TacticalUtilities.TileContainsMoreThanOneUnit(SelectedUnit.Position.X, SelectedUnit.Position.Y))
                {
                    UndoMovement();
                }
            }

            _arrowManager.ClearNodes();
            MovementGrid.ClearAllTiles();
            if (value == 0) _specialType = SpecialAction.None;
            CommandsUI.SelectorIcon.transform.position = new Vector2(2000f, 2000f);
            if (_units != null) RemoveHitPercentages();
            switch (value)
            {
                case 1:
                    if (SelectedUnit != null && SelectedUnit.Targetable) ShowMeleeHitPercentages(SelectedUnit);
                    StatusUI.ButtonSelection.transform.position = StatusUI.MeleeButton.transform.position;
                    break;
                case 2:
                    StatusUI.ButtonSelection.transform.position = StatusUI.RangedButton.transform.position;
                    if (SelectedUnit != null && SelectedUnit.Targetable) ShowRangedHitPercentages(SelectedUnit);
                    break;
                case 3:
                    if (SelectedUnit != null && SelectedUnit.Targetable) ShowVoreHitPercentages(SelectedUnit);
                    StatusUI.ButtonSelection.transform.position = StatusUI.VoreButton.transform.position;
                    break;
                case 4:
                    if (SelectedUnit != null && SelectedUnit.Targetable) ShowSpecialHitPercentages(SelectedUnit);
                    StatusUI.ButtonSelection.transform.position = new Vector2(2000f, 2000f);
                    break;
                case 5:
                    CreateMovementGrid();
                    StatusUI.ButtonSelection.transform.position = StatusUI.MovementButton.transform.position;
                    break;
                case 6:
                    if (SelectedUnit != null && SelectedUnit.Targetable) ShowMagicPercentages(SelectedUnit);
                    StatusUI.ButtonSelection.transform.position = new Vector2(2000f, 2000f);
                    break;
                default:
                    StatusUI.ButtonSelection.transform.position = new Vector2(2000f, 2000f);
                    break;
            }

            _mode = value;
        }
    }

    internal Side GetDefenderSide()
    {
        return _defenderSide;
    }

    internal Side GetAttackerSide()
    {
        return _attackerSide;
    }

    private ActorUnit _selectedUnit;

    public ActorUnit SelectedUnit
    {
        get { return _selectedUnit; }
        set
        {
            if (_tiles != null && _selectedUnit != null)
            {
                if (TacticalUtilities.TileContainsMoreThanOneUnit(_selectedUnit.Position.X, _selectedUnit.Position.Y) || TacticalUtilities.OpenTile(new Vec2I(_selectedUnit.Position.X, _selectedUnit.Position.Y), null))
                {
                    UndoMovement();
                }
            }

            _selectedUnit = value;
            if (value != null)
            {
                RebuildInfo();
                PlaceUndoMarker();
            }
        }
    }

    private void Start()
    {
        var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
        _buildingTileTypes = new Tile[allSprites.Length];
        for (int i = 0; i < allSprites.Length; i++)
        {
            _buildingTileTypes[i] = ScriptableObject.CreateInstance<Tile>();
            _buildingTileTypes[i].sprite = allSprites[i];
        }
        InvokeRepeating("CheckSpriteRefresh", 3.0f, 0.3f);
    }
    
    void CheckSpriteRefresh()
    {
        GameManager.CustomManager.CheckIfRefreshNeeded();
        GameManager.CustomManager.RefreshIfNeeded();
    }

    private void PlaceUndoMarker()
    {
        if (SelectedUnit == null) return;
        _startingLocation = SelectedUnit.Position;
        _startingMp = SelectedUnit.Movement;
    }

    private void UndoMovement()
    {
        if (SelectedUnit == null) return;
        SelectedUnit.Movement = _startingMp;
        Translator.SetTranslator(SelectedUnit.UnitSprite.transform, SelectedUnit.Position, _startingLocation, .2f, State.GameManager.TacticalMode.IsPlayerTurn || PseudoTurn);
        SelectedUnit.SetPos(_startingLocation);
        DirtyPack = true;
        RebuildInfo();
    }

    internal void ForceUpdate()
    {
        ItemRepository repo = State.World.ItemRepository;
        foreach (Unit unit in _armies[0].Units)
        {
            UpdateUnit(unit);
        }

        if (_armies[1] != null)
        {
            foreach (Unit unit in _armies[1].Units)
            {
                UpdateUnit(unit);
            }
        }

        if (_garrison != null)
        {
            foreach (ActorUnit actor in _garrison)
            {
                UpdateUnit(actor.Unit);
            }
        }

        void UpdateUnit(Unit unit)
        {
            unit.UpdateItems(repo);
            unit.UpdateSpells();
            unit.ReloadTraits();
        }
    }


    internal bool TacticalSoundBlocked()
    {
        bool ret = false;
        if (TurboMode)
            ret = true;
        else if (_autoAdvancing && Config.AutoAdvance == Config.AutoAdvanceType.SkipToEnd) ret = true;
        return ret;
    }

    public void Begin(StrategicTileType tiletype, Village village, Army invader, Army defender, TacticalAIType aIinvader, TacticalAIType aIdefender, TacticalBattleOverride tacticalBattleOverride = TacticalBattleOverride.Ignore)
    {
        BattleReviewText.gameObject.SetActive(false);
        CheatAttackerControl = false;
        CheatDefenderControl = false;
        SpectatorMode = false;
        if (_arrowManager == null) _arrowManager = FindObjectOfType<PathNodeManager>();
        if (Config.AutoScaleTactical)
        {
            int tempCount = (village?.Garrison ?? 0) + invader.Units.Count() + (defender?.Units.Count() ?? 0);
            int size = 16 + (int)(2 * Mathf.Sqrt(tempCount));
            Config.World.TacticalSizeX = size;
            Config.World.TacticalSizeY = size;
        }

        _retreatedDigestors = new List<ActorUnit>();

        _armies = new Army[2];
        _armies[0] = invader;
        _armies[1] = defender;
        this._village = village;
        _attackersTurn = true;

        CurrentTurn = 1;
        _corpseCount = 0;

        _units = new List<ActorUnit>();
        _discardedClothing = new List<ClothingDiscards>();
        _miscDiscards = new List<MiscDiscard>();

        TacticalMapGenerator mapGen = new TacticalMapGenerator(tiletype, village);
        _tiles = mapGen.GenMap(village?.HasWalls() ?? false);

        _defenderSide = defender?.Side ?? village.Side;
        _attackerSide = invader.Side;


        DefectProcessor defectors = new DefectProcessor(_armies[0], _armies[1], village);

        //convert armies	

        List<ActorUnit> attackers = new List<ActorUnit>();
        List<ActorUnit> defenders = new List<ActorUnit>();
        _garrison = new List<ActorUnit>();
        List<Unit> grabbedGarrison = village?.PrepareAndReturnGarrison();
        Unit attackerLeader = null;
        Unit defenderLeader = null;
        attackerLeader = _armies[0].LeaderIfInArmy();
        for (int i = 0; i < _armies[0].Units.Count; i++)
        {
            ActorUnit unit = new ActorUnit(mapGen.RandomActorPosition(_tiles, _blockedTile, _units, TacticalMapGenerator.SpawnLocation.Upper, _armies[0].Units[i].GetBestRanged() == null), _armies[0].Units[i]);
            _units.Add(unit);
            unit.Unit.Side = _armies[0].Side;
            unit.InSight = true; // All units visible by default, for daytime
            unit.Unit.CurrentLeader = attackerLeader;
            attackers.Add(unit);
        }

        if (_armies[1] != null)
        {
            defenderLeader = _armies[1].LeaderIfInArmy();
            for (int i = 0; i < _armies[1].Units.Count; i++)
            {
                ActorUnit unit = new ActorUnit(mapGen.RandomActorPosition(_tiles, _blockedTile, _units, TacticalMapGenerator.SpawnLocation.Lower, _armies[1].Units[i].GetBestRanged() == null), _armies[1].Units[i]);
                _units.Add(unit);
                unit.Unit.Side = _defenderSide;
                unit.InSight = true; //All units visible by default, for daytime
                unit.Unit.CurrentLeader = defenderLeader;
                defenders.Add(unit);
            }
        }

        if (this._village != null && grabbedGarrison != null)
        {
            for (int i = 0; i < grabbedGarrison.Count; i++)
            {
                ActorUnit unit = new ActorUnit(mapGen.RandomActorPosition(_tiles, _blockedTile, _units, TacticalMapGenerator.SpawnLocation.Lower, grabbedGarrison[i].GetBestRanged() == null), grabbedGarrison[i]);
                _units.Add(unit);
                unit.Unit.Side = _defenderSide;
                unit.InSight = true; //All units visible by default, for daytime
                unit.Unit.CurrentLeader = defenderLeader;
                _garrison.Add(unit);
            }
        }

        StartingAttackerPower = StrategicUtilities.UnitValue(_armies[0].Units);
        StartingDefenderPower = StrategicUtilities.UnitValue(defenders.Concat(_garrison).Select(s => s.Unit).ToList());

        Race defenderRace = defender?.EmpireOutside?.ReplacedRace ?? village.Empire?.Race ?? _defenderSide.ToRace();
        Race attackerRace = invader.EmpireOutside?.ReplacedRace ?? invader.Side.ToRace();
        if (Config.Defections && !State.GameManager.PureTactical)
        {
            foreach (ActorUnit actor in attackers)
            {
                defectors.AttackerDefectCheck(actor, defenderRace);
            }

            foreach (ActorUnit actor in defenders)
            {
                defectors.DefenderDefectCheck(actor, attackerRace);
            }

            foreach (ActorUnit actor in _garrison.ToList())
            {
                defectors.GarrisonDefectCheck(actor, attackerRace);
                if (!Equals(actor.Unit.Side, _defenderSide)) _garrison.Remove(actor);
            }
        }

        ExtraAttackers = defectors.ExtraAttackers;
        ExtraDefenders = defectors.ExtraDefenders;

        _retreatedAttackers = new List<Unit>();
        _retreatedDefenders = new List<Unit>();

        foreach (ActorUnit actor in _units)
        {
            actor.Unit.EnemiesKilledThisBattle = 0;
            actor.AllowedToDefect = !actor.DefectedThisTurn && !Equals(TacticalUtilities.GetPreferredSide(actor.Unit, actor.Unit.Side, Equals(actor.Unit.Side, _attackerSide) ? _defenderSide : _attackerSide), actor.Unit.Side);
            actor.DefectedThisTurn = false;
            actor.Unit.Heal(actor.Unit.GetLeaderBonus() * 3); // mainly for the new Stat boosts => maxHealth option, but eh why not have it for everyone anyway?
        }


        int summonedUnits = SummonUnits(mapGen, attackerLeader, defenderLeader);
        int antSummonedUnits = SummonAnts(mapGen, attackerLeader, defenderLeader);


        _activeSide = _armies[0].Side;

        TacticalStats = new TacticalStats();
        TacticalStats.SetInitialUnits(_armies[0].Units.Count + defectors.NewAttackers + defectors.DefectedGarrison,
            _armies[1]?.Units.Count ?? 0 + defectors.NewDefenders,
            _garrison.Count - defectors.DefectedGarrison,
            _armies[0].Side, _defenderSide);


        AIDefender = aIdefender != TacticalAIType.None;
        AIAttacker = aIinvader != TacticalAIType.None;

        if (AttackerName == null) AttackerName = $"{_armies[0].EmpireOutside?.Name ?? _armies[0].Side.ToRace().ToString()}";
        if (DefenderName == null) DefenderName = $"{_armies[1]?.EmpireOutside?.Name ?? village?.Empire?.Name ?? _defenderSide.ToRace().ToString()}";

        IsPlayerTurn = !AIAttacker;

        GeneralSetup();

        if ((_armies[1]?.Units.Count ?? 0) <= 0 && _garrison.Count <= 0)
        {
            VictoryCheck();
            string msg = $"All defenders have defected to rejoin their race, attackers win by default.";
            State.GameManager.CreateMessageBox(msg);
            return;
        }

        if (_armies[0].Units.Count <= 0)
        {
            VictoryCheck();
            string msg = $"All attackers have defected to rejoin their race, defenders win by default.";
            State.GameManager.CreateMessageBox(msg);
            return;
        }

        if (aIinvader == TacticalAIType.Legacy)
            _attackerAI = new LegacyTacticalAI(_units, _tiles, _armies[0].Side);
        else
        {
            object[] argArray = { _units, _tiles, _armies[0].Side, false };
            RaceAI rai = State.RaceSettings.GetRaceAI(attackerRace);
            _attackerAI = Activator.CreateInstance(RaceAIType.Dict[rai], args: argArray) as TacticalAI;
            InitRetreatConditions(_attackerAI, attackers, invader.EmpireOutside, AIAttacker);
        }

        if (aIdefender == TacticalAIType.Legacy)
            _defenderAI = new LegacyTacticalAI(_units, _tiles, _defenderSide);
        else
        {
            object[] argArray = { _units, _tiles, _defenderSide, village != null };
            RaceAI rai = State.RaceSettings.GetRaceAI(defenderRace);
            _defenderAI = Activator.CreateInstance(RaceAIType.Dict[rai], args: argArray) as TacticalAI;

            var defenderEmp = defender?.EmpireOutside ?? village.Empire;
            InitRetreatConditions(_defenderAI, defenders, defenderEmp, AIDefender);
        }

        _currentAI = _attackerAI;


        Log.RegisterNewTurn(AttackerName, 1);

        bool skip = (!Config.WatchAIBattles || (Config.IgnoreMonsterBattles && RaceFuncs.IsMonstersOrUniqueMercsOrRebelsOrBandits(_armies[0].Side) && RaceFuncs.IsMonstersOrUniqueMercsOrRebelsOrBandits(_defenderSide))) && AIAttacker && AIDefender;

        if (tacticalBattleOverride == TacticalBattleOverride.ForceWatch) skip = false;
        if (tacticalBattleOverride == TacticalBattleOverride.ForceSkip && AIAttacker && AIDefender) skip = true;
        if (_units.Any(actor => State.World.AllActiveEmpires != null && State.World.GetEmpireOfSide(actor.Unit.FixedSide)?.StrategicAI == null)) skip = false;

        if (skip)
        {
            TurboModeMethod();
        }
        else
        {
            TurboMode = false;
            State.Save($"{State.SaveDirectory}Autosave_Battle.sav");
            defectors.DefectReport();
            if (summonedUnits > 0 || antSummonedUnits > 0)
            {
                string message = "";
                if (summonedUnits > 0) message += $"{summonedUnits} units were summoned by astral call\n";
                if (antSummonedUnits > 0) message += $"{antSummonedUnits} units were summoned by ant pheromones";
                if (AIDefender && AIAttacker)
                    State.GameManager.CreateMessageBox(message, 4);
                else
                    State.GameManager.CreateMessageBox(message);
            }
        }

        if (State.World.IsNight)
        {
            UpdateFog();
        }
    }

    private void InitRetreatConditions(ITacticalAI ai, List<ActorUnit> fighters, Empire empire, bool nonPlayer)
    {
        if (State.GameManager.PureTactical == false && Config.NoAIRetreat == false)
        {
            if (empire != null && Equals(empire?.Race, Race.Vagrant))
            {
                ai.RetreatPlan = new TacticalAI.RetreatConditions(.2f, fighters.Count + 2);
            }

            if (empire != null && Equals(empire?.ReplacedRace, Race.FeralLion))
            {
                ai.RetreatPlan = new TacticalAI.RetreatConditions(0, fighters.Count * 3, 0.9f);
            }
            else if (empire != null && empire is MonsterEmpire)
            {
                if (fighters.Where(s => s.Unit.HasTrait(TraitType.EvasiveBattler)).Count() / (float)fighters.Count > .8f) //If more than 80% has fast flee
                    ai.RetreatPlan = new TacticalAI.RetreatConditions(.05f, 0);
                else
                    ai.RetreatPlan = new TacticalAI.RetreatConditions(.025f, 0);
            }
            else if (nonPlayer) //Don't set retreat for players
            {
                if (fighters.Where(s => s.Unit.HasTrait(TraitType.EvasiveBattler)).Count() / (float)fighters.Count > .8f) //If more than 80% has fast flee
                    ai.RetreatPlan = new TacticalAI.RetreatConditions(.3f, 0);
                else
                    ai.RetreatPlan = new TacticalAI.RetreatConditions(.15f, 0);
            }
        }
    }

    private int SummonUnits(TacticalMapGenerator mapGen, Unit attackerLeader, Unit defenderLeader)
    {
        int summonedUnits = 0;
        int attackerSummoners = 0;
        int defenderSummoners = 0;
        int defenderLevels = 0;
        int attackerLevels = 0;
        bool attackerPred = false;
        bool defenderPred = false;
        List<Race> attackerRaces = new List<Race>();
        List<Race> defenderRaces = new List<Race>();
        foreach (ActorUnit actor in _units.ToList())
        {
            if (actor.Unit.HasTrait(TraitType.AstralCall))
            {
                if (Equals(actor.Unit.Side, _defenderSide))
                {
                    defenderSummoners += 1;
                    defenderLevels += actor.Unit.Level;
                    defenderPred = actor.Unit.Predator;
                    defenderRaces.Add(actor.Unit.Race);
                }
                else
                {
                    attackerSummoners += 1;
                    attackerLevels += actor.Unit.Level;
                    attackerPred = actor.Unit.Predator;
                    attackerRaces.Add(actor.Unit.Race);
                }
            }
        }

        if (attackerSummoners > 0)
        {
            int summons = attackerSummoners / 8;
            float extraFraction = attackerSummoners % 8 / 8f;
            if (State.Rand.NextDouble() < extraFraction) summons += 1;
            for (int i = 0; i < summons; i++)
            {
                Race race = attackerRaces[State.Rand.Next(attackerRaces.Count())];
                attackerRaces.Remove(race);
                Unit newUnit = new NpcUnit((int)Mathf.Max(0.9f * attackerLevels / attackerSummoners - 2, 1), false, 2, _armies[0].Side, race, 0, attackerPred);
                newUnit.Type = UnitType.Summon;
                ActorUnit unit = new ActorUnit(mapGen.RandomActorPosition(_tiles, _blockedTile, _units, TacticalMapGenerator.SpawnLocation.UpperMiddle, newUnit.GetBestRanged() == null), newUnit);
                _units.Add(unit);
                summonedUnits++;
                unit.Unit.CurrentLeader = attackerLeader;
            }
        }

        if (defenderSummoners > 0)
        {
            int summons = defenderSummoners / 8;
            float extraFraction = defenderSummoners % 8 / 8f;
            if (State.Rand.NextDouble() < extraFraction) summons += 1;
            for (int i = 0; i < summons; i++)
            {
                Race race = defenderRaces[State.Rand.Next(defenderRaces.Count())];
                defenderRaces.Remove(race);
                Unit newUnit = new NpcUnit((int)Mathf.Max(0.9f * defenderLevels / defenderSummoners - 2, 1), false, 2, _defenderSide, race, 0, defenderPred);
                newUnit.Type = UnitType.Summon;
                ActorUnit unit = new ActorUnit(mapGen.RandomActorPosition(_tiles, _blockedTile, _units, TacticalMapGenerator.SpawnLocation.LowerMiddle, newUnit.GetBestRanged() == null), newUnit);
                _units.Add(unit);
                summonedUnits++;
                unit.Unit.CurrentLeader = defenderLeader;
            }
        }


        return summonedUnits;
    }

    private int SummonAnts(TacticalMapGenerator mapGen, Unit attackerLeader, Unit defenderLeader)
    {
        int summonedUnits = 0;
        int attackerSummoners = 0;
        int defenderSummoners = 0;
        int defenderLevels = 0;
        int attackerLevels = 0;
        bool attackerPred = false;
        bool defenderPred = false;
        foreach (ActorUnit actor in _units.ToList())
        {
            if (actor.Unit.HasTrait(TraitType.AntPheromones))
            {
                if (Equals(actor.Unit.Side, _defenderSide))
                {
                    defenderSummoners += 1;
                    defenderLevels += actor.Unit.Level;
                    defenderPred = actor.Unit.Predator;
                }
                else
                {
                    attackerSummoners += 1;
                    attackerLevels += actor.Unit.Level;
                    attackerPred = actor.Unit.Predator;
                }
            }
        }

        if (attackerSummoners > 0)
        {
            int summons = attackerSummoners / 4;
            float extraFraction = attackerSummoners % 4 / 4f;
            if (State.Rand.NextDouble() < extraFraction) summons += 1;
            for (int i = 0; i < summons; i++)
            {
                Race race;
                int level = (int)Mathf.Max(0.4f * attackerLevels / attackerSummoners - 2, 1);
                if (State.Rand.Next(3) == 0)
                    race = Race.WarriorAnt;
                else
                    race = Race.FeralAnt;
                Unit newUnit = new NpcUnit(level, false, 2, _armies[0].Side, race, 0, attackerPred);
                newUnit.Type = UnitType.Summon;
                ActorUnit unit = new ActorUnit(mapGen.RandomActorPosition(_tiles, _blockedTile, _units, TacticalMapGenerator.SpawnLocation.UpperMiddle, newUnit.GetBestRanged() == null), newUnit);
                _units.Add(unit);
                summonedUnits++;
                unit.Unit.CurrentLeader = attackerLeader;
            }
        }

        if (defenderSummoners > 0)
        {
            int summons = defenderSummoners / 4;
            float extraFraction = defenderSummoners % 4 / 4f;
            if (State.Rand.NextDouble() < extraFraction) summons += 1;
            for (int i = 0; i < summons; i++)
            {
                Race race;
                int level = (int)Mathf.Max(0.4f * defenderLevels / defenderSummoners - 2, 1);
                if (State.Rand.Next(3) == 0)
                    race = Race.WarriorAnt;
                else
                    race = Race.FeralAnt;
                Unit newUnit = new NpcUnit(level, false, 2, _defenderSide, race, 0, defenderPred);
                newUnit.Type = UnitType.Summon;
                ActorUnit unit = new ActorUnit(mapGen.RandomActorPosition(_tiles, _blockedTile, _units, TacticalMapGenerator.SpawnLocation.LowerMiddle, newUnit.GetBestRanged() == null), newUnit);
                _units.Add(unit);
                summonedUnits++;
                unit.Unit.CurrentLeader = defenderLeader;
            }
        }


        return summonedUnits;
    }

    internal void DisableAttackerAI()
    {
        AIAttacker = false;
        CheatAttackerControl = true;
    }

    internal void DisableDefenderAI()
    {
        AIDefender = false;
        CheatDefenderControl = true;
    }

    internal void ClearNames()
    {
        AttackerName = null;
        DefenderName = null;
    }

    internal void RefreshPureTacticalTraits()
    {
        foreach (ActorUnit unit in _units)
        {
            unit.Unit.ReloadTraits();
            unit.Unit.InitializeTraits();
        }

        ForceUpdate();
    }

    internal int GetNextCorpseLayer()
    {
        _corpseCount += 1;
        return -20020 + 20 * _corpseCount;
    }

    private void TurboModeMethod()
    {
        float time = Time.realtimeSinceStartup;
        TurboMode = true;

        RefreshAIIfNecessary();

        while (!VictoryCheck() && CurrentTurn < 2000)
        {
            if (_waitingForDialog) return;
            if (DirtyPack) UpdateAreaTraits();
            if (_currentAI.RunAI() == false)
            {
                EndTurn();
            }
        }

        if (CurrentTurn >= 2000) TurboMode = false;
        if (State.GameManager.CurrentScene == State.GameManager.TacticalMode && State.GameManager.StatScreen.gameObject.activeSelf == false) Log.RefreshListing();
        if (Time.realtimeSinceStartup - time > .5f) Debug.Log($"{AttackerName} vs {DefenderName} - {Time.realtimeSinceStartup - time}");
        if (State.Warned == false && Time.realtimeSinceStartup - time > 4f)
        {
            State.Warned = true;
            State.GameManager.CreateMessageBox($@"Just had a quick simulated battle take more than 4 seconds to resolve (I.e. one your settings are set to not show).
Smaller army sizes will be faster to process, so if the wait bothers you try playing with smaller max armies.
This warning will only appear once per session.  

Misc Info:
Battle took {Math.Round(Time.realtimeSinceStartup - time, 2)} seconds
{_units.Count()} Total units {AttackerName} vs {DefenderName}.
Turns: {CurrentTurn}
");
        }
    }

    private void RefreshAIIfNecessary()
    {
        List<ActorUnit> fighters = _units.Where(actor => actor.Targetable == true && Equals(actor.Unit.Side, _activeSide) && actor.Movement > 0).ToList();
        ActorUnit nextUnit = fighters.Count() > 0 ? fighters[0] : null;
        Type desiredAIType;
        if (nextUnit != null)
        {
            desiredAIType = !Equals(TacticalUtilities.GetMindControlSide(nextUnit.Unit), Side.TrueNoneSide) ? GetAITypeForMindControledUnit(nextUnit.Unit) : RaceAIType.Dict[State.RaceSettings.GetRaceAI(nextUnit.Unit.Race)];
        }
        else
            desiredAIType = typeof(StandardTacticalAI);

        if (_currentAI == null || _currentAI.GetType() != desiredAIType)
        {
            object[] argArray = { _units, _tiles, _activeSide, false };
            _currentAI = Activator.CreateInstance(desiredAIType, args: argArray) as TacticalAI;
            InitRetreatConditions(_currentAI, fighters, State.World.GetEmpireOfSide(_activeSide), true);
            if (_attackersTurn)
            {
                _attackerAI = _currentAI;
            }
            else
            {
                _defenderAI = _currentAI;
            }
        }
    }

    internal void LoadData(TacticalData data)
    {
        _retreatedDigestors = new List<ActorUnit>();
        if (_arrowManager == null) _arrowManager = FindObjectOfType<PathNodeManager>();
        Import(data);
        if (Config.AutoScaleTactical)
        {
            Config.World.TacticalSizeX = _tiles.GetUpperBound(0) + 1;
            Config.World.TacticalSizeY = _tiles.GetUpperBound(1) + 1;
        }

        if (_miscDiscards == null) _miscDiscards = new List<MiscDiscard>();
        Unit attackerLeader = _armies[0].LeaderIfInArmy();
        Unit defenderLeader = null;
        if (_armies[1] != null) defenderLeader = _armies[1].LeaderIfInArmy();
        foreach (ActorUnit actor in _units)
        {
            if (Equals(actor.Unit.Side, _defenderSide))
                actor.Unit.CurrentLeader = defenderLeader;
            else
                actor.Unit.CurrentLeader = attackerLeader;
        }

        foreach (ActorUnit unit in _units)
        {
            unit.PredatorComponent?.UpdateAlivePrey();
        }

        GeneralSetup();
        Log.RefreshListing();
        TacticalUtilities.UpdateVersion();
        if (State.TutorialMode) State.GameManager.TutorialScript.InitializeTactical(_units);
        foreach (var actor in _units)
        {
            if (actor.Fled && actor.PredatorComponent?.PreyCount > 0) _retreatedDigestors.Add(actor);
        }

        if (!Config.WatchAIBattles && AIAttacker && AIDefender)
        {
            TurboModeMethod();
        }
        else
        {
            TurboMode = false;
        }

        if (Config.VisibleCorpses)
        {
            foreach (ActorUnit actor in _units)
            {
                if (actor.Targetable == false && actor.Visible == true && actor.Surrendered)
                {
                    float angle = 40 + State.Rand.Next(280);
                    if (actor.UnitSprite != null) actor.UnitSprite.transform.rotation = Quaternion.Euler(0, 0, angle);
                    _corpseCount += 1;
                }
            }
        }
    }

    private void GeneralSetup()
    {
        if (Log == null)
        {
            Log = new TacticalMessageLog();
        }

        LogUI.SetBase();

        DirtyPack = true;
        _paused = false;
        AITimer = 0;
        RebuildDecorations();
        RebuildBlockedTiles();

        if (_village != null && State.World.MainEmpires != null)
        {
            FrontColorTilemap.color = State.World.GetEmpireOfSide(_village.Side)?.UnityColor ?? Color.white;
        }

        FledReturn = ChoiceOption.Default;
        _waitingForDialog = false;


        _reviewingBattle = false;

        if (_village != null)
        {
            _wallType = RaceFuncs.GetRace(_village.Race).WallType();
        }

        RebuildInfo();

        InfoPanel = new InfoPanel(InfoUI);
        Translator = new Translator();
        UpdateEndTurnButtonText();
        StatusUI.EndTurn.interactable = IsPlayerTurn;
        _manualSkip = false;
        StatusUI.SkipToEndButton.interactable = true;
        EnemyTurnText.SetActive(!IsPlayerTurn);


        RedrawTiles();
        CreateActors();

        ActionMode = 0;
        string attackerController = AIAttacker == false ? "Player(Atk)" : "AI(Atk)";
        string defenderController = AIDefender == false ? "Player(Def)" : "AI(Def)";
        StatusUI.AttackerText.text = $"{AttackerName} - {attackerController}";
        StatusUI.DefenderText.text = $"{DefenderName} - {defenderController}";

        if (_attackersTurn)
        {
            StatusUI.AttackerText.fontStyle = FontStyle.Bold;
            StatusUI.DefenderText.fontStyle = FontStyle.Normal;
        }
        else
        {
            StatusUI.AttackerText.fontStyle = FontStyle.Normal;
            StatusUI.DefenderText.fontStyle = FontStyle.Bold;
        }

        TacticalUtilities.ResetData(_armies, _village, _units, _garrison, _tiles, _blockedTile, _blockedClimberTile);
    }

    private void RebuildDecorations()
    {
        if (DecorationStorage == null)
        {
            Decorations = new PlacedDecoration[0];
            return;
        }

        Decorations = new PlacedDecoration[DecorationStorage.Length];
        for (int i = 0; i < Decorations.Length; i++)
        {
            Decorations[i] = new PlacedDecoration(DecorationStorage[i].Position, TacticalDecorationList.DecDict[DecorationStorage[i].Type]);
        }
    }

    private void RebuildBlockedTiles()
    {
        _blockedTile = new bool[_tiles.GetUpperBound(0) + 1, _tiles.GetUpperBound(1) + 1];
        _blockedClimberTile = new bool[_tiles.GetUpperBound(0) + 1, _tiles.GetUpperBound(1) + 1];
        if (Buildings != null)
        {
            foreach (var building in Buildings)
            {
                if (building == null) continue;
                for (int y = 0; y < building.Height; y++)
                {
                    for (int x = 0; x < building.Width; x++)
                    {
                        _blockedTile[building.LowerLeftPosition.X + x, building.LowerLeftPosition.Y + y] = true;
                        _blockedClimberTile[building.LowerLeftPosition.X + x, building.LowerLeftPosition.Y + y] = true;
                    }
                }
            }
        }

        if (Decorations != null)
        {
            foreach (var decoration in Decorations)
            {
                if (decoration == null) continue;
                if (decoration.TacDec == null) continue;
                for (int y = 0; y < decoration.TacDec.Height; y++)
                {
                    for (int x = 0; x < decoration.TacDec.Width; x++)
                    {
                        if (decoration.LowerLeftPosition.X + x < _blockedTile.GetLength(0) && decoration.LowerLeftPosition.Y + y < _blockedTile.GetLength(1))
                        {
                            _blockedTile[decoration.LowerLeftPosition.X + x, decoration.LowerLeftPosition.Y + y] = true;
                            if (decoration.TacDec.PathType != PathType.Tree) _blockedClimberTile[decoration.LowerLeftPosition.X + x, decoration.LowerLeftPosition.Y + y] = true;
                        }
                    }
                }
            }
        }
    }

    internal TacticalData Export()
    {
        TacticalData data = new TacticalData
        {
            Units = _units,
            Armies = _armies,
            Village = _village,

            Buildings = Buildings,

            Tiles = _tiles,
            DecorationStorage = DecorationStorage,

            Garrison = _garrison,

            SelectedUnit = SelectedUnit,

            ActiveEffects = ActiveEffects,

            DefenderSide = _defenderSide,

            AttackersTurn = _attackersTurn,
            IsAPlayerTurn = IsPlayerTurn,
            ActiveSide = _activeSide,

            AIAttacker = AIAttacker,
            AIDefender = AIDefender,

            StartingAttackerPower = StartingAttackerPower,
            StartingDefenderPower = StartingDefenderPower,


            AttackerName = AttackerName,
            DefenderName = DefenderName,

            CurrentTurn = CurrentTurn,

            CurrentAI = _currentAI,
            AttackerAI = _attackerAI,
            DefenderAI = _defenderAI,
            TacticalStats = TacticalStats,

            ExtraAttackers = ExtraAttackers,
            ExtraDefenders = ExtraDefenders,
            RetreatedAttackers = _retreatedAttackers,
            RetreatedDefenders = _retreatedDefenders,

            DiscardedClothing = _discardedClothing,
            MiscDiscards = _miscDiscards,
            LastDiscard = _lastDiscard,

            LOG = Log,

            RunningFriendlyAI = RunningFriendlyAI
        };
        return data;
    }

    private void Import(TacticalData data)
    {
        _units = data.Units;
        _armies = data.Armies;
        _village = data.Village;

        Buildings = data.Buildings;

        _garrison = data.Garrison;

        _tiles = data.Tiles;
        DecorationStorage = data.DecorationStorage;

        ActiveEffects = data.ActiveEffects;

        SelectedUnit = data.SelectedUnit;

        _defenderSide = data.DefenderSide;

        CurrentTurn = data.CurrentTurn;

        _attackersTurn = data.AttackersTurn;
        IsPlayerTurn = data.IsAPlayerTurn;
        _activeSide = data.ActiveSide;

        AIAttacker = data.AIAttacker;
        AIDefender = data.AIDefender;

        StartingAttackerPower = data.StartingAttackerPower;
        StartingDefenderPower = data.StartingDefenderPower;

        AttackerName = data.AttackerName;
        DefenderName = data.DefenderName;

        _currentAI = data.CurrentAI;
        _attackerAI = data.AttackerAI;
        _defenderAI = data.DefenderAI;
        TacticalStats = data.TacticalStats;

        _miscDiscards = data.MiscDiscards;
        _discardedClothing = data.DiscardedClothing;
        _lastDiscard = data.LastDiscard;


        ExtraAttackers = data.ExtraAttackers;
        ExtraDefenders = data.ExtraDefenders;
        _retreatedAttackers = data.RetreatedAttackers;
        _retreatedDefenders = data.RetreatedDefenders;

        if (_retreatedAttackers == null) _retreatedAttackers = new List<Unit>();
        if (_retreatedDefenders == null) _retreatedDefenders = new List<Unit>();

        Log = data.LOG;

        RunningFriendlyAI = data.RunningFriendlyAI;
    }

    private void UpdateAreaTraits()
    {
        for (int i = 0; i < _units.Count; i++)
        {
            _units[i].Intimidated = false;
            _units[i].Unit.Harassed = false;
            _units[i].Unit.NearbyFriendlies = 0;
        }

        for (int i = 0; i < _units.Count; i++)
        {
            if (_units[i].Targetable == false) continue;

            for (int j = i + 1; j < _units.Count; j++)
            {
                if (_units[j].Targetable == false) continue;

                if (_units[i].Position.GetNumberOfMovesDistance(_units[j].Position) == 1)
                {
                    if (Equals(_units[i].Unit.Side, _units[j].Unit.Side) && _units[i].Surrendered == false && _units[j].Surrendered == false)
                    {
                        _units[i].Unit.NearbyFriendlies++;
                        _units[j].Unit.NearbyFriendlies++;
                    }
                    else
                    {
                        ApplyEnemyTags(_units[i], _units[j]);
                        ApplyEnemyTags(_units[j], _units[i]);
                    }
                }
            }
        }

        void ApplyEnemyTags(ActorUnit source, ActorUnit target)
        {
            if (source.Unit.HasTrait(TraitType.Intimidating)) target.Intimidated = true;
            if (source.Unit.HasTrait(TraitType.TentacleHarassment)) target.Unit.Harassed = true;
        }

        DirtyPack = false;
    }

    internal void CreateBloodHitEffect(Vector2 location)
    {
        if (TurboMode == false) Instantiate(HitEffectPrefab, location, new Quaternion());
    }

    internal void CreateSwipeHitEffect(Vector2 location, int angle = 0)
    {
        if (TurboMode == false)
        {
            Quaternion quat = Quaternion.Euler(0, 0, angle);
            Instantiate(SwipeEffectPrefab, location, quat);
        }
    }


    private void RedrawTiles()
    {
        Tilemap.ClearAllTiles();
        UnderTilemap.ClearAllTiles();
        FrontTilemap.ClearAllTiles();
        FogOfWar.ClearAllTiles();
        FrontColorTilemap.ClearAllTiles();
        FrontSpriteTilemap.ClearAllTiles();
        EffectTileMap.ClearAllTiles();
        int children = TerrainFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(TerrainFolder.GetChild(i).gameObject);
        }

        for (int i = 0; i <= _tiles.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= _tiles.GetUpperBound(1); j++)
            {
                UnderTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassEnviroment[0]);
                switch (_tiles[i, j])
                {
                    case TacticalTileType.Wall:
                        if (_wallType != WallType.Fence)
                        {
                            int startIndex = (int)TacticalTileType.WallStart + ((int)_wallType - 1) * 4;
                            if (i < _tiles.GetUpperBound(0) - 2 && _tiles[i + 1, j] != TacticalTileType.Wall)
                                FrontTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.TileTypes[startIndex + 2]);
                            else if (i > 0 && _tiles[i - 1, j] != TacticalTileType.Wall)
                                FrontTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.TileTypes[startIndex + 3]);
                            else
                                FrontTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.TileTypes[startIndex + State.Rand.Next(2)]);
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassEnviroment[0]);
                        }
                        else
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.TileTypes[(int)TacticalTileType.Wall]);
                        }

                        break;
                    default:
                        if (_tiles[i, j] >= (TacticalTileType)2400)
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.VolcanicOverLava[(int)_tiles[i, j] - 2400]);
                        }
                        else if (_tiles[i, j] >= (TacticalTileType)2300)
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.VolcanicOverGravel[(int)_tiles[i, j] - 2300]);
                        }
                        else if (_tiles[i, j] >= (TacticalTileType)2200)
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassOverWater[(int)_tiles[i, j] - 2200]);
                        }
                        else if (_tiles[i, j] >= (TacticalTileType)2100)
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.RocksOverTar[(int)_tiles[i, j] - 2100]);
                        }
                        else if (_tiles[i, j] >= (TacticalTileType)2000)
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.RocksOverSand[(int)_tiles[i, j] - 2000]);
                        }
                        else if (_tiles[i, j] >= (TacticalTileType)500)
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.VolcanicTileTypes[(int)_tiles[i, j] - 500]);
                        }
                        else if (_tiles[i, j] >= (TacticalTileType)400)
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.SnowEnviroment[(int)_tiles[i, j] - 400]);
                        }
                        else if (_tiles[i, j] >= (TacticalTileType)300)
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.Paths[(int)_tiles[i, j] - 300]);
                        }
                        else if (_tiles[i, j] >= (TacticalTileType)200)
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.DesertTileTypes[(int)_tiles[i, j] - 200]);
                        }
                        else if (_tiles[i, j] >= TacticalTileType.Greengrass)
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassEnviroment[(int)_tiles[i, j] - 100]);
                        }
                        else
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.TileTypes[(int)_tiles[i, j]]);
                        }

                        break;
                }
            }
        }

        if (Decorations != null)
        {
            foreach (var decoration in Decorations)
            {
                for (int x = 0; x < decoration.TacDec.Tile.GetLength(0); x++)
                {
                    for (int y = 0; y < decoration.TacDec.Tile.GetLength(1); y++)
                    {
                        int i = decoration.LowerLeftPosition.X + x;
                        int j = decoration.LowerLeftPosition.Y + y;
                        if (i >= _tiles.GetLength(0) || j >= _tiles.GetLength(1)) continue;
                        int type = decoration.TacDec.Tile[x, y];
                        if (type >= 500)
                        {
                            if (type < 507)
                                FrontTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.VolcanicTileTypes[type - 500]);
                            else
                            {
                                var obj = Instantiate(State.GameManager.SpriteRendererPrefab, TerrainFolder).GetComponent<SpriteRenderer>();
                                obj.sprite = TileDictionary.VolcanicTileSprites[type - 500];
                                obj.sortingOrder = 20000 - 30 * (i + j * 3);
                                obj.transform.position = new Vector3(i, j, 0);
                                if (y >= decoration.TacDec.Height) obj.sortingOrder += 30;
                            }
                        }
                        else if (type >= 400)
                        {
                            FrontTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.SnowEnviroment[type - 400]);
                        }
                        else if (type >= 200)
                        {
                            if (type < 207)
                                FrontTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.DesertTileTypes[type - 200]);
                            else
                            {
                                var obj = Instantiate(State.GameManager.SpriteRendererPrefab, TerrainFolder).GetComponent<SpriteRenderer>();
                                obj.sprite = TileDictionary.DesertTileSprites[type - 200];
                                obj.sortingOrder = 20000 - 30 * (i + j * 3);
                                obj.transform.position = new Vector3(i, j, 0);
                                if (y >= decoration.TacDec.Height) obj.sortingOrder += 30;
                            }
                        }
                        else if (type >= 100)
                        {
                            if (decoration.TacDec.Height == 0 && decoration.TacDec.Width == 0)
                            {
                                FrontTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassEnviroment[type - 100]);
                            }
                            else
                            {
                                var obj = Instantiate(State.GameManager.SpriteRendererPrefab, TerrainFolder).GetComponent<SpriteRenderer>();
                                obj.sprite = TileDictionary.GrassEnviromentSprites[type - 100];
                                obj.sortingOrder = 20000 - 30 * (i + j * 3);
                                obj.transform.position = new Vector3(i, j, 0);
                                if (y >= decoration.TacDec.Height) obj.sortingOrder += 30;
                            }
                        }
                    }
                }
            }


            //int decNum = Decorations[i, j];
            //if (decNum >= 200)
            //{

            //}
            //else if (Decorations[i, j] >= 100)
            //{
            //    if (DecorationTypes.)
            //    {
            //        FrontTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassEnviroment[Decorations[i, j] - 100]);
            //    }
            //    else
            //    {
            //        var obj = Instantiate(State.GameManager.SpriteRendererPrefab, TerrainFolder).GetComponent<SpriteRenderer>();
            //        obj.sprite = TileDictionary.GrassEnviromentSprites[Decorations[i, j] - 100];
            //        obj.sortingOrder = 20000 - (30 * (i + (j * 3)));
            //        obj.transform.position = new Vector3(i, j, 0);
            //        if (Decorations[i, j] == 105 || Decorations[i, j] == 111)
            //            obj.sortingOrder += 30;
            //    }

            //    //if (Decorations[i, j] == 105 || Decorations[i, j] == 111)
            //    //    FrontSpriteTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassEnviroment[Decorations[i, j] - 100]);
            //    //else
            //    //    FrontTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassEnviroment[Decorations[i, j] - 100]);
            //}
        }

        if (Buildings != null)
        {
            foreach (var building in Buildings)
            {
                for (int y = 0; y < building.Tile.GetLength(0); y++)
                {
                    for (int x = 0; x < building.Tile.GetLength(1); x++)
                    {
                        if (y >= building.Height || x >= building.Width)
                            FrontSpriteTilemap.SetTile(new Vector3Int(building.LowerLeftPosition.X + x, building.LowerLeftPosition.Y + y, 0), _buildingTileTypes[building.Tile[y, x]]);
                        else
                            FrontTilemap.SetTile(new Vector3Int(building.LowerLeftPosition.X + x, building.LowerLeftPosition.Y + y, 0), _buildingTileTypes[building.Tile[y, x]]);
                    }
                }

                for (int y = 0; y < building.FrontColoredTile.GetLength(0); y++)
                {
                    for (int x = 0; x < building.FrontColoredTile.GetLength(1); x++)
                    {
                        if (building.FrontColoredTile[y, x] != -1)
                        {
                            FrontColorTilemap.SetTile(new Vector3Int(building.LowerLeftPosition.X + x, building.LowerLeftPosition.Y + y, 0), _buildingTileTypes[building.FrontColoredTile[y, x]]);
                        }
                    }
                }
            }
        }
    }

    internal ActorUnit AddUnitToBattle(Unit unit, ActorUnit reciepient)
    {
        ActorUnit actor = new ActorUnit(unit, reciepient);
        _units.Add(actor);
        actor.UpdateBestWeapons();
        UpdateActorColor(actor);
        if (actor.UnitSprite != null)
        {
            actor.UnitSprite.HitPercentagesDisplayed(false);
            actor.UnitSprite.DisplaySummoned();
        }

        if (Equals(actor.Unit.Side, _defenderSide))
            DefenderConvert(actor);
        else if (Equals(actor.Unit.Side, _attackerSide)) AttackerConvert(actor);
        return actor;
    }

    internal ActorUnit AddUnitToBattle(Unit unit, Vec2I position)
    {
        ActorUnit actor = new ActorUnit(position, unit);
        _units.Add(actor);
        actor.UpdateBestWeapons();
        UpdateActorColor(actor);
        if (actor.UnitSprite != null)
        {
            actor.UnitSprite.HitPercentagesDisplayed(false);
            actor.UnitSprite.DisplaySummoned();
        }

        if (Equals(actor.Unit.Side, _defenderSide))
            DefenderConvert(actor);
        else if (Equals(actor.Unit.Side, _attackerSide)) AttackerConvert(actor);
        return actor;
    }

    private void UpdateActorColor(ActorUnit actor)
    {
        if (!AIAttacker && AIDefender) //If there's one human, he's blue, otherwise the defender is blue
            actor.UnitSprite.BlueColored = !Equals(_defenderSide, actor.Unit.Side);
        else
            actor.UnitSprite.BlueColored = Equals(_defenderSide, actor.Unit.Side);
    }

    private void CreateActors()
    {
        int children = ActorFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(ActorFolder.GetChild(i).gameObject);
        }

        for (int i = 0; i < _units.Count; i++)
        {
            ActorUnit unit = _units[i];
            unit.UpdateBestWeapons();

            if (!AIAttacker && AIDefender) //If there's one human, he's blue, otherwise the defender is blue
                unit.UnitSprite.BlueColored = !Equals(_defenderSide, unit.Unit.Side);
            else
                unit.UnitSprite.BlueColored = Equals(_defenderSide, unit.Unit.Side);
        }

        for (int i = 0; i < _discardedClothing.Count; i++)
        {
            _discardedClothing[i].GenerateSpritePrefab(ActorFolder);
        }

        for (int i = 0; i < _miscDiscards.Count; i++)
        {
            _miscDiscards[i].GenerateSpritePrefab(ActorFolder);
        }
    }

    internal void CreateDiscardedClothing(Vec2I location, Race race, ClothingId type, int color, string name)
    {
        if (type == null || TurboMode) return;
        if (location == null) return;
        _lastDiscard++;
        int sortOrder = _lastDiscard;
        _discardedClothing.Add(new ClothingDiscards(location, race, type, color, sortOrder, name));
        _discardedClothing.Last().GenerateSpritePrefab(ActorFolder);
    }

    internal void CreateScat(Vec2I location, ScatInfo scatInfo)
    {
        if (TurboMode) return;
        if (location == null) return;
        _lastDiscard++;
        int sortOrder = _lastDiscard;

        if (Config.ScatV2 == true)
        {
            _lastDiscard += 1 + scatInfo.BonesInfos.Count; //scatback + scatfront + bones
            _miscDiscards.Add(new ScatV2Discard(location, sortOrder, scatInfo));
        }
        else
        {
            int spriteNum;
            int offset = Equals(scatInfo.PredRace, Race.Slime) ? 2 : 0;

            if (Config.ScatBones == false)
            {
                spriteNum = 0 + offset;
            }
            else
            {
                if (Equals(scatInfo.PreyRace, Race.Slime))
                    spriteNum = 0 + offset;
                else
                    spriteNum = offset + State.Rand.Next(2);
            }

            _miscDiscards.Add(new MiscDiscard(location, MiscDiscardType.Scat, spriteNum, sortOrder, scatInfo.Color, scatInfo.GetDescription()));
        }

        _miscDiscards.Last().GenerateSpritePrefab(ActorFolder);
    }

    internal void CreateMiscDiscard(Vec2I location, BoneType type, string name, int color = -1)
    {
        if (TurboMode) return;
        if (location == null) return;
        _lastDiscard++;
        int sortOrder = _lastDiscard;
        int spriteNum = (int)type;


        string description = $"Remains of {name}";
        if (type == BoneType.CumPuddle)
            _miscDiscards.Add(new MiscDiscard(location, MiscDiscardType.Cum, spriteNum, sortOrder, color, description));
        else if (type == BoneType.DisposedCondom)
            _miscDiscards.Add(new MiscDiscard(location, MiscDiscardType.DisposedCondom, spriteNum, sortOrder, color, description));
        else
            _miscDiscards.Add(new MiscDiscard(location, MiscDiscardType.Bones, spriteNum, sortOrder, color, description));
        _miscDiscards.Last().GenerateSpritePrefab(ActorFolder);
    }


    private void ShowVoreHitPercentages(ActorUnit actor, PreyLocation location = PreyLocation.Stomach)
    {
        foreach (ActorUnit target in _units)
        {
            if (TacticalUtilities.AppropriateVoreTarget(actor, target) == false) continue;
            if ((Config.EdibleCorpses == false && target.Targetable == false && target.Visible) || target.Visible == false) continue;
            Vec2I pos = target.Position;
            target.UnitSprite.HitPercentagesDisplayed(true);
            if (actor.PredatorComponent.FreeCap() < target.Bulk() || (actor.BodySize() < target.BodySize() * 3 && actor.Unit.HasTrait(TraitType.TightNethers) && PreyLocationMethods.IsGenital(location)))
                target.UnitSprite.DisplayHitPercentage(target.GetDevourChance(actor, true), Color.yellow);
            else if (actor.Unit.CanVore(location) != actor.PredatorComponent.CanVore(location, target))
                target.UnitSprite.DisplayHitPercentage(target.GetDevourChance(actor, true), Color.yellow);
            else if (actor.Position.GetNumberOfMovesDistance(target.Position) < 2)
                target.UnitSprite.DisplayHitPercentage(target.GetDevourChance(actor, true), Color.red);
            else if (actor.Unit.HasTrait(TraitType.RangedVore) && actor.Position.GetNumberOfMovesDistance(target.Position) < 5)
            {
                int dist = target.Position.GetNumberOfMovesDistance(actor.Position);
                if (target.Unit.HasTrait(TraitType.Flight)) dist = 1;
                int boost = -3 * (dist - 1);
                target.UnitSprite.DisplayHitPercentage(target.GetDevourChance(actor, true, skillBoost: boost), Color.red);
            }
            else
                target.UnitSprite.DisplayHitPercentage(target.GetDevourChance(actor, true), Color.black);
        }
    }


    internal bool TakeSpecialAction(SpecialAction type, ActorUnit actor, ActorUnit target)
    {
        switch (type)
        {
            case SpecialAction.PounceVore:
                return actor.VorePounce(target, _lastSpecial);
        }

        if (Equals(actor.Unit.Race, Race.Ki) && type == SpecialAction.CockVore && !Equals(target.Unit.Race, Race.Selicia))
        {
            State.GameManager.CreateMessageBox("Ki will only acccept Selicia into his cock, and nothing else");
            return false;
        }

        if (TacticalActionList.TargetedDictionary.TryGetValue(type, out var targetedAction))
        {
            if (targetedAction.RequiresPred && actor.Unit.Predator == false) return false;
            if (targetedAction.OnExecute == null) return false;
            targetedAction.OnExecute(actor, target);
            return true;
        }


        return false;
    }

    internal bool TakeSpecialActionLocation(SpecialAction type, ActorUnit actor, Vec2I location)
    {
        if (TacticalActionList.TargetedDictionary.TryGetValue(type, out var targetedAction))
        {
            if (targetedAction.OnExecuteLocation != null) return targetedAction.OnExecuteLocation(actor, location);
        }

        return false;
    }


    private void ShowSpecialHitPercentages(ActorUnit actor)
    {
        switch (_specialType)
        {
            case SpecialAction.Unbirth:
                ShowVoreHitPercentages(actor, PreyLocation.Womb);
                break;
            case SpecialAction.CockVore:
                ShowVoreHitPercentages(actor, PreyLocation.Balls);
                break;
            case SpecialAction.TailVore:
                ShowVoreHitPercentages(actor, PreyLocation.Tail);
                break;
            case SpecialAction.AnalVore:
                ShowVoreHitPercentages(actor, PreyLocation.Anal);
                break;
            case SpecialAction.BreastVore:
                ShowVoreHitPercentages(actor, PreyLocation.Breasts);
                break;
            case SpecialAction.Transfer:
                ShowCockVoreTransferPercentages(actor);
                break;
            case SpecialAction.StealVore:
                ShowVoreStealPercentages(actor);
                break;
            case SpecialAction.BreastFeed:
                ShowBreastFeedPercentages(actor);
                break;
            case SpecialAction.CumFeed:
                ShowCumFeedPercentages(actor);
                break;
            case SpecialAction.Suckle:
                ShowSucklePercentages(actor);
                break;
            case SpecialAction.BellyRub:
                ShowRubHitPercentages(actor);
                break;
            case SpecialAction.PounceMelee:
                if (actor.Movement > 1) ShowPounceMeleeHitPercentages(actor);
                break;
            case SpecialAction.PounceVore:
                if (actor.Movement > 1) ShowPounceVoreHitPercentages(actor);
                break;
            case SpecialAction.ShunGokuSatsu:
                ShowMeleeHitPercentages(actor, 2);
                break;
            case SpecialAction.TailStrike:
                ShowMeleeHitPercentages(actor, .66f);
                break;
        }
    }

    private void ShowRubHitPercentages(ActorUnit actor)
    {
        foreach (ActorUnit target in _units)
        {
            if (!Equals(target.Unit.GetApparentSide(actor.Unit), actor.Unit.FixedSide) && !Equals(target.Unit.GetApparentSide(actor.Unit), actor.Unit.GetApparentSide()) &&
                !(actor.Unit.HasTrait(TraitType.SeductiveTouch) || Config.CanUseStomachRubOnEnemies))
                continue;
            if ((target.Targetable == false && target.Visible) || target.Visible == false) continue;
            if (target.PredatorComponent?.Fullness <= 0) continue;
            if (target.ReceivedRub) continue;
            Vec2I pos = target.Position;
            target.UnitSprite.HitPercentagesDisplayed(true);
            if (actor.Unit.HasTrait(TraitType.SeductiveTouch) && !Equals(target.Unit.Side, actor.Unit.Side))
            {
                float charmChance = target.GetPureStatClashChance(actor.Unit.GetStat(Stat.Dexterity), target.Unit.GetStat(Stat.Will), .1f);
                if (actor.Position.GetNumberOfMovesDistance(target.Position) < 2)
                    target.UnitSprite.DisplayHitPercentage(charmChance, Color.magenta);
                else
                    target.UnitSprite.DisplayHitPercentage(charmChance, Color.black);
            }
            else
            {
                if (actor.Position.GetNumberOfMovesDistance(target.Position) < 2)
                    target.UnitSprite.DisplayHitPercentage(1f, Color.red);
                else
                    target.UnitSprite.DisplayHitPercentage(1f, Color.black);
            }
        }
    }

    private void ShowCockVoreTransferPercentages(ActorUnit actor)
    {
        if (actor.Unit.Predator == false) return;
        foreach (ActorUnit target in _units)
        {
            if (target.Unit.Predator == false) continue;
            if (Equals(target.Unit.Side, actor.Unit.Side) && target.Surrendered == false)
            {
                if (actor.PredatorComponent.CanTransfer())
                {
                    if (target.PredatorComponent.FreeCap() < actor.PredatorComponent.TransferBulk() && !(target.Unit == actor.Unit))
                        target.UnitSprite.DisplayHitPercentage(target.GetSpecialChance(SpecialAction.Transfer), Color.yellow);
                    else if (actor.Position.GetNumberOfMovesDistance(target.Position) < 2)
                        target.UnitSprite.DisplayHitPercentage(target.GetSpecialChance(SpecialAction.Transfer), Color.red);
                    else
                        target.UnitSprite.DisplayHitPercentage(target.GetSpecialChance(SpecialAction.Transfer), Color.black);
                }

                continue;
            }
        }
    }

    private void ShowVoreStealPercentages(ActorUnit actor)
    {
        foreach (ActorUnit target in _units)
        {
            if (!actor.PredatorComponent.CanVoreSteal(target)) continue;
            if (actor.PredatorComponent.FreeCap() < target.PredatorComponent.StealBulk() && !(target.Unit == actor.Unit))
                target.UnitSprite.DisplayHitPercentage(target.PredatorComponent.GetVoreStealChance(actor), Color.yellow);
            else if (actor.Position.GetNumberOfMovesDistance(target.Position) < 2)
                target.UnitSprite.DisplayHitPercentage(target.PredatorComponent.GetVoreStealChance(actor), Color.red);
            else
                target.UnitSprite.DisplayHitPercentage(target.PredatorComponent.GetVoreStealChance(actor), Color.black);
            continue;
        }
    }


    private void ShowBreastFeedPercentages(ActorUnit actor)
    {
        if (actor.Unit.Predator == false) return;
        foreach (ActorUnit target in _units)
        {
            if ((Equals(target.Unit.GetApparentSide(actor.Unit), actor.Unit.FixedSide) || Equals(target.Unit.GetApparentSide(actor.Unit), actor.Unit.GetApparentSide()) || target.Unit == actor.Unit) && target.Surrendered == false)
            {
                if (actor.PredatorComponent.CanFeed())
                {
                    if (actor.Position.GetNumberOfMovesDistance(target.Position) < 2)
                        target.UnitSprite.DisplayHitPercentage(target.GetSpecialChance(SpecialAction.BreastFeed), Color.red);
                    else
                        target.UnitSprite.DisplayHitPercentage(target.GetSpecialChance(SpecialAction.BreastFeed), Color.black);
                }

                continue;
            }
        }
    }


    private void ShowCumFeedPercentages(ActorUnit actor)
    {
        if (actor.Unit.Predator == false) return;
        foreach (ActorUnit target in _units)
        {
            if ((Equals(target.Unit.GetApparentSide(actor.Unit), actor.Unit.FixedSide) || Equals(target.Unit.GetApparentSide(actor.Unit), actor.Unit.GetApparentSide()) || target.Unit == actor.Unit) && target.Surrendered == false)
            {
                if (actor.PredatorComponent.CanFeedCum())
                {
                    if (actor.Position.GetNumberOfMovesDistance(target.Position) < 2)
                        target.UnitSprite.DisplayHitPercentage(target.GetSpecialChance(SpecialAction.CumFeed), Color.red);
                    else
                        target.UnitSprite.DisplayHitPercentage(target.GetSpecialChance(SpecialAction.CumFeed), Color.black);
                }

                continue;
            }
        }
    }

    private void ShowSucklePercentages(ActorUnit actor)
    {
        if (actor.Unit.Predator == false) return;
        foreach (ActorUnit target in _units)
        {
            if (actor.PredatorComponent.GetSuckle(target)[0] == 0)
                continue;
            else if (actor.Position.GetNumberOfMovesDistance(target.Position) == 1)
                target.UnitSprite.DisplayHitPercentage(actor.PredatorComponent.GetSuckleChance(target), Color.red);
            else
                target.UnitSprite.DisplayHitPercentage(actor.PredatorComponent.GetSuckleChance(target), Color.black);
        }
    }

    private void ShowPounceMeleeHitPercentages(ActorUnit actor)
    {
        foreach (ActorUnit target in _units)
        {
            if ((TacticalUtilities.IsUnitControlledByPlayer(target.Unit) && Config.AllowInfighting == false && !(!AIDefender && !AIAttacker)) || actor == target) continue;
            if (target.Targetable == false || target.Visible == false) continue;
            if (TacticalUtilities.FreeSpaceAroundTarget(target.Position, actor) == false) continue;
            int weaponDamage = actor.WeaponDamageAgainstTarget(target, false);
            if (actor.Unit.HasTrait(TraitType.HeavyPounce)) weaponDamage = (int)Mathf.Min(weaponDamage + (weaponDamage * actor.PredatorComponent?.Fullness ?? 0) / 4, weaponDamage * 2);
            Vec2I pos = target.Position;
            if (actor.Position.GetNumberOfMovesDistance(target.Position) <= 4 && actor.Position.GetNumberOfMovesDistance(target.Position) >= 2)
                target.UnitSprite.DisplayHitPercentage(target.GetAttackChance(actor, false, true), Color.red, weaponDamage);
            else
                target.UnitSprite.DisplayHitPercentage(target.GetAttackChance(actor, false, true), Color.black, weaponDamage);
        }
    }

    private void ShowPounceVoreHitPercentages(ActorUnit actor)
    {
        foreach (ActorUnit target in _units)
        {
            if (TacticalUtilities.AppropriateVoreTarget(actor, target) == false) continue;
            if ((Config.EdibleCorpses == false && target.Targetable == false && target.Visible) || target.Visible == false) continue;
            if (TacticalUtilities.FreeSpaceAroundTarget(target.Position, actor) == false) continue;
            Vec2I pos = target.Position;
            target.UnitSprite.HitPercentagesDisplayed(true);
            if (actor.PredatorComponent.FreeCap() < target.Bulk())
                target.UnitSprite.DisplayHitPercentage(target.GetDevourChance(actor, true), Color.yellow);
            else if (actor.Position.GetNumberOfMovesDistance(target.Position) <= 4 && actor.Position.GetNumberOfMovesDistance(target.Position) >= 2)
                target.UnitSprite.DisplayHitPercentage(target.GetDevourChance(actor, true), Color.red);
            else
                target.UnitSprite.DisplayHitPercentage(target.GetDevourChance(actor, true), Color.black);
        }
    }

    private void ShowMeleeHitPercentages(ActorUnit actor, float multiplier = 1)
    {
        foreach (ActorUnit target in _units)
        {
            if ((TacticalUtilities.IsUnitControlledByPlayer(target.Unit) && Config.AllowInfighting == false && !(!AIDefender && !AIAttacker)) || actor == target) continue;
            if (target.Targetable == false || target.Visible == false) continue;
            int weaponDamage = (int)(multiplier * actor.WeaponDamageAgainstTarget(target, false));
            Vec2I pos = target.Position;
            if (actor.Position.GetNumberOfMovesDistance(target.Position) < 2)
                target.UnitSprite.DisplayHitPercentage(target.GetAttackChance(actor, false, true), Color.red, weaponDamage);
            else
                target.UnitSprite.DisplayHitPercentage(target.GetAttackChance(actor, false, true), Color.black, weaponDamage);
        }
    }


    private void ShowRangedHitPercentages(ActorUnit actor)
    {
        foreach (ActorUnit target in _units)
        {
            if ((TacticalUtilities.IsUnitControlledByPlayer(target.Unit) && Config.AllowInfighting == false && !(!AIDefender && !AIAttacker)) || actor == target) continue;
            if (target.Targetable == false || target.Visible == false) continue;
            int weaponDamage = actor.WeaponDamageAgainstTarget(target, true);

            Vec2I pos = target.Position;
            if (actor.Position.GetNumberOfMovesDistance(target.Position) <= actor.BestRanged.Range && (actor.Position.GetNumberOfMovesDistance(target.Position) > 1 || (actor.BestRanged.Omni && actor.Position.GetNumberOfMovesDistance(target.Position) > 0)))
                target.UnitSprite.DisplayHitPercentage(target.GetAttackChance(actor, true, true), Color.red, weaponDamage);
            else
                target.UnitSprite.DisplayHitPercentage(target.GetAttackChance(actor, true, true), Color.black, weaponDamage);
        }
    }

    private void ShowMagicPercentages(ActorUnit actor)
    {
        foreach (ActorUnit target in _units)
        {
            if (_currentSpell.AcceptibleTargets.Contains(AbilityTargets.Self) == true && actor.Unit != target.Unit) continue;
            if (_currentSpell.AcceptibleTargets.Contains(AbilityTargets.Ally) == false && TacticalUtilities.IsUnitControlledByPlayer(target.Unit) && Config.AllowInfighting == false && !(!AIDefender && !AIAttacker)) continue;
            if (_currentSpell.AcceptibleTargets.Contains(AbilityTargets.Enemy) == false && !(TacticalUtilities.IsUnitControlledByPlayer(target.Unit) || Equals(target.Unit.Side, actor.Unit.Side)) && !(!AIDefender && !AIAttacker)) continue;

            if (target.Targetable == false || target.Visible == false) continue;
            int spellDamage = 0;
            if (_currentSpell is DamageSpell damageSpell)
            {
                spellDamage = damageSpell.Damage(actor, target);
                if (TacticalUtilities.SneakAttackCheck(actor.Unit, target.Unit)) // sneakAttack
                {
                    spellDamage *= 3;
                }
            }

            float magicChance = _currentSpell.Resistable ? target.GetMagicChance(actor, _currentSpell) : 1;

            if (_currentSpell == SpellList.Maw || _currentSpell == SpellList.GateMaw) magicChance *= target.GetDevourChance(actor, skillBoost: actor.Unit.GetStat(Stat.Mind));

            if (_currentSpell == SpellList.Bind && target.Unit.Type != UnitType.Summon) magicChance = 0;

            Vec2I pos = target.Position;
            if (actor.Position.GetNumberOfMovesDistance(target.Position) <= _currentSpell.Range.Max && actor.Position.GetNumberOfMovesDistance(target.Position) >= _currentSpell.Range.Min)
                target.UnitSprite.DisplayHitPercentage(magicChance, Color.red, spellDamage);
            else
                target.UnitSprite.DisplayHitPercentage(magicChance, Color.black, spellDamage);
        }
    }

    private void RemoveHitPercentages()
    {
        foreach (ActorUnit target in _units)
        {
            target.UnitSprite?.HitPercentagesDisplayed(false);
        }
    }

    private void CreateMovementGrid()
    {
        _walkable = TacticalPathfinder.GetGrid(SelectedUnit.Position, SelectedUnit.Unit.HasTrait(TraitType.Flight), SelectedUnit.Movement, SelectedUnit);
        UpdateMovementGrid();
    }

    private void UpdateMovementGrid()
    {
        MovementGrid.ClearAllTiles();
        for (int x = 0; x <= _tiles.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= _tiles.GetUpperBound(1); y++)
            {
                if (_walkable[x, y]) MovementGrid.SetTile(new Vector3Int(x, y, 0), MovementGridTileTypes[0]);
            }
        }
    }

    private void UpdateAreaOfEffectGrid(Vec2I mouseLocation)
    {
        MovementGrid.ClearAllTiles();

        int radius = _currentSpell.AreaOfEffect;
        bool outOfRange = mouseLocation.GetNumberOfMovesDistance(SelectedUnit.Position) > _currentSpell.Range.Max;

        for (int x = mouseLocation.X - radius; x <= mouseLocation.X + radius; x++)
        {
            for (int y = mouseLocation.Y - radius; y <= mouseLocation.Y + radius; y++)
            {
                if (outOfRange)
                    MovementGrid.SetTile(new Vector3Int(x, y, 0), MovementGridTileTypes[0]);
                else
                    MovementGrid.SetTile(new Vector3Int(x, y, 0), MovementGridTileTypes[1]);
            }
        }
    }

    private void UpdateTailStrikeGrid(Vec2I mouseLocation)
    {
        MovementGrid.ClearAllTiles();

        if (SelectedUnit.Position.GetNumberOfMovesDistance(mouseLocation.X, mouseLocation.Y) != 1) return;

        Vec2 pos = mouseLocation;
        TestTile(pos);
        TestTile(pos + new Vec2(1, 0));
        TestTile(pos + new Vec2(0, 1));
        TestTile(pos + new Vec2(-1, 0));
        TestTile(pos + new Vec2(0, -1));

        void TestTile(Vec2 p)
        {
            if (SelectedUnit.Position.GetNumberOfMovesDistance(p.X, p.Y) == 1) MovementGrid.SetTile(new Vector3Int(p.X, p.Y, 0), MovementGridTileTypes[1]);
        }
    }


    private void UpdateAttackGrid(Vec2I source)
    {
        int range = SelectedUnit.BestRanged?.Range ?? 1;
        for (int x = 0; x <= _tiles.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= _tiles.GetUpperBound(1); y++)
            {
                if (source.GetNumberOfMovesDistance(new Vec2I(x, y)) <= range)
                {
                    if (MovementGrid.GetTile(new Vector3Int(x, y, 0)) == MovementGridTileTypes[0])
                        MovementGrid.SetTile(new Vector3Int(x, y, 0), MovementGridTileTypes[2]);
                    else
                        MovementGrid.SetTile(new Vector3Int(x, y, 0), MovementGridTileTypes[1]);
                }
            }
        }
    }

    private bool ButtonsInteractable => (IsPlayerTurn || PseudoTurn) == true && (RunningFriendlyAI || _foreignAI != null) == false && _queuedPath == null && _paused == false;


    public void ButtonCallback(int id)
    {
        RightClickMenu.CloseAll();
        if (ButtonsInteractable)
        {
            switch (id)
            {
                case 0:
                    if (SelectedUnit != null && SelectedUnit.Targetable)
                        if (SelectedUnit.BestMelee != null && SelectedUnit.Movement > 0)
                        {
                            ActionMode = 1;
                        }

                    break;
                case 1:
                    if (SelectedUnit != null && SelectedUnit.Targetable)
                        if (SelectedUnit.BestRanged != null && SelectedUnit.Movement > 0)
                        {
                            ActionMode = 2;
                        }

                    break;
                case 2:
                    if (SelectedUnit != null && SelectedUnit.Targetable && SelectedUnit.Unit.Predator)
                        if (SelectedUnit.Movement > 0)
                        {
                            ActionMode = 3;
                        }

                    break;
                case 3:
                    if (State.TutorialMode && State.GameManager.TutorialScript.Step < 6) return;
                    if (PseudoTurn)
                    {
                        IgnorePseudo = true;
                    }
                    else
                        RunningFriendlyAI = true;

                    break;
                case 4:
                    PromptEndTurn();
                    break;
                case 7:
                    if (SelectedUnit != null && SelectedUnit.Targetable)
                        if (SelectedUnit.Movement > 0)
                        {
                            _currentPathDestination = null;
                            ActionMode = 5;
                        }

                    break;
                case 9:
                    if (State.TutorialMode && State.GameManager.TutorialScript.Step < 6) return;
                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    {
                        bool curAttacker = _attackersTurn;
                        var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
                        box.SetData(() => SurrenderAll(curAttacker), "Yes", "No", "Are you sure you want to surrender all your units (you were holding left shift)");
                    }
                    else if (SelectedUnit != null && SelectedUnit.Targetable)
                    {
                        if (SelectedUnit.Surrendered && SelectedUnit.SurrenderedThisTurn)
                        {
                            SelectedUnit.Surrendered = false;
                            SelectedUnit.SurrenderedThisTurn = false;
                            RebuildInfo();
                        }
                        else if (SelectedUnit.Surrendered == false)
                        {
                            SelectedUnit.Surrendered = true;
                            SelectedUnit.SurrenderedThisTurn = true;
                            RebuildInfo();
                        }
                    }

                    break;
                case 10:
                    if (SelectedUnit != null && SelectedUnit.Targetable)
                    {
                        AttemptRetreat(SelectedUnit, false);
                        RebuildInfo();
                    }

                    break;

                //case 11-13: Handled Below As they are now independent of player turns                  

                case 40:
                    UndoMovement();
                    break;
                case 95:
                    if (SelectedUnit != null)
                    {
                        if (State.TutorialMode && State.GameManager.TutorialScript.Step < 6)
                        {
                            State.GameManager.CreateMessageBox("Can't use this in this tutorial battle");
                            break;
                        }

                        SelectedUnit.Movement = 0;
                        RebuildInfo();
                    }

                    break;
            }
        }
        else if (_reviewingBattle)
        {
            if (id == 4) PromptEndTurn();
        }

        if (id == 11)
        {
            if (AIDefender && AIAttacker)
            {
                _manualSkip = true;
                TurboModeMethod();
                return;
            }

            if (State.TutorialMode && State.GameManager.TutorialScript.Step < 6)
            {
                State.GameManager.CreateMessageBox("Can't use this skip in the tutorial");
                return;
            }

            SkipUI.gameObject.SetActive(true);
            if (SkipUI.KeepSettings.isOn == false)
            {
                SkipUI.WatchRest.isOn = false;
                SkipUI.AllowRetreat.isOn = false;
                SkipUI.Surrender.isOn = false;
            }
        }

        if (id == 12)
        {
            SkipUI.gameObject.SetActive(false);
        }

        if (id == 13)
        {
            SkipUI.gameObject.SetActive(false);
            ProcessSkip(SkipUI.Surrender.isOn, SkipUI.WatchRest.isOn);
        }

        if (id == 14)
        {
            if (SelectedUnit != null && SelectedUnit.Targetable) SwitchAlignment(SelectedUnit);
        }

        if (id == 15)
        {
            if (SelectedUnit != null && SelectedUnit.Targetable) SelectedUnit.Unit.HiddenFixedSide = false;
        }

        if (id == 16)
        {
            if (SelectedUnit != null && SelectedUnit.Targetable) TacticalUtilities.ShapeshifterPanel(SelectedUnit);
        }
    }

    internal void ProcessSkip(bool surrender, bool watchRest)
    {
        var defenderRace = _armies[1]?.EmpireOutside?.ReplacedRace ?? _village?.Empire?.ReplacedRace ?? _defenderSide.ToRace();
        var attackerRace = _armies[0]?.EmpireOutside?.ReplacedRace;

        _manualSkip = true;
        if (IsPlayerTurn == false)
        {
            if (AIDefender == false)
            {
                object[] argArray = { _units, _tiles, _defenderSide, false };
                RaceAI rai = State.RaceSettings.GetRaceAI(defenderRace);
                _defenderAI = Activator.CreateInstance(RaceAIType.Dict[rai], args: argArray) as TacticalAI;
                if (SkipUI.AllowRetreat.isOn) _defenderAI.RetreatPlan = new TacticalAI.RetreatConditions(.2f, 0);
                AIDefender = true;
                if (surrender)
                {
                    foreach (ActorUnit actor in _units.Where(s => Equals(s.Unit.Side, _defenderSide) && UnitControllableBySide(s, _defenderSide)))
                    {
                        actor.Surrendered = true;
                        actor.Movement = 0;
                    }
                }
            }
            else if (AIAttacker == false)
            {
                object[] argArray = { _units, _tiles, _attackerSide, false };
                //RaceAI rai = State.RaceSettings.GetRaceAI(attackerRace.Value);
                RaceAI rai = State.RaceSettings.GetRaceAI(attackerRace);
                _attackerAI = Activator.CreateInstance(RaceAIType.Dict[rai], args: argArray) as TacticalAI;
                if (SkipUI.AllowRetreat.isOn) _attackerAI.RetreatPlan = new TacticalAI.RetreatConditions(.2f, 0);
                AIAttacker = true;
                if (surrender)
                {
                    foreach (ActorUnit actor in _units.Where(s => !Equals(s.Unit.Side, _defenderSide) && UnitControllableBySide(s, s.Unit.Side)))
                    {
                        actor.Surrendered = true;
                        actor.Movement = 0;
                    }
                }
            }

            if (watchRest == false)
                TurboModeMethod();
            else
                SpectatorMode = true;
            return;
        }

        if (_attackersTurn)
        {
            object[] argArray = { _units, _tiles, _activeSide, false };
            //RaceAI rai = State.RaceSettings.GetRaceAI(attackerRace.Value);
            RaceAI rai = State.RaceSettings.GetRaceAI(attackerRace);
            _attackerAI = Activator.CreateInstance(RaceAIType.Dict[rai], args: argArray) as TacticalAI;
            if (SkipUI.AllowRetreat.isOn) _attackerAI.RetreatPlan = new TacticalAI.RetreatConditions(.2f, 0);
            AIAttacker = true;
            _currentAI = _attackerAI;
        }
        else
        {
            object[] argArray = { _units, _tiles, _activeSide, false };
            RaceAI rai = State.RaceSettings.GetRaceAI(defenderRace);
            _defenderAI = Activator.CreateInstance(RaceAIType.Dict[rai], args: argArray) as TacticalAI;
            if (SkipUI.AllowRetreat.isOn) _defenderAI.RetreatPlan = new TacticalAI.RetreatConditions(.2f, 0);
            AIDefender = true;
            _currentAI = _defenderAI;
        }

        AITimer = 1;
        IsPlayerTurn = false;
        if (surrender)
        {
            foreach (ActorUnit actor in _units.Where(s => Equals(s.Unit.Side, _activeSide) && UnitControllableBySide(s, _activeSide)))
            {
                actor.Surrendered = true;
                actor.Movement = 0;
            }
        }

        if (watchRest == false)
            TurboModeMethod();
        else
            SpectatorMode = true;
    }

    internal void SetMagicMode(Spell spell)
    {
        if (ButtonsInteractable && spell != null && SelectedUnit != null && SelectedUnit.Targetable && SelectedUnit.Movement > 0)
        {
            _currentSpell = spell;
            ActionMode = 6;
        }
    }

    internal void TrySetSpecialMode(SpecialAction mode)
    {
        if (TacticalActionList.TargetedDictionary.TryGetValue(mode, out var targetedAction))
        {
            if (SelectedUnit == null || SelectedUnit.Targetable == false || SelectedUnit.Movement < targetedAction.MinimumMp) return;
            if (targetedAction.RequiresPred && SelectedUnit.Unit.Predator == false) return;
            _lastSpecial = _specialType;
            _specialType = mode;
            ActionMode = 4;
        }
    }

    private void PromptEndTurn()
    {
        if (Config.PromptEndTurn == false)
        {
            EndTurn();
            return;
        }

        bool canStillMove = false;
        for (int i = 0; i < _units.Count; i++)
        {
            if (TacticalUtilities.IsUnitControlledByPlayer(_units[i].Unit) && _units[i].Targetable && _units[i].Movement > 0)
            {
                canStillMove = true;
                break;
            }
        }

        if (canStillMove)
        {
            var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
            box.SetData(EndTurn, "Yes", "No", "You still have units left that are capable of moving, end turn anyway?");
        }
        else
            EndTurn();
    }

    internal void SurrenderAll(bool attacker)
    {
        for (int i = 0; i < _units.Count; i++)
        {
            if (attacker ? !Equals(_units[i].Unit.Side, _defenderSide) : Equals(_units[i].Unit.Side, _defenderSide))
            {
                _units[i].Surrendered = true;
                _units[i].SurrenderedThisTurn = true;
            }
        }

        RebuildInfo();
    }

    internal void KillAll(bool attacker)
    {
        for (int i = 0; i < _units.Count; i++)
        {
            if (attacker ? !Equals(_units[i].Unit.Side, _defenderSide) : Equals(_units[i].Unit.Side, _defenderSide))
            {
                _units[i].Unit.Health = -1;
            }
        }

        RebuildInfo();
    }

    internal void AttemptRetreat(ActorUnit actor, bool silent)
    {
        if (CurrentTurn < actor.Unit.TraitBoosts.TurnCanFlee)
        {
            if (silent == false) State.GameManager.CreateMessageBox($"Can't retreat before the {actor.Unit.TraitBoosts.TurnCanFlee}th turn");
            return;
        }

        if (actor.Movement <= 0)
        {
            if (silent == false) State.GameManager.CreateMessageBox("Unit needs at least 1 mp to flee");
            return;
        }

        if (actor.Unit.Type == UnitType.Summon)
        {
            if (silent == false) State.GameManager.CreateMessageBox("A summoned unit can not flee");
            return;
        }

        if (actor.Unit.HasTrait(TraitType.Fearless))
        {
            if (silent == false) State.GameManager.CreateMessageBox("A unit with the fearless trait can not flee");
            return;
        }

        if (actor.Unit.Type == UnitType.SpecialMercenary)
        {
            if (silent == false) State.GameManager.CreateMessageBox($"{actor.Unit.Name}'s pride prevents them from fleeing (Special merc)");
            return;
        }

        if (Equals(actor.Unit.Side, _defenderSide))
        {
            if (actor.Position.Y == 0)
            {
                RetreatUnit(actor, true);
            }
            else if (silent == false) State.GameManager.CreateMessageBox("Unit must be on the bottom edge of the map to flee");
        }
        else
        {
            if (actor.Position.Y == _tiles.GetUpperBound(1))
            {
                RetreatUnit(actor, false);
            }
            else if (silent == false) State.GameManager.CreateMessageBox("Unit must be on the top edge of the map to flee");
        }
    }

    private void RetreatUnit(ActorUnit actor, bool defender)
    {
        if (defender)
        {
            if (_armies[1] != null) _armies[1].Units.Remove(actor.Unit);
            _retreatedDefenders.Add(actor.Unit);
        }
        else
        {
            _armies[0].Units.Remove(actor.Unit);
            _retreatedAttackers.Add(actor.Unit);
        }

        if (actor.PredatorComponent?.PreyCount > 0) _retreatedDigestors.Add(actor);
        actor.Visible = false;
        actor.Targetable = false;
        actor.UnitSprite.gameObject.SetActive(false);
        actor.Fled = true;
        actor.Unit.RefreshSecrecy();
        SelectedUnit = null;
    }

    private void UpdateStatus(float dt)
    {
        
        if (SpectatorMode) RunningFriendlyAI = true;

        Translator?.UpdateLocation();

        if (State.World.IsNight)
        {
            UpdateFog();
        }

        SpellHelperText.SetActive(ActionMode == 6 && _currentSpell.AcceptibleTargets.Contains(AbilityTargets.Tile));

        if (SelectedUnit != null)
        {
            if (SelectedUnit.Targetable)
            {
                if (SelectedUnit.UnitSprite != null)
                    SelectionBox.position = SelectedUnit.UnitSprite.transform.position;
                else
                    SelectionBox.position = new Vector2(SelectedUnit.Position.X, SelectedUnit.Position.Y);
                SelectionBox.gameObject.SetActive(true);
            }
            else
            {
                SelectionBox.gameObject.SetActive(false);
                SelectedUnit = null;
            }
        }

        foreach (var unit in _units)
        {
            if (unit.Visible != unit.UnitSprite.GraphicsFolder.gameObject.activeSelf)
            {
                unit.UnitSprite.GraphicsFolder.gameObject.SetActive(unit.Visible);
                unit.UnitSprite.OtherFolder.gameObject.SetActive(unit.Visible);
            }

            if (unit.Visible)
            {
                unit.Update(dt);
                unit.AnimationController?.UpdateTimes(dt);
                unit.UnitSprite.UpdateSprites(unit, Equals(unit.Unit.Side, _activeSide));

                // TODO discriminate between living and dead prey

                if (Config.CloseInDigestionNoises && unit.PredatorComponent?.PreyCount > 0)
                {
                    if (unit.PredatorComponent?.AlivePrey > 0)
                    {
                        foreach (var loc in PredatorComponent.PreyLocationOrder)
                        {
                            if (unit.PredatorComponent.PreyInLocation(loc, true) > 0)
                            {
                                State.GameManager.SoundManager.PlayDigestLoop(loc, unit);
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (var loc in PredatorComponent.PreyLocationOrder)
                        {
                            if (unit.PredatorComponent.PreyInLocation(loc, false) > 0)
                            {
                                State.GameManager.SoundManager.PlayAbsorbLoop(loc, unit);
                                break;
                            }
                        }
                    }
                }


                if (unit.PredatorComponent?.AlivePrey > 0 && unit.PredatorComponent?.Fullness > 0)
                {
                    if (Equals(unit.Unit.Race, Race.EasternDragon))
                        unit.UnitSprite.AnimateBelly(unit.PredatorComponent.PreyInLocation(PreyLocation.Stomach, true) * 0.0022f);
                    else
                        unit.UnitSprite.AnimateBelly(unit.PredatorComponent.PreyNearLocation(PreyLocation.Stomach, true) * 0.0022f);
                }

                if (unit.PredatorComponent?.BallsFullness > 0 && unit.PredatorComponent?.AlivePrey > 0)
                {
                    unit.UnitSprite.AnimateBalls(unit.PredatorComponent.PreyNearLocation(PreyLocation.Balls, true) * 0.0022f);
                }

                if (unit.PredatorComponent?.BreastFullness > 0 && unit.PredatorComponent?.AlivePrey > 0)
                {
                    unit.UnitSprite.AnimateBoobs(unit.PredatorComponent.PreyNearLocation(PreyLocation.Breasts, true) * 0.0022f);
                }

                if (unit.PredatorComponent?.LeftBreastFullness > 0 && unit.PredatorComponent?.AlivePrey > 0)
                {
                    if (Config.FairyBvType == FairyBVType.Shared)
                    {
                        unit.UnitSprite.AnimateBoobs(unit.PredatorComponent.PreyNearLocation(PreyLocation.LeftBreast, true) * 0.022f);
                        unit.UnitSprite.AnimateSecondBoobs(unit.PredatorComponent.PreyNearLocation(PreyLocation.LeftBreast, true) * 0.022f);
                    }
                    else
                        unit.UnitSprite.AnimateBoobs(unit.PredatorComponent.PreyNearLocation(PreyLocation.LeftBreast, true) * 0.0022f);
                }

                if (unit.PredatorComponent?.RightBreastFullness > 0 && unit.PredatorComponent?.AlivePrey > 0)
                {
                    unit.UnitSprite.AnimateSecondBoobs(unit.PredatorComponent.PreyNearLocation(PreyLocation.RightBreast, true) * 0.0022f);
                }
            }
        }

        if (_waitingForDialog) return;
        var foreignUnits = _units.Where(unit => Equals(unit.Unit.Side, _activeSide) && !TacticalUtilities.IsUnitControlledByPlayer(unit.Unit) && unit.Movement > 0).ToList();
        if (IsPlayerTurn)
        {
            if (RunningFriendlyAI)
            {
                if (AITimer > 0)
                {
                    AITimer -= dt;
                }
                else
                {
                    RefreshAIIfNecessary();
                    //do AI processing
                    if (_currentAI.RunAI() == false)
                    {
                        RunningFriendlyAI = false;
                        EndTurn();
                    }

                    if (AITimer <= 0) AITimer = Config.TacticalPlayerMovementDelay;
                }
            }
            else if (_foreignAI != null || foreignUnits.Count() > 0)
            {
                Type desiredAIType;
                if (foreignUnits.Count() > 0)
                    desiredAIType = !Equals(TacticalUtilities.GetMindControlSide(foreignUnits[0].Unit), Side.TrueNoneSide) ? GetAITypeForMindControledUnit(foreignUnits[0].Unit) : RaceAIType.Dict[State.RaceSettings.GetRaceAI(foreignUnits[0].Unit.Race)];
                else
                    desiredAIType = typeof(StandardTacticalAI);
                if (_foreignAI == null || _foreignAI.GetType() != desiredAIType)
                {
                    object[] argArray = { _units, _tiles, _activeSide, false };
                    _foreignAI = Activator.CreateInstance(desiredAIType, args: argArray) as TacticalAI;
                }

                _foreignAI.ForeignTurn = true;
                if (AITimer > 0)
                {
                    AITimer -= dt;
                }
                else
                {
                    //do AI processing
                    if (_foreignAI.RunAI() == false)
                    {
                        _foreignAI = null;
                    }

                    if (AITimer <= 0) AITimer = Config.TacticalPlayerMovementDelay;
                }
            }
        }
        else
        {
            if (_autoAdvancing == false) AI(dt);
        }

        if (_autoAdvancing)
        {
            _autoAdvanceTimer -= Time.deltaTime;
            if (_autoAdvanceTimer <= 0)
            {
                EndTurn();
                if (Config.AutoAdvance == Config.AutoAdvanceType.SkipToEnd)
                {
                    while (_autoAdvancing && CurrentTurn < 2000)
                    {
                        if (_waitingForDialog) return;
                        EndTurn();
                    }
                }
                else
                    _autoAdvanceTimer = AutoAdvanceRate;
            }
        }

        if (_queuedPath != null)
        {
            if (SelectedUnit == null)
                _queuedPath = null;
            else if (SelectedUnit.Movement <= 0)
            {
                _queuedPath = null;
                SelectedUnit.ClearMovement();
            }
            else if (Translator.IsActive == false)
            {
                if (_queuedPath.Count > 0)
                {
                    if (SelectedUnit.MoveTo(new Vec2I(_queuedPath[0].X, _queuedPath[0].Y), _tiles, Config.TacticalPlayerMovementDelay))
                    {
                        _queuedPath.RemoveAt(0);
                        RebuildInfo();
                    }
                    else
                        _queuedPath = null;
                }
                else
                    _queuedPath = null;
            }
        }
    }

    public Type GetAITypeForMindControledUnit(Unit unit)
    {
        if (unit.GetStatusEffect(StatusEffectType.Hypnotized) != null) return typeof(NonCombatantTacticalAI);
        if (unit.GetStatusEffect(StatusEffectType.Charmed) != null) return typeof(HedonistTacticalAI);
        return typeof(HedonistTacticalAI);
    }

    private void AI(float dt)
    {
        if (AITimer > 0)
        {
            AITimer -= dt;
        }
        else
        {
            RefreshAIIfNecessary();
            if (_currentAI.RunAI() == false)
            {
                EndTurn();
            }

            if (AITimer <= 0) AITimer = Config.TacticalAIMovementDelay;
        }
    }

    private void MouseOver(int x, int y)
    {
        Vec2I mouseLocation = new Vec2I(x, y);
        StatusUI.HitRate.text = "";
        InfoPanel.RefreshTacticalUnitInfo(null);
        bool refreshed = false;

        List<string> clothesFound = new List<string>();
        CheckPath(mouseLocation);

        for (int i = 0; i < _units.Count; i++)
        {
            ActorUnit actor = _units[i];
            if (actor.Position.GetDistance(mouseLocation) < 1 && actor.Targetable)
            {
                refreshed = true;
                if (_remainingLockedPanelTime <= 0) InfoPanel.RefreshTacticalUnitInfo(actor);

                if (!TacticalUtilities.IsUnitControlledByPlayer(actor.Unit) && SelectedUnit != null && SelectedUnit.Targetable)
                {
                    //write chance
                    switch (ActionMode)
                    {
                        case 1:

                            if (actor.Position.GetNumberOfMovesDistance(SelectedUnit.Position) < 2)
                            {
                                int weaponDamage = SelectedUnit.WeaponDamageAgainstTarget(actor, false);
                                string str = Math.Round(actor.GetAttackChance(SelectedUnit, false) * 100, 1) + "%\n-" + weaponDamage;
                                StatusUI.HitRate.text = str;
                                actor.UnitSprite.ShowDamagedHealthBar(actor, weaponDamage);
                            }

                            break;
                        case 2:
                            if (SelectedUnit.BestRanged.Range > 1
                                && actor.Position.GetNumberOfMovesDistance(SelectedUnit.Position) > 1
                                && actor.Position.GetNumberOfMovesDistance(SelectedUnit.Position) <= SelectedUnit.BestRanged.Range)
                            {
                                int weaponDamage = SelectedUnit.WeaponDamageAgainstTarget(actor, true);
                                string str = Math.Round(actor.GetAttackChance(SelectedUnit, true) * 100, 1) + "%\n-" + weaponDamage;
                                StatusUI.HitRate.text = str;
                                actor.UnitSprite.ShowDamagedHealthBar(actor, weaponDamage);
                            }

                            break;
                        case 3:
                            if (actor.Position.GetNumberOfMovesDistance(SelectedUnit.Position) < 2)
                            {
                                if (SelectedUnit.PredatorComponent?.FreeCap() >= actor.Bulk())
                                {
                                    string str = Math.Round(actor.GetDevourChance(SelectedUnit) * 100, 1) + "%";
                                    StatusUI.HitRate.text = str;
                                }
                                else
                                    StatusUI.HitRate.text = "Not enough room";
                            }

                            break;
                        case 4:
                            if (actor.Position.GetNumberOfMovesDistance(SelectedUnit.Position) < 2)
                            {
                                if (SelectedUnit.PredatorComponent?.FreeCap() >= actor.Bulk())
                                {
                                    string str = Math.Round(actor.GetDevourChance(SelectedUnit) * 100, 1) + "%";
                                    StatusUI.HitRate.text = str;
                                }
                                else
                                    StatusUI.HitRate.text = "Not enough room";
                            }
                            else if (_specialType == SpecialAction.PounceMelee)
                            {
                                int weaponDamage = SelectedUnit.WeaponDamageAgainstTarget(actor, false);
                                if (SelectedUnit.Unit.HasTrait(TraitType.HeavyPounce)) weaponDamage = (int)Mathf.Min(weaponDamage + (weaponDamage * SelectedUnit.PredatorComponent?.Fullness ?? 0) / 4, weaponDamage * 2);
                                string str = Math.Round(actor.GetAttackChance(SelectedUnit, false) * 100, 1) + "%\n-" + weaponDamage;
                                StatusUI.HitRate.text = str;
                                actor.UnitSprite.ShowDamagedHealthBar(actor, weaponDamage);
                            }

                            if (_specialType == SpecialAction.TailStrike)
                            {
                                UpdateTailStrikeGrid(mouseLocation);
                            }

                            break;
                    }
                }
            }
            else if (actor.Position.GetDistance(mouseLocation) < 1 && actor.Targetable == false && actor.Visible)
            {
                if (refreshed == false)
                {
                    InfoPanel.ClearPicture();
                    InfoPanel.ClearText();
                    refreshed = true;
                }

                InfoPanel.AddCorpse(actor.Unit.Name);
            }
        }

        foreach (ClothingDiscards discards in _discardedClothing)
        {
            if (discards.Location.GetDistance(mouseLocation) < 1)
            {
                if (clothesFound.Contains(discards.Name)) continue;
                if (refreshed == false)
                {
                    InfoPanel.ClearText();
                    refreshed = true;
                }

                clothesFound.Add(discards.Name);
                InfoPanel.AddClothes(discards.Name);
            }
        }

        foreach (MiscDiscard discard in _miscDiscards)
        {
            if (discard.Location.GetDistance(mouseLocation) < 1)
            {
                if (refreshed == false)
                {
                    InfoPanel.ClearText();
                    refreshed = true;
                }

                InfoPanel.AddLine(discard.Description);
            }
        }

        if (ActionMode == 6)
        {
            if (_currentSpell?.AreaOfEffect > 0) UpdateAreaOfEffectGrid(mouseLocation);
            if (_currentSpell is DamageSpell spell)
            {
                if (spell.AreaOfEffect == 0)
                {
                    for (int i = 0; i < _units.Count; i++)
                    {
                        ActorUnit actor = _units[i];
                        if (actor.Position.GetDistance(mouseLocation) < 1 && actor.Targetable)
                        {
                            if (actor != null)
                            {
                                int spellDamage = spell.Damage(SelectedUnit, actor);
                                if (TacticalUtilities.SneakAttackCheck(SelectedUnit.Unit, actor.Unit)) // sneakAttack
                                {
                                    spellDamage *= 3;
                                }

                                actor.UnitSprite.ShowDamagedHealthBar(actor, spellDamage);
                                string str = Math.Round(actor.GetMagicChance(SelectedUnit, _currentSpell) * 100, 1) + "%\n-" + spellDamage;
                                StatusUI.HitRate.text = str;
                            }
                        }
                    }
                }
                else if (mouseLocation != null)
                {
                    foreach (var splashTarget in TacticalUtilities.UnitsWithinTiles(mouseLocation, spell.AreaOfEffect))
                    {
                        int spellDamage = spell.Damage(SelectedUnit, splashTarget);
                        if (TacticalUtilities.SneakAttackCheck(SelectedUnit.Unit, splashTarget.Unit)) // sneakAttack
                        {
                            spellDamage *= 3;
                        }

                        splashTarget.UnitSprite.ShowDamagedHealthBar(splashTarget, spellDamage);
                    }
                }
            }
        }
    }

    private void CheckPath(Vec2I mouseLocation)
    {
        if (ActionMode != 5)
        {
            _arrowManager.ClearNodes();
            return;
        }

        if (SelectedUnit == null) return;

        if (_currentPathDestination != null && mouseLocation.Matches(_currentPathDestination)) return;
        if (TacticalUtilities.OpenTile(mouseLocation, SelectedUnit) == false) return;
        _currentPathDestination = mouseLocation;
        var path = TacticalPathfinder.GetPath(SelectedUnit.Position, mouseLocation, 0, SelectedUnit);
        _arrowManager.ClearNodes();
        if (path == null || path.Count == 0) return;
        int remainingMp = SelectedUnit.Movement;

        for (int i = 0; i < path.Count; i++)
        {
            remainingMp -= TacticalTileInfo.TileCost(new Vec2(path[i].X, path[i].Y));
            if (remainingMp > 1)
                _arrowManager.PlaceNode(Color.green, new Vec2I(path[i].X, path[i].Y));
            else if (remainingMp == 1)
                _arrowManager.PlaceNode(Color.yellow, new Vec2I(path[i].X, path[i].Y));
            else if (remainingMp == 0)
                _arrowManager.PlaceNode(Color.red, new Vec2I(path[i].X, path[i].Y));
            else
                _arrowManager.PlaceNode(Color.gray, new Vec2I(path[i].X, path[i].Y));
        }

        UpdateMovementGrid();
        UpdateAttackGrid(mouseLocation);
    }

    internal void RebuildInfo()
    {
        if (SelectedUnit != null && SelectedUnit.Targetable)
        {
            var voreTypes = State.RaceSettings.GetVoreTypes(SelectedUnit.Unit.Race);
            StatusUI.UnitName.text = SelectedUnit.Unit.Name;
            StatusUI.Hp.text = "HP:" + SelectedUnit.Unit.Health + "/" + SelectedUnit.Unit.MaxHealth;
            StatusUI.Mp.text = "AP:" + SelectedUnit.Movement;

            StatusUI.ZeroAPButton.interactable = true;
            StatusUI.MeleeButton.interactable = SelectedUnit.BestMelee != null;
            StatusUI.RangedButton.interactable = SelectedUnit.BestRanged != null;
            StatusUI.VoreButton.interactable = SelectedUnit.Unit.Predator;
            StatusUI.UndoMovement.interactable = _startingLocation != null && (_startingLocation != SelectedUnit.Position || _startingMp > SelectedUnit.Movement);
            if (voreTypes.Count == 1)
            {
                var type = voreTypes[0];
                switch (type)
                {
                    case VoreType.Unbirth:
                        StatusUI.VoreButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Unbirth";
                        break;
                    case VoreType.CockVore:
                        StatusUI.VoreButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Cock Vore";
                        break;
                    case VoreType.BreastVore:
                        StatusUI.VoreButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Breast Vore";
                        break;
                    case VoreType.TailVore:
                        StatusUI.VoreButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Tail Vore";
                        break;
                    default:
                        StatusUI.VoreButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Vore";
                        break;
                }
            }
            else
            {
                StatusUI.VoreButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Vore";
            }


            CommandsUI.gameObject.SetActive(true);
            CommandsUI.SetUpButtons(SelectedUnit);
        }
        else if (SelectedUnit == null)
        {
            StatusUI.UnitName.text = "No unit selected";
            StatusUI.Hp.text = "";
            StatusUI.Mp.text = "";
            StatusUI.VoreButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Vore";
            StatusUI.UndoMovement.interactable = false;
            StatusUI.ZeroAPButton.interactable = false;
            CommandsUI.gameObject.SetActive(false);
        }
    }

    internal void UpdateHealthBars()
    {
        foreach (ActorUnit actor in _units)
        {
            actor.UnitSprite?.UpdateHealthBar(actor);
        }
    }


    private void NextActor(NextUnitType type)
    {
        int startingIndex = _units.IndexOf(SelectedUnit);
        int currentIndex = startingIndex + 1;
        ActionMode = 0;
        for (int i = 0; i < _units.Count; i++)
        {
            if (currentIndex >= _units.Count) currentIndex -= _units.Count;
            if (Equals(_units[currentIndex].Unit.Side, _activeSide) && TacticalUtilities.IsUnitControlledByPlayer(_units[currentIndex].Unit) && _units[currentIndex].Targetable && _units[currentIndex].Movement > 0)
            {
                if (type == NextUnitType.Any || (type == NextUnitType.Melee && _units[currentIndex].BestMelee.Damage > 2) || (type == NextUnitType.Ranged && _units[currentIndex].BestRanged != null))
                {
                    SelectedUnit = _units[currentIndex];
                    RebuildInfo();
                    State.GameManager.CenterCameraOnTile(_units[currentIndex].Position.X, _units[currentIndex].Position.Y);
                    break;
                }
            }

            currentIndex++;
        }
    }

    private void MoveActor()
    {
        if (Input.GetButtonDown("Melee")) ButtonCallback(0);
        if (Input.GetButtonDown("Ranged")) ButtonCallback(1);
        if (Input.GetButtonDown("Vore")) ButtonCallback(2);
        if (Input.GetButtonDown("Movement Mode")) ButtonCallback(7);

        if (SelectedUnit != null && SelectedUnit.Targetable && SelectedUnit.Movement > 0)
        {
            if (Input.GetButtonDown("Move Southwest"))
                ProcessMovement(SelectedUnit, 5);
            else if (Input.GetButtonDown("Move South"))
                ProcessMovement(SelectedUnit, 4);
            else if (Input.GetButtonDown("Move Southeast"))
                ProcessMovement(SelectedUnit, 3);
            else if (Input.GetButtonDown("Move East"))
                ProcessMovement(SelectedUnit, 2);
            else if (Input.GetButtonDown("Move Northeast"))
                ProcessMovement(SelectedUnit, 1);
            else if (Input.GetButtonDown("Move North"))
                ProcessMovement(SelectedUnit, 0);
            else if (Input.GetButtonDown("Move Northwest"))
                ProcessMovement(SelectedUnit, 7);
            else if (Input.GetButtonDown("Move West")) ProcessMovement(SelectedUnit, 6);
        }
        //if (Input.GetButtonDown("Special"))
        //    ButtonCallback(5);
    }

    private void ProcessMovement(ActorUnit unit, int moveCode)
    {
        if (unit.Move(moveCode, _tiles))
        {
            RemoveHitPercentages();
            RebuildInfo();
            if (unit.Movement == 0) ActionMode = 0;
            if (ActionMode == 1)
                ShowMeleeHitPercentages(unit);
            else if (ActionMode == 2)
                ShowRangedHitPercentages(unit);
            else if (ActionMode == 3)
                ShowVoreHitPercentages(unit);
            else if (ActionMode == 4) ShowSpecialHitPercentages(unit);
        }
    }

    private void ProcessLeftClick(int x, int y)
    {
        RightClickMenu.CloseAll();
        if ((!IsPlayerTurn && !PseudoTurn) || _queuedPath != null) return;


        Vec2I clickLocation = new Vec2I(x, y);

        for (int i = 0; i < _units.Count; i++)
        {
            ActorUnit unit = _units[i];

            if (unit.Position.GetDistance(clickLocation) < 1 && unit.Targetable == true)
            {
                if (ActionMode == 0)
                {
                    if (TacticalUtilities.IsUnitControlledByPlayer(unit.Unit) && Equals(unit.Unit.Side, _activeSide))
                    {
                        if (SelectedUnit != _units[i])
                        {
                            SelectedUnit = _units[i];
                            RebuildInfo();
                        }

                        break;
                    }
                }

                if (SelectedUnit == null) continue;
                if (ActionMode == 1)
                {
                    if (!TacticalUtilities.IsUnitControlledByPlayer(unit.Unit) || ((Config.AllowInfighting || (!AIDefender && !AIAttacker)) && unit != SelectedUnit))
                    {
                        MeleeAttack(SelectedUnit, unit);
                        return;
                    }
                }

                if (ActionMode == 2)
                {
                    if (!TacticalUtilities.IsUnitControlledByPlayer(unit.Unit) || ((Config.AllowInfighting || (!AIDefender && !AIAttacker)) && unit != SelectedUnit))
                    {
                        RangedAttack(SelectedUnit, unit);
                        return;
                    }
                }

                if (ActionMode == 3)
                {
                    if (TacticalUtilities.AppropriateVoreTarget(SelectedUnit, unit))
                    {
                        VoreAttack(SelectedUnit, unit);
                        return;
                    }
                }

                if (ActionMode == 4)
                {
                    if (TakeSpecialAction(_specialType, SelectedUnit, unit))
                    {
                        RemoveHitPercentages();
                        ActionDone();
                        return;
                    }
                }

                if (ActionMode == 6)
                {
                    int distance = SelectedUnit.Position.GetNumberOfMovesDistance(unit.Position);
                    if (TacticalUtilities.MeetsQualifier(_currentSpell.AcceptibleTargets, SelectedUnit, unit) && _currentSpell.Range.Max >= distance && _currentSpell.Range.Min <= distance)
                    {
                        _currentSpell.TryCast(SelectedUnit, unit);
                        RemoveHitPercentages();
                        ActionDone();
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < _units.Count; i++)
        {
            ActorUnit unit = _units[i];
            if (Config.EdibleCorpses && ActionMode == 3 && unit.Position.GetDistance(clickLocation) < 1 && unit.Targetable == false && unit.Visible && unit.Bulk() <= SelectedUnit.PredatorComponent.FreeCap())
            {
                var voreTypes = State.RaceSettings.GetVoreTypes(SelectedUnit.Unit.Race);
                if (voreTypes.Contains(VoreType.Oral))
                    SelectedUnit.PredatorComponent.Devour(unit);
                else
                    SelectedUnit.PredatorComponent.UsePreferredVore(unit);
                ActionDone();
            }
            else if (Config.EdibleCorpses && ActionMode == 4 && unit.Position.GetDistance(clickLocation) < 1 && unit.Targetable == false && unit.Visible && unit.Bulk() <= SelectedUnit.PredatorComponent.FreeCap())
            {
                if (TakeSpecialAction(_specialType, SelectedUnit, unit))
                {
                    RemoveHitPercentages();
                    ActionDone();
                    return;
                }
            }
        }

        if (ActionMode == 4 && _specialType == SpecialAction.Regurgitate)
        {
            if (TakeSpecialActionLocation(_specialType, SelectedUnit, clickLocation))
            {
                RemoveHitPercentages();
                ActionDone();
                return;
            }
        }

        if (ActionMode == 5)
        {
            OrderSelectedUnitToMoveTo(x, y);
        }

        if (ActionMode == 6 && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            int distance = SelectedUnit.Position.GetNumberOfMovesDistance(clickLocation);
            if (_currentSpell.Range.Max >= distance && _currentSpell.Range.Min <= distance && _currentSpell.AcceptibleTargets.Contains(AbilityTargets.Tile))
            {
                _currentSpell.TryCast(SelectedUnit, clickLocation);
                RemoveHitPercentages();
                ActionDone();
                return;
            }
        }
    }

    private bool UnitControllableBySide(ActorUnit unit, Side side)
    {
        bool correctSide = Equals(unit.Unit.Side, side);
        bool controlOverridden = !Equals(TacticalUtilities.GetMindControlSide(unit.Unit), Side.TrueNoneSide) || (!Equals(unit.Unit.FixedSide, side) && !unit.Unit.IsInfiltratingSide(side));
        return correctSide && !controlOverridden;
    }

    internal void VoreAttack(ActorUnit actor, ActorUnit unit)
    {
        var voreTypes = State.RaceSettings.GetVoreTypes(SelectedUnit.Unit.Race);
        if (voreTypes.Contains(VoreType.Oral))
            actor.PredatorComponent.Devour(unit);
        else
            actor.PredatorComponent.UsePreferredVore(unit);
        ActionDone();
    }

    internal void ActionDone()
    {
        ActionMode = 0;
        PlaceUndoMarker();
        RebuildInfo();
    }

    internal void RangedAttack(ActorUnit actor, ActorUnit unit)
    {
        actor.Attack(unit, true);
        ActionDone();
    }

    internal void MeleeAttack(ActorUnit actor, ActorUnit unit)
    {
        actor.Attack(unit, false);
        ActionDone();
    }

    private void ProcessRightClick(int x, int y)
    {
        if (_queuedPath != null) return;

        Vec2I clickLocation = new Vec2I(x, y);

        for (int i = 0; i < _units.Count; i++)
        {
            ActorUnit unit = _units[i];

            if (unit.Position.GetDistance(clickLocation) < 1 && unit.Targetable == true)
            {
                if (TacticalUtilities.TileContainsMoreThanOneUnit(SelectedUnit.Position.X, SelectedUnit.Position.Y))
                {
                    UndoMovement();
                    return;
                }

                RightClickMenu.Open(SelectedUnit, unit);
                return;
            }
        }

        if (Config.RightClickMenu || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (TacticalUtilities.TileContainsMoreThanOneUnit(SelectedUnit.Position.X, SelectedUnit.Position.Y))
            {
                UndoMovement();
                return;
            }

            RightClickMenu.OpenWithNoTarget(SelectedUnit, new Vec2I(x, y));
        }
        else
        {
            OrderSelectedUnitToMoveTo(x, y);
        }
    }

    internal void OrderSelectedUnitToMoveTo(int x, int y)
    {
        _arrowManager.ClearNodes();
        Vec2I clickLoc = new Vec2I(x, y);
        _queuedPath = TacticalPathfinder.GetPath(SelectedUnit.Position, clickLoc, 0, SelectedUnit);
        ActionMode = 0;
    }

    //public void AddUnit(Actor_Unit newUnit)
    //{
    //    units.Add(newUnit);
    //}

    internal void SwitchAlignment(ActorUnit actor)
    {
        Side startingSide = actor.Unit.Side;
        if (Equals(actor.Unit.Side, _defenderSide))
            AttackerConvert(actor);
        else
            DefenderConvert(actor);
        actor.DefectedThisTurn = !Equals(startingSide, actor.Unit.Side);
        actor.SidesAttackedThisBattle = new List<Side>();
    }

    internal bool IsDefender(ActorUnit actor)
    {
        return Equals(actor.Unit.Side, _defenderSide);
    }


    //public void CheckAlignment(Actor_Unit childUnit, Actor_Unit parentUnit)
    //{
    //    if (parentUnit.Unit.Side == defenderSide)
    //        DefenderConvert(childUnit);
    //    else
    //        AttackerConvert(childUnit);
    //}

    //void CheckAlignment(Actor_Unit childUnit, Actor_Unit parentUnit)
    //{
    //    if (!units.Contains(childUnit))
    //    {
    //        units.Add(childUnit);
    //    }
    //    if (childUnit.Unit.Side != parentUnit.Unit.Side && !childUnit.Unit.ImmuneToDefections)
    //    {
    //        childUnit.Unit.Side = parentUnit.Unit.Side;
    //    }
    //    if (armies[0].Units.Contains(parentUnit.Unit))
    //    {
    //        AttackerConvert(childUnit);
    //    }
    //    else
    //    {
    //        DefenderConvert(childUnit);
    //    }
    //    childUnit.UnitSprite.BlueColored = parentUnit.UnitSprite.BlueColored;
    //}

    internal void DefenderConvert(ActorUnit actor)
    {
        if (_armies[1]?.Units.Contains(actor.Unit) ?? (false || ExtraDefenders.Contains(actor))) return;
        _armies[0].Units.Remove(actor.Unit);
        ExtraAttackers.Remove(actor);
        actor.Unit.Side = _defenderSide;
        UpdateActorColor(actor);
        if (actor.Unit.Type != UnitType.Summon)
        {
            if (_armies[1] != null && _armies[1].Units.Count < _armies[1].MaxSize)
            {
                _armies[1].Units.Add(actor.Unit);
            }
            else
            {
                ExtraDefenders.Add(actor);
            }
        }
    }

    internal void AttackerConvert(ActorUnit actor)
    {
        if (_armies[0].Units.Contains(actor.Unit) || ExtraAttackers.Contains(actor)) return;
        _armies[1]?.Units.Remove(actor.Unit);
        ExtraDefenders.Remove(actor);
        _village?.GetRecruitables().Remove(actor.Unit);
        actor.Unit.Side = _armies[0].Side;
        UpdateActorColor(actor);
        if (actor.Unit.Type != UnitType.Summon)
        {
            if (_armies[0] != null && _armies[0].Units.Count < _armies[0].MaxSize)
            {
                _armies[0].Units.Add(actor.Unit);
            }
            else
            {
                ExtraAttackers.Add(actor);
            }
        }

        _garrison.Remove(actor);
    }


    private void EndTurn()
    {
        if (_waitingForDialog) return;
        if (PseudoTurn)
        {
            SkipPseudo = true;
            PseudoTurn = false;
            StatusUI.EndTurn.interactable = false;
            return;
        }

        SkipPseudo = false;
        if (Config.AutoUseAI && IsPlayerInControl && _repeatingTurn == false)
        {
            _repeatingTurn = true;
            ButtonCallback(3);
            return;
        }

        _repeatingTurn = false;
        IgnorePseudo = false;
        RightClickMenu.CloseAll();
        if (State.TutorialMode && State.GameManager.TutorialScript.Step < 6) return;
        ActionMode = 0;
        SelectedUnit = null;
        SkipUI.gameObject.SetActive(false);
        SelectionBox.gameObject.SetActive(false);
        IsPlayerTurn = true;
        if (_attackersTurn)
        {
            _attackersTurn = false;
            _activeSide = _defenderSide;
            _currentAI = _defenderAI;
            NewTurn();
            if (AIDefender) IsPlayerTurn = false;
        }
        else
        {
            _attackersTurn = true;
            CurrentTurn++;
            _activeSide = _armies[0].Side;
            _currentAI = _attackerAI;
            NewTurn();
            if (AIAttacker) IsPlayerTurn = false;
        }

        ProcessTileEffects();

        StatusUI.SkipToEndButton.interactable = true;
        UpdateEndTurnButtonText();
        StatusUI.EndTurn.interactable = IsPlayerTurn;
        EnemyTurnText.SetActive(!IsPlayerTurn);
        if (IsPlayerTurn == false) AITimer = .2f;
        if (_attackersTurn)
        {
            StatusUI.AttackerText.fontStyle = FontStyle.Bold;
            StatusUI.DefenderText.fontStyle = FontStyle.Normal;
        }
        else
        {
            StatusUI.AttackerText.fontStyle = FontStyle.Normal;
            StatusUI.DefenderText.fontStyle = FontStyle.Bold;
        }

        if (_reviewingBattle) StatusUI.EndTurn.interactable = true;
    }

    internal void UpdateEndTurnButtonText()
    {
        if (Config.DisplayEndOfTurnText)
        {
            string str = "";
            if (Config.AutoAdvance == Config.AutoAdvanceType.DoNothing)
                str = "Manual";
            else if (Config.AutoAdvance == Config.AutoAdvanceType.AdvanceTurns)
                str = "Quick";
            else if (Config.AutoAdvance == Config.AutoAdvanceType.SkipToEnd) str = "Skip";

            StatusUI.EndTurn.GetComponentInChildren<UnityEngine.UI.Text>().text = $"End Turn ({str})";
        }
        else
            StatusUI.EndTurn.GetComponentInChildren<UnityEngine.UI.Text>().text = "End Turn";
    }

    public bool CanDefect(ActorUnit unit)
    {
        if (unit.Possessed > 0 || unit.DefectedThisTurn) return false;
        return !Equals(TacticalUtilities.GetPreferredSide(unit.Unit, _activeSide, _attackersTurn ? _defenderSide : _attackerSide), _activeSide)
               || (_units.Any(u => !Equals(u.Unit.Side, unit.Unit.Side) && u.Targetable && u.Visible && !u.Fled) && !_units.Any(u => TacticalUtilities.TreatAsHostile(unit, u) && u.Targetable && u.Visible && !u.Fled));
    }

    private void NewTurn()
    {
        AllSurrenderedCheck();
        Log.RegisterNewTurn(_attackersTurn ? AttackerName : DefenderName, CurrentTurn);
        if (State.World.IsNight)
        {
            UpdateFog();
        }

        for (int i = 0; i < _units.Count; i++)
        {
            if (_units[i].Unit.IsDead == false && !Equals(_units[i].Unit.Side, _activeSide))
            {
                //You seem to be causing issues, but I may need you for reference later.
                /*if (Config.KuroTenkoEnabled)
                {
                    List<Actor_Unit> released;
                    released = units[i].BirthCheck();
                    foreach (Actor_Unit child in released)
                    {
                        CheckAlignment(child, units[i]);
                    }
                }*/
                _units[i].ReceivedRub = false; // Hedonists now get just as much benefit out of mind-control effects
                _units[i].DigestCheck(); //Done first so that freed units are checked properly below
            }

            if (_units[i].SelfPrey != null) _units[i].SelfPrey.TurnsSinceLastDamage++;
        }

        for (int i = 0; i < _units.Count; i++)
        {
            if (_units[i].Unit.IsDead == false && Equals(_units[i].Unit.Side, _activeSide))
            {
                _units[i].AllowedToDefect = CanDefect(_units[i]);
                _units[i].DefectedThisTurn = false;
                _units[i].NewTurn();
            }

            if (_units[i].Unit.IsDead && Equals(_units[i].Unit.Side, _activeSide))
            {
                if (_units[i].Targetable) _units[i].Damage(0);
            }
        }

        for (int i = 0; i < _units.Count; i++)
        {
            _units[i].PredatorComponent?.UpdateFullness(); //Catches any sizes changed by expiration of shrink / expand
        }

        for (int i = 0; i < _retreatedDigestors.Count; i++)
        {
            if (_retreatedDigestors[i].Unit.IsDead == false)
            {
                if (Equals(_retreatedDigestors[i].Unit.Side, _activeSide))
                {
                    _retreatedDigestors[i].DigestCheck();
                }

                if (_retreatedDigestors[i].Unit.HasTrait(TraitType.Endosoma))
                {
                    foreach (var prey in _retreatedDigestors[i].PredatorComponent.GetAllPrey())
                    {
                        if (!TacticalUtilities.TreatAsHostile(_retreatedDigestors[i], prey.Actor) && prey.Actor.Fled == false)
                        {
                            RetreatUnit(prey.Actor, Equals(prey.Unit.Side, _defenderSide));
                        }
                    }
                }
            }
        }

        if (TurboMode == false)
        {
            RebuildInfo();
            _autoAdvancing = Config.AutoAdvance > 0 && IsOnlyOneSideVisible();
            VictoryCheck();
        }
    }

    private void AllSurrenderedCheck()
    {
        bool allSurrendered = true;
        foreach (ActorUnit actor in _units)
        {
            if (actor.Unit.IsDead == false && actor.Surrendered == false && actor.Visible && actor.Fled == false) allSurrendered = false;
        }

        if (allSurrendered)
        {
            bool flipped = false;
            foreach (ActorUnit actor in _units)
            {
                if (actor.Surrendered) flipped = true;
                actor.Surrendered = false;
            }

            if (flipped)
            {
                if (TurboMode == false && State.GameManager.CurrentScene == State.GameManager.TacticalMode)
                    State.GameManager.CreateMessageBox("After several minutes of staring at each other doing nothing, all of the surrendered units decided to fight again");
                else
                    Debug.Log("The AI ended up needing an all surrendered reset... this probably shouldn't have happened");
            }
            else
                Debug.Log("All units had apparently surrendered without any units surrendered.  I'm guessing it has to do with fleeing units");
        }
    }

    internal bool IsOnlyOneSideVisible()
    {
        List<ActorUnit> visibleAttackers = new List<ActorUnit>();
        List<ActorUnit> visibleDefenders = new List<ActorUnit>();

        for (int i = 0; i < _units.Count; i++)
        {
            if (_units[i] != null && _units[i].Fled == false)
            {
                ActorUnit actor = _units[i];
                if (actor.Targetable)
                {
                    if (Equals(actor.Unit.Side, _armies[0].Side))
                    {
                        visibleAttackers.Add(actor);
                    }
                    else
                    {
                        visibleDefenders.Add(actor);
                    }
                }
            }
        }

        bool oneSideLeft = false;
        if (visibleAttackers.Count() == 0)
        {
            oneSideLeft = !visibleDefenders.Any(vd => !vd.Unit.HiddenFixedSide && Equals(TacticalUtilities.GetPreferredSide(vd.Unit, _defenderSide, _attackerSide), _attackerSide)); // They are probably still fighting in this case
        }

        if (visibleDefenders.Count() == 0)
        {
            oneSideLeft = !visibleAttackers.Any(vd => !vd.Unit.HiddenFixedSide && Equals(TacticalUtilities.GetPreferredSide(vd.Unit, _attackerSide, _defenderSide), _defenderSide)); // They are probably still fighting in this case
        }

        _autoAdvanceTimer = AutoAdvanceRate;
        AutoAdvanceText.SetActive(oneSideLeft && Config.AutoAdvance > Config.AutoAdvanceType.DoNothing);
        return oneSideLeft;
    }


    private bool VictoryCheck()
    {
        if (_waitingForDialog) return false;
        int remainingAttackers = 0;
        int remainingDefenders = 0;

        CalculateRemaining(ref remainingAttackers, ref remainingDefenders);
        if (remainingAttackers == 0 || remainingDefenders == 0)
        {
            foreach (ActorUnit actor in _units)
            {
                if (actor.Targetable && actor.Visible && !actor.Fled && !actor.Surrendered && actor.TurnsSinceLastDamage < 2) return false;
                if (actor.Targetable && actor.Visible && !actor.Fled && !actor.Surrendered && !actor.Unit.HiddenFixedSide && _units.Any(u => u.Targetable && !u.Fled && u.Visible && TacticalUtilities.TreatAsHostile(actor, u))) return false;
                if (actor.Unit.Predator == false) continue;
                foreach (var prey in actor.PredatorComponent.GetDirectPrey().Where(s => s.Unit.HasTrait(TraitType.TheGreatEscape)).ToList())
                {
                    actor.PredatorComponent.FreeGreatEscapePrey(prey);
                    RetreatUnit(prey.Actor, Equals(prey.Unit.Side, _defenderSide));
                }

                if (actor.Fled == false) continue;
                for (int i = 0; i < 1000; i++)
                {
                    if (actor.PredatorComponent.DigestingUnitCount < 1) break;
                    actor.DigestCheck();
                }
            }

            remainingAttackers = 0;
            remainingDefenders = 0;
            CalculateRemaining(ref remainingAttackers, ref remainingDefenders);
            if (_units.Where(s => s.Unit.IsDead == false && s.PredatorComponent?.DigestingUnitCount > 0).FirstOrDefault() != null)
            {
                return false;
            }

            if (remainingAttackers > 0 && remainingDefenders > 0)
            {
                Debug.Log("It's a battle again, an escaped unit came back to the battlefield - going back to minimize undefined behavior");
                return false;
            }

            if (remainingAttackers > 0 && AIAttacker == false)
            {
                if (FledReturn == ChoiceOption.Default)
                {
                    int fledAttackers = _units.Where(s => Equals(s.Unit.Side, _armies[0].Side) && s.Fled).Count();
                    if (fledAttackers > 0)
                    {
                        var box = State.GameManager.CreateDialogBox();
                        _waitingForDialog = true;
                        box.SetData(() =>
                        {
                            FledReturn = ChoiceOption.Yes;
                            _waitingForDialog = false;
                            VictoryCheck();
                        }, "Return to army", "Flee to a town", "You've won the battle, should your units that retreated rejoin your forces, or keep retreating to a town?", () =>
                        {
                            FledReturn = ChoiceOption.No;
                            _waitingForDialog = false;
                            VictoryCheck();
                        });
                        return false;
                    }
                }
            }

            if (remainingDefenders > 0 && AIDefender == false)
            {
                if (FledReturn == ChoiceOption.Default)
                {
                    int fledDefenders = _units.Where(s => Equals(s.Unit.Side, _defenderSide) && s.Fled).Count();
                    if (fledDefenders > 0)
                    {
                        var box = State.GameManager.CreateDialogBox();
                        _waitingForDialog = true;
                        box.SetData(() =>
                        {
                            FledReturn = ChoiceOption.Yes;
                            _waitingForDialog = false;
                            VictoryCheck();
                        }, "Return to army", "Flee to a town", "You've won the battle, should your units that retreated rejoin your forces, or keep retreating to a town?", () =>
                        {
                            FledReturn = ChoiceOption.No;
                            _waitingForDialog = false;
                            VictoryCheck();
                        });
                        return false;
                    }
                }
            }

            if (Config.StopAtEndOfBattle && _reviewingBattle == false && TurboMode == false)
            {
                _reviewingBattle = true;
                _autoAdvancing = false;
                BattleReviewText.SetActive(true);
                foreach (ActorUnit actor in _units)
                {
                    actor.Movement = 0;
                    actor.DigestCheck();
                    actor.Update(2);
                    actor.UnitSprite.UpdateSprites(actor, Equals(actor.Unit.Side, _activeSide));
                }

                StatusUI.EndTurn.interactable = true;
                return false;
            }

            ProcessReplaceable(remainingAttackers);

            foreach (ActorUnit actor in _units)
            {
                actor.Unit.GiveExp(4);
                if (actor.Unit.TraitBoosts.HealthRegen > 0 && actor.Unit.IsDead == false) actor.Unit.HealPercentage(1);
                actor.Unit.StatusEffects.Clear();
            }

            BattleReviewText.SetActive(false);
            foreach (ActorUnit actor in _units.ToList())
            {
                actor.Unit.SetSizeToDefault();
                actor.Unit.EnemiesKilledThisBattle = 0;
                if (actor.Unit.IsDead && actor.Unit.Type != UnitType.Summon &&
                    (actor.Unit.HasTrait(TraitType.Eternal) || (actor.Unit.HasTrait(TraitType.LuckySurvival) && State.Rand.Next(5) != 0) ||
                     (actor.Unit.HasTrait(TraitType.Reformer) && actor.KilledByDigestion) ||
                     (actor.Unit.HasTrait(TraitType.Revenant) && actor.KilledByDigestion == false)
                    ))
                {
                    actor.Surrendered = false;
                    actor.Unit.Health = actor.Unit.MaxHealth;
                    if (Equals(actor.Unit.Side, _defenderSide))
                    {
                        if (_garrison.Contains(actor) && remainingDefenders > 0)
                        {
                            actor.Unit.Health = actor.Unit.MaxHealth;
                        }
                        else
                        {
                            _retreatedDefenders.Add(actor.Unit);
                            _armies[1]?.Units.Remove(actor.Unit);
                            _village?.GetRecruitables().Remove(actor.Unit);
                        }
                    }
                    else
                    {
                        _retreatedAttackers.Add(actor.Unit);
                        _armies[0].Units.Remove(actor.Unit);
                    }

                    actor.PredatorComponent?.PurgePrey();
                    _units.Remove(actor);
                }
                else if ((actor.Unit.HasTrait(TraitType.Transmigration) || actor.Unit.HasTrait(TraitType.InfiniteTransmigration)) && actor.KilledByDigestion && actor.Unit.IsDead
                         && actor.Unit.Type != UnitType.Summon && actor.Unit.Type != UnitType.Leader && actor.Unit.Type != UnitType.SpecialMercenary)
                {
                    if (State.World.MainEmpires != null)
                    {
                        Race race = actor.Unit.KilledBy.Race;
                        if (State.World.Reincarnators == null) State.World.Reincarnators = new List<Reincarnator>();
                        if (!State.World.Reincarnators.Any(rc => rc.PastLife == actor.Unit))
                        {
                            actor.Unit.RemoveTrait(TraitType.Transmigration);
                            State.World.Reincarnators.Add(new Reincarnator(actor.Unit, race, true));
                            State.World.GetEmpireOfSide(actor.Unit.Side)?.Reports.Add(new StrategicReport($"{actor.Unit.Name} will reincarnate as a {InfoPanel.RaceSingular(actor.Unit.KilledBy)}.", new Vec2(0, 0)));
                        }
                    }
                }
                else if ((actor.Unit.HasTrait(TraitType.Reincarnation) || actor.Unit.HasTrait(TraitType.InfiniteReincarnation)) && actor.Unit.IsDead
                                                                                                                                && actor.Unit.Type != UnitType.Summon && actor.Unit.Type != UnitType.Leader && actor.Unit.Type != UnitType.SpecialMercenary)
                {
                    if (State.World.MainEmpires != null)
                    {
                        List<Race> activeRaces = StrategicUtilities.GetAllUnits(false).ConvertAll(u => u.Race).Distinct()
                            .ToList();
                        if (activeRaces.Any())
                        {
                            Race race = activeRaces[State.Rand.Next(activeRaces.Count)];
                            if (State.World.Reincarnators == null) State.World.Reincarnators = new List<Reincarnator>();
                            if (!State.World.Reincarnators.Any(rc => rc.PastLife == actor.Unit))
                            {
                                actor.Unit.RemoveTrait(TraitType.Reincarnation);
                                State.World.Reincarnators.Add(new Reincarnator(actor.Unit, race));
                                State.World.GetEmpireOfSide(actor.Unit.Side)?.Reports.Add(new StrategicReport($"{actor.Unit.Name} will reincarnate as a random race.", new Vec2(0, 0)));
                            }
                        }
                    }
                }
                else if (actor.Fled)
                    _units.Remove(actor);
                else if (actor.Unit.IsDead && actor.Unit.SavedCopy != null && (!State.World.Reincarnators?.Any(rc => rc.PastLife == actor.Unit) ?? true))
                {
                    var emp = State.World.GetEmpireOfSide(actor.Unit.Side);
                    var vill = actor.Unit.SavedVillage;
                    if (Equals(actor.Unit.Side, _defenderSide))
                    {
                        _armies[1]?.Units.Remove(actor.Unit);
                        _village?.GetRecruitables().Remove(actor.Unit);
                    }
                    else
                    {
                        _armies[0].Units.Remove(actor.Unit);
                    }

                    _armies[1]?.Units.Remove(actor.Unit);
                    _units.Remove(actor);
                    if (vill != null && vill.Empire.IsAlly(emp))
                    {
                        actor.Unit.SavedVillage.AddPopulation(1);
                        actor.Unit.SavedVillage.GetRecruitables().Add(actor.Unit.SavedCopy);
                        actor.Unit.SavedCopy.SavedCopy = null;
                        actor.Unit.SavedCopy.SavedVillage = null;
                        emp.Reports.Add(new StrategicReport($"{actor.Unit.Name}'s soul has returned the imprinted copy in the village of {vill.Name}", vill.Position));
                        if (State.GameManager.StrategyMode.IsPlayerTurn)
                        {
                            State.GameManager.StrategyMode.NewReports = true;
                        }
                    }
                }
            }

            EatSurrenderedAllies();
            SelectedUnit = null;
            if (remainingAttackers > 0)
            {
                State.World.Stats?.BattleResolution(_armies[0].Side, _defenderSide);
                if ((FledReturn == ChoiceOption.Yes || FledReturn == ChoiceOption.Default) && _retreatedAttackers != null) //Default is to catch eternal units when no units fled
                {
                    if (_armies[0] != null) _armies[0].Units.AddRange(_retreatedAttackers);
                    _retreatedAttackers.Clear();
                }

                StrategicUtilities.TryClaim(_armies[0].Position, _armies[0].EmpireOutside);
            }
            else
            {
                State.World.Stats?.BattleResolution(_defenderSide, _armies[0].Side);
                State.World.Stats?.LostArmy(_armies[0].Side);
                if (FledReturn == ChoiceOption.Yes || (FledReturn == ChoiceOption.Default && _retreatedDefenders != null)) //Default is to catch eternal units when no units fled)
                {
                    if (_armies[1] != null)
                        _armies[1].Units.AddRange(_retreatedDefenders);
                    else if (_village != null) _village.GetRecruitables().AddRange(_retreatedDefenders);
                    _retreatedDefenders.Clear();
                }
            }

            if (remainingAttackers > 0 && ExtraAttackers != null && ExtraAttackers.Any())
            {
                AssignLeftoverTroops(_armies[0], ExtraAttackers);
            }
            else if (remainingDefenders > 0 && ExtraDefenders != null && ExtraDefenders.Any())
            {
                AssignLeftoverTroops(_armies[1], ExtraDefenders);
            }


            ProcessFledUnits();
            remainingAttackers = 0;
            remainingDefenders = 0;
            CalculateRemaining(ref remainingAttackers, ref remainingDefenders);
            if (remainingAttackers > 0 && remainingDefenders > 0)
            {
                //This is just a back-up incase there's some other method of causing the issue I fixed
                VictoryCheck();
                return false;
            }

            ProcessConsumedCorpses();
            if (_village != null)
            {
                TacticalUtilities.CleanVillage(remainingAttackers);
            }

            bool skipStats = false;
            if (AIAttacker && AIDefender && Config.SkipAIOnlyStats) skipStats = true;
            string attackerReceives = "";
            string defenderReceives = "";
            if (remainingAttackers > 0)
            {
                if (_armies[1]?.BountyGoods != null)
                {
                    attackerReceives = _armies[1].BountyGoods.ApplyToArmyOrVillage(_armies[0]);
                }

                Wins.x++;
            }
            else
            {
                if (_armies[0]?.BountyGoods != null)
                {
                    defenderReceives = _armies[0].BountyGoods.ApplyToArmyOrVillage(_armies[1], _village);
                }

                Wins.y++;
            }

            LootItems(remainingDefenders, ref attackerReceives, ref defenderReceives);

            if (skipStats == false && (TurboMode == false || Config.ShowStatsForSkippedBattles || _manualSkip || (Config.BattleReport && State.GameManager.CurrentPreviewSkip == GameManager.PreviewSkip.SkipWithStats)))
            {
                int remainingGarrison = 0;
                if (_garrison != null) remainingGarrison = _garrison.Where(s => s.Unit.IsDead == false).Count();
                string defenderNames = "";
                if (_armies[1] != null) defenderNames += $"{_armies[1].Name}\n";
                if (_village != null && (_garrison?.Any() ?? false)) defenderNames += $"{_village.Name} Garrison\n";
                State.GameManager.StatScreen.AttackerTitle.text = $"{AttackerName} - Attacker\n{_armies[0].Name}";
                State.GameManager.StatScreen.DefenderTitle.text = $"{DefenderName} - Defender\n{defenderNames}";
                List<Unit> defenders = new List<Unit>();
                if (_armies[1] != null) defenders.AddRange(_armies[1].Units);
                if (_garrison != null) defenders.AddRange(_garrison.Select(s => s.Unit));
                double endingAttackerPower = StrategicUtilities.UnitValue(_armies[0].Units.Where(s => s.IsDead == false).ToList());
                double endingDefenderPower = StrategicUtilities.UnitValue(defenders.Where(s => s.IsDead == false).ToList());
                State.GameManager.StatScreen.VictoryType.text = TacticalStats.OverallSummary(StartingAttackerPower, endingAttackerPower, StartingDefenderPower, endingDefenderPower, remainingAttackers > 0);
                State.GameManager.StatScreen.AttackerText.text = TacticalStats.AttackerSummary(remainingAttackers) + attackerReceives;
                State.GameManager.StatScreen.DefenderText.text = TacticalStats.DefenderSummary(remainingDefenders - remainingGarrison, remainingGarrison) + defenderReceives;
                State.GameManager.StatScreen.Open(AIAttacker && AIDefender);
            }
            else
                State.GameManager.SwitchToStrategyMode();

            _autoAdvancing = false;
            return true;
        }

        return false;
    }

    private void ProcessReplaceable(int remainingAttackers)
    {
        if (remainingAttackers > 0)
        {
            foreach (ActorUnit actor in _units.Where(s => !Equals(s.Unit.Side, _defenderSide)))
            {
                if (actor.Unit.HasTrait(TraitType.Replaceable) && actor.Unit.IsDead)
                {
                    actor.Unit.SetExp(actor.Unit.Experience * .5f);
                    if (actor.Unit.Experience < _armies[0].EmpireOutside.StartingXp) actor.Unit.SetExp(_armies[0].EmpireOutside.StartingXp);
                    RunReplace(actor);
                }
            }
        }
        else
        {
            foreach (ActorUnit actor in _units.Where(s => Equals(s.Unit.Side, _defenderSide)))
            {
                if (actor.Unit.HasTrait(TraitType.Replaceable) && actor.Unit.IsDead)
                {
                    actor.Unit.SetExp(actor.Unit.Experience * .5f);
                    var defEmp = State.World.GetEmpireOfSide(_defenderSide);
                    var startingExp = defEmp?.StartingXp ?? 0;
                    if (actor.Unit.Experience < startingExp) actor.Unit.SetExp(startingExp);
                    RunReplace(actor);
                }
            }
        }

        void RunReplace(ActorUnit actor)
        {
            for (int i = 0; i < 20; i++)
            {
                if (actor.Unit.Experience < actor.Unit.GetExperienceRequiredForLevel(actor.Unit.Level - 1))
                    actor.Unit.LevelDown();
                else
                    break;
            }

            var raceData = RaceFuncs.GetRace(actor.Unit);
            actor.Unit.RandomizeNameAndGender(actor.Unit.Race, raceData);
            raceData.RandomCustomCall(actor.Unit);
            actor.Unit.DigestedUnits = 0;
            actor.Unit.KilledUnits = 0;
            actor.Unit.Health = actor.Unit.MaxHealth;
            actor.PredatorComponent?.PurgePrey();
        }
    }

    private void LootItems(int remainingDefenders, ref string attackerReceives, ref string defenderReceives)
    {
        bool attackerFoundSpell = false;
        bool defenderFoundSpell = false;
        List<Item> items = new List<Item>();
        foreach (ActorUnit actor in _units)
        {
            if (actor.Unit.IsDead && actor.Unit.Type != UnitType.Leader)
            {
                foreach (Item item in actor.Unit.Items)
                {
                    if (item is SpellBook)
                    {
                        if (remainingDefenders > 0)
                        {
                            if (_armies[1] != null)
                            {
                                _armies[1].ItemStock.AddItem(State.World.ItemRepository.GetItemType(item));
                                items.Add(item);
                                defenderFoundSpell = true;
                            }
                            else
                            {
                                _village.ItemStock.AddItem(State.World.ItemRepository.GetItemType(item));
                                items.Add(item);
                                defenderFoundSpell = true;
                            }
                        }
                        else
                        {
                            _armies[0].ItemStock.AddItem(State.World.ItemRepository.GetItemType(item));
                            items.Add(item);
                            attackerFoundSpell = true;
                        }
                    }
                }
            }
        }

        if (remainingDefenders > 0)
        {
            if (_armies[1] != null)
            {
                if (_armies[0].ItemStock.TransferAllItems(_armies[1].ItemStock, ref items)) defenderFoundSpell = true;
            }
            else
            {
                if (_armies[0].ItemStock.TransferAllItems(_village.ItemStock, ref items)) defenderFoundSpell = true;
            }
        }
        else
        {
            if (_armies[1] != null)
            {
                if (_armies[1].ItemStock.TransferAllItems(_armies[0].ItemStock, ref items)) attackerFoundSpell = true;
            }

            if (_village?.ItemStock.TransferAllItems(_armies[0].ItemStock, ref items) ?? false) attackerFoundSpell = true;
        }

        string itemString = "";
        if (items.Count > 0)
        {
            itemString = string.Join(", ", items.Distinct().Select(s => s.Name));
        }

        if (defenderFoundSpell) defenderReceives += $"<color=yellow>Received Items</color>\n{itemString}";
        if (attackerFoundSpell) attackerReceives += $"<color=yellow>Received Items</color>\n{itemString}";
    }

    private void CalculateRemaining(ref int remainingAttackers, ref int remainingDefenders)
    {
        int surrenderedAttackers = 0;
        int surrenderedDefenders = 0;

        for (int i = 0; i < _units.Count; i++)
        {
            if (_units[i] != null && _units[i].Fled == false)
            {
                ActorUnit actor = _units[i];
                if (actor.Unit.IsDead == false)
                {
                    //if (actor.SelfPrey == null && actor.Visible == false || actor.Targetable == false)
                    //{
                    //    actor.Visible = true;
                    //    actor.Targetable = true;
                    //}
                    if ((actor.SelfPrey?.Predator == null || actor.SelfPrey?.Predator.PredatorComponent?.IsActorInPrey(actor) == false || (actor.SelfPrey.TurnsSinceLastDamage > 3 && actor.SelfPrey.Predator.Unit.HasTrait(TraitType.Endosoma))) && actor.Unit.IsDead == false && actor.Visible == false && actor.Targetable == false)
                    {
                        actor.SelfPrey = null;
                        Debug.Log("Prey orphan found, fixing");
                        actor.Targetable = true;
                        actor.Visible = true;
                    }

                    if (actor.SelfPrey?.Predator != null && (actor.SelfPrey.Predator.Unit?.IsDead ?? true))
                    {
                        actor.SelfPrey.Predator.PredatorComponent.FreeAnyAlivePrey();
                    }

                    if (Equals(actor.Unit.Side, _armies[0].Side))
                    {
                        remainingAttackers++;
                        if (actor.SelfPrey != null && actor.Unit.HasTrait(TraitType.TheGreatEscape)) remainingAttackers--;
                        if (actor.Surrendered) surrenderedAttackers++;
                        var preyCount = actor.PredatorComponent?.PreyCount ?? 0;
                        if (preyCount > 0)
                        {
                            remainingDefenders += preyCount;
                            if (actor.Unit.HasTrait(TraitType.Endosoma))
                            {
                                remainingDefenders -= actor.PredatorComponent.GetDirectPrey().Where(s => Equals(actor.Unit.Side, s.Unit.Side) || s.Unit.HasTrait(TraitType.TheGreatEscape)).Count();
                            }
                            else
                            {
                                remainingDefenders -= actor.PredatorComponent.GetDirectPrey().Where(s => s.Unit.HasTrait(TraitType.TheGreatEscape)).Count();
                            }
                        }
                    }
                    else
                    {
                        remainingDefenders++;
                        if (actor.SelfPrey != null && actor.Unit.HasTrait(TraitType.TheGreatEscape)) remainingDefenders--;
                        if (actor.Surrendered) surrenderedDefenders++;
                        var preyCount = actor.PredatorComponent?.PreyCount ?? 0;
                        if (preyCount > 0)
                        {
                            remainingAttackers += preyCount;
                            if (actor.Unit.HasTrait(TraitType.Endosoma))
                            {
                                remainingAttackers -= actor.PredatorComponent.GetDirectPrey().Where(s => Equals(actor.Unit.Side, s.Unit.Side) || s.Unit.HasTrait(TraitType.TheGreatEscape)).Count();
                            }
                            else
                            {
                                remainingAttackers -= actor.PredatorComponent.GetDirectPrey().Where(s => s.Unit.HasTrait(TraitType.TheGreatEscape)).Count();
                            }
                        }
                    }
                }
            }
        }

        if (CurrentTurn > 500)
        {
            if (surrenderedAttackers == remainingAttackers)
            {
                for (int i = 0; i < _units.Count; i++)
                {
                    if (_units[i] != null && _units[i].Fled == false)
                    {
                        ActorUnit actor = _units[i];
                        if (actor.Unit.IsDead == false)
                        {
                            if (Equals(actor.Unit.Side, _armies[0].Side))
                            {
                                actor.Unit.Health = 0;
                            }
                        }
                    }
                }
            }
            else if (surrenderedDefenders == remainingDefenders)
            {
                for (int i = 0; i < _units.Count; i++)
                {
                    if (_units[i] != null && _units[i].Fled == false)
                    {
                        ActorUnit actor = _units[i];
                        if (actor.Unit.IsDead == false)
                        {
                            if (!Equals(actor.Unit.Side, _armies[0].Side))
                            {
                                actor.Unit.Health = 0;
                            }
                        }
                    }
                }
            }
        }
    }

    private void AssignLeftoverTroops(Army army, List<ActorUnit> actors)
    {
        foreach (ActorUnit actor in actors.ToList())
        {
            if (actor.Unit.IsDead)
            {
                actors.Remove(actor);
            }
            else
            {
                //Extra safety to eliminate the possibility of doubled units
                _retreatedAttackers.Remove(actor.Unit);
                _retreatedDefenders.Remove(actor.Unit);
            }
        }

        List<ActorUnit> leftover = new List<ActorUnit>();
        if (army != null)
        {
            foreach (ActorUnit actor in actors.ToList()) //Prevent doubling up of retreated defected units
            {
                if (army.Units.Contains(actor.Unit))
                {
                    actors.Remove(actor);
                }
            }

            foreach (Unit unit in army.Units.ToList())
            {
                if (unit.IsDead)
                {
                    army.Units.Remove(unit);
                    State.World.Stats?.SoldiersLost(1, unit.Side);
                }
            }

            while (army.Units.Count() < army.MaxSize && actors.Any())
            {
                army.Units.Add(actors[0].Unit);
                actors.RemoveAt(0);
            }

            while (army.Units.Count() > army.MaxSize)
            {
                var last = army.Units.Last();
                army.Units.Remove(last);
                actors.Add(new ActorUnit(last));
            }
        }

        if (_village != null && actors.Any())
        {
            foreach (var unit in actors.Select(s => s.Unit))
            {
                if (_village.GetRecruitables().Contains(unit) == false) _village.VillagePopulation.AddHireable(unit);
            }

            actors.Clear();
        }

        if (actors.Any())
        {
            TacticalUtilities.ProcessTravelingUnits(actors.Select(s => s.Unit).ToList());
        }
    }

    private void ProcessFledUnits()
    {
        _retreatedAttackers.RemoveAll(s => s.IsDead);
        _retreatedDefenders.RemoveAll(s => s.IsDead);
        if (_retreatedAttackers.Any())
        {
            TacticalUtilities.ProcessTravelingUnits(_retreatedAttackers);
        }

        if (_retreatedDefenders.Any())
        {
            TacticalUtilities.ProcessTravelingUnits(_retreatedDefenders);
        }
    }

    private void ProcessConsumedCorpses()
    {
        if (Config.EdibleCorpses == false) return;
        ActorUnit[] survivingPredators = _units.Where(s => s.Unit.IsDead == false && s.Unit.Predator).ToArray();
        if (survivingPredators.Length == 0) return;
        for (int i = 0; i < 500; i++)
        {
            ActorUnit preyUnit = _units.Where(s => s.Unit.IsDead && s.Unit.Health > s.Unit.MaxHealth * -1).FirstOrDefault();
            if (preyUnit == null) break;
            ActorUnit predatorUnit = survivingPredators.Where(s => s.Unit.Health < s.Unit.MaxHealth).OrderByDescending(s => s.Unit.MaxHealth - s.Unit.Health).FirstOrDefault();
            if (predatorUnit == null) predatorUnit = survivingPredators[State.Rand.Next(survivingPredators.Length)];
            predatorUnit.Unit.GiveScaledExp(4, predatorUnit.Unit.Level - preyUnit.Unit.Level, true);
            predatorUnit.Unit.Heal((preyUnit.Unit.MaxHealth + preyUnit.Unit.Health) / 2);
            //Weight gain disabled for consuming corpses
            preyUnit.Unit.Health = -999999;
        }
    }

    private void EatSurrenderedAllies()
    {
        if (Config.EatSurrenderedAllies == false) return;
        ActorUnit[] survivingPredators = _units.Where(s => s.Unit.IsDead == false && s.Unit.Predator && s.Unit.Predator).ToArray();
        if (survivingPredators.Length == 0) return;
        for (int i = 0; i < 500; i++)
        {
            ActorUnit preyUnit = _units.Where(s => s.Unit.IsDead == false && s.Surrendered).FirstOrDefault();
            if (preyUnit == null) break;
            ActorUnit predatorUnit = survivingPredators.Where(s => s.Unit.Health < s.Unit.MaxHealth).OrderByDescending(s => s.Unit.MaxHealth - s.Unit.Health).FirstOrDefault();
            if (predatorUnit == null) predatorUnit = survivingPredators[State.Rand.Next(survivingPredators.Length)];
            predatorUnit.Unit.GiveScaledExp(6, predatorUnit.Unit.Level - preyUnit.Unit.Level, true);
            predatorUnit.Unit.Heal((preyUnit.Unit.MaxHealth + preyUnit.Unit.Health) / 2);
            //Weight gain disabled for consuming allies
            TacticalStats.RegisterAllyVore(predatorUnit.Unit.Side);
            predatorUnit.Unit.DigestedUnits++;
            preyUnit.Unit.Kill();
            if (predatorUnit.Unit.HasTrait(TraitType.EssenceAbsorption) && predatorUnit.Unit.DigestedUnits % 4 == 0) predatorUnit.Unit.GeneralStatIncrease(1);
            preyUnit.Unit.Health = -999999;
        }
    }


    public void ProcessTileEffects()
    {
        if (ActiveEffects == null) return;
        foreach (var key in ActiveEffects.ToList())
        {
            key.Value.RemainingDuration -= 1;

            if (key.Value.Type == TileEffectType.Fire)
            {
                var actor = TacticalUtilities.GetActorAt(key.Key);
                if (actor != null)
                {
                    int damage = Mathf.RoundToInt(key.Value.Strength * actor.Unit.TraitBoosts.FireDamageTaken);
                    if (actor.Damage(damage, true))
                    {
                        Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> took <color=red>{damage}</color> points of fire damage");
                    }
                }
            }

            if (key.Value.RemainingDuration <= 0)
            {
                EffectTileMap.SetTile(new Vector3Int(key.Key.X, key.Key.Y, 0), null);
                ActiveEffects.Remove(key.Key);
            }
        }
    }


    public override void ReceiveInput()
    {
        _remainingLockedPanelTime -= Time.deltaTime;
        if (Input.GetButtonDown("Hide"))
        {
            InfoPanel.RefreshTacticalUnitInfo(null);
            InfoPanel.HidePanel();
        }
        else
        {
            InfoPanel.RefreshLastUnitInfo();
        }

        if (TacticalLogUpdated) //Done this way so the unity auto-size will adjust between the change of the window and this movement
        {
            var transform = LogUI.Text.transform;
            transform.localPosition = new Vector3(0, transform.GetComponent<RectTransform>().rect.height, 0);
            TacticalLogUpdated = false;
        }

        if (State.GameManager.StatScreen.gameObject.activeSelf) return;
        if (DirtyPack) UpdateAreaTraits();
        if (Input.GetButtonDown("Menu"))
        {
            State.GameManager.OpenMenu();
        }

        if (TurboMode) return;
        if (Input.GetButtonDown("Pause"))
        {
            _paused = !_paused;
            PausedText.SetActive(_paused);
        }

        if (Input.GetButtonDown("ReloadSprites"))
        {
            GameManager.CustomManager.Refresh();
        }

        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Z))
        {
            UndoMovement();
        }

        if (Input.GetButtonDown("Menu"))
        {
            State.GameManager.OpenMenu();
        }

        if (Input.GetButtonDown("Quicksave"))
        {
            State.Save($"{State.SaveDirectory}Quicksave.sav");
        }
        else if (Input.GetButtonDown("Quickload"))
        {
            State.GameManager.AskQuickLoad();
        }

        if (EventSystem.current.IsPointerOverGameObject() == false) //Makes sure mouse isn't over a UI element
        {
            Vector2 currentMousePos = State.GameManager.Camera.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)(currentMousePos.x + 0.5f);
            int y = (int)(currentMousePos.y + 0.5f);
            if (x >= 0 && x <= _tiles.GetUpperBound(0) && y >= 0 && y <= _tiles.GetUpperBound(1))
            {
                MouseOver(x, y);
                if (Input.GetMouseButtonDown(0)) ProcessLeftClick(x, y);
                if (Input.GetMouseButtonDown(1) && SelectedUnit != null && SelectedUnit.Movement > 0 && (IsPlayerTurn || PseudoTurn)) ProcessRightClick(x, y);
                if (Input.GetMouseButtonDown(2)) _remainingLockedPanelTime = 1.5f;
            }
            else
            {
                _arrowManager.ClearNodes();
                _currentPathDestination = null;
            }
        }

        if (_reviewingBattle)
        {
            if (Input.GetButtonDown("Submit")) EndTurn();
            return;
        }

        if (_paused || State.GameManager.UnitEditor.gameObject.activeSelf) return;
        UpdateStatus(Time.deltaTime);
        if (IsPlayerTurn || PseudoTurn)
        {
            if (_queuedPath != null) return;

            if (RunningFriendlyAI) return;
            if (_foreignAI != null) return;

            if (SelectedUnit != null) MoveActor();
            if (Input.GetButtonDown("Cancel")) ActionMode = 0;
            if (Input.GetButtonDown("Next Unit")) NextActor(NextUnitType.Any);
            if (Input.GetButtonDown("Next Melee Unit")) NextActor(NextUnitType.Melee);
            if (Input.GetButtonDown("Next Ranged Unit")) NextActor(NextUnitType.Ranged);
            if (Input.GetButtonDown("Submit"))
            {
                var box = FindObjectOfType<DialogBox>();
                if (box != null)
                    box.YesClicked();
                else
                    PromptEndTurn();
            }
        }
    }

    private void UpdateFog()
    {
        FogOfWar.gameObject.SetActive(true);
        if (FogSystem == null) FogSystem = new FogSystemTactical(FogOfWar, FogTile);
        FogSystem.UpdateFog(_units, _defenderSide, _attackersTurn, AIAttacker, AIDefender, CurrentTurn);
    }

    public override void CleanUp()
    {
        ActionMode = 0;
        _autoAdvancing = false;
        TurboMode = false;
        AutoAdvanceText.SetActive(false);
        RunningFriendlyAI = false;
        SelectedUnit = null;
        SelectionBox.gameObject.SetActive(false);

        Decorations = new PlacedDecoration[0];

        Log.Clear();

        foreach (ActorUnit actor in _units)
        {
            actor.UnitSprite = null;
        }

        int children = TerrainFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(TerrainFolder.GetChild(i).gameObject);
        }

        children = ActorFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(ActorFolder.GetChild(i).gameObject);
        }

        Tilemap.ClearAllTiles();
        UnderTilemap.ClearAllTiles();
        FrontTilemap.ClearAllTiles();
        FrontSpriteTilemap.ClearAllTiles();
        FrontColorTilemap.ClearAllTiles();
        EffectTileMap.ClearAllTiles();
        FogOfWar.ClearAllTiles();
        RightClickMenu.CloseAll();
        TacticalUtilities.ResetData();
    }

    internal void HandleReanimationSideEffects(Unit caster, ActorUnit target)
    {
        _armies[0].Units.Remove(target.Unit);
        State.GameManager.TacticalMode.ExtraAttackers.Remove(target);
        _garrison.Remove(target);
        _armies[1]?.Units.Remove(target.Unit);
        State.GameManager.TacticalMode.ExtraDefenders.Remove(target);
        _village?.GetRecruitables().Remove(target.Unit);
        target.Unit.Side = caster.Side;
        State.GameManager.TacticalMode.UpdateActorColor(target);
    }
}