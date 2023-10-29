internal struct RaceFrameList
{
    internal int[] frames;
    internal float[] times;

    internal RaceFrameList(int[] fra, float[] tim)
    {
        frames = fra;
        times = tim;
    }
}