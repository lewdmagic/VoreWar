using System;
using System.Collections.Generic;
using System.IO;
using DaVikingCode.RectanglePacking;
using UnityEngine;

namespace DaVikingCode.AssetPacker
{
    
    public class SpriteManager
    {
        private readonly Dictionary<string, Sprite> _mSprites = new Dictionary<string, Sprite>();

        public void Process2()
        {
            var files = Directory.GetFiles("GameData/CustomRaces/Equinezz/Sprites", "*.png");
            
            
            
            List<TextureToPack> itemsToRaster = new List<TextureToPack>();
            foreach (var file in files)
            {
                itemsToRaster.Add(new TextureToPack(file, Path.GetFileNameWithoutExtension(file)));
            }
            
            //string path = Application.persistentDataPath + "/AssetPacker/" + cacheName + "/" + cacheVersion + "/";
            //string path = $"UserData{Path.DirectorySeparatorChar}CachedSprites";
            //Debug.Log(Application.persistentDataPath);

            
            
            //LoadPack("UserData");
            Create(itemsToRaster);
        }

        private void Create(List<TextureToPack> itemsToRaster)
        {
            var res = SpritePacker.PackTextures(itemsToRaster, 2048, 160f);

            foreach (var spriteSheets in res)
            {
                foreach (var sprite in spriteSheets.Dict)
                {
                    //_mSprites.Add(sprite.Key, sprite.Value);
                    _mSprites[sprite.Key] = sprite.Value;
                }
            }
            
            //Save("UserData", res);
        }
/*
        private static List<Entry> CreatePack2(List<TextureToPack> itemsToRaster, int textureSize, float pixelsPerUnit)
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
                    packer.insertRectangle((int)rectangles[i].width, (int)rectangles[i].height, i);
                }

                
                packer.packRectangles();

                if (packer.rectangleCount > 0)
                {
                    texture.SetPixels32(fillColor);
                    var rect = new IntegerRectangle();
                    var spriteInfos = new List<SpriteInfo>();

                    var garbageRect = new List<Rect>();
                    var garabeTextures = new List<Texture2D>();
                    var garbageImages = new List<string>();

                    for (var j = 0; j < packer.rectangleCount; j++)
                    {
                        rect = packer.getRectangle(j, rect);

                        var index = packer.getRectangleId(j);

                        texture.SetPixels32(rect.x, rect.y, rect.width, rect.height, textures[index].GetPixels32());

                        var spriteInfo = new SpriteInfo(
                            x: rect.x,
                            y: rect.y,
                            width: rect.width,
                            height: rect.height,
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
                        mSprites.Add(spriteInfo.name, Sprite.Create(texture, new Rect(spriteInfo.x, spriteInfo.y, spriteInfo.width, spriteInfo.height), Vector2.zero, pixelsPerUnit, 0, SpriteMeshType.FullRect));
                    }

                    entries.Add(new Entry(mSprites, texture, spriteInfos.ToArray()));
                }
            }
            watch1.Stop();
            Debug.Log(watch1.ElapsedMilliseconds);

            return entries;
        }
*/

        private static void Save(string savePath, List<Entry> toSave)
        {
            List<TextureAsset> assets = new List<TextureAsset>();

            for (int i = 0; i < toSave.Count; i++)
            {
                Entry entry = toSave[i];
                string textureName = "CachedSpriteSheet" + i;
                assets.Add(new TextureAsset(textureName, entry.SpriteInfos));
                File.WriteAllBytes(savePath + "/" + textureName + ".png", entry.Texture2D.EncodeToPNG());
            }
            
            SpriteSheetCache spriteSheetCache = new SpriteSheetCache(assets.ToArray());
            File.WriteAllText(savePath + "/CachedSprites.json", JsonUtility.ToJson(spriteSheetCache));
        }

        protected void LoadPack(string savePath)
        {
            var loaderJson = new WWW("file:///" + savePath + "/CachedSprites.json");
            var textureAssets = JsonUtility.FromJson<SpriteSheetCache>(loaderJson.text);
            
            foreach (TextureAsset textureAsset in textureAssets.assets)
            {
                
                var bytes = File.ReadAllBytes(savePath + "/" + textureAsset.textureFileName + ".png");
                var texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
                texture.filterMode = FilterMode.Point;
                texture.LoadImage(bytes);
                
                // var loaderTexture = new WWW("file:///" + savePath + "/" + textureAsset.textureFileName + ".png");
                // var texture = loaderTexture.texture; // prevent creating a new Texture2D each time.
                foreach (var spriteInfo in textureAsset.spriteInfos)
                {
                    Sprite sprite = Sprite.Create(
                        texture,
                        new Rect(spriteInfo.x, spriteInfo.y, spriteInfo.width, spriteInfo.height),
                        Vector2.zero,
                        spriteInfo.pixelsPerUnit,
                        0,
                        SpriteMeshType.FullRect
                    );
                    
                    _mSprites.Add(spriteInfo.name, sprite);
                }
            }
        }

        public void Dispose()
        {
            // foreach (var asset in _mSprites)
            // {
            //     Destroy(asset.Value.texture);
            // }
            //
            // _mSprites.Clear();
        }

        private void Destroy()
        {
            Dispose();
        }

        public Sprite GetSprite(string id, bool returnNull = false)
        {
            _mSprites.TryGetValue(id, out var sprite);
            if (sprite == null && !returnNull) throw new Exception($"Sprite {id} not found");

            return sprite;
        }

        public Sprite GetSprite0(string id, int number, bool returnNull = false)
        {
            return GetSprite(id, number + 1, returnNull);
        }

        public Sprite GetSprite(string id, int number, bool returnNull = false)
        {
            string usedId = id + "_" + number.ToString("D3");
            return GetSprite(usedId, returnNull);
        }

        public Sprite[] GetSprites(string prefix)
        {
            var spriteNames = new List<string>();
            foreach (var asset in _mSprites)
            {
                if (asset.Key.StartsWith(prefix))
                {
                    spriteNames.Add(asset.Key);
                }
            }

            spriteNames.Sort(StringComparer.Ordinal);

            var sprites = new List<Sprite>();
            Sprite sprite;
            for (var i = 0; i < spriteNames.Count; ++i)
            {
                _mSprites.TryGetValue(spriteNames[i], out sprite);

                sprites.Add(sprite);
            }

            return sprites.ToArray();
        }
    }
}