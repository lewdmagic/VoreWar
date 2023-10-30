using System;
using System.Collections.Generic;
using UnityEngine;

internal class SpriteChangeDict : ISpriteChanger
{
    internal readonly Dictionary<SpriteType, RaceRenderOutput> ReusedChangesDict = new Dictionary<SpriteType, RaceRenderOutput>();
    
    public IRaceRenderOutput ChangeSprite(SpriteType spriteType)
    {
        if (!ReusedChangesDict.TryGetValue(spriteType, out var raceRenderOutput))
        {
            raceRenderOutput = new RaceRenderOutput();
            ReusedChangesDict.Add(spriteType, raceRenderOutput);
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
    public Sprite Sprite { get; private set; }
    public Vector2 Offset { get; private set; } = new Vector2(0, 0);

    public Vector3? GameObjectLocalScale { get; private set; }
    public bool? GameObjectActive { get; private set; }
    public Tuple<SpriteType, bool> SetParentData { get; private set; }

    public int? Layer { get; private set; }
    public bool? Hide { get; private set; }
        
    public Color? Color { get; private set; }
    public ColorSwapPalette Palette { get; private set; }
        
    IRaceRenderOutput IRaceRenderOutput.Sprite(Sprite sprite)
    {
        Sprite = sprite;
        return this;
    }

    public IRaceRenderOutput AddOffset(float x, float y)
    {
        Offset = new Vector2(Offset.x + x, Offset.y + y);
        return this;
    }
        
    public IRaceRenderOutput AddOffset(Vector2 offset)
    {
        Offset = offset;
        return this;
    }

    public IRaceRenderOutput SetOffset(float x, float y)
    {
        Offset = new Vector2(x, y);
        return this;
    }

    public IRaceRenderOutput SetOffset(Vector2 offset)
    {
        Offset = offset;
        return this;
    }

    IRaceRenderOutput IRaceRenderOutput.Layer(int layer)
    {
        Layer = layer;
        return this;
    }
        
    IRaceRenderOutput IRaceRenderOutput.Coloring(Color? color)
    {
        Color = color;
        Palette = null;
        return this;
    }

    IRaceRenderOutput IRaceRenderOutput.Coloring(ColorSwapPalette palette)
    {
        Palette = palette;
        Color = null;
        return this;
    }

    public IRaceRenderOutput SetHide(bool hide)
    {
        Hide = hide;
        return this;
    }
    
    public IRaceRenderOutput SetTransformParent(SpriteType parent, bool worldPositionStays)
    {
        SetParentData = Tuple.Create(parent, worldPositionStays);
        return this;
    }
    
    public IRaceRenderOutput SetActive(bool active)
    {
        GameObjectActive = active;
        return this;
    }
    
    public IRaceRenderOutput SetLocalScale(Vector3 localScale)
    {
        GameObjectLocalScale = localScale;
        return this;
    }
}