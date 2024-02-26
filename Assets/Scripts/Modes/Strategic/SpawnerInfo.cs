using OdinSerializer;


internal class SpawnerInfo
{
    [OdinSerialize]
    private bool _enabled;

    internal bool Enabled { get => _enabled; set => _enabled = value; }

    [OdinSerialize]
    private int _maxArmies;

    internal int MaxArmies { get => _maxArmies; set => _maxArmies = value; }

    [OdinSerialize]
    private float _confidence;

    internal float Confidence { get => _confidence; set => _confidence = value; }

    [OdinSerialize]
    private int _minArmySize;

    internal int MinArmySize { get => _minArmySize; set => _minArmySize = value; }

    [OdinSerialize]
    private int _maxArmySize;

    internal int MaxArmySize { get => _maxArmySize; set => _maxArmySize = value; }

    [OdinSerialize]
    private float _spawnRate;

    internal float SpawnRate { get => _spawnRate; set => _spawnRate = value; }

    [OdinSerialize]
    private float _scalingFactor;

    internal float ScalingFactor { get => _scalingFactor; set => _scalingFactor = value; }

    [OdinSerialize]
    private int _team;

    internal int Team { get => _team; set => _team = value; }

    [OdinSerialize]
    private int _spawnAttempts;

    internal int SpawnAttempts { get => _spawnAttempts; set => _spawnAttempts = value; }

    [OdinSerialize]
    private int _turnOrder;

    internal int TurnOrder { get => _turnOrder; set => _turnOrder = value; }

    [OdinSerialize]
    private bool _addOnRace;

    internal bool AddOnRace { get => _addOnRace; set => _addOnRace = value; }

    [OdinSerialize]
    private bool _usingCustomType;

    internal bool UsingCustomType { get => _usingCustomType; set => _usingCustomType = value; }

    [OdinSerialize]
    internal Config.MonsterConquestType ConquestType;

    internal Config.MonsterConquestType GetConquestType()
    {
        return UsingCustomType ? ConquestType : Config.MonsterConquest;
    }


    public SpawnerInfo(bool enabled, int maxArmies, float spawnRate, float scalingFactor, int team, int spawnAttempts, bool addOnRace, float confidence, int minArmySize, int maxArmySize, int turnOrder)
    {
        Enabled = enabled;
        MaxArmies = maxArmies;
        this.SpawnRate = spawnRate;
        this.ScalingFactor = scalingFactor;
        Team = team;
        SpawnAttempts = spawnAttempts;
        AddOnRace = addOnRace;
        Confidence = confidence == 0 ? 6 : confidence;
        MinArmySize = minArmySize;
        MaxArmySize = maxArmySize;
        TurnOrder = turnOrder;
    }

    public void SetSpawnerType(Config.MonsterConquestType conquestType)
    {
        ConquestType = conquestType;
        UsingCustomType = true;
    }
}