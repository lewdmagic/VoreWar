using System;
using System.Linq;
using System.Text;
using UnityEngine.UI;

public class InfoPanel
{
    private UnitInfoPanel _unitInfoPanel;
    private string _parentMenu;

    public InfoPanel(UnitInfoPanel infoPanel, string menu = "unknown")
    {
        _unitInfoPanel = infoPanel;
        _parentMenu = menu;
    }

    public ActorUnit LastActor;

    public void RefreshLastUnitInfo()
    {
        if (LastActor == null) return;
        UpdatePanel(LastActor);
    }

    internal void HidePanel()
    {
        ClearText();
        _unitInfoPanel.gameObject.SetActive(false);
        _unitInfoPanel.Sprite?.transform.parent.gameObject.SetActive(false);
    }

    public void RefreshTacticalUnitInfo(ActorUnit actor)
    {
        if (actor != null) LastActor = actor;
        if (actor == null)
        {
            //ClearText();
            return;
        }

        _unitInfoPanel.gameObject.SetActive(true);
        _unitInfoPanel.Sprite?.transform.parent.gameObject.SetActive(Config.HideUnitViewer == false);
        UpdatePanel(actor);
    }

    private void UpdatePanel(ActorUnit actor)
    {
        if (actor.Hidden) return;
        UpdateBars(actor.Unit);
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        BuildStatus(sb2, actor.Unit);
        BuildStats(sb, actor.Unit, actor.Unit.Predator, actor);
        if (actor.Unit.Predator) BuildPredStat(sb, actor);
        if (_parentMenu == "unitEditor")
        {
            _unitInfoPanel.InfoText.text = sb2.ToString();
        }
        else
        {
            _unitInfoPanel.InfoText.text = sb.ToString();
            _unitInfoPanel.BasicInfo.text = sb2.ToString();
        }

        _unitInfoPanel.Unit = actor.Unit;
        _unitInfoPanel.Actor = actor;
        if (Config.HideUnitViewer == false)
        {
            _unitInfoPanel.Sprite?.ResetBellyScale(actor);
            _unitInfoPanel.Sprite?.UpdateSprites(actor, false);
        }
    }

    private void UpdateBars(Unit actor, bool showNextText = false)
    {
        _unitInfoPanel.ExpBar.GetComponentInChildren<Text>().text = $"EXP: {(int)actor.Experience} ";
        if (showNextText) _unitInfoPanel.ExpBar.GetComponentInChildren<Text>().text += $"(To Next: {actor.ExperienceRequiredForNextLevel - (int)actor.Experience})";
        _unitInfoPanel.HealthBar.GetComponentInChildren<Text>().text = $"Health: {actor.Health}/{actor.MaxHealth}";
        _unitInfoPanel.ManaBar.GetComponentInChildren<Text>().text = $"Mana: {actor.Mana}/{actor.MaxMana}";
        if (actor.ExperienceRequiredForNextLevel != 0)
            _unitInfoPanel.ExpBar.value = (actor.Experience - actor.GetExperienceRequiredForLevel(actor.Level - 1)) / (actor.ExperienceRequiredForNextLevel - actor.GetExperienceRequiredForLevel(actor.Level - 1));
        else
            _unitInfoPanel.ExpBar.value = 1;
        _unitInfoPanel.HealthBar.value = actor.HealthPct;
        _unitInfoPanel.ManaBar.value = (float)actor.Mana / actor.MaxMana;
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
        _unitInfoPanel.InfoText.text = sb.ToString();
        BuildStatus(sb2, unit);
        _unitInfoPanel.BasicInfo.text = sb2.ToString();
        _unitInfoPanel.Unit = unit;
        _unitInfoPanel.Actor = null;
    }

    internal void AddCorpse(string name)
    {
        if (name == "") return;
        _unitInfoPanel.InfoText.text = _unitInfoPanel.InfoText.text += $"Corpse of {name}\n";
    }

    internal void AddClothes(string name)
    {
        if (name == "") return;
        _unitInfoPanel.InfoText.text = _unitInfoPanel.InfoText.text += $"Clothes from {name}\n";
    }

