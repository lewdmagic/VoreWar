﻿#region

using UnityEngine;
using UnityEngine.UI;

#endregion

internal interface ISpriteContainer
{
    int SortOrder { get; set; }
    Sprite Sprite { get; set; }
    Color Color { get; set; }
    GameObject GameObject { get; }
    bool IsImage { get; }
    //void UpdatePalette(ColorSwapPalette palette);
    //void SetOffSets(float xOffset, float yOffset);
    void Destroy();
    string Name { get; set; }

    void NewSetSprite(ISpriteChangeReadable spriteChange, Vector2 wholeBodyOffset, int extraLayerOffset);
}


internal static class SpriteContainer
{
    internal static ISpriteContainer MakeContainer(GameObject type, Transform folder)
    {
        GameObject GameObject = Object.Instantiate(type, folder);

        SpriteRenderer spriteRenderer;
        if (type == State.GameManager.SpriteRenderAnimatedPrefab)
        {
            spriteRenderer = GameObject.GetComponentInChildren<SpriteRenderer>();
            GameObject = spriteRenderer.gameObject;
        }
        else
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>();
        }

        ISpriteContainer result;

        if (spriteRenderer != null)
        {
            result = new SpriteContainerSpriteRenderer(GameObject, spriteRenderer);
        }
        else
        {
            Image image = GameObject.GetComponent<Image>();
            result = new SpriteContainerImage(GameObject, image);
        }

        return result;
    }
}

abstract class SpriteContainerShared
{
    public GameObject GameObject { get; protected private set; }

    public string Name
    {
        get => GameObject.name;
        set => GameObject.name = value;
    }
    
    
    public void Destroy()
    {
        Object.Destroy(GameObject);
    }
}

internal class SpriteContainerSpriteRenderer : SpriteContainerShared, ISpriteContainer
{
    private SpriteRenderer _spriteRenderer;

    internal SpriteContainerSpriteRenderer(GameObject obj, SpriteRenderer spriteRenderer)
    {
        GameObject = obj;
        _spriteRenderer = spriteRenderer;
    }

    
    public void NewSetSprite(ISpriteChangeReadable spriteChange, Vector2 wholeBodyOffset, int extraLayerOffset)
    {
        Sprite actualSprite = spriteChange._Sprite;
        
        if (actualSprite == null)
        {
            GameObject.SetActive(false);
            return;
        }
        
        if (spriteChange._Palette != null)
        {
            _spriteRenderer.material = spriteChange._Palette.colorSwapMaterial;
        }
        else
        {
            _spriteRenderer.material = ColorPaletteMap.Default.colorSwapMaterial;
        }
        
        if (spriteChange._Color.HasValue)
        {
            if (_spriteRenderer.color != spriteChange._Color.Value)
            {
                _spriteRenderer.color = spriteChange._Color.Value;
            }
        }
        
        int usedLayer = 0;
        if (spriteChange._Layer.HasValue)
        {
            usedLayer = spriteChange._Layer.Value;
        }
        else
        {
            Debug.LogWarning("Layer is not set");
        }
        
        GameObject.SetActive(true);
        Sprite = actualSprite;
        SortOrder = usedLayer + extraLayerOffset;
        
        Vector2 usedOffset = spriteChange._Offset + wholeBodyOffset;
        SetOffSets(usedOffset.x, usedOffset.y);
    }
    
    public bool IsImage => false;

    public int SortOrder
    {
        get => _spriteRenderer.sortingOrder;
        set => _spriteRenderer.sortingOrder = value + 20000 - 30 * ((int)GameObject.transform.parent.position.x + 3 * (int)GameObject.transform.parent.position.y);
    }

    public Sprite Sprite
    {
        get => _spriteRenderer.sprite;
        set => _spriteRenderer.sprite = value;
    }

    public Color Color
    {
        get => _spriteRenderer.color;

        set
        {
            if (_spriteRenderer.color != value)
            {
                _spriteRenderer.color = value;
            }
        }
    }

    public void UpdatePalette(ColorSwapPalette palette)
    {
        if (palette != null)
        {
            _spriteRenderer.material = palette.colorSwapMaterial;
        }
        else
        {
            _spriteRenderer.material = ColorPaletteMap.Default.colorSwapMaterial;
        }
    }

    public void SetOffSets(float xOffset, float yOffset)
    {
        GameObject.transform.localPosition = new Vector3(xOffset / 100, 0.1f + yOffset / 100, 0);
    }
}


internal class SpriteContainerImage : SpriteContainerShared, ISpriteContainer
{
    private Image _image;


    internal SpriteContainerImage(GameObject obj, Image image)
    {
        GameObject = obj;
        _image = image;
    }
    
    public void NewSetSprite(ISpriteChangeReadable spriteChange, Vector2 wholeBodyOffset, int extraLayerOffset)
    {
        Sprite actualSprite = spriteChange._Sprite;
        
        if (actualSprite == null)
        {
            GameObject.SetActive(false);
            return;
        }

        Vector2 usedOffset = spriteChange._Offset + wholeBodyOffset;
        
        UpdatePalette(spriteChange._Palette);
        if (spriteChange._Color.HasValue)
        {
            Color = spriteChange._Color.Value;
        }
        
        int usedLayer = 0;
        if (spriteChange._Layer.HasValue)
        {
            usedLayer = spriteChange._Layer.Value;
        }
        else
        {
            Debug.LogWarning("Layer is not set");
        }
        
        GameObject.SetActive(true);
        Sprite = actualSprite;
        SortOrder = usedLayer + extraLayerOffset;
        SetOffSets(usedOffset.x, usedOffset.y);
    }

    public int SortOrder { get; set; }
    public bool IsImage => true;

    public Sprite Sprite
    {
        get => _image.sprite;

        set
        {
            _image.sprite = value;
            if (!Mathf.Approximately(value.rect.width, value.rect.height))
            {
                if (value.rect.width > value.rect.height)
                {
                    GameObject.transform.localScale = new Vector3(1, value.rect.height / value.rect.width, 1);
                }
                else
                {
                    GameObject.transform.localScale = new Vector3(value.rect.width / value.rect.height, 1, 1);
                }
            }
            else
            {
                GameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public Color Color
    {
        get => _image.color;

        set
        {
            if (_image.color != value)
            {
                _image.color = value;
            }
        }
    }

    public void UpdatePalette(ColorSwapPalette palette)
    {
        if (palette != null)
        {
            _image.material = palette.colorSwapMaterial;
        }
        else
        {
            _image.material = ColorPaletteMap.Default.colorSwapMaterial;
        }
    }

    public void SetOffSets(float xOffset, float yOffset)
    {
        GameObject.transform.localPosition = new Vector3(xOffset * (160 / _image.sprite.rect.width) * (160 / _image.sprite.pixelsPerUnit), 30 + yOffset * (160 / _image.sprite.rect.height) * (160 / _image.sprite.pixelsPerUnit), 0);
    }


}