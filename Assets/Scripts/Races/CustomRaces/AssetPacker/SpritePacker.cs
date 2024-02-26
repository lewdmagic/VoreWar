using System;
using System.Collections.Generic;
using System.Linq;
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


    internal static List<Entry> PackTextures(List<TextureToPack> itemsToRaster, int textureSize, float pixelsPerUnit)
    {
        List<Entry> entries = new List<Entry>();

        var textures = new List<Texture2D>();
        var images = new List<string>();

        foreach (var itemToRaster in itemsToRaster)
        {
            var loader = new WWW("file:///" + itemToRaster.File);
            textures.Add(loader.texture);
            images.Add(itemToRaster.Id);
        }


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
            var texture = new Texture2D(textureSize, textureSize, TextureFormat.ARGB32, false);
            texture.filterMode = FilterMode.Point;
            var fillColor = texture.GetPixels32();
            for (var i = 0; i < fillColor.Length; ++i)
            {
                fillColor[i] = Color.clear;
            }

            var packer = new RectanglePacker(texture.width, texture.height, padding);


            for (var i = 0; i < rectangles.Count; i++)
            {
                packer.InsertRectangle((int)rectangles[i].width, (int)rectangles[i].height, i);
            }


            packer.PackRectangles();

            if (packer.RectangleCount > 0)
            {
                texture.SetPixels32(fillColor);
                var spriteInfos = new List<SpriteInfo>();

                var garbageRect = new List<Rect>();
                var garabeTextures = new List<Texture2D>();
                var garbageImages = new List<string>();

                for (var j = 0; j < packer.RectangleCount; j++)
                {
                    IntegerRectangle rect = packer.GetRectangle(j);
                    var index = packer.GetRectangleId(j);

                    texture.SetPixels32(rect.X, rect.Y, rect.Width, rect.Height, textures[index].GetPixels32());

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


                Dictionary<string, Sprite> mSprites = new Dictionary<string, Sprite>();
                foreach (var spriteInfo in spriteInfos)
                {
                    //mSprites.Add(spriteInfo.name, Sprite.Create(texture, new Rect(spriteInfo.x, spriteInfo.y, spriteInfo.width, spriteInfo.height), Vector2.zero, pixelsPerUnit, 0, SpriteMeshType.FullRect));
                    //mSprites.Add(spriteInfo.name, Sprite.Create(texture, new Rect(spriteInfo.x, spriteInfo.y, spriteInfo.width, spriteInfo.height), new Vector2(spriteInfo.width/2f, spriteInfo.height/2f), pixelsPerUnit, 0, SpriteMeshType.FullRect));
                    //mSprites.Add(spriteInfo.name, Sprite.Create(texture, new Rect(spriteInfo.x, spriteInfo.y, spriteInfo.width, spriteInfo.height), new Vector2(spriteInfo.width/2f, 0), pixelsPerUnit, 0, SpriteMeshType.FullRect));
                    mSprites.Add(spriteInfo.Name, Sprite.Create(texture, new Rect(spriteInfo.X, spriteInfo.Y, spriteInfo.Width, spriteInfo.Height), new Vector2(0.5f, 0.5f), pixelsPerUnit, 0, SpriteMeshType.FullRect));
                }

                entries.Add(new Entry(mSprites, texture, spriteInfos.ToArray()));
            }
        }

        watch1.Stop();
        //Debug.Log(watch1.ElapsedMilliseconds);

        return entries;
    }
}