using UnityEngine;

internal partial class RaceRenderer
{
    private class RunOutput : IRunOutput
    {
        public Vector3? ClothingShift { get; set; }
        public bool? ActorFurry { get; set; }

        public Vector2? WholeBodyOffset { get; set; }

        private readonly RaceSpriteChangeDict _changeDict;
    
        //
        internal RunOutput(RaceSpriteChangeDict changeDict)
        {
            _changeDict = changeDict;
        }

        public IRaceRenderOutput ChangeSprite(SpriteType spriteType) => _changeDict.ChangeSprite(spriteType);
    }
}