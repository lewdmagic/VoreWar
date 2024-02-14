internal enum Neighbor
{
    North,
    NorthEast,
    East,
    SouthEast,
    South,
    SouthWest,
    West,
    NorthWest
}

internal enum Wanted
{
    Yes,
    No,
    DontCare
}

internal struct DirectionalInfo
{
    internal Wanted North;
    internal Wanted Northeast;
    internal Wanted East;
    internal Wanted Southeast;
    internal Wanted South;
    internal Wanted Southwest;
    internal Wanted West;
    internal Wanted Northwest;

    public DirectionalInfo(Wanted north = Wanted.DontCare, Wanted northeast = Wanted.DontCare, Wanted east = Wanted.DontCare, Wanted southeast = Wanted.DontCare, Wanted south = Wanted.DontCare, Wanted southwest = Wanted.DontCare, Wanted west = Wanted.DontCare, Wanted northwest = Wanted.DontCare)
    {
        this.North = north;
        this.Northeast = northeast;
        this.East = east;
        this.Southeast = southeast;
        this.South = south;
        this.Southwest = southwest;
        this.West = west;
        this.Northwest = northwest;
    }
}

internal class TacticalTileLogic
{
    private TacticalTileType[,] _tiles;
    private TacticalTileType[,] _newtiles;

    internal TacticalTileType[,] ApplyLogic(TacticalTileType[,] tiles)
    {
        this._tiles = tiles;
        _newtiles = new TacticalTileType[tiles.GetLength(0), tiles.GetLength(1)];
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                switch (tiles[x, y])
                {
                    case TacticalTileType.RockOverSand:
                    {
                        int type = DetermineType(new Vec2(x, y), TacticalTileType.RockOverSand);
                        _newtiles[x, y] = (TacticalTileType)(2000 + type);
                        break;
                    }
                    case TacticalTileType.RockOverTar:
                    {
                        int type = DetermineType(new Vec2(x, y), TacticalTileType.RockOverTar);
                        _newtiles[x, y] = (TacticalTileType)(2100 + type);
                        break;
                    }
                    case TacticalTileType.GrassOverWater:
                    {
                        int type = DetermineType(new Vec2(x, y), TacticalTileType.GrassOverWater);
                        _newtiles[x, y] = (TacticalTileType)(2200 + type);
                        break;
                    }
                    case TacticalTileType.VolcanicOverGravel:
                    {
                        int type = DetermineType(new Vec2(x, y), TacticalTileType.VolcanicOverGravel);
                        _newtiles[x, y] = (TacticalTileType)(2300 + type);
                        break;
                    }
                    case TacticalTileType.VolcanicOverLava:
                    {
                        int type = DetermineType(new Vec2(x, y), TacticalTileType.VolcanicOverLava);
                        _newtiles[x, y] = (TacticalTileType)(2400 + type);
                        break;
                    }
                    default:
                        _newtiles[x, y] = tiles[x, y];
                        continue;
                }
            }
        }

        return _newtiles;
    }

    internal int DetermineType(Vec2 pos, TacticalTileType type)
    {
        Wanted yes = Wanted.Yes;
        Wanted no = Wanted.No;
        Wanted any = Wanted.DontCare;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, yes, yes, any, no, any))) return 0;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, yes, yes, yes, yes, any))) return 1;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, yes, yes, yes, any))) return 2;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, any, no, any, no, any))) return 3;
        if (AreaCheck(pos, type, new DirectionalInfo(north: no, west: yes, south: no, east: yes))) return 4;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, no, any, yes, any))) return 5;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, yes, yes, yes))) return 6;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, no, yes, yes))) return 7;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, any, no, any))) return 8;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, yes, yes, yes))) return 9;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, yes, yes, yes))) return 10;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, no, yes, no))) return 11;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, yes, any, no, any))) return 12;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, no, any, no, any))) return 13;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, yes, yes, yes))) return 14;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, yes, yes, no))) return 15;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, any, no, any, no, any))) return 16;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, any, no, any, yes, yes))) return 17;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, no, any, yes, yes))) return 18;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, no, any, no, any))) return 19;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, any, no, any))) return 20;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, no, yes, any, no, any))) return 21;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, yes, no, yes, any))) return 22;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, no, yes, yes))) return 23;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, no, yes, yes, yes, any))) return 24;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, yes, yes, no, yes, any))) return 25;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, any, no, any))) return 26;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, no, yes, yes))) return 27;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, no, any, no, any))) return 28;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, any, no, any, no, any))) return 29;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, no, any, yes, no))) return 30;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, yes, yes, no))) return 31;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, any, no, any, yes, yes))) return 32;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, any, no, any, yes, no))) return 33;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, any, no, any))) return 34;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, yes, yes, no))) return 35;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, any, no, any))) return 36;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, no, yes, no, yes, any))) return 37;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, any, no, any, yes, no))) return 38;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, no, yes, no))) return 39;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, no, yes, no))) return 40;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, yes, yes, yes))) return 41;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, no, yes, yes))) return 42;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, yes, yes, no))) return 43;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, no, yes, no))) return 44;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, no, yes, yes))) return 45;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, no, yes, no))) return 46;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, yes, yes, no))) return 47;
        UnityEngine.Debug.Log("fallthrough");
        return 9;
    }


    internal bool AreaCheck(Vec2 pos, TacticalTileType type, DirectionalInfo info)
    {
        if (Fails(info.North, Neighbor.North)) return false;
        if (Fails(info.Northeast, Neighbor.NorthEast)) return false;
        if (Fails(info.East, Neighbor.East)) return false;
        if (Fails(info.Southeast, Neighbor.SouthEast)) return false;
        if (Fails(info.South, Neighbor.South)) return false;
        if (Fails(info.Southwest, Neighbor.SouthWest)) return false;
        if (Fails(info.West, Neighbor.West)) return false;
        if (Fails(info.Northwest, Neighbor.NorthWest)) return false;

        return true;

        bool Fails(Wanted direction, Neighbor neighbor)
        {
            if (direction == Wanted.DontCare) return false;
            if (direction == Wanted.Yes && IsTileType(GetPos(pos, neighbor), type) == false) return true;
            if (direction == Wanted.No && IsTileType(GetPos(pos, neighbor), type)) return true;
            return false;
        }
    }

    internal Vec2 GetPos(Vec2 startingPos, Neighbor direction)
    {
        switch (direction)
        {
            case Neighbor.North:
                return new Vec2(startingPos.X, startingPos.Y + 1);
            case Neighbor.NorthEast:
                return new Vec2(startingPos.X + 1, startingPos.Y + 1);
            case Neighbor.East:
                return new Vec2(startingPos.X + 1, startingPos.Y);
            case Neighbor.SouthEast:
                return new Vec2(startingPos.X + 1, startingPos.Y - 1);
            case Neighbor.South:
                return new Vec2(startingPos.X, startingPos.Y - 1);
            case Neighbor.SouthWest:
                return new Vec2(startingPos.X - 1, startingPos.Y - 1);
            case Neighbor.West:
                return new Vec2(startingPos.X - 1, startingPos.Y);
            case Neighbor.NorthWest:
                return new Vec2(startingPos.X - 1, startingPos.Y + 1);
            default:
                return startingPos;
        }
    }

    internal bool IsTileType(Vec2 pos, TacticalTileType type)
    {
        if (pos.X < 0 || pos.Y < 0 || pos.X > _tiles.GetUpperBound(0) || pos.Y > _tiles.GetUpperBound(1)) return false;
        return _tiles[pos.X, pos.Y] == type;
    }
}

