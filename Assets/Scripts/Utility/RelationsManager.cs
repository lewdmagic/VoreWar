using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal static class RelationsManager
{
    /// <summary>
    ///     Creates the network of relations from scratch (leaves monsters to be auto-created)
    /// </summary>
    internal static void ResetRelations()
    {
        var sides = State.World.MainEmpires.Select(s => s.Side).ToList();
        sides.Add(Race.Goblin.ToSide());
        State.World.Relations = new Dictionary<Side, Dictionary<Side, Relationship>>();
        foreach (Side side in sides)
        {
            State.World.Relations[side] = new Dictionary<Side, Relationship>();
            foreach (Side targetSide in sides)
            {
                State.World.Relations[side][targetSide] = new Relationship(State.World.GetEmpireOfSide(side)?.Team ?? -1, State.World.GetEmpireOfSide(targetSide)?.Team ?? -1);
            }
        }
    }

    internal static void ResetMonsterRelations()
    {
        var sides = State.World.AllActiveEmpires.Select(s => s.Side).ToList();
        foreach (Side side in sides)
        {
            foreach (Side targetSide in sides)
            {
                if (RaceFuncs.IsMonstersOrUniqueMercsOrRebelsOrBandits(side) || RaceFuncs.IsMonstersOrUniqueMercsOrRebelsOrBandits(targetSide))
                {
                    if (State.World.Relations.ContainsKey(side)) State.World.Relations[side].Remove(targetSide);
                }
            }
        }
    }

    /// <summary>
    ///     Resets the type of relation to be based on the teams, but doesn't change the actual relations values
    /// </summary>
    internal static void ResetRelationTypes()
    {
        var sides = State.World.MainEmpires.Select(s => s.Side).ToList();
        foreach (Side side in sides)
        {
            foreach (Side targetSide in sides)
            {
                RelationState newType = State.World.GetEmpireOfSide(side).Team == State.World.GetEmpireOfSide(targetSide).Team ? RelationState.Allied : RelationState.Enemies;
                GetRelation(side, targetSide).Type = newType;
            }
        }
    }

    internal static void TeamUpdated(Empire empire)
    {
        foreach (Empire otherEmp in State.World.AllActiveEmpires)
        {
            var rel = GetRelation(empire.Side, otherEmp.Side);
            var counterRel = GetRelation(otherEmp.Side, empire.Side);
            if (rel.Type == RelationState.Allied && otherEmp.Team != empire.Team)
            {
                rel.Type = RelationState.Enemies;
                rel.Attitude = -1;
                counterRel.Type = RelationState.Enemies;
                counterRel.Attitude = -1;
            }

            if ((rel.Type == RelationState.Enemies || rel.Type == RelationState.Neutral) && otherEmp.Team == empire.Team)
            {
                rel.Type = RelationState.Allied;
                rel.Attitude = 2;
                counterRel.Type = RelationState.Allied;
                counterRel.Attitude = 2;
            }
        }
    }

    internal static Relationship GetRelation(Side sideA, Side sideB)
    {
        // Special case. Previously 0 was used as a no side and I have no idea
        // what kind of behavior it led to, but im pretty sure it was not intentional
        // or required
        if (sideA == null || sideB == null)
        {
            return new Relationship(0, 0);
        }

        if (State.World.Relations == null)
        {
            ResetRelations();
        }

        if (RaceFuncs.IsRebelOrBandit(sideA) || RaceFuncs.IsRebelOrBandit(sideB))
        {
            if (Equals(sideA, sideB))
            {
                return new Relationship(0, 0);
            }
            else
            {
                return new Relationship(-1, -1);
            }
        }

        if (State.World.Relations.TryGetValue(sideA, out var dict))
        {
            if (dict.TryGetValue(sideB, out var rel))
            {
                return rel;
            }

            var empAI = State.World.GetEmpireOfSide(sideA);
            var empBi = State.World.GetEmpireOfSide(sideB);
            if (empAI == null || empBi == null)
            {
                Debug.Log($"Invalid relationship returned between {sideA} and {sideB}");
                return new Relationship(0, 1);
            }

            Relationship newRel = new Relationship(empAI.Team, empBi.Team);
            dict[sideB] = newRel;
            return newRel;
        }

        var empA = State.World.GetEmpireOfSide(sideA);
        var empB = State.World.GetEmpireOfSide(sideB);
        if (empA == null || empB == null)
        {
            Debug.Log($"Invalid relationship returned between {sideA} and {sideB}");
            return new Relationship(0, 1);
        }

        var newDict = new Dictionary<Side, Relationship>();
        State.World.Relations[sideA] = newDict;
        Relationship newRel2 = new Relationship(empA.Team, empB.Team);
        newDict[sideB] = newRel2;
        return newRel2;
    }

    private struct RelCata
    {
        internal float Goal;
        internal float IncreaseMult;
        internal float IncreaseAdd;
        internal float DecreaseMult;
        internal float DecreaseAdd;

        public RelCata(float goal, float increaseMult, float increaseAdd, float decreaseMult, float decreaseAdd)
        {
            Goal = goal;
            IncreaseMult = increaseMult;
            IncreaseAdd = increaseAdd;
            DecreaseMult = decreaseMult;
            DecreaseAdd = decreaseAdd;
        }
    }

    internal static void TurnElapsed()
    {
        RelCata neutral;
        RelCata allied;
        RelCata enemies;
        switch (Config.DiplomacyScale)
        {
            case DiplomacyScale.Default:
                neutral = new RelCata(0, .02f, .02f, .002f, .004f);
                allied = new RelCata(2, .02f, .01f, .002f, .003f);
                enemies = new RelCata(-.25f, .01f, .01f, .01f, .005f);
                break;
            case DiplomacyScale.Suspicious:
                neutral = new RelCata(-0.2f, .02f, .02f, .005f, .0075f);
                allied = new RelCata(1.5f, .02f, .01f, .005f, .0055f);
                enemies = new RelCata(-2f, .01f, .01f, .02f, .01f);
                break;
            case DiplomacyScale.Distrustful:
                neutral = new RelCata(-0.4f, .02f, .02f, .02f, .02f);
                allied = new RelCata(0.8f, .02f, .01f, .01f, .015f);
                enemies = new RelCata(-3f, .01f, .01f, .03f, .03f);
                break;
            case DiplomacyScale.Friendly:
                neutral = new RelCata(0.5f, .04f, .04f, .002f, .004f);
                allied = new RelCata(3f, .06f, .06f, .002f, .003f);
                enemies = new RelCata(0, .04f, .04f, .01f, .002f);
                break;
            default: //No scaling
                return;
        }

        foreach (var list in State.World.Relations.Values)
        {
            foreach (var rel in list.Values)
            {
                switch (rel.Type)
                {
                    case RelationState.Neutral:
                        Update(rel, neutral);
                        break;
                    case RelationState.Allied:
                        Update(rel, allied);
                        break;
                    case RelationState.Enemies:
                        Update(rel, enemies);
                        break;
                }
            }
        }

        void Update(Relationship rel, RelCata cata)
        {
            if (rel.TurnsSinceAsked >= 0) rel.TurnsSinceAsked++;
            if (rel.Attitude < cata.Goal)
            {
                rel.Attitude = Mathf.Lerp(rel.Attitude, cata.Goal, cata.IncreaseMult);
                rel.Attitude += cata.IncreaseAdd;
            }

            if (rel.Attitude > cata.Goal)
            {
                rel.Attitude = Mathf.Lerp(rel.Attitude, cata.Goal, cata.DecreaseMult);
                rel.Attitude -= cata.DecreaseAdd;
            }
        }
    }

    internal static void SetWar(Empire sideA, Empire sideB)
    {
        var relation = GetRelation(sideA.Side, sideB.Side);
        relation.Type = RelationState.Enemies;
        var counterRelation = GetRelation(sideB.Side, sideA.Side);
        counterRelation.Type = RelationState.Enemies;
        relation.TurnsSinceAsked = -1;
        counterRelation.TurnsSinceAsked = -1;
    }

    internal static void SetPeace(Empire sideA, Empire sideB)
    {
        var relation = GetRelation(sideA.Side, sideB.Side);
        relation.Type = RelationState.Neutral;
        var counterRelation = GetRelation(sideB.Side, sideA.Side);
        counterRelation.Type = RelationState.Neutral;
        relation.TurnsSinceAsked = -1;
        counterRelation.TurnsSinceAsked = -1;
    }

    internal static void SetAlly(Empire sideA, Empire sideB)
    {
        var relation = GetRelation(sideA.Side, sideB.Side);
        relation.Type = RelationState.Allied;
        var counterRelation = GetRelation(sideB.Side, sideA.Side);
        counterRelation.Type = RelationState.Allied;
        relation.TurnsSinceAsked = -1;
        counterRelation.TurnsSinceAsked = -1;
    }

    internal static void VillageAttacked(Empire attacker, Empire defender)
    {
        if (attacker is MonsterEmpire || defender is MonsterEmpire) return;
        if (GetRelation(defender.Side, attacker.Side).Attitude > -.25f) GetRelation(defender.Side, attacker.Side).Attitude = -.25f;
        GetRelation(defender.Side, attacker.Side).Attitude -= .3f;
        foreach (Empire emp in State.World.MainEmpires)
        {
            if (emp == attacker || emp == defender) continue;
            var attackerRel = GetRelation(emp.Side, attacker.Side);
            var defenderRel = GetRelation(emp.Side, defender.Side);
            if (defenderRel.Type == RelationState.Allied)
            {
                attackerRel.Attitude -= .15f;
            }
            else if (defenderRel.Type == RelationState.Enemies)
            {
                attackerRel.Attitude += .1f;
            }
        }
    }

    internal static void GoldMineTaken(Empire attacker, Empire defender)
    {
        if (attacker is MonsterEmpire || defender is MonsterEmpire) return;
        if (defender == null) return;
        if (GetRelation(defender.Side, attacker.Side).Attitude > 0) GetRelation(defender.Side, attacker.Side).Attitude *= .75f;
        GetRelation(defender.Side, attacker.Side).Attitude -= .1f;
        foreach (Empire emp in State.World.MainEmpires)
        {
            if (emp == attacker || emp == defender) continue;
            var attackerRel = GetRelation(emp.Side, attacker.Side);
            var defenderRel = GetRelation(emp.Side, defender.Side);
            if (defenderRel.Type == RelationState.Allied)
            {
                attackerRel.Attitude -= .05f;
            }
            else if (defenderRel.Type == RelationState.Enemies)
            {
                attackerRel.Attitude += .0375f;
            }
        }
    }

    internal static void ArmyAttacked(Empire attacker, Empire defender)
    {
        if (attacker is MonsterEmpire || defender is MonsterEmpire) return;
        GetRelation(defender.Side, attacker.Side).Attitude -= .2f;
        foreach (Empire emp in State.World.MainEmpires)
        {
            if (emp == attacker || emp == defender) continue;
            var attackerRel = GetRelation(emp.Side, attacker.Side);
            var defenderRel = GetRelation(emp.Side, defender.Side);
            if (defenderRel.Type == RelationState.Allied)
            {
                attackerRel.Attitude -= .1f;
            }
            else if (defenderRel.Type == RelationState.Enemies)
            {
                attackerRel.Attitude += .075f;
            }
        }
    }

    internal static void CityReturned(Empire giver, Empire receiver)
    {
        if (giver == null || receiver == null) return;
        if (giver is MonsterEmpire || receiver is MonsterEmpire) return;
        GetRelation(receiver.Side, giver.Side).Attitude += 1f;
        foreach (Empire emp in State.World.MainEmpires)
        {
            if (emp == giver || emp == receiver) continue;
            var giverRel = GetRelation(emp.Side, giver.Side);
            var receiverRel = GetRelation(emp.Side, receiver.Side);
            if (receiverRel.Type == RelationState.Allied)
            {
                giverRel.Attitude += .2f;
            }
        }
    }

    internal static void ChangeRelations(Empire likee, Empire liker, float increase)
    {
        var relation = GetRelation(liker.Side, likee.Side);
        relation.Attitude += increase;
    }

    internal static void MakeHate(Empire attacker, Empire defender)
    {
        var relation = GetRelation(defender.Side, attacker.Side);
        if (relation.Attitude > -1.5f) relation.Attitude = -1.5f;
        SetWar(attacker, defender);
    }

    internal static void MakeLike(Empire likee, Empire liker, float setMinRelation = 1)
    {
        var relation = GetRelation(liker.Side, likee.Side);
        if (relation.Attitude < setMinRelation) relation.Attitude = setMinRelation;
        SetAlly(likee, liker);
    }

    internal static void Genocide(Empire attacker, Empire defender)
    {
        if (attacker is MonsterEmpire || defender is MonsterEmpire) return;
        GetRelation(defender.Side, attacker.Side).Attitude -= .6f;
        foreach (Empire emp in State.World.MainEmpires)
        {
            if (emp == attacker || emp == defender) continue;
            var attackerRel = GetRelation(emp.Side, attacker.Side);
            var defenderRel = GetRelation(emp.Side, defender.Side);
            if (defenderRel.Type == RelationState.Allied)
            {
                attackerRel.Attitude -= .4f;
            }
            else if (defenderRel.Type == RelationState.Enemies)
            {
                attackerRel.Attitude += .1f;
            }
        }
    }

    internal static void AskPlayerForPeace(Empire ai, Empire player)
    {
        var box = State.GameManager.CreateDialogBox();
        State.GameManager.ActiveInput = true;
        box.SetData(() =>
            {
                SetPeace(ai, player);
                State.GameManager.ActiveInput = false;
            }, "Accept", "Reject", $"The {ai.Name} wants to know if you ({player.Name}) would accept a peace treaty?", () =>
            {
                GetRelation(ai.Side, player.Side).TurnsSinceAsked = 0;
                State.GameManager.ActiveInput = false;
            });
    }

    internal static void AskPlayerForAlliance(Empire ai, Empire player)
    {
        var box = State.GameManager.CreateDialogBox();
        State.GameManager.ActiveInput = true;
        box.SetData(() =>
            {
                SetAlly(ai, player);
                State.GameManager.ActiveInput = false;
            }, "Accept", "Reject", $"The {ai.Name} wants to know if you ({player.Name}) would accept an alliance?", () =>
            {
                GetRelation(ai.Side, player.Side).TurnsSinceAsked = 0;
                State.GameManager.ActiveInput = false;
            });
    }
}