#region

using System.Collections.Generic;

#endregion

internal class FullSpriteProcessOut
{
    internal readonly AccumulatedClothes AccumulatedClothes;
    internal readonly IRunOutputReadable RunOutput;
    internal readonly IReadOnlyDictionary<SpriteType, RaceRenderOutput> SpriteOutputs;

    public FullSpriteProcessOut(IRunOutputReadable runOutput, IReadOnlyDictionary<SpriteType, RaceRenderOutput> spriteOutputs, AccumulatedClothes accumulatedClothes)
    {
        RunOutput = runOutput;
        SpriteOutputs = spriteOutputs;
        AccumulatedClothes = accumulatedClothes;
    }
}