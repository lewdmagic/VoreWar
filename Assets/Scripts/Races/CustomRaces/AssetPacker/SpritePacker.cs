using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DaVikingCode.AssetPacker;
using DaVikingCode.RectanglePacking;
using UnityEngine;
using UnityEngine.Networking;


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
    private const int TextureSize = 2048;
    private const float PixelsPerUnit = 160f;

    internal static (string, Sprite)[] LoadOrUpdateTextures(IEnumerable<SpriteToLoad> toLoads)
    {
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

        return result.ToArray();
    }

    // private void aaa()
    // {
    //     
    //     using (UnityWebRequest www = UnityWebRequest.GetTexture("http://www.my-server.com/image.png"))
    //     {
    //         yield return www.Se();
    //         if (www.isError)
    //         {
    //             Debug.Log(www.error);
    //         }
    //         else
    //         {
    //             m_myTexture = DownloadHandlerTexture.GetContent(www);
    //         }
    //     }
    //     
    // }
    
    public static Texture2D LoadPNG(string filePath) {
        Texture2D tex = null;

        if (File.Exists(filePath)) 	{
            byte[] fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
    
    public static Texture2D LoadPNG2(string filePath) {
        Texture2D tex = null;

        if (File.Exists(filePath)) 	{
            byte[] fileData = File.ReadAllBytes(filePath);
            //tex = new Texture2D(2, 2);
            //tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }


    internal class TextureEntry
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
        Dictionary<int, TextureEntry> textureEntryMap = new Dictionary<int, TextureEntry>();
        
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
        Debug.Log($"{watch5.ElapsedMilliseconds}");
        
        var watch8 = System.Diagnostics.Stopwatch.StartNew();
        
        ConcurrentBag<(TextureToPack, byte[])> resultCollection = new ConcurrentBag<(TextureToPack, byte[])>();
        Parallel.ForEach(itemsToRaster, item =>
        {
            byte[] bytes = File.ReadAllBytes(item.File);
            resultCollection.Add((item, bytes));
        });
        watch8.Stop();
        Debug.Log($"{watch8.ElapsedMilliseconds}");

        var watch9 = System.Diagnostics.Stopwatch.StartNew();
        var res = resultCollection.ToList();
        for (int i = 0; i < res.Count; i++)
        {
            TextureToPack itemToRaster = res[i].Item1;
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(res[i].Item2); //..this will auto-resize the texture dimensions.
            TextureEntry textureEntry = new TextureEntry(texture, itemToRaster.Id, new Rect(0, 0, texture.width, texture.height));
            textureEntryMap[i] = textureEntry;
        }
        watch9.Stop();
        Debug.Log($"{watch9.ElapsedMilliseconds}");
        
        

        var watch1 = System.Diagnostics.Stopwatch.StartNew();
        while (textureEntryMap.Count > 0)
        {
            var (entry, toRemove) = NewMethod(textureSize, pixelsPerUnit, textureEntryMap);
            if (entry != null) entries.Add(entry);
            
            foreach (int i in toRemove)
            {
                textureEntryMap.Remove(i);
            }
        }

        watch1.Stop();
        Debug.Log(watch1.ElapsedMilliseconds);

        return entries;
    }

    private static (Entry, List<int>) NewMethod(int textureSize, float pixelsPerUnit, Dictionary<int, TextureEntry> textureEntryMap)
    {
        const int padding = 1;
        var watch20 = System.Diagnostics.Stopwatch.StartNew();
        Debug.Log($"Making tex, rects left: {textureEntryMap.Count}");
        var texture = new Texture2D(textureSize, textureSize, TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Point;
        //var fillColor = texture.GetPixels32();
        // I believe this isnt needed.
        /*
            var g = 0;
            for (; g < fillColor.Length; ++g)
            {
                //fillColor[g] = Color.clear;
            }
            */

        watch20.Stop();
        var watch30 = System.Diagnostics.Stopwatch.StartNew();

        var packer = new RectanglePacker(texture.width, texture.height, padding);
        foreach (var (key, entry) in textureEntryMap)
        {
            packer.InsertRectangle((int)entry.Rect.width, (int)entry.Rect.height, key);
        }

        packer.PackRectangles();


        watch30.Stop();
        Debug.Log(watch30.ElapsedMilliseconds);

        if (packer.RectangleCount > 0)
        {
            var watch35 = System.Diagnostics.Stopwatch.StartNew();
            //texture.SetPixels32(fillColor);
            watch35.Stop();
            Debug.Log(watch35.ElapsedMilliseconds);
            var spriteInfos = new List<SpriteInfo>();
            var watch40 = System.Diagnostics.Stopwatch.StartNew();

            var toRemove = new List<int>();
            
            for (var j = 0; j < packer.RectangleCount; j++)
            {
                IntegerRectangle rect = packer.GetRectangle(j);
                var id = packer.GetRectangleId(j);
                texture.SetPixels32(rect.X, rect.Y, rect.Width, rect.Height, textureEntryMap[id].Texture.GetPixels32());

                var spriteInfo = new SpriteInfo(
                    x: rect.X,
                    y: rect.Y,
                    width: rect.Width,
                    height: rect.Height,
                    name: textureEntryMap[id].Image,
                    pixelsPerUnit: pixelsPerUnit
                );

                spriteInfos.Add(spriteInfo);
                toRemove.Add(id);
            }

            watch40.Stop();
            Debug.Log(watch40.ElapsedMilliseconds);

            var watch50 = System.Diagnostics.Stopwatch.StartNew();

            texture.Apply();

            //then Save To Disk as PNG
            //byte[] bytes = texture.EncodeToPNG();
            //File.WriteAllBytes("UserData/texture" + textureEntryMap.Count + ".png", bytes);

            Dictionary<string, Sprite> mSprites = new Dictionary<string, Sprite>();
            foreach (var spriteInfo in spriteInfos)
            {
                mSprites.Add(spriteInfo.Name, Sprite.Create(texture, new Rect(spriteInfo.X, spriteInfo.Y, spriteInfo.Width, spriteInfo.Height), new Vector2(0.5f, 0.5f), pixelsPerUnit, 0, SpriteMeshType.FullRect));
            }

            watch50.Stop();
            Debug.Log(watch50.ElapsedMilliseconds);
            return (new Entry(mSprites, texture, spriteInfos.ToArray()), toRemove);
        }

        return (null, null);
    }
    
    
    internal static List<Entry> PackTextures4(List<TextureToPack> itemsToRaster, int textureSize, float pixelsPerUnit)
    {
        List<Entry> entries = new List<Entry>();
        List<TextureEntry> textureEntries = new List<TextureEntry>();
        Dictionary<int, TextureEntry> textureEntryMap = new Dictionary<int, TextureEntry>();
        
        var watch5 = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < itemsToRaster.Count; i++)
        {
            TextureToPack itemToRaster = itemsToRaster[i];
            
            var texture = LoadPNG(itemToRaster.File);
            TextureEntry textureEntry = new TextureEntry(texture, itemToRaster.Id, new Rect(0, 0, texture.width, texture.height));
            textureEntries.Add(textureEntry);
            textureEntryMap[i] = textureEntry;
        }
        
        watch5.Stop();
        Debug.Log($"{watch5.ElapsedMilliseconds}");
        
        var watch8 = System.Diagnostics.Stopwatch.StartNew();
        
        ConcurrentBag<string> resultCollection = new ConcurrentBag<string>();
        Parallel.ForEach(itemsToRaster, item =>
        {
            byte[] bytes = File.ReadAllBytes(item.File);
        });
        
        
        watch8.Stop();
        Debug.Log($"{watch8.ElapsedMilliseconds}");
        


        //Debug.Log("");
        



        const int padding = 1;
        var watch1 = System.Diagnostics.Stopwatch.StartNew();
        while (textureEntries.Count > 0)
        {
            
            
            var watch20 = System.Diagnostics.Stopwatch.StartNew();
            Debug.Log($"Making tex, rects left: {textureEntries.Count}");
            var texture = new Texture2D(textureSize, textureSize, TextureFormat.ARGB32, false);
            texture.filterMode = FilterMode.Point;
            //var fillColor = texture.GetPixels32();
            // I believe this isnt needed.
            /*
            var g = 0;
            for (; g < fillColor.Length; ++g)
            {
                //fillColor[g] = Color.clear;
            }
            */
            
            watch20.Stop();
            var watch30 = System.Diagnostics.Stopwatch.StartNew();
            
            var packer = new RectanglePacker(texture.width, texture.height, padding);
            for (var i = 0; i < textureEntries.Count; i++)
            {
                packer.InsertRectangle((int)textureEntries[i].Rect.width, (int)textureEntries[i].Rect.height, i);
            }
            packer.PackRectangles();
            
            
            watch30.Stop();
            Debug.Log(watch30.ElapsedMilliseconds);

            if (packer.RectangleCount > 0)
            {
                var watch35 = System.Diagnostics.Stopwatch.StartNew();
                //texture.SetPixels32(fillColor);
                watch35.Stop();
                Debug.Log(watch35.ElapsedMilliseconds);
                var spriteInfos = new List<SpriteInfo>();
                var garbageRect = new List<TextureEntry>();

                
                var watch40 = System.Diagnostics.Stopwatch.StartNew();
                
                for (var j = 0; j < packer.RectangleCount; j++)
                {
                    IntegerRectangle rect = packer.GetRectangle(j);
                    var index = packer.GetRectangleId(j);
                    texture.SetPixels32(rect.X, rect.Y, rect.Width, rect.Height, textureEntries[index].Texture.GetPixels32());
                    
                    var spriteInfo = new SpriteInfo(
                        x: rect.X,
                        y: rect.Y,
                        width: rect.Width,
                        height: rect.Height,
                        name: textureEntries[index].Image,
                        pixelsPerUnit: pixelsPerUnit
                    );

                    spriteInfos.Add(spriteInfo);
                    garbageRect.Add(textureEntries[index]);
                }

                watch40.Stop();
                Debug.Log(watch40.ElapsedMilliseconds);
                
                
                var watch50 = System.Diagnostics.Stopwatch.StartNew();
                foreach (var garbage in garbageRect)
                {
                    textureEntries.Remove(garbage);
                }

                texture.Apply();

                //then Save To Disk as PNG
                byte[] bytes = texture.EncodeToPNG();
                File.WriteAllBytes("UserData/texture" + textureEntries.Count + ".png", bytes);

                Dictionary<string, Sprite> mSprites = new Dictionary<string, Sprite>();
                foreach (var spriteInfo in spriteInfos)
                {
                    //mSprites.Add(spriteInfo.name, Sprite.Create(texture, new Rect(spriteInfo.x, spriteInfo.y, spriteInfo.width, spriteInfo.height), Vector2.zero, pixelsPerUnit, 0, SpriteMeshType.FullRect));
                    //mSprites.Add(spriteInfo.name, Sprite.Create(texture, new Rect(spriteInfo.x, spriteInfo.y, spriteInfo.width, spriteInfo.height), new Vector2(spriteInfo.width/2f, spriteInfo.height/2f), pixelsPerUnit, 0, SpriteMeshType.FullRect));
                    //mSprites.Add(spriteInfo.name, Sprite.Create(texture, new Rect(spriteInfo.x, spriteInfo.y, spriteInfo.width, spriteInfo.height), new Vector2(spriteInfo.width/2f, 0), pixelsPerUnit, 0, SpriteMeshType.FullRect));
                    mSprites.Add(spriteInfo.Name, Sprite.Create(texture, new Rect(spriteInfo.X, spriteInfo.Y, spriteInfo.Width, spriteInfo.Height), new Vector2(0.5f, 0.5f), pixelsPerUnit, 0, SpriteMeshType.FullRect));
                }

                entries.Add(new Entry(mSprites, texture, spriteInfos.ToArray()));
                watch50.Stop();
                Debug.Log(watch50.ElapsedMilliseconds);
            }
        }

        watch1.Stop();
        Debug.Log(watch1.ElapsedMilliseconds);

        return entries;
    }

    
    internal static List<Entry> PackTextures3(List<TextureToPack> itemsToRaster, int textureSize, float pixelsPerUnit)
    {
        List<Entry> entries = new List<Entry>();

        var textures = new List<Texture2D>();
        var images = new List<string>();
        var rectangles = new List<Rect>();
        
        var watch5 = System.Diagnostics.Stopwatch.StartNew();
        foreach (var itemToRaster in itemsToRaster)
        {
            var texture = LoadPNG(itemToRaster.File);
            
            textures.Add(texture);
            images.Add(itemToRaster.Id);
            
            if (texture.width > textureSize || texture.height > textureSize)
            {
                throw new Exception("A texture size is bigger than the sprite sheet size!");
            }

            rectangles.Add(new Rect(0, 0, texture.width, texture.height));
        }
        
        
        watch5.Stop();
        Debug.Log($"{watch5.ElapsedMilliseconds}");
        
        var watch8 = System.Diagnostics.Stopwatch.StartNew();
        
        ConcurrentBag<string> resultCollection = new ConcurrentBag<string>();
        Parallel.ForEach(itemsToRaster, item =>
        {
            byte[] bytes = File.ReadAllBytes(item.File);
            
        });
        
        
        watch8.Stop();
        Debug.Log($"{watch8.ElapsedMilliseconds}");
        


        //Debug.Log("");
        



        const int padding = 1;
        var watch1 = System.Diagnostics.Stopwatch.StartNew();
        while (rectangles.Count > 0)
        {
            
            
            var watch20 = System.Diagnostics.Stopwatch.StartNew();
            Debug.Log($"Making tex, rects left: {rectangles.Count}");
            var texture = new Texture2D(textureSize, textureSize, TextureFormat.ARGB32, false);
            texture.filterMode = FilterMode.Point;
            //var fillColor = texture.GetPixels32();
            // I believe this isnt needed.
            /*
            var g = 0;
            for (; g < fillColor.Length; ++g)
            {
                //fillColor[g] = Color.clear;
            }
            */
            
            watch20.Stop();
            //Debug.Log($"{watch20.ElapsedMilliseconds} _ {g}");

            
            var watch30 = System.Diagnostics.Stopwatch.StartNew();
            
            var packer = new RectanglePacker(texture.width, texture.height, padding);


            for (var i = 0; i < rectangles.Count; i++)
            {
                packer.InsertRectangle((int)rectangles[i].width, (int)rectangles[i].height, i);
            }


            packer.PackRectangles();
            
            
            watch30.Stop();
            Debug.Log(watch30.ElapsedMilliseconds);

            if (packer.RectangleCount > 0)
            {
                var watch35 = System.Diagnostics.Stopwatch.StartNew();
                //texture.SetPixels32(fillColor);
                watch35.Stop();
                Debug.Log(watch35.ElapsedMilliseconds);
                var spriteInfos = new List<SpriteInfo>();

                var garbageRect = new List<Rect>();
                var garabeTextures = new List<Texture2D>();
                var garbageImages = new List<string>();

                
                var watch40 = System.Diagnostics.Stopwatch.StartNew();


                int[] indexArr = new int[packer.RectangleCount];
                for (var j = 0; j < packer.RectangleCount; j++)
                {
                    indexArr[j] = j;
                }
                
                foreach (int j in indexArr)
                {
                    IntegerRectangle rect = packer.GetRectangle(j);
                    var index = packer.GetRectangleId(j);
                
                    texture.SetPixels32(rect.X, rect.Y, rect.Width, rect.Height, textures[index].GetPixels32());
                }

                // var a = textures[0].GetPixels32();
                //
                // Parallel.For(0, packer.RectangleCount, i =>
                // {
                //     IntegerRectangle rect = packer.GetRectangle(i);
                //     var index = packer.GetRectangleId(i);
                //
                //     texture.SetPixels32(rect.X, rect.Y, rect.Width, rect.Height, a);
                //     
                //     texture.SetPixelData();
                // });
                
                for (var j = 0; j < packer.RectangleCount; j++)
                {
                    IntegerRectangle rect = packer.GetRectangle(j);
                    var index = packer.GetRectangleId(j);

                    var spriteInfo = new SpriteInfo(
                        x: rect.X,
                        y: rect.Y,
                        width: rect.Width,
                        height: rect.Height,
                        name: images[index],
                        pixelsPerUnit: pixelsPerUnit
                    );

                    spriteInfos.Add(spriteInfo);

                    garbageRect.Add(rectangles[index]);
                    garabeTextures.Add(textures[index]);
                    garbageImages.Add(images[index]);
                }

                watch40.Stop();
                Debug.Log(watch40.ElapsedMilliseconds);
                
                
                var watch50 = System.Diagnostics.Stopwatch.StartNew();
                foreach (var garbage in garbageRect)
                {
                    rectangles.Remove(garbage);
                }

                foreach (var garbage in garabeTextures)
                {
                    textures.Remove(garbage);
                }

                foreach (var garbage in garbageImages)
                {
                    images.Remove(garbage);
                }

                texture.Apply();

                //then Save To Disk as PNG
                byte[] bytes = texture.EncodeToPNG();
                File.WriteAllBytes("UserData/texture" + rectangles.Count + ".png", bytes);

                Dictionary<string, Sprite> mSprites = new Dictionary<string, Sprite>();
                foreach (var spriteInfo in spriteInfos)
                {
                    //mSprites.Add(spriteInfo.name, Sprite.Create(texture, new Rect(spriteInfo.x, spriteInfo.y, spriteInfo.width, spriteInfo.height), Vector2.zero, pixelsPerUnit, 0, SpriteMeshType.FullRect));
                    //mSprites.Add(spriteInfo.name, Sprite.Create(texture, new Rect(spriteInfo.x, spriteInfo.y, spriteInfo.width, spriteInfo.height), new Vector2(spriteInfo.width/2f, spriteInfo.height/2f), pixelsPerUnit, 0, SpriteMeshType.FullRect));
                    //mSprites.Add(spriteInfo.name, Sprite.Create(texture, new Rect(spriteInfo.x, spriteInfo.y, spriteInfo.width, spriteInfo.height), new Vector2(spriteInfo.width/2f, 0), pixelsPerUnit, 0, SpriteMeshType.FullRect));
                    mSprites.Add(spriteInfo.Name, Sprite.Create(texture, new Rect(spriteInfo.X, spriteInfo.Y, spriteInfo.Width, spriteInfo.Height), new Vector2(0.5f, 0.5f), pixelsPerUnit, 0, SpriteMeshType.FullRect));
                }

                entries.Add(new Entry(mSprites, texture, spriteInfos.ToArray()));
                watch50.Stop();
                Debug.Log(watch50.ElapsedMilliseconds);
            }
        }

        watch1.Stop();
        Debug.Log(watch1.ElapsedMilliseconds);

        return entries;
    }

    internal static List<Entry> PackTextures2(List<TextureToPack> itemsToRaster, int textureSize, float pixelsPerUnit)
    {
        List<Entry> entries = new List<Entry>();

        var textures = new List<Texture2D>();
        var images = new List<string>();

        var watch5 = System.Diagnostics.Stopwatch.StartNew();
        foreach (var itemToRaster in itemsToRaster)
        {
            // var loader = new WWW("file:///" + itemToRaster.File);
            // var texture = loader.texture;
            
            var texture = LoadPNG(itemToRaster.File);
            

            
            
            textures.Add(texture);
            images.Add(itemToRaster.Id);
        }
        watch5.Stop();
        Debug.Log($"{watch5.ElapsedMilliseconds}");
        
        var watch8 = System.Diagnostics.Stopwatch.StartNew();
        
        ConcurrentBag<string> resultCollection = new ConcurrentBag<string>();
        Parallel.ForEach(itemsToRaster, item =>
        {
            byte[] bytes = File.ReadAllBytes(item.File);
            
        });
        
        
        watch8.Stop();
        Debug.Log($"{watch8.ElapsedMilliseconds}");
        


        //Debug.Log("");
        

        var rectangles = new List<Rect>();
        for (var i = 0; i < textures.Count; i++)
        {
            if (textures[i].width > textureSize || textures[i].height > textureSize)
            {
                throw new Exception("A texture size is bigger than the sprite sheet size!");
            }

            rectangles.Add(new Rect(0, 0, textures[i].width, textures[i].height));
        }

        const int padding = 1;
        var watch1 = System.Diagnostics.Stopwatch.StartNew();
        while (rectangles.Count > 0)
        {
            
            
            var watch20 = System.Diagnostics.Stopwatch.StartNew();
            
            var texture = new Texture2D(textureSize, textureSize, TextureFormat.ARGB32, false);
            texture.filterMode = FilterMode.Point;
            //var fillColor = texture.GetPixels32();
            // I believe this isnt needed.
            /*
            var g = 0;
            for (; g < fillColor.Length; ++g)
            {
                //fillColor[g] = Color.clear;
            }
            */
            
            watch20.Stop();
            //Debug.Log($"{watch20.ElapsedMilliseconds} _ {g}");

            
            var watch30 = System.Diagnostics.Stopwatch.StartNew();
            
            var packer = new RectanglePacker(texture.width, texture.height, padding);


            for (var i = 0; i < rectangles.Count; i++)
            {
                packer.InsertRectangle((int)rectangles[i].width, (int)rectangles[i].height, i);
            }


            packer.PackRectangles();
            
            
            watch30.Stop();
            Debug.Log(watch30.ElapsedMilliseconds);

            if (packer.RectangleCount > 0)
            {
                var watch35 = System.Diagnostics.Stopwatch.StartNew();
                //texture.SetPixels32(fillColor);
                watch35.Stop();
                Debug.Log(watch35.ElapsedMilliseconds);
                var spriteInfos = new List<SpriteInfo>();

                var garbageRect = new List<Rect>();
                var garabeTextures = new List<Texture2D>();
                var garbageImages = new List<string>();

                
                var watch40 = System.Diagnostics.Stopwatch.StartNew();


                int[] indexArr = new int[packer.RectangleCount];
                for (var j = 0; j < packer.RectangleCount; j++)
                {
                    indexArr[j] = j;
                }
                
                foreach (int j in indexArr)
                {
                    IntegerRectangle rect = packer.GetRectangle(j);
                    var index = packer.GetRectangleId(j);
                
                    texture.SetPixels32(rect.X, rect.Y, rect.Width, rect.Height, textures[index].GetPixels32());
                }

                // var a = textures[0].GetPixels32();
                //
                // Parallel.For(0, packer.RectangleCount, i =>
                // {
                //     IntegerRectangle rect = packer.GetRectangle(i);
                //     var index = packer.GetRectangleId(i);
                //
                //     texture.SetPixels32(rect.X, rect.Y, rect.Width, rect.Height, a);
                //     
                //     texture.SetPixelData();
                // });
                
                foreach (int j in indexArr)
                {
                    IntegerRectangle rect = packer.GetRectangle(j);
                    var index = packer.GetRectangleId(j);

                    var spriteInfo = new SpriteInfo(
                        x: rect.X,
                        y: rect.Y,
                        width: rect.Width,
                        height: rect.Height,
                        name: images[index],
                        pixelsPerUnit: pixelsPerUnit
                    );

                    spriteInfos.Add(spriteInfo);

                    garbageRect.Add(rectangles[index]);
                    garabeTextures.Add(textures[index]);
                    garbageImages.Add(images[index]);
                }

                watch40.Stop();
                Debug.Log(watch40.ElapsedMilliseconds);
                
                
                var watch50 = System.Diagnostics.Stopwatch.StartNew();
                foreach (var garbage in garbageRect)
                {
                    rectangles.Remove(garbage);
                }

                foreach (var garbage in garabeTextures)
                {
                    textures.Remove(garbage);
                }

                foreach (var garbage in garbageImages)
                {
                    images.Remove(garbage);
                }

                texture.Apply();

                //then Save To Disk as PNG
                //byte[] bytes = texture.EncodeToPNG();
                //File.WriteAllBytes("UserData/texture" + ".png", bytes);

                Dictionary<string, Sprite> mSprites = new Dictionary<string, Sprite>();
                foreach (var spriteInfo in spriteInfos)
                {
                    //mSprites.Add(spriteInfo.name, Sprite.Create(texture, new Rect(spriteInfo.x, spriteInfo.y, spriteInfo.width, spriteInfo.height), Vector2.zero, pixelsPerUnit, 0, SpriteMeshType.FullRect));
                    //mSprites.Add(spriteInfo.name, Sprite.Create(texture, new Rect(spriteInfo.x, spriteInfo.y, spriteInfo.width, spriteInfo.height), new Vector2(spriteInfo.width/2f, spriteInfo.height/2f), pixelsPerUnit, 0, SpriteMeshType.FullRect));
                    //mSprites.Add(spriteInfo.name, Sprite.Create(texture, new Rect(spriteInfo.x, spriteInfo.y, spriteInfo.width, spriteInfo.height), new Vector2(spriteInfo.width/2f, 0), pixelsPerUnit, 0, SpriteMeshType.FullRect));
                    mSprites.Add(spriteInfo.Name, Sprite.Create(texture, new Rect(spriteInfo.X, spriteInfo.Y, spriteInfo.Width, spriteInfo.Height), new Vector2(0.5f, 0.5f), pixelsPerUnit, 0, SpriteMeshType.FullRect));
                }

                entries.Add(new Entry(mSprites, texture, spriteInfos.ToArray()));
                watch50.Stop();
                Debug.Log(watch50.ElapsedMilliseconds);
            }
        }

        watch1.Stop();
        Debug.Log(watch1.ElapsedMilliseconds);

        return entries;
    }
}