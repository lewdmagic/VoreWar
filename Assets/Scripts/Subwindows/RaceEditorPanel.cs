﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaceEditorPanel : MonoBehaviour
{
    public TMP_Dropdown RaceDropdown;
    public TMP_Dropdown DisplayRaceDropdown;

    public Toggle OverrideGender;

    public Slider MaleFraction;
    public Slider HermFraction;

    public Toggle OverrideFurry;
    public Slider FurryFraction;

    public InputField BodySize;
    public InputField StomachSize;

    public TMP_Dropdown InnateSpellDropdown;
    public TMP_Dropdown SpawnRaceDropdown;
    public TMP_Dropdown ConversionRaceDropdown;
    public TMP_Dropdown LeaderRaceDropdown;

    public TMP_Dropdown TraitDropdown;
    public TextMeshProUGUI TraitList;

    public Toggle UnbirthDisabled;
    public Toggle CockVoreDisabled;
    public Toggle BreastVoreDisabled;
    public Toggle TailVoreDisabled;
    public Toggle AnalVoreDisabled;

    public InputField MinStrength;
    public InputField MaxStrength;
    public InputField MinDexterity;
    public InputField MaxDexterity;
    public InputField MinEndurance;
    public InputField MaxEndurance;
    public InputField MinMind;
    public InputField MaxMind;
    public InputField MinWill;
    public InputField MaxWill;
    public InputField MinAgility;
    public InputField MaxAgility;
    public InputField MinVoracity;
    public InputField MaxVoracity;
    public InputField MinStomach;
    public InputField MaxStomach;

    public InputField PowerAdjustment;

    public Toggle OverrideClothed;
    public Slider ClothedFraction;
    public Toggle OverrideWeight;
    public InputField MinWeight;
    public InputField MaxWeight;
    public Toggle OverrideBoob;
    public InputField MinBoob;
    public InputField MaxBoob;
    public Toggle OverrideDick;
    public InputField MinDick;
    public InputField MaxDick;

    public Text WeightText;
    public Text BoobText;
    public Text DickText;

    public InputField MaleTraits;
    public InputField FemaleTraits;
    public InputField HermTraits;
    public InputField SpawnTraits;
    public InputField LeaderTraits;

    public TMP_Dropdown BannerType;

    public TMP_Dropdown FavoredStat;

    public TMP_Dropdown RaceAIDropdown;

    public TextMeshProUGUI InfoText;

    public GameObject GeneralPanel;
    public GameObject TraitsPanel;

    public Button GeneralButton;
    public Button TraitsButton;

    private List<TraitType> _currentTraits;


    private Race _previousRace = Race.TrueNone;

    internal void ShowPanel()
    {
        if (RaceDropdown.options?.Any() == false)
        {
            foreach (Race race in RaceFuncs.RaceEnumerable().Where(s => RaceFuncs.IsNotNone(s)).OrderBy((s) => s.ToString()))
            {
                RaceDropdown.options.Add(new TMP_Dropdown.OptionData(race.ToString()));
            }

            RaceDropdown.RefreshShownValue();
        }

        TraitDropdown.options.Clear();
        foreach (RandomizeList rl in State.RandomizeLists)
        {
            TraitDropdown.options.Add(new TMP_Dropdown.OptionData(rl.Name.ToString()));
        }

        foreach (TraitType traitId in ((TraitType[])Enum.GetValues(typeof(TraitType))).OrderBy(s => { return s >= TraitType.LightningSpeed ? "ZZZ" + s.ToString() : s.ToString(); }))
        {
            TraitDropdown.options.Add(new TMP_Dropdown.OptionData(traitId.ToString()));
        }

        TraitDropdown.RefreshShownValue();

        if (FavoredStat.options?.Any() == false)
        {
            foreach (Stat stat in ((Stat[])Enum.GetValues(typeof(TraitType))).Where((s) => s < Stat.Leadership))
            {
                FavoredStat.options.Add(new TMP_Dropdown.OptionData(stat.ToString()));
            }

            FavoredStat.RefreshShownValue();
        }

        if (InnateSpellDropdown.options?.Any() == false)
        {
            foreach (SpellType type in ((SpellType[])Enum.GetValues(typeof(SpellType))).Where(s => (int)s < 100))
            {
                InnateSpellDropdown.options.Add(new TMP_Dropdown.OptionData(type.ToString()));
            }

            InnateSpellDropdown.RefreshShownValue();
        }

        if (SpawnRaceDropdown.options?.Any() == false)
        {
            foreach (Race race in RaceFuncs.RaceEnumerable().OrderBy((s) => s.ToString()))
            {
                SpawnRaceDropdown.options.Add(new TMP_Dropdown.OptionData(race.ToString()));
            }

            SpawnRaceDropdown.RefreshShownValue();
        }

        if (ConversionRaceDropdown.options?.Any() == false)
        {
            foreach (Race race in RaceFuncs.RaceEnumerable().OrderBy((s) => s.ToString()))
            {
                ConversionRaceDropdown.options.Add(new TMP_Dropdown.OptionData(race.ToString()));
            }

            ConversionRaceDropdown.RefreshShownValue();
        }

        if (LeaderRaceDropdown.options?.Any() == false)
        {
            foreach (Race race in RaceFuncs.RaceEnumerable().OrderBy((s) => s.ToString()))
            {
                LeaderRaceDropdown.options.Add(new TMP_Dropdown.OptionData(race.ToString()));
            }

            LeaderRaceDropdown.RefreshShownValue();
        }

        if (BannerType.options.Count < 4)
        {
            foreach (BannerType type in (BannerType[])Enum.GetValues(typeof(BannerType)))
            {
                BannerType.options.Add(new TMP_Dropdown.OptionData(type.ToString()));
            }

            for (int i = 0; i < CustomBannerTest.Sprites.Length; i++)
            {
                if (CustomBannerTest.Sprites[i] == null) break;
                BannerType.options.Add(new TMP_Dropdown.OptionData($"Custom {i + 1}"));
            }
        }

        if (RaceAIDropdown.options?.Any() == false)
        {
            foreach (RaceAI raceAI in (RaceAI[])Enum.GetValues(typeof(RaceAI)))
            {
                RaceAIDropdown.options.Add(new TMP_Dropdown.OptionData(raceAI.ToString()));
            }

            RaceAIDropdown.RefreshShownValue();
        }

        LoadRace();
        UpdateInteractable();
        ActivateGeneral();
    }

    public void RaceChanged()
    {
        SaveRace();
        LoadRace();
        UpdateInteractable();
    }


    public void AddTrait()
    {
        if (State.RandomizeLists.Any(rl => rl.Name == TraitDropdown.options[TraitDropdown.value].text))
        {
            _currentTraits.Add((TraitType)State.RandomizeLists.Where(rl => rl.Name == TraitDropdown.options[TraitDropdown.value].text).FirstOrDefault()?.ID);
        }

        if (Enum.TryParse(TraitDropdown.options[TraitDropdown.value].text, out TraitType trait))
        {
            if (_currentTraits.Contains(trait) == false) _currentTraits.Add(trait);
        }

        UpdateInteractable();
    }

    public void AddTraitAll()
    {
        if (State.RandomizeLists.Any(rl => rl.Name == TraitDropdown.options[TraitDropdown.value].text))
        {
            foreach (Race race in RaceFuncs.RaceEnumerable())
            {
                RaceSettingsItem item = State.RaceSettings.Get(race);
                item.RaceTraits.Add((TraitType)State.RandomizeLists.Where(rl => rl.Name == TraitDropdown.options[TraitDropdown.value].text).FirstOrDefault()?.ID);
            }

            AddTrait();
        }

        if (Enum.TryParse(TraitDropdown.options[TraitDropdown.value].text, out TraitType trait))
        {
            foreach (Race race in RaceFuncs.RaceEnumerable())
            {
                RaceSettingsItem item = State.RaceSettings.Get(race);
                if (item.RaceTraits.Contains(trait) == false) item.RaceTraits.Add(trait);
            }

            AddTrait();
        }
    }

    public void RemoveTrait()
    {
        if (State.RandomizeLists.Any(rl => rl.Name == TraitDropdown.options[TraitDropdown.value].text))
        {
            _currentTraits.Remove((TraitType)State.RandomizeLists.Where(rl => rl.Name == TraitDropdown.options[TraitDropdown.value].text).FirstOrDefault()?.ID);
        }

        if (Enum.TryParse(TraitDropdown.options[TraitDropdown.value].text, out TraitType trait))
        {
            _currentTraits.Remove(trait);
        }

        UpdateInteractable();
    }

    public void RemoveTraitAll()
    {
        if (State.RandomizeLists.Any(rl => rl.Name == TraitDropdown.options[TraitDropdown.value].text))
        {
            foreach (Race race in RaceFuncs.RaceEnumerable())
            {
                RaceSettingsItem item = State.RaceSettings.Get(race);
                item.RaceTraits.Remove((TraitType)State.RandomizeLists.Where(rl => rl.Name == TraitDropdown.options[TraitDropdown.value].text).FirstOrDefault()?.ID);
            }

            RemoveTrait();
        }

        if (Enum.TryParse(TraitDropdown.options[TraitDropdown.value].text, out TraitType trait))
        {
            foreach (Race race in RaceFuncs.RaceEnumerable())
            {
                RaceSettingsItem item = State.RaceSettings.Get(race);
                item.RaceTraits.Remove(trait);
            }

            RemoveTrait();
        }
    }

    internal void SaveRace()
    {
        try
        {
            if (!Equals(_previousRace, Race.TrueNone))
            {
                RaceSettingsItem item = State.RaceSettings.Get(_previousRace);
                var racePar = RaceParameters.GetRaceTraits(_previousRace);
                var raceData = RaceFuncs.GetRace(_previousRace);
                item.OverrideGender = OverrideGender.isOn;
                item.OverrideFurry = OverrideFurry.isOn;
                item.MaleFraction = 1 - MaleFraction.value;
                item.HermFraction = HermFraction.value;
                item.FurryFraction = FurryFraction.value;

                item.BannerType = BannerType.value;
                item.RaceAI = (RaceAI)RaceAIDropdown.value;

                item.OverrideClothes = OverrideClothed.isOn;
                item.ClothedFraction = ClothedFraction.value;

                item.OverrideWeight = OverrideWeight.isOn;
                item.MinWeight = Convert.ToInt32(MinWeight.text) - 1;
                item.MaxWeight = Convert.ToInt32(MaxWeight.text) - 1;

                item.FavoredStat = (Stat)FavoredStat.value;
                item.FavoredStatSet = true;

                int spellIndex = InnateSpellDropdown.value;
                var value = (SpellType)Enum.GetValues(typeof(SpellType)).GetValue(spellIndex);

                //if (value > SpellTypes.Resurrection)
                //    value = value - SpellTypes.Resurrection + SpellTypes.AlraunePuff - 1;

                item.InnateSpell = value;
                if (RaceFuncs.TryParse(SpawnRaceDropdown.options[SpawnRaceDropdown.value].text, out Race spawnRace)) item.SpawnRace = spawnRace;
                if (RaceFuncs.TryParse(ConversionRaceDropdown.options[ConversionRaceDropdown.value].text, out Race conversionRace)) item.ConversionRace = conversionRace;
                if (RaceFuncs.TryParse(LeaderRaceDropdown.options[LeaderRaceDropdown.value].text, out Race leaderRace)) item.LeaderRace = leaderRace;

                item.OverrideBoob = OverrideBoob.isOn;
                item.MinBoob = Convert.ToInt32(MinBoob.text) - 1;
                item.MaxBoob = Convert.ToInt32(MaxBoob.text) - 1;

                item.OverrideDick = OverrideDick.isOn;
                item.MinDick = Convert.ToInt32(MinDick.text) - 1;
                item.MaxDick = Convert.ToInt32(MaxDick.text) - 1;

                if (item.MinWeight < 0) item.MinWeight = 0;
                if (item.MaxWeight >= raceData.SetupOutput.BodySizes) item.MaxWeight = Math.Max(raceData.SetupOutput.BodySizes - 1, 0);
                if (item.MinWeight > item.MaxWeight) item.MinWeight = item.MaxWeight;

                if (item.MinBoob < 0) item.MinBoob = 0;
                if (item.MaxBoob >= raceData.SetupOutput.BreastSizes()) item.MaxBoob = Math.Max(raceData.SetupOutput.BreastSizes() - 1, 0);
                if (item.MinBoob > item.MaxBoob) item.MinBoob = item.MaxBoob;

                if (item.MinDick < 0) item.MinDick = 0;
                if (item.MaxDick >= raceData.SetupOutput.DickSizes()) item.MaxDick = Math.Max(raceData.SetupOutput.DickSizes() - 1, 0);
                if (item.MinDick > item.MaxDick) item.MinDick = item.MaxDick;


                item.BodySize = Convert.ToInt32(BodySize.text);
                item.StomachSize = Convert.ToInt32(StomachSize.text);

                if (item.BodySize < 1) item.BodySize = 1;
                if (item.StomachSize < 1) item.StomachSize = 1;


                item.RaceTraits = _currentTraits.ToList();
                List<VoreType> newtypes = racePar.AllowedVoreTypes.ToList();
                if (UnbirthDisabled.isOn) newtypes.Remove(VoreType.Unbirth);
                if (CockVoreDisabled.isOn) newtypes.Remove(VoreType.CockVore);
                if (AnalVoreDisabled.isOn) newtypes.Remove(VoreType.Anal);
                if (BreastVoreDisabled.isOn) newtypes.Remove(VoreType.BreastVore);
                if (TailVoreDisabled.isOn) newtypes.Remove(VoreType.TailVore);
                item.AllowedVoreTypes = newtypes;


                item.Stats.Strength.Minimum = Convert.ToInt32(MinStrength.text);
                item.Stats.Strength.Roll = 1 + Convert.ToInt32(MaxStrength.text) - item.Stats.Strength.Minimum;
                if (item.Stats.Strength.Roll < 1) item.Stats.Strength.Roll = 1;
                item.Stats.Dexterity.Minimum = Convert.ToInt32(MinDexterity.text);
                item.Stats.Dexterity.Roll = 1 + Convert.ToInt32(MaxDexterity.text) - item.Stats.Dexterity.Minimum;
                if (item.Stats.Dexterity.Roll < 1) item.Stats.Strength.Roll = 1;
                item.Stats.Endurance.Minimum = Convert.ToInt32(MinEndurance.text);
                item.Stats.Endurance.Roll = 1 + Convert.ToInt32(MaxEndurance.text) - item.Stats.Endurance.Minimum;
                if (item.Stats.Endurance.Roll < 1) item.Stats.Strength.Roll = 1;
                item.Stats.Mind.Minimum = Convert.ToInt32(MinMind.text);
                item.Stats.Mind.Roll = 1 + Convert.ToInt32(MaxMind.text) - item.Stats.Mind.Minimum;
                if (item.Stats.Mind.Roll < 1) item.Stats.Strength.Roll = 1;
                item.Stats.Will.Minimum = Convert.ToInt32(MinWill.text);
                item.Stats.Will.Roll = 1 + Convert.ToInt32(MaxWill.text) - item.Stats.Will.Minimum;
                if (item.Stats.Will.Roll < 1) item.Stats.Strength.Roll = 1;
                item.Stats.Agility.Minimum = Convert.ToInt32(MinAgility.text);
                item.Stats.Agility.Roll = 1 + Convert.ToInt32(MaxAgility.text) - item.Stats.Agility.Minimum;
                if (item.Stats.Agility.Roll < 1) item.Stats.Strength.Roll = 1;
                item.Stats.Voracity.Minimum = Convert.ToInt32(MinVoracity.text);
                item.Stats.Voracity.Roll = 1 + Convert.ToInt32(MaxVoracity.text) - item.Stats.Voracity.Minimum;
                if (item.Stats.Voracity.Roll < 1) item.Stats.Strength.Roll = 1;
                item.Stats.Stomach.Minimum = Convert.ToInt32(MinStomach.text);
                item.Stats.Stomach.Roll = 1 + Convert.ToInt32(MaxStomach.text) - item.Stats.Stomach.Minimum;
                if (item.Stats.Stomach.Roll < 1) item.Stats.Strength.Roll = 1;

                item.PowerAdjustment = Convert.ToInt32(PowerAdjustment.text) / 100f;

                item.FemaleTraits = TextToTraitList(FemaleTraits.text);
                item.MaleTraits = TextToTraitList(MaleTraits.text);
                item.HermTraits = TextToTraitList(HermTraits.text);
                item.SpawnTraits = TextToTraitList(SpawnTraits.text);
                item.LeaderTraits = TextToTraitList(LeaderTraits.text);
            }
        }
        catch
        {
            State.GameManager.CreateMessageBox("There's an input box that's not filled in");
        }
    }

    public void OpenImportTraitList()
    {
        var box = State.GameManager.CreateInputBox();
        box.SetData(SetBaseTraitsToImportedList, "Import", "Cancel", "Insert a list of space separated traits here to replace the current list of traits for this race with that list", 120);
    }

    private void SetBaseTraitsToImportedList(string traits)
    {
        _currentTraits = TextToTraitList(traits);
        UpdateInteractable();
    }

    public static List<TraitType> TextToTraitList(string text)
    {
        List<TraitType> traits = new List<TraitType>();
        foreach (RandomizeList rl in State.RandomizeLists)
        {
            if (text.ToLower().Contains(rl.Name.ToString().ToLower()))
            {
                traits.Add((TraitType)rl.ID);
            }
        }

        foreach (TraitType trait in (Stat[])Enum.GetValues(typeof(TraitType)))
        {
            if (text.ToLower().Contains(trait.ToString().ToLower()))
            {
                traits.Add(trait);
            }
        }

        traits = traits.Distinct().ToList();
        return traits;
    }


    public static string TraitListToText(List<TraitType> traits, bool hideSecret = false)
    {
        if (traits == null) return "";
        string ret = "";
        bool first = true;
        foreach (var trait in traits)
        {
            if (hideSecret && Unit.SecretTags.Contains(trait)) continue; // We even hide secret traits if they would be shown once purchased, otherwise if you read "Infiltrator" you already know it's safe.
            if (first)
                first = false;
            else
                ret += ", ";
            if (State.RandomizeLists.Any(rl => (TraitType)rl.ID == trait))
                ret += State.RandomizeLists.Where(rl => (TraitType)rl.ID == trait).FirstOrDefault().Name;
            else
                ret += trait.ToString();
        }

        return ret;
    }

    internal void LoadRace()
    {
        if (RaceFuncs.TryParse(RaceDropdown.options[RaceDropdown.value].text, out Race race))
        {
            _previousRace = race;
            RaceSettingsItem item = State.RaceSettings.Get(race);
            var racePar = RaceParameters.GetRaceTraits(race);
            var raceData = RaceFuncs.GetRace(race);
            OverrideGender.isOn = item.OverrideGender;
            OverrideFurry.isOn = item.OverrideFurry;
            MaleFraction.value = 1 - item.MaleFraction;
            HermFraction.value = item.HermFraction;
            FurryFraction.value = item.FurryFraction;

            BannerType.value = item.BannerType;

            RaceAIDropdown.value = (int)item.RaceAI;
            RaceAIDropdown.RefreshShownValue();

            OverrideClothed.isOn = item.OverrideClothes;
            ClothedFraction.value = item.ClothedFraction;

            OverrideWeight.isOn = item.OverrideWeight;
            MinWeight.text = (1 + item.MinWeight).ToString();
            MaxWeight.text = (1 + item.MaxWeight).ToString();

            OverrideBoob.isOn = item.OverrideBoob;
            MinBoob.text = (1 + item.MinBoob).ToString();
            MaxBoob.text = (1 + item.MaxBoob).ToString();

            OverrideDick.isOn = item.OverrideDick;
            MinDick.text = (1 + item.MinDick).ToString();
            MaxDick.text = (1 + item.MaxDick).ToString();

            WeightText.text = $"Weight 1 - {Math.Max(raceData.SetupOutput.BodySizes, 1)}";
            BoobText.text = $"Breasts 1 - {Math.Max(raceData.SetupOutput.BreastSizes(), 1)}";
            DickText.text = $"Dick 1 - {Math.Max(raceData.SetupOutput.DickSizes(), 1)}";

            FavoredStat.value = (int)State.RaceSettings.GetFavoredStat(race);
            FavoredStat.RefreshShownValue();

            var spell = Array.IndexOf(Enum.GetValues(typeof(SpellType)), State.RaceSettings.GetInnateSpell(race));
            //if (spell > SpellTypes.Resurrection)
            //    spell = spell - SpellTypes.AlraunePuff + SpellTypes.Resurrection + 1;


            InnateSpellDropdown.value = (int)spell;

            InnateSpellDropdown.RefreshShownValue();

            var spawnRace = State.RaceSettings.GetSpawnRace(race);
            foreach (TMP_Dropdown.OptionData option in SpawnRaceDropdown.options.ToList())
                if (option.text == spawnRace.ToString())
                {
                    SpawnRaceDropdown.value = SpawnRaceDropdown.options.IndexOf(option);
                    break;
                }

            SpawnRaceDropdown.RefreshShownValue();

            var conversionRace = State.RaceSettings.GetConversionRace(race);
            foreach (TMP_Dropdown.OptionData option in ConversionRaceDropdown.options.ToList())
                if (option.text == conversionRace.ToString())
                {
                    ConversionRaceDropdown.value = ConversionRaceDropdown.options.IndexOf(option);
                    break;
                }

            ConversionRaceDropdown.RefreshShownValue();

            var leaderRace = State.RaceSettings.GetLeaderRace(race);
            foreach (TMP_Dropdown.OptionData option in LeaderRaceDropdown.options.ToList())
                if (option.text == leaderRace.ToString())
                {
                    LeaderRaceDropdown.value = LeaderRaceDropdown.options.IndexOf(option);
                    break;
                }

            LeaderRaceDropdown.RefreshShownValue();

            BodySize.text = item.BodySize.ToString();
            StomachSize.text = item.StomachSize.ToString();

            _currentTraits = item.RaceTraits.ToList();
            if (_currentTraits == null) _currentTraits = new List<TraitType>();
            UnbirthDisabled.isOn = !item.AllowedVoreTypes.Contains(VoreType.Unbirth);
            UnbirthDisabled.interactable = racePar.AllowedVoreTypes.Contains(VoreType.Unbirth);
            CockVoreDisabled.isOn = !item.AllowedVoreTypes.Contains(VoreType.CockVore);
            CockVoreDisabled.interactable = racePar.AllowedVoreTypes.Contains(VoreType.CockVore);
            AnalVoreDisabled.isOn = !item.AllowedVoreTypes.Contains(VoreType.Anal);
            AnalVoreDisabled.interactable = racePar.AllowedVoreTypes.Contains(VoreType.Anal);
            BreastVoreDisabled.isOn = !item.AllowedVoreTypes.Contains(VoreType.BreastVore);
            BreastVoreDisabled.interactable = racePar.AllowedVoreTypes.Contains(VoreType.BreastVore);
            TailVoreDisabled.isOn = !item.AllowedVoreTypes.Contains(VoreType.TailVore);
            TailVoreDisabled.interactable = racePar.AllowedVoreTypes.Contains(VoreType.TailVore);

            MinStrength.text = item.Stats.Strength.Minimum.ToString();
            MaxStrength.text = (item.Stats.Strength.Minimum + item.Stats.Strength.Roll - 1).ToString();
            MinDexterity.text = item.Stats.Dexterity.Minimum.ToString();
            MaxDexterity.text = (item.Stats.Dexterity.Minimum + item.Stats.Dexterity.Roll - 1).ToString();
            MinEndurance.text = item.Stats.Endurance.Minimum.ToString();
            MaxEndurance.text = (item.Stats.Endurance.Minimum + item.Stats.Endurance.Roll - 1).ToString();
            MinMind.text = item.Stats.Mind.Minimum.ToString();
            MaxMind.text = (item.Stats.Mind.Minimum + item.Stats.Mind.Roll - 1).ToString();
            MinWill.text = item.Stats.Will.Minimum.ToString();
            MaxWill.text = (item.Stats.Will.Minimum + item.Stats.Will.Roll - 1).ToString();
            MinAgility.text = item.Stats.Agility.Minimum.ToString();
            MaxAgility.text = (item.Stats.Agility.Minimum + item.Stats.Agility.Roll - 1).ToString();
            MinVoracity.text = item.Stats.Voracity.Minimum.ToString();
            MaxVoracity.text = (item.Stats.Voracity.Minimum + item.Stats.Voracity.Roll - 1).ToString();
            MinStomach.text = item.Stats.Stomach.Minimum.ToString();
            MaxStomach.text = (item.Stats.Stomach.Minimum + item.Stats.Stomach.Roll - 1).ToString();

            var powerAdj = item.PowerAdjustment;
            if (powerAdj == 0f)
            {
                powerAdj = racePar.PowerAdjustment;
            }

            PowerAdjustment.text = (powerAdj * 100).ToString();
            FemaleTraits.text = TraitListToText(item.FemaleTraits);
            MaleTraits.text = TraitListToText(item.MaleTraits);
            HermTraits.text = TraitListToText(item.HermTraits);
            SpawnTraits.text = TraitListToText(item.SpawnTraits);
            LeaderTraits.text = TraitListToText(item.LeaderTraits);
        }
    }

    public void UpdateInteractable()
    {
        var raceData = RaceFuncs.GetRace(_previousRace);

        if (raceData == null)
        {
            OverrideGender.isOn = false;
            OverrideFurry.isOn = false;
            return;
        }

        if (raceData.SetupOutput.CanBeGender.Count() > 1)
        {
            OverrideGender.interactable = true;
        }
        else
        {
            OverrideGender.interactable = false;
            OverrideGender.isOn = false;
        }

        if (raceData.SetupOutput.FurCapable)
        {
            OverrideFurry.interactable = true;
        }
        else
        {
            OverrideFurry.interactable = false;
            OverrideFurry.isOn = false;
        }

        if (raceData.SetupOutput.BreastSizes() > 0)
        {
            OverrideBoob.interactable = true;
        }
        else
        {
            OverrideBoob.interactable = false;
            OverrideBoob.isOn = false;
        }

        if (raceData.SetupOutput.DickSizes() > 0)
        {
            OverrideDick.interactable = true;
        }
        else
        {
            OverrideDick.interactable = false;
            OverrideDick.isOn = false;
        }

        if (raceData.SetupOutput.BreastSizes() > 0)
        {
            OverrideDick.interactable = true;
        }
        else
        {
            OverrideDick.interactable = false;
            OverrideDick.isOn = false;
        }

        if (raceData.SetupOutput.AllowedMainClothingTypes?.Count > 0)
        {
            OverrideClothed.interactable = true;
        }
        else
        {
            OverrideClothed.interactable = false;
            OverrideClothed.isOn = false;
        }

        BannerType.interactable = RaceFuncs.IsMainRace(_previousRace);


        MaleFraction.interactable = OverrideGender.isOn;
        HermFraction.interactable = OverrideGender.isOn;
        FurryFraction.interactable = OverrideFurry.isOn;

        ClothedFraction.interactable = OverrideClothed.isOn;
        MinWeight.interactable = OverrideWeight.isOn;
        MaxWeight.interactable = OverrideWeight.isOn;
        MinBoob.interactable = OverrideBoob.isOn;
        MaxBoob.interactable = OverrideBoob.isOn;
        MinDick.interactable = OverrideDick.isOn;
        MaxDick.interactable = OverrideDick.isOn;


        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Traits:");
        if (_currentTraits == null) _currentTraits = new List<TraitType>();
        foreach (TraitType trait in _currentTraits)
        {
            if (State.RandomizeLists.Any(rl => (TraitType)rl.ID == trait))
            {
                sb.AppendLine(State.RandomizeLists.Where(rl => (TraitType)rl.ID == trait).FirstOrDefault().Name);
            }
            else
                sb.AppendLine(trait.ToString());
        }

        TraitList.text = sb.ToString();
    }

    public void CloseAndSave()
    {
        SaveRace();
        State.SaveEditedRaces();
        gameObject.SetActive(false);
        if (State.GameManager.CurrentScene == State.GameManager.StartMode) return;
        if (State.World.Villages != null)
        {
            var units = StrategicUtilities.GetAllUnits();
            foreach (Unit unit in units)
            {
                unit.ReloadTraits();
            }
        }
        else
        {
            foreach (ActorUnit actor in TacticalUtilities.Units)
            {
                actor.Unit.ReloadTraits();
                actor.Unit.InitializeTraits();
                actor.Unit.UpdateSpells();
            }
        }

        if (State.World.AllActiveEmpires != null)
        {
            foreach (Empire emp in State.World.AllActiveEmpires)
            {
                if (RaceFuncs.IsRebelOrBandit4(emp.Side)) continue;
                var raceFlags = State.RaceSettings.GetRaceTraits(emp.ReplacedRace);
                if (raceFlags != null)
                {
                    if (raceFlags.Contains(TraitType.Prey))
                        emp.CanVore = false;
                    else
                        emp.CanVore = true;
                }
            }

            foreach (Empire emp in State.World.MainEmpires)
            {
                if (RaceFuncs.IsRebelOrBandit4(emp.Side)) continue;
                if (State.RaceSettings.Exists(emp.Race))
                {
                    emp.BannerType = State.RaceSettings.Get(emp.Race).BannerType;
                }
                else
                    emp.BannerType = 0;
            }
        }
    }

    public void CloseWithoutSave()
    {
        State.LoadEditedRaces();
        gameObject.SetActive(false);
    }

    public void ResetThisRace()
    {
        State.RaceSettings.Reset(_previousRace);
        LoadRace();
        UpdateInteractable();
    }

    public void ResetAllRaces()
    {
        State.RaceSettings.ResetAll();
        LoadRace();
        UpdateInteractable();
    }

    public void ActivateGeneral()
    {
        GeneralPanel.SetActive(true);
        TraitsPanel.SetActive(false);
        GeneralButton.interactable = false;
        TraitsButton.interactable = true;
    }

    public void ActivateTraits()
    {
        GeneralPanel.SetActive(false);
        TraitsPanel.SetActive(true);
        GeneralButton.interactable = true;
        TraitsButton.interactable = false;
    }
}