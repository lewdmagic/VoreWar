using OdinSerializer;
using System.Collections.Generic;
using UnityEngine;

internal class ScatInfo
{
    [OdinSerialize]
    private string _name = "";

    public string name { get => _name; set => _name = value; }

    [OdinSerialize]
    private int _color = -1;

    public int color { get => _color; set => _color = value; }

    [OdinSerialize]
    private Race _predRace;

    public Race predRace { get => _predRace; set => _predRace = value; }

    [OdinSerialize]
    private Race _preyRace;

    public Race preyRace { get => _preyRace; set => _preyRace = value; }

    [OdinSerialize]
    private int _preySize;

    public int preySize { get => _preySize; set => _preySize = value; }

    [OdinSerialize]
    private List<BoneInfo> _bonesInfos;

    public List<BoneInfo> bonesInfos { get => _bonesInfos; set => _bonesInfos = value; }

    public ScatInfo(Unit pred, Prey preyUnit)
    {
        name = preyUnit.Unit.Name;
        predRace = pred.Race;
        preyRace = preyUnit.Unit.Race;
        preySize = Mathf.RoundToInt(preyUnit.Actor.BodySize());

        // ReSharper disable once PossibleUnintendedReferenceComparison
        if (pred.Race == Race.Slime)
        {
            color = pred.AccessoryColor;
        }

        if (Equals(pred.Race, Race.Selicia) || Equals(preyUnit.Unit.Race, Race.Selicia))
        {
            color = 3;
        }

        bonesInfos = preyUnit.GetBoneTypes();
        if (Config.ScatBones == false)
        {
            //empty list
            bonesInfos = new List<BoneInfo>();
        }
        else
        {
            //Randomizing
            List<BoneInfo> result = new List<BoneInfo>();
            foreach (BoneInfo bonesInfo in bonesInfos)
            {
                if (Random.Range(0, 100) > 45)
                {
                    result.Add(bonesInfo);
                }
            }

            bonesInfos = result;
        }
    }

    public string GetDescription()
    {
        return $"Remains of {name}";
    }
}