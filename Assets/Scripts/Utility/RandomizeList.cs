using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class RandomizeList
{
    public int ID = -1;
    public string Name;
    public float Chance;

    [AllowEditing]
    internal List<TraitType> RandomTraits;


    internal void Save()
    {
    }

    public override string ToString()
    {
        string str = ID + ", " + Name + ", " + Chance.ToString("N", new CultureInfo("en-US")) + ", ";
        RandomTraits.ForEach(rt => str += (int)rt + "|");
        str = str.Remove(str.Length - 1);
        return str;
    }
}