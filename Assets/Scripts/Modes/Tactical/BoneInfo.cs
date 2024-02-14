using OdinSerializer;
using UnityEngine;

public class BoneInfo
{
    [OdinSerialize]
    private BoneType _boneType = BoneType.GenericBonePile;
    public BoneType BoneType { get => _boneType; set => _boneType = value; }
    [OdinSerialize]
    private string _name = "";
    public string name { get => _name; set => _name = value; }
    [OdinSerialize]
    private int _accessoryColor = -1;
    public int accessoryColor { get => _accessoryColor; set => _accessoryColor = value; }

    public BoneInfo(BoneType boneType, string name = "", int accessoryColor = -1)
    {
        this.name = name;
        this.BoneType = boneType;
        this.accessoryColor = accessoryColor;
    }

    public Vector3 GetBoneScalingForScat()
    {
        Vector3 rtn = new Vector3(1f, 1f);
        switch (BoneType)
        {
            case BoneType.GenericBonePile:
            case BoneType.SlimePile:
            case BoneType.CrypterBonePile:
            case BoneType.DisposedCondom:
            case BoneType.CumPuddle:
                rtn = new Vector3(0.65f, 0.65f);
                break;
            case BoneType.CrypterSkull:
            case BoneType.HumanoidSkull:
            case BoneType.LizardSkull:
            case BoneType.Imp2EyeSkull:
            case BoneType.Imp1EyeSkull:
            case BoneType.Imp3EyeSkull:
            case BoneType.SeliciaSkull:
                rtn = new Vector3(0.65f, 0.65f);
                break;
            case BoneType.FurryBones:
            case BoneType.FurryRabbitBones:
            case BoneType.HarrysGooPile:
                rtn = new Vector3(1f, 1f);
                break;
            case BoneType.Kangaroo:
                rtn = new Vector3(0.85f, 0.85f);
                break;
            case BoneType.Alligator:
                rtn = new Vector3(1f, 1f);
                break;
            case BoneType.Wyvern:
            case BoneType.YoungWyvern:
            case BoneType.Compy:
            case BoneType.Shark:
            case BoneType.DarkSwallower:
            case BoneType.Cake:
            case BoneType.WyvernBonesWithoutHead:
                rtn = new Vector3(0.5f, 0.5f);
                break;
            case BoneType.VisionSkull:
                rtn = new Vector3(0.8f, 0.8f);
                break;
            default:
                // Hide unknown bone type
                rtn = new Vector3(0f, 0f);
                break;
        }
        return rtn;
    }

    public Vector3 GetBoneOffsetForScat()
    {
        Vector3 rtn;
        switch (this.BoneType)
        {
            case BoneType.GenericBonePile:
                rtn = new Vector3(0, 0.01f);
                break;
            case BoneType.SlimePile:
            case BoneType.CrypterBonePile:
            case BoneType.DisposedCondom:
            case BoneType.CumPuddle:
                rtn = new Vector3(0, 0);
                break;
            case BoneType.CrypterSkull:
            case BoneType.HumanoidSkull:
            case BoneType.LizardSkull:
            case BoneType.Imp2EyeSkull:
            case BoneType.Imp1EyeSkull:
            case BoneType.Imp3EyeSkull:
                rtn = new Vector3(Random.Range(-.07f, .07f), -.02f);
                break;
            case BoneType.SeliciaSkull:
                rtn = new Vector3(Random.Range(-.07f, .07f), Random.Range(0, .03f));
                break;
            case BoneType.FurryBones:
            case BoneType.FurryRabbitBones:
            case BoneType.HarrysGooPile:
                rtn = new Vector3(0, 0.02f);
                break;
            case BoneType.Kangaroo:
                rtn = new Vector3(0, 0.02f);
                break;
            case BoneType.Alligator:
            case BoneType.Wyvern:
                rtn = new Vector3(0, 0);
                break;
            case BoneType.YoungWyvern:
                rtn = new Vector3(0, 0.015f);
                break;
            case BoneType.Compy:
                rtn = new Vector3(0, 0.03f);
                break;
            case BoneType.Shark:
                rtn = new Vector3(0 - 0.01f, -0.01f);
                break;
            case BoneType.DarkSwallower:
                rtn = new Vector3(0, 0.05f);
                break;
            case BoneType.Cake:
                rtn = new Vector3(Random.Range(-.11f, .11f), 0.1f);
                break;
            case BoneType.WyvernBonesWithoutHead:
                rtn = new Vector3(0, .05f);
                break;
            case BoneType.VisionSkull:
                rtn = new Vector3(Random.Range(-.07f, .07f), Random.Range(-0.13f, -0.15f));
                break;
            default:
                rtn = new Vector3(0, 0);
                break;
        }
        return rtn;
    }

    public Vector3 GetBonePosForScat(Vector3 orgin)
    {
        Vector3 offset = GetBoneOffsetForScat();
        return new Vector3(orgin.x + offset.x, orgin.y + offset.y);
    }
}
