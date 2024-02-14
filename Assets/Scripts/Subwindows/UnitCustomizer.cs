using System;
using System.Linq;
using UnityEngine.UI;

public class UnitCustomizer
{
    protected Actor_Unit actor;

    internal IRaceData RaceData;

    //protected CustomizerButton[] buttons;
    internal EnumIndexedArray<ButtonType, CustomizerButton> buttons = new EnumIndexedArray<ButtonType, CustomizerButton>();

    protected CustomizerPanel CustomizerUI;

    internal static string HairColorLookup(int colorNumber)
    {
        switch (colorNumber)
        {
            case 0:
                return "Black";
            case 1:
                return "Cream";
            case 2:
                return "Orange";
            case 3:
                return "Blonde";
            case 4:
                return "Pink";
            case 5:
                return "Brown";
            case 6:
                return "Dark Gray";
            case 7:
                return "Yellow";
            case 8:
                return "Red";
            case 9:
                return "Maroon";
            case 10:
                return "Light Gray";
            case 11:
                return "Purple";
            case 12:
                return "Teal";
            case 13:
                return "Grape";
            case 14:
                return "Blue";
            case 15:
                return "Lime";
            case 16:
                return "Light Blue";
            case 17:
                return "Silver";
            case 18:
                return "Fire";
            case 19:
                return "Bubblegum";
            case 20:
                return "Bright Red";
            case 21:
                return "Tangerine";
            default:
                return colorNumber.ToString();
        }
    }

    public UnitCustomizer(Unit unit, CustomizerPanel UI)
    {
        CustomizerUI = UI;
        CreateButtons();
        SetUnit(unit);
    }

    public UnitCustomizer(Actor_Unit actor, CustomizerPanel UI)
    {
        CustomizerUI = UI;
        CreateButtons();
        SetActor(actor);
    }

