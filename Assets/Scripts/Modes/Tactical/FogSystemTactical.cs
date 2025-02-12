﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

internal class FogSystemTactical
{
    internal bool[,] FoggedTile;
    internal Tilemap FogOfWar;
    internal TileBase FogTile;

    public FogSystemTactical(Tilemap fogOfWar, TileBase fogTile)
    {
        FogTile = fogTile;
        FoggedTile = new bool[Config.TacticalSizeX, Config.TacticalSizeY];
        FogOfWar = fogOfWar;
    }

    internal void UpdateFog(List<ActorUnit> all, Side defenderSide, bool attackersturn, bool aiAttacker, bool aiDefender, int currentturn)
    {
        //FogOfWar.ClearAllTiles();
        if (FoggedTile.GetUpperBound(0) + 1 != Config.TacticalSizeX || FoggedTile.GetUpperBound(1) + 1 != Config.TacticalSizeY) FoggedTile = new bool[Config.TacticalSizeX, Config.TacticalSizeY];
        //StrategicTileType[,] tiles = State.World.Tiles;
        for (int i = 0; i <= FoggedTile.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= FoggedTile.GetUpperBound(1); j++)
            {
                if (FoggedTile[i, j] == false)
                {
                    FoggedTile[i, j] = true;
                }
            }
        }

        //Set all as unseen
        foreach (ActorUnit unit in all)
        {
            unit.InSight = true;
            int unitSightRange = Config.DefualtTacticalSightRange + unit.Unit.TraitBoosts.SightRangeBoost;
            if (Config.RevealTurn > currentturn) //Keeps all units revealed after the set ammount of turns have passed or if in turbo mode.
            {
                if (unit.PredatorComponent.PreyCount <= 0 && !Config.DayNightCosmetic)
                {
                    unit.InSight = false;
                }
            }

            if (unit.Targetable)
            {
                if ((aiAttacker && aiDefender) || Config.DayNightCosmetic == true)
                {
                    ClearWithinSTilesOf(unit.Position, unitSightRange); // Shows all units to player for AI only battles
                }

                foreach (var seenUnit in TacticalUtilities.UnitsWithinTiles(unit.Position, unitSightRange).Where(s => TacticalUtilities.TreatAsHostile(unit, s)))
                {
                    seenUnit.InSight = true;
                }

                if (Equals(unit.Unit.GetApparentSide(), defenderSide) && ((attackersturn != true && State.GameManager.TacticalMode.IsPlayerTurn) || !aiDefender))
                {
                    ClearWithinSTilesOf(unit.Position, unitSightRange);
                }

                if (!Equals(unit.Unit.GetApparentSide(), defenderSide) && ((attackersturn == true && State.GameManager.TacticalMode.IsPlayerTurn) || !aiAttacker))
                {
                    ClearWithinSTilesOf(unit.Position, unitSightRange);
                    unit.InSight = true;
                }
            }
        }

        // Removes fog from tile if it's not suppose to be there.
        for (int i = 0; i <= FoggedTile.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= FoggedTile.GetUpperBound(1); j++)
            {
                if (FoggedTile[i, j])
                    FogOfWar.SetTile(new Vector3Int(i, j, 0), FogTile);
                else
                    FogOfWar.SetTile(new Vector3Int(i, j, 0), null);
            }

            if (all != null)
            {
                if (all != null)
                {
                    foreach (ActorUnit unit in all)
                    {
                        if (FoggedTile[unit.Position.X, unit.Position.Y] && unit.PredatorComponent.PreyCount == 0)
                        {
                            unit.UnitSprite.gameObject.SetActive(false);
                            unit.UnitSprite.FlexibleSquare.gameObject.SetActive(false);
                            unit.Hidden = true;
                        }
                        else if (unit.Targetable == true)
                        {
                            unit.UnitSprite.gameObject.SetActive(true);
                            unit.UnitSprite.FlexibleSquare.gameObject.SetActive(true);
                            unit.Hidden = false;
                        }
                    }
                }
            }
        }
    }

    private void ClearWithinSTilesOf(Vec2I pos, int sight = 1)
    {
        for (int x = pos.X - sight; x <= pos.X + sight; x++)
        {
            for (int y = pos.Y - sight; y <= pos.Y + sight; y++)
            {
                if (x < 0 || y < 0 || x > FoggedTile.GetUpperBound(0) || y > FoggedTile.GetUpperBound(1)) continue;
                FoggedTile[x, y] = false;
            }
        }
    }
}