using System;
using System.Collections.Generic;
using UnityEngine;

internal class SpriteChangeDict : ISpriteChanger
{
    internal Dictionary<SpriteType, RaceRenderOutput> reusedChangesDict = new Dictionary<SpriteType, RaceRenderOutput>();
    
    public IRaceRenderOutput changeSprite(SpriteType spriteType)
    {
        RaceRenderOutput raceRenderOutput;
        if (!reusedChangesDict.TryGetValue(spriteType, out raceRenderOutput))
        {
            raceRenderOutput = new RaceRenderOutput();
            reusedChangesDict.Add(spriteType, raceRenderOutput);
        }

        return raceRenderOutput;
    }    
    
    // public ISpriteChange changeSprites(IEnumerable<SpriteType> spriteTypes)
    // {
    //     SpriteChange spriteChange;
    //     if (!reusedChangesDict.TryGetValue(spriteType, out spriteChange))
    //     {
    //         spriteChange = new SpriteChange();
    //         reusedChangesDict.Add(spriteType, spriteChange);
    //     }
    //
    //     return spriteChange;
    // }
    //
    

}


internal class RaceRenderOutput : IRaceRenderOutput, ISpriteChangeReadable
{
    public Sprite _Sprite { get; private set; }
    public Vector2 _Offset { get; private set; } = new Vector2(0, 0);

    public Vector3? _gameObjectLocalScale { get; private set; }
    public bool? _gameObjectActive { get; private set; }
    public Tuple<SpriteType, bool> _SetParentData { get; private set; }

    public int? _Layer { get; private set; }
    public bool? _Hide { get; private set; }
        
    public Color? _Color { get; private set; }
    public ColorSwapPalette _Palette { get; private set; }
        
    public IRaceRenderOutput Sprite(Sprite sprite)
    {
        _Sprite = sprite;
        return this;
    }

    public IRaceRenderOutput AddOffset(float x, float y)
    {
        _Offset = new Vector2(_Offset.x + x, _Offset.y + y);
        return this;
    }
        
    public IRaceRenderOutput AddOffset(Vector2 offset)
    {
        _Offset = offset;
        return this;
    }

    public IRaceRenderOutput SetOffset(float x, float y)
    {
        _Offset = new Vector2(x, y);
        return this;
    }

    public IRaceRenderOutput SetOffset(Vector2 offset)
    {
        _Offset = offset;
        return this;
    }

    public IRaceRenderOutput Layer(int layer)
    {
        _Layer = layer;
        return this;
    }
        
    public IRaceRenderOutput Coloring(Color? color)
    {
        _Color = color;
        _Palette = null;
        return this;
    }

    public IRaceRenderOutput Coloring(ColorSwapPalette palette)
    {
        _Palette = palette;
        _Color = null;
        return this;
    }

    public IRaceRenderOutput SetHide(bool hide)
    {
        _Hide = hide;
        return this;
    }
    
    public IRaceRenderOutput SetTransformParent(SpriteType parent, bool worldPositionStays)
    {
        _SetParentData = Tuple.Create(parent, worldPositionStays);
        return this;
    }
    
    public IRaceRenderOutput SetActive(bool active)
    {
        _gameObjectActive = active;
        return this;
    }
    
    public IRaceRenderOutput SetLocalScale(Vector3 localScale)
    {
        _gameObjectLocalScale = localScale;
        return this;
    }
}