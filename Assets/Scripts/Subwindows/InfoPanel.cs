using System;
using System.Linq;
using System.Text;
using UnityEngine.UI;

public class InfoPanel
{
    UnitInfoPanel UnitInfoPanel;
    string parentMenu;
    public InfoPanel(UnitInfoPanel infoPanel, string menu = "unknown")
    {
        UnitInfoPanel = infoPanel;
        parentMenu = menu;
    }

    public Actor_Unit lastActor;

    public void RefreshLastUnitInfo()
    {
        if (lastActor == null)
            return;
        UpdatePanel(lastActor);
    }

    internal void HidePanel()
    {
        ClearText();
        UnitInfoPanel.gameObject.SetActive(false);
        UnitInfoPanel.Sprite?.transform.parent.gameObject.SetActive(false);
    }

    public void RefreshTacticalUnitInfo(Actor_Unit actor)
    {
        if (actor != null)
            lastActor = actor;
        if (actor == null)
        {
            //ClearText();
            return;
        }
        UnitInfoPanel.gameObject.SetActive(true);
        UnitInfoPanel.Sprite?.transform.parent.gameObject.SetActive(Config.HideUnitViewer == false);
        UpdatePanel(actor);

    }

    private void UpdatePanel(Actor_Unit actor)
    {
        if (actor.Hidden)
            return;
        UpdateBars(actor.Unit);
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        BuildStatus(sb2, actor.Unit);
        BuildStats(sb, actor.Unit, actor.Unit.Predator, actor);
        if (actor.Unit.Predator)
            BuildPredStat(sb, actor);
        if (parentMenu == "unitEditor")
        {
            UnitInfoPanel.InfoText.text = sb2.ToString();
        }
        else
        {
            UnitInfoPanel.InfoText.text = sb.ToString();
            UnitInfoPanel.BasicInfo.text = sb2.ToString();
        }
        UnitInfoPanel.Unit = actor.Unit;
        UnitInfoPanel.Actor = actor;
        if (Config.HideUnitViewer == false)
        {
            UnitInfoPanel.Sprite?.ResetBellyScale(actor);
            UnitInfoPanel.Sprite?.UpdateSprites(actor, false);
        }
    }

    private void UpdateBars(Unit actor, bool showNextText = false)
    {
        UnitInfoPanel.ExpBar.GetComponentInChildren<Text>().text = $"EXP: {(int)actor.Experience} ";
        if (showNextText)
            UnitInfoPanel.ExpBar.GetComponentInChildren<Text>().text += $"(To Next: {actor.ExperienceRequiredForNextLevel - (int)actor.Experience})";
        UnitInfoPanel.HealthBar.GetComponentInChildren<Text>().text = $"Health: {actor.Health}/{actor.MaxHealth}";
        UnitInfoPanel.ManaBar.GetComponentInChildren<Text>().text = $"Mana: {actor.Mana}/{actor.MaxMana}";
        if (actor.ExperienceRequiredForNextLevel != 0)
            UnitInfoPanel.ExpBar.value = (actor.Experience - actor.GetExperienceRequiredForLevel(actor.Level - 1)) / (actor.ExperienceRequiredForNextLevel - actor.GetExperienceRequiredForLevel(actor.Level - 1));
        else
            UnitInfoPanel.ExpBar.value = 1;
        UnitInfoPanel.HealthBar.value = actor.HealthPct;
        UnitInfoPanel.ManaBar.value = (float)actor.Mana / actor.MaxMana;
    }

    public void RefreshStrategicUnitInfo(Unit unit)
    {
        if (unit == null)
        {
            ClearText();
            return;
        }
        UpdateBars(unit, true);
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        BuildStats(sb, unit, unit.Predator, null);
        UnitInfoPanel.InfoText.text = sb.ToString();
        BuildStatus(sb2, unit);
        UnitInfoPanel.BasicInfo.text = sb2.ToString();
        UnitInfoPanel.Unit = unit;
        UnitInfoPanel.Actor = null;
    }

    internal void AddCorpse(string name)
    {
        if (name == "")
            return;
        UnitInfoPanel.InfoText.text = UnitInfoPanel.InfoText.text += $"Corpse of {name}\n";
    }

    internal void AddClothes(string name)
    {
        if (name == "")
            return;
        UnitInfoPanel.InfoText.text = UnitInfoPanel.InfoText.text += $"Clothes from {name}\n";
    }

    internal void AddLine(string description)
    {
        if (description == "")
            return;
        UnitInfoPanel.InfoText.text = UnitInfoPanel.InfoText.text += $"{description}\n";
    }

    public void ClearText()
    {
        UnitInfoPanel.InfoText.text = "";
    }

    public void ClearPicture()
    {
        UnitInfoPanel.Sprite.transform.parent.gameObject.SetActive(false);
    }


    public static string RaceSingular(Unit unit)
    {
        return Races2.GetRace(unit.Race).SingularName(unit);
    }

