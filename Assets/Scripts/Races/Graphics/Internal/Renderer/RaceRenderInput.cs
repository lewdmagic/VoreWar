internal partial class RaceRenderer
{
    private class RaceRenderInput : RenderInput, IRaceRenderInput
    {
        internal RaceRenderInput(Actor_Unit actor, ISetupOutput setupOutput, bool baseBody) : base(actor)
        {
            RaceData = setupOutput;
            BaseBody = baseBody;
        }

        public ISetupOutput RaceData { get; private set; }
        public bool BaseBody { get; private set; }
    }
}