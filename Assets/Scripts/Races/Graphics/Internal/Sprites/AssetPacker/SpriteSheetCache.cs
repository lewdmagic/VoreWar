#region

using System;

#endregion

namespace DaVikingCode.AssetPacker
{
    [Serializable]
    public class SpriteSheetCache
    {
        public TextureAsset[] assets;

        public SpriteSheetCache(TextureAsset[] assets)
        {
            this.assets = assets;
        }
    }
    
    [Serializable]
    public class TextureAsset
    {
        public string textureFileName;
        public SpriteInfo[] spriteInfos;

        public TextureAsset(string textureFileName, SpriteInfo[] spriteInfos)
        {
            this.textureFileName = textureFileName;
            this.spriteInfos = spriteInfos;
        }
    }
    
    [Serializable]
    public class SpriteInfo
    {
        public int x;
        public int y;
        public int width;
        public int height;
        public string name;
        public float pixelsPerUnit;

        public SpriteInfo(int x, int y, int width, int height, string name, float pixelsPerUnit)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.name = name;
            this.pixelsPerUnit = pixelsPerUnit;
        }
    }
}