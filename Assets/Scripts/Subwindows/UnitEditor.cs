﻿using System.Linq;
using static UnityEngine.UI.CanvasScaler;

internal class UnitEditor : UnitCustomizer
{
    private UnitEditorPanel UnitEditorPanel;
    private InfoPanel panel;

    public UnitEditor(Actor_Unit actor, CustomizerPanel UI, UnitInfoPanel unitPanel) : base(actor, UI)
    {
        panel = new InfoPanel(unitPanel, "unitEditor");
        RefreshStats();
    }

    public UnitEditor(Unit unit, CustomizerPanel UI, UnitInfoPanel unitPanel) : base(unit, UI)
    {
        panel = new InfoPanel(unitPanel, "unitEditor");
        RefreshStats();
    }

    internal void RefreshActor()
    {
        RaceData = Races2.GetRace(actor.Unit);
        Normal(actor.Unit);
        RefreshStats();
    }

    internal void ClearAnimations()
    {
        actor.AnimationController = new AnimationController();
    }

    internal void ChangeSide()
    {
        State.GameManager.TacticalMode.SwitchAlignment(actor);
    }


    internal void RefreshStats()
    {
        panel.RefreshTacticalUnitInfo(actor);
    }

    internal void ChangeStat(Stat stat, int change)
    {
    }

    internal void ChangeLevel(Stat stat, int change)
    {
        if (change > 0)
        {
            Unit.SetExp(Unit.ExperienceRequiredForNextLevel);
            Unit.LevelUp(stat);
        }
        else
        {
            Unit.LevelDown(stat);
            Unit.SetExp(Unit.GetExperienceRequiredForLevel(Unit.Level - 1));
        }

        RefreshStats();
    }

    internal new void ChangeGender()
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
            Unit.HasVagina = true;
            Unit.SetDefaultBreastSize(-1);
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