    void BuildStatus(StringBuilder sb, Unit unit)
    {
        // Add Name
        if (unit.Type == UnitType.Summon)
            sb.AppendLine(unit.Name + "(Summon)");
        else
            sb.AppendLine(unit.Name);

        // Add Level and Race
        sb.AppendLine($"Level {unit.Level} {RaceSingular(unit)}");

        // Add Gender and Type
        if (RaceFuncs.isUniqueMerc(unit.Race))
            sb.AppendLine($"{unit.Type}");
        else
        {
            var race = Races2.GetRace(unit.Race);
            if (race != null && race.SetupOutput.CanBeGender.Contains(Gender.None))
                sb.AppendLine($"{unit.Type}");
            else if (unit.GetGender() == Gender.Hermaphrodite)
                sb.AppendLine($"Herm {unit.Type}");
            else if (unit.GetGender() == Gender.Andromorph)
                sb.AppendLine($"Andro {unit.Type}");
            else
                sb.AppendLine($"{unit.GetGender()} {unit.Type}");
        }
    }

    void BuildStats(StringBuilder sb, Unit unit, bool CanVore, Actor_Unit actor)
    {
        if (parentMenu != "unitEditor")
        {
            if (unit.HasFixedSide() && TacticalUtilities.PlayerCanSeeTrueSide(unit))
            {
                if (State.World.MainEmpires == null)
                {
                    sb.AppendLine($"Special Allegiance: {(Equals(unit.FixedSide, State.GameManager.TacticalMode.GetDefenderSide()) ? "Defender" : "Attacker")}");
                }
                else
                    sb.AppendLine($"Special Allegiance: {State.World.GetEmpireOfSide(unit.FixedSide)?.Name ?? "Unkown"}");
            }
            UnityEngine.Transform EquipRow = UnitInfoPanel.StatBlock.transform.GetChild(5);

            EquipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = unit.GetItem(0)?.Name;
            EquipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = unit.GetItem(1)?.Name;
            if (unit.HasTrait(TraitType.Resourceful))
            {
                EquipRow.transform.GetChild(2).gameObject.SetActive(true);
                EquipRow.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = unit.GetItem(2)?.Name;
            }
            else
                EquipRow.transform.GetChild(2).gameObject.SetActive(false);

            // Add Equipment
            //for (int i = 0; i < unit.Items.Length; i++)
            //{
            //    sb.AppendLine(unit.GetItem(i)?.Name ?? "empty");
            //}

            Text STRVal = UnitInfoPanel.StatBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
            Text DEXVal = UnitInfoPanel.StatBlock.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>();
            Text MNDVal = UnitInfoPanel.StatBlock.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Text>();
            Text WLLVal = UnitInfoPanel.StatBlock.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Text>();
            Text ENDVal = UnitInfoPanel.StatBlock.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Text>();
            Text AGIVal = UnitInfoPanel.StatBlock.transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<Text>();
            Text VORVal = UnitInfoPanel.StatBlock.transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<Text>();
            Text STMVal = UnitInfoPanel.StatBlock.transform.GetChild(3).GetChild(1).GetChild(1).GetComponent<Text>();
            Text LDRVal = UnitInfoPanel.StatBlock.transform.GetChild(4).GetChild(0).GetChild(1).GetComponent<Text>();

            // Add Battle Stats
            STRVal.text = unit.GetStatInfo(Stat.Strength);
            DEXVal.text = unit.GetStatInfo(Stat.Dexterity);
            ENDVal.text = unit.GetStatInfo(Stat.Endurance);
            AGIVal.text = unit.GetStatInfo(Stat.Agility);
            MNDVal.text = unit.GetStatInfo(Stat.Mind);
            WLLVal.text = unit.GetStatInfo(Stat.Will);
            if (CanVore)
            {
                UnitInfoPanel.StatBlock.transform.GetChild(3).gameObject.SetActive(true);
                VORVal.text = unit.GetStatInfo(Stat.Voracity);
                STMVal.text = unit.GetStatInfo(Stat.Stomach);
            }
            else
                UnitInfoPanel.StatBlock.transform.GetChild(3).gameObject.SetActive(false);

            UnityEngine.Transform FifthLine = UnitInfoPanel.StatBlock.transform.GetChild(4);

            // Add Leadership Stat if unit is a Leader
            if (unit.Type == UnitType.Leader)
            {
                FifthLine.gameObject.SetActive(true);
                FifthLine.GetChild(0).gameObject.SetActive(true);
                LDRVal.text = unit.GetStatInfo(Stat.Leadership);
            }
            else
                FifthLine.GetChild(0).gameObject.SetActive(false);

            UnityEngine.Transform killSection = FifthLine.GetChild(1).GetChild(0);
            UnityEngine.Transform digestSection = FifthLine.GetChild(1).GetChild(1);
            UnityEngine.Transform deathSection = FifthLine.GetChild(1).GetChild(2);
            // Set kill counter, hide otherwise
            if (unit.KilledUnits > 0)
            {
                FifthLine.gameObject.SetActive(true);
                FifthLine.GetChild(1).gameObject.SetActive(true);
                killSection.gameObject.SetActive(true);
                killSection.GetChild(1).GetComponent<Text>().text = unit.KilledUnits.ToString();
            }
            else
                killSection.gameObject.SetActive(false);
            // Set digestion counter, hide otherwise
            if (unit.DigestedUnits > 0)
            {
                FifthLine.gameObject.SetActive(true);
                FifthLine.GetChild(1).gameObject.SetActive(true);
                digestSection.gameObject.SetActive(true);
                digestSection.GetChild(1).GetComponent<Text>().text = unit.DigestedUnits.ToString();
            }
            else
                digestSection.gameObject.SetActive(false);
            // Set death counter, hide otherwise
            if (unit.TimesKilled > 0)
            {
                FifthLine.gameObject.SetActive(true);
                FifthLine.GetChild(1).gameObject.SetActive(true);
                deathSection.gameObject.SetActive(true);
                deathSection.GetChild(1).GetComponent<Text>().text = unit.TimesKilled.ToString();
            }
            else
                deathSection.gameObject.SetActive(false);
            if (!killSection.gameObject.activeSelf && !digestSection.gameObject.activeSelf && !deathSection.gameObject.activeSelf)
                FifthLine.GetChild(1).gameObject.SetActive(false);
            if (!FifthLine.GetChild(0).gameObject.activeSelf && !FifthLine.GetChild(1).gameObject.activeSelf)
                FifthLine.gameObject.SetActive(false);

            if (unit.SavedCopy != null && unit.SavedVillage != null)
                sb.AppendLine($"Imprinted");
            if (actor?.Surrendered ?? false)
                sb.AppendLine("Unit has surrendered!");
            string traits = unit.ListTraits(!(TacticalUtilities.IsUnitControlledByPlayer(unit) && TacticalUtilities.PlayerCanSeeTrueSide(unit)));
            if (traits != "")
                sb.AppendLine("Traits:\n" + traits);
            StringBuilder sbSecond = new StringBuilder();
            sbSecond.AppendLine("Status:");
            if (unit.HasTrait(TraitType.Frenzy) && unit.EnemiesKilledThisBattle > 0)
                sbSecond.AppendLine($"Frenzy ({unit.EnemiesKilledThisBattle})");
            if (unit.HasTrait(TraitType.Growth) && unit.BaseScale > 1)
                sbSecond.AppendLine($"Growth ({Math.Round(unit.BaseScale, 2)}x)");
            if (actor?.Slimed ?? false)
                sbSecond.AppendLine("Slimed");
            if (actor?.Paralyzed ?? false)
                sbSecond.AppendLine("Paralyzed");
            if (actor?.Corruption > 0 && !TacticalUtilities.IsUnitControlledByPlayer(unit))
                sbSecond.AppendLine($"Corruption ({actor.Corruption}/{unit.GetStatTotal() + unit.GetStat(Stat.Will)})");
            if (actor?.Possessed > 0 && !TacticalUtilities.IsUnitControlledByPlayer(unit))
                sbSecond.AppendLine($"Possessed ({actor.Corruption + actor.Possessed}/{unit.GetStatTotal() + unit.GetStat(Stat.Will)})");
            if (actor?.Infected ?? false)
                sbSecond.AppendLine($"Infected");
            if (unit.StatusEffects?.Any() ?? false)
            {
                foreach (StatusEffectType type in (StatusEffectType[])Enum.GetValues(typeof(StatusEffectType)))
                {
                    var effect = unit.GetLongestStatusEffect(type);
                    if (unit.GetLongestStatusEffect(type) != null)
                        sbSecond.AppendLine($"{type} ({effect.Duration})");
                }
            }

            if (sbSecond.Length > 10)
                sb.Append($"{sbSecond}");

            var racePar = RaceParameters.GetTraitData(unit);

            if ((unit.InnateSpells?.Any() ?? false) || (racePar.InnateSpells?.Any() ?? false))
            {
                sb.AppendLine("Innate Spells:");
                if (unit.InnateSpells != null)
                {
                    foreach (var spellType in unit.InnateSpells)
                    {
                        if (SpellList.SpellDict.TryGetValue(spellType, out Spell spell))
                        {
                            sb.AppendLine(spell.Name);
                        }
                        else
                        {
                            sb.AppendLine("Invalid Spell");
                        }
                    }
                }
                if (racePar.InnateSpells != null)
                {
                    foreach (var spellType in racePar.InnateSpells)
                    {
                        if (SpellList.SpellDict.TryGetValue(spellType, out Spell spell))
                        {
                            sb.AppendLine(spell.Name);
                        }
                        else
                        {
                            sb.AppendLine("Invalid Spell");
                        }
                    }
                }
                var grantedSpell = State.RaceSettings.GetInnateSpell(unit.Race);
                if (grantedSpell != SpellType.None)
                {
                    if (SpellList.SpellDict.TryGetValue(grantedSpell, out Spell spell))
                    {
                        sb.AppendLine(spell.Name);
                    }
                    else
                    {
                        sb.AppendLine("Invalid Spell");
                    }
                }
            }
            if (Config.CheatUnitEditorEnabled)
                sb.AppendLine("<color=#AB5200ff>UnitEditor</color>");
        }
    }

    void BuildPredStat(StringBuilder sb, Actor_Unit unit)
    {
        sb.Append(unit.PredatorComponent.GetPreyInformation());

    }


}
