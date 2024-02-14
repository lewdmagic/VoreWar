using OdinSerializer;

internal class EventLog
{
    [OdinSerialize]
    private TacticalMessageLog.MessageLogEvent _type;

    internal TacticalMessageLog.MessageLogEvent Type { get => _type; set => _type = value; }

    [OdinSerialize]
    private float _odds;

    internal float Odds { get => _odds; set => _odds = value; }

    [OdinSerialize]
    private Unit _target;

    internal Unit Target { get => _target; set => _target = value; }

    [OdinSerialize]
    private Unit _unit;

    internal Unit Unit { get => _unit; set => _unit = value; }

    [OdinSerialize]
    private Unit _prey;

    internal Unit Prey { get => _prey; set => _prey = value; }

    [OdinSerialize]
    private PreyLocation _preyLocation;

    internal PreyLocation preyLocation { get => _preyLocation; set => _preyLocation = value; }

    [OdinSerialize]
    private PreyLocation _oldLocation;

    internal PreyLocation oldLocation { get => _oldLocation; set => _oldLocation = value; }

    [OdinSerialize]
    private Weapon _weapon;

    internal Weapon Weapon { get => _weapon; set => _weapon = value; }

    [OdinSerialize]
    private int _damage;

    internal int Damage { get => _damage; set => _damage = value; }

    [OdinSerialize]
    private int _bonus;

    internal int Bonus { get => _bonus; set => _bonus = value; }

    [OdinSerialize]
    private string _message;

    internal string Message { get => _message; set => _message = value; }

    [OdinSerialize]
    private string _extra;

    internal string Extra { get => _extra; set => _extra = value; }
}