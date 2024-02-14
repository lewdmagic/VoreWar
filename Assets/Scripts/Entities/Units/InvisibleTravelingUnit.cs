using OdinSerializer;


internal class InvisibleTravelingUnit
{
    [OdinSerialize]
    private Unit _unit;

    internal Unit Unit { get => _unit; set => _unit = value; }

    [OdinSerialize]
    private int _remainingTurns;

    internal int RemainingTurns { get => _remainingTurns; set => _remainingTurns = value; }

    public InvisibleTravelingUnit(Unit unit, int remainingTurns)
    {
        this.Unit = unit;
        this.RemainingTurns = remainingTurns;
    }
}