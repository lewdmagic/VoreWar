using OdinSerializer;

class PassiveAI : IStrategicAI
{
    [OdinSerialize]
    Side AISide;

    public PassiveAI(Side aISide)
    {
        AISide = aISide;
    }

    public bool RunAI()
    {
        return false;
    }

    public bool TurnAI()
    {
        //Empire empire = State.World.Empires[AISide];
        Village[] villages = State.World.Villages;
        for (int i = 0; i < State.World.Villages.Length; i++)
        {
            if (Equals(villages[i].Side, AISide))
            {
                StrategicUtilities.BuyBasicWeapons(villages[i]);
            }
        }
        return false;
    }
}

