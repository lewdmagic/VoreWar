using OdinSerializer;
using System.Collections.Generic;
using TacticalBuildings;

public class TacticalData
{
    [OdinSerialize]
    public List<ActorUnit> Units;

    [OdinSerialize]
    private Army[] _armies;

    public Army[] Armies { get => _armies; set => _armies = value; }

    [OdinSerialize]
    private Village _village;

    public Village Village { get => _village; set => _village = value; }

    [OdinSerialize]
    private List<ClothingDiscards> _discardedClothing;

    internal List<ClothingDiscards> DiscardedClothing { get => _discardedClothing; set => _discardedClothing = value; }

    [OdinSerialize]
    private TacticalTileType[,] _tiles;

    public TacticalTileType[,] Tiles { get => _tiles; set => _tiles = value; }

    [OdinSerialize]
    public ActorUnit SelectedUnit;

    [OdinSerialize]
    private Side _defenderSide;

    public Side DefenderSide { get => _defenderSide; set => _defenderSide = value; }

    [OdinSerialize]
    private int _currentTurn;

    public int CurrentTurn { get => _currentTurn; set => _currentTurn = value; }

    [OdinSerialize]
    private bool _attackersTurn;

    public bool AttackersTurn { get => _attackersTurn; set => _attackersTurn = value; }

    [OdinSerialize]
    private bool _isAPlayerTurn;

    public bool IsAPlayerTurn { get => _isAPlayerTurn; set => _isAPlayerTurn = value; }

    [OdinSerialize]
    private Side _activeSide;

    public Side ActiveSide { get => _activeSide; set => _activeSide = value; }

    [OdinSerialize]
    private bool _aIAttacker;

    public bool AIAttacker { get => _aIAttacker; set => _aIAttacker = value; }

    [OdinSerialize]
    private bool _aIDefender;

    public bool AIDefender { get => _aIDefender; set => _aIDefender = value; }

    [OdinSerialize]
    private string _attackerName;

    public string AttackerName { get => _attackerName; set => _attackerName = value; }

    [OdinSerialize]
    private string _defenderName;

    public string DefenderName { get => _defenderName; set => _defenderName = value; }


    [OdinSerialize]
    private TacticalStats _tacticalStats;

    public TacticalStats TacticalStats { get => _tacticalStats; set => _tacticalStats = value; }

    [OdinSerialize]
    private ITacticalAI _currentAI;

    public ITacticalAI CurrentAI { get => _currentAI; set => _currentAI = value; }

    [OdinSerialize]
    private ITacticalAI _attackerAI;

    public ITacticalAI AttackerAI { get => _attackerAI; set => _attackerAI = value; }

    [OdinSerialize]
    private ITacticalAI _defenderAI;

    public ITacticalAI DefenderAI { get => _defenderAI; set => _defenderAI = value; }

    [OdinSerialize]
    private bool _runningFriendlyAI;

    public bool RunningFriendlyAI { get => _runningFriendlyAI; set => _runningFriendlyAI = value; }

    [OdinSerialize]
    public List<ActorUnit> ExtraAttackers;

    [OdinSerialize]
    public List<ActorUnit> ExtraDefenders;

    [OdinSerialize]
    private List<Unit> _retreatedAttackers;

    public List<Unit> RetreatedAttackers { get => _retreatedAttackers; set => _retreatedAttackers = value; }

    [OdinSerialize]
    private List<Unit> _retreatedDefenders;

    public List<Unit> RetreatedDefenders { get => _retreatedDefenders; set => _retreatedDefenders = value; }

    [OdinSerialize]
    private TacticalMessageLog _log;

    public TacticalMessageLog LOG { get => _log; set => _log = value; }

    [OdinSerialize]
    private List<MiscDiscard> _miscDiscards;

    internal List<MiscDiscard> MiscDiscards { get => _miscDiscards; set => _miscDiscards = value; }

    [OdinSerialize]
    private int _lastDiscard;

    public int LastDiscard { get => _lastDiscard; set => _lastDiscard = value; }

    [OdinSerialize]
    internal List<ActorUnit> Garrison;

    [OdinSerialize]
    private double _startingAttackerPower;

    public double StartingAttackerPower { get => _startingAttackerPower; set => _startingAttackerPower = value; }

    [OdinSerialize]
    private double _startingDefenderPower;

    public double StartingDefenderPower { get => _startingDefenderPower; set => _startingDefenderPower = value; }

    [OdinSerialize]
    private TacticalBuilding[] _buildings;

    internal TacticalBuilding[] Buildings { get => _buildings; set => _buildings = value; }

    [OdinSerialize]
    internal Dictionary<Vec2, TileEffect> ActiveEffects;

    [OdinSerialize]
    private DecorationStorage[] _decorationStorage;

    internal DecorationStorage[] DecorationStorage { get => _decorationStorage; set => _decorationStorage = value; }
}