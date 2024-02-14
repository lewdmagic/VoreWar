using OdinSerializer;

public class RacePop
{

    [OdinSerialize]
    private Race _race;
    public Race Race { get => _race; set => _race = value; }
    [OdinSerialize]
    public int Population { get; private set; }
    [OdinSerialize]
    private int _hireables;
    public int Hireables { get => _hireables; set => _hireables = value; }

    public RacePop(Race inRace, int popChange, int hireables)
    {
        Race = inRace;
        Population = popChange;
        Hireables = hireables;
    }

    public RacePop(Race inRace, int popChange)
    {
        Race = inRace;
        Population = popChange;
    }


    internal void ChangePop(int popChange)
    {
        Population += popChange;

    }
}