internal class StrategicTileLogic
{
    private StrategicTileType[,] _tiles;
    private StrategicTileType[,] _newtiles;

    internal StrategicTileType[,] ApplyLogic(StrategicTileType[,] originalTiles, out StrategicTileType[,] overTiles, out StrategicTileType[,] underTiles)
    {
        _tiles = new StrategicTileType[originalTiles.GetLength(0), originalTiles.GetLength(1)];
        StrategicTileType[,] temptiles = new StrategicTileType[originalTiles.GetLength(0), originalTiles.GetLength(1)];
        underTiles = new StrategicTileType[_tiles.GetLength(0), _tiles.GetLength(1)];
        for (int x = 0; x < originalTiles.GetLength(0); x++)
        {
            for (int y = 0; y < originalTiles.GetLength(1); y++)
            {
                temptiles[x, y] = originalTiles[x, y];
                _tiles[x, y] = originalTiles[x, y];
                underTiles[x, y] = (StrategicTileType)99;
            }
        }

        _newtiles = new StrategicTileType[_tiles.GetLength(0), _tiles.GetLength(1)];
        overTiles = new StrategicTileType[_tiles.GetLength(0), _tiles.GetLength(1)];

        for (int x = 0; x < _tiles.GetLength(0); x++)
        {
            for (int y = 0; y < _tiles.GetLength(1); y++)
            {
                if (_tiles[x, y] == StrategicTileType.Water)
                {
                    int type = DetermineType(new Vec2(x, y), StrategicTileType.Water);
                    overTiles[x, y] = (StrategicTileType)(2000 + type);
                    if (type == 19) overTiles[x, y] = StrategicTileType.Water;

                    int sand = DirectBorderTiles(x, y, StrategicTileType.Desert);
                    int snow = DirectBorderTiles(x, y, StrategicTileType.Snow);
                    int grass = DirectBorderTiles(x, y, StrategicTileType.Grass);
                    if (grass >= sand && grass >= snow)
                        temptiles[x, y] = StrategicTileType.Grass;
                    else if (sand >= grass && sand >= snow)
                        temptiles[x, y] = StrategicTileType.Desert;
                    else if (snow >= sand && snow >= grass)
                        temptiles[x, y] = StrategicTileType.Snow;
                    else
                        temptiles[x, y] = StrategicTileType.Grass;


                    continue;
                }
            }
        }

        _tiles = temptiles;

        for (int x = 0; x < _tiles.GetLength(0); x++)
        {
            for (int y = 0; y < _tiles.GetLength(1); y++)
            {
                if (overTiles[x, y] != 0)
                {
                    underTiles[x, y] = _tiles[x, y];
                    int type = DetermineType(new Vec2(x, y), _tiles[x, y]);
                    _newtiles[x, y] = (StrategicTileType)(2100 + type);
                    //newtiles[x, y] = tiles[x, y];
                    continue;
                }
                else if (Config.HardLava)
                {
                    if (_tiles[x, y] == StrategicTileType.Lava) overTiles[x, y] = StrategicTileType.Lava;
                    if (_tiles[x, y] == StrategicTileType.Volcanic) overTiles[x, y] = StrategicTileType.Volcanic;
                }

                switch (_tiles[x, y])
                {
                    case StrategicTileType.Ice:
                    {
                        int type = DetermineType(new Vec2(x, y), StrategicTileType.Ice);
                        underTiles[x, y] = (StrategicTileType)(2200 + type);
                        //underTiles[x, y] = StrategicTileType.ice;
                        type = DetermineType(new Vec2(x, y), StrategicTileType.Snow);
                        _newtiles[x, y] = (StrategicTileType)(2100 + type);
                        break;
                    }
                    default:
                    {
                        int type = DetermineType(new Vec2(x, y), _tiles[x, y]);
                        _newtiles[x, y] = (StrategicTileType)(2100 + type);
                        break;
                    }

                    //newtiles[x, y] = tiles[x, y];
                    //continue;
                }
            }
        }

        return _newtiles;

        int DirectBorderTiles(int x, int y, StrategicTileType type)
        {
            int count = 0;
            count += IsTileType(new Vec2(x, y + 1), type) ? 3 : 0;
            count += IsTileType(new Vec2(x, y - 1), type) ? 3 : 0;
            count += IsTileType(new Vec2(x - 1, y), type) ? 3 : 0;
            count += IsTileType(new Vec2(x + 1, y), type) ? 3 : 0;
            count += IsTileType(new Vec2(x + 1, y + 1), type) ? 1 : 0;
            count += IsTileType(new Vec2(x + 1, y - 1), type) ? 1 : 0;
            count += IsTileType(new Vec2(x - 1, y + 1), type) ? 1 : 0;
            count += IsTileType(new Vec2(x - 1, y - 1), type) ? 1 : 0;
            return count;
        }
    }