    internal void AddLine(string description)
    {
        if (description == "") return;
        _unitInfoPanel.InfoText.text = _unitInfoPanel.InfoText.text += $"{description}\n";
    }

    public void ClearText()
    {
        _unitInfoPanel.InfoText.text = "";
    }

    public void ClearPicture()
    {
        _unitInfoPanel.Sprite.transform.parent.gameObject.SetActive(false);
    }


    public static string RaceSingular(Unit unit)
    {
        return Races2.GetRace(unit.Race).SingularName(unit);
    }

    private void BuildStatus(StringBuilder sb, Unit unit)
    {
        // Add Name
        if (unit.Type == UnitType.Summon)
            sb.AppendLine(unit.Name + "(Summon)");
        else
            sb.AppendLine(unit.Name);

        // Add Level and Race
        sb.AppendLine($"Level {unit.Level} {RaceSingular(unit)}");

        // Add Gender and Type
        if (RaceFuncs.IsUniqueMerc(unit.Race))
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

    private void BuildStats(StringBuilder sb, Unit unit, bool canVore, ActorUnit actor)
    {
        if (_parentMenu != "unitEditor")
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

            UnityEngine.Transform equipRow = _unitInfoPanel.StatBlock.transform.GetChild(5);

            equipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = unit.GetItem(0)?.Name;
            equipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = unit.GetItem(1)?.Name;
            if (unit.HasTrait(TraitType.Resourceful))
            {
                equipRow.transform.GetChild(2).gameObject.SetActive(true);
                equipRow.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = unit.GetItem(2)?.Name;
            }
            else
                equipRow.transform.GetChild(2).gameObject.SetActive(false);

            // Add Equipment
            //for (int i = 0; i < unit.Items.Length; i++)
            //{
            //    sb.AppendLine(unit.GetItem(i)?.Name ?? "empty");
            //}

            Text strVal = _unitInfoPanel.StatBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
            Text dexVal = _unitInfoPanel.StatBlock.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>();
            Text mndVal = _unitInfoPanel.StatBlock.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Text>();
            Text wllVal = _unitInfoPanel.StatBlock.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Text>();
            Text endVal = _unitInfoPanel.StatBlock.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Text>();
            Text agiVal = _unitInfoPanel.StatBlock.transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<Text>();
            Text vorVal = _unitInfoPanel.StatBlock.transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<Text>();
            Text stmVal = _unitInfoPanel.StatBlock.transform.GetChild(3).GetChild(1).GetChild(1).GetComponent<Text>();
            Text ldrVal = _unitInfoPanel.StatBlock.transform.GetChild(4).GetChild(0).GetChild(1).GetComponent<Text>();

            // Add Battle Stats
            strVal.text = unit.GetStatInfo(Stat.Strength);
            dexVal.text = unit.GetStatInfo(Stat.Dexterity);
            endVal.text = unit.GetStatInfo(Stat.Endurance);
            agiVal.text = unit.GetStatInfo(Stat.Agility);
            mndVal.text = unit.GetStatInfo(Stat.Mind);
            wllVal.text = unit.GetStatInfo(Stat.Will);
            if (canVore)
            {
                _unitInfoPanel.StatBlock.transform.GetChild(3).gameObject.SetActive(true);
                vorVal.text = unit.GetStatInfo(Stat.Voracity);
                stmVal.text = unit.GetStatInfo(Stat.Stomach);
            }
            else
                _unitInfoPanel.StatBlock.transform.GetChild(3).gameObject.SetActive(false);

            UnityEngine.Transform fifthLine = _unitInfoPanel.StatBlock.transform.GetChild(4);

            // Add Leadership Stat if unit is a Leader
            if (unit.Type == UnitType.Leader)
            {
                fifthLine.gameObject.SetActive(true);
                fifthLine.GetChild(0).gameObject.SetActive(true);
                ldrVal.text = unit.GetStatInfo(Stat.Leadership);
            }
            else
                fifthLine.GetChild(0).gameObject.SetActive(false);

            UnityEngine.Transform killSection = fifthLine.GetChild(1).GetChild(0);
            UnityEngine.Transform digestSection = fifthLine.GetChild(1).GetChild(1);
            UnityEngine.Transform deathSection = fifthLine.GetChild(1).GetChild(2);
            // Set kill counter, hide otherwise
            if (unit.KilledUnits > 0)
            {
                fifthLine.gameObject.SetActive(true);
                fifthLine.GetChild(1).gameObject.SetActive(true);
                killSection.gameObject.SetActive(true);
                killSection.GetChild(1).GetComponent<Text>().text = unit.KilledUnits.ToString();
            }
            else
                killSection.gameObject.SetActive(false);

            // Set digestion counter, hide otherwise
            if (unit.DigestedUnits > 0)
            {
                fifthLine.gameObject.SetActive(true);
                fifthLine.GetChild(1).gameObject.SetActive(true);
                digestSection.gameObject.SetActive(true);
                digestSection.GetChild(1).GetComponent<Text>().text = unit.DigestedUnits.ToString();
            }
            else
                digestSection.gameObject.SetActive(false);

            // Set death counter, hide otherwise
            if (unit.TimesKilled > 0)
            {
                fifthLine.gameObject.SetActive(true);
                fifthLine.GetChild(1).gameObject.SetActive(true);
                deathSection.gameObject.SetActive(true);
                deathSection.GetChild(1).GetComponent<Text>().text = unit.TimesKilled.ToString();
            }
            else
                deathSection.gameObject.SetActive(false);

            if (!killSection.gameObject.activeSelf && !digestSection.gameObject.activeSelf && !deathSection.gameObject.activeSelf) fifthLine.GetChild(1).gameObject.SetActive(false);
            if (!fifthLine.GetChild(0).gameObject.activeSelf && !fifthLine.GetChild(1).gameObject.activeSelf) fifthLine.gameObject.SetActive(false);

            if (unit.SavedCopy != null && unit.SavedVillage != null) sb.AppendLine($"Imprinted");
            if (actor?.Surrendered ?? false) sb.AppendLine("Unit has surrendered!");
            string traits = unit.ListTraits(!(TacticalUtilities.IsUnitControlledByPlayer(unit) && TacticalUtilities.PlayerCanSeeTrueSide(unit)));
            if (traits != "") sb.AppendLine("Traits:\n" + traits);
            StringBuilder sbSecond = new StringBuilder();
            sbSecond.AppendLine("Status:");
            if (unit.HasTrait(TraitType.Frenzy) && unit.EnemiesKilledThisBattle > 0) sbSecond.AppendLine($"Frenzy ({unit.EnemiesKilledThisBattle})");
            if (unit.HasTrait(TraitType.Growth) && unit.BaseScale > 1) sbSecond.AppendLine($"Growth ({Math.Round(unit.BaseScale, 2)}x)");
            if (actor?.Slimed ?? false) sbSecond.AppendLine("Slimed");
            if (actor?.Paralyzed ?? false) sbSecond.AppendLine("Paralyzed");
            if (actor?.Corruption > 0 && !TacticalUtilities.IsUnitControlledByPlayer(unit)) sbSecond.AppendLine($"Corruption ({actor.Corruption}/{unit.GetStatTotal() + unit.GetStat(Stat.Will)})");
            if (actor?.Possessed > 0 && !TacticalUtilities.IsUnitControlledByPlayer(unit)) sbSecond.AppendLine($"Possessed ({actor.Corruption + actor.Possessed}/{unit.GetStatTotal() + unit.GetStat(Stat.Will)})");
            if (actor?.Infected ?? false) sbSecond.AppendLine($"Infected");
            if (unit.StatusEffects?.Any() ?? false)
            {
                foreach (StatusEffectType type in (StatusEffectType[])Enum.GetValues(typeof(StatusEffectType)))
                {
                    var effect = unit.GetLongestStatusEffect(type);
                    if (unit.GetLongestStatusEffect(type) != null) sbSecond.AppendLine($"{type} ({effect.Duration})");
                }
            }

            if (sbSecond.Length > 10) sb.Append($"{sbSecond}");

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

            if (Config.CheatUnitEditorEnabled) sb.AppendLine("<color=#AB5200ff>UnitEditor</color>");
        }
    }

    private void BuildPredStat(StringBuilder sb, ActorUnit unit)
    {
        sb.Append(unit.PredatorComponent.GetPreyInformation());
    }
}