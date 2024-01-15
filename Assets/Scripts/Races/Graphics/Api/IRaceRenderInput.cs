public interface IRaceRenderInput : IRenderInput
{
    IMiscRaceData RaceData { get; }
    bool BaseBody { get; }
}