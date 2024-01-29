#region

using UnityEngine;

#endregion

public interface IRaceRenderAllOutput
{
    IRaceRenderOutput NewSprite(SpriteType spriteType, int layer);
    //IRaceRenderOutput ChangeSprite(SpriteType spriteType);
    IRaceRenderOutput ChangeSprite(SpriteType spriteType);
}


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

//  private readonly SpriteTypeIndexed<SingleRenderFunc<T>> RaceSpriteSet = new SpriteTypeIndexed<SingleRenderFunc<T>>();