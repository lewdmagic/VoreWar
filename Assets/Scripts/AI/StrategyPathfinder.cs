using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class StrategyPathfinder
{
    public static bool Initialized = false;


    private struct Cell
    {
        public bool FriendlyOccupied;
        public bool EnemyOccupied;
        public bool WalkableSurface;
        public int MovementCost;
        public StrategicTileType TileType;
    }

    private struct QueueNode : IComparable<QueueNode>
    {
        public Vec2 Value;
        public int Dist;
        public int Diagonals;

        public QueueNode(Vec2 value, int dist, int diagonals)
        {
            Value = value;
            Dist = dist;
            Diagonals = diagonals;
        }

        public int CompareTo(QueueNode other)
        {
            if (Dist != other.Dist)
            {
                return Dist.CompareTo(other.Dist);
            }
            else if (Diagonals != other.Diagonals)
            {
                return Diagonals.CompareTo(other.Diagonals);
            }
            else
                return Value.CompareTo(other.Value);
        }
    }

    private class Vec2Comparer : IEqualityComparer<Vec2>
    {
        public bool Equals(Vec2 a, Vec2 b)
        {
            return a == b;
        }

        public int GetHashCode(Vec2 obj)
        {
            return (IntegerHash(obj.X)
                    ^ (IntegerHash(obj.Y) << 1)) >> 1;
        }

        private static int IntegerHash(int a)
        {
            // fmix32 from murmurhash
            uint h = (uint)a;
            h ^= h >> 16;
            h *= 0x85ebca6bU;
            h ^= h >> 13;
            h *= 0xc2b2ae35U;
            h ^= h >> 16;
            return (int)h;
        }
    }

    private static bool _flyingPath = false;

    private struct EndPoint
    {
        internal QueueNode Node;
        internal int Priority;

        public EndPoint(QueueNode node, int priority)
        {
            this.Node = node;
            this.Priority = priority;
        }
    }

    private static Cell[,] _grid = null;

    private static List<Vec2> GetNeighbours(Vec2 cell, List<Vec2> neighbours)
    {
        neighbours.Clear();

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                var coord = cell + new Vec2(dx, dy);

                bool notSelf = !(dx == 0 && dy == 0);
                bool connectivity = Math.Abs(dx) + Math.Abs(dy) <= 2;
                bool withinGrid = coord.X >= 0 && coord.Y >= 0 && coord.X < Config.StrategicWorldSizeX && coord.Y < Config.StrategicWorldSizeY;

                if (notSelf && connectivity && withinGrid)
                {
                    neighbours.Add(coord);
                }
            }
        }

        return neighbours;
    }

    private static List<Vec2> FindPath(Vec2 start, Vec2 end, Army army, int mpPerTurn, int maxDistance)
    {
        Vec2Comparer comparer = new Vec2Comparer();
        var dist = new Dictionary<Vec2, int>(comparer);
        var prev = new Dictionary<Vec2, Vec2?>(comparer);

        var q = new SortedSet<QueueNode>();

        for (int x = 0; x < Config.StrategicWorldSizeX; x++)
        {
            for (int y = 0; y < Config.StrategicWorldSizeY; y++)
            {
                var coord = new Vec2(x, y);
                if (CanEnter(coord, army) == false)
                {
                    dist[coord] = int.MaxValue;
                }
            }
        }

        dist[start] = 0;
        prev[start] = null;
        q.Add(new QueueNode(start, 0, 0));

        List<Vec2> neighbours = new List<Vec2>();

        // Search loop
        while (q.Count > 0)
        {
            var u = q.Min;
            q.Remove(q.Min);

            // Old priority queue value
            if (u.Dist != dist[u.Value])
            {
                continue;
            }

            if (u.Value == end)
            {
                break;
            }

            foreach (var v in GetNeighbours(u.Value, neighbours))
            {
                if (CanEnter(v, army))
                {
                    int walkCost = _grid[v.X, v.Y].MovementCost;
                    if (_flyingPath) walkCost = 1;
                    if (dist[u.Value] + walkCost > maxDistance) continue;
                    if (_grid[v.X, v.Y].EnemyOccupied && end != v) //avoid targets you're not looking for
                        walkCost += 6;
                    int remainingMp = Config.ArmyMp - dist[u.Value] % mpPerTurn;
                    if (walkCost > remainingMp) walkCost = walkCost + remainingMp;
                    int alt = dist[u.Value] + walkCost;
                    if (dist.TryGetValue(v, out int distInt) == false || alt < dist[v])
                    {
                        dist[v] = alt;
                        bool diagonal = u.Value.X != v.X && u.Value.Y != v.Y;
                        q.Add(new QueueNode(v, alt, u.Diagonals + (diagonal ? 1 : 0)));

                        prev[v] = u.Value;
                    }
                }
            }
        }

        // Trace path - if there is one
        var path = new List<Vec2>();

        if (prev.ContainsKey(end))
        {
            Vec2? current = end;

            while (current != null)
            {
                path.Add(current.Value);
                if (prev.ContainsKey(current.Value)) current = prev[current.Value];
            }

            path.Reverse();
        }

        return path;
    }

    private static int TurnsToReach(Vec2 start, Vec2 end, Army army, int mpPerTurn)
    {
        Vec2Comparer comparer = new Vec2Comparer();
        var dist = new Dictionary<Vec2, int>(comparer);
        var prev = new Dictionary<Vec2, Vec2?>(comparer);

        var q = new SortedSet<QueueNode>();

        for (int x = 0; x < Config.StrategicWorldSizeX; x++)
        {
            for (int y = 0; y < Config.StrategicWorldSizeY; y++)
            {
                var coord = new Vec2(x, y);
                if (CanEnter(coord, army) == false)
                {
                    dist[coord] = int.MaxValue;
                }
            }
        }

        dist[start] = 0;
        prev[start] = null;
        q.Add(new QueueNode(start, 0, 0));

        List<Vec2> neighbours = new List<Vec2>();

        // Search loop
        while (q.Count > 0)
        {
            var u = q.Min;
            q.Remove(q.Min);

            // Old priority queue value
            if (u.Dist != dist[u.Value])
            {
                continue;
            }

            if (u.Value == end)
            {
                break;
            }

            foreach (var v in GetNeighbours(u.Value, neighbours))
            {
                if (CanEnter(v, army))
                {
                    int walkCost = _grid[v.X, v.Y].MovementCost;
                    if (_flyingPath) walkCost = 1;
                    int remainingMp = mpPerTurn - dist[u.Value] % mpPerTurn;
                    if (walkCost > remainingMp) walkCost = walkCost + remainingMp;
                    int alt = dist[u.Value] + walkCost;
                    if (dist.TryGetValue(v, out int distInt) == false || alt < dist[v])
                    {
                        dist[v] = alt;
                        bool diagonal = u.Value.X != v.X && u.Value.Y != v.Y;
                        q.Add(new QueueNode(v, alt, u.Diagonals + (diagonal ? 1 : 0)));

                        prev[v] = u.Value;
                    }
                }
            }
        }

        if (prev.ContainsKey(end))
        {
            return (dist[end] + 2) / mpPerTurn;
        }

        return 9999;
    }

    private static List<Vec2> FindPathMany(Vec2 start, Vec2[] ends, Army army, int mpPerTurn, int[] priorities, int maxDistance)
    {
        List<EndPoint> endNodes = new List<EndPoint>();
        Vec2Comparer comparer = new Vec2Comparer();
        var dist = new Dictionary<Vec2, int>(comparer);
        var prev = new Dictionary<Vec2, Vec2?>(comparer);

        var q = new SortedSet<QueueNode>();

        for (int x = 0; x < Config.StrategicWorldSizeX; x++)
        {
            for (int y = 0; y < Config.StrategicWorldSizeY; y++)
            {
                var coord = new Vec2(x, y);
                if (CanEnter(coord, army) == false)
                {
                    dist[coord] = int.MaxValue;
                }
            }
        }

        dist[start] = 0;
        prev[start] = null;
        q.Add(new QueueNode(start, 0, 0));

        List<Vec2> neighbours = new List<Vec2>();

        // Search loop
        while (q.Count > 0)
        {
            var u = q.Min;
            q.Remove(q.Min);

            // Old priority queue value
            if (u.Dist != dist[u.Value])
            {
                continue;
            }

            if (ends.Any(s => s == u.Value))
            {
                if (priorities != null)
                    endNodes.Add(new EndPoint(u, dist[u.Value] - priorities[Array.IndexOf(ends, u.Value)]));
                else
                    endNodes.Add(new EndPoint(u, dist[u.Value]));
                if (endNodes.Count > 10) break;
            }

            foreach (var v in GetNeighbours(u.Value, neighbours))
            {
                if (CanEnter(v, army))
                {
                    int walkCost = _grid[v.X, v.Y].MovementCost;
                    if (_flyingPath) walkCost = 1;
                    if (dist[u.Value] + walkCost > maxDistance) continue;
                    if (_grid[v.X, v.Y].EnemyOccupied && ends.Any(s => s == v) == false) //avoid targets you're not looking for
                        walkCost += 6;
                    int remainingMp = Config.ArmyMp - dist[u.Value] % mpPerTurn;
                    if (walkCost > remainingMp) walkCost = walkCost + remainingMp;
                    int alt = dist[u.Value] + walkCost;
                    if (dist.TryGetValue(v, out int distInt) == false || alt < dist[v])
                    {
                        dist[v] = alt;
                        bool diagonal = u.Value.X != v.X && u.Value.Y != v.Y;
                        q.Add(new QueueNode(v, alt, u.Diagonals + (diagonal ? 1 : 0)));

                        prev[v] = u.Value;
                    }
                }
            }
        }

        // Trace path - if there is one
        var path = new List<Vec2>();

        var end = endNodes.OrderBy(s => s.Priority).FirstOrDefault();

        if (end.Node.Dist <= 0) return null;

        Vec2? current = end.Node.Value;
        while (current != null)
        {
            path.Add(current.Value);
            if (prev.ContainsKey(current.Value)) current = prev[current.Value];
        }

        path.Reverse();

        return path;
    }

    internal static bool CanEnter(Vec2 pos, Army army)
    {
        if (State.World.Doodads != null && State.World.Doodads[pos.X, pos.Y] >= StrategicDoodadType.BridgeVertical && State.World.Doodads[pos.X, pos.Y] <= StrategicDoodadType.VirtualBridgeIntersection) return true;
        if (_grid[pos.X, pos.Y].FriendlyOccupied) return false;
        if (army.Impassables.Contains(_grid[pos.X, pos.Y].TileType)) return false;
        return true;
    }


    internal static List<PathNode> GetPath(Empire empire, Army army, Vec2I destination, int startingMp, bool flight, int maxDistance = 999)
    {
        var origin = army.Position;
        //Quick sanity check to make sure that tile is actually passable
        if (TileCheck(empire, destination) == false) return null;
        _flyingPath = flight;
        InitializeGrid();
        NormalOccupancy(empire);

        var otherPath = FindPath(new Vec2(origin.X, origin.Y), new Vec2(destination.X, destination.Y), army, Config.ArmyMp, maxDistance);

        if (otherPath == null) return null;

        List<PathNode> path = new List<PathNode>();
        for (int i = 0; i < otherPath.Count(); i++)
        {
            path.Add(new PathNode(otherPath[i].X, otherPath[i].Y));
        }

        if (path.Count <= 1)
        {
            return null;
        }

        path.RemoveAt(0);

        return path;
    }


    /// <summary>
    ///     Similar to normal path but can't go through towns
    /// </summary>
    /// <returns></returns>
    internal static List<PathNode> GetMonsterPath(Empire empire, Army army, Vec2I destination, int startingMp, bool flight)
    {
        var origin = army.Position;
        //Quick sanity check to make sure that tile is actually passable
        if (TileCheck(empire, destination) == false) return null;
        _flyingPath = flight;
        InitializeGrid();
        MonsterOccupancy(empire);

        var otherPath = FindPath(new Vec2(origin.X, origin.Y), new Vec2(destination.X, destination.Y), army, Config.ArmyMp, 9999);

        if (otherPath == null) return null;

        List<PathNode> path = new List<PathNode>();
        for (int i = 0; i < otherPath.Count(); i++)
        {
            path.Add(new PathNode(otherPath[i].X, otherPath[i].Y));
        }

        if (path.Count > 0)
            path.RemoveAt(0);
        else
            return null;

        return path;
    }


    private static void MonsterOccupancy(Empire empire)
    {
        for (int x = 0; x < Config.StrategicWorldSizeX; x++)
        {
            for (int y = 0; y < Config.StrategicWorldSizeY; y++)
            {
                _grid[x, y].FriendlyOccupied = false;
                _grid[x, y].EnemyOccupied = false;
            }
        }

        foreach (var army in StrategicUtilities.GetAllArmies())
        {
            if (empire != null)
            {
                if (army.EmpireOutside.IsEnemy(empire))
                    _grid[army.Position.X, army.Position.Y].EnemyOccupied = true;
                else
                    _grid[army.Position.X, army.Position.Y].FriendlyOccupied = true;
            }
        }

        foreach (var village in State.World.Villages)
        {
            _grid[village.Position.X, village.Position.Y].FriendlyOccupied = true;
        }
    }

    private static void NormalOccupancy(Empire empire)
    {
        for (int x = 0; x < Config.StrategicWorldSizeX; x++)
        {
            for (int y = 0; y < Config.StrategicWorldSizeY; y++)
            {
                _grid[x, y].FriendlyOccupied = false;
                _grid[x, y].EnemyOccupied = false;
            }
        }

        foreach (var army in StrategicUtilities.GetAllArmies())
        {
            if (empire != null)
            {
                if (army.EmpireOutside.IsEnemy(empire))
                    _grid[army.Position.X, army.Position.Y].EnemyOccupied = true;
                else
                    _grid[army.Position.X, army.Position.Y].FriendlyOccupied = true;
            }
        }

        foreach (var village in State.World.Villages)
        {
            if (empire != null)
            {
                if (village.Empire.IsEnemy(empire))
                    _grid[village.Position.X, village.Position.Y].EnemyOccupied = true;
                else if (village.Empire.IsNeutral(empire)) //If it's neither an enemy or an ally, it should be blocked
                    _grid[village.Position.X, village.Position.Y].FriendlyOccupied = true;
            }
        }
    }

    private static void InitializeGrid()
    {
        if (Initialized == false)
        {
            _grid = new Cell[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];
            for (int x = 0; x < Config.StrategicWorldSizeX; x++)
            {
                for (int y = 0; y < Config.StrategicWorldSizeY; y++)
                {
                    Cell cell = new Cell
                    {
                        WalkableSurface = StrategicTileInfo.CanWalkInto(x, y),
                        MovementCost = StrategicTileInfo.WalkCost(x, y),
                        TileType = State.World.Tiles[x, y]
                    };

                    _grid[x, y] = cell;
                }
            }

            Initialized = true;
        }
    }

    internal static int TurnsToReach(Empire empire, Army army, Vec2I destination, int mpPerTurn, bool flight)
    {
        var origin = army.Position;
        //Quick sanity check to make sure that tile is actually passable
        if (TileCheck(empire, destination) == false) return 9999;
        _flyingPath = flight;
        InitializeGrid();
        NormalOccupancy(empire);

        int turns = TurnsToReach(new Vec2(origin.X, origin.Y), new Vec2(destination.X, destination.Y), army, mpPerTurn);

        return turns;
    }

    internal static List<PathNode> GetPathToClosestObject(Empire empire, Army army, Vec2I[] destinations, int startingMp, int maxDistance, bool flight, int[] priorities = null)
    {
        var origin = army.Position;
        _flyingPath = flight;
        InitializeGrid();
        NormalOccupancy(empire);

        Vec2[] ends = new Vec2[destinations.Length];
        for (int i = 0; i < destinations.Length; i++)
        {
            ends[i].X = destinations[i].X;
            ends[i].Y = destinations[i].Y;
        }

        var otherPath = FindPathMany(new Vec2(origin.X, origin.Y), ends, army, Config.ArmyMp, priorities, maxDistance);

        if (otherPath == null) return null;
        List<PathNode> path = new List<PathNode>();

        for (int i = 0; i < otherPath.Count(); i++)
        {
            path.Add(new PathNode(otherPath[i].X, otherPath[i].Y));
        }

        path.RemoveAt(0);

        return path;
    }

    internal static List<PathNode> GetMonsterPathToClosestObject(Empire empire, Army army, Vec2I[] destinations, int startingMp, int maxDistance, bool flight)
    {
        var origin = army.Position;
        //float time = Time.realtimeSinceStartup;
        _flyingPath = flight;
        InitializeGrid();
        MonsterOccupancy(empire);

        Vec2[] ends = new Vec2[destinations.Length];
        for (int i = 0; i < destinations.Length; i++)
        {
            ends[i].X = destinations[i].X;
            ends[i].Y = destinations[i].Y;
        }

        var otherPath = FindPathMany(new Vec2(origin.X, origin.Y), ends, army, Config.ArmyMp, null, maxDistance);

        if (otherPath == null) return null;
        List<PathNode> path = new List<PathNode>();

        for (int i = 0; i < otherPath.Count(); i++)
        {
            path.Add(new PathNode(otherPath[i].X, otherPath[i].Y));
        }

        //Debug.Log(Time.realtimeSinceStartup - time);

        path.RemoveAt(0);

        return path;
    }


    private static List<PathNode> GetWalkableAdjacentSquares(Empire empire, int x, int y)
    {
        var proposedLocations = new List<PathNode>()
        {
            new PathNode { X = x, Y = y - 1 },
            new PathNode { X = x, Y = y + 1 },
            new PathNode { X = x - 1, Y = y },
            new PathNode { X = x + 1, Y = y },
            new PathNode { X = x + 1, Y = y + 1 },
            new PathNode { X = x + 1, Y = y - 1 },
            new PathNode { X = x - 1, Y = y + 1 },
            new PathNode { X = x - 1, Y = y - 1 },
        };

        return proposedLocations.Where(l => TileCheck(empire, new Vec2I(l.X, l.Y)) == true).ToList();
    }

    private static int ComputeHScore(int x, int y, int targetX, int targetY)
    {
        int dx = Mathf.Abs(x - targetX);
        int dy = Mathf.Abs(y - targetY);
        return Mathf.Max(dx, dy);
    }

    private static int TotalManhattan(int x, int y, int targetX, int targetY)
    {
        int dx = Mathf.Abs(x - targetX);
        int dy = Mathf.Abs(y - targetY);
        return dx + dy;
    }


    public static bool TileCheck(Empire empire, Vec2I p)
    {
        //check tile
        if (p.X < 0 || p.Y < 0 || p.X >= Config.StrategicWorldSizeX || p.Y >= Config.StrategicWorldSizeY) return false;
        if (StrategicTileInfo.CanWalkInto(p.X, p.Y))
        {
            if (empire != null)
            {
                foreach (Army army in StrategicUtilities.GetAllArmies())
                {
                    if (army.EmpireOutside.IsAlly(empire) && army.Position.Matches(p)) return false;
                }
            }

            return true;
        }

        return false;
    }
}