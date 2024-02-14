using UnityEngine;

namespace Assets.Scripts.Modes.Strategic
{
    internal class StrategicTerrainGenerator
    {
        private WorldGenerator.MapGenArgs _genArgs;

        private int _xSize = Config.StrategicWorldSizeX;
        private int _ySize = Config.StrategicWorldSizeY;
        private float _humidityPlus = 0.0f;
        private float _mountainThreshold = 0.7f;

        //this is used to sharpen mountains and flatten valleys. 
        //Ideally the curve should be 0y in 0x and 1y in 1x, and below 0.5 for most of its length
        private AnimationCurve _heightMultiplier = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(.2f, .2f), new Keyframe(.8f, .5f), new Keyframe(1, 1) });

        private float _heZoom = 10f;
        private float _heFactor = 2f; //1.8 to 4 look good
        private Vector2 _heSeed;

        private float _huZoom = 10f;
        private float _huFactor = 1.8f; //1.8 to 10 look good
        private Vector2 _huSeed;

        private float _tmpZoom = 10f;
        private float _tmpFactor = 1.8f;
        private Vector2 _tmpSeed;

        private float[,] _heArray;
        private float[,] _huArray;
        private float[,] _teArray;

        public StrategicTerrainGenerator(WorldGenerator.MapGenArgs genArgs)
        {
            _genArgs = genArgs;
        }

        internal StrategicTileType[,] GenerateTerrain()
        {
            _heSeed = new Vector2(Random.Range(0, 200), Random.Range(0, 200));
            _huSeed = new Vector2(Random.Range(0, 200), Random.Range(0, 200));
            _tmpSeed = new Vector2(Random.Range(0, 200), Random.Range(0, 200));

            StrategicTileType[,] tiles = new StrategicTileType[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];
            MakeArrays();
            for (int x = 0; x < Config.StrategicWorldSizeX; x++)
            {
                for (int y = 0; y < Config.StrategicWorldSizeY; y++)
                {
                    tiles[x, y] = GetTerrain(x, y);
                }
            }

            return tiles;
        }

        private StrategicTileType GetTerrain(int x, int y)
        {
            float height = _heArray[x, y];
            float humidity = _huArray[x, y];
            float temperature = _teArray[x, y];
            temperature += _genArgs.Temperature;
            if (_genArgs.Poles)
            {
                float position = (float)y / Config.StrategicWorldSizeY;
                if (position < .4f)
                {
                    temperature = (temperature + .5f) / 2;
                    temperature += (.4f - position) / 2;
                }
                else if (position > .6f)
                {
                    temperature = (temperature + .5f) / 2;
                    temperature += (.6f - position) / 1.3333f;
                }
                else
                    temperature = (temperature + .5f) / 2;
            }

            if (height < _genArgs.WaterPct && temperature < .3f) return StrategicTileType.Ice;
            if (height < _genArgs.WaterPct) return StrategicTileType.Water;
            if (temperature > .6f && height < .55f - _genArgs.Hilliness / 5) return StrategicTileType.Desert;
            if (temperature > .6f && height > .55f - _genArgs.Hilliness / 5 + Random.Range(0f, 1 - _mountainThreshold)) return StrategicTileType.BrokenCliffs;
            if (temperature > .6f) return StrategicTileType.SandHills;
            if (temperature < .3f && height < .55f - _genArgs.Hilliness / 5 && humidity < .5f + _genArgs.ForestPct / 4 && humidity > .5f - _genArgs.ForestPct / 4) return StrategicTileType.SnowTrees;
            if (temperature < .3f && height < .55f - _genArgs.Hilliness / 5) return StrategicTileType.Snow;
            if (temperature < .3f && height > .55f - _genArgs.Hilliness / 5 + Random.Range(0f, 1 - _mountainThreshold)) return StrategicTileType.SnowMountain;
            if (temperature < .3f) return StrategicTileType.SnowHills;
            if (height > .55f - _genArgs.Hilliness / 5 + Random.Range(0f, 1 - _mountainThreshold)) return StrategicTileType.Mountain;
            if (height > .55f - _genArgs.Hilliness / 5) return StrategicTileType.Hills;
            if (humidity > .75f - _genArgs.Swampiness / 4) return StrategicTileType.Swamp;
            if (temperature < .55f && temperature > .4f && humidity < .5f + _genArgs.ForestPct / 4 && humidity > .5f - _genArgs.ForestPct / 4)
                return StrategicTileType.Forest;
            else
                return StrategicTileType.Grass;
        }


        //calculate the value of an element of the array based on noise and location
        private float FractalNoise(int i, int j, float zoom, float factor, Vector2 seed)
        {
            i = i + Mathf.RoundToInt(seed.x * zoom);
            j = j + Mathf.RoundToInt(seed.y * zoom);
            //fractal behavior occurs here. Everything else is parameter fine-tuning
            return 0
                   + Mathf.PerlinNoise(i / zoom, j / zoom) / 3
                   + Mathf.PerlinNoise(i / (zoom / factor), j / (zoom / factor)) / 3
                   + Mathf.PerlinNoise(i / (zoom / factor * factor), j / (zoom / factor * factor)) / 3;
        }

        //float FractalNoiseRidges(int i, int j, float zoom, float factor, Vector2 seed)
        //{
        //    i = i + Mathf.RoundToInt(seed.x * zoom);
        //    j = j + Mathf.RoundToInt(seed.y * zoom);
        //    //fractal behavior occurs here. Everything else is parameter fine-tuning
        //    float sum = 0
        //    + Mathf.PerlinNoise(i / zoom, j / zoom) / 3
        //    + Mathf.PerlinNoise(i / (zoom / factor), j / (zoom / factor)) / 3
        //    + Mathf.PerlinNoise(i / (zoom / factor * factor), j / (zoom / factor * factor)) / 3;

        //    return 1 - Mathf.Abs(sum - 0.5f) * 2f;
        //}

        //float FractalNoiseMounds(int i, int j, float zoom, float factor, Vector2 seed)
        //{
        //    i = i + Mathf.RoundToInt(seed.x * zoom);
        //    j = j + Mathf.RoundToInt(seed.y * zoom);
        //    //fractal behavior occurs here. Everything else is parameter fine-tuning
        //    float sum = 0
        //    + Mathf.PerlinNoise(i / zoom, j / zoom) / 3
        //    + Mathf.PerlinNoise(i / (zoom / factor), j / (zoom / factor)) / 3
        //    + Mathf.PerlinNoise(i / (zoom / factor * factor), j / (zoom / factor * factor)) / 3;

        //    return Mathf.Abs(sum - 0.5f) * 2f;
        //}


        //makes and calculates the values of the 3 arrays that are used to determine the type of tile
        private void MakeArrays()
        {
            _heArray = new float[_xSize, _ySize];
            _huArray = new float[_xSize, _ySize];
            _teArray = new float[_xSize, _ySize];

            RecalculateArray();
        }

        private void RecalculateArray()
        {
            for (int i = 0; i < _xSize; i++)
            {
                for (int j = 0; j < _ySize; j++)
                {
                    _heArray[i, j] = _heightMultiplier.Evaluate(FractalNoise(i, j, _heZoom, _heFactor, _heSeed));
                    _huArray[i, j] = FractalNoise(i, j, _huZoom, _huFactor, _huSeed) + _humidityPlus;
                    _teArray[i, j] = FractalNoise(i, j, _tmpZoom, _tmpFactor, _tmpSeed);
                }
            }
        }
    }
}