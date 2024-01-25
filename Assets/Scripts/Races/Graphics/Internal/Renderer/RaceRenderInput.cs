internal partial class RaceRenderer
{
    private class RaceRenderInput : RenderInput, IRaceRenderInput
    {
        internal RaceRenderInput(Actor_Unit actor, IMiscRaceData miscRaceData, bool baseBody) : base(actor)
        {
            RaceData = miscRaceData;
            BaseBody = baseBody;
        }

        public IMiscRaceData RaceData { get; private set; }
        public bool BaseBody { get; private set; }
    }
}