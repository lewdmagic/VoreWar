using OdinSerializer;


internal abstract class ClaimableBuilding
{
    [OdinSerialize]
    private Empire _owner;
    internal Empire Owner { get => _owner; set => _owner = value; }

    [OdinSerialize]
    private Vec2i _position;
    internal Vec2i Position { get => _position; set => _position = value; }

    protected ClaimableBuilding(Vec2i location)
    {
        Position = location;
    }

    internal abstract void TurnChanged();


}

