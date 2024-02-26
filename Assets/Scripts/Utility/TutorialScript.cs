using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TutorialScript
{
    private List<ActorUnit> _tacticalUnits;

    private string _message;
    private bool _warned = false;
    internal int Step = -1;

    internal void InitializeTactical(List<ActorUnit> actors)
    {
        _tacticalUnits = actors;
        if (_tacticalUnits.Count != 5)
        {
            State.GameManager.CreateMessageBox("Seems as though the tutorial save was replaced with a different save, exiting tutorial mode to avoid exceptions");
            State.TutorialMode = false;
            return;
        }

        Config.World.ClothedFraction = 1;
        var allUnits = StrategicUtilities.GetAllUnits();
        foreach (Unit unit in allUnits)
        {
            var race = RaceFuncs.GetRace(unit);
            race.RandomCustomCall(unit);
        }

        State.GameManager.TacticalMode.AttackerName = "Cats";
        State.GameManager.TacticalMode.DefenderName = "Imps";

        foreach (ActorUnit actor in actors)
        {
            actor.Unit.ClearAllTraits();
            actor.PredatorComponent = new PredatorComponent(actor, actor.Unit);
            actor.Unit.Predator = true;
        }


        UpdateStep();
        _tacticalUnits[0].Movement = 9999;
        _tacticalUnits[1].Movement = 0;
        _tacticalUnits[2].Movement = 0;
        for (int i = 0; i < _tacticalUnits.Count(); i++)
        {
            _tacticalUnits[i].AnimationController = new AnimationController();
        }

        _tacticalUnits[0].Unit.SetDefaultBreastSize(2);
        _tacticalUnits[0].Unit.DickSize = -1;
        State.GameManager.TacticalMode.TacticalStats.SetInitialUnits(3, 1, 0, Race.Cat.ToSide(), Race.Imp.ToSide());

        Empire imps = State.World.MainEmpires.First((item) => Equals(item.Race, Race.Imp));
        imps.MaxGarrisonSize = 4;
        imps.Armies[0].SetEmpire(imps);


        Empire cats = State.World.MainEmpires.First((item) => Equals(item.Race, Race.Cat));
        cats.Armies[0].SetEmpire(cats);
        cats.AddGold(1000);

        State.World.Villages[0].UpdateNetBoosts();
        State.World.Villages[1].UpdateNetBoosts();
        State.World.Villages[0].AddPopulation(60);
        State.World.Villages[1].AddPopulation(60);
        State.World.Villages[1].TutorialWeapons();
        State.World.MonsterEmpires = new MonsterEmpire[1];
        State.World.MonsterEmpires[0] = new MonsterEmpire(new Empire.ConstructionArgs(Race.Vagrant, Race.Vagrant.ToSide(), Color.white, Color.white, 9, StrategyAIType.Monster, TacticalAIType.Full, 999, 8, 0));
    }

    internal void CheckStatus()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Step--;
            UpdateStep();
        }

        try
        {
            switch (Step)
            {
                case 0:
                    if (State.GameManager.TacticalMode.ActionMode > 0 && State.GameManager.TacticalMode.ActionMode <= 3) State.GameManager.TacticalMode.ActionMode = 0;
                    if (_tacticalUnits.Count < 3)
                    {
                        Step = 5;
                        UpdateStep();
                    }

                    if (_tacticalUnits[0].Position.GetNumberOfMovesDistance(_tacticalUnits[3].Position) == 1) UpdateStep();
                    break;
                case 1:
                    if (State.GameManager.TacticalMode.ActionMode == 2 || State.GameManager.TacticalMode.ActionMode == 3) State.GameManager.TacticalMode.ActionMode = 0;
                    if (_tacticalUnits.Count < 3)
                    {
                        Step = 5;
                        UpdateStep();
                    }

                    if (_tacticalUnits[0].Movement == 0) UpdateStep();
                    break;
                case 2:
                    if (State.GameManager.TacticalMode.ActionMode > 0 && State.GameManager.TacticalMode.ActionMode <= 3) State.GameManager.TacticalMode.ActionMode = 0;
                    if (_tacticalUnits.Count < 3)
                    {
                        Step = 5;
                        UpdateStep();
                    }

                    if (_tacticalUnits[1].Position.GetNumberOfMovesDistance(_tacticalUnits[3].Position) > 1 && _tacticalUnits[1].Position.GetNumberOfMovesDistance(_tacticalUnits[3].Position) < 6) UpdateStep();
                    break;
                case 3:
                    if (State.GameManager.TacticalMode.ActionMode == 1 || State.GameManager.TacticalMode.ActionMode == 3) State.GameManager.TacticalMode.ActionMode = 0;
                    if (_tacticalUnits.Count < 3)
                    {
                        Step = 5;
                        UpdateStep();
                    }

                    if (_tacticalUnits[1].Movement == 0) UpdateStep();
                    break;
                case 4:
                    if (State.GameManager.TacticalMode.ActionMode > 0 && State.GameManager.TacticalMode.ActionMode <= 3) State.GameManager.TacticalMode.ActionMode = 0;
                    if (_tacticalUnits.Count < 3)
                    {
                        Step = 5;
                        UpdateStep();
                    }

                    if (_tacticalUnits[2].Position.GetNumberOfMovesDistance(_tacticalUnits[3].Position) == 1) UpdateStep();
                    break;
                case 5:
                    if (State.GameManager.TacticalMode.ActionMode == 1 || State.GameManager.TacticalMode.ActionMode == 2) State.GameManager.TacticalMode.ActionMode = 0;
                    if (_tacticalUnits.Count < 3)
                    {
                        Step = 5;
                        UpdateStep();
                    }

                    if (_tacticalUnits[2].Movement == 0) UpdateStep();
                    break;
                case 6:
                    if (State.GameManager.CurrentScene == State.GameManager.StrategyMode) UpdateStep();
                    break;
                case 7:
                    if (State.GameManager.CurrentScene == State.GameManager.RecruitMode) UpdateStep();
                    break;
                case 8:
                    if (State.GameManager.CurrentScene == State.GameManager.StrategyMode) UpdateStep();
                    break;
                case 9:
                    if (State.World.MainEmpires[0].Armies[0].InVillageIndex > -1) UpdateStep();
                    break;
                case 10:
                    if (State.GameManager.CurrentScene == State.GameManager.RecruitMode) UpdateStep();
                    break;
                case 11:
                    if (State.World.MainEmpires[0].Armies[0].Units.Count() >= 10) UpdateStep();
                    break;
                case 12:
                    if (State.World.MainEmpires[0].Armies[0].Units.Count > 0 && State.World.MainEmpires[0].Armies[0].Units.Where(s => s.GetBestMelee() != State.World.ItemRepository.Claws || s.GetBestRanged() != null).Count() == State.World.MainEmpires[0].Armies[0].Units.Count()) UpdateStep();
                    break;
                case 13:
                    if (State.World.Villages[0].Weapons.Count >= 4) UpdateStep();
                    break;
            }
        }
        catch
        {
            if (_warned == false)
            {
                _warned = true;
                State.GameManager.CreateMessageBox("Something has gone wrong, let the developer know that there was an error in step " + Step + " of the tutorial and what you were doing that may have caused it, if known");
            }
        }
    }


    private void UpdateStep()
    {
        Step++;
        switch (Step)
        {
            case 0:
                _message = "Looks like we're under attack, press N to select a unit, then right click next to the enemy unit (with the red border around it) (You can press H to repeat the current message at any point during the tutorial)";
                break;
            case 1:
                _message = "Now press 1 or click the melee button, then left click on the enemy unit to attack";
                break;
            case 2:
                _message = "Good, now press N to select the next unit (it is a ranged unit), and move it to anywhere within 5 tiles of the enemy unit, but not directly bordering it";
                _tacticalUnits[1].Movement = 9999;
                break;
            case 3:
                _message = "Now press 2 or click the ranged button, then left click on the enemy unit to attack";
                break;
            case 4:
                _message = "Now that we've softened it up, it's time to eat it. Press N to select the next unit, and move it right next to the enemy unit.  An alternate way of moving is to press M, then left click, which shows you the movement path";
                _tacticalUnits[2].Movement = 9999;
                _tacticalUnits[2].Unit.ModifyStat(Stat.Voracity, 400);
                _tacticalUnits[3].SubtractHealth(_tacticalUnits[3].Unit.Health - 1);
                break;
            case 5:
                _message = "Now press 3 or click the vore button, then left click on the enemy unit to eat it";
                break;
            case 6:
                _message = "Now, depending on your settings, press end turn and the battle should finish automatically";
                break;
            case 7:
                _message = "One of your units has a level up, so select your army and click on the army info button at the bottom";
                var allUnits = StrategicUtilities.GetAllUnits();
                foreach (Unit unit in allUnits)
                {
                    unit.ReloadTraits();
                    unit.InitializeTraits();
                }

                State.World.MainEmpires[0].Armies[0].RemainingMp = 999;
                break;
            case 8:
                _message = "Select the unit with the orange text, and either level up manually, or let it auto-level your unit with the auto-level button.  Then, when you're ready, exit back to the strategy screen";
                break;
            case 9:
                _message = "Move your army to the friendly city, by either pressing m and left clicking on the city, or just right clicking on the city";
                break;
            case 10:
                _message = "Now left click on your army again, or click the army info button at the bottom to enter the city view";
                break;
            case 11:
                _message = "We need some more units to take on this imp threat, so click recruit unit until you have at least 10 units";
                break;
            case 12:
                _message = "Now, you can either hit the shop manually button, or use the auto-buy button to quickly outfit your troops.  Buy weapons for all of your units now";
                break;
            case 13:
                _message = "It is a good idea to buy some weapons to enable the villagers to fight back if this village gets attacked.   Click the buy weapons button near the top left and buy at least 4 weapons.";
                break;
            case 14:
                _message = "Return to the world view, then right click your army on the enemy village, and battle will start, you can either fight that battle, or quit, because this is basically the end of the tutorial";
                break;
        }

        State.GameManager.CreateMessageBox(_message);
    }
}