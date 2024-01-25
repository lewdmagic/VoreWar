using System.Collections.Generic;

internal partial class RaceRenderer
{
    private class RaceSpriteChangeDict : ISpriteChanger
    {
        internal readonly Dictionary<SpriteType, RaceRenderOutput> ReusedChangesDict = new Dictionary<SpriteType, RaceRenderOutput>();
        private readonly ISpriteCollection _spriteCollection;

        public RaceSpriteChangeDict(ISpriteCollection spriteCollection)
        {
            _spriteCollection = spriteCollection;
        }
    
        public IRaceRenderOutput ChangeSprite(SpriteType spriteType)
        {
            if (!ReusedChangesDict.TryGetValue(spriteType, out var raceRenderOutput))
            {
                raceRenderOutput = new RaceRenderOutput(_spriteCollection);
                ReusedChangesDict.Add(spriteType, raceRenderOutput);
            }

            return raceRenderOutput;
        }
    }
}