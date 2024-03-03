using System;



/// <summary>
/// Alternative to enum
/// Currently it's not in use and not planned for use
/// </summary>
public class SwapTypeNew : IComparable<SwapTypeNew>
{

    public readonly string Id;

    internal SwapTypeNew(string id)
    {
        Id = id;
    }
    
    public override bool Equals(object obj)
    {
        SwapTypeNew otherSwapType = obj as SwapTypeNew;
        if (otherSwapType == null)
        {
            return false;
        }

        return Id.Equals(otherSwapType.Id);
    }
    
    public static bool operator == (SwapTypeNew b1, SwapTypeNew b2)
    {
        if ((object)b1 == null)
            return (object)b2 == null;

        return b1.Equals(b2);
    }

    public static bool operator != (SwapTypeNew b1, SwapTypeNew b2)
    {
        return !(b1 == b2);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return Id;
    }

    public int CompareTo(SwapTypeNew other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (ReferenceEquals(null, other))
        {
            return 1;
        }

        return string.Compare(Id, other.Id, StringComparison.Ordinal);
    }



    public static readonly SwapTypeNew NormalHair = new SwapTypeNew("NormalHair");
    public static readonly SwapTypeNew HairRedKeyStrict = new SwapTypeNew("HairRedKeyStrict");
    public static readonly SwapTypeNew WildHair = new SwapTypeNew("WildHair");
    public static readonly SwapTypeNew UniversalHair = new SwapTypeNew("UniversalHair");
    public static readonly SwapTypeNew Fur = new SwapTypeNew("Fur");
    public static readonly SwapTypeNew FurStrict = new SwapTypeNew("FurStrict");
    public static readonly SwapTypeNew Skin = new SwapTypeNew("Skin");
    public static readonly SwapTypeNew RedSkin = new SwapTypeNew("RedSkin");
    public static readonly SwapTypeNew RedFur = new SwapTypeNew("RedFur");
    public static readonly SwapTypeNew Mouth = new SwapTypeNew("Mouth");
    public static readonly SwapTypeNew EyeColor = new SwapTypeNew("EyeColor");
    public static readonly SwapTypeNew LizardMain = new SwapTypeNew("LizardMain");
    public static readonly SwapTypeNew LizardLight = new SwapTypeNew("LizardLight");
    public static readonly SwapTypeNew SlimeMain = new SwapTypeNew("SlimeMain");
    public static readonly SwapTypeNew SlimeSub = new SwapTypeNew("SlimeSub");
    public static readonly SwapTypeNew Imp = new SwapTypeNew("Imp");
    public static readonly SwapTypeNew ImpDark = new SwapTypeNew("ImpDark");
    public static readonly SwapTypeNew ImpRedKey = new SwapTypeNew("ImpRedKey");
    public static readonly SwapTypeNew OldImp = new SwapTypeNew("OldImp");
    public static readonly SwapTypeNew OldImpDark = new SwapTypeNew("OldImpDark");
    public static readonly SwapTypeNew Goblins = new SwapTypeNew("Goblins");
    public static readonly SwapTypeNew CrypterWeapon = new SwapTypeNew("CrypterWeapon");
    public static readonly SwapTypeNew Clothing = new SwapTypeNew("Clothing");
    public static readonly SwapTypeNew ClothingStrict = new SwapTypeNew("ClothingStrict");
    public static readonly SwapTypeNew ClothingStrictRedKey = new SwapTypeNew("ClothingStrictRedKey");
    public static readonly SwapTypeNew Clothing50Spaced = new SwapTypeNew("Clothing50Spaced");
    public static readonly SwapTypeNew SkinToClothing = new SwapTypeNew("SkinToClothing");
    public static readonly SwapTypeNew Kangaroo = new SwapTypeNew("Kangaroo");
    public static readonly SwapTypeNew FeralWolfMane = new SwapTypeNew("FeralWolfMane");
    public static readonly SwapTypeNew FeralWolfFur = new SwapTypeNew("FeralWolfFur");
    public static readonly SwapTypeNew Alligator = new SwapTypeNew("Alligator");
    public static readonly SwapTypeNew Crux = new SwapTypeNew("Crux");
    public static readonly SwapTypeNew BeeNewSkin = new SwapTypeNew("BeeNewSkin");
    public static readonly SwapTypeNew DriderSkin = new SwapTypeNew("DriderSkin");
    public static readonly SwapTypeNew DriderEyes = new SwapTypeNew("DriderEyes");
    public static readonly SwapTypeNew AlrauneSkin = new SwapTypeNew("AlrauneSkin");
    public static readonly SwapTypeNew AlrauneHair = new SwapTypeNew("AlrauneHair");
    public static readonly SwapTypeNew AlrauneFoliage = new SwapTypeNew("AlrauneFoliage");
    public static readonly SwapTypeNew DemibatSkin = new SwapTypeNew("DemibatSkin");
    public static readonly SwapTypeNew DemibatHumanSkin = new SwapTypeNew("DemibatHumanSkin");
    public static readonly SwapTypeNew MermenSkin = new SwapTypeNew("MermenSkin");
    public static readonly SwapTypeNew MermenHair = new SwapTypeNew("MermenHair");
    public static readonly SwapTypeNew AviansSkin = new SwapTypeNew("AviansSkin");
    public static readonly SwapTypeNew DemiantSkin = new SwapTypeNew("DemiantSkin");
    public static readonly SwapTypeNew DemifrogSkin = new SwapTypeNew("DemifrogSkin");
    public static readonly SwapTypeNew SharkSkin = new SwapTypeNew("SharkSkin");
    public static readonly SwapTypeNew DeerSkin = new SwapTypeNew("DeerSkin");
    public static readonly SwapTypeNew DeerLeaf = new SwapTypeNew("DeerLeaf");
    public static readonly SwapTypeNew SharkReversed = new SwapTypeNew("SharkReversed");
    public static readonly SwapTypeNew Puca = new SwapTypeNew("Puca");
    public static readonly SwapTypeNew PucaBalls = new SwapTypeNew("PucaBalls");
    public static readonly SwapTypeNew HippoSkin = new SwapTypeNew("HippoSkin");
    public static readonly SwapTypeNew ViperSkin = new SwapTypeNew("ViperSkin");
    public static readonly SwapTypeNew KomodosSkin = new SwapTypeNew("KomodosSkin");
    public static readonly SwapTypeNew KomodosReversed = new SwapTypeNew("KomodosReversed");
    public static readonly SwapTypeNew CockatriceSkin = new SwapTypeNew("CockatriceSkin");
    public static readonly SwapTypeNew Harvester = new SwapTypeNew("Harvester");
    public static readonly SwapTypeNew Bat = new SwapTypeNew("Bat");
    public static readonly SwapTypeNew Kobold = new SwapTypeNew("Kobold");
    public static readonly SwapTypeNew Frog = new SwapTypeNew("Frog");
    public static readonly SwapTypeNew Dragon = new SwapTypeNew("Dragon");
    public static readonly SwapTypeNew Dragonfly = new SwapTypeNew("Dragonfly");
    public static readonly SwapTypeNew FairySpringSkin = new SwapTypeNew("FairySpringSkin");
    public static readonly SwapTypeNew FairySpringClothes = new SwapTypeNew("FairySpringClothes");
    public static readonly SwapTypeNew FairySummerSkin = new SwapTypeNew("FairySummerSkin");
    public static readonly SwapTypeNew FairySummerClothes = new SwapTypeNew("FairySummerClothes");
    public static readonly SwapTypeNew FairyFallSkin = new SwapTypeNew("FairyFallSkin");
    public static readonly SwapTypeNew FairyFallClothes = new SwapTypeNew("FairyFallClothes");
    public static readonly SwapTypeNew FairyWinterSkin = new SwapTypeNew("FairyWinterSkin");
    public static readonly SwapTypeNew FairyWinterClothes = new SwapTypeNew("FairyWinterClothes");
    public static readonly SwapTypeNew Ant = new SwapTypeNew("Ant");
    public static readonly SwapTypeNew GryphonSkin = new SwapTypeNew("GryphonSkin");
    public static readonly SwapTypeNew SlugSkin = new SwapTypeNew("SlugSkin");
    public static readonly SwapTypeNew PantherSkin = new SwapTypeNew("PantherSkin");
    public static readonly SwapTypeNew PantherHair = new SwapTypeNew("PantherHair");
    public static readonly SwapTypeNew PantherBodyPaint = new SwapTypeNew("PantherBodyPaint");
    public static readonly SwapTypeNew PantherClothes = new SwapTypeNew("PantherClothes");
    public static readonly SwapTypeNew SalamanderSkin = new SwapTypeNew("SalamanderSkin");
    public static readonly SwapTypeNew MantisSkin = new SwapTypeNew("MantisSkin");
    public static readonly SwapTypeNew EasternDragon = new SwapTypeNew("EasternDragon");
    public static readonly SwapTypeNew CatfishSkin = new SwapTypeNew("CatfishSkin");
    public static readonly SwapTypeNew GazelleSkin = new SwapTypeNew("GazelleSkin");
    public static readonly SwapTypeNew EarthwormSkin = new SwapTypeNew("EarthwormSkin");
    public static readonly SwapTypeNew HorseSkin = new SwapTypeNew("HorseSkin");
    public static readonly SwapTypeNew TerrorbirdSkin = new SwapTypeNew("TerrorbirdSkin");
    public static readonly SwapTypeNew VargulSkin = new SwapTypeNew("VargulSkin");
    public static readonly SwapTypeNew FeralLionsFur = new SwapTypeNew("FeralLionsFur");
    public static readonly SwapTypeNew FeralLionsEyes = new SwapTypeNew("FeralLionsEyes");
    public static readonly SwapTypeNew FeralLionsMane = new SwapTypeNew("FeralLionsMane");
    public static readonly SwapTypeNew GoodraSkin = new SwapTypeNew("GoodraSkin");
    public static readonly SwapTypeNew AabayxSkin = new SwapTypeNew("AabayxSkin");
}