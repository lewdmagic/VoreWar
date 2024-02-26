using OdinSerializer;

public abstract class Item
{
    [OdinSerialize]
    private string _name;

    public string Name { get => _name; set => _name = value; }

    [OdinSerialize]
    private int _cost;

    public int Cost { get => _cost; set => _cost = value; }

    [OdinSerialize]
    private string _description;

    public string Description { get => _description; set => _description = value; }

    [OdinSerialize]
    private bool _lockedItem;

    public bool LockedItem { get => _lockedItem; set => _lockedItem = value; }
}