using System.Collections.Generic;
using DaVikingCode.AssetPacker;
using UnityEngine;

public class Entry
{
    internal Dictionary<string, Sprite> Dict;
    internal Texture2D Texture2D;
    internal SpriteInfo[] SpriteInfos;

    public Entry(Dictionary<string, Sprite> dict, Texture2D texture2D, SpriteInfo[] spriteInfos)
    {
        Dict = dict;
        Texture2D = texture2D;
        SpriteInfos = spriteInfos;
    }
}