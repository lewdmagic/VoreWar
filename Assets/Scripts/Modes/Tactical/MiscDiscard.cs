using OdinSerializer;
using UnityEngine;

internal enum MiscDiscardType
{
    Scat,
    Bones,
    Cum,
    DisposedCondom
}

internal class MiscDiscard
{
    [OdinSerialize]
    private Vec2i _location;

    internal Vec2i location { get => _location; set => _location = value; }

    [OdinSerialize]
    private MiscDiscardType _type;

    internal MiscDiscardType type { get => _type; set => _type = value; }

    [OdinSerialize]
    private int _spriteNum;

    internal int spriteNum { get => _spriteNum; set => _spriteNum = value; }

    [OdinSerialize]
    private int _sortOrder;

    internal int sortOrder { get => _sortOrder; set => _sortOrder = value; }

    [OdinSerialize]
    private string _description;

    internal string description { get => _description; set => _description = value; }

    [OdinSerialize]
    private int _color;

    internal int color { get => _color; set => _color = value; }

    public MiscDiscard(Vec2i location, MiscDiscardType type, int spriteNum, int sortOrder, int color, string description = "")
    {
        this.location = location;
        this.type = type;
        this.spriteNum = spriteNum;
        this.sortOrder = sortOrder;
        this.description = description;
        this.color = color;
    }

    public virtual void GenerateSpritePrefab(Transform folder)
    {
        Vector3 loc = new Vector3(location.X - .5f + Random.Range(0, 1f), location.Y - .5f + Random.Range(0, 1f));

        var sprite = Object.Instantiate(State.GameManager.DiscardedClothing, loc, new Quaternion(), folder).GetComponent<SpriteRenderer>();
        sprite.sortingOrder = sortOrder;
        var sprite2 = Object.Instantiate(State.GameManager.DiscardedClothing, loc, new Quaternion(), folder).GetComponent<SpriteRenderer>();
        sprite2.sortingOrder = sortOrder;
        var sprite3 = Object.Instantiate(State.GameManager.DiscardedClothing, loc, new Quaternion(), folder).GetComponent<SpriteRenderer>();
        sprite3.sortingOrder = sortOrder;
        switch (type)
        {
            case MiscDiscardType.Scat:
                sprite.sprite = State.GameManager.SpriteDictionary.Scat[spriteNum];
                if (color != -1)
                {
                    sprite.color = ColorPaletteMap.GetSlimeBaseColor(color);
                }

                break;
            case MiscDiscardType.Bones:
                sprite.sprite = State.GameManager.SpriteDictionary.Bones[spriteNum];
                if (color != -1)
                {
                    if (spriteNum == (int)BoneType.CrypterBonePile)
                        sprite.GetComponentInChildren<SpriteRenderer>().material = ColorPaletteMap.GetPalette(SwapType.CrypterWeapon, color).colorSwapMaterial;
                    else if (spriteNum == (int)BoneType.SlimePile) sprite.GetComponentInChildren<SpriteRenderer>().material = ColorPaletteMap.GetPalette(SwapType.SlimeMain, color).colorSwapMaterial;
                }

                break;
            case MiscDiscardType.Cum:
                sprite.sprite = State.GameManager.SpriteDictionary.Bones[spriteNum];
                sprite.sortingOrder = int.MinValue;
                if (color == 0) sprite.color = new Color(.51f, .89f, .98f);
                break;
            case MiscDiscardType.DisposedCondom:
                sprite.sprite = State.GameManager.SpriteDictionary.Bones[25];
                sprite2.sprite = State.GameManager.SpriteDictionary.Bones[3];
                sprite3.sprite = State.GameManager.SpriteDictionary.Bones[26];
                int hue = Random.Range(1, 10);
                if (hue == 1)
                {
                    int r = 255;
                    int g = 0;
                    int b = 0;
                    Color defaultColor = new Color(r / 255.0F, g / 255.0F, b / 255.0F);
                    sprite2.color = defaultColor;
                }

                if (hue == 2)
                {
                    int r = 255;
                    int g = 125;
                    int b = 0;
                    Color defaultColor = new Color(r / 255.0F, g / 255.0F, b / 255.0F);
                    sprite2.color = defaultColor;
                }

                if (hue == 3)
                {
                    int r = 0;
                    int g = 255;
                    int b = 0;
                    Color defaultColor = new Color(r / 255.0F, g / 255.0F, b / 255.0F);
                    sprite2.color = defaultColor;
                }

                if (hue == 4)
                {
                    int r = 100;
                    int g = 100;
                    int b = 255;
                    Color defaultColor = new Color(r / 255.0F, g / 255.0F, b / 255.0F);
                    sprite2.color = defaultColor;
                }

                if (hue == 5)
                {
                    int r = 255;
                    int g = 0;
                    int b = 255;
                    Color defaultColor = new Color(r / 255.0F, g / 255.0F, b / 255.0F);
                    sprite2.color = defaultColor;
                }

                if (hue == 6)
                {
                    int r = 255;
                    int g = 255;
                    int b = 0;
                    Color defaultColor = new Color(r / 255.0F, g / 255.0F, b / 255.0F);
                    sprite2.color = defaultColor;
                }

                if (hue == 7)
                {
                    int r = 0;
                    int g = 255;
                    int b = 255;
                    Color defaultColor = new Color(r / 255.0F, g / 255.0F, b / 255.0F);
                    sprite2.color = defaultColor;
                }

                if (hue == 8)
                {
                    int r = 255;
                    int g = 255;
                    int b = 255;
                    Color defaultColor = new Color(r / 255.0F, g / 255.0F, b / 255.0F);
                    sprite2.color = defaultColor;
                }

                if (hue == 9)
                {
                    int r = 0;
                    int g = 0;
                    int b = 0;
                    Color defaultColor = new Color(r / 255.0F, g / 255.0F, b / 255.0F);
                    sprite2.color = defaultColor;
                }

                break;
        }
    }
}