    private void SetUpNameChangeButton(Unit unit, CustomizerPanel UI)
    {
        var button = UI.DisplayedSprite.Name.GetComponent<Button>();
        if (button == null)
        {
            button = UI.DisplayedSprite.Name.gameObject.AddComponent<Button>();
            UI.DisplayedSprite.Name.gameObject.GetComponent<Text>().raycastTarget = true;
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            var input = State.GameManager.CreateInputBox();
            input.SetData((s) =>
            {
                unit.Name = s;
                RefreshView();
            }, "Change", "Cancel", $"Modify name?", 14);
        });
    }

    public Unit Unit { get; private set; }

    public void SetUnit(Unit unit)
    {
        Vec2i noLoc = new Vec2i(0, 0);
        actor = new Actor_Unit(noLoc, unit);
        RaceData = Races2.GetRace(unit);
        Unit = unit;
        Normal(unit);
        SetUpNameChangeButton(Unit, CustomizerUI);
        RefreshView();
    }

    public void SetActor(Actor_Unit actor)
    {
        this.actor = actor;
        RaceData = Races2.GetRace(actor.Unit);
        Unit = actor.Unit;
        Normal(actor.Unit);
        SetUpNameChangeButton(Unit, CustomizerUI);
        RefreshView();
    }


    protected void Normal(Unit unit2)
    {
        foreach (ButtonType buttonType in EnumUtil.GetValues<ButtonType>())
        {
            buttons[buttonType].gameObject.SetActive(true);
            buttons[buttonType].Label.text = buttons[buttonType].defaultText;
        }

        buttons[ButtonType.BodyAccessoryType].gameObject.SetActive(RaceData.SetupOutput.SpecialAccessoryCount > 1);

        buttons[ButtonType.BodyAccessoryColor].gameObject.SetActive(RaceData.SetupOutput.AccessoryColors > 1);

        buttons[ButtonType.ExtraColor1].gameObject.SetActive(false);
        buttons[ButtonType.EyeType].gameObject.SetActive(RaceData.SetupOutput.EyeTypes > 1);
        buttons[ButtonType.Skintone].gameObject.SetActive(RaceData.SetupOutput.SkinColors > 1);
        buttons[ButtonType.EyeColor].gameObject.SetActive(RaceData.SetupOutput.EyeColors > 1);
        buttons[ButtonType.HairColor].gameObject.SetActive(RaceData.SetupOutput.HairColors > 1);
        buttons[ButtonType.HairStyle].gameObject.SetActive(RaceData.SetupOutput.HairStyles > 1);
        buttons[ButtonType.BeardStyle].gameObject.SetActive(RaceData.SetupOutput.BeardStyles > 1);
        buttons[ButtonType.BreastSize].gameObject.SetActive(unit2.HasBreasts && RaceData.SetupOutput.BreastSizes() > 1);
        buttons[ButtonType.CockSize].gameObject.SetActive(unit2.HasDick && RaceData.SetupOutput.DickSizes() > 1);
        CustomizerUI.Gender.gameObject.SetActive(RaceData.SetupOutput.CanBeGender.Count > 1);
        CustomizerUI.Nominative.gameObject.SetActive(RaceData.SetupOutput.CanBeGender.Count > 1);
        CustomizerUI.Accusative.gameObject.SetActive(RaceData.SetupOutput.CanBeGender.Count > 1);
        CustomizerUI.PronominalPossessive.gameObject.SetActive(RaceData.SetupOutput.CanBeGender.Count > 1);
        CustomizerUI.PredicativePossessive.gameObject.SetActive(RaceData.SetupOutput.CanBeGender.Count > 1);
        CustomizerUI.Reflexive.gameObject.SetActive(RaceData.SetupOutput.CanBeGender.Count > 1);
        CustomizerUI.Quantification.gameObject.SetActive(RaceData.SetupOutput.CanBeGender.Count > 1);
        RefreshGenderDropdown(unit2);
        RefreshPronouns(unit2);
        buttons[ButtonType.BodyWeight].gameObject.SetActive(RaceData.SetupOutput.BodySizes > 0);
        buttons[ButtonType.ClothingColor].gameObject.SetActive(RaceData.SetupOutput.ClothingColors > 1 && (RaceData.SetupOutput.MainClothingTypesCount > 1 || RaceData.SetupOutput.WaistClothingTypesCount > 1 || RaceData.SetupOutput.ClothingHatTypesCount > 1));
        buttons[ButtonType.ClothingType].gameObject.SetActive(RaceData.SetupOutput.MainClothingTypesCount > 1);
        buttons[ButtonType.Clothing2Type].gameObject.SetActive(RaceData.SetupOutput.WaistClothingTypesCount > 1);
        buttons[ButtonType.ClothingExtraType1].gameObject.SetActive(RaceData.SetupOutput.ExtraMainClothing1Count > 1);
        buttons[ButtonType.ClothingExtraType2].gameObject.SetActive(RaceData.SetupOutput.ExtraMainClothing2Count > 1);
        buttons[ButtonType.ClothingExtraType3].gameObject.SetActive(RaceData.SetupOutput.ExtraMainClothing3Count > 1);
        buttons[ButtonType.ClothingExtraType4].gameObject.SetActive(RaceData.SetupOutput.ExtraMainClothing4Count > 1);
        buttons[ButtonType.ClothingExtraType5].gameObject.SetActive(RaceData.SetupOutput.ExtraMainClothing5Count > 1);
        buttons[ButtonType.HatType].gameObject.SetActive(RaceData.SetupOutput.ClothingHatTypesCount > 1);
        buttons[ButtonType.ClothingAccessoryType].gameObject.SetActive(RaceData.SetupOutput.ClothingAccessoryTypesCount > 1 || (unit2.EarnedMask && RaceFuncs.isMainRaceOrTigerOrSuccubiOrGoblin(Unit.Race) && !Equals(Unit.Race, Race.Lizard)));
        
        
        // TODO needs to be fixed, missing feature until there is a good way to achieve it. 
        //buttons[ButtonTypes.ClothingColor2].gameObject.SetActive(RaceData == Races.SlimeQueenInstance || RaceData == Races.PanthersInstance || RaceData == Races.ImpsInstance || RaceData == Races.GoblinsInstance); //The additional clothing colors are slime queen only for the moment // TODO imrpove comparison
        //buttons[ButtonTypes.ClothingColor3].gameObject.SetActive(RaceData == Races.SlimeQueenInstance || RaceData == Races.PanthersInstance);
        buttons[ButtonType.ClothingColor2].gameObject.SetActive(false); //The additional clothing colors are slime queen only for the moment // TODO imrpove comparison
        buttons[ButtonType.ClothingColor3].gameObject.SetActive(false);
        
        
        buttons[ButtonType.MouthType].gameObject.SetActive(RaceData.SetupOutput.MouthTypes > 1);

        buttons[ButtonType.ExtraColor1].gameObject.SetActive(RaceData.SetupOutput.ExtraColors1 > 0);
        buttons[ButtonType.ExtraColor2].gameObject.SetActive(RaceData.SetupOutput.ExtraColors2 > 0);
        buttons[ButtonType.ExtraColor3].gameObject.SetActive(RaceData.SetupOutput.ExtraColors3 > 0);
        buttons[ButtonType.ExtraColor4].gameObject.SetActive(RaceData.SetupOutput.ExtraColors4 > 0);


        buttons[ButtonType.Furry].gameObject.SetActive(RaceData.SetupOutput.FurCapable);
        buttons[ButtonType.HeadType].gameObject.SetActive(RaceData.SetupOutput.HeadTypes > 1);
        buttons[ButtonType.TailTypes].gameObject.SetActive(RaceData.SetupOutput.TailTypes > 1);
        buttons[ButtonType.FurTypes].gameObject.SetActive(RaceData.SetupOutput.FurTypes > 1);
        buttons[ButtonType.EarTypes].gameObject.SetActive(RaceData.SetupOutput.EarTypes > 1);
        buttons[ButtonType.BodyAccentTypes1].gameObject.SetActive(RaceData.SetupOutput.BodyAccentTypes1 > 1);
        buttons[ButtonType.BodyAccentTypes2].gameObject.SetActive(RaceData.SetupOutput.BodyAccentTypes2 > 1);
        buttons[ButtonType.BodyAccentTypes3].gameObject.SetActive(RaceData.SetupOutput.BodyAccentTypes3 > 1);
        buttons[ButtonType.BodyAccentTypes4].gameObject.SetActive(RaceData.SetupOutput.BodyAccentTypes4 > 1);
        buttons[ButtonType.BodyAccentTypes5].gameObject.SetActive(RaceData.SetupOutput.BodyAccentTypes5 > 1);
        buttons[ButtonType.BallsSizes].gameObject.SetActive(RaceData.SetupOutput.BallsSizes > 1);
        buttons[ButtonType.VulvaTypes].gameObject.SetActive(RaceData.SetupOutput.VulvaTypes > 1);
        buttons[ButtonType.AltWeaponTypes].gameObject.SetActive(RaceData.SetupOutput.BasicMeleeWeaponTypes > 1 || RaceData.SetupOutput.BasicRangedWeaponTypes > 1 || RaceData.SetupOutput.AdvancedMeleeWeaponTypes > 1 || RaceData.SetupOutput.AdvancedRangedWeaponTypes > 1);


        Unit unit = Unit;
        
        Races2.GetRace(unit.Race)?.CustomizeButtons(unit, buttons);
        
        
        // switch (RaceFuncs.RaceToSwitch(unit2.Race))
        // {
        //     case RaceNumbers.Cats:
        //     case RaceNumbers.Dogs:
        //     case RaceNumbers.Foxes:
        //     case RaceNumbers.Wolves:
        //     case RaceNumbers.Bunnies:
        //     case RaceNumbers.Tigers:
        //         buttons[ButtonTypes.HairColor].Label.text = "Hair Color: " + HairColorLookup(unit.HairColor);
        //         buttons[ButtonTypes.BodyAccessoryColor].Label.text = "Fur Color: " + HairColorLookup(unit.AccessoryColor);
        //         break;
        //     case RaceNumbers.Imps:
        //         Imp();
        //         break;
        //     case RaceNumbers.Goblins:
        //         Goblin();
        //         break;
        //     case RaceNumbers.Lizards:
        //         Lizard();
        //         break;
        //     case RaceNumbers.Slimes:
        //         Slime();
        //         break;
        //     case RaceNumbers.Crypters:
        //         buttons[ButtonTypes.ClothingColor].gameObject.SetActive(false);
        //         break;
        //     case RaceNumbers.Harpies:
        //         Harpy();
        //         break;
        //     case RaceNumbers.Lamia:
        //         Lamia();
        //         break;
        //     case RaceNumbers.Kangaroos:
        //         buttons[ButtonTypes.HairStyle].Label.text = "Ear Type";
        //         break;
        //     case RaceNumbers.Taurus:
        //         buttons[ButtonTypes.EyeType].Label.text = "Face Expression";
        //         break;
        //     case RaceNumbers.Crux:
        //         Crux();
        //         break;
        //     case RaceNumbers.Wyvern:
        //         buttons[ButtonTypes.BodyWeight].Label.text = "Horn Type";
        //         break;
        //     case RaceNumbers.Succubi:
        //         buttons[ButtonTypes.ClothingColor2].gameObject.SetActive(true);
        //         break;
        //     case RaceNumbers.Collectors:
        //         buttons[ButtonTypes.ExtraColor2].Label.text = "Mouth / Dick Color";
        //         break;
        //     case RaceNumbers.Asura:
        //         buttons[ButtonTypes.ClothingAccessoryType].Label.text = "Mask";
        //         break;
        //     case RaceNumbers.Kobolds:
        //         buttons[ButtonTypes.TailTypes].Label.text = "Preferred Facing";
        //         break;
        //     case RaceNumbers.Fairies:
        //         buttons[ButtonTypes.HatType].Label.text = "Leg Accessory";
        //         buttons[ButtonTypes.ClothingAccessoryType].Label.text = "Arm Accessory";
        //         buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Fairy Season";
        //         break;
        //     case RaceNumbers.Equines:
        //         buttons[ButtonTypes.ClothingExtraType1].Label.text = "Overtop";
        //         buttons[ButtonTypes.ClothingExtraType2].Label.text = "Overbottom";
        //         buttons[ButtonTypes.BodyAccentTypes3].Label.text = "Skin Pattern";
        //         buttons[ButtonTypes.BodyAccentTypes4].Label.text = "Head Pattern";
        //         buttons[ButtonTypes.BodyAccentTypes5].Label.text = "Torso Color";
        //         break;
        //     case RaceNumbers.Zera:
        //         buttons[ButtonTypes.TailTypes].Label.text = "Default Facing";
        //         break;
        //     case RaceNumbers.Bees:
        //         buttons[ButtonTypes.BodyAccessoryColor].Label.text = "Exoskeleton Color";
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Antennae Type";
        //         break;
        //     case RaceNumbers.Driders:
        //         buttons[ButtonTypes.BodyAccessoryColor].Label.text = "Spider Half Color";
        //         buttons[ButtonTypes.ExtraColor1].Label.text = "Spider Accent Color";
        //         break;
        //     case RaceNumbers.Alraune:
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Hair Accessory";
        //         buttons[ButtonTypes.ExtraColor1].Label.text = "Plant Colors";
        //         buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Inner Petals";
        //         buttons[ButtonTypes.BodyAccentTypes2].Label.text = "Outer Petals";
        //         buttons[ButtonTypes.BodyAccentTypes3].Label.text = "Plant Base";
        //         break;
        //     case RaceNumbers.Gryphons:
        //         buttons[ButtonTypes.Skintone].Label.text = "Body Color";
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Body Style";
        //         break;
        //     case RaceNumbers.Bats:
        //         buttons[ButtonTypes.BodyAccessoryColor].Label.text = "Fur Color";
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Ear Type";
        //         buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Collar Fur Type";
        //         break;
        //     case RaceNumbers.Panthers:
        //         Panther();
        //         break;
        //     case RaceNumbers.Salamanders:
        //         buttons[ButtonTypes.Skintone].Label.text = "Body Color";
        //         buttons[ButtonTypes.BodyAccessoryColor].Label.text = "Spine Color";
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Spine Type";
        //         break;
        //     case RaceNumbers.Vipers:
        //         buttons[ButtonTypes.Skintone].Label.text = "Body Color";
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Hood Type";
        //         buttons[ButtonTypes.ExtraColor1].Label.text = "Accent Color";
        //         buttons[ButtonTypes.TailTypes].Label.text = "Tail Pattern";
        //         buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Accent Pattern";
        //         break;
        //     case RaceNumbers.Merfolk:
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Head Fin";
        //         buttons[ButtonTypes.ClothingAccessoryType].Label.text = "Necklace / Hair Ornament";
        //         buttons[ButtonTypes.ExtraColor1].Label.text = "Scale Color";
        //         buttons[ButtonTypes.BodyAccentTypes2].Label.text = "Tail Fin";
        //         buttons[ButtonTypes.BodyAccentTypes3].Label.text = "Arm Fin";
        //         buttons[ButtonTypes.BodyAccentTypes4].Label.text = "Eyebrow";
        //
        //         break;
        //     case RaceNumbers.Avians:
        //         buttons[ButtonTypes.HairStyle].Label.text = "Head Type";
        //         buttons[ButtonTypes.BodyAccessoryColor].Label.text = "Beak Color";
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Head Pattern";
        //         buttons[ButtonTypes.ExtraColor1].Label.text = "Core Color";
        //         buttons[ButtonTypes.ExtraColor2].Label.text = "Feather Color";
        //         buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Underwing Palettes";
        //         break;
        //     case RaceNumbers.Hippos:
        //         buttons[ButtonTypes.BodyAccessoryColor].Label.text = "Accent Color";
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Ear Type";
        //         buttons[ButtonTypes.HatType].Label.text = "Headwear Type";
        //         buttons[ButtonTypes.ClothingAccessoryType].Label.text = "Necklace Type";
        //         buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Left Arm Pattern";
        //         buttons[ButtonTypes.BodyAccentTypes2].Label.text = "Right Arm Pattern";
        //         buttons[ButtonTypes.BodyAccentTypes3].Label.text = "Head Pattern";
        //         buttons[ButtonTypes.BodyAccentTypes4].Label.text = "Leg Pattern";
        //
        //         break;
        //     case RaceNumbers.Mantis:
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Antennae Type";
        //         buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Wing Type";
        //         buttons[ButtonTypes.BodyAccentTypes2].Label.text = "Back Spines";
        //
        //         break;
        //     case RaceNumbers.Auri:
        //         buttons[ButtonTypes.ClothingType].Label.text = "Breast Wrap";
        //         buttons[ButtonTypes.ClothingExtraType1].Label.text = "Kimono";
        //         buttons[ButtonTypes.ClothingExtraType2].Label.text = "Socks";
        //         buttons[ButtonTypes.ClothingExtraType3].Label.text = "Hair Ornament";
        //         buttons[ButtonTypes.TailTypes].Label.text = "Tail Quantity";
        //         buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Beast Mode";
        //
        //         break;
        //     case RaceNumbers.Catfish:
        //         buttons[ButtonTypes.Skintone].Label.text = "Body Color";
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Barbel (Whisker) Type";
        //         buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Dorsal Fin Type";
        //
        //         break;
        //     case RaceNumbers.Ants:
        //         buttons[ButtonTypes.BodyAccessoryColor].Label.text = "Exoskeleton Color";
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Antennae Type";
        //         break;
        //     case RaceNumbers.WarriorAnts:
        //         buttons[ButtonTypes.Skintone].Label.text = "Body Color";
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Antennae Type";
        //         break;
        //
        //     case RaceNumbers.Frogs:
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Primary Pattern Type";
        //         buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Secondary Pattern Type";
        //         buttons[ButtonTypes.BodyAccentTypes2].Label.text = "Extra Colors for Females";
        //         buttons[ButtonTypes.BodyAccessoryColor].Label.text = "Secondary Pattern Colors";
        //         break;
        //
        //     case RaceNumbers.Gazelle:
        //         buttons[ButtonTypes.Skintone].Label.text = "Fur Color";
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Ear Type";
        //         buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Fur Pattern";
        //         buttons[ButtonTypes.BodyAccentTypes2].Label.text = "Horn Type (for males)";
        //         break;
        //
        //     case RaceNumbers.Sharks:
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Ear Type";
        //         buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Body Pattern Type";
        //         buttons[ButtonTypes.BodyAccentTypes2].Label.text = "Secondary Pattern Type";
        //         buttons[ButtonTypes.BodyAccentTypes3].Label.text = "Nose Type";
        //         buttons[ButtonTypes.BodyAccessoryColor].Label.text = "Secondary Pattern Colors";
        //         buttons[ButtonTypes.ClothingExtraType1].Label.text = "Hats";
        //         break;
        //
        //     case RaceNumbers.Komodos:
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Body Pattern Type";
        //         buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Head Shape";
        //         buttons[ButtonTypes.BodyAccentTypes2].Label.text = "Secondary Pattern Type";
        //         buttons[ButtonTypes.BodyAccentTypes3].Label.text = "Head Pattern on/off";
        //         break;
        //
        //     case RaceNumbers.FeralLizards:
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Body Pattern Type";
        //         buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Visible Teeth (during attacks)";
        //         break;
        //
        //     case RaceNumbers.Cockatrice:
        //         buttons[ButtonTypes.BodyAccessoryColor].Label.text = "Feather Color";
        //         break;
        //
        //     case RaceNumbers.Monitors:
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Body Pattern Type";
        //         buttons[ButtonTypes.BodyAccessoryColor].Label.text = "Body Pattern Colors";
        //         break;
        //
        //     case RaceNumbers.Deer:
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Ear Type";
        //         buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Antlers Type";
        //         buttons[ButtonTypes.BodyAccentTypes2].Label.text = "Body Pattern Type";
        //         buttons[ButtonTypes.BodyAccentTypes3].Label.text = "Leg Type";
        //         break;
        //
        //     case RaceNumbers.Schiwardez:
        //         buttons[ButtonTypes.Skintone].Label.text = "Body Color";
        //         break;
        //
        //     case RaceNumbers.Terrorbird:
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Head Plumage Type";
        //         break;
        //
        //     case RaceNumbers.Erin:
        //         buttons[ButtonTypes.ClothingExtraType1].Label.text = "Panties";
        //         buttons[ButtonTypes.ClothingExtraType2].Label.text = "Stockings";
        //         buttons[ButtonTypes.ClothingExtraType3].Label.text = "Shoes";
        //         break;
        //
        //     case RaceNumbers.Vargul:
        //         buttons[ButtonTypes.BodyAccessoryType].Label.text = "Body Pattern Type";
        //         buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Ear Type";
        //         buttons[ButtonTypes.BodyAccentTypes2].Label.text = "Head Pattern Type";
        //         buttons[ButtonTypes.BodyAccentTypes3].Label.text = "Mask On/Off (for armors)";
        //         buttons[ButtonTypes.BodyAccessoryColor].Label.text = "Body Pattern Colors";
        //         buttons[ButtonTypes.ExtraColor1].Label.text = "Armor Details Color";
        //         break;
        //
        //     case RaceNumbers.FeralLions:
        //         buttons[ButtonTypes.Skintone].Label.text = "Fur Color";
        //         buttons[ButtonTypes.HairStyle].Label.text = "Mane Style";
        //         buttons[ButtonTypes.HairColor].Label.text = "Mane Color";
        //         break;
        //     case RaceNumbers.Aabayx:
        //         buttons[ButtonTypes.BodyAccessoryColor].Label.text = "Head Color";
        //         buttons[ButtonTypes.ClothingExtraType1].Label.text = "Face Paint";
        //         break;
        // }
    }

    private void RefreshGenderDropdown(Unit unit)
    {
        if (unit.HasBreasts)
        {
            if (unit.HasDick)
            {
                if (unit.HasVagina || Config.HermsCanUB == false)
                    CustomizerUI.Gender.value = 2;
                else
                    CustomizerUI.Gender.value = 3;
            }
            else
            {
                if (unit.HasVagina)
                    CustomizerUI.Gender.value = 1;
                else
                    CustomizerUI.Gender.value = 6;
            }
        }
        else
        {
            if (unit.HasDick)
            {
                if (unit.HasVagina)
                    CustomizerUI.Gender.value = 4;
                else
                    CustomizerUI.Gender.value = 0;
            }
            else
            {
                if (unit.HasVagina)
                    CustomizerUI.Gender.value = 5;
                else
                    // What in the hell--
                    CustomizerUI.Gender.value = 0;
            }
        }
        CustomizerUI.Gender.options[0].text = RaceData.SetupOutput.CanBeGender.Contains(Gender.Male) ? "Male" : "--";
        CustomizerUI.Gender.options[1].text = RaceData.SetupOutput.CanBeGender.Contains(Gender.Female) ? "Female" : "--";
        CustomizerUI.Gender.options[2].text = RaceData.SetupOutput.CanBeGender.Contains(Gender.Hermaphrodite) ? "Hermaphrodite" : "--";
        CustomizerUI.Gender.options[3].text = RaceData.SetupOutput.CanBeGender.Contains(Gender.Gynomorph) ? "Gynomorph" : "--";
        CustomizerUI.Gender.options[4].text = RaceData.SetupOutput.CanBeGender.Contains(Gender.Maleherm) ? "Maleherm" : "--";
        CustomizerUI.Gender.options[5].text = RaceData.SetupOutput.CanBeGender.Contains(Gender.Andromorph) ? "Andromorph" : "--";
        CustomizerUI.Gender.options[6].text = RaceData.SetupOutput.CanBeGender.Contains(Gender.Agenic) ? "Agenic" : "--";

    }

    private void RefreshPronouns(Unit unit)
    {
        CustomizerUI.Nominative.text = unit.GetPronoun(0);
        CustomizerUI.Accusative.text = unit.Pronouns[1];
        CustomizerUI.PronominalPossessive.text = unit.Pronouns[2];
        CustomizerUI.PredicativePossessive.text = unit.Pronouns[3];
        CustomizerUI.Reflexive.text = unit.Pronouns[4];
        if (Unit.Pronouns[5] == "singular")
            CustomizerUI.Quantification.value = 0;
        else
            CustomizerUI.Quantification.value = 1;
    }

    // void Panther()
    // {
    //     buttons[ButtonTypes.EyeType].Label.text = "Face Type";
    //     buttons[ButtonTypes.ClothingColor].Label.text = "Innerwear Color";
    //     buttons[ButtonTypes.ClothingColor2].Label.text = "Outerwear Color";
    //     buttons[ButtonTypes.ClothingColor3].Label.text = "Clothing Accent Color";
    //     buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Arm Bodypaint";
    //     buttons[ButtonTypes.BodyAccentTypes2].Label.text = "Shoulder Bodypaint";
    //     buttons[ButtonTypes.BodyAccentTypes3].Label.text = "Feet Bodypaint";
    //     buttons[ButtonTypes.BodyAccentTypes4].Label.text = "Thigh Bodypaint";
    //     buttons[ButtonTypes.BodyAccentTypes5].Label.text = "Face Bodypaint";
    //     buttons[ButtonTypes.BodyAccessoryColor].Label.text = "Bodypaint Color";
    //     buttons[ButtonTypes.ClothingExtraType1].Label.text = "Over Tops";
    //     buttons[ButtonTypes.ClothingExtraType2].Label.text = "Over Bottoms";
    //     buttons[ButtonTypes.ClothingExtraType3].Label.text = "Hats";
    //     buttons[ButtonTypes.ClothingExtraType4].Label.text = "Gloves";
    //     buttons[ButtonTypes.ClothingExtraType5].Label.text = "Legs";
    // }
    //
    // void Imp()
    // {
    //     buttons[ButtonTypes.BodyAccessoryColor].Label.text = "Body Accent Color";
    //     buttons[ButtonTypes.ClothingType].Label.text = "Under Tops";
    //     buttons[ButtonTypes.Clothing2Type].Label.text = "Under Bottoms";
    //     buttons[ButtonTypes.ClothingExtraType1].Label.text = "Over Bottoms";
    //     buttons[ButtonTypes.ClothingExtraType2].Label.text = "Over Tops";
    //     buttons[ButtonTypes.ClothingExtraType3].Label.text = "Legs";
    //     buttons[ButtonTypes.ClothingExtraType4].Label.text = "Gloves";
    //     buttons[ButtonTypes.ClothingExtraType5].Label.text = "Hats";
    //     buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Center Pattern";
    //     buttons[ButtonTypes.BodyAccentTypes2].Label.text = "Outer Pattern";
    //     buttons[ButtonTypes.BodyAccentTypes3].Label.text = "Horn Type";
    //     buttons[ButtonTypes.BodyAccentTypes4].Label.text = "Special Type";
    // }
    //
    // void Goblin()
    // {
    //     buttons[ButtonTypes.BodyAccessoryColor].Label.text = "Body Accent Color";
    //     buttons[ButtonTypes.ClothingType].Label.text = "Under Tops";
    //     buttons[ButtonTypes.Clothing2Type].Label.text = "Under Bottoms";
    //     buttons[ButtonTypes.ClothingExtraType1].Label.text = "Over Bottoms";
    //     buttons[ButtonTypes.ClothingExtraType2].Label.text = "Over Tops";
    //     buttons[ButtonTypes.ClothingExtraType3].Label.text = "Legs";
    //     buttons[ButtonTypes.ClothingExtraType4].Label.text = "Gloves";
    //     buttons[ButtonTypes.ClothingExtraType5].Label.text = "Hats";
    //     buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Ear Type";
    //     buttons[ButtonTypes.BodyAccentTypes2].Label.text = "Eyebrow Type";
    // }
    //
    // void Lizard()
    // {
    //     buttons[ButtonTypes.Skintone].gameObject.SetActive(false);
    //     buttons[ButtonTypes.HairColor].gameObject.SetActive(true);
    //     buttons[ButtonTypes.HairColor].Label.text = "Horn Color";
    //     buttons[ButtonTypes.HairStyle].Label.text = "Horn Style";
    //     buttons[ButtonTypes.BodyAccessoryColor].Label.text = "Body Color";
    //     buttons[ButtonTypes.ClothingExtraType1].Label.text = "Leg Guards";
    //     buttons[ButtonTypes.ClothingExtraType2].Label.text = "Armlets";
    //     buttons[ButtonTypes.HatType].Label.text = "Crown";
    // }
    //
    // void Slime()
    // {
    //     buttons[ButtonTypes.HairColor].gameObject.SetActive(true);
    //     buttons[ButtonTypes.Skintone].gameObject.SetActive(false);
    //     buttons[ButtonTypes.BodyAccessoryColor].Label.text = "Body Color";
    //     buttons[ButtonTypes.HairColor].Label.text = "Secondary Color";
    //     if (Unit.Type == UnitType.Leader)
    //     {
    //         buttons[ButtonTypes.ExtraColor1].Label.text = "Breast Covering";
    //         buttons[ButtonTypes.ExtraColor2].Label.text = "Cock Covering";
    //     }
    //
    // }
    //
    // void Harpy()
    // {
    //     buttons[ButtonTypes.ExtraColor1].Label.text = "Upper Feathers";
    //     buttons[ButtonTypes.ExtraColor2].Label.text = "Middle Feathers";
    //     buttons[ButtonTypes.ExtraColor3].Label.text = "Lower Feathers";
    //     buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Lower Feather brightness";
    // }
    //
    // void Crux()
    // {
    //     buttons[ButtonTypes.ClothingColor2].Label.text = "Pack / Boxer Color";
    //     buttons[ButtonTypes.ClothingColor2].gameObject.SetActive(true);
    //     buttons[ButtonTypes.BodyWeight].Label.text = "Body Type";
    //     buttons[ButtonTypes.EyeType].Label.text = "Face Expression";
    //     buttons[ButtonTypes.ExtraColor1].Label.text = "Primary Color";
    //     buttons[ButtonTypes.ExtraColor2].Label.text = "Secondary Color";
    //     buttons[ButtonTypes.ExtraColor3].Label.text = "Flesh Color";
    //     buttons[ButtonTypes.ExtraColor4].gameObject.SetActive(Config.HideCocks == false && actor.Unit.GetBestMelee().Damage == 4);
    //     buttons[ButtonTypes.ExtraColor4].Label.text = "Dildo Color";
    //     buttons[ButtonTypes.FurTypes].Label.text = "Head Fluff";
    //     buttons[ButtonTypes.BodyAccentTypes1].Label.text = "Visible Areola";
    //     buttons[ButtonTypes.BodyAccentTypes2].Label.text = "Leg Stripes";
    //     buttons[ButtonTypes.BodyAccentTypes3].Label.text = "Arm Stripes";
    // }
    //
    // void Lamia()
    // {
    //     buttons[ButtonTypes.BodyAccessoryColor].Label.text = "Scale Color";
    //     buttons[ButtonTypes.ExtraColor1].Label.text = "Accent Color";
    //     buttons[ButtonTypes.ExtraColor2].Label.text = "Tail Pattern Color";
    // }


    internal void RefreshGenderSelector()
    {
        if (actor.Unit.HasBreasts)
        {
            if (actor.Unit.HasDick)
            {
                if (actor.Unit.HasVagina)
                    CustomizerUI.Gender.value = 2;
                else
                    CustomizerUI.Gender.value = 3;
            }
            else
            {
                if (actor.Unit.HasVagina)
                    CustomizerUI.Gender.value = 1;
                else
                    CustomizerUI.Gender.value = 6;
            }
        }
        else
        {
            if (actor.Unit.HasDick)
            {
                if (actor.Unit.HasVagina)
                    CustomizerUI.Gender.value = 4;
                else
                    CustomizerUI.Gender.value = 0;
            }
            else
            {
                if (actor.Unit.HasVagina)
                    CustomizerUI.Gender.value = 5;
                else
                    // What in the hell--
                    CustomizerUI.Gender.value = 0;
            }
        }
        if (RaceData.SetupOutput.CanBeGender.Count <= 2)
        {

        }
    }

    internal void RefreshView()
    {
        actor.UpdateBestWeapons();
        CustomizerUI.DisplayedSprite.UpdateSprites(actor);
        CustomizerUI.DisplayedSprite.Name.text = Unit.Name;
    }

    private void CreateButtons()
    {
        buttons[ButtonType.Skintone] = CreateNewButton("Skintone", ChangeSkinTone);
        buttons[ButtonType.HairColor] = CreateNewButton("Hair Color", ChangeHairColor);
        buttons[ButtonType.HairStyle] = CreateNewButton("Hair Style", ChangeHairStyle);
        buttons[ButtonType.BeardStyle] = CreateNewButton("Beard Style", ChangeBeardStyle);
        buttons[ButtonType.BodyAccessoryColor] = CreateNewButton("Body Accessory Color", ChangeBodyAccessoryColor);
        buttons[ButtonType.BodyAccessoryType] = CreateNewButton("Body Accessory Type", ChangeBodyAccessoryType);
        buttons[ButtonType.HeadType] = CreateNewButton("Head Type", ChangeHeadType);
        buttons[ButtonType.EyeColor] = CreateNewButton("Eye Color", ChangeEyeColor);
        buttons[ButtonType.EyeType] = CreateNewButton("Eye Type", ChangeEyeType);
        buttons[ButtonType.MouthType] = CreateNewButton("Mouth Type", ChangeMouthType);
        buttons[ButtonType.BreastSize] = CreateNewButton("Breast Size", ChangeBreastSize);
        buttons[ButtonType.CockSize] = CreateNewButton("Cock Size", ChangeDickSize);
        buttons[ButtonType.BodyWeight] = CreateNewButton("Body Weight", ChangeBodyWeight);
        buttons[ButtonType.ClothingType] = CreateNewButton("Main Clothing Type", ChangeClothingType);
        buttons[ButtonType.Clothing2Type] = CreateNewButton("Waist Clothing Type", ChangeClothing2Type);
        buttons[ButtonType.ClothingExtraType1] = CreateNewButton("Extra Clothing Type 1", ChangeExtraClothing1Type);
        buttons[ButtonType.ClothingExtraType2] = CreateNewButton("Extra Clothing Type 2", ChangeExtraClothing2Type);
        buttons[ButtonType.ClothingExtraType3] = CreateNewButton("Extra Clothing Type 3", ChangeExtraClothing3Type);
        buttons[ButtonType.ClothingExtraType4] = CreateNewButton("Extra Clothing Type 4", ChangeExtraClothing4Type);
        buttons[ButtonType.ClothingExtraType5] = CreateNewButton("Extra Clothing Type 5", ChangeExtraClothing5Type);
        buttons[ButtonType.HatType] = CreateNewButton("Hat Type", ChangeClothingHatType);
        buttons[ButtonType.ClothingAccessoryType] = CreateNewButton("Clothing Accessory Type", ChangeClothingAccesoryType);
        buttons[ButtonType.ClothingColor] = CreateNewButton("Clothing Color", ChangeClothingColor);
        buttons[ButtonType.ClothingColor2] = CreateNewButton("Clothing Color 2", ChangeClothingColor2);
        buttons[ButtonType.ClothingColor3] = CreateNewButton("Clothing Color 3", ChangeClothingColor3);
        buttons[ButtonType.ExtraColor1] = CreateNewButton("Extra Color 1", ChangeExtraColor1);
        buttons[ButtonType.ExtraColor2] = CreateNewButton("Extra Color 2", ChangeExtraColor2);
        buttons[ButtonType.ExtraColor3] = CreateNewButton("Extra Color 3", ChangeExtraColor3);
        buttons[ButtonType.ExtraColor4] = CreateNewButton("Extra Color 4", ChangeExtraColor4);
        buttons[ButtonType.Furry] = CreateNewButton("Furry", ChangeFurriness);
        buttons[ButtonType.TailTypes] = CreateNewButton("Tail Types", ChangeTailType);
        buttons[ButtonType.FurTypes] = CreateNewButton("Fur Types", ChangeFurType);
        buttons[ButtonType.EarTypes] = CreateNewButton("Ear Types", ChangeEarType);
        buttons[ButtonType.BodyAccentTypes1] = CreateNewButton("BATypes1", ChangeBodyAccentTypes1Type);
        buttons[ButtonType.BodyAccentTypes2] = CreateNewButton("BATypes2", ChangeBodyAccentTypes2Type);
        buttons[ButtonType.BodyAccentTypes3] = CreateNewButton("BATypes3", ChangeBodyAccentTypes3Type);
        buttons[ButtonType.BodyAccentTypes4] = CreateNewButton("BATypes4", ChangeBodyAccentTypes4Type);
        buttons[ButtonType.BodyAccentTypes5] = CreateNewButton("BATypes5", ChangeBodyAccentTypes5Type);
        buttons[ButtonType.BallsSizes] = CreateNewButton("Ball Size", ChangeBallsSize);
        buttons[ButtonType.VulvaTypes] = CreateNewButton("Vulva Type", ChangeVulvaType);
        buttons[ButtonType.AltWeaponTypes] = CreateNewButton("Alt Weapon Sprite", ChangeWeaponSprite);
    }

    private CustomizerButton CreateNewButton(string text, Action<int> action)
    {
        CustomizerButton button = UnityEngine.Object.Instantiate(CustomizerUI.ButtonPrefab, CustomizerUI.ButtonPanel.transform).GetComponent<CustomizerButton>();
        button.SetData(text, action);
        return button;
    }


    public void RandomizeUnit()
    {
        RaceData.RandomCustomCall(Unit);
        RefreshView();
    }


    private void ChangeSkinTone(int change)
    {
        Unit.SkinColor = (RaceData.SetupOutput.SkinColors + Unit.SkinColor + change) % RaceData.SetupOutput.SkinColors;
        RefreshView();
    }

    private void ChangeHairColor(int change)
    {
        Unit.HairColor = (RaceData.SetupOutput.HairColors + Unit.HairColor + change) % RaceData.SetupOutput.HairColors;
        if (Equals(Unit.Race, Race.Cat) | Equals(Unit.Race, Race.Dog) | Equals(Unit.Race, Race.Bunny) | Equals(Unit.Race, Race.Wolf) | Equals(Unit.Race, Race.Fox) | Equals(Unit.Race, Race.Tiger))
        {
            buttons[ButtonType.HairColor].Label.text = "Hair Color: " + HairColorLookup(Unit.HairColor);
        }
        RefreshView();
    }

    private void ChangeEyeColor(int change)
    {
        Unit.EyeColor = (RaceData.SetupOutput.EyeColors + Unit.EyeColor + change) % RaceData.SetupOutput.EyeColors;
        RefreshView();
    }

    private void ChangeExtraColor1(int change)
    {
        Unit.ExtraColor1 = (RaceData.SetupOutput.ExtraColors1 + Unit.ExtraColor1 + change) % RaceData.SetupOutput.ExtraColors1;
        RefreshView();
    }

    private void ChangeExtraColor2(int change)
    {
        Unit.ExtraColor2 = (RaceData.SetupOutput.ExtraColors2 + Unit.ExtraColor2 + change) % RaceData.SetupOutput.ExtraColors2;
        RefreshView();
    }

    private void ChangeExtraColor3(int change)
    {
        Unit.ExtraColor3 = (RaceData.SetupOutput.ExtraColors3 + Unit.ExtraColor3 + change) % RaceData.SetupOutput.ExtraColors3;
        RefreshView();
    }

    private void ChangeExtraColor4(int change)
    {
        Unit.ExtraColor4 = (RaceData.SetupOutput.ExtraColors4 + Unit.ExtraColor4 + change) % RaceData.SetupOutput.ExtraColors4;
        RefreshView();
    }

    private void ChangeEyeType(int change)
    {
        Unit.EyeType = (RaceData.SetupOutput.EyeTypes + Unit.EyeType + change) % RaceData.SetupOutput.EyeTypes;
        RefreshView();
    }

    private void ChangeHairStyle(int change)
    {
        Unit.HairStyle = (RaceData.SetupOutput.HairStyles + Unit.HairStyle + change) % RaceData.SetupOutput.HairStyles;

        RefreshView();
    }

    private void ChangeBeardStyle(int change)
    {
        Unit.BeardStyle = (RaceData.SetupOutput.BeardStyles + Unit.BeardStyle + change) % RaceData.SetupOutput.BeardStyles;

        RefreshView();
    }


    private void ChangeBodyAccessoryColor(int change)
    {
        Unit.AccessoryColor = (RaceData.SetupOutput.AccessoryColors + Unit.AccessoryColor + change) % RaceData.SetupOutput.AccessoryColors;
        if (Equals(Unit.Race, Race.Cat) | Equals(Unit.Race, Race.Dog) | Equals(Unit.Race, Race.Bunny) | Equals(Unit.Race, Race.Wolf) | Equals(Unit.Race, Race.Fox) | Equals(Unit.Race, Race.Tiger))
            buttons[ButtonType.BodyAccessoryColor].Label.text = "Fur Color: " + HairColorLookup(Unit.AccessoryColor);
        RefreshView();
    }

    private void ChangeBodyAccessoryType(int change)
    {
        Unit.SpecialAccessoryType = (RaceData.SetupOutput.SpecialAccessoryCount + Unit.SpecialAccessoryType + change) % RaceData.SetupOutput.SpecialAccessoryCount;
        RefreshView();
    }

    internal void ChangeGender()
    {
        bool changedGender = false;
        if (CustomizerUI.Gender.value == 0 && Unit.GetGender() != Gender.Male)
        {
            if (RaceData.SetupOutput.CanBeGender.Contains(Gender.Male) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = State.Rand.Next(RaceData.SetupOutput.DickSizes());
            Unit.HasVagina = false;
            Unit.SetDefaultBreastSize(-1);
        }
        else if (CustomizerUI.Gender.value == 1 && Unit.GetGender() != Gender.Female)
        {
            if (RaceData.SetupOutput.CanBeGender.Contains(Gender.Female) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = -1;
            Unit.HasVagina = true;
            Unit.SetDefaultBreastSize(State.Rand.Next(RaceData.SetupOutput.BreastSizes()));
        }
        else if (CustomizerUI.Gender.value == 2 && Unit.GetGender() != Gender.Hermaphrodite)
        {
            if (RaceData.SetupOutput.CanBeGender.Contains(Gender.Hermaphrodite) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = State.Rand.Next(RaceData.SetupOutput.DickSizes());
            Unit.HasVagina = Config.HermsCanUB;
            Unit.SetDefaultBreastSize(State.Rand.Next(RaceData.SetupOutput.BreastSizes()));
        }
        else if (CustomizerUI.Gender.value == 3 && Unit.GetGender() != Gender.Gynomorph)
        {
            if (RaceData.SetupOutput.CanBeGender.Contains(Gender.Gynomorph) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = State.Rand.Next(RaceData.SetupOutput.DickSizes());
            Unit.HasVagina = false;
            Unit.SetDefaultBreastSize(State.Rand.Next(RaceData.SetupOutput.BreastSizes()));
        }
        else if (CustomizerUI.Gender.value == 4 && Unit.GetGender() != Gender.Maleherm)
        {
            if (RaceData.SetupOutput.CanBeGender.Contains(Gender.Maleherm) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = State.Rand.Next(RaceData.SetupOutput.DickSizes());
        }
        else if (CustomizerUI.Gender.value == 5 && Unit.GetGender() != Gender.Andromorph)
        {
            if (RaceData.SetupOutput.CanBeGender.Contains(Gender.Andromorph) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = -1;
            Unit.HasVagina = true;
            Unit.SetDefaultBreastSize(-1);
        }
        else if (CustomizerUI.Gender.value == 6 && Unit.GetGender() != Gender.Agenic)
        {
            if (RaceData.SetupOutput.CanBeGender.Contains(Gender.Agenic) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = -1;
            Unit.HasVagina = false;
            Unit.SetDefaultBreastSize(State.Rand.Next(RaceData.SetupOutput.BreastSizes()));
        }

        buttons[ButtonType.BreastSize].gameObject.SetActive(Unit.HasBreasts && RaceData.SetupOutput.BreastSizes() > 1);
        buttons[ButtonType.CockSize].gameObject.SetActive(Unit.HasDick && RaceData.SetupOutput.DickSizes() > 1);
        if (changedGender)
        {
            if (CustomizerUI.Gender.value == 0 || CustomizerUI.Gender.value == 5)
            {
                CustomizerUI.Nominative.text = "he";
                CustomizerUI.Accusative.text = "him";
                CustomizerUI.PronominalPossessive.text = "his";
                CustomizerUI.PredicativePossessive.text = "his";
                CustomizerUI.Reflexive.text = "himself";
                CustomizerUI.Quantification.value = 0;
            }
            else if (CustomizerUI.Gender.value == 1)
            {
                CustomizerUI.Nominative.text = "she";
                CustomizerUI.Accusative.text = "her";
                CustomizerUI.PronominalPossessive.text = "her";
                CustomizerUI.PredicativePossessive.text = "hers";
                CustomizerUI.Reflexive.text = "herself";
                CustomizerUI.Quantification.value = 0;
            }
            else
            {
                CustomizerUI.Nominative.text = "they";
                CustomizerUI.Accusative.text = "them";
                CustomizerUI.PronominalPossessive.text = "their";
                CustomizerUI.PredicativePossessive.text = "theirs";
                CustomizerUI.Reflexive.text = "themself";
                CustomizerUI.Quantification.value = 1;
            }
            RefreshPronouns(Unit);
            Unit.ReloadTraits();
            Unit.InitializeTraits();
            RefreshView();
        }

    }

    internal void ChangePronouns()
    {
        if (Unit.Pronouns == null)
            Unit.GeneratePronouns();
        Unit.Pronouns[0] = CustomizerUI.Nominative.text;
        Unit.Pronouns[1] = CustomizerUI.Accusative.text;
        Unit.Pronouns[2] = CustomizerUI.PronominalPossessive.text;
        Unit.Pronouns[3] = CustomizerUI.PredicativePossessive.text;
        Unit.Pronouns[4] = CustomizerUI.Reflexive.text;
        if (CustomizerUI.Quantification.value == 0)
            Unit.Pronouns[5] = "singular";
        else
            Unit.Pronouns[5] = "plural";
    }

    private void ChangeBreastSize(int change)
    {
        Unit.SetDefaultBreastSize((RaceData.SetupOutput.BreastSizes() + Unit.BreastSize + change) % RaceData.SetupOutput.BreastSizes());
        RefreshView();
    }


    private void ChangeDickSize(int change)
    {
        Unit.DickSize = (RaceData.SetupOutput.DickSizes() + Unit.DickSize + change) % RaceData.SetupOutput.DickSizes();
        RefreshView();
    }

    private void ChangeMouthType(int change)
    {
        Unit.MouthType = (RaceData.SetupOutput.MouthTypes + Unit.MouthType + change) % RaceData.SetupOutput.MouthTypes;
        RefreshView();
    }

    private void ChangeBodyWeight(int change)
    {
        if (Unit.BodySizeManuallyChanged == false)
            Unit.BodySize = Config.DefaultStartingWeight;
        Unit.BodySizeManuallyChanged = true;
        Unit.BodySize = (RaceData.SetupOutput.BodySizes + Unit.BodySize + change) % RaceData.SetupOutput.BodySizes;
        RefreshView();
    }

    private void ChangeClothingType(int change)
    {
        int totalClothingTypes = RaceData.SetupOutput.MainClothingTypesCount;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingType = 0;
            return;
        }

        if (Unit.ClothingType > RaceData.SetupOutput.MainClothingTypesCount)
            Unit.ClothingType = 0;

        Unit.ClothingType = (totalClothingTypes + Unit.ClothingType + change) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingType == 0)
                break;
            if (RaceData.SetupOutput.AllowedMainClothingTypes[Unit.ClothingType - 1].CanWear(Unit))
                break;
            Unit.ClothingType = (totalClothingTypes + Unit.ClothingType + change) % totalClothingTypes;
        }
        RefreshView();
    }

    private void ChangeClothing2Type(int change)
    {
        int totalClothingTypes = RaceData.SetupOutput.WaistClothingTypesCount;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingType2 = 0;
            return;
        }

        if (Unit.ClothingType2 > RaceData.SetupOutput.WaistClothingTypesCount)
            Unit.ClothingType2 = 0;

        Unit.ClothingType2 = (totalClothingTypes + Unit.ClothingType2 + change) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingType2 == 0)
                break;
            if (RaceData.SetupOutput.AllowedWaistTypes[Unit.ClothingType2 - 1].CanWear(Unit))
                break;
            Unit.ClothingType2 = (totalClothingTypes + Unit.ClothingType2 + change) % totalClothingTypes;
        }
        RefreshView();
    }

    private void ChangeExtraClothing1Type(int change)
    {
        int totalClothingTypes = RaceData.SetupOutput.ExtraMainClothing1Count;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingExtraType1 = 0;
            return;
        }

        if (Unit.ClothingExtraType1 > RaceData.SetupOutput.ExtraMainClothing1Count)
            Unit.ClothingExtraType1 = 0;

        Unit.ClothingExtraType1 = (totalClothingTypes + Unit.ClothingExtraType1 + change) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingExtraType1 == 0)
                break;
            if (RaceData.SetupOutput.ExtraMainClothing1Types[Unit.ClothingExtraType1 - 1].CanWear(Unit))
                break;
            Unit.ClothingExtraType1 = (totalClothingTypes + Unit.ClothingExtraType1 + change) % totalClothingTypes;
        }
        RefreshView();
    }

    private void ChangeExtraClothing2Type(int change)
    {
        int totalClothingTypes = RaceData.SetupOutput.ExtraMainClothing2Count;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingExtraType2 = 0;
            return;
        }

        if (Unit.ClothingExtraType2 > RaceData.SetupOutput.ExtraMainClothing2Count)
            Unit.ClothingExtraType2 = 0;

        Unit.ClothingExtraType2 = (totalClothingTypes + Unit.ClothingExtraType2 + change) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingExtraType2 == 0)
                break;
            if (RaceData.SetupOutput.ExtraMainClothing2Types[Unit.ClothingExtraType2 - 1].CanWear(Unit))
                break;
            Unit.ClothingExtraType2 = (totalClothingTypes + Unit.ClothingExtraType2 + change) % totalClothingTypes;
        }
        RefreshView();
    }

    private void ChangeExtraClothing3Type(int change)
    {
        int totalClothingTypes = RaceData.SetupOutput.ExtraMainClothing3Count;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingExtraType3 = 0;
            return;
        }

        if (Unit.ClothingExtraType3 > RaceData.SetupOutput.ExtraMainClothing3Count)
            Unit.ClothingExtraType3 = 0;

        Unit.ClothingExtraType3 = (totalClothingTypes + Unit.ClothingExtraType3 + change) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingExtraType3 == 0)
                break;
            if (RaceData.SetupOutput.ExtraMainClothing3Types[Unit.ClothingExtraType3 - 1].CanWear(Unit))
                break;
            Unit.ClothingExtraType3 = (totalClothingTypes + Unit.ClothingExtraType3 + change) % totalClothingTypes;
        }
        RefreshView();
    }

    private void ChangeExtraClothing4Type(int change)
    {
        int totalClothingTypes = RaceData.SetupOutput.ExtraMainClothing4Count;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingExtraType4 = 0;
            return;
        }

        if (Unit.ClothingExtraType4 > RaceData.SetupOutput.ExtraMainClothing4Count)
            Unit.ClothingExtraType4 = 0;

        Unit.ClothingExtraType4 = (totalClothingTypes + Unit.ClothingExtraType4 + change) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingExtraType4 == 0)
                break;
            if (RaceData.SetupOutput.ExtraMainClothing4Types[Unit.ClothingExtraType4 - 1].CanWear(Unit))
                break;
            Unit.ClothingExtraType4 = (totalClothingTypes + Unit.ClothingExtraType4 + change) % totalClothingTypes;
        }
        RefreshView();
    }

    private void ChangeExtraClothing5Type(int change)
    {
        int totalClothingTypes = RaceData.SetupOutput.ExtraMainClothing5Count;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingExtraType5 = 0;
            return;
        }

        if (Unit.ClothingExtraType5 > RaceData.SetupOutput.ExtraMainClothing5Count)
            Unit.ClothingExtraType5 = 0;

        Unit.ClothingExtraType5 = (totalClothingTypes + Unit.ClothingExtraType5 + change) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingExtraType5 == 0)
                break;
            if (RaceData.SetupOutput.ExtraMainClothing5Types[Unit.ClothingExtraType5 - 1].CanWear(Unit))
                break;
            Unit.ClothingExtraType5 = (totalClothingTypes + Unit.ClothingExtraType5 + change) % totalClothingTypes;
        }
        RefreshView();
    }

    private void ChangeClothingAccesoryType(int change)
    {
        int totalClothingTypes = RaceData.SetupOutput.ClothingAccessoryTypesCount;
        if (Unit.EarnedMask && RaceFuncs.isMainRaceOrTigerOrSuccubiOrGoblin(Unit.Race) && !Equals(Unit.Race, Race.Lizard))
            totalClothingTypes += 1;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingAccessoryType = 0;
            return;
        }

        if (Unit.ClothingAccessoryType > totalClothingTypes)
            Unit.ClothingAccessoryType = 0;

        Unit.ClothingAccessoryType = (totalClothingTypes + Unit.ClothingAccessoryType + change) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingAccessoryType == 0)
                break;
            if (Unit.ClothingAccessoryType == RaceData.SetupOutput.ClothingAccessoryTypesCount && Unit.EarnedMask)
                break;
            if (RaceData.SetupOutput.AllowedClothingAccessoryTypes[Unit.ClothingAccessoryType - 1].CanWear(Unit))
                break;
            Unit.ClothingAccessoryType = (totalClothingTypes + Unit.ClothingAccessoryType + change) % totalClothingTypes;
        }
        RefreshView();
    }

    private void ChangeClothingHatType(int change)
    {
        int totalClothingTypes = RaceData.SetupOutput.ClothingHatTypesCount;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingHatType = 0;
            return;
        }

        if (Unit.ClothingHatType > RaceData.SetupOutput.ClothingHatTypesCount)
            Unit.ClothingHatType = 0;

        Unit.ClothingHatType = (totalClothingTypes + Unit.ClothingHatType + change) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingHatType == 0)
                break;
            if (RaceData.SetupOutput.AllowedClothingHatTypes[Unit.ClothingHatType - 1].CanWear(Unit))
                break;
            Unit.ClothingHatType = (totalClothingTypes + Unit.ClothingHatType + change) % totalClothingTypes;
        }
        RefreshView();
    }

    private void ChangeClothingColor(int change)
    {
        Unit.ClothingColor = (RaceData.SetupOutput.ClothingColors + Unit.ClothingColor + change) % RaceData.SetupOutput.ClothingColors;
        RefreshView();
    }

    private void ChangeClothingColor2(int change)
    {
        Unit.ClothingColor2 = (RaceData.SetupOutput.ClothingColors + Unit.ClothingColor2 + change) % RaceData.SetupOutput.ClothingColors;
        RefreshView();
    }

    private void ChangeClothingColor3(int change)
    {
        Unit.ClothingColor3 = (RaceData.SetupOutput.ClothingColors + Unit.ClothingColor3 + change) % RaceData.SetupOutput.ClothingColors;
        RefreshView();
    }

    private void ChangeFurriness(int change)
    {
        Unit.Furry = !Unit.Furry;
        RefreshView();
    }

    //    FurTypes = 11;
    //    EarTypes = 17;
    //    BodyAccentTypes1 = 2; // Used for checking if breasts have a visible areola or not.
    //    BodyAccentTypes2 = 7; // Leg Stripe patterns.
    //    BodyAccentTypes3 = 5; // Arm Stripe patterns.
    //    BallsSizes = 3;
    //    VulvaTypes = 3;
    //    BasicMeleeWeaponTypes = 2;
    //    AdvancedMeleeWeaponTypes = 2;
    //    BasicRangedWeaponTypes = 1;
    //    AdvancedRangedWeaponTypes = 1;

    private void ChangeHeadType(int change)
    {
        Unit.HeadType = (RaceData.SetupOutput.HeadTypes + Unit.HeadType + change) % RaceData.SetupOutput.HeadTypes;
        RefreshView();
    }

    private void ChangeTailType(int change)
    {
        Unit.TailType = (RaceData.SetupOutput.TailTypes + Unit.TailType + change) % RaceData.SetupOutput.TailTypes;
        RefreshView();
    }

    private void ChangeFurType(int change)
    {
        Unit.FurType = (RaceData.SetupOutput.FurTypes + Unit.FurType + change) % RaceData.SetupOutput.FurTypes;
        RefreshView();
    }

    private void ChangeEarType(int change)
    {
        Unit.EarType = (RaceData.SetupOutput.EarTypes + Unit.EarType + change) % RaceData.SetupOutput.EarTypes;
        RefreshView();
    }

    private void ChangeBodyAccentTypes1Type(int change)
    {
        Unit.BodyAccentType1 = (RaceData.SetupOutput.BodyAccentTypes1 + Unit.BodyAccentType1 + change) % RaceData.SetupOutput.BodyAccentTypes1;
        RefreshView();
    }

    private void ChangeBodyAccentTypes2Type(int change)
    {
        Unit.BodyAccentType2 = (RaceData.SetupOutput.BodyAccentTypes2 + Unit.BodyAccentType2 + change) % RaceData.SetupOutput.BodyAccentTypes2;
        RefreshView();
    }

    private void ChangeBodyAccentTypes3Type(int change)
    {
        Unit.BodyAccentType3 = (RaceData.SetupOutput.BodyAccentTypes3 + Unit.BodyAccentType3 + change) % RaceData.SetupOutput.BodyAccentTypes3;
        RefreshView();
    }

    private void ChangeBodyAccentTypes4Type(int change)
    {
        Unit.BodyAccentType4 = (RaceData.SetupOutput.BodyAccentTypes4 + Unit.BodyAccentType4 + change) % RaceData.SetupOutput.BodyAccentTypes4;
        RefreshView();
    }

    private void ChangeBodyAccentTypes5Type(int change)
    {
        Unit.BodyAccentType5 = (RaceData.SetupOutput.BodyAccentTypes5 + Unit.BodyAccentType5 + change) % RaceData.SetupOutput.BodyAccentTypes5;
        RefreshView();
    }

    private void ChangeBallsSize(int change)
    {
        Unit.BallsSize = (RaceData.SetupOutput.BallsSizes + Unit.BallsSize + change) % RaceData.SetupOutput.BallsSizes;
        RefreshView();
    }

    private void ChangeVulvaType(int change)
    {
        Unit.VulvaType = (RaceData.SetupOutput.VulvaTypes + Unit.VulvaType + change) % RaceData.SetupOutput.VulvaTypes;
        RefreshView();
    }

    private void ChangeWeaponSprite(int change)
    {
        int basicMeleeTypes = RaceData.SetupOutput.BasicMeleeWeaponTypes;
        if (Config.HideCocks == false)
            basicMeleeTypes++;
        Unit.BasicMeleeWeaponType = (basicMeleeTypes + Unit.BasicMeleeWeaponType + change) % basicMeleeTypes;

        Unit.AdvancedMeleeWeaponType = (RaceData.SetupOutput.AdvancedMeleeWeaponTypes + Unit.AdvancedMeleeWeaponType + change) % RaceData.SetupOutput.AdvancedMeleeWeaponTypes;
        Unit.BasicRangedWeaponType = (RaceData.SetupOutput.BasicRangedWeaponTypes + Unit.BasicRangedWeaponType + change) % RaceData.SetupOutput.BasicRangedWeaponTypes;
        Unit.AdvancedRangedWeaponType = (RaceData.SetupOutput.AdvancedRangedWeaponTypes + Unit.AdvancedRangedWeaponType + change) % RaceData.SetupOutput.AdvancedRangedWeaponTypes;
        RefreshView();
    }




}