    internal int DetermineType(Vec2 pos, StrategicTileType type, bool inverted = false)
    {
        Wanted yes;
        Wanted no;
        if (inverted)
        {
            yes = Wanted.No;
            no = Wanted.Yes;
        }
        else
        {
            yes = Wanted.Yes;
            no = Wanted.No;
        }


        Wanted any = Wanted.DontCare;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, yes, yes, any, no, any))) return 0;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, yes, yes, yes, yes, any))) return 1;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, yes, yes, yes, any))) return 2;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, any, no, any, no, any))) return 3;
        if (AreaCheck(pos, type, new DirectionalInfo(north: no, west: yes, south: no, east: yes))) return 4;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, no, any, yes, any))) return 5;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, yes, yes, yes))) return 6;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, no, yes, yes))) return 7;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, any, no, any))) return 8;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, yes, yes, yes))) return 9;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, yes, yes, yes))) return 10;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, no, yes, no))) return 11;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, yes, any, no, any))) return 12;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, no, any, no, any))) return 13;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, yes, yes, yes))) return 14;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, yes, yes, no))) return 15;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, any, no, any, no, any))) return 16;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, any, no, any, yes, yes))) return 17;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, no, any, yes, yes))) return 18;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, no, any, no, any))) return 19;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, any, no, any))) return 20;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, no, yes, any, no, any))) return 21;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, yes, no, yes, any))) return 22;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, no, yes, yes))) return 23;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, no, yes, yes, yes, any))) return 24;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, yes, yes, no, yes, any))) return 25;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, any, no, any))) return 26;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, no, yes, yes))) return 27;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, no, any, no, any))) return 28;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, any, no, any, no, any))) return 29;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, no, any, yes, no))) return 30;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, yes, yes, no))) return 31;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, any, no, any, yes, yes))) return 32;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, any, no, any, yes, no))) return 33;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, any, no, any))) return 34;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, yes, yes, no))) return 35;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, any, no, any))) return 36;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, no, yes, no, yes, any))) return 37;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, any, no, any, yes, no))) return 38;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, no, yes, no))) return 39;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, no, yes, no))) return 40;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, yes, yes, yes))) return 41;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, no, yes, yes))) return 42;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, yes, yes, no))) return 43;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, no, yes, no))) return 44;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, no, yes, yes))) return 45;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, no, yes, no))) return 46;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, yes, yes, no))) return 47;
        UnityEngine.Debug.Log("fallthrough");
        return 9;
    }


    internal bool AreaCheck(Vec2 pos, StrategicTileType type, DirectionalInfo info)
    {
        if (Fails(info.North, Neighbor.North)) return false;
        if (Fails(info.Northeast, Neighbor.NorthEast)) return false;
        if (Fails(info.East, Neighbor.East)) return false;
        if (Fails(info.Southeast, Neighbor.SouthEast)) return false;
        if (Fails(info.South, Neighbor.South)) return false;
        if (Fails(info.Southwest, Neighbor.SouthWest)) return false;
        if (Fails(info.West, Neighbor.West)) return false;
        if (Fails(info.Northwest, Neighbor.NorthWest)) return false;

        return true;

        bool Fails(Wanted direction, Neighbor neighbor)
        {
            if (direction == Wanted.DontCare) return false;
            if (direction == Wanted.Yes && IsTileType(GetPos(pos, neighbor), type) == false) return true;
            if (direction == Wanted.No && IsTileType(GetPos(pos, neighbor), type)) return true;
            return false;
        }
    }

    internal Vec2 GetPos(Vec2 startingPos, Neighbor direction)
    {
        switch (direction)
        {
            case Neighbor.North:
                return new Vec2(startingPos.X, startingPos.Y + 1);
            case Neighbor.NorthEast:
                return new Vec2(startingPos.X + 1, startingPos.Y + 1);
            case Neighbor.East:
                return new Vec2(startingPos.X + 1, startingPos.Y);
            case Neighbor.SouthEast:
                return new Vec2(startingPos.X + 1, startingPos.Y - 1);
            case Neighbor.South:
                return new Vec2(startingPos.X, startingPos.Y - 1);
            case Neighbor.SouthWest:
                return new Vec2(startingPos.X - 1, startingPos.Y - 1);
            case Neighbor.West:
                return new Vec2(startingPos.X - 1, startingPos.Y);
            case Neighbor.NorthWest:
                return new Vec2(startingPos.X - 1, startingPos.Y + 1);
            default:
                return startingPos;
        }
    }

    internal bool IsTileType(Vec2 pos, StrategicTileType type)
    {
        if (pos.X < 0 || pos.Y < 0 || pos.X > _tiles.GetUpperBound(0) || pos.Y > _tiles.GetUpperBound(1)) return true;
        if (type == StrategicTileType.Ice) return _tiles[pos.X, pos.Y] == type;
        if (StrategicTileInfo.SandFamily.Contains(type))
        {
            return StrategicTileInfo.SandFamily.Contains(_tiles[pos.X, pos.Y]);
        }

        if (StrategicTileInfo.GrassFamily.Contains(type))
        {
            return StrategicTileInfo.GrassFamily.Contains(_tiles[pos.X, pos.Y]);
        }

        if (StrategicTileInfo.SnowFamily.Contains(type))
        {
            return StrategicTileInfo.SnowFamily.Contains(_tiles[pos.X, pos.Y]);
        }

        //if (StrategicTileInfo.WaterFamily.Contains(type))
        //{
        //    return StrategicTileInfo.WaterFamily.Contains(tiles[pos.x, pos.y]);
        //}
        return _tiles[pos.X, pos.Y] == type;
    }
}