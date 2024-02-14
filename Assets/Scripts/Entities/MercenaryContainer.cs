using OdinSerializer;

internal class MercenaryContainer
{
    [OdinSerialize]
    private Unit _unit;

    internal Unit Unit { get => _unit; set => _unit = value; }

    [OdinSerialize]
    private int _cost;

    internal int Cost { get => _cost; set => _cost = value; }

    [OdinSerialize]
    private string _title;

    internal string Title { get => _title; set => _title = value; }
}