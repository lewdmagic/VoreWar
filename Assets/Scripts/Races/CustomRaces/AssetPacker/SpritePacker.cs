using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DaVikingCode.AssetPacker;
using DaVikingCode.RectanglePacking;
using UnityEngine;


internal class SpriteToLoad
{
    internal readonly string Key;
    internal readonly string Path;
    internal readonly long LastChangeTime;

    public SpriteToLoad(string key, string path, long lastChangeTime)
    {
        Key = key;
        Path = path;
        LastChangeTime = lastChangeTime;
    }
}


public static class SpritePacker
{
    private const bool LogStats = false;
    
    private const int TextureSize = 2048;
    private const float PixelsPerUnit = 160f;

    private static List<Texture2D> _texture2Ds = new List<Texture2D>();
    
    internal static (string, Sprite)[] LoadOrUpdateTextures(IEnumerable<SpriteToLoad> toLoads)
    {
        List<Texture2D> toRemove = new List<Texture2D>(_texture2Ds);
        _texture2Ds.Clear();
        
        // TODO a lot of redundant and inefficient conversions
        List<TextureToPack> texturesToPack = toLoads.Select(it => { return new TextureToPack(it.Path, it.Key); }).ToList();

        var packResult = PackTextures(texturesToPack, TextureSize, PixelsPerUnit);

        List<(string, Sprite)> result = new List<(string, Sprite)>();

        foreach (Entry entry in packResult)
        {
            foreach (var dictEntry in entry.Dict)
            {
                result.Add((dictEntry.Key, dictEntry.Value));
            }
        }        
        
        foreach (var texture in toRemove)
        {
            Object.Destroy(texture);
        }

        return result.ToArray();
    }
    
    public static Texture2D LoadPNG(string filePath) {
        Texture2D tex = null;

        if (File.Exists(filePath)) 	{
            byte[] fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }


    private class TextureEntry
    {
        internal Texture2D Texture;
        internal string Image;
        internal Rect Rect;

        public TextureEntry(Texture2D texture, string image, Rect rect)
        {
            Texture = texture;
            Image = image;
            Rect = rect;
        }
    }
    
    
    internal static List<Entry> PackTextures(List<TextureToPack> itemsToRaster, int textureSize, float pixelsPerUnit)
    {
        List<Entry> entries = new List<Entry>();
        Dictionary<string, TextureEntry> textureEntryMap = new Dictionary<string, TextureEntry>();
        
        var watch5 = System.Diagnostics.Stopwatch.StartNew();
        // for (int i = 0; i < itemsToRaster.Count; i++)
        // {
        //     TextureToPack itemToRaster = itemsToRaster[i];
        //     
        //     var texture = LoadPNG(itemToRaster.File);
        //     TextureEntry textureEntry = new TextureEntry(texture, itemToRaster.Id, new Rect(0, 0, texture.width, texture.height));
        //     textureEntryMap[i] = textureEntry;
        // }
        
        watch5.Stop();
        if (LogStats) Debug.Log($"{watch5.ElapsedMilliseconds}");
        
        var watch8 = System.Diagnostics.Stopwatch.StartNew();
        
        ConcurrentBag<(TextureToPack, byte[])> resultCollection = new ConcurrentBag<(TextureToPack, byte[])>();
        Parallel.ForEach(itemsToRaster, item =>
        {
            byte[] bytes = File.ReadAllBytes(item.File);
            resultCollection.Add((item, bytes));
        });
        watch8.Stop();
        if (LogStats) Debug.Log($"{watch8.ElapsedMilliseconds}");

        var watch9 = System.Diagnostics.Stopwatch.StartNew();
        var res = resultCollection.ToList();
        for (int i = 0; i < res.Count; i++)
        {
            TextureToPack itemToRaster = res[i].Item1;
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(res[i].Item2); //..this will auto-resize the texture dimensions.
            TextureEntry textureEntry = new TextureEntry(texture, itemToRaster.Id, new Rect(0, 0, texture.width, texture.height));
            textureEntryMap[itemToRaster.Id] = textureEntry;
        }
        watch9.Stop();
        if (LogStats) Debug.Log($"{watch9.ElapsedMilliseconds}");
        
        int width = textureSize;
        int height = textureSize;

        var watch1 = System.Diagnostics.Stopwatch.StartNew();
        while (textureEntryMap.Count > 0)
        {
            var spriteInfos = MakeSpriteInfos(width, height, pixelsPerUnit, textureEntryMap);
            Entry entry = MakeEntry(pixelsPerUnit, width, height, spriteInfos, textureEntryMap);
            entries.Add(entry);
                
            foreach (var info in spriteInfos)
            {
                textureEntryMap.Remove(info.Name);
            }
        }

        watch1.Stop();
        if (LogStats) Debug.Log(watch1.ElapsedMilliseconds);

        return entries;
    }

    private static Entry MakeEntry(float pixelsPerUnit, int width, int height, List<SpriteInfo> spriteInfos, Dictionary<string, TextureEntry> textureEntryMap)
    {
        var texture = new Texture2D(width, height, TextureFormat.ARGB32, false);
        _texture2Ds.Add(texture);
        texture.filterMode = FilterMode.Point;
        
        Dictionary<string, Sprite> mSprites = new Dictionary<string, Sprite>();
        foreach (var spriteInfo in spriteInfos)
        {
            Texture2D spriteTexture = textureEntryMap[spriteInfo.Name].Texture;
            texture.SetPixels32(spriteInfo.X, spriteInfo.Y, spriteInfo.Width, spriteInfo.Height, spriteTexture.GetPixels32());
            mSprites.Add(spriteInfo.Name, Sprite.Create(texture, new Rect(spriteInfo.X, spriteInfo.Y, spriteInfo.Width, spriteInfo.Height), new Vector2(0.5f, 0.5f), pixelsPerUnit, 0, SpriteMeshType.FullRect));
            Object.Destroy(spriteTexture);
        }

        var watch50 = System.Diagnostics.Stopwatch.StartNew();

        texture.Apply();

        //then Save To Disk as PNG
        //byte[] bytes = texture.EncodeToPNG();
        //File.WriteAllBytes("UserData/texture" + textureEntryMap.Count + ".png", bytes);

        watch50.Stop();
        if (LogStats) Debug.Log(watch50.ElapsedMilliseconds);
        
        return new Entry(mSprites, texture, spriteInfos.ToArray());
    }

    private static List<SpriteInfo> MakeSpriteInfos(int width, int height, float pixelsPerUnit, Dictionary<string, TextureEntry> textureEntryMap)
    {
        var spriteInfos = new List<SpriteInfo>();
        const int padding = 1;

        var packer = new RectanglePacker(width, height, padding);
        foreach (var (key, entry) in textureEntryMap)
        {
            packer.InsertRectangle((int)entry.Rect.width, (int)entry.Rect.height, key);
        }

        packer.PackRectangles();
        for (var j = 0; j < packer.RectangleCount; j++)
        {
            IntegerRectangle rect = packer.GetRectangle(j);
            var id = packer.GetRectangleId(j);
            var spriteInfo = new SpriteInfo(rect.X, rect.Y, rect.Width, rect.Height, id, pixelsPerUnit);
            spriteInfos.Add(spriteInfo);    
        }

        return spriteInfos;
    }
    
    
}