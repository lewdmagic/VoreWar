internal interface IRaceRenderInput
{
    Actor_Unit Actor { get; }
    IMiscRaceData RaceData { get; }
    SpriteDictionary Sprites { get; }
    bool BaseBody { get; }
}

internal interface IRaceRenderInput<out T> : IRaceRenderInput where T : IParameters
{
    T Params { get; }
}