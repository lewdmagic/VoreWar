public interface IRaceRenderInput : IRenderInput
{
    IMiscRaceData RaceData { get; }
    bool BaseBody { get; }
}

public interface IRaceRenderInput<out T> : IRaceRenderInput where T : IParameters
{
    T Params { get; }
}