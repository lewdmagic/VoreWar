using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EmpireReport : MonoBehaviour
{
    private Dictionary<Side, EmpireReportItem> Reports = new Dictionary<Side, EmpireReportItem>();

    public GameObject ReportItemPrefab;
    public Transform ReportFolder;

    public DiplomacyScreen DiplomacyScreen;

    private bool pausedState = false;

    private readonly int GoblinNum = RaceFuncs.MainRaceCount;
    private readonly int FirstMonster = RaceFuncs.MainRaceCount + 1;

    private EmpireReportItem GetReportItem(Side side)
    {
        return Reports.GetOrSet(side, () => Instantiate(ReportItemPrefab, ReportFolder).GetComponent<EmpireReportItem>());
    }

    public void Open()
    {
        pausedState = State.GameManager.StrategyMode.Paused;
        State.GameManager.StrategyMode.Paused = true;
        gameObject.SetActive(true);

        foreach (Race race in RaceFuncs.MainRaceEnumerable())
        {
            Side side = race.ToSide();
            GetReportItem(side).Contact.onClick.RemoveAllListeners();
            GetReportItem(side).Contact.gameObject.SetActive(!Equals(State.World.ActingEmpire.Side, side));
            GetReportItem(side).Contact.onClick.AddListener(() => DiplomacyScreen.Open(State.World.ActingEmpire, State.World.GetEmpireOfSide(side)));
        }

        if (Config.GoblinCaravans)
        {
            Side side = Race.Goblin.ToSide();
            GetReportItem(Race.Goblin.ToSide()).Contact.onClick.RemoveAllListeners();
            GetReportItem(Race.Goblin.ToSide()).Contact.onClick.AddListener(() => DiplomacyScreen.Open(State.World.ActingEmpire, State.World.GetEmpireOfSide(side)));
        }


        Refresh();
    }

    public void Refresh()
    {
        foreach (EmpireReportItem report in Reports.Values)
        {
            report.gameObject.SetActive(false);
        }

        for (int i = 0; i < State.World.MainEmpires.Count; i++)
        {
            Empire empire = State.World.MainEmpires[i];
            Reports[empire.Side].gameObject.SetActive(!empire.KnockedOut);
            if (empire.KnockedOut) continue;

            Reports[empire.Side].EmpireStatus.text = $"{empire.Name}  Villages: {State.World.Villages.Where(s => Equals(s.Side, empire.Side)).Count()}  Mines: {State.World.Claimables.Where(s => s.Owner == empire).Count()} Armies : {empire.Armies.Count()} ";
            if (empire.IsAlly(State.World.ActingEmpire) || Config.CheatExtraStrategicInfo || State.GameManager.StrategyMode.OnlyAIPlayers)
            {
                Reports[empire.Side].EmpireStatus.text += $"Units: {empire.GetAllUnits().Count} Gold: {empire.Gold}  Income: {empire.Income}";
            }
        }

        if (Config.GoblinCaravans)
        {
            Empire empire = State.World.GetEmpireOfRace(Race.Goblin);
            GetReportItem(Race.Goblin.ToSide()).gameObject.SetActive(true);
            if (empire != null)
            {
                GetReportItem(Race.Goblin.ToSide()).EmpireStatus.text = $"{empire.Name}  Armies : {empire.Armies.Count()} ";
                if (empire.IsAlly(State.World.ActingEmpire) || Config.CheatExtraStrategicInfo || State.GameManager.StrategyMode.OnlyAIPlayers)
                {
                    GetReportItem(Race.Goblin.ToSide()).EmpireStatus.text += $"Units: {empire.GetAllUnits().Count}";
                }
            }
        }


        foreach (Empire empire in State.World.MonsterEmpires)
        {
            if (Equals(empire.Race, Race.Goblin)) continue;
            SpawnerInfo spawner = Config.World.GetSpawner(empire.Race);
            if (spawner == null) continue;
            Reports[empire.Side].gameObject.SetActive(spawner.Enabled);
            Reports[empire.Side].Contact.gameObject.SetActive(false);

            Reports[empire.Side].EmpireStatus.text = $"{empire.Name}  Villages: {State.World.Villages.Where(s => Equals(s.Side, empire.Side)).Count()} Armies : {empire.Armies.Count()} ";
            if (empire.IsAlly(State.World.ActingEmpire) || Config.CheatExtraStrategicInfo || State.GameManager.StrategyMode.OnlyAIPlayers)
            {
                Reports[empire.Side].EmpireStatus.text += $"Units: {empire.GetAllUnits().Count}";
            }
        }
    }

    public void CreateDiplomacyReport()
    {
        StringBuilder sb = new StringBuilder();
        List<string> Allies = new List<string>();
        List<string> Neutral = new List<string>();
        List<string> Enemies = new List<string>();
        List<Empire> list = State.World.MainEmpires.Where(s => s.KnockedOut == false).ToList();
        //list.Append(State.World.GetEmpireOfRace(Race.Goblins));

        foreach (Empire emp in list)
        {
            Allies.Clear();
            Neutral.Clear();
            Enemies.Clear();

            foreach (Empire emp2 in list)
            {
                if (emp == emp2) continue;
                if (RaceFuncs.IsRebelOrBandit5(emp.Side) || RaceFuncs.IsRebelOrBandit5(emp2.Side)) continue;
                var relation = RelationsManager.GetRelation(emp.Side, emp2.Side);
                switch (relation.Type)
                {
                    case RelationState.Neutral:
                        Neutral.Add(emp2.Name.ToString());
                        break;
                    case RelationState.Allied:
                        Allies.Add(emp2.Name.ToString());
                        break;
                    case RelationState.Enemies:
                        Enemies.Add(emp2.Name.ToString());
                        break;
                }
            }

            string allies = Allies.Count() > 0 ? $"Allies:<color=blue> {string.Join(", ", Allies)}</color>" : "";
            string neutrals = Neutral.Count() > 0 ? $"Neutral: {string.Join(", ", Neutral)}" : "";
            string enemies;
            if (Enemies.Count > 6)
                enemies = "Enemies: <color=red>all others</color>";
            else
                enemies = Enemies.Count() > 0 ? $"Enemies: <color=red>{string.Join(", ", Enemies)}</color>" : "";

            sb.AppendLine($"{emp.Name} - {allies} {neutrals} {enemies} ");
        }

        State.GameManager.CreateFullScreenMessageBox(sb.ToString());
    }

    public void Close()
    {
        gameObject.SetActive(false);
        State.GameManager.StrategyMode.Paused = pausedState;
    }
}