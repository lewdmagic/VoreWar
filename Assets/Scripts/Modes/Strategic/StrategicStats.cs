using System.Collections.Generic;
using OdinSerializer;
using System.Text;



public class StrategicStats
{
    class RaceStats
    {
        [OdinSerialize]
        public string EmpireName;
        [OdinSerialize]
        public int BattlesWon;
        [OdinSerialize]
        public int BattlesLost;
        [OdinSerialize]
        public int ArmiesLost;
        [OdinSerialize]
        public int LeaderResurrections;
        [OdinSerialize]
        public int TotalGoldCollected;
        [OdinSerialize]
        public int TotalGoldSpent;
        [OdinSerialize]
        public int GoldSpentOnEquipment;
        [OdinSerialize]
        public int GoldSpentOnBuildings;
        [OdinSerialize]
        public int GoldSpentOnTraining;
        [OdinSerialize]
        public int GoldSpentOnMaintainingArmies;
        [OdinSerialize]
        public int SoldiersRecruited;
        [OdinSerialize]
        public int SoldiersLost;

        public RaceStats(string empireName)
        {
            EmpireName = empireName;
            TotalGoldCollected = Config.StartingGold;
        }

    }
    [OdinSerialize]
    Dictionary<Side, RaceStats> EmpireStats;

    public StrategicStats()
    {
        EmpireStats = new Dictionary<Side, RaceStats>();
        foreach (Empire empire in State.World.MainEmpires)
        {
            
            
            EmpireStats[empire.Side] = new RaceStats(empire.Side.ToString());
        }
    }

    public void ExpandToIncludeNewRaces()
    {
        var empireStats = new Dictionary<Side, RaceStats>();
        foreach (Empire empire in State.World.MainEmpires)
        {
            if (EmpireStats.TryGetValue(empire.Side, out RaceStats stats))
            {
                empireStats[empire.Side] = EmpireStats[empire.Side];
            }
            else
            {
                empireStats[empire.Side] = new RaceStats(empire.Side.ToString());
            }
        }
        EmpireStats = empireStats;
    }

    public string Summary()
    {
        StringBuilder sb = new StringBuilder();
        foreach (RaceStats race in EmpireStats.Values)
        {
            if (race.TotalGoldCollected == Config.StartingGold || (race.BattlesLost == 0 && race.BattlesWon == 0))
                continue;
            sb.AppendLine($"Empire of {race.EmpireName}");
            sb.AppendLine($"Battles Won: {race.BattlesWon}");
            sb.AppendLine($"Battles Lost: {race.BattlesLost}");
            sb.AppendLine($"Armies Lost: {race.ArmiesLost}");
            if (Config.FactionLeaders)
                sb.AppendLine($"Times Leader Resurrected: {race.LeaderResurrections}");
            sb.AppendLine($"Gold Collected: {race.TotalGoldCollected}");
            sb.AppendLine($"Gold Spent: {race.TotalGoldSpent}");
            sb.AppendLine($"Gold Spent on Army Equipment: {race.GoldSpentOnEquipment}");
            sb.AppendLine($"Gold Spent on Buildings: {race.GoldSpentOnBuildings}");
            sb.AppendLine($"Gold Spent on Army Training: {race.GoldSpentOnTraining}");
            sb.AppendLine($"Gold Spent on Army Maintenance: {race.GoldSpentOnMaintainingArmies}");
            sb.AppendLine($"Soldiers Recruited: {race.SoldiersRecruited}");
            sb.AppendLine($"Soldiers Lost: {race.SoldiersLost}");
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public void BattleResolution(Side winner, Side loser)
    {
        RaceStats stats;
        if (EmpireStats.TryGetValue(winner, out stats))
        {
            stats.BattlesWon++;
        }
        if (EmpireStats.TryGetValue(loser, out stats))
        {
            stats.BattlesLost++;
        }
    }

    public void LostArmy(Side side)
    {
        if (EmpireStats.TryGetValue(side, out RaceStats stats))
        {
            stats.ArmiesLost++;
        }
    }

    public void ResurrectedLeader(Side side)
    {
        if (EmpireStats.TryGetValue(side, out RaceStats stats))
        {
            stats.LeaderResurrections++;
        }
    }

    public void CollectedGold(int amount, Side side)
    {
        if (EmpireStats.TryGetValue(side, out RaceStats stats))
        {
            stats.TotalGoldCollected++;
        }
    }

    public void SpentGold(int amount, Side side)
    {
        if (EmpireStats.TryGetValue(side, out RaceStats stats))
        {
            stats.TotalGoldSpent++;
        }
    }

    public void SpentGoldOnArmyEquipment(int amount, Side side)
    {
        if (EmpireStats.TryGetValue(side, out RaceStats stats))
        {
            stats.GoldSpentOnEquipment++;
        }
    }

    public void SpentGoldOnBuildings(int amount, Side side)
    {
        if (EmpireStats.TryGetValue(side, out RaceStats stats))
        {
            stats.GoldSpentOnBuildings++;
        }
    }

    public void SpentGoldOnArmyTraining(int amount, Side side)
    {
        if (EmpireStats.TryGetValue(side, out RaceStats stats))
        {
            stats.GoldSpentOnTraining++;
        }
    }

    public void SpentGoldOnArmyMaintenance(int amount, Side side)
    {
        if (EmpireStats.TryGetValue(side, out RaceStats stats))
        {
            stats.TotalGoldSpent++;
            stats.GoldSpentOnMaintainingArmies++;
        }
    }

    public void SoldiersRecruited(int amount, Side side)
    {
        if (EmpireStats.TryGetValue(side, out RaceStats stats))
        {
            stats.SoldiersRecruited++;
        }
    }

    public void SoldiersLost(int amount, Side side)
    {
        if (EmpireStats.TryGetValue(side, out RaceStats stats))
        {
            stats.SoldiersLost++;
        }
    }




}
