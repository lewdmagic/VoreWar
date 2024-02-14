﻿using OdinSerializer;
using System;

namespace TacticalBuildings
{
    internal abstract class TacticalBuilding
    {
        [OdinSerialize]
        internal Vec2 _lowerLeftPosition;

        internal Vec2 LowerLeftPosition { get => _lowerLeftPosition; set => _lowerLeftPosition = value; }

        [OdinSerialize]
        private int _width;

        /// <summary>
        ///     The width of blocked tiles
        /// </summary>
        internal int Width { get => _width; set => _width = value; }

        [OdinSerialize]
        private int _height;

        /// <summary>
        ///     The height of blocked tiles
        /// </summary>
        internal int Height { get => _height; set => _height = value; }

        [OdinSerialize]
        private int[,] _tile;

        /// <summary>
        ///     Graphical tiles.  Height, then width
        /// </summary>
        internal int[,] Tile { get => _tile; set => _tile = value; }

        [OdinSerialize]
        internal int[,] FrontColoredTile;

        public TacticalBuilding(Vec2 lowerLeftPosition)
        {
            LowerLeftPosition = lowerLeftPosition;
            FrontColoredTile = new int[0, 0];
        }
    }

    internal class LogCabin : TacticalBuilding
    {
        public LogCabin(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 2;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[3, 2]
            {
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.LogCabins[8 + State.Rand.Next(3)]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.LogCabins[11 + State.Rand.Next(3)])
                },
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.LogCabins[2 + State.Rand.Next(3)]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.LogCabins[5 + State.Rand.Next(3)])
                },
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.LogCabins[0]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.LogCabins[1])
                }
            };
        }
    }

    internal class Log1X2 : TacticalBuilding
    {
        public Log1X2(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 1;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 2]
            {
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.WoodenBuildings[2 + State.Rand.Next(3)]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.WoodenBuildings[5 + State.Rand.Next(3)])
                },
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.WoodenBuildings[0]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.WoodenBuildings[1])
                },
            };
        }
    }

    internal class Log1X1 : TacticalBuilding
    {
        public Log1X1(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 1;
            Height = 1;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 1]
            {
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.WoodenBuildings[9]),
                },
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.WoodenBuildings[8]),
                },
            };
        }
    }

    internal class Well : TacticalBuilding
    {
        public Well(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 1;
            Height = 1;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[1, 1]
            {
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.WoodenBuildings[10]),
                },
            };
        }
    }

    internal class HarpyNest : TacticalBuilding
    {
        public HarpyNest(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 2;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 2]
            {
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.HarpyNests[2]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.HarpyNests[3])
                },
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.HarpyNests[0]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.HarpyNests[1])
                },
            };
        }
    }

    internal class HarpyNestCanopy : TacticalBuilding
    {
        public HarpyNestCanopy(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 2;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 2]
            {
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.HarpyNests[2]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.HarpyNests[3])
                },
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.HarpyNests[4]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.HarpyNests[5])
                },
            };
            FrontColoredTile = new int[2, 2]
            {
                {
                    -1, -1
                },
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.HarpyNests[6]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.HarpyNests[7])
                },
            };
        }
    }

    internal class CatHouse : TacticalBuilding
    {
        public CatHouse(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 1;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 2]
            {
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[2]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[3])
                },
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[0]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[1])
                },
            };
        }
    }

    internal class StoneHouse : TacticalBuilding
    {
        public StoneHouse(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 2;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 2]
            {
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[6]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[7])
                },
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[4]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[5])
                },
            };
        }
    }

    internal class LamiaTemple : TacticalBuilding
    {
        public LamiaTemple(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 1;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 2]
            {
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.LamiaTemple[2]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.LamiaTemple[3])
                },
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.LamiaTemple[0]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.LamiaTemple[1])
                },
            };
        }
    }

    internal class CobbleStoneHouse : TacticalBuilding
    {
        public CobbleStoneHouse(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 1;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 2]
            {
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[10]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[11])
                },
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[8]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[9])
                },
            };
        }
    }

    internal class YellowCobbleStoneHouse : TacticalBuilding
    {
        public YellowCobbleStoneHouse(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 1;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 2]
            {
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[12]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[13])
                },
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[8]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[9])
                },
            };
        }
    }

    internal class Barrels : TacticalBuilding
    {
        public Barrels(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 1;
            Height = 1;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[1, 1]
            {
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.WoodenBuildings[11]),
                },
            };
        }
    }

    internal class LogPile : TacticalBuilding
    {
        public LogPile(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 1;
            Height = 1;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[1, 1]
            {
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.WoodenBuildings[12]),
                },
            };
        }
    }

    internal class FancyStoneHouse : TacticalBuilding
    {
        public FancyStoneHouse(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 2;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 2]
            {
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[16]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[17])
                },
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[14]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[15])
                },
            };
        }
    }

    internal class FoxStoneHouse : TacticalBuilding
    {
        public FoxStoneHouse(Vec2 lowerLeftPosition) : base(lowerLeftPosition)
        {
            Width = 2;
            Height = 2;
            var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
            Tile = new int[2, 2]
            {
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[20]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[21])
                },
                {
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[18]),
                    Array.IndexOf(allSprites, State.GameManager.TacticalBuildingSpriteDictionary.Buildings[19])
                },
            };
        }
    }
}