using OdinSerializer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class StrategyMode : SceneBase
{
    public Translator Translator;

    private bool _armyBarUp;

    private bool ArmyBarUp
    {
        get { return _armyBarUp; }
        set
        {
            _armyBarUp = value;
            ArmyStatusUI.gameObject.SetActive(value);
        }
    }

    private bool _subWindowUp = false;

    private Empire ActingEmpire { get { return State.World.ActingEmpire; } set { State.World.ActingEmpire = value; } }

    private Empire _renamingEmpire;

    private Army SelectedArmy
    {
        get { return _selectedArmy; }
        set
        {
            _selectedArmy = value;
            if (State.World != null)
            {
                _arrowManager?.ClearNodes();
                _mouseMovementMode = false;
                if (State.World.MainEmpires != null) UpdateArmyLocationsAndSprites();
                ArmyBarUp = value != null;
                if (ArmyBarUp) RegenArmyBar(_selectedArmy);
            }
        }
    }

    private Army _selectedArmy;
    private float _mTimer;

    private int _scaledExp;

    public int ScaledExp
    {
        get
        {
            if (_scaledExp == 0) _scaledExp = StrategicUtilities.Get80ThExperiencePercentile();
            return _scaledExp;
        }
        set { _scaledExp = value; }
    }

    internal bool IsPlayerTurn => ActingEmpire.StrategicAI == null;

    internal bool OnlyAIPlayers { get; private set; }

    private Empire _lastHumanTeam = null;

    public Empire LastHumanEmpire
    {
        get
        {
            if (ActingEmpire?.StrategicAI == null) _lastHumanTeam = ActingEmpire;
            return _lastHumanTeam;
        }
        set => _lastHumanTeam = value;
    }

    internal List<PathNode> QueuedPath
    {
        get => _queuedPath;
        set
        {
            if (value == null) _queuedAttackPermission = false;
            _queuedPath = value;
        }
    }

    public bool NewReports { get; internal set; }

    private List<GameObject> _shownIff;

    private bool _runningQueued = true;

    private bool _mouseMovementMode = false;
    private PathNodeManager _arrowManager;
    private List<PathNode> _queuedPath;
    private bool _queuedAttackPermission;
    private Vec2I _currentPathDestination;

    public StrategicTileDictionary TileDictionary;

    internal FogSystem FogSystem;

    private List<GameObject> _currentVillageTiles;
    private List<GameObject> _currentClaimableTiles;

    public Tilemap[] TilemapLayers;
    public Tilemap FogOfWar;
    public TileBase FogTile;
    internal Tile[] TileTypes;
    public TileBase[] DoodadTypes;
    public Sprite[] Sprites;
    public Sprite[] VillageSprites;
    public GameObject[] SpriteCategories;

    private GameObject SelectionBox => SpriteCategories[0];
    private GameObject GenericBanner => SpriteCategories[1];
    private GameObject GenericVillage => SpriteCategories[2];
    private GameObject MultiStageBanner => SpriteCategories[3];

    public Sprite[] Banners;

    public Transform VillageFolder;
    public Transform ArmyFolder;

    public ArmyStatusPanel ArmyStatusUI;
    public StatusBarPanel StatusBarUI;
    public DevourPanel DevourUI;
    public TurnReportPanel ReportUI;
    public TrainPanel TrainUI;
    public GameObject EnemyTurnText;
    public GameObject PausedText;

    public EventScreen EventUI;

    public float NightChance = Config.BaseNightChance;

    public GameObject NotificationWindow;
    public TextMeshProUGUI NotificationText;
    private float _remainingNotificationTime;

    public GameObject ExchangeBlockerPanels;

    public ArmyExchanger ExchangerUI;

    public SimpleTextPanel ArmyTooltip;
    public SimpleTextPanel VillageTooltip;

    public Button UndoButton;

    internal List<MonsterSpawnerLocation> Spawners;

    internal List<StrategicMoveUndo> UndoMoves = new List<StrategicMoveUndo>();

    private int[] _trainCost;
    private int[] _trainExp;

    private bool _pickingExchangeLocation = false;

    internal bool Paused;

    public Sprite[] TileSprites;

    public DevourSelectPanel RaceUI;

    private void Start()
    {
        Translator = new Translator();
        _arrowManager = FindObjectOfType<PathNodeManager>();

        _shownIff = new List<GameObject>();

        TileTypes = new Tile[TileSprites.Count()];
        for (int i = 0; i < TileSprites.Count(); i++)
        {
            TileTypes[i] = ScriptableObject.CreateInstance<Tile>();
            TileTypes[i].sprite = TileSprites[i];
        }

        State.EventList.SetUI(EventUI);
    }

    public void Setup()
    {
        ActingEmpire = null;
        OnlyAIPlayers = true;
        State.GameManager.CenterCameraOnTile((int)(Config.StrategicWorldSizeX * .5f), (int)(Config.StrategicWorldSizeY * .5f));
        State.GameManager.CameraController.SetZoom(Config.TacticalSizeX * .5f);
        foreach (var village in State.World.Villages)
        {
            village.UpdateNetBoosts();
        }

        foreach (Empire emp in State.World.MainEmpires)
        {
            if (State.World.Villages.Where(s => Equals(s.Side, emp.Side)).Any() == false)
            {
                emp.KnockedOut = true;
                continue;
            }

            if (emp.StrategicAI == null)
            {
                OnlyAIPlayers = false;
            }

            emp.Regenerate();
        }

        State.World.PopulateMonsterTurnOrders();
        State.World.RefreshTurnOrder();
        for (int i = 0; i < State.World.EmpireOrder.Count; i++)
        {
            if (State.World.EmpireOrder[i].KnockedOut == false || (Equals(State.World.EmpireOrder[i].Side, Side.BanditSide) && State.World.EmpireOrder[i].Armies.Any()))
            {
                ActingEmpire = State.World.EmpireOrder[i];
                break;
            }
        }

        GenericSetup();
        StatusBarUI.RecreateWorld.gameObject.SetActive(true);
    }

    /// <summary>
    ///     Designed to be used through NotificationSystem, but can be used manually
    /// </summary>
    /// <param name="message"></param>
    /// <param name="time"></param>
    internal void ShowNotification(string message, float time)
    {
        if (Config.Notifications == false) return;
        NotificationWindow.SetActive(true);
        _remainingNotificationTime = time;
        NotificationText.text = message;
    }

    public void GenericSetup()
    {
        LastHumanEmpire = State.World.MainEmpires.Where(s => s.StrategicAI == null).FirstOrDefault();
        RedrawTiles();
        RebuildSpawners();
        ResetButtons();
    }


    public void ClearData()
    {
        StrategyPathfinder.Initialized = false;
        if (State.World != null) ActingEmpire = null;
        _selectedArmy = null;
        _subWindowUp = false;
        _arrowManager?.ClearNodes();
        _mouseMovementMode = false;
        ClearArmies();
        ClearVillages();
        foreach (Tilemap tilemap in TilemapLayers)
        {
            tilemap.ClearAllTiles();
        }
    }

    public void ClearGraphics()
    {
        ClearArmies();
        ClearVillages();
        foreach (Tilemap tilemap in TilemapLayers)
        {
            tilemap.ClearAllTiles();
        }
    }

    private void RecreateObjects()
    {
        RedrawTiles();
        RedrawVillages();
        RedrawArmies();
    }

    public void CheckIfOnlyAIPlayers()
    {
        OnlyAIPlayers = true;
        foreach (Empire emp in State.World.MainEmpires)
        {
            if (emp.StrategicAI == null)
            {
                OnlyAIPlayers = false;
            }
        }
    }

    private void PromptArmyPick()
    {
        ExchangeBlockerPanels.SetActive(true);
        _pickingExchangeLocation = true;
    }


    private void OpenExchangerPanel(Army left, Vec2I location)
    {
        if (left == null) return;
        if (left.Position.GetNumberOfMovesDistance(location) != 1) return;
        if (left.RemainingMp < 1)
        {
            State.GameManager.CreateMessageBox("Army needs to have at least 1 MP to exchange units");
            return;
        }

        Village village = StrategicUtilities.GetVillageAt(location);
        if (village != null && village.Empire.IsEnemy(left.EmpireOutside))
        {
            if (!left.Units.All(u => u.HasTrait(TraitType.Infiltrator)))
            {
                State.GameManager.CreateMessageBox("Can't split armies onto an enemy village");
                return;
            }
        }

        Army right = StrategicUtilities.ArmyAt(location);
        if (right == null)
        {
            if (ActingEmpire.Armies.Count() >= Config.MaxArmies)
            {
                State.GameManager.CreateMessageBox("You already have the maximum amount of armies");
                return;
            }

            right = new Army(ActingEmpire, new Vec2I(location.X, location.Y), left.Side)
            {
                RemainingMp = left.RemainingMp - 1
            };
            State.World.GetEmpireOfSide(left.Side).Armies.Add(right);
        }
        else
        {
            if (right.EmpireOutside.IsAlly(left.EmpireOutside) == false)
            {
                State.GameManager.CreateMessageBox("You can't exchange units with a hostile army");
                return;
            }

            if (right.RemainingMp < 1 && Equals(right.Side, left.Side))
            {
                State.GameManager.CreateMessageBox("Recieving Army needs to have at least 1 MP to exchange units (0 MP for Allied armies)");
                return;
            }
        }

        _subWindowUp = true;
        ExchangeBlockerPanels.SetActive(false);
        _pickingExchangeLocation = false;
        ExchangerUI.gameObject.SetActive(true);
        ExchangerUI.Initialize(left, right);
    }


    private void ResetButtons()
    {
        StatusBarUI.EndTurn.interactable = ActingEmpire?.StrategicAI == null;
        StatusBarUI.EmpireStatus.interactable = ActingEmpire?.StrategicAI == null || OnlyAIPlayers;
        StatusBarUI.ShowTurnReport.gameObject.SetActive(ActingEmpire?.StrategicAI == null && ActingEmpire.Reports.Count > 0);
        StatusBarUI.RecreateWorld.gameObject.SetActive(false);
        EnemyTurnText.SetActive(ActingEmpire?.StrategicAI != null);
    }

    public void RedrawArmies()
    {
        ClearArmies();
        var armiesToReassign = new List<Army>();
        foreach (Empire empire in State.World.AllActiveEmpires)
        {
            foreach (Army army in empire.Armies)
            {
                if (army.Units.Any() && !army.Units.Any(unit => Equals(unit.GetApparentSide(), army.Side)))
                {
                    armiesToReassign.Add(army);
                }

                if (RaceFuncs.IsPlayableRace(army.Side.ToRace()))
                {
                    army.Banner = Instantiate(MultiStageBanner, new Vector3(army.Position.X, army.Position.Y), new Quaternion(), ArmyFolder).GetComponent<MultiStageBanner>();
                    army.Banner.Refresh(army, army == SelectedArmy);
                }
                else
                {
                    int tileType = empire.BannerType;
                    if (army.Units.Contains(empire.Leader)) tileType += 4;
                    if (SelectedArmy == army) tileType += 1;
                    army.Sprite = Instantiate(GenericBanner, new Vector3(army.Position.X, army.Position.Y), new Quaternion(), ArmyFolder).GetComponent<SpriteRenderer>();
                    army.Sprite.sprite = Sprites[tileType];
                    army.Sprite.color = empire.UnityColor;
                }
            }
        }

        foreach (Army army in armiesToReassign)
        {
            ReassignArmyEmpire(army);
        }

        UpdateFog();
    }

    private void ClearArmies()
    {
        var previousTiles = GameObject.FindGameObjectsWithTag("Army");
        foreach (var tile in previousTiles.ToList())
        {
            Destroy(tile);
        }
    }

    internal void RebuildSpawners()
    {
        Spawners = new List<MonsterSpawnerLocation>();
        StrategicDoodadType[,] doodads = State.World.Doodads;
        if (doodads != null)
        {
            for (int i = 0; i <= doodads.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= doodads.GetUpperBound(1); j++)
                {
                    if (doodads[i, j] >= StrategicDoodadType.SpawnerVagrant)

                    {
                        StrategicDoodadType doodad = doodads[i, j];
                        switch (doodad)
                        {
                            case StrategicDoodadType.SpawnerVagrant:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Vagrant));
                                break;
                            case StrategicDoodadType.SpawnerSerpents:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Serpent));
                                break;
                            case StrategicDoodadType.SpawnerWyvern:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Wyvern));
                                break;
                            case StrategicDoodadType.SpawnerCompy:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Compy));
                                break;
                            case StrategicDoodadType.SpawnerSharks:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.FeralShark));
                                break;
                            case StrategicDoodadType.SpawnerFeralWolves:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.FeralWolve));
                                break;
                            case StrategicDoodadType.SpawnerCake:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Cake));
                                break;
                            case StrategicDoodadType.SpawnerHarvester:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Harvester));
                                break;
                            case StrategicDoodadType.SpawnerVoilin:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Voilin));
                                break;
                            case StrategicDoodadType.SpawnerBats:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.FeralBat));
                                break;
                            case StrategicDoodadType.SpawnerFrogs:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.FeralFrog));
                                break;
                            case StrategicDoodadType.SpawnerDragon:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Dragon));
                                break;
                            case StrategicDoodadType.SpawnerDragonfly:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Dragonfly));
                                break;
                            case StrategicDoodadType.SpawnerTwistedVines:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.TwistedVine));
                                break;
                            case StrategicDoodadType.SpawnerFairy:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Fairy));
                                break;
                            case StrategicDoodadType.SpawnerAnts:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.FeralAnt));
                                break;
                            case StrategicDoodadType.SpawnerGryphon:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Gryphon));
                                break;
                            case StrategicDoodadType.SpawnerSlugs:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.RockSlug));
                                break;
                            case StrategicDoodadType.SpawnerSalamanders:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Salamander));
                                break;
                            case StrategicDoodadType.SpawnerMantis:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Manti));
                                break;
                            case StrategicDoodadType.SpawnerEasternDragon:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.EasternDragon));
                                break;
                            case StrategicDoodadType.SpawnerCatfish:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Catfish));
                                break;
                            case StrategicDoodadType.SpawnerGazelle:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Gazelle));
                                break;
                            case StrategicDoodadType.SpawnerEarthworm:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Earthworms));
                                break;
                            case StrategicDoodadType.SpawnerFeralLizards:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.FeralLizard));
                                break;
                            case StrategicDoodadType.SpawnerMonitor:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Monitor));
                                break;
                            case StrategicDoodadType.SpawnerSchiwardez:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Schiwardez));
                                break;
                            case StrategicDoodadType.SpawnerTerrorbird:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Terrorbird));
                                break;
                            case StrategicDoodadType.SpawnerDratopyr:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Dratopyr));
                                break;
                            case StrategicDoodadType.SpawnerFeralLions:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.FeralLion));
                                break;
                            case StrategicDoodadType.SpawnerGoodra:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2I(i, j), Race.Goodra));
                                break;
                        }
                    }
                }
            }
        }
    }


    public void UpdateArmyLocationsAndSprites()
    {
        var armiesToReassign = new List<Army>();
        foreach (Empire empire in State.World.MainEmpires)
        {
            foreach (Army army in empire.Armies)
            {
                if (army.Banner == null && army.Sprite == null)
                {
                    RedrawArmies();
                    return;
                }

                if (army.Banner != null) army.Banner.Refresh(army, army == SelectedArmy);
            }
        }

        UpdateFog();
    }

    private static readonly int TheFreeTeam = 2500;
    private static readonly int UnboundTeam = 3000;

    internal void ReassignArmyEmpire(Army army)
    {
        var sidesRepresented = new Dictionary<Side, int>();
        army.Units.ForEach(unit =>
        {
            if (sidesRepresented.ContainsKey(unit.FixedSide))
            {
                sidesRepresented[unit.FixedSide]++;
            }
            else
            {
                sidesRepresented.Add(unit.FixedSide, 1);
            }
        });

        var finalSide = sidesRepresented.OrderByDescending(s => s.Value).First().Key;
        var pos = army.Position;
        Vec2 loc = pos;
        if (StrategicUtilities.GetVillageAt(pos) != null)
        {
            var distance = 1;
            loc = new Vec2(0, 0);
            CheckTile(pos + new Vec2(-distance, 0));
            CheckTile(pos + new Vec2(0, distance));
            CheckTile(pos + new Vec2(distance, 0));
            CheckTile(pos + new Vec2(-distance, -distance));
            CheckTile(pos + new Vec2(-distance, distance));
            CheckTile(pos + new Vec2(0, -distance));
            CheckTile(pos + new Vec2(distance, -distance));
            CheckTile(pos + new Vec2(distance, distance));

            if (loc.X == 0 && loc.Y == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    loc.X = State.Rand.Next(Config.StrategicWorldSizeX);
                    loc.Y = State.Rand.Next(Config.StrategicWorldSizeY);

                    if (StrategicUtilities.IsTileClear(loc)) break;
                }

                Debug.Log("Could not place army");
                return;
            }

            void CheckTile(Vec2 spot)
            {
                if (StrategicUtilities.IsTileClear(spot) == false) return;
                if (loc.X == 0 && loc.Y == 0)
                    loc = spot;
                else if (State.Rand.Next(4) == 0) loc = spot;
            }
        }

        pos = new Vec2I(loc.X, loc.Y);
        var emp = State.World.GetEmpireOfSide(finalSide);
        if (emp != null)
        {
            var newArmy = new Army(emp, pos, emp.Side);
            newArmy.Units = army.Units;
            emp.Armies.Add(newArmy);
            newArmy.Units.ForEach(u => u.Side = newArmy.Side);
        }
        else // we'll literally make up an empire on the spot. Should rarely happen
        {
            var monsterEmp = State.World.MonsterEmpires.Where(e => Equals(e.Race, army.Units.Where(u => Equals(u.FixedSide, finalSide)).FirstOrDefault()?.Race)).FirstOrDefault();
            if (monsterEmp != null)
            {
                Empire brandNewEmp = new MonsterEmpire(new Empire.ConstructionArgs(emp.Race, finalSide, Color.white, Color.white, monsterEmp.BannerType, StrategyAIType.Monster, TacticalAIType.Full, UnboundTeam, monsterEmp.MaxArmySize, 0));
                brandNewEmp.ReplacedRace = monsterEmp.Race;
                brandNewEmp.TurnOrder = 1234;
                brandNewEmp.Name = "Unbound " + monsterEmp.Name;
                var newArmy = new Army(brandNewEmp, pos, brandNewEmp.Side);
                newArmy.Units = army.Units;
                brandNewEmp.Armies.Add(newArmy);
                State.World.AllActiveEmpiresWritable.Add(brandNewEmp);
                State.World.RefreshTurnOrder();
                Config.World.SpawnerInfo[finalSide.ToRace()] = new SpawnerInfo(true, 1, 0, 0.4f, brandNewEmp.Team, 0, false, 9999, 1, monsterEmp.MaxArmySize, brandNewEmp.TurnOrder);
            }
            else
            {
                Empire brandNewEmp = new Empire(new Empire.ConstructionArgs(emp.Race, finalSide, UnityEngine.Random.ColorHSV(), UnityEngine.Random.ColorHSV(), 5, StrategyAIType.Advanced, TacticalAIType.Full, TheFreeTeam, State.World.MainEmpires[0].MaxArmySize, 0));
                brandNewEmp.ReplacedRace = army.Units.Where(u => Equals(u.FixedSide, finalSide)).First().Race;
                brandNewEmp.TurnOrder = 1432;
                brandNewEmp.Name = "The Free";
                var newArmy = new Army(brandNewEmp, pos, brandNewEmp.Side);
                newArmy.Units = army.Units;
                brandNewEmp.Armies.Add(newArmy);
                State.World.AllActiveEmpiresWritable.Add(brandNewEmp);
                State.World.MainEmpiresWritable.Add(brandNewEmp);
                State.World.RefreshTurnOrder();
            }
        }

        army.EmpireOutside.Armies.Remove(army);
        RedrawArmies();
    }

    private void UpdateFog()
    {
        if (Config.FogOfWar == false && State.World.IsNight == false)
        {
            if (FogOfWar.gameObject.activeSelf)
            {
                FogOfWar.ClearAllTiles();
                FogOfWar.gameObject.SetActive(false);
            }

            UpdateVisibility();
            return;
        }

        FogOfWar.gameObject.SetActive(true);

        if (FogSystem == null) FogSystem = new FogSystem(FogOfWar, FogTile);
        if (OnlyAIPlayers)
        {
            Config.World.Toggles["FogOfWar"] = false;
            return;
        }

        FogSystem.UpdateFog(LastHumanEmpire, State.World.Villages, StrategicUtilities.GetAllArmies(), _currentVillageTiles, _currentClaimableTiles);
    }

    private void UpdateVisibility()
    {
        if (OnlyAIPlayers) return;
        if (LastHumanEmpire == null) return; // Sometimes when loading, the above may not be enough
        foreach (Army army in StrategicUtilities.GetAllHostileArmies(LastHumanEmpire))
        {
            var spr = army.Banner?.GetComponent<MultiStageBanner>();
            if (spr != null) spr.gameObject.SetActive(Equals(army.Side, LastHumanEmpire.Side) || !army.Units.All(u => u.HasTrait(TraitType.Infiltrator)) || (StrategicUtilities.GetAllHumanSides().Count() > 1 ? army.Units.Any(u => Equals(u.FixedSide, ActingEmpire.Side)) : army.Units.Any(u => Equals(u.FixedSide, LastHumanEmpire.Side))));
            var spr2 = army.Sprite;
            if (spr2 != null) spr2.enabled = Equals(army.Side, LastHumanEmpire.Side) || !army.Units.All(u => u.HasTrait(TraitType.Infiltrator)) || (StrategicUtilities.GetAllHumanSides().Count() > 1 ? army.Units.Any(u => Equals(u.FixedSide, ActingEmpire.Side)) : army.Units.Any(u => Equals(u.FixedSide, LastHumanEmpire.Side)));
        }
    }

    public void RedrawTiles()
    {
        foreach (Tilemap tilemap in TilemapLayers)
        {
            tilemap.ClearAllTiles();
        }

        StrategicTileLogic logic = new StrategicTileLogic();
        StrategicTileType[,] tiles = logic.ApplyLogic(State.World.Tiles, out var overTiles, out var underTiles);
        for (int i = 0; i <= tiles.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= tiles.GetUpperBound(1); j++)
            {
                //if (overTiles[i, j] >= (StrategicTileType)2300)
                //{
                //    TilemapLayers[2].SetTile(new Vector3Int(i, j, 0), TileDictionary.DeepWaterOverWater[(int)overTiles[i, j] - 2300]);
                //}
                if (overTiles[i, j] >= (StrategicTileType)2000)
                {
                    TilemapLayers[2].SetTile(new Vector3Int(i, j, 0), TileDictionary.WaterFloat[(int)overTiles[i, j] - 2000]);
                }
                else if (overTiles[i, j] != 0)
                {
                    TilemapLayers[2].SetTile(new Vector3Int(i, j, 0), TileTypes[StrategicTileInfo.GetTileType(overTiles[i, j], i, j)]);
                }
                else
                {
                    var type = StrategicTileInfo.GetObjectTileType(State.World.Tiles[i, j], i, j);
                    if (type != -1) TilemapLayers[2].SetTile(new Vector3Int(i, j, 0), TileDictionary.Objects[type]);
                }

                if (tiles[i, j] >= (StrategicTileType)2100 && underTiles[i, j] >= (StrategicTileType)2200)
                {
                    TilemapLayers[1].SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassFloat[(int)tiles[i, j] - 2100]);
                    TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileDictionary.IceOverSnow[(int)underTiles[i, j] - 2200]);
                    //TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[(int)underTiles[i, j]]);
                }
                else if (tiles[i, j] >= (StrategicTileType)2100)
                {
                    TilemapLayers[1].SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassFloat[(int)tiles[i, j] - 2100]);
                    if (underTiles[i, j] != (StrategicTileType)99)
                    {
                        TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[(int)underTiles[i, j]]);
                    }
                    else
                    {
                        switch (State.World.Tiles[i, j])
                        {
                            case StrategicTileType.Field:
                                TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[(int)StrategicTileType.Grass]);
                                break;
                            case StrategicTileType.FieldDesert:
                                TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[(int)StrategicTileType.Desert]);
                                break;
                            case StrategicTileType.FieldSnow:
                                TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[(int)StrategicTileType.Snow]);
                                break;
                            default:
                                TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[StrategicTileInfo.GetTileType(State.World.Tiles[i, j], i, j)]);
                                break;
                        }
                    }

                    //TilemapLayers[1].SetTile(new Vector3Int(i, j, 0), TileDictionary.IceOverSnow[(int)tiles[i, j] - 2100]);
                }
                //else if (tiles[i, j] >= (StrategicTileType)2000)
                //{
                //    TilemapLayers[2].SetTile(new Vector3Int(i, j, 0), TileDictionary.WaterFloat[(int)tiles[i, j] - 2000]);
                //    //TilemapLayers[1].SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassFloat[(int)tiles[i, j] - 2000]);
                //    TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[(int)underTiles[i,j]]);
                //}
                else
                {
                    //TilemapLayers[1].SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassFloat[(int)tiles[i, j] - 2100]);
                    TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[StrategicTileInfo.GetTileType(tiles[i, j], i, j)]);
                }
            }
        }

        StrategicDoodadType[,] doodads = State.World.Doodads;
        if (doodads != null)
        {
            for (int i = 0; i <= doodads.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= doodads.GetUpperBound(1); j++)
                {
                    if (doodads[i, j] > 0 && doodads[i, j] < StrategicDoodadType.SpawnerVagrant) TilemapLayers[3].SetTile(new Vector3Int(i, j, 0), DoodadTypes[-1 + (int)doodads[i, j]]);
                }
            }
        }
    }

    public void RedrawVillages()
    {
        if (State.World == null) return;
        ClearVillages();
        Village[] villages = State.World.Villages;
        _currentVillageTiles = new List<GameObject>();
        _currentClaimableTiles = new List<GameObject>();
        for (int i = 0; i < villages.Length; i++)
        {
            Village village = villages[i];
            if (village == null) continue;

            ///////////////////
            GameObject vill = Instantiate(GenericVillage, new Vector3(village.Position.X, village.Position.Y), new Quaternion(), VillageFolder);
            vill.GetComponent<SpriteRenderer>().sprite = village.GetIconSprite();
            vill.GetComponent<SpriteRenderer>().sortingOrder = 1;
            _currentVillageTiles.Add(vill);

            /////////
            Sprite villageColorSprite = village.GetColoredIcon();
            if (villageColorSprite != null)
            {
                GameObject villColored = Instantiate(GenericVillage, new Vector3(village.Position.X, village.Position.Y), new Quaternion(), VillageFolder);
                villColored.GetComponent<SpriteRenderer>().sprite = villageColorSprite;
                villColored.GetComponent<SpriteRenderer>().color = State.World.GetEmpireOfSide(village.Side).UnityColor;
                _currentVillageTiles.Add(villColored);
                //if (villageColorSprite == 0) villColored.GetComponent<SpriteRenderer>().color = Color.clear;
            }

            /////////
            GameObject villShield = Instantiate(GenericVillage, new Vector3(village.Position.X, village.Position.Y), new Quaternion(), VillageFolder);
            villShield.GetComponent<SpriteRenderer>().sprite = Sprites[11];
            villShield.GetComponent<SpriteRenderer>().sortingOrder = 2;
            villShield.GetComponent<SpriteRenderer>().color = State.World.GetEmpireOfSide(village.Side).UnitySecondaryColor;
            _currentVillageTiles.Add(villShield);

            ///////////
            GameObject villShieldInner = Instantiate(GenericVillage, new Vector3(village.Position.X, village.Position.Y), new Quaternion(), VillageFolder);
            villShieldInner.GetComponent<SpriteRenderer>().sprite = Sprites[10];
            villShieldInner.GetComponent<SpriteRenderer>().sortingOrder = 2;
            villShieldInner.GetComponent<SpriteRenderer>().color = State.World.GetEmpireOfSide(village.Side).UnityColor;
            _currentVillageTiles.Add(villShieldInner);
        }

        foreach (var mercHouse in State.World.MercenaryHouses)
        {
            GameObject merc = Instantiate(GenericVillage, new Vector3(mercHouse.Position.X, mercHouse.Position.Y), new Quaternion(), VillageFolder);
            merc.GetComponent<SpriteRenderer>().sprite = Sprites[14];
            merc.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }

        foreach (var claimable in State.World.Claimables)
        {
            int spr = 0;
            if (claimable is GoldMine) spr = 12;
            GameObject vill = Instantiate(GenericVillage, new Vector3(claimable.Position.X, claimable.Position.Y), new Quaternion(), VillageFolder);
            vill.GetComponent<SpriteRenderer>().sprite = Sprites[spr];
            vill.GetComponent<SpriteRenderer>().sortingOrder = 1;
            GameObject villColored = Instantiate(GenericVillage, new Vector3(claimable.Position.X, claimable.Position.Y), new Quaternion(), VillageFolder);
            villColored.GetComponent<SpriteRenderer>().sprite = Sprites[spr + 1];
            villColored.GetComponent<SpriteRenderer>().color = claimable.Owner?.UnityColor ?? Color.clear;
            GameObject villShield = Instantiate(GenericVillage, new Vector3(claimable.Position.X, claimable.Position.Y), new Quaternion(), VillageFolder);
            villShield.GetComponent<SpriteRenderer>().sprite = Sprites[11];
            villShield.GetComponent<SpriteRenderer>().sortingOrder = 2;
            villShield.GetComponent<SpriteRenderer>().color = claimable.Owner?.UnityColor ?? Color.clear;
            GameObject villShieldInner = Instantiate(GenericVillage, new Vector3(claimable.Position.X, claimable.Position.Y), new Quaternion(), VillageFolder);
            villShieldInner.GetComponent<SpriteRenderer>().sprite = Sprites[10];
            villShieldInner.GetComponent<SpriteRenderer>().sortingOrder = 2;
            villShieldInner.GetComponent<SpriteRenderer>().color = claimable.Owner?.UnityColor ?? Color.clear;
            _currentClaimableTiles.Add(vill);
            _currentClaimableTiles.Add(villColored);
            _currentClaimableTiles.Add(villShield);
            _currentClaimableTiles.Add(villShieldInner);
        }


        if (Config.FogOfWar || State.World.IsNight)
            UpdateFog();
        else
            FogOfWar.ClearAllTiles();
    }

    private void ClearVillages()
    {
        _currentVillageTiles = null;
        var previousTiles = GameObject.FindGameObjectsWithTag("Village");
        foreach (var tile in previousTiles.ToList())
        {
            Destroy(tile);
        }
    }

    public void Regenerate(bool initialLoad = false)
    {
        if (LastHumanEmpire == null)
        {
            LastHumanEmpire = State.World.MainEmpires.Where(s => s.StrategicAI == null).FirstOrDefault();
        }

        if (!initialLoad)
        {
            foreach (Empire empire in State.World.AllActiveEmpires)
            {
                empire.ArmyCleanup();
            }
        }

        ActingEmpire.CalcIncome(State.World.Villages);
        if (ActingEmpire.StrategicAI == null || OnlyAIPlayers || Config.CheatExtraStrategicInfo || State.World.MainEmpires.Where(s => s.IsAlly(ActingEmpire) && s.StrategicAI == null).Any())
        {
            StatusBarUI.Side.text = ActingEmpire.Name;
            StatusBarUI.Gold.text = "Gold:" + ActingEmpire.Gold;
            StatusBarUI.Income.text = "Income:" + ActingEmpire.Income;
        }
        else
        {
            StatusBarUI.Side.text = ActingEmpire.Name;
            StatusBarUI.Gold.text = "Gold: hidden";
            StatusBarUI.Income.text = "Income: hidden";
        }

        if (SelectedArmy == null || SelectedArmy.Units.Count == 0) ArmyBarUp = false;
        if (SelectedArmy != null) SelectedArmy.RefreshMovementMode();
        RegenArmyBar(SelectedArmy);
        RedrawArmies();
        RedrawVillages();

        if (NewReports)
        {
            ShowTurnReport();
            NewReports = false;
        }

        if (ActingEmpire.StrategicAI == null) ActingEmpire.CheckAutoLevel();
    }

    private void RegenArmyBar(Army army)
    {
        if (army == null || SelectedArmy.Units.Any() == false) return;
        if (ArmyBarUp == false)
        {
            ArmyBarUp = true;
        }

        ArmyStatusUI.Soldiers.text = army.Units.Count + " soldiers";
        ArmyStatusUI.Health.text = army.GetHealthPercentage() + "% health";
        ArmyStatusUI.Mp.text = army.RemainingMp + " MP";
        if (StrategicUtilities.GetMercenaryHouseAt(SelectedArmy.Position) != null)
            ArmyStatusUI.ArmyStatus.text = "Mercenaries";
        else
            ArmyStatusUI.ArmyStatus.text = "Army Info";
        if (army.InVillageIndex > -1 && army.RemainingMp > 0)
        {
            var village = StrategicUtilities.GetVillageAt(SelectedArmy.Position);
            var setActive = village != null && village.NetBoosts.MaximumTrainingLevelAdd > 0;

            ArmyStatusUI.Train.gameObject.SetActive(setActive);
            ArmyStatusUI.Devour.gameObject.SetActive(army.Units.Where(s => s.Predator).Any());
        }
        else
        {
            ArmyStatusUI.Train.gameObject.SetActive(false);
            ArmyStatusUI.Devour.gameObject.SetActive(false);
        }
    }

    public void AttemptUndo()
    {
        if (UndoMoves.Any())
        {
            UndoMoves[UndoMoves.Count - 1].Undo();
            UndoMoves.RemoveAt(UndoMoves.Count - 1);
        }
    }

    private void NextArmy()
    {
        int startingIndex = ActingEmpire.Armies.IndexOf(SelectedArmy);
        int currentIndex = startingIndex + 1;
        bool found = false;
        for (int i = 0; i < ActingEmpire.Armies.Count; i++)
        {
            if (currentIndex >= ActingEmpire.Armies.Count) currentIndex -= ActingEmpire.Armies.Count;
            if (ActingEmpire.Armies[currentIndex].RemainingMp > 0)
            {
                found = true;
                SelectedArmy = ActingEmpire.Armies[currentIndex];
                State.GameManager.CenterCameraOnTile(SelectedArmy.Position.X, SelectedArmy.Position.Y);
                break;
            }

            currentIndex++;
        }

        if (found == false) SelectedArmy = null;
    }

    private void CheckForMovementInput()
    {
        if (SelectedArmy.RemainingMp > 0)
        {
            if (Input.GetButtonDown("Move Southwest"))
            {
                Move(SelectedArmy, 5);
            }

            if (Input.GetButtonDown("Move South"))
            {
                Move(SelectedArmy, 4);
            }

            if (Input.GetButtonDown("Move Southeast"))
            {
                Move(SelectedArmy, 3);
            }

            if (Input.GetButtonDown("Move East"))
            {
                Move(SelectedArmy, 2);
            }

            if (Input.GetButtonDown("Move Northeast"))
            {
                Move(SelectedArmy, 1);
            }

            if (Input.GetButtonDown("Move North"))
            {
                Move(SelectedArmy, 0);
            }

            if (Input.GetButtonDown("Move Northwest"))
            {
                Move(SelectedArmy, 7);
            }

            if (Input.GetButtonDown("Move West"))
            {
                Move(SelectedArmy, 6);
            }
        }

        RegenArmyBar(SelectedArmy);
    }

    private void Move(Army army, int x)
    {
        if (!army.Move(x))
            RegenArmyBar(army);
        else
            StrategicUtilities.StartBattle(army);
    }

    private void MoveTo(Army army, Vec2I pos)
    {
        if (!army.MoveTo(pos))
            RegenArmyBar(army);
        else
            StrategicUtilities.StartBattle(army);
    }

    private void AI(float dt)
    {
        if (State.GameManager.QueuedTactical) return;
        if (_mTimer > 0)
        {
            _mTimer -= dt;
        }
        else
        {
            //do AI processing
            if (ActingEmpire.StrategicAI.RunAI() == false)
            {
                EndTurn();
            }

            _mTimer = Config.StrategicAIMoveDelay;
        }
    }


    public void ButtonCallback(int id)
    {
        if (ActingEmpire.StrategicAI == null && State.GameManager.StrategicControlsLocked == false)
        {
            if (!_subWindowUp)
            {
                switch (id)
                {
                    case 10:
                        if (_runningQueued || QueuedPath != null) return;
                        EndTurn();
                        break;
                    case 11:
                        ArmyInfoSetup();
                        break;
                    case 12:
                        SetupDevour();
                        _subWindowUp = true;
                        DevourUI.gameObject.SetActive(true);
                        break;
                    case 15:
                        SetupTrain();
                        _subWindowUp = true;
                        TrainUI.gameObject.SetActive(true);
                        break;
                    case 20:
                        PromptArmyPick();
                        break;
                }
            }
            else
            {
                switch (id)
                {
                    case 13:
                        int numToEat = 0;
                        if (int.TryParse(DevourUI.NumberToDevour.text, out numToEat) == false)
                        {
                            State.GameManager.CreateMessageBox("Please enter a number for the amount of units to devour, or cancel");
                            return;
                        }

                        Devour(SelectedArmy, numToEat);
                        _subWindowUp = false;
                        DevourUI.gameObject.SetActive(false);
                        break;
                    case 14:
                        DevourUI.gameObject.SetActive(false);
                        _subWindowUp = false;
                        break;
                    case 16:
                        TrainSelectedArmy();
                        _subWindowUp = false;
                        TrainUI.gameObject.SetActive(false);
                        break;
                    case 17:
                        TrainUI.gameObject.SetActive(false);
                        _subWindowUp = false;
                        break;
                    case 18:
                        ReportUI.gameObject.SetActive(false);
                        _subWindowUp = false;
                        break;
                    case 21:
                        ExchangerUI.gameObject.SetActive(false);
                        int leftStartingMp = ExchangerUI.LeftArmy.RemainingMp;
                        int rightStartingMp = ExchangerUI.RightArmy.RemainingMp;
                        if (ExchangerUI.LeftReceived)
                        {
                            UndoMoves.Clear();
                            int costToEnter = StrategicTileInfo.WalkCost(ExchangerUI.LeftArmy.Position.X, ExchangerUI.LeftArmy.Position.Y);
                            if (rightStartingMp - costToEnter < leftStartingMp) ExchangerUI.LeftArmy.RemainingMp = Math.Max(0, rightStartingMp - costToEnter);
                        }

                        if (ExchangerUI.RightReceived)
                        {
                            UndoMoves.Clear();
                            int costToEnter = StrategicTileInfo.WalkCost(ExchangerUI.RightArmy.Position.X, ExchangerUI.RightArmy.Position.Y);
                            if (leftStartingMp - costToEnter < rightStartingMp) ExchangerUI.RightArmy.RemainingMp = Math.Max(0, leftStartingMp - costToEnter);
                        }

                        if (ExchangerUI.LeftArmy.Units.Count == 0)
                        {
                            ExchangerUI.LeftArmy.ItemStock.TransferAllItems(ExchangerUI.RightArmy.ItemStock);
                        }
                        else if (ExchangerUI.RightArmy.Units.Count == 0)
                        {
                            ExchangerUI.RightArmy.ItemStock.TransferAllItems(ExchangerUI.LeftArmy.ItemStock);
                        }

                        if (ExchangerUI.RightArmy.Units.All(u => u.HasTrait(TraitType.Infiltrator)))
                        {
                            Village village = StrategicUtilities.GetVillageAt(ExchangerUI.RightArmy.Position);
                            var infilitrators = new List<Unit>();


                            ExchangerUI.RightArmy.Units.ForEach(unit => { infilitrators.Add(unit); });
                            if (village != null && village.Empire.IsEnemy(ExchangerUI.LeftArmy.EmpireOutside))
                            {
                                infilitrators.ForEach(inf => { StrategicUtilities.TryInfiltrate(ExchangerUI.RightArmy, inf, village); });
                                ExchangerUI.RightArmy.EmpireOutside.Armies.Remove(ExchangerUI.RightArmy);
                                ExchangerUI.RightArmy.EmpireOutside.ArmiesCreated--;
                            }
                            else
                            {
                                MercenaryHouse mercHouse = StrategicUtilities.GetMercenaryHouseAt(ExchangerUI.RightArmy.Position);
                                if (mercHouse != null)
                                {
                                    infilitrators.ForEach(inf => { StrategicUtilities.TryInfiltrate(ExchangerUI.RightArmy, inf, null, mercHouse); });
                                    ExchangerUI.RightArmy.EmpireOutside.Armies.Remove(ExchangerUI.RightArmy);
                                    ExchangerUI.RightArmy.EmpireOutside.ArmiesCreated--;
                                }
                            }
                        }

                        Regenerate();
                        _subWindowUp = false;
                        break;
                }
            }
        }
    }


    private void SetupDevour()
    {
        Village village = StrategicUtilities.GetVillageAt(SelectedArmy.Position);

        if (village != null && SelectedArmy.RemainingMp > 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"this is the village of {village.Name}");
            sb.AppendLine($"the population are {village.Race}");
            sb.AppendLine($"it has a population of {village.GetTotalPop()}");
            sb.AppendLine($"of which {village.Garrison} are garrison units");
            sb.AppendLine($"to completely heal you should eat {SelectedArmy.GetDevourmentCapacity(1)}");
            if (SelectedArmy.Units.Where(s => s.Predator == false).Any()) sb.AppendLine($"note that some of the units in this army are not predators");
            sb.AppendLine("how many should they devour?");

            if (village.GetRecruitables().Count > 0)
            {
                sb.AppendLine($"(it has {village.GetRecruitables().Count} stored units");
                sb.AppendLine($"if you try to eat too many it will eat these)");
            }

            DevourUI.FullText.text = sb.ToString();
        }
    }

    public void BuildDevourSelectDisplay()
    {
        if (SelectedArmy == null) return;
        Village village = StrategicUtilities.GetVillageAt(SelectedArmy.Position);
        if (village == null) return;
        int children = RaceUI.ActorFolder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(RaceUI.ActorFolder.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < village.VillagePopulation.Population.Count; i++)
        {
            if (village.VillagePopulation.Population[i].Population > 0)
            {
                GameObject obj = Instantiate(RaceUI.DevourPanel, RaceUI.ActorFolder);
                UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
                // Side was 1 for Unit
                ActorUnit actor = new ActorUnit(new Vec2I(0, 0), new Unit(Race.Dog.ToSide(), village.VillagePopulation.Population[i].Race, 0, true));
                TextMeshProUGUI text = obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
                var racePar = RaceParameters.GetTraitData(actor.Unit);
                text.text = $"{village.VillagePopulation.Population[i].Race}\nTotal: {village.VillagePopulation.Population[i].Population} \nHireable: {village.VillagePopulation.Population[i].Hireables}\nFavored Stat: {State.RaceSettings.GetFavoredStat(actor.Unit.Race)}\nDefault Traits:\n{State.RaceSettings.ListTraits(actor.Unit.Race)}";
                sprite.UpdateSprites(actor);
                Button button1 = obj.GetComponentsInChildren<Button>()[0];
                Race tempRace = village.VillagePopulation.Population[i].Race;
                button1.onClick.AddListener(() => Devour(SelectedArmy, tempRace));
                button1.onClick.AddListener(() => BuildDevourSelectDisplay());
                Button button2 = obj.GetComponentsInChildren<Button>()[1];
                button2.onClick.AddListener(() => DevourAll(SelectedArmy, tempRace));
                button2.onClick.AddListener(() => BuildDevourSelectDisplay());
            }
        }

        RaceUI.gameObject.SetActive(true);
        if (SelectedArmy.RemainingMp == 0)
        {
            DevourUI.gameObject.SetActive(false);
            _subWindowUp = false;
        }
    }

    public void CloseDevourSelectPanel()
    {
        RaceUI.gameObject.SetActive(false);
    }

    public void Devour(Army predArmy, int numToEat)
    {
        if (numToEat <= 0) return;
        Village village = StrategicUtilities.GetVillageAt(predArmy.Position);

        if (village != null && predArmy.RemainingMp > 0 && village.GetTotalPop() > 0)
        {
            if (village.GetTotalPop() < numToEat) numToEat = village.GetTotalPop();

            village.DevouredPercentage(numToEat / village.GetTotalPop());
            village.SubtractPopulation(numToEat);


            predArmy.DevourHeal(numToEat);

            if (village.GetTotalPop() < 1)
            {
                RedrawVillages();
                if (!Equals(village.Empire.ReplacedRace, predArmy.EmpireOutside.ReplacedRace))
                {
                    RelationsManager.Genocide(predArmy.EmpireOutside, village.Empire);
                }
            }
        }

        RegenArmyBar(SelectedArmy);
    }

    public void Devour(Army predArmy, Race race)
    {
        Village village = StrategicUtilities.GetVillageAt(predArmy.Position);

        if (village != null && (predArmy.RemainingMp > 0 || predArmy.DevourThisTurn == true) && village.GetTotalPop() > 0)
        {
            village.DevouredPercentage(1 / village.GetTotalPop());
            village.SubtractPopulation(1, race);

            if (Config.MultiRaceVillages && village.VillagePopulation.GetRacePop(race) <= 0) village.VillagePopulation.DirectLinkToNamed().RemoveAll(s => Equals(s.Race, race));

            predArmy.DevourHeal(1);

            if (village.GetTotalPop() < 1)
            {
                RedrawVillages();
                if (!Equals(village.Empire.ReplacedRace, predArmy.EmpireOutside.ReplacedRace))
                {
                    RelationsManager.Genocide(predArmy.EmpireOutside, village.Empire);
                }
            }
        }

        RegenArmyBar(SelectedArmy);
    }

    public void DevourAll(Army predArmy, Race race)
    {
        Village village = StrategicUtilities.GetVillageAt(predArmy.Position);

        int numToEat = village.VillagePopulation.GetRacePop(race);

        if (village != null && (predArmy.RemainingMp > 0 || predArmy.DevourThisTurn == true) && village.GetTotalPop() > 0)
        {
            village.DevouredPercentage(numToEat / village.GetTotalPop());
            village.SubtractPopulation(numToEat, race);

            if (Config.MultiRaceVillages) village.VillagePopulation.DirectLinkToNamed().RemoveAll(s => Equals(s.Race, race));

            predArmy.DevourHeal(numToEat);

            if (village.GetTotalPop() < 1)
            {
                RedrawVillages();
                if (!Equals(village.Empire.ReplacedRace, predArmy.EmpireOutside.ReplacedRace))
                {
                    RelationsManager.Genocide(predArmy.EmpireOutside, village.Empire);
                }
            }
        }

        RegenArmyBar(SelectedArmy);
    }

    //public void Devour(Army predArmy, int numToEat, Race race)
    //{
    //    if (numToEat <= 0)
    //        return;
    //    Village village = StrategicUtilities.GetVillageAt(predArmy.Position);

    //    if (village != null && (predArmy.RemainingMP > 0 || predArmy.DevourThisTurn == true) && village.GetTotalPop() > 0)
    //    {

    //        if (village.GetTotalPop() < numToEat)
    //            numToEat = village.GetTotalPop();

    //        village.DevouredPercentage(numToEat / village.GetTotalPop());
    //        village.SubtractPopulation(numToEat, race);


    //        predArmy.DevourHeal(numToEat);

    //        if (village.GetTotalPop() < 1)
    //        {
    //            RedrawVillages();
    //            if (village.Empire.Race != predArmy.Empire.Race)
    //            {
    //                RelationsManager.Genocide(predArmy.Empire, village.Empire);
    //            }
    //        }
    //    }

    //    RegenArmyBar(SelectedArmy);
    //}

    private void CheckPath(Vec2I mouseLocation)
    {
        if (!_mouseMovementMode)
        {
            _arrowManager.ClearNodes();
            return;
        }

        if (SelectedArmy == null) return;

        if (_currentPathDestination != null && mouseLocation.Matches(_currentPathDestination)) return;
        _currentPathDestination = mouseLocation;
        var path = StrategyPathfinder.GetArmyPath(ActingEmpire, SelectedArmy, mouseLocation, SelectedArmy.RemainingMp, SelectedArmy.MovementMode == MovementMode.Flight);
        _arrowManager.ClearNodes();
        if (path == null || path.Count == 0) return;
        int remainingMp = SelectedArmy.RemainingMp;
        for (int i = 0; i < path.Count; i++)
        {
            Vec2I nextNode = new Vec2I(path[i].X, path[i].Y);
            Army.TileAction action = SelectedArmy.CheckTileForActionType(nextNode);
            if (action == Army.TileAction.OneMp)
                remainingMp--;
            else if (action == Army.TileAction.TwoMp)
                remainingMp -= 2;
            else if (action == Army.TileAction.Attack)
            {
                if (remainingMp > 0)
                {
                    _arrowManager.PlaceNode(Color.red, nextNode);
                    break;
                }

                remainingMp -= 1;
            }

            if (remainingMp >= 0)
                _arrowManager.PlaceNode(Color.green, nextNode);
            else
            {
                ContinuePath(SelectedArmy, path, i);
                break;
            }
        }
    }

    private void ShowPathOfArmy(Army army)
    {
        var path = StrategyPathfinder.GetArmyPath(ActingEmpire, army, army.Destination, army.RemainingMp, army.MovementMode == MovementMode.Flight);
        _arrowManager.ClearNodes();
        if (path == null || path.Count == 0) return;
        int remainingMp = army.RemainingMp;

        for (int i = 0; i < path.Count; i++)
        {
            Vec2I nextNode = new Vec2I(path[i].X, path[i].Y);

            Army.TileAction action = army.CheckTileForActionType(nextNode);
            if (action == Army.TileAction.OneMp)
                remainingMp--;
            else if (action == Army.TileAction.TwoMp)
                remainingMp -= 2;
            else if (action == Army.TileAction.Attack)
            {
                if (remainingMp > 0)
                {
                    _arrowManager.PlaceNode(Color.red, nextNode);
                    break;
                }

                remainingMp -= 1;
            }

            if (remainingMp >= 0)
                _arrowManager.PlaceNode(Color.green, nextNode);
            else
            {
                ContinuePath(army, path, i);
                break;
            }
        }
    }

    private void ContinuePath(Army army, List<PathNode> path, int current)
    {
        int lastNumber = 1;
        int remainingBatchMp = army.GetMaxMovement();
        for (int i = current; i < path.Count; i++)
        {
            Vec2I nextNode = new Vec2I(path[i].X, path[i].Y);
            Army.TileAction action = army.CheckTileForActionType(nextNode);

            if (action == Army.TileAction.OneMp)
                remainingBatchMp--;
            else if (action == Army.TileAction.TwoMp)
                remainingBatchMp -= 2;
            else if (action == Army.TileAction.Attack)
            {
                remainingBatchMp = 0;
            }


            if (remainingBatchMp == 1)
            {
                if (path.Count >= i + 2)
                {
                    Army.TileAction nextAction = army.CheckTileForActionType(new Vec2I(path[i + 1].X, path[i + 1].Y));
                    if (nextAction == Army.TileAction.TwoMp) remainingBatchMp = 0;
                }
            }

            if (remainingBatchMp == 0)
            {
                _arrowManager.PlaceNode(Color.gray, nextNode, lastNumber.ToString());
                lastNumber++;
                remainingBatchMp = army.GetMaxMovement();
            }
            else
                _arrowManager.PlaceNode(Color.gray, nextNode);
        }
    }


    private void SetupTrain()
    {
        Village village = StrategicUtilities.GetVillageAt(SelectedArmy.Position);

        if (village != null && SelectedArmy.RemainingMp > 0)
        {
            StringBuilder sb = new StringBuilder();
            int unitCount = SelectedArmy.Units.Count;
            sb.Append($"This army has {unitCount} units");
            _trainCost = new int[7];
            _trainExp = new int[7];

            for (int i = 0; i < 7; i++)
            {
                _trainCost[i] = SelectedArmy.TrainingGetCost(i);
                _trainExp[i] = SelectedArmy.TrainingGetExpValue(i);
            }

            var maxTrainLevel = village.NetBoosts.MaximumTrainingLevelAdd;

            List<string> options = new List<string>();

            if (maxTrainLevel >= 1) options.Add($"Steady Training - {_trainExp[0]} EXP, {_trainCost[0]}G");
            if (maxTrainLevel >= 2) options.Add($"Involved Training - {_trainExp[1]} EXP, {_trainCost[1]}G");
            if (maxTrainLevel >= 3) options.Add($"Heavy Training - {_trainExp[2]} EXP, {_trainCost[2]}G");
            if (maxTrainLevel >= 4) options.Add($"Advanced Training - {_trainExp[3]} EXP, {_trainCost[3]}G");
            if (maxTrainLevel >= 5) options.Add($"Extreme Training - {_trainExp[4]} EXP, {_trainCost[4]}G");
            if (maxTrainLevel >= 6) options.Add($"Hero Training - {_trainExp[5]} EXP, {_trainCost[5]}G");
            if (maxTrainLevel >= 7) options.Add($"Godly Training - {_trainExp[6]} EXP, {_trainCost[6]}G");

            TrainUI.TrainingLevel.ClearOptions();
            TrainUI.TrainingLevel.AddOptions(options);
            if (TrainUI.TrainingLevel.value >= maxTrainLevel)
            {
                TrainUI.TrainingLevel.value = maxTrainLevel - 1;
                TrainUI.TrainingLevel.RefreshShownValue();
            }

            TrainUI.FullText.text = sb.ToString();
            CheckTrainingCost();
        }
    }

    public void CheckTrainingCost()
    {
        if (ActingEmpire.Gold >= _trainCost[TrainUI.TrainingLevel.value])
        {
            TrainUI.Train.interactable = true;
            TrainUI.Train.GetComponentInChildren<Text>().text = "Train!";
        }
        else
        {
            TrainUI.Train.interactable = false;
            TrainUI.Train.GetComponentInChildren<Text>().text = "Not enough gold";
        }
    }

    public void TrainSelectedArmy()
    {
        Village village = StrategicUtilities.GetVillageAt(SelectedArmy.Position);

        if (village != null && SelectedArmy.RemainingMp > 0)
        {
            SelectedArmy.Train(TrainUI.TrainingLevel.value);
            SelectedArmy.RemainingMp = 0;
            State.GameManager.StrategyMode.UndoMoves.Clear();
        }

        Regenerate();
    }

    private void ArmyInfoSetup()
    {
        Village village = StrategicUtilities.GetVillageAt(SelectedArmy.Position);
        if (village != null)
        {
            State.GameManager.ActivateRecruitMode(village, ActingEmpire);
        }
        else
        {
            MercenaryHouse house = StrategicUtilities.GetMercenaryHouseAt(SelectedArmy.Position);
            if (house != null)
                State.GameManager.ActivateRecruitMode(house, ActingEmpire, SelectedArmy);
            else
                State.GameManager.ActivateRecruitMode(ActingEmpire, SelectedArmy);
        }
    }


    private void EndTurn()
    {
        UndoMoves.Clear();
        ActingEmpire.Reports.Clear();
        if (State.World.EmpireOrder == null || State.World.EmpireOrder.Count != State.World.AllActiveEmpiresCount) State.World.RefreshTurnOrder();
        int startingIndex = State.World.EmpireOrder.IndexOf(ActingEmpire);
        SelectedArmy = null;
        StatusBarUI.EndTurn.interactable = false;
        StatusBarUI.ShowTurnReport.gameObject.SetActive(false);
        StatusBarUI.EmpireStatus.interactable = OnlyAIPlayers;
        if (startingIndex + 1 >= State.World.EmpireOrder.Count)
        {
            ScaledExp = StrategicUtilities.Get80ThExperiencePercentile();
            StatusBarUI.RecreateWorld.gameObject.SetActive(false);
            State.World.Turn++;
            ProcessGrowth();
            if (Config.Diplomacy == false)
            {
                RelationsManager.ResetRelationTypes();
            }

            State.EventList.CheckStartAIEvent();

            RelationsManager.TurnElapsed();
            ActingEmpire = State.World.EmpireOrder[0];
            if (State.World.MonsterEmpires.Count() < RaceFuncs.SpawnerElligibleMonsterRaces.Count) State.World.RefreshMonstersKeepingArmies();
            foreach (ClaimableBuilding claimable in State.World.Claimables)
            {
                claimable.TurnChanged();
            }

            if (OnlyAIPlayers) State.Save($"{State.SaveDirectory}Autosave.sav");
        }
        else
        {
            ActingEmpire = State.World.EmpireOrder[startingIndex + 1];
        }

        float nightRoll = (float)State.Rand.NextDouble();
        if (Config.DayNightEnabled)
        {
            if (Config.DayNightSchedule && State.World.Turn % Config.World.NightRounds == 0)
            {
                State.World.IsNight = true;
            }
            else if (Config.DayNightRandom && nightRoll < NightChance)
            {
                State.World.IsNight = true;
                NightChance = Config.BaseNightChance;
            }
            else
            {
                State.World.IsNight = false;
                NightChance += Config.NightChanceIncrease;
            }
        }
        else
        {
            State.World.IsNight = false;
            NightChance = Config.BaseNightChance;
        }

        if (ActingEmpire is MonsterEmpire)
        {
            if (Equals(ActingEmpire.Race, Race.Goblin))
            {
            }
            else if (Config.World.GetSpawner(ActingEmpire.Race).Enabled == false)
            {
                EndTurn();
                return;
            }
        }


        VictoryCheck();

        if (ActingEmpire.KnockedOut && (RaceFuncs.NotRebelOrBandit(ActingEmpire.Side) || (ActingEmpire.Armies.Count == 0 && ActingEmpire.VillageCount == 0)))
        {
            EndTurn();
            return;
        }

        CheckIfLastHumanPlayerEliminated();
        BeginTurn();
    }

    internal void BeginTurn()
    {
#if UNITY_EDITOR
        var units = StrategicUtilities.GetAllUnits();
        int duplicates = 0;
        for (int i = 0; i < units.Count; i++)
        {
            for (int j = 0; j < units.Count; j++)
            {
                if (i == j) continue;
                if (units[i] == units[j]) duplicates++;
            }
        }

        if (duplicates > 0)
        {
            Debug.LogWarning($"{duplicates / 2} duplicated units found!");
        }
#endif
        ProcessIncome(ActingEmpire);
        if (ActingEmpire.StrategicAI == null)
        {
            if (State.World.Turn > 1) ActingEmpire.CheckEvent();
            LastHumanEmpire = ActingEmpire;
            State.Save($"{State.SaveDirectory}Autosave.sav");
            StatusBarUI.EndTurn.interactable = true;
            StatusBarUI.ShowTurnReport.gameObject.SetActive(ActingEmpire.Reports.Count > 0);
            StatusBarUI.EmpireStatus.interactable = true;
            EnemyTurnText.SetActive(false);
            ShowTurnReport();
        }
        else
        {
            if (ActingEmpire.StrategicAI.TurnAI()) //If a unit was purchased
                RedrawArmies();
            if (ActingEmpire.Armies.Count > 0)
                _mTimer = .3f;
            else
                _mTimer = .01f;
            StatusBarUI.EmpireStatus.interactable = ActingEmpire.IsAlly(LastHumanEmpire) || OnlyAIPlayers;
            EnemyTurnText.SetActive(true);
        }

        _runningQueued = true;
        Regenerate();
    }

    internal void CheckForRevivedPlayerFromMapEditor()
    {
        foreach (Empire empire in State.World.MainEmpires)
        {
            if (empire.KnockedOut && State.World.Villages.Where(s => s.Empire.IsAlly(empire)).Count() > 0) empire.KnockedOut = false;
        }
    }

    private void CheckIfLastHumanPlayerEliminated()
    {
        if (ActingEmpire.KnockedOut == false && StrategicUtilities.GetAllArmies().Where(s => s.EmpireOutside.IsAlly(ActingEmpire)).Any() == false && State.World.Villages.Where(s => s.Empire.IsAlly(ActingEmpire)).Count() == 0 && ActingEmpire is MonsterEmpire == false)
        {
            ActingEmpire.KnockedOut = true;

            if (ActingEmpire.StrategicAI == null)
            {
                OnlyAIPlayers = true;
                foreach (Empire emp in State.World.MainEmpires)
                {
                    if (emp.StrategicAI == null && emp.KnockedOut == false)
                    {
                        OnlyAIPlayers = false;
                    }
                }

                if (OnlyAIPlayers && State.GameManager.CurrentScene == State.GameManager.StrategyMode)
                {
                    var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
                    box.SetData(State.GameManager.ActivateEndSceneLose,
                        "End Game",
                        "Continue Watching",
                        "The last human player is eradicated, do you want to continue watching or end the game now and show stats?",
                        EndTurn);
                    return;
                }
            }
        }
    }


    private void VictoryCheck()
    {
        Dictionary<Empire, bool> survivors = new Dictionary<Empire, bool>();

        switch (Config.VictoryCondition)
        {
            case Config.VictoryType.AllCapitals:
                foreach (Village village in State.World.Villages.Where(s => s.Capital))
                {
                    if (village.GetTotalPop() < 1) continue;
                    survivors[village.Empire] = true;
                }

                break;
            case Config.VictoryType.AllCities:
                foreach (Village village in State.World.Villages)
                {
                    if (village.GetTotalPop() < 1) continue;
                    survivors[village.Empire] = true;
                }

                break;
            case Config.VictoryType.CompleteElimination:
                foreach (Village village in State.World.Villages)
                {
                    if (village.GetTotalPop() < 1) continue;
                    survivors[village.Empire] = true;
                }

                foreach (Army army in StrategicUtilities.GetAllArmies(true))
                {
                    survivors[army.EmpireOutside] = true;
                }

                break;
            case Config.VictoryType.NeverEnd:
                return;
        }

        // was set to 0
        Side side = Race.Cat.ToSide();
        foreach (Empire emp in survivors.Keys)
        {
            side = emp.Side;
            foreach (Empire emp2 in survivors.Keys)
            {
                if (emp == emp2) continue;
                if (emp.IsAlly(emp2) == false) return;
            }
        }

        State.GameManager.ActivateEndSceneWin(side);
    }


    private void ProcessIncome(Empire empire)
    {
        empire.CalcIncome(State.World.Villages, true);
        empire.AddGold(empire.Income);
        if (RaceFuncs.NotMainRace(empire.Side))
        {
            if (empire.Gold < 0) empire.AddGold(-empire.Gold);
        }
        else if (empire.Gold < 0)
        {
            int income = empire.Income;

            if (income < 0)
            {
                Unit[] dismissOrder = empire.GetAllUnits().Where(s => s.Health > 0).OrderBy(s => s.Experience).ToArray();
                for (int k = 0; k < dismissOrder.Length; k++)
                {
                    if (dismissOrder[k].Type == UnitType.Leader) continue;
                    if (income >= 0) break;
                    foreach (Army army in empire.Armies)
                    {
                        if (army.Units.Contains(dismissOrder[k]))
                        {
                            StrategicUtilities.ProcessTravelingUnit(dismissOrder[k], army);
                            army.Units.Remove(dismissOrder[k]);
                            break;
                        }
                    }

                    dismissOrder[k].Health = 0;
                    income += Config.World.ArmyUpkeep;
                }
            }

            Regenerate();
        }
    }

    private void ProcessGrowth()
    {
        for (int i = 0; i < State.World.Villages.Length; i++)
        {
            State.World.Villages[i].NewTurn();
        }

        foreach (Empire empire in State.World.AllActiveEmpires)
        {
            empire.CalcIncome(State.World.Villages);
            empire.Regenerate();
        }

        for (int i = 0; i < State.World.MercenaryHouses.Length; i++)
        {
            State.World.MercenaryHouses[i].UpdateStock();
        }

        MercenaryHouse.UpdateStaticStock();
    }


    private void ProcessClick(int x, int y)
    {
        Vec2I clickLocation = new Vec2I(x, y);
        if (_mouseMovementMode)
        {
            SetSelectedArmyPathTo(x, y);
        }
        else if (_pickingExchangeLocation)
        {
            OpenExchangerPanel(SelectedArmy, clickLocation);
        }
        else
        {
            foreach (Army army in ActingEmpire.Armies.Where(a => Equals(a.Side, ActingEmpire.Side)))
            {
                if (army.Position.GetDistance(clickLocation) < 1)
                {
                    if (SelectedArmy != army)
                    {
                        SelectedArmy = army;
                        SelectedArmy.RefreshMovementMode();
                        if (army.Destination != null && army.Destination.GetDistance(army.Position) > 0)
                        {
                            var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
                            box.SetData(() => SelectedArmy.Destination = null, "Clear Orders", "Leave Orders", "This army has a queued movement order, do you want to clear it?");
                        }

                        return;
                    }
                }
            }

            foreach (Army army in StrategicUtilities.GetAllArmies().Where(s => Equals(s.Side, ActingEmpire.Side)))
            {
                if (army.Position.GetDistance(clickLocation) < 1)
                {
                    Village village = StrategicUtilities.GetVillageAt(army.Position);
                    if (village != null)
                    {
                        State.GameManager.ActivateRecruitMode(village, ActingEmpire);
                    }
                    else
                    {
                        MercenaryHouse house = StrategicUtilities.GetMercenaryHouseAt(army.Position);
                        if (house != null)
                            State.GameManager.ActivateRecruitMode(house, ActingEmpire, army, RecruitMode.ActivatingEmpire.Self);
                        else
                            State.GameManager.ActivateRecruitMode(ActingEmpire, army, RecruitMode.ActivatingEmpire.Self);
                    }

                    return;
                }
            }


            foreach (Army army in StrategicUtilities.GetAllArmies().Where(s => !Equals(s.Side, ActingEmpire.Side) && s.EmpireOutside.IsAlly(ActingEmpire)))
            {
                if (army.Position.GetDistance(clickLocation) < 1)
                {
                    Village village = StrategicUtilities.GetVillageAt(army.Position);
                    if (village != null)
                    {
                        State.GameManager.ActivateRecruitMode(village, ActingEmpire);
                    }
                    else
                    {
                        MercenaryHouse house = StrategicUtilities.GetMercenaryHouseAt(army.Position);
                        if (house != null)
                            State.GameManager.ActivateRecruitMode(house, ActingEmpire, army, RecruitMode.ActivatingEmpire.Ally);
                        else
                            State.GameManager.ActivateRecruitMode(ActingEmpire, army, RecruitMode.ActivatingEmpire.Ally);
                    }

                    return;
                }
            }

            foreach (Army army in StrategicUtilities.GetAllArmies().Where(s => ((!Equals(s.Side, ActingEmpire.Side) && s.EmpireOutside.IsEnemy(ActingEmpire)) || (!Equals(s.Side, ActingEmpire.Side) && s.EmpireOutside.IsNeutral(ActingEmpire))) && ContainsFriendly(s)))
            {
                if (army.Position.GetDistance(clickLocation) < 1)
                {
                    Village village = StrategicUtilities.GetVillageAt(army.Position);
                    if (village != null)
                    {
                        State.GameManager.ActivateRecruitMode(village, ActingEmpire, RecruitMode.ActivatingEmpire.Infiltrator);
                    }
                    else
                    {
                        MercenaryHouse house = StrategicUtilities.GetMercenaryHouseAt(army.Position);
                        if (house != null)
                            State.GameManager.ActivateRecruitMode(house, ActingEmpire, army, RecruitMode.ActivatingEmpire.Infiltrator);
                        else
                            State.GameManager.ActivateRecruitMode(ActingEmpire, army, RecruitMode.ActivatingEmpire.Infiltrator);
                    }

                    return;
                }
            }

            for (int i = 0; i < State.World.Villages.Length; i++)
            {
                if (State.World.Villages[i].Position.GetDistance(clickLocation) == 0)
                {
                    if (State.World.Villages[i].Empire.IsAlly(ActingEmpire))
                    {
                        State.GameManager.ActivateRecruitMode(State.World.Villages[i], ActingEmpire);
                        return;
                    }
                }
            }

            if (Config.CheatViewHostileArmies)
            {
                if (ProcessClickWithoutEmpire(x, y)) return;
            }

            SelectedArmy = null;
        }
    }

    private bool ContainsFriendly(Army s)
    {
        return s.Units.Any(u => { return Equals(u.FixedSide, ActingEmpire.Side) || (State.World.GetEmpireOfSide(u.FixedSide)?.IsAlly(ActingEmpire) ?? false); });
    }

    private bool ProcessClickWithoutEmpire(int x, int y)
    {
        Vec2I clickLocation = new Vec2I(x, y);
        foreach (Army army in StrategicUtilities.GetAllArmies())
        {
            if (army.Position.GetDistance(clickLocation) < 1)
            {
                Village village = StrategicUtilities.GetVillageAt(army.Position);
                if (village != null)
                {
                    State.GameManager.ActivateRecruitMode(village, ActingEmpire, RecruitMode.ActivatingEmpire.Observer);
                }
                else
                    State.GameManager.ActivateRecruitMode(ActingEmpire, army, RecruitMode.ActivatingEmpire.Observer);

                return true;
            }
        }

        for (int i = 0; i < State.World.Villages.Length; i++)
        {
            if (State.World.Villages[i].Position.GetDistance(clickLocation) == 0)
            {
                State.GameManager.ActivateRecruitMode(State.World.Villages[i], ActingEmpire, RecruitMode.ActivatingEmpire.Observer);
                return true;
            }
        }

        return false;
    }

    private bool ProcessClickBetweenTurns(int x, int y)
    {
        Vec2I clickLocation = new Vec2I(x, y);
        foreach (Army army in StrategicUtilities.GetAllArmies())
        {
            if (army.Position.GetDistance(clickLocation) < 1 && army.EmpireOutside.IsAlly(LastHumanEmpire))
            {
                Village village = StrategicUtilities.GetVillageAt(army.Position);
                if (village != null)
                {
                    State.GameManager.ActivateRecruitMode(village, ActingEmpire, RecruitMode.ActivatingEmpire.Observer);
                }
                else
                    State.GameManager.ActivateRecruitMode(ActingEmpire, army, RecruitMode.ActivatingEmpire.Observer);

                return true;
            }
        }

        for (int i = 0; i < State.World.Villages.Length; i++)
        {
            if (State.World.Villages[i].Position.GetDistance(clickLocation) == 0 && State.World.Villages[i].Empire.IsAlly(LastHumanEmpire))
            {
                State.GameManager.ActivateRecruitMode(State.World.Villages[i], ActingEmpire, RecruitMode.ActivatingEmpire.Observer);
                return true;
            }
        }

        return false;
    }

    private void SetSelectedArmyPathTo(int x, int y)
    {
        if (SelectedArmy == null) return;
        _arrowManager.ClearNodes();
        Vec2I clickLoc = new Vec2I(x, y);
        QueuedPath = StrategyPathfinder.GetArmyPath(ActingEmpire, SelectedArmy, clickLoc, SelectedArmy.RemainingMp, SelectedArmy.MovementMode == MovementMode.Flight);
        if (QueuedPath == null)
        {
            Army army = StrategicUtilities.ArmyAt(clickLoc);
            Village village = StrategicUtilities.GetVillageAt(clickLoc);
            if (army != null && army.EmpireOutside.IsNeutral(SelectedArmy.EmpireOutside))
            {
                var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
                box.SetData(() => RelationsManager.SetWar(army.EmpireOutside, SelectedArmy.EmpireOutside), "Declare War!", "Maintain Peace", $"Declare War against the {army.EmpireOutside.Name}?");
            }

            if (village != null && village.Empire.IsNeutral(SelectedArmy.EmpireOutside))
            {
                var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
                box.SetData(() => RelationsManager.SetWar(village.Empire, SelectedArmy.EmpireOutside), "Declare War!", "Maintain Peace", $"Declare War against the {village.Empire.Name}?");
            }

            return;
        }

        SelectedArmy.Destination = clickLoc;
        if (StrategicUtilities.ArmyAt(clickLoc) != null || StrategicUtilities.GetVillageAt(clickLoc) != null)
        {
            _queuedAttackPermission = true;
        }
        else
            _queuedAttackPermission = false;

        _mouseMovementMode = false;
    }

    private void SearchForVillage(string text)
    {
        var village = State.World.Villages.Where(s => s.Name.ToLower().Contains(text.ToLower())).FirstOrDefault();
        if (village != null)
        {
            State.GameManager.CenterCameraOnTile(village.Position.X, village.Position.Y);
        }
    }

    private string ReturnTextVillageCount(string search)
    {
        var count = State.World.Villages.Where(s => s.Name.ToLower().Contains(search.ToLower())).Count();
        if (count == 1) return "1 match";
        return $"{count} matches";
    }


    public override void ReceiveInput()
    {
        if (State.GameManager.ActiveInput) return;
        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.RightCommand)) && Input.GetKey(KeyCode.F))
        {
            var box = Instantiate(State.GameManager.InputBoxPrefab).GetComponentInChildren<InputBox>();
            box.SetData(SearchForVillage, "Search", "Cancel", "Search For Village", 20);
            box.ActivateTypeMethod(ReturnTextVillageCount);
        }

        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Z))
        {
            if (UndoButton.gameObject.activeSelf) UndoButton.onClick.Invoke();
        }

        UndoButton.gameObject.SetActive(UndoMoves.Any());

        if (State.GameManager.TacticalMode.ActorFolder.childCount > 0)
        {
            int children = State.GameManager.TacticalMode.ActorFolder.childCount;
            for (int i = children - 1; i >= 0; i--)
            {
                Destroy(State.GameManager.TacticalMode.ActorFolder.GetChild(i).gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            foreach (Village village in State.World.Villages)
            {
                var obj = Instantiate(SpriteCategories[1]).GetComponent<SpriteRenderer>();
                obj.transform.SetPositionAndRotation(new Vector3(village.Position.X, village.Position.Y), new Quaternion());
                obj.sprite = Banners[2];
                obj.sortingOrder = 30000;
                if (Equals(village.Side, ActingEmpire.Side))
                    obj.color = new Color(0, 1, 0, .75f);
                else if (village.Empire.IsEnemy(ActingEmpire))
                    obj.color = new Color(1, 0, 0, .75f);
                else if (village.Empire.IsNeutral(ActingEmpire))
                    obj.color = new Color(0, 0, 0, .75f);
                else
                    obj.color = new Color(0, 0, 1, .75f);
                _shownIff.Add(obj.gameObject);
            }

            foreach (Army army in StrategicUtilities.GetAllArmies())
            {
                var obj = Instantiate(SpriteCategories[1]).GetComponent<SpriteRenderer>();
                obj.transform.SetPositionAndRotation(new Vector3(army.Position.X, army.Position.Y), new Quaternion());
                obj.sprite = Banners[2];
                obj.sortingOrder = 30000;
                if (Equals(army.Side, ActingEmpire.Side))
                    obj.color = new Color(0, 1, 0, .75f);
                else if (army.EmpireOutside.IsEnemy(ActingEmpire))
                    obj.color = new Color(1, 0, 0, .75f);
                else if (army.EmpireOutside.IsNeutral(ActingEmpire))
                    obj.color = new Color(0, 0, 0, .75f);
                else
                    obj.color = new Color(0, 0, 1, .75f);
                _shownIff.Add(obj.gameObject);
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            foreach (var obj in _shownIff.ToList())
            {
                Destroy(obj);
            }
        }


        if (_remainingNotificationTime > 0) _remainingNotificationTime -= Time.deltaTime;
        if (_remainingNotificationTime <= 0) NotificationWindow.SetActive(false);

        if (Input.GetButtonDown("ChangeBannerSize"))
        {
            Config.BannerScale += .1f;
            if (Config.BannerScale > 1.02f) Config.BannerScale = .6f;
            PlayerPrefs.SetFloat("BannerScale", (Config.BannerScale - .5f) * 10);
        }


        if (State.GameManager.CameraTransitioning || State.GameManager.StrategicControlsLocked) return;

        if (Input.GetButtonDown("Pause"))
        {
            Paused = !Paused;
            PausedText.SetActive(Paused);
        }

        if (EventSystem.current.IsPointerOverGameObject() == false) //Makes sure mouse isn't over a UI element
        {
            Vector2 currentMousePos = State.GameManager.Camera.ScreenToWorldPoint(Input.mousePosition);

            int x = (int)(currentMousePos.x + 0.5f);
            int y = (int)(currentMousePos.y + 0.5f);
            if (x >= 0 && x < Config.StrategicWorldSizeX && y >= 0 && y < Config.StrategicWorldSizeY)
            {
                UpdateTooltips(x, y);
                if (Input.GetMouseButtonDown(0) && ActingEmpire.StrategicAI == null && _subWindowUp == false && QueuedPath == null)
                    ProcessClick(x, y);
                else if (Input.GetMouseButtonDown(0) && OnlyAIPlayers)
                    ProcessClickWithoutEmpire(x, y);
                else if (Input.GetMouseButtonDown(0) && LastHumanEmpire.IsAlly(ActingEmpire)) ProcessClickBetweenTurns(x, y);
                if (Input.GetMouseButtonDown(1) && ActingEmpire.StrategicAI == null && _subWindowUp == false && QueuedPath == null && SelectedArmy != null && SelectedArmy.RemainingMp > 0) SetSelectedArmyPathTo(x, y);
                if (_mouseMovementMode) CheckPath(new Vec2I(x, y));
            }
            else
            {
                _arrowManager.ClearNodes();
                _currentPathDestination = null;
            }
        }

        if (Input.GetButtonDown("Menu"))
        {
            State.GameManager.OpenMenu();
        }

        if (Paused) return;

        Translator.UpdateLocation();

        if (ActingEmpire.StrategicAI != null)
        {
            AI(Time.deltaTime);
        }
        else
        {
            if (_subWindowUp) return;

            RunQueuedMovement();

            if (Input.GetButtonDown("Next Unit"))
            {
                NextArmy();
            }

            if (Input.GetButtonDown("Cancel"))
            {
                SelectedArmy = null;
                ExchangeBlockerPanels.SetActive(false);
                _pickingExchangeLocation = false;
            }

            if (Input.GetButtonDown("Movement Mode") && SelectedArmy != null)
            {
                if (SelectedArmy.RemainingMp > 0)
                    _mouseMovementMode = true;
                else
                    SelectedArmy.Destination = null;
            }


            if (Input.GetButtonDown("Quicksave"))
            {
                State.Save($"{State.SaveDirectory}Quicksave.sav");
            }
            else if (Input.GetButtonDown("Quickload"))
            {
                State.GameManager.AskQuickLoad();
            }

            if (Translator.IsActive == false) UpdateArmyLocationsAndSprites();
            if (Input.GetButtonDown("Submit")) ButtonCallback(10);
        }
    }

    private void RunQueuedMovement()
    {
        MoveQueued();

        if (SelectedArmy != null && QueuedPath == null) CheckForMovementInput();

        if (_runningQueued && QueuedPath == null)
        {
            bool foundWaiting = false;
            foreach (Army army in ActingEmpire.Armies)
            {
                if (army.RemainingMp > 1 && army.Destination != null)
                {
                    SelectedArmy = army;
                    foundWaiting = true;
                    QueuedPath = StrategyPathfinder.GetArmyPath(ActingEmpire, SelectedArmy, SelectedArmy.Destination, SelectedArmy.RemainingMp, SelectedArmy.MovementMode == MovementMode.Flight);
                    if (QueuedPath == null) army.Destination = null;
                    break;
                }
            }

            if (foundWaiting == false) _runningQueued = false;
        }
    }

    private void MoveQueued()
    {
        if (QueuedPath != null)
        {
            if (SelectedArmy == null || SelectedArmy?.RemainingMp <= 0 || SelectedArmy.Destination == null)
            {
                QueuedPath = null;
                return;
            }

            if (QueuedPath.Count > 0 && SelectedArmy.RemainingMp == 1 && (SelectedArmy.CheckTileForActionType(new Vec2I(QueuedPath[0].X, QueuedPath[0].Y)) == Army.TileAction.TwoMp || SelectedArmy.CheckTileForActionType(new Vec2I(QueuedPath[0].X, QueuedPath[0].Y)) == Army.TileAction.AttackTwoMp))
            {
                QueuedPath = null;
                return;
            }

            if (QueuedPath.Count > 0)
            {
                Vec2I nextTile = new Vec2I(QueuedPath[0].X, QueuedPath[0].Y);

                if (SelectedArmy.Destination.Matches(nextTile) == false)
                {
                    if (StrategicUtilities.ArmyAt(nextTile) != null)
                    {
                        QueuedPath = null;
                        SelectedArmy.Destination = null;
                        return;
                    }

                    Village village = StrategicUtilities.GetVillageAt(nextTile);
                    if (village != null)
                    {
                        if (village.Empire.IsEnemy(SelectedArmy.EmpireOutside))
                        {
                            QueuedPath = null;
                            SelectedArmy.Destination = null;
                            return;
                        }
                    }
                }
                else
                {
                    if (StrategicUtilities.ArmyAt(nextTile) != null && _queuedAttackPermission == false)
                    {
                        QueuedPath = null;
                        SelectedArmy.Destination = null;
                        return;
                    }

                    Village village = StrategicUtilities.GetVillageAt(nextTile);
                    if (village != null)
                    {
                        if (_queuedAttackPermission == false && village.Empire.IsEnemy(SelectedArmy.EmpireOutside))
                        {
                            QueuedPath = null;
                            SelectedArmy.Destination = null;
                            return;
                        }
                        else if (village.Empire.IsNeutral(SelectedArmy.EmpireOutside))
                        {
                            QueuedPath = null;
                            SelectedArmy.Destination = null;
                            return;
                        }
                    }
                }
            }

            if (Translator.IsActive == false)
            {
                if (QueuedPath?.Count > 0)
                {
                    MoveTo(SelectedArmy, new Vec2I(QueuedPath[0].X, QueuedPath[0].Y));
                    if (QueuedPath[0].X != SelectedArmy.Position.X || QueuedPath[0].Y != SelectedArmy.Position.Y)
                        QueuedPath = null;
                    else
                        QueuedPath.RemoveAt(0);
                }
                else
                    QueuedPath = null;
            }
        }
    }

    public void OpenRenameEmpire()
    {
        var box = Instantiate(State.GameManager.InputBoxPrefab).GetComponentInChildren<InputBox>();
        _renamingEmpire = ActingEmpire;
        box.SetData(RenameEmpire, "Rename", "Cancel", $"Rename this empire ({ActingEmpire.Name})?", 20);
    }

    internal void RenameEmpire(string name)
    {
        if (_renamingEmpire != null) _renamingEmpire.Name = name;
        Regenerate();
    }

    private void UpdateTooltips(int clickX, int clickY)
    {
        Village villageAtCursor = StrategicUtilities.GetVillageAt(new Vec2I(clickX, clickY));
        if (villageAtCursor == null)
        {
            VillageTooltip.gameObject.SetActive(false);
            MercenaryHouse house = StrategicUtilities.GetMercenaryHouseAt(new Vec2I(clickX, clickY));
            if (house != null)
            {
                VillageTooltip.gameObject.SetActive(true);
                VillageTooltip.Text.text = $"Mercenary House\nMercs: {house.Mercenaries.Count}\nHas Special? {(MercenaryHouse.UniqueMercs.Count > 0 ? "Yes" : "No")}";
            }

            ClaimableBuilding claimable = StrategicUtilities.GetClaimableAt(new Vec2I(clickX, clickY));
            if (claimable != null)
            {
                VillageTooltip.gameObject.SetActive(true);
                if (claimable is GoldMine)
                {
                    VillageTooltip.Text.text = $"Gold Mine\nOwner: {claimable.Owner?.Name}\nGold Per Turn: {Config.GoldMineIncome}";
                }
            }
        }
        else
        {
            if ((Config.FogOfWar || State.World.IsNight) && FogSystem.FoggedTile[villageAtCursor.Position.X, villageAtCursor.Position.Y]) return;
            StringBuilder sb = new StringBuilder();
            VillageTooltip.gameObject.SetActive(true);
            sb.AppendLine($"Village: {villageAtCursor.Name}");
            if (villageAtCursor.Capital) sb.AppendLine($"Capital City ({villageAtCursor.OriginalRace})");
            sb.AppendLine($"Owner: {villageAtCursor.Empire.Name}");
            sb.AppendLine($"Race: {villageAtCursor.Race}");
            sb.AppendLine($"Happiness : {villageAtCursor.Happiness}");
            if (villageAtCursor.Empire.IsAlly(LastHumanEmpire) || OnlyAIPlayers || Config.CheatExtraStrategicInfo)
            {
                sb.AppendLine($"Garrison: {villageAtCursor.Garrison}");
                sb.AppendLine($"Population: {villageAtCursor.GetTotalPop()}");
                if (villageAtCursor.buildings.Count > 0)
                {
                    string buildings = "";
                    bool first = true;
                    foreach (VillageBuilding building in villageAtCursor.buildings)
                    {
                        if (!first) buildings += ", ";
                        buildings += building;
                        first = false;
                    }

                    sb.AppendLine($"Buildings: {buildings}");
                }
            }
            else
            {
                sb.AppendLine($"Garrison: {SizeToName.ForTroops(villageAtCursor.Garrison)}");
            }

            VillageTooltip.Text.text = sb.ToString();
        }

        Army armyAtCursor = StrategicUtilities.ArmyAt(new Vec2I(clickX, clickY));
        if (armyAtCursor == null)
        {
            ArmyTooltip.gameObject.SetActive(false);
        }
        else
        {
            if (((Config.FogOfWar || State.World.IsNight) && FogSystem.FoggedTile[armyAtCursor.Position.X, armyAtCursor.Position.Y]) || (armyAtCursor.Banner != null && !armyAtCursor.Banner.gameObject.activeSelf)) return;
            StringBuilder sb = new StringBuilder();
            sb = ArmyToolTip(armyAtCursor);

            ArmyTooltip.gameObject.SetActive(true);
            ArmyTooltip.Text.text = sb.ToString();

            if (armyAtCursor.Destination != null && armyAtCursor.EmpireOutside.IsAlly(ActingEmpire) && ActingEmpire.StrategicAI == null)
            {
                _currentPathDestination = null;
                ShowPathOfArmy(armyAtCursor);
            }
        }

        if (_mouseMovementMode == false && (armyAtCursor == null || armyAtCursor.Destination == null))
        {
            _arrowManager.ClearNodes();
        }
    }

    internal StringBuilder ArmyToolTip(Army armyAtCursor)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"Empire: {armyAtCursor.EmpireOutside.Name}");
        sb.AppendLine($"Army Name: {armyAtCursor.Name}");
        if (armyAtCursor.EmpireOutside.IsAlly(LastHumanEmpire) || OnlyAIPlayers || Config.CheatExtraStrategicInfo)
        {
            sb.AppendLine($"Size: {armyAtCursor.Units.Count}");
            sb.AppendLine($"Strength: {SizeToName.ForArmyStrength(StrategicUtilities.ArmyPower(armyAtCursor))}");
            sb.AppendLine($"Average Health: {Mathf.Round(armyAtCursor.GetHealthPercentage())}%");
            if (armyAtCursor.Units.Count > 0) sb.AppendLine($"Average Exp: {Math.Round(armyAtCursor.Units.Sum(s => s.Experience) / armyAtCursor.Units.Count())}");
            if (State.World.GetEmpireOfSide(armyAtCursor.Side).StrategicAI != null) sb.AppendLine($"Order: {armyAtCursor.AIMode}");
        }
        else
        {
            sb.AppendLine($"Size: {SizeToName.ForTroops(armyAtCursor.Units.Count)}");
            sb.AppendLine($"Strength: {SizeToName.ForArmyStrength(StrategicUtilities.ArmyPower(armyAtCursor))}");
            if (Config.CheatExtraStrategicInfo)
            {
                sb.AppendLine($"Average Health: {Mathf.Round(armyAtCursor.GetHealthPercentage())}%");
                if (armyAtCursor.Units.Count > 0) sb.AppendLine($"Average Exp: {Math.Round(armyAtCursor.Units.Sum(s => s.Experience) / armyAtCursor.Units.Count())}");
            }
        }

        if (Config.CheatExtraStrategicInfo)
        {
            sb.AppendLine($"Est. Power: {Math.Round(StrategicUtilities.ArmyPower(armyAtCursor), 1)}");
        }

        return sb;
    }

    public void ShowTurnReport()
    {
        if (ActingEmpire.Reports.Count > 0)
        {
            _subWindowUp = true;
            ReportUI.Generate(ActingEmpire.Reports);
        }
    }

    public void CleanUpLingeringWindows()
    {
        //Done seperately because I want to make sure it triggers only when desired.  
        _subWindowUp = false;
        DevourUI.gameObject.SetActive(false);
        TrainUI.gameObject.SetActive(false);
        ReportUI.gameObject.SetActive(false);
        ExchangerUI.gameObject.SetActive(false);
    }

    public override void CleanUp()
    {
    }
}