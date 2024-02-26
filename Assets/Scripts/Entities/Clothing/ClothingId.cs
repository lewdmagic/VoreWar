using OdinSerializer;

public class ClothingId
{
    [OdinSerialize]
    public readonly string Id;

    public ClothingId(string id)
    {
        Id = id;
    }
}