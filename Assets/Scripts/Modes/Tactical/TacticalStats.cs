﻿using OdinSerializer;
using System.Collections.Generic;
using System.Text;


public class TacticalStats
{
    private class RaceStats
    {
        [OdinSerialize]
        private Dictionary<Weapon, int> _damageDealtBy;

        public Dictionary<Weapon, int> DamageDealtBy { get => _damageDealtBy; set => _damageDealtBy = value; }

        [OdinSerialize]
        private Dictionary<Weapon, int> _killsWith;

        public Dictionary<Weapon, int> KillsWith { get => _killsWith; set => _killsWith = value; }

        [OdinSerialize]
        private Dictionary<Spell, int> _damageDealtBySpell;

        public Dictionary<Spell, int> DamageDealtBySpell { get => _damageDealtBySpell; set => _damageDealtBySpell = value; }

        [OdinSerialize]
        private Dictionary<Spell, int> _killsWithSpell;

        public Dictionary<Spell, int> KillsWithSpell { get => _killsWithSpell; set => _killsWithSpell = value; }

        [OdinSerialize]
        private int _hits;

        public int Hits { get => _hits; set => _hits = value; }

        [OdinSerialize]
        private int _misses;

        public int Misses { get => _misses; set => _misses = value; }

        [OdinSerialize]
        private int _totalHpHealed;

        public int TotalHpHealed { get => _totalHpHealed; set => _totalHpHealed = value; }

        [OdinSerialize]
        private int _targetsVored;

        public int TargetsVored { get => _targetsVored; set => _targetsVored = value; }

        [OdinSerialize]
        private int _targetsRegurgitated;

        public int TargetsRegurgitated { get => _targetsRegurgitated; set => _targetsRegurgitated = value; }

        [OdinSerialize]
        private int _targetsEscaped;

        public int TargetsEscaped { get => _targetsEscaped; set => _targetsEscaped = value; }

        [OdinSerialize]
        private int _targetsFreed;

        public int TargetsFreed { get => _targetsFreed; set => _targetsFreed = value; }

        [OdinSerialize]
        private int _targetsDigested;

        public int TargetsDigested { get => _targetsDigested; set => _targetsDigested = value; }

        [OdinSerialize]
        private int _alliesEaten;

        public int AlliesEaten { get => _alliesEaten; set => _alliesEaten = value; }

        public RaceStats()
        {
            DamageDealtBy = new Dictionary<Weapon, int>();
            KillsWith = new Dictionary<Weapon, int>();
            DamageDealtBySpell = new Dictionary<Spell, int>();
            KillsWithSpell = new Dictionary<Spell, int>();
        }
    }

    [OdinSerialize]
    private Side _defenderSide;

    public Side DefenderSide { get => _defenderSide; set => _defenderSide = value; }

    [OdinSerialize]
    private Side _attackerSide;

    public Side AttackerSide { get => _attackerSide; set => _attackerSide = value; }

    [OdinSerialize]
    private int _attackers;

    private int Attackers { get => _attackers; set => _attackers = value; }

    [OdinSerialize]
    private int _defenders;

    private int Defenders { get => _defenders; set => _defenders = value; }

    [OdinSerialize]
    private int _garrison;

    private int Garrison { get => _garrison; set => _garrison = value; }

    [OdinSerialize]
    private RaceStats _attackerStats;

    private RaceStats AttackerStats { get => _attackerStats; set => _attackerStats = value; }

    [OdinSerialize]
    private RaceStats _defenderStats;

    private RaceStats DefenderStats { get => _defenderStats; set => _defenderStats = value; }

    public void SetInitialUnits(int attack, int defend, int garr, Side attackerSide, Side defenderSide)
    {
        Attackers = attack;
        Defenders = defend;
        Garrison = garr;
        AttackerSide = attackerSide;
        DefenderSide = defenderSide;

        AttackerStats = new RaceStats();
        DefenderStats = new RaceStats();
    }

