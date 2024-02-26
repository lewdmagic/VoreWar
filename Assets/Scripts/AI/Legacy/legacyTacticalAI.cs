using OdinSerializer;
using System.Collections.Generic;

namespace LegacyAI
{
    public class LegacyTacticalAI : ITacticalAI
    {
        [OdinSerialize]
        private List<ActorUnit> _actors;

        [OdinSerialize]
        private TacticalTileType[,] _tiles;

        private TacticalTileType[,] Tiles { get => _tiles; set => _tiles = value; }

        private bool _didAction;
        private bool _foundPath;

        [OdinSerialize]
        private Side _aISide;

        private Side AISide { get => _aISide; set => _aISide = value; }
        private List<PathNode> _path;
        private ActorUnit _pathIsFor;

        public TacticalAI.RetreatConditions RetreatPlan { get { return null; } set { } }

        [OdinSerialize]
        public bool ForeignTurn;

        bool ITacticalAI.ForeignTurn { get { return ForeignTurn; } set => ForeignTurn = value; }

        public LegacyTacticalAI(List<ActorUnit> actors, TacticalTileType[,] tiles, Side aIteam)
        {
            AISide = aIteam;
            this.Tiles = tiles;
            this._actors = actors;
        }

        public bool RunAI()
        {
            foreach (ActorUnit actor in _actors)
            {
                if (actor.Targetable == true && Equals(actor.Unit.Side, AISide) && actor.Movement > 0)
                {
                    if (_path != null && _pathIsFor == actor)
                    {
                        if (_path.Count == 0)
                        {
                            _path = null;
                            continue;
                        }

                        Vec2I newLoc = new Vec2I(_path[0].X, _path[0].Y);
                        _path.RemoveAt(0);
                        if (actor.MoveTo(newLoc, Tiles, State.GameManager.TacticalMode.RunningFriendlyAI ? Config.TacticalFriendlyAIMovementDelay : Config.TacticalAIMovementDelay) == false)
                        {
                            //Can't move -- most likely a multiple movement point tile when on low MP
                            actor.Movement = 0;
                            _path = null;
                            return true;
                        }

                        if (actor.Movement == 1 && IsRanged(actor) && TacticalUtilities.TileContainsMoreThanOneUnit(actor.Position.X, actor.Position.Y) == false)
                        {
                            _path = null;
                        }
                        else if (_path.Count == 0 || actor.Movement == 0)
                        {
                            _path = null;
                        }

                        return true;
                    }
                    else
                    {
                        _foundPath = false;
                        _didAction = false;
                        //do action
                        RunPred(actor);
                        _pathIsFor = actor;
                        if (_foundPath || _didAction) return true;
                        if (IsRanged(actor))
                            RunRanged(actor);
                        else
                            RunMelee(actor);
                        if (_foundPath || _didAction) return true;
                        //If no path to any targets, will sit out its turn
                        actor.ClearMovement();
                        return true;
                    }
                }
            }

            return false;
        }


        public bool RunPred(ActorUnit actor)
        {
            if (actor.Unit.Predator == false) return false;
            int index = -1;
            int chance = 0;
            float distance = 64;
            float cap = actor.PredatorComponent.FreeCap();
            if (cap >= 8)
            {
                for (int i = 0; i < _actors.Count; i++)
                {
                    float d = _actors[i].Position.GetDistance(actor.Position);
                    if (_actors[i].InSight == true && _actors[i].InSight == true && _actors[i].Targetable == true && d < 8)
                    {
                        ActorUnit unit = _actors[i];
                        if (!Equals(unit.Unit.Side, AISide) && unit.Bulk() <= cap)
                        {
                            int c = (int)(100 * unit.GetDevourChance(actor, true));
                            if (c > 50 && c > chance && TacticalUtilities.FreeSpaceAroundTarget(_actors[i].Position, actor) && unit.AIAvoidEat <= 0)
                            {
                                chance = c;
                                index = i;
                                distance = d;
                            }
                        }
                    }
                }

                if (index != -1)
                {
                    if (distance < 2)
                    {
                        actor.PredatorComponent.UsePreferredVore(_actors[index]);
                        _didAction = true;
                        return true;
                    }
                    else if (distance < 8)
                    {
                        return Walkto(actor, _actors[index].Position, 8);
                    }
                }
            }

            return false;
        }

