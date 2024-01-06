#region

using UnityEngine;

#endregion

public interface IRaceRenderAllOutput
{
    IRaceRenderOutput NewSprite(SpriteType spriteType, int layer);
    //IRaceRenderOutput ChangeSprite(SpriteType spriteType);
    IRaceRenderOutput ChangeSprite(SpriteType spriteType);
}

public interface IRaceRenderAllOutput<out T> : IRaceRenderAllOutput where T : IParameters
{
    T Params { get; }
}


public class RaceRenderAllOutput<T> : IRaceRenderAllOutput<T> where T : IParameters
{
    
    private readonly SpriteChangeDict _changeDict;
    
    //
    public RaceRenderAllOutput(SpriteChangeDict changeDict, T parameters)
    {
        _changeDict = changeDict;
        Params = parameters;
    }

    public IRaceRenderOutput NewSprite(SpriteType spriteType, int layer)
    {
        IRaceRenderOutput sprite = _changeDict.ChangeSprite(spriteType);
        sprite.Layer(layer);
        return sprite;
    }
    

    public T Params { get; }

    public IRaceRenderOutput ChangeSprite(SpriteType spriteType) => _changeDict.ChangeSprite(spriteType);
    
}

//  private readonly SpriteTypeIndexed<SingleRenderFunc<T>> RaceSpriteSet = new SpriteTypeIndexed<SingleRenderFunc<T>>();