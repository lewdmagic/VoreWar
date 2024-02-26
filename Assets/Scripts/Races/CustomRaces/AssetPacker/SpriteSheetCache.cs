#region

using System;

#endregion

namespace DaVikingCode.AssetPacker
{
    [Serializable]
    public class SpriteSheetCache
    {
        public TextureAsset[] Assets;

        public SpriteSheetCache(TextureAsset[] assets)
        {
            this.Assets = assets;
        }
    }

    [Serializable]
    public class TextureAsset
    {
        public string TextureFileName;
        public SpriteInfo[] SpriteInfos;

        public TextureAsset(string textureFileName, SpriteInfo[] spriteInfos)
        {
            this.TextureFileName = textureFileName;
            this.SpriteInfos = spriteInfos;
        }
    }

    [Serializable]
    public class SpriteInfo
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public string Name;
        public float PixelsPerUnit;

        public SpriteInfo(int x, int y, int width, int height, string name, float pixelsPerUnit)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Name = name;
            this.PixelsPerUnit = pixelsPerUnit;
        }
    }
}