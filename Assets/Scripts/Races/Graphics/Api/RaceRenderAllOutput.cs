public class RaceRenderAllOutput : IRaceRenderAllOutput
{
    private readonly ISpriteChanger _changeDict;

    //
    public RaceRenderAllOutput(ISpriteChanger changeDict)
    {
        _changeDict = changeDict;
    }

    public IRaceRenderOutput NewSprite(SpriteType spriteType, int layer)
    {
        IRaceRenderOutput sprite = _changeDict.ChangeSprite(spriteType);
        sprite.Layer(layer);
        return sprite;
    }

    public IRaceRenderOutput ChangeSprite(SpriteType spriteType) => _changeDict.ChangeSprite(spriteType);
}