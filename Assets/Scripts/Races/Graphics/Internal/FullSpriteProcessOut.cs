#region

using System.Collections.Generic;

#endregion

internal class FullSpriteProcessOut
{
    internal AccumulatedClothes AccumulatedClothes;
    internal IRunOutputReadable RunOutput;
    internal IReadOnlyDictionary<SpriteType, RaceRenderOutput> spriteOutputs;

    public FullSpriteProcessOut(IRunOutputReadable runOutput, IReadOnlyDictionary<SpriteType, RaceRenderOutput> spriteOutputs, AccumulatedClothes accumulatedClothes)
    {
        RunOutput = runOutput;
        this.spriteOutputs = spriteOutputs;
        AccumulatedClothes = accumulatedClothes;
    }
}