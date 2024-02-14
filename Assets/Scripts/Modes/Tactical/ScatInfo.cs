using OdinSerializer;
using System.Collections.Generic;
using UnityEngine;

internal class ScatInfo
{
    [OdinSerialize]
    private string _name = "";

    public string Name { get => _name; set => _name = value; }

    [OdinSerialize]
    private int _color = -1;

    public int Color { get => _color; set => _color = value; }

    [OdinSerialize]
    private Race _predRace;

    public Race PredRace { get => _predRace; set => _predRace = value; }

    [OdinSerialize]
    private Race _preyRace;

    public Race PreyRace { get => _preyRace; set => _preyRace = value; }

    [OdinSerialize]
    private int _preySize;

    public int PreySize { get => _preySize; set => _preySize = value; }

    [OdinSerialize]
    private List<BoneInfo> _bonesInfos;

    public List<BoneInfo> BonesInfos { get => _bonesInfos; set => _bonesInfos = value; }

    public ScatInfo(Unit pred, Prey preyUnit)
    {
        Name = preyUnit.Unit.Name;
        PredRace = pred.Race;
        PreyRace = preyUnit.Unit.Race;
        PreySize = Mathf.RoundToInt(preyUnit.Actor.BodySize());

        // ReSharper disable once PossibleUnintendedReferenceComparison
        if (pred.Race == Race.Slime)
        {
            Color = pred.AccessoryColor;
        }

        if (Equals(pred.Race, Race.Selicia) || Equals(preyUnit.Unit.Race, Race.Selicia))
        {
            Color = 3;
        }

        BonesInfos = preyUnit.GetBoneTypes();
        if (Config.ScatBones == false)
        {
            //empty list
            BonesInfos = new List<BoneInfo>();
        }
        else
        {
            //Randomizing
            List<BoneInfo> result = new List<BoneInfo>();
            foreach (BoneInfo bonesInfo in BonesInfos)
            {
                if (Random.Range(0, 100) > 45)
                {
                    result.Add(bonesInfo);
                }
            }

            BonesInfos = result;
        }
    }

    public string GetDescription()
    {
        return $"Remains of {Name}";
    }
}