    public string OverallSummary(double attackerStart, double attackerEnd, double defenderStart, double defenderEnd, bool attackerWon)
    {
        double remainingAttackerPct = attackerEnd / attackerStart;
        double remainingDefenderPct = defenderEnd / defenderStart;

        double attackerValueLost = attackerStart - attackerEnd;
        double defenderValueLost = defenderStart - defenderEnd;

        string lossRatio = "";
        string survivorRatio;

        if (attackerWon)
        {
            if (attackerValueLost > 4 * defenderValueLost)
                lossRatio = "Pyrrhic ";
            else if (attackerValueLost > 2 * defenderValueLost) lossRatio = "Costly ";
            if (remainingAttackerPct > .9)
                survivorRatio = "Flawless";
            else if (remainingAttackerPct > .6)
                survivorRatio = "Decisive";
            else if (remainingAttackerPct > .2)
                survivorRatio = "Adequate";
            else
                survivorRatio = "Marginal";
            return $"{lossRatio}{survivorRatio} Attacker Victory";
        }
        else
        {
            if (defenderValueLost > 4 * attackerValueLost)
                lossRatio = "Pyrrhic ";
            else if (defenderValueLost > 2 * attackerValueLost) lossRatio = "Costly ";
            if (remainingDefenderPct > .9)
                survivorRatio = "Flawless";
            else if (remainingDefenderPct > .6)
                survivorRatio = "Decisive";
            else if (remainingDefenderPct > .2)
                survivorRatio = "Adequate";
            else
                survivorRatio = "Marginal";
            return $"{lossRatio}{survivorRatio} Defender Victory";
        }
    }

