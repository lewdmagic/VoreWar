#region

using UnityEngine;

#endregion

internal interface IRaceRenderOutput
{
    IRaceRenderOutput Sprite(Sprite hide);
    IRaceRenderOutput Layer(int layer);
    IRaceRenderOutput AddOffset(float x, float y);
    IRaceRenderOutput AddOffset(Vector2 offset);
    IRaceRenderOutput SetOffset(float x, float y);
    IRaceRenderOutput SetOffset(Vector2 offset);
    IRaceRenderOutput Coloring(Color? colorFunc);
    IRaceRenderOutput Coloring(ColorSwapPalette paletteFunc);
    
    IRaceRenderOutput SetHide(bool hide);

    IRaceRenderOutput SetTransformParent(SpriteType parent, bool worldPositionStays);
    IRaceRenderOutput SetActive(bool active);
    IRaceRenderOutput SetLocalScale(Vector3 localScale);
}