using System;
using UnityEngine;

internal class RaceRenderOutput : IRaceRenderOutput
{
    private readonly ISpriteCollection _spriteCollection;

    internal RaceRenderOutput(ISpriteCollection spriteCollection)
    {
        _spriteCollection = spriteCollection;
    }

    public Sprite SpriteVal { get; private set; }
    public Vector2 Offset { get; private set; } = new Vector2(0, 0);

    public Vector3? GameObjectLocalScale { get; private set; }
    public bool? GameObjectActive { get; private set; }
    public Tuple<SpriteType, bool> SetParentData { get; private set; }

    public int? LayerVal { get; private set; }
    public bool? Hide { get; private set; }

    public Color? Color { get; private set; }
    public ColorSwapPalette Palette { get; private set; }

    private Sprite GetSpriteInternal(string id, bool returnNull)
    {
        //return State.SpriteManager.GetSprite(id, returnNull);
        Sprite sprite = _spriteCollection.GetSprite(id);
        if (sprite == null && !returnNull)
        {
            throw new Exception($"Sprite {id} not found in {_spriteCollection.Description}");
        }

        return sprite;
    }

    public Sprite GetSpriteInternal(string id, int number, bool returnNull = false)
    {
        string usedId = id + "_" + number.ToString("D3");
        return GetSpriteInternal(usedId, returnNull);
    }

    public Sprite GetSprite0Internal(string id, int number, bool returnNull = false)
    {
        return GetSpriteInternal(id, number + 1, returnNull);
    }


    public IRaceRenderOutput Sprite(Sprite sprite)
    {
        SpriteVal = sprite;
        return this;
    }

    public IRaceRenderOutput Sprite(string id, bool returnNull = false)
    {
        SpriteVal = GetSpriteInternal(id, returnNull);
        return this;
    }

    public IRaceRenderOutput Sprite(string id, string word1, bool returnNull = false)
    {
        SpriteVal = GetSpriteInternal($"{id}_{word1}", returnNull);
        return this;
    }

    public IRaceRenderOutput Sprite(string id, string word1, string word2, bool returnNull = false)
    {
        SpriteVal = GetSpriteInternal($"{id}_{word1}_{word2}", returnNull);
        return this;
    }

    public IRaceRenderOutput Sprite(string id, string word1, string word2, string word3, bool returnNull = false)
    {
        SpriteVal = GetSpriteInternal($"{id}_{word1}_{word2}_{word3}", returnNull);
        return this;
    }

    public IRaceRenderOutput Sprite(string id, string word1, string word2, string word3, string word4, bool returnNull = false)
    {
        SpriteVal = GetSpriteInternal($"{id}_{word1}_{word2}_{word3}_{word4}", returnNull);
        return this;
    }

    public IRaceRenderOutput Sprite(string id, string word1, string word2, string word3, string word4, string word5, bool returnNull = false)
    {
        SpriteVal = GetSpriteInternal($"{id}_{word1}_{word2}_{word3}_{word4}_{word5}", returnNull);
        return this;
    }

    public IRaceRenderOutput Sprite0(string id, int number, bool returnNull = false)
    {
        SpriteVal = GetSprite0Internal(id, number, returnNull);
        return this;
    }

    public IRaceRenderOutput Sprite0(string id, string word1, int number, bool returnNull = false)
    {
        SpriteVal = GetSprite0Internal($"{id}_{word1}", number, returnNull);
        return this;
    }

    public IRaceRenderOutput Sprite0(string id, string word1, string word2, int number, bool returnNull = false)
    {
        SpriteVal = GetSprite0Internal($"{id}_{word1}_{word2}", number, returnNull);
        return this;
    }

    public IRaceRenderOutput Sprite0(string id, string word1, string word2, string word3, int number, bool returnNull = false)
    {
        SpriteVal = GetSprite0Internal($"{id}_{word1}_{word2}_{word3}", number, returnNull);
        return this;
    }

    public IRaceRenderOutput Sprite0(string id, string word1, string word2, string word3, string word4, int number, bool returnNull = false)
    {
        SpriteVal = GetSprite0Internal($"{id}_{word1}_{word2}_{word3}_{word4}", number, returnNull);
        return this;
    }

    public IRaceRenderOutput Sprite0(string id, string word1, string word2, string word3, string word4, string word5, int number, bool returnNull = false)
    {
        SpriteVal = GetSprite0Internal($"{id}_{word1}_{word2}_{word3}_{word4}_{word5}", number, returnNull);
        return this;
    }

    public IRaceRenderOutput Sprite(string id, int number, bool returnNull = false)
    {
        SpriteVal = GetSpriteInternal(id, number, returnNull);
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