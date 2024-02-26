internal class MonsterSpawnerLocation
{
    internal Vec2I Location;

    internal Race Race;

    public MonsterSpawnerLocation(Vec2I location, Race race)
    {
        Location = location;
        Race = race;
    }
}