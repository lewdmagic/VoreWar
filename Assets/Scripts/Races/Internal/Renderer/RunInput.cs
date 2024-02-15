internal partial class RaceRenderer
{
    private class RunInput : RenderInput, IRunInput
    {
        internal RunInput(ActorUnit actor) : base(actor)
        {
        }
    }
}