using OdinSerializer;


internal abstract class ClaimableBuilding
{
    [OdinSerialize]
    private Empire _owner;

    internal Empire Owner { get => _owner; set => _owner = value; }

    [OdinSerialize]
    private Vec2I _position;

    internal Vec2I Position { get => _position; set => _position = value; }

    protected ClaimableBuilding(Vec2I location)
    {
        Position = location;
    }

    internal abstract void TurnChanged();
}