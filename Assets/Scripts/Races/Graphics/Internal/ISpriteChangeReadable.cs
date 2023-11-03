#region

using System;
using UnityEngine;

#endregion

internal interface ISpriteChangeReadable
{
    Sprite SpriteVal { get; }
    Vector2 Offset { get; }
    int? LayerVal { get; }
    
    ColorSwapPalette Palette { get; }
    Color? Color { get; }
}