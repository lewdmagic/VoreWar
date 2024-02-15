internal static class TraitsMethods
{
    public static bool IsRaceModifying(TraitType traitType)
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

    public static TraitType LastTrait()
    {
        return TraitType.SpiritPossession;
    }
}