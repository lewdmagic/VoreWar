﻿using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Reincarnator
{
    [OdinSerialize]
    public Unit PastLife { get; set; }
    [OdinSerialize]
    public Race Race { get; set; }
    [OdinSerialize]
    public bool RaceLocked { get; set; } = false;

    public Reincarnator(Unit Unit, Race race, bool raceLocked = false)
    {
        PastLife = Unit;
        PastLife.FixedSide = PastLife.FixedSide;
        PastLife.RemoveTrait(TraitType.Diseased);
        PastLife.RemoveTrait(TraitType.Illness);
        PastLife.RemoveTrait(TraitType.Infertile);
        Race = race;
        RaceLocked = raceLocked;
    }
}

