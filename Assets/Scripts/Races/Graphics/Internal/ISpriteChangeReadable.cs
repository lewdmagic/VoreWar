#region

using System;
using UnityEngine;

#endregion

internal interface ISpriteChangeReadable
{
    Sprite _Sprite { get; }
    Vector2 _Offset { get; }
    int? _Layer { get; }
    
    ColorSwapPalette _Palette { get; }
    Color? _Color { get; }
}