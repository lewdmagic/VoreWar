using OdinSerializer;

public abstract class Item
{
    [OdinSerialize]
    public string Name { get; set; }
    [OdinSerialize]
    public int Cost { get; set; }
    [OdinSerialize]
    public string Description { get; set; }
    [OdinSerialize]
    public bool LockedItem { get; set; }
}
