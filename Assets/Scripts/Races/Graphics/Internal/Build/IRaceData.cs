internal interface IRaceData
{
    IMiscRaceData MiscRaceData { get; }

    FullSpriteProcessOut NewUpdate(Actor_Unit actor);

    void RandomCustomCall(Unit unit);
}