using OdinSerializer;
using TacticalDecorations;

class DecorationStorage
{
    [OdinSerialize]
    private Vec2 _position;
    internal Vec2 Position { get => _position; set => _position = value; }
    [OdinSerialize]
    private TacDecType _type;
    internal TacDecType Type { get => _type; set => _type = value; }

    public DecorationStorage(Vec2 position, TacDecType type)
    {
        Position = position;
        Type = type;
    }
}

