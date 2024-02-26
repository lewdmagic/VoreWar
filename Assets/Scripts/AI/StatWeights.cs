using OdinSerializer;


internal class StatWeights
{
    [OdinSerialize]
    private float[] _weight = new float[(int)Stat.None];

    internal float[] Weight { get => _weight; set => _weight = value; }
}