#region

using System;
using UnityEngine;

#endregion

internal interface ISpriteChangeReadable
{
    Sprite Sprite { get; }
    Vector2 Offset { get; }
    int? Layer { get; }
    
    ColorSwapPalette Palette { get; }
    Color? Color { get; }
}