    public string AttackerSummary(int remAttackers)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Attackers Remaining: {remAttackers} / {Attackers}");
        GetRaceStats(sb, AttackerStats);
        return sb.ToString();
    }

    public string DefenderSummary(int remDefenders, int remGarrison)
    {
        if (remDefenders < 0) remDefenders = 0;
        if (remGarrison < 0) remGarrison = 0;
        StringBuilder sb = new StringBuilder();
        if (Defenders > 0) sb.AppendLine($"Defenders Remaining: {remDefenders} / {Defenders}");
        if (Garrison > 0) sb.AppendLine($"Garrison Remaining: {remGarrison} / {Garrison}");
        GetRaceStats(sb, DefenderStats);
        return sb.ToString();
    }

    private void GetRaceStats(StringBuilder sb, RaceStats stats)
    {
        foreach (var item in stats.DamageDealtBy)
        {
            sb.AppendLine($"{item.Key.Name} - Damage Dealt: {stats.DamageDealtBy[item.Key]} - Kills: {(stats.KillsWith.ContainsKey(item.Key) ? stats.KillsWith[item.Key] : 0)}");
        }

        if (stats.DamageDealtBySpell != null && stats.KillsWithSpell != null)
        {
            foreach (var item in stats.DamageDealtBySpell)
            {
                sb.AppendLine($"{item.Key.Name} - Damage Dealt: {stats.DamageDealtBySpell[item.Key]} - Kills: {(stats.KillsWithSpell.ContainsKey(item.Key) ? stats.KillsWithSpell[item.Key] : 0)}");
            }
        }

        sb.AppendLine($"Hits: {stats.Hits}");
        sb.AppendLine($"Misses: {stats.Misses}");
        sb.AppendLine($"Total HP Healed: {stats.TotalHpHealed}");
        sb.AppendLine($"Targets Vored: {stats.TargetsVored}");
        sb.AppendLine($"Prey Freed: {stats.TargetsFreed}");
        sb.AppendLine($"Prey Escaped: {stats.TargetsEscaped}");
        sb.AppendLine($"Prey Regurgitated: {stats.TargetsRegurgitated}");
        sb.AppendLine($"Prey Digested: {stats.TargetsDigested}");
        if (stats.AlliesEaten > 0) sb.AppendLine($"Allies Eaten: {stats.AlliesEaten}");
    }

    public void RegisterKill(Weapon weapon, Side attackerSide)
    {
        if (ReferenceEquals(attackerSide, AttackerSide))
        {
            if (AttackerStats.KillsWith.ContainsKey(weapon))
                AttackerStats.KillsWith[weapon] += 1;
            else
                AttackerStats.KillsWith[weapon] = 1;
        }
        else
        {
            if (DefenderStats.KillsWith.ContainsKey(weapon))
                DefenderStats.KillsWith[weapon] += 1;
            else
                DefenderStats.KillsWith[weapon] = 1;
        }
    }

    internal void RegisterKill(Spell spell, Side attackerSide)
    {
        if (Equals(attackerSide, AttackerSide))
        {
            if (AttackerStats.KillsWithSpell == null) AttackerStats.KillsWithSpell = new Dictionary<Spell, int>();
            if (AttackerStats.KillsWithSpell.ContainsKey(spell))
                AttackerStats.KillsWithSpell[spell] += 1;
            else
                AttackerStats.KillsWithSpell[spell] = 1;
        }
        else
        {
            if (DefenderStats.KillsWithSpell == null) DefenderStats.KillsWithSpell = new Dictionary<Spell, int>();
            if (DefenderStats.KillsWithSpell.ContainsKey(spell))
                DefenderStats.KillsWithSpell[spell] += 1;
            else
                DefenderStats.KillsWithSpell[spell] = 1;
        }
    }

    public void RegisterHit(Weapon weapon, int damage, Side attackerSide)
    {
        if (Equals(attackerSide, AttackerSide))
        {
            if (AttackerStats.DamageDealtBy.ContainsKey(weapon))
                AttackerStats.DamageDealtBy[weapon] += damage;
            else
                AttackerStats.DamageDealtBy[weapon] = damage;
            AttackerStats.Hits++;
        }
        else
        {
            if (DefenderStats.DamageDealtBy.ContainsKey(weapon))
                DefenderStats.DamageDealtBy[weapon] += damage;
            else
                DefenderStats.DamageDealtBy[weapon] = damage;
            DefenderStats.Hits++;
        }
    }

    internal void RegisterHit(Spell spell, int damage, Side attackerSide)
    {
        if (Equals(attackerSide, AttackerSide))
        {
            if (AttackerStats.DamageDealtBySpell == null) AttackerStats.DamageDealtBySpell = new Dictionary<Spell, int>();
            if (AttackerStats.DamageDealtBySpell.ContainsKey(spell))
                AttackerStats.DamageDealtBySpell[spell] += damage;
            else
                AttackerStats.DamageDealtBySpell[spell] = damage;
            AttackerStats.Hits++;
        }
        else
        {
            if (DefenderStats.DamageDealtBySpell == null) DefenderStats.DamageDealtBySpell = new Dictionary<Spell, int>();
            if (DefenderStats.DamageDealtBySpell.ContainsKey(spell))
                DefenderStats.DamageDealtBySpell[spell] += damage;
            else
                DefenderStats.DamageDealtBySpell[spell] = damage;
            DefenderStats.Hits++;
        }
    }

    public void RegisterMiss(Side attackerSide)
    {
        if (Equals(attackerSide, AttackerSide))
            AttackerStats.Misses++;
        else
            DefenderStats.Misses++;
    }

    public void RegisterHealing(int amount, Side attackerSide)
    {
        if (Equals(attackerSide, AttackerSide))
            AttackerStats.TotalHpHealed += amount;
        else
            DefenderStats.TotalHpHealed += amount;
    }

    public void RegisterVore(Side attackerSide)
    {
        if (Equals(attackerSide, AttackerSide))
            AttackerStats.TargetsVored++;
        else
            DefenderStats.TargetsVored++;
    }

    public void RegisterAllyVore(Side attackerSide)
    {
        if (ReferenceEquals(attackerSide, AttackerSide))
            AttackerStats.AlliesEaten++;
        else
            DefenderStats.AlliesEaten++;
    }

    public void RegisterEscape(Side attackerSide)
    {
        if (Equals(attackerSide, AttackerSide))
            AttackerStats.TargetsEscaped++;
        else
            DefenderStats.TargetsEscaped++;
    }

    public void RegisterFreed(Side attackerSide)
    {
        if (Equals(attackerSide, AttackerSide))
            AttackerStats.TargetsFreed++;
        else
            DefenderStats.TargetsFreed++;
    }

    public void RegisterRegurgitation(Side attackerSide)
    {
        if (Equals(attackerSide, AttackerSide))
            AttackerStats.TargetsRegurgitated++;
        else
            DefenderStats.TargetsRegurgitated++;
    }

    public void RegisterDigestion(Side attackerSide)
    {
        if (Equals(attackerSide, AttackerSide))
            AttackerStats.TargetsDigested++;
        else
            DefenderStats.TargetsDigested++;
    }
}