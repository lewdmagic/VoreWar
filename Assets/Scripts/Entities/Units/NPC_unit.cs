public class NpcUnit : Unit
{
    public NpcUnit(int level, bool advancedWeapons, int type, Side side, Race race, int startingXp, bool canVore) : base(side, race, startingXp, canVore, type: type == 3 ? UnitType.Leader : UnitType.Soldier)
    {
        if (Equals(race, Race.Alligator) || Equals(race, Race.Komodo))
            GenMelee(level - 1, advancedWeapons);
        else if (RaceFuncs.IsMosnterOrUniqueMerc(race))
            GenMonster(level - 1);
        else if (FixedGear || Equals(race, Race.Succubus))
            StrategicUtilities.CheatForceLevelUps(this, level - 1);
        else
        {
            switch (type)
            {
                case 0:
                    GenMelee(level - 1, advancedWeapons);
                    break;
                case 1:
                    GenArcher(level - 1, advancedWeapons);
                    break;
                case 2:
                    GenGarrison(level - 1, advancedWeapons);
                    break;
                case 3:
                    GenLeader(level - 1, advancedWeapons, race);
                    break;
            }
        }

        if (startingXp == 0 && level > 1)
        {
            SetExp(GetExperienceRequiredForLevel(level - 1));
        }

        RestoreManaPct(1);
        GiveTraitBooks();
    }

    private void GenMonster(int desiredLevels)
    {
        StrategicUtilities.CheatForceLevelUps(this, desiredLevels);
    }

    private void GenGarrison(int levels, bool advancedWeapons)
    {
        if (BestSuitedForRanged())
            GenArcher(levels, advancedWeapons);
        else
            GenMelee(levels, advancedWeapons);
    }

    private void GenMelee(int levels, bool advancedWeapons)
    {
        if (advancedWeapons)
            Items[0] = State.World.ItemRepository.GetItem(ItemType.Axe);
        else
            Items[0] = State.World.ItemRepository.GetItem(ItemType.Mace);
        if (Stats[(int)Stat.Dexterity] > Stats[(int)Stat.Strength])
        {
            int temp = Stats[(int)Stat.Dexterity];
            Stats[(int)Stat.Dexterity] = Stats[(int)Stat.Strength];
            Stats[(int)Stat.Strength] = temp;
        }

        Stats[(int)Stat.Strength] += levels;
        Stats[(int)Stat.Voracity] += levels;
        Stats[(int)Stat.Stomach] += levels;
        Stats[(int)Stat.Agility] += levels;
        SetLevel(levels + 1);
        GeneralStatIncrease(levels);
        Health = MaxHealth;
    }

    private void GenArcher(int levels, bool advancedWeapons)
    {
        if (advancedWeapons)
            Items[0] = State.World.ItemRepository.GetItem(ItemType.CompoundBow);
        else
            Items[0] = State.World.ItemRepository.GetItem(ItemType.Bow);
        if (Stats[(int)Stat.Strength] > Stats[(int)Stat.Dexterity])
        {
            int temp = Stats[(int)Stat.Dexterity];
            Stats[(int)Stat.Dexterity] = Stats[(int)Stat.Strength];
            Stats[(int)Stat.Strength] = temp;
        }

        Stats[(int)Stat.Dexterity] += levels;
        Stats[(int)Stat.Voracity] += levels;
        Stats[(int)Stat.Stomach] += levels;
        Stats[(int)Stat.Agility] += levels;
        SetLevel(levels + 1);
        GeneralStatIncrease(levels);
        Health = MaxHealth;
    }

    private void GenLeader(int levels, bool ranged, Race race)
    {
        Type = UnitType.Leader;
        if (ranged)
            Items[0] = State.World.ItemRepository.GetItem(ItemType.CompoundBow);
        else
            Items[0] = State.World.ItemRepository.GetItem(ItemType.Axe);
        var raceStats = State.RaceSettings.GetRaceStats(Race);

        Stats[(int)Stat.Strength] = 5 + raceStats.Strength.Minimum + raceStats.Strength.Roll;
        Stats[(int)Stat.Dexterity] = 5 + raceStats.Dexterity.Minimum + raceStats.Dexterity.Roll;
        Stats[(int)Stat.Endurance] = 6 + raceStats.Endurance.Minimum + raceStats.Endurance.Roll;
        Stats[(int)Stat.Mind] = 6 + raceStats.Mind.Minimum + raceStats.Mind.Roll;
        Stats[(int)Stat.Will] = 6 + raceStats.Will.Minimum + raceStats.Will.Roll;
        Stats[(int)Stat.Agility] = 9 + raceStats.Agility.Minimum + raceStats.Agility.Roll;
        Stats[(int)Stat.Voracity] = 8 + raceStats.Voracity.Minimum + raceStats.Voracity.Roll;
        Stats[(int)Stat.Stomach] = 4 + raceStats.Stomach.Minimum + raceStats.Stomach.Roll;
        Stats[(int)Stat.Leadership] = 10;

        if (Equals(race, Race.Lizard)) RaceFuncs.GetRace(Race.Lizard).RandomCustomCall(this);
        if (Config.LetterBeforeLeaderNames != "") Name = Config.LetterBeforeLeaderNames + Name.ToLower();
        ExpMultiplier = 2;
        if (Equals(race, Race.Slime))
        {
            if (Config.HermFraction >= 0.05)
            {
                DickSize = 0;
            }
            else
            {
                DickSize = -1;
            }

            ClothingType = 1;
            SetDefaultBreastSize(1);
            HairStyle = 1;
            ClothingColor = 0;
            ClothingColor2 = 0;
            ClothingColor3 = 0;
            ReloadTraits();
            InitializeTraits();
        }

        if (ranged)
            Stats[(int)Stat.Dexterity] += levels;
        else
            Stats[(int)Stat.Strength] += levels;
        Stats[(int)Stat.Leadership] += 2 * levels;
        Stats[(int)Stat.Agility] += levels;
        SetLevel(levels + 1);
        GeneralStatIncrease(levels);
        Health = MaxHealth;
        if (Config.LeadersUseCustomizations)
        {
            var list = CustomizationDataStorer.GetCompatibleCustomizations(race, UnitType.Leader, false);
            if (list != null && list.Count > 0)
            {
                CustomizationDataStorer.ExternalCopyToUnit(list[State.Rand.Next(list.Count)], this);
            }
        }
    }
}