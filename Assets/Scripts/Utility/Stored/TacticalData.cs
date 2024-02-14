using OdinSerializer;
using System.Collections.Generic;
using TacticalBuildings;

public class TacticalData
{
    [OdinSerialize]
    public List<Actor_Unit> units;
    [OdinSerialize]
    private Army[] _armies;
    public Army[] armies { get => _armies; set => _armies = value; }
    [OdinSerialize]
    private Village _village;
    public Village village { get => _village; set => _village = value; }

    [OdinSerialize]
    private List<ClothingDiscards> _discardedClothing;
    internal List<ClothingDiscards> DiscardedClothing { get => _discardedClothing; set => _discardedClothing = value; }

    [OdinSerialize]
    private TacticalTileType[,] _tiles;
    public TacticalTileType[,] tiles { get => _tiles; set => _tiles = value; }

    [OdinSerialize]
    public Actor_Unit selectedUnit;

    [OdinSerialize]
    private Side _defenderSide;
    public Side defenderSide { get => _defenderSide; set => _defenderSide = value; }

    [OdinSerialize]
    private int _currentTurn;
    public int currentTurn { get => _currentTurn; set => _currentTurn = value; }

    [OdinSerialize]
    private bool _attackersTurn;
    public bool attackersTurn { get => _attackersTurn; set => _attackersTurn = value; }
    [OdinSerialize]
    private bool _isAPlayerTurn;
    public bool isAPlayerTurn { get => _isAPlayerTurn; set => _isAPlayerTurn = value; }
    [OdinSerialize]
    private Side _activeSide;
    public Side activeSide { get => _activeSide; set => _activeSide = value; }

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
    public ITacticalAI currentAI { get => _currentAI; set => _currentAI = value; }
    [OdinSerialize]
    private ITacticalAI _attackerAI;
    public ITacticalAI attackerAI { get => _attackerAI; set => _attackerAI = value; }
    [OdinSerialize]
    private ITacticalAI _defenderAI;
    public ITacticalAI defenderAI { get => _defenderAI; set => _defenderAI = value; }

    [OdinSerialize]
    private bool _runningFriendlyAI;
    public bool runningFriendlyAI { get => _runningFriendlyAI; set => _runningFriendlyAI = value; }

    [OdinSerialize]
    public List<Actor_Unit> extraAttackers;

    [OdinSerialize]
    public List<Actor_Unit> extraDefenders;

    [OdinSerialize]
    private List<Unit> _retreatedAttackers;
    public List<Unit> retreatedAttackers { get => _retreatedAttackers; set => _retreatedAttackers = value; }

    [OdinSerialize]
    private List<Unit> _retreatedDefenders;
    public List<Unit> retreatedDefenders { get => _retreatedDefenders; set => _retreatedDefenders = value; }

    [OdinSerialize]
    private TacticalMessageLog _log;
    public TacticalMessageLog log { get => _log; set => _log = value; }

    [OdinSerialize]
    private List<MiscDiscard> _miscDiscards;
    internal List<MiscDiscard> MiscDiscards { get => _miscDiscards; set => _miscDiscards = value; }

    [OdinSerialize]
    private int _lastDiscard;
    public int LastDiscard { get => _lastDiscard; set => _lastDiscard = value; }

    [OdinSerialize]
    internal List<Actor_Unit> garrison;

    [OdinSerialize]
    private double _startingAttackerPower;
    public double StartingAttackerPower { get => _startingAttackerPower; set => _startingAttackerPower = value; }

    [OdinSerialize]
    private double _startingDefenderPower;
    public double StartingDefenderPower { get => _startingDefenderPower; set => _startingDefenderPower = value; }

    [OdinSerialize]
    private TacticalBuilding[] _buildings;
    internal TacticalBuilding[] buildings { get => _buildings; set => _buildings = value; }
    [OdinSerialize]
    internal Dictionary<Vec2, TileEffect> activeEffects;

    [OdinSerialize]
    private DecorationStorage[] _decorationStorage;
    internal DecorationStorage[] decorationStorage { get => _decorationStorage; set => _decorationStorage = value; }
}

