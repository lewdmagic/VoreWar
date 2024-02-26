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
    private Vec2I _location;

    internal Vec2I Location { get => _location; set => _location = value; }

    [OdinSerialize]
    private MiscDiscardType _type;

    internal MiscDiscardType Type { get => _type; set => _type = value; }

    [OdinSerialize]
    private int _spriteNum;

    internal int SpriteNum { get => _spriteNum; set => _spriteNum = value; }

    [OdinSerialize]
    private int _sortOrder;

    internal int SortOrder { get => _sortOrder; set => _sortOrder = value; }

    [OdinSerialize]
    private string _description;

    internal string Description { get => _description; set => _description = value; }

    [OdinSerialize]
    private int _color;

    internal int Color { get => _color; set => _color = value; }

    public MiscDiscard(Vec2I location, MiscDiscardType type, int spriteNum, int sortOrder, int color, string description = "")
    {
        this.Location = location;
        this.Type = type;
        this.SpriteNum = spriteNum;
        this.SortOrder = sortOrder;
        this.Description = description;
        this.Color = color;
    }

    public virtual void GenerateSpritePrefab(Transform folder)
    {
        Vector3 loc = new Vector3(Location.X - .5f + Random.Range(0, 1f), Location.Y - .5f + Random.Range(0, 1f));

        var sprite = Object.Instantiate(State.GameManager.DiscardedClothing, loc, new Quaternion(), folder).GetComponent<SpriteRenderer>();
        sprite.sortingOrder = SortOrder;
        var sprite2 = Object.Instantiate(State.GameManager.DiscardedClothing, loc, new Quaternion(), folder).GetComponent<SpriteRenderer>();
        sprite2.sortingOrder = SortOrder;
        var sprite3 = Object.Instantiate(State.GameManager.DiscardedClothing, loc, new Quaternion(), folder).GetComponent<SpriteRenderer>();
        sprite3.sortingOrder = SortOrder;
        switch (Type)
        {
            case MiscDiscardType.Scat:
                sprite.sprite = State.GameManager.SpriteDictionary.Scat[SpriteNum];
                if (Color != -1)
                {
                    sprite.color = ColorPaletteMap.GetSlimeBaseColor(Color);
                }

                break;
            case MiscDiscardType.Bones:
                sprite.sprite = State.GameManager.SpriteDictionary.Bones[SpriteNum];
                if (Color != -1)
                {
                    if (SpriteNum == (int)BoneType.CrypterBonePile)
                        sprite.GetComponentInChildren<SpriteRenderer>().material = ColorPaletteMap.GetPalette(SwapType.CrypterWeapon, Color).ColorSwapMaterial;
                    else if (SpriteNum == (int)BoneType.SlimePile) sprite.GetComponentInChildren<SpriteRenderer>().material = ColorPaletteMap.GetPalette(SwapType.SlimeMain, Color).ColorSwapMaterial;
                }

                break;
            case MiscDiscardType.Cum:
                sprite.sprite = State.GameManager.SpriteDictionary.Bones[SpriteNum];
                sprite.sortingOrder = int.MinValue;
                if (Color == 0) sprite.color = new Color(.51f, .89f, .98f);
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