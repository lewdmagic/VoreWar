using UnityEngine;

public interface IRaceRenderOutput
{
    IRaceRenderOutput Sprite(Sprite hide);

    IRaceRenderOutput Sprite(string id, bool returnNull = false);
    IRaceRenderOutput Sprite(string id, string word1, bool returnNull = false);
    IRaceRenderOutput Sprite(string id, string word1, string word2, bool returnNull = false);
    IRaceRenderOutput Sprite(string id, string word1, string word2, string word3, bool returnNull = false);
    IRaceRenderOutput Sprite(string id, string word1, string word2, string word3, string word4, bool returnNull = false);
    IRaceRenderOutput Sprite(string id, string word1, string word2, string word3, string word4, string word5, bool returnNull = false);


    IRaceRenderOutput Sprite(string id, int number, bool returnNull = false);


    IRaceRenderOutput Sprite0(string id, int number, bool returnNull = false);
    IRaceRenderOutput Sprite0(string id, string word1, int number, bool returnNull = false);
    IRaceRenderOutput Sprite0(string id, string word1, string word2, int number, bool returnNull = false);
    IRaceRenderOutput Sprite0(string id, string word1, string word2, string word3, int number, bool returnNull = false);
    IRaceRenderOutput Sprite0(string id, string word1, string word2, string word3, string word4, int number, bool returnNull = false);
    IRaceRenderOutput Sprite0(string id, string word1, string word2, string word3, string word4, string word5, int number, bool returnNull = false);

    IRaceRenderOutput Layer(int layer);
    IRaceRenderOutput AddOffset(float x, float y);
    IRaceRenderOutput AddOffset(Vector2 offset);
    IRaceRenderOutput SetOffset(float x, float y);
    IRaceRenderOutput SetOffset(Vector2 offset);
    IRaceRenderOutput Coloring(Color? colorFunc);
    IRaceRenderOutput Coloring(ColorSwapPalette paletteFunc);
    IRaceRenderOutput Coloring(SwapType swap, int index);

    IRaceRenderOutput SetHide(bool hide);

    IRaceRenderOutput SetTransformParent(SpriteType parent, bool worldPositionStays);
    IRaceRenderOutput SetActive(bool active);
    IRaceRenderOutput SetLocalScale(Vector3 localScale);
}