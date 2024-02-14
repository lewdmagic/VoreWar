using OdinSerializer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


internal class ScatV2Discard : MiscDiscard
{
    [OdinSerialize]
    private ScatInfo _scatInfo;

    internal ScatInfo ScatInfo { get => _scatInfo; set => _scatInfo = value; }

    public ScatV2Discard(Vec2I location, int sortOrder, ScatInfo scatInfo, MiscDiscardType type = 0, int spriteNum = 0, int color = 0, string description = "") : base(location, type, spriteNum, sortOrder, color, description)
    {
        this.ScatInfo = scatInfo;
        this.Color = scatInfo.Color;
        this.Description = scatInfo.GetDescription();
    }

    public override void GenerateSpritePrefab(Transform folder)
    {
        Vector3 loc = new Vector3(Location.X - .5f + Random.Range(0, 1f), Location.Y - .5f + Random.Range(0, 1f));

        var scatBack = Object.Instantiate(State.GameManager.DiscardedClothing, loc, new Quaternion(), folder).GetComponent<SpriteRenderer>();
        scatBack.sortingOrder = SortOrder;

        var scatFront = Object.Instantiate(State.GameManager.DiscardedClothing, loc, new Quaternion(), folder).GetComponent<SpriteRenderer>();
        scatFront.sortingOrder = SortOrder + 1 + ScatInfo.BonesInfos.Count;

        if (Color != -1)
        {
            scatBack.color = ColorPaletteMap.GetSlimeBaseColor(Color);
            scatFront.color = ColorPaletteMap.GetSlimeBaseColor(Color);
        }
        else
        {
            int r = 135 + Random.Range(-20, 20);
            int g = 107 + Random.Range(-5, 5);
            int b = 80 + Random.Range(-5, 5);
            Color defaultColor = new Color(r / 255.0F, g / 255.0F, b / 255.0F);
            scatBack.color = defaultColor;
            scatFront.color = defaultColor;
        }

        Vector3 scatSpriteScalingGloble = new Vector3(1f, 1f);

        if (ScatInfo.PreySize < 9)
        {
            int rndNum = Random.Range(0, State.GameManager.SpriteDictionary.ScatV2SBack.Length);
            scatBack.sprite = State.GameManager.SpriteDictionary.ScatV2SBack[rndNum];
            scatFront.sprite = State.GameManager.SpriteDictionary.ScatV2SFront[rndNum];
        }
        else if (ScatInfo.PreySize > 15)
        {
            int rndNum = Random.Range(0, State.GameManager.SpriteDictionary.ScatV2LBack.Length);
            scatBack.sprite = State.GameManager.SpriteDictionary.ScatV2LBack[rndNum];
            scatFront.sprite = State.GameManager.SpriteDictionary.ScatV2LFront[rndNum];
            int baseSize = ScatInfo.PreySize - 16; // min = 0
            float xy = 1f + baseSize / (100.0f + baseSize);
            scatSpriteScalingGloble = new Vector3(xy, xy);
            scatBack.transform.localScale = scatSpriteScalingGloble;
            scatFront.transform.localScale = scatSpriteScalingGloble;
        }
        else
        {
            int rndNum = Random.Range(0, State.GameManager.SpriteDictionary.ScatV2MBack.Length);
            scatBack.sprite = State.GameManager.SpriteDictionary.ScatV2MBack[rndNum];
            scatFront.sprite = State.GameManager.SpriteDictionary.ScatV2MFront[rndNum];
        }

        //insert bones
        List<SpriteRenderer> boneSprites = new List<SpriteRenderer>();
        foreach (BoneInfo bonesInfo in ScatInfo.BonesInfos)
        {
            boneSprites.Add(Object.Instantiate(State.GameManager.DiscardedClothing, bonesInfo.GetBonePosForScat(loc), new Quaternion(), folder).GetComponent<SpriteRenderer>());
            boneSprites.Last().transform.localScale = Vector3.Scale(bonesInfo.GetBoneScalingForScat(), scatSpriteScalingGloble);
            boneSprites.Last().sortingOrder = SortOrder + boneSprites.Count;
            boneSprites.Last().sprite = State.GameManager.SpriteDictionary.Bones[(int)bonesInfo.BoneType];
            if (Color != -1)
            {
                if (bonesInfo.BoneType == BoneType.CrypterBonePile)
                    boneSprites.Last().GetComponentInChildren<SpriteRenderer>().material = ColorPaletteMap.GetPalette(SwapType.CrypterWeapon, Color).ColorSwapMaterial;
                else if (bonesInfo.BoneType == BoneType.SlimePile) boneSprites.Last().GetComponentInChildren<SpriteRenderer>().material = ColorPaletteMap.GetPalette(SwapType.SlimeMain, Color).ColorSwapMaterial;
            }
        }
    }
}