        private bool Walkto(ActorUnit actor, Vec2I p)
        {
            _path = TacticalPathfinder.GetPath(actor.Position, p, 1, actor);
            if (_path == null || _path.Count == 0)
            {
                return false;
            }

            _foundPath = true;
            return true;
        }

        private bool Walkto(ActorUnit actor, Vec2I p, int maxDistance)
        {
            _path = TacticalPathfinder.GetPath(actor.Position, p, 1, actor, maxDistance);
            if (_path == null || _path.Count == 0)
            {
                return false;
            }

            _foundPath = true;
            return true;
        }


        private bool RandomWalk(ActorUnit actor)
        {
            int r = State.Rand.Next(8);
            int d = 8;
            while (!actor.Move(r, Tiles))
            {
                r++;
                d--;
                if (r > 7)
                {
                    r = 0;
                }

                if (d < 1)
                {
                    actor.Movement = 0;
                    break;
                }
            }

            _didAction = true;
            return true;
        }

        private bool RunRanged(ActorUnit actor)
        {
            float distance = 64;
            int index = -1;
            for (int i = 0; i < _actors.Count; i++)
            {
                float d = _actors[i].Position.GetNumberOfMovesDistance(actor.Position);
                if (_actors[i].InSight == true && _actors[i].InSight == true && _actors[i].Targetable == true)
                {
                    ActorUnit unit = _actors[i];
                    if (!Equals(unit.Unit.Side, AISide) && d < distance && (d > 1 || (actor.BestRanged.Omni && d > 0)))
                    {
                        index = i;
                        distance = d;
                    }
                }
            }

            if (index == -1)
            {
                bool walked = RandomWalk(actor);
                if (walked)
                {
                    _didAction = true;
                    return true;
                }

                _didAction = true;
                RunMelee(actor); // Surrounded
                actor.ClearMovement();
                return true;
            }
            else
            {
                distance = _actors[index].Position.GetDistance(actor.Position);
                if (distance >= actor.Unit.GetBestRanged().Range)
                {
                    return Walkto(actor, _actors[index].Position);
                }
                else
                {
                    if (distance < 2)
                    {
                        bool walked = RandomWalk(actor);
                        if (walked)
                        {
                            _didAction = true;
                            return true;
                        }

                        _didAction = true;
                        RunMelee(actor); // Surrounded
                        actor.ClearMovement();
                        return true;
                    }
                    else
                    {
                        _didAction = true;
                        actor.Attack(_actors[index], true);
                    }

                    return true;
                }
            }
        }

        private bool RunMelee(ActorUnit actor)
        {
            //move towards closest target
            float distance = 64;
            int index = -1;
            for (int i = 0; i < _actors.Count; i++)
            {
                float d = _actors[i].Position.GetDistance(actor.Position);
                if (_actors[i].InSight == true && _actors[i].InSight == true && _actors[i].Targetable == true)
                {
                    ActorUnit unit = _actors[i];
                    if (!Equals(unit.Unit.Side, AISide))
                    {
                        if (d < distance)
                        {
                            index = i;
                            distance = d;
                        }
                    }
                }
            }

            if (index == -1)
            {
                bool walked = RandomWalk(actor);
                if (walked)
                {
                    _didAction = true;
                    return true;
                }

                _didAction = true;
                actor.ClearMovement();
                return true;
            }
            else
            {
                distance = _actors[index].Position.GetDistance(actor.Position);
                if (distance < 2)
                {
                    if (_actors[index] == null)
                    {
                        actor.ClearMovement();
                        return true;
                    }

                    _didAction = true;
                    actor.Attack(_actors[index], false);
                    return true;
                }
                else
                {
                    return Walkto(actor, _actors[index].Position);
                }
            }
        }


        private bool IsRanged(ActorUnit actor) => actor.Unit.GetBestRanged() != null;
    }
}