        if (changedGender)
        {
            if (Unit.Pronouns == null) Unit.GeneratePronouns();
            if (CustomizerUI.Gender.value == 0)
            {
                Unit.Pronouns[0] = "he";
                Unit.Pronouns[1] = "him";
                Unit.Pronouns[2] = "his";
                Unit.Pronouns[3] = "his";
                Unit.Pronouns[4] = "himself";
                CustomizerUI.Quantification.value = 0;
            }
            else if (CustomizerUI.Gender.value == 1)
            {
                Unit.Pronouns[0] = "she";
                Unit.Pronouns[1] = "her";
                Unit.Pronouns[2] = "her";
                Unit.Pronouns[3] = "hers";
                Unit.Pronouns[4] = "herself";
                CustomizerUI.Quantification.value = 0;
            }
            else
            {
                Unit.Pronouns[0] = "they";
                Unit.Pronouns[1] = "them";
                Unit.Pronouns[2] = "their";
                Unit.Pronouns[3] = "theirs";
                Unit.Pronouns[4] = "themself";
                CustomizerUI.Quantification.value = 1;
            }

            RefreshPronouns(Unit);
            Unit.ReloadTraits();
            Unit.InitializeTraits();
            RefreshView();
        }
    }

    private void RefreshGenderDropdown(Unit unit)
    {
        if (unit.HasBreasts)
        {
            if (unit.HasDick)
            {
                if (unit.HasVagina)
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

    internal void ClearStatus()
    {
        Unit.StatusEffects.Clear();
        if (actor != null) actor.Surrendered = false;
    }

    internal void AddTrait(TraitType traitType)
    {
        if (actor.Unit.AddPermanentTrait(traitType))
            if (traitType == TraitType.MadScience && State.World?.ItemRepository != null)
            {
                Unit.SingleUseSpells.Add(((SpellBook)State.World.ItemRepository.GetRandomBook(1, 4)).ContainedSpell);
                Unit.UpdateSpells();
            }
            else if (traitType == TraitType.PollenProjector && State.World?.ItemRepository != null)
            {
                Unit.SingleUseSpells.Add(SpellList.AlraunePuff.SpellType);
                Unit.UpdateSpells();
            }
            else if (traitType == TraitType.Webber && State.World?.ItemRepository != null)
            {
                Unit.SingleUseSpells.Add(SpellList.Web.SpellType);
                Unit.UpdateSpells();
            }
            else if (traitType == TraitType.GlueBomb && State.World?.ItemRepository != null)
            {
                Unit.SingleUseSpells.Add(SpellList.GlueBomb.SpellType);
                Unit.UpdateSpells();
            }
            else if (traitType == TraitType.PoisonSpit && State.World?.ItemRepository != null)
            {
                Unit.SingleUseSpells.Add(SpellList.ViperPoisonStatus.SpellType);
                Unit.UpdateSpells();
            }
            else if (traitType == TraitType.Petrifier && State.World?.ItemRepository != null)
            {
                Unit.SingleUseSpells.Add(SpellList.Petrify.SpellType);
                Unit.UpdateSpells();
            }
            else if (traitType == TraitType.Charmer && State.World?.ItemRepository != null)
            {
                Unit.SingleUseSpells.Add(SpellList.Charm.SpellType);
                Unit.UpdateSpells();
            }
            else if (traitType == TraitType.HypnoticGas && State.World?.ItemRepository != null)
            {
                Unit.SingleUseSpells.Add(SpellList.HypnoGas.SpellType);
                Unit.UpdateSpells();
            }
            else if (traitType == TraitType.Reanimator && State.World?.ItemRepository != null)
            {
                Unit.SingleUseSpells.Add(SpellList.Reanimate.SpellType);
                Unit.UpdateSpells();
            }
            else if (traitType == TraitType.Binder && State.World?.ItemRepository != null)
            {
                Unit.SingleUseSpells.Add(SpellList.Bind.SpellType);
                Unit.UpdateSpells();
            }
            else
                // Multi-use section
            if (traitType == TraitType.ForceFeeder && State.World?.ItemRepository != null)
            {
                Unit.MultiUseSpells.Add(SpellList.ForceFeed.SpellType);
                Unit.UpdateSpells();
            }
            else if (traitType == TraitType.Prey)
            {
                Unit.Predator = false;
                actor.PredatorComponent?.FreeAnyAlivePrey();
            }
            else if (traitType == TraitType.BookWormI || traitType == TraitType.BookWormII || traitType == TraitType.BookWormIII) Unit.GiveTraitBooks();

        actor.Unit.InitializeTraits();
        RefreshStats();
    }


    internal void RemoveTrait(TraitType traitType)
    {
        actor.Unit.RemoveTrait(traitType);
        if (traitType == TraitType.Prey)
        {
            if (RaceParameters.GetTraitData(actor.Unit).AllowedVoreTypes.Any())
            {
                Unit.Predator = true;
                actor.PredatorComponent = new PredatorComponent(actor, Unit);
            }
            else
                actor.Unit.AddTrait(traitType);
        }

        actor.Unit.InitializeTraits();
        RefreshStats();
    }

    internal void RestoreMana()
    {
        actor.Unit.RestoreManaPct(1);
        RefreshStats();
    }

    internal void RestoreHealth()
    {
        actor.Unit.HealPercentage(1);
        RefreshStats();
    }

    internal void RestoreMovement()
    {
        actor.Movement = actor.CurrentMaxMovement();
        RefreshStats();
    }


    internal void SetLevelTo(int level)
    {
        if (level > Unit.Level)
        {
            StrategicUtilities.CheatForceLevelUps(Unit, level - Unit.Level);
            Unit.SetExp(Unit.GetExperienceRequiredForLevel(Unit.Level - 1));
            RefreshStats();
        }
        else
        {
            for (int i = 0; i < 1000; i++)
            {
                if (level < Unit.Level)
                {
                    Unit.LevelDown();
                    Unit.SetExp(Unit.GetExperienceRequiredForLevel(Unit.Level - 1));
                    continue;
                }

                break;
            }
        }

        RefreshStats();
    }

    internal void ChangeItem(int slot, Item item)
    {
        Unit.SetItem(item, slot, true);
        RefreshStats();
    }
}