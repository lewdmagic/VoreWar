using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

internal class FogSystem
{
    internal bool[,] FoggedTile;
    internal Tilemap FogOfWar;
    internal TileBase FogTile;

    public FogSystem(Tilemap fogOfWar, TileBase fogTile)
    {
        FogTile = fogTile;
        FoggedTile = new bool[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];
        FogOfWar = fogOfWar;
    }

    internal void UpdateFog(Empire playerEmpire, Village[] villages, Army[] armies, List<GameObject> currentVillageTiles, List<GameObject> currentClaimableTiles)
    {
        FogOfWar.ClearAllTiles();
        if (State.World.Relations == null) return;
        if (State.World.AllActiveEmpires == null) return;
        if (playerEmpire == null) return;
        if (FoggedTile.GetUpperBound(0) + 1 != Config.StrategicWorldSizeX || FoggedTile.GetUpperBound(1) + 1 != Config.StrategicWorldSizeY) FoggedTile = new bool[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];
        //StrategicTileType[,] tiles = State.World.Tiles;
        for (int i = 0; i <= FoggedTile.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= FoggedTile.GetUpperBound(1); j++)
            {
                FoggedTile[i, j] = true;
            }
        }

        foreach (Village village in villages)
        {
            if ((village.Empire.IsAlly(playerEmpire) || (State.World.IsNight && Config.DayNightCosmetic && !Config.FogOfWar)) && village.GetTotalPop() > 0)
            {
                ClearWithinXTilesOf(village.Position);
            }
        }

        foreach (Army army in armies)
        {
            if (army.EmpireOutside.IsAlly(playerEmpire) || (State.World.IsNight && Config.DayNightCosmetic && !Config.FogOfWar))
            {
                ClearWithinXTilesOf(army.Position);
            }
        }

        for (int i = 0; i <= FoggedTile.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= FoggedTile.GetUpperBound(1); j++)
            {
                if (FoggedTile[i, j]) FogOfWar.SetTile(new Vector3Int(i, j, 0), FogTile);
            }
        }

        foreach (Army army in StrategicUtilities.GetAllHostileArmies(playerEmpire))
        {
            var spr = army.Banner?.GetComponent<MultiStageBanner>();
            if (spr != null) spr.gameObject.SetActive(!FoggedTile[army.Position.X, army.Position.Y] && (!army.Units.All(u => u.HasTrait(TraitType.Infiltrator)) || army.Units.Any(u => Equals(u.FixedSide, playerEmpire.Side))));
            var spr2 = army.Sprite;
            if (spr2 != null) spr2.enabled = !FoggedTile[army.Position.X, army.Position.Y] && (!army.Units.All(u => u.HasTrait(TraitType.Infiltrator)) || army.Units.Any(u => Equals(u.FixedSide, playerEmpire.Side)));
        }

        if (currentVillageTiles != null)
        {
            for (int i = 0; i < State.World.Villages.Length; i++)
            {
                if (FoggedTile[State.World.Villages[i].Position.X, State.World.Villages[i].Position.Y])
                {
                    currentVillageTiles[4 * i].GetComponent<SpriteRenderer>().enabled = false;
                    currentVillageTiles[4 * i + 1].GetComponent<SpriteRenderer>().enabled = false;
                    currentVillageTiles[4 * i + 2].GetComponent<SpriteRenderer>().enabled = false;
                    currentVillageTiles[4 * i + 3].GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    currentVillageTiles[4 * i].GetComponent<SpriteRenderer>().enabled = true;
                    currentVillageTiles[4 * i + 1].GetComponent<SpriteRenderer>().enabled = true;
                    currentVillageTiles[4 * i + 2].GetComponent<SpriteRenderer>().enabled = true;
                    currentVillageTiles[4 * i + 3].GetComponent<SpriteRenderer>().enabled = true;
                }
            }

            for (int i = 0; i < State.World.Claimables.Length; i++)
            {
                if (FoggedTile[State.World.Claimables[i].Position.X, State.World.Claimables[i].Position.Y])
                {
                    currentClaimableTiles[4 * i].GetComponent<SpriteRenderer>().enabled = false;
                    currentClaimableTiles[4 * i + 1].GetComponent<SpriteRenderer>().enabled = false;
                    currentClaimableTiles[4 * i + 2].GetComponent<SpriteRenderer>().enabled = false;
                    currentClaimableTiles[4 * i + 3].GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    currentClaimableTiles[4 * i].GetComponent<SpriteRenderer>().enabled = true;
                    currentClaimableTiles[4 * i + 1].GetComponent<SpriteRenderer>().enabled = true;
                    currentClaimableTiles[4 * i + 2].GetComponent<SpriteRenderer>().enabled = true;
                    currentClaimableTiles[4 * i + 3].GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
    }

    private void ClearWithinXTilesOf(Vec2I pos)
    {
        int dist = Config.FogDistance - ((State.World.IsNight && Config.FogOfWar) ? Config.NightStrategicSightReduction : 0);
        for (int x = pos.X - dist; x <= pos.X + dist; x++)
        {
            for (int y = pos.Y - dist; y <= pos.Y + dist; y++)
            {
                if (x < 0 || y < 0 || x > FoggedTile.GetUpperBound(0) || y > FoggedTile.GetUpperBound(1)) continue;
                FoggedTile[x, y] = false;
            }
        }
    }
}