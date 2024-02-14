using OdinSerializer;


internal class InvisibleTravelingUnit
{
    [OdinSerialize]
    private Unit _unit;

    internal Unit unit { get => _unit; set => _unit = value; }

    [OdinSerialize]
    private int _remainingTurns;

    internal int remainingTurns { get => _remainingTurns; set => _remainingTurns = value; }

    public InvisibleTravelingUnit(Unit unit, int remainingTurns)
    {
        this.unit = unit;
        this.remainingTurns = remainingTurns;
    }
}