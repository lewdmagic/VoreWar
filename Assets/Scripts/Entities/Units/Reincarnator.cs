using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Reincarnator
{
    [OdinSerialize]
    private Unit _pastLife;
    public Unit PastLife { get => _pastLife; set => _pastLife = value; }
    [OdinSerialize]
    private Race _race;
    public Race Race { get => _race; set => _race = value; }
    [OdinSerialize]
    private bool _raceLocked = false;
    public bool RaceLocked { get => _raceLocked; set => _raceLocked = value; }

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

