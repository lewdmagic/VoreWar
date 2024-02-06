static class TraitsMethods
{

    static public bool IsRaceModifying(TraitType traitType)
    {
        switch (traitType)
        {
            case TraitType.Metamorphosis:
            case TraitType.Changeling:
            case TraitType.GreaterChangeling:
                //case Traits.Shapeshifter:
                //case Traits.Skinwalker:
                return true;
            default:
                return false;
        }
    }

    static public TraitType LastTrait()
    {
        return TraitType.SpiritPossession;
    }
}