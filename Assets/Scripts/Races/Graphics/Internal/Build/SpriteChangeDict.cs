using System;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChangeDict : ISpriteChanger
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
    public Sprite SpriteVal { get; private set; }
    public Vector2 Offset { get; private set; } = new Vector2(0, 0);

    public Vector3? GameObjectLocalScale { get; private set; }
    public bool? GameObjectActive { get; private set; }
    public Tuple<SpriteType, bool> SetParentData { get; private set; }

    public int? LayerVal { get; private set; }
    public bool? Hide { get; private set; }
        
    public Color? Color { get; private set; }
    public ColorSwapPalette Palette { get; private set; }
        
    public IRaceRenderOutput Sprite(Sprite sprite)
    {
        SpriteVal = sprite;
        return this;
    }
    
    public IRaceRenderOutput Sprite(string id, bool returnNull = false)
    {
        SpriteVal = State.SpriteManager.GetSprite(id, returnNull);
        return this;
    }
    
    public IRaceRenderOutput Sprite(string id, string word1, bool returnNull = false)
    {
        SpriteVal = State.SpriteManager.GetSprite($"{id}_{word1}", returnNull);
        return this;
    }
    
    public IRaceRenderOutput Sprite(string id, string word1, string word2, bool returnNull = false)
    {
        SpriteVal = State.SpriteManager.GetSprite($"{id}_{word1}_{word2}", returnNull);
        return this;
    }
    
    public IRaceRenderOutput Sprite(string id, string word1, string word2, string word3, bool returnNull = false)
    {
        SpriteVal = State.SpriteManager.GetSprite($"{id}_{word1}_{word2}_{word3}", returnNull);
        return this;
    }
    
    public IRaceRenderOutput Sprite(string id, string word1, string word2, string word3, string word4, bool returnNull = false)
    {
        SpriteVal = State.SpriteManager.GetSprite($"{id}_{word1}_{word2}_{word3}_{word4}", returnNull);
        return this;
    }
    
    public IRaceRenderOutput Sprite(string id, string word1, string word2, string word3, string word4, string word5, bool returnNull = false)
    {
        SpriteVal = State.SpriteManager.GetSprite($"{id}_{word1}_{word2}_{word3}_{word4}_{word5}", returnNull);
        return this;
    }

    public IRaceRenderOutput Sprite0(string id, int number, bool returnNull = false)
    {
        SpriteVal = State.SpriteManager.GetSprite0(id, number, returnNull);
        return this;
    }

    public IRaceRenderOutput Sprite0(string id, string word1, int number, bool returnNull = false)
    {
        SpriteVal = State.SpriteManager.GetSprite0($"{id}_{word1}", number, returnNull);
        return this;
    }
    public IRaceRenderOutput Sprite0(string id, string word1, string word2, int number, bool returnNull = false)
    {
        SpriteVal = State.SpriteManager.GetSprite0($"{id}_{word1}_{word2}", number, returnNull);
        return this;
    }
    public IRaceRenderOutput Sprite0(string id, string word1, string word2, string word3, int number, bool returnNull = false)
    {
        SpriteVal = State.SpriteManager.GetSprite0($"{id}_{word1}_{word2}_{word3}", number, returnNull);
        return this;
    }
    public IRaceRenderOutput Sprite0(string id, string word1, string word2, string word3, string word4, int number, bool returnNull = false)
    {
        SpriteVal = State.SpriteManager.GetSprite0($"{id}_{word1}_{word2}_{word3}_{word4}", number, returnNull);
        return this;
    }
    
    public IRaceRenderOutput Sprite0(string id, string word1, string word2, string word3, string word4, string word5, int number, bool returnNull = false)
    {
        SpriteVal = State.SpriteManager.GetSprite0($"{id}_{word1}_{word2}_{word3}_{word4}_{word5}", number, returnNull);
        return this;
    }

    public IRaceRenderOutput Sprite(string id, int number, bool returnNull = false)
    {
        SpriteVal = State.SpriteManager.GetSprite(id, number, returnNull);
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

    public IRaceRenderOutput Layer(int layer)
    {
        LayerVal = layer;
        return this;
    }
        
    public IRaceRenderOutput Coloring(Color? color)
    {
        Color = color;
        Palette = null;
        return this;
    }

    public IRaceRenderOutput Coloring(ColorSwapPalette palette)
    {
        Palette = palette;
        Color = null;
        return this;
    }

    public IRaceRenderOutput Coloring(SwapType swap, int index)
    {
        Coloring(ColorPaletteMap.GetPalette(swap, index));
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