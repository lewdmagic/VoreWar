internal struct RaceFrameList
{
    internal readonly int[] Frames;
    internal readonly float[] Times;

    internal RaceFrameList(int[] fra, float[] tim)
    {
        Frames = fra;
        Times = tim;
    }
}