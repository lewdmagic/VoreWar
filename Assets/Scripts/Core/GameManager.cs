using System.IO;
using System.Linq;
using DaVikingCode.AssetPacker;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
#if UNITY_EDITOR == false
    bool confirmedQuit = false;
#endif

    private SceneBase _currentScene;

    public SceneBase CurrentScene
    {
        get { return _currentScene; }
        private set
        {
            if (_currentScene != null) _currentScene.UI.SetActive(false);
            _currentScene = value;
            _currentScene.UI.SetActive(true);
        }
    }

    public Camera Camera;
    public CameraController CameraController;

    public PipCamera PipCamera;
    public StartMode StartMode;
    public StrategyMode StrategyMode;
    public TacticalMode TacticalMode;
    public RecruitMode RecruitMode;
    public EndScene EndScene;
    public MapEditor MapEditor;

    public VariableEditor VariableEditor;


    public StatScreenPanel StatScreen;

    public HoveringTooltip HoveringTooltip;
    public HoveringRacePicture HoveringRacePicture;

    public GameObject SavePrompt;
    public GameObject DialogBoxPrefab;
    public GameObject OptionsBoxPrefab;
    public GameObject MessageBoxPrefab;
    public GameObject InputBoxPrefab;
    public GameObject FullScreenMessageBoxPrefab;
    public GameObject UIUnit;

    public GameObject ParticleSystem;

    public Material ColorSwapMaterial;

    public GameObject LoadPicker;

    public GameObject DiscardedClothing;

    public GameObject SpriteRendererPrefab;
    public GameObject SpriteRenderAnimatedPrefab;
    public GameObject ImagePrefab;
    public BattleReportPanel BattleReportPanel;

    public GameObject CornerCameraView;

    public GameMenu Menu;

    public TutorialScript TutorialScript;

    private bool _menuOpen = false;

    public bool PureTactical = false;

    public GameObject UnitBase;
    public UnitEditorPanel UnitEditor;
    public SpriteDictionary SpriteDictionary;
    public TacticalBuildingDictionary TacticalBuildingSpriteDictionary;
    public PaletteDictionary PaletteDictionary;
    public SoundManager SoundManager;
    public TacticalEffectPrefabList TacticalEffectPrefabList;
    public bool StrategicControlsLocked { get; private set; }
    public bool CameraTransitioning { get; private set; }
    private Vector3 _oldCameraPosition;
    private Vector3 _newCameraPosition;
    private float _cameraTransitionTime;
    private float _cameraCurrentTransitionTime;

    private StrategicTileType _queuedTiletype;
    private Village _queuedVillage;
    private Army _queuedInvader;
    private Army _queuedDefender;
    internal bool QueuedTactical;

    private float _remainingCameraTime;

    internal bool ActiveInput;

    public enum PreviewSkip
    {
        Watch,
        SkipWithStats,
        SkipNoStats
    }

    internal PreviewSkip CurrentPreviewSkip;


    public static CustomManager CustomManager;

    // Helper function for getting the command line arguments
    private static string GetArg(string name)
    {
        var args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == name && args.Length > i + 1)
            {
                return args[i + 1];
            }
        }
        return null;
    }
    
    // Helper function for getting the command line arguments
    private static bool HasFlag(string name)
    {
        var args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == name)
            {
                return true;
            }
        }
        return false;
    }
    
    private void Start()
    {
        State.CarefulIntatntiate();
        
        // Improved player preference library 
        FBPP.Start(new FBPPConfig()
        {
            SaveFileName = "contentsettings.json",
            AutoSaveData = false,
            ScrambleSaveData = false,
            //EncryptionSecret = "my-secret",
            SaveFilePath = "UserData"
        });
        
        StartMode.UI.SetActive(true);
        _currentScene = StartMode;
        State.GameManager = this;
        Application.wantsToQuit += () => WantsToQuit();

        //State.SpriteManager = new SpriteManager();
        //State.SpriteManager.Process2();

        bool developerMode = HasFlag("--dev-mode");
        
        #if UNITY_EDITOR == true
            developerMode = true;
        #endif
        
        CustomManager = new CustomManager(developerMode);
        CustomManager.LoadAllCustom();
        Race2.LoadHardcodedRaces();
        SeliciaMod.ModAll();
    }

    private void Quit()
    {
#if UNITY_EDITOR == false
        confirmedQuit = true;
#endif
        Application.Quit();
    }

    private bool WantsToQuit()
    {
#if UNITY_EDITOR
        {
            return true;
        }
#else
        var box = Instantiate(DialogBoxPrefab).GetComponent<DialogBox>();
        box.SetData(() => Quit(), "Quit Game", "Cancel", "Are you sure you want to quit?");
        return confirmedQuit;
#endif
    }

    //private void CopyFilesOfType(string extension, string destination)
    //{
    //    string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());

    //    foreach (string file in files)
    //    {
    //        if (!File.Exists(file)) return;

    //        if (file.EndsWith("." + extension))
    //        {
    //            string fileName = Path.GetFileName(file);
    //            File.Move(file, destination + fileName);

    //        }
    //    }
    //}

    /// <summary>
    ///     must be manually initialized
    /// </summary>
    public DialogBox CreateDialogBox()
    {
        return Instantiate(DialogBoxPrefab).GetComponent<DialogBox>();
    }

    public OptionsBox CreateOptionsBox()
    {
        return Instantiate(OptionsBoxPrefab).GetComponent<OptionsBox>();
    }

    public void CreateMessageBox(string text, int timedLife = 0)
    {
        MessageBox box = Instantiate(MessageBoxPrefab).GetComponent<MessageBox>();
        box.InfoText.text = text;
        if (timedLife > 0)
        {
            box.gameObject.AddComponent<TimedLife>();
            var timer = box.GetComponent<TimedLife>();
            timer.Life = timedLife;
        }
    }

    public void CreateFullScreenMessageBox(string text)
    {
        MessageBox box = Instantiate(FullScreenMessageBoxPrefab).GetComponent<MessageBox>();
        box.InfoText.text = text;
    }

    public InputBox CreateInputBox()
    {
        InputBox box = Instantiate(InputBoxPrefab).GetComponent<InputBox>();
        return box;
    }

    public void OpenMenu()
    {
        Menu.UpdateButtons();
        Menu.UIPanel.SetActive(true);
        _menuOpen = true;
    }

    public void CloseMenu()
    {
        Menu.UIPanel.SetActive(false);
        _menuOpen = false;
        if (_currentScene == StrategyMode) StrategyMode.ButtonCallback(70);
    }

    public void CloseStatsScreen()
    {
        StatScreen.AutoClose = false;
        StatScreen.gameObject.SetActive(false);
        SwitchToStrategyMode();
    }

    public void CenterCameraOnTile(int x, int y)
    {
        Camera.transform.position = new Vector3(x, y, Camera.transform.position.z);
    }

    public void SlideCameraToTile(int x, int y)
    {
        _oldCameraPosition = Camera.transform.position;
        _newCameraPosition = new Vector3(x, y, Camera.transform.position.z);
        float distance = Vector3.Distance(_oldCameraPosition, _newCameraPosition);
        _cameraTransitionTime = .2f + 0.1f * Mathf.Sqrt(distance);
        _cameraCurrentTransitionTime = 0;
        CameraTransitioning = true;
    }

    public void SwitchToMainMenu()
    {
        PureTactical = false;
        CurrentScene.CleanUp();
        CurrentScene = StartMode;
        StartMode.ReturnToStart();
        State.GameManager.StrategyMode.CleanUpLingeringWindows();
    }

    private void Update()
    {
        if (ActiveInput) return;
        if (_remainingCameraTime > 0) _remainingCameraTime -= Time.deltaTime;
        if (_remainingCameraTime <= 0) CornerCameraView.gameObject.SetActive(false);
        if (CameraTransitioning) UpdateSlidingCamera();
        if (_menuOpen)
            Menu.UpdateDisplay();
        else if (CurrentScene != null) CurrentScene.ReceiveInput();
        if (State.TutorialMode) TutorialScript.CheckStatus();
    }

    private void UpdateSlidingCamera()
    {
        _cameraCurrentTransitionTime += Time.deltaTime;
        Camera.transform.position = Vector3.Lerp(_oldCameraPosition, _newCameraPosition, _cameraCurrentTransitionTime / _cameraTransitionTime);
        if (_cameraCurrentTransitionTime > _cameraTransitionTime)
        {
            CameraTransitioning = false;
            ActivateTacticalMode(_queuedTiletype, _queuedVillage, _queuedInvader, _queuedDefender);
        }
    }

    internal void CameraCall(Vector3 location)
    {
        if ((Config.TacticalCenterCameraOnAction && CurrentScene == TacticalMode) || (Config.StrategicCenterCameraOnAction && CurrentScene == StrategyMode))
        {
            Camera.transform.position = location;
        }
        else if ((Config.TacticalCameraActionPanel && CurrentScene == TacticalMode) || (Config.StrategicCameraActionPanel && CurrentScene == StrategyMode))
        {
            CornerCameraView.gameObject.SetActive(true);
            PipCamera.SetLocation(location, 5);
            _remainingCameraTime = 1;
        }
    }

    internal void CameraCall(Vec2I location) => CameraCall(new Vector3(location.X, location.Y, 0));

    public void SwitchToStrategyMode(bool initialLoad = false)
    {
        if (PureTactical)
        {
            CurrentScene.CleanUp();
            PureTactical = false;
            SwitchToMainMenu();
            return;
        }

        bool needsCameraRefresh = _currentScene == StartMode || _currentScene == TacticalMode;
        StrategyMode.gameObject.SetActive(true);
        CurrentScene.CleanUp();
        CurrentScene = StrategyMode;
        StrategyMode.UndoMoves.Clear();
        StrategyMode.Regenerate(initialLoad);
        if (needsCameraRefresh) CameraController.LoadStrategicCamera();
        QueuedTactical = false;
    }

    public void SwitchToMapEditor()
    {
        if (_currentScene == StrategyMode)
        {
            CloseMenu();
            StrategyMode.ClearGraphics();
            MapEditor.gameObject.SetActive(true);
            MapEditor.Initialize(true);
            CurrentScene = MapEditor;
        }
        else if (_currentScene == StartMode)
        {
            StrategyMode.ClearGraphics();
            MapEditor.gameObject.SetActive(true);
            MapEditor.Initialize(false);
            CurrentScene = MapEditor;
        }
        else
        {
            CreateMessageBox("Can't open map editor from here...");
        }

        return;
    }

    public void ActivatePureTacticalMode(StrategicTileType tiletype, Village village, Army invader, Army defender, TacticalAIType attackerAI, TacticalAIType defenderAI)
    {
        PureTactical = true;
        CurrentScene = TacticalMode;
        TacticalMode.Begin(tiletype, village, invader, defender, attackerAI, defenderAI);
        if (_currentScene == TacticalMode)
        {
            CenterCameraOnTile((int)(Config.TacticalSizeX * .5f), (int)(Config.TacticalSizeY * .5f));
            CameraController.SetZoom(Config.TacticalSizeX * .5f);
        }
    }

    public void ActivateTacticalWithDelay(StrategicTileType tiletype, Village village, Army invader, Army defender)
    {
        if (Config.BattleReport && State.GameManager.StrategyMode.IsPlayerTurn == false)
        {
            BattleReportPanel.Activate(village, invader, defender);
            StrategicControlsLocked = true;
            QueuedTactical = true;
            _queuedTiletype = tiletype;
            _queuedVillage = village;
            _queuedInvader = invader;
            _queuedDefender = defender;
        }
        else if (Config.ScrollToBattleLocation)
        {
            SlideCameraToTile(invader.Position.X, invader.Position.Y);
            QueuedTactical = true;
            StrategicControlsLocked = true;
            _queuedTiletype = tiletype;
            _queuedVillage = village;
            _queuedInvader = invader;
            _queuedDefender = defender;
        }
        else
            ActivateTacticalMode(tiletype, village, invader, defender);
    }


    public void ActivateQueuedTacticalMode(PreviewSkip skipType)
    {
        CurrentPreviewSkip = skipType;
        TacticalBattleOverride battleOverride = skipType == PreviewSkip.Watch ? TacticalBattleOverride.ForceWatch : TacticalBattleOverride.ForceSkip;
        ActivateTacticalMode(_queuedTiletype, _queuedVillage, _queuedInvader, _queuedDefender, battleOverride);
        BattleReportPanel.gameObject.SetActive(false);
    }


    public void ActivateTacticalMode(StrategicTileType tiletype, Village village, Army invader, Army defender, TacticalBattleOverride tacticalBattleOverride = TacticalBattleOverride.Ignore)
    {
        CameraController.SaveStrategicCamera();
        StrategyMode.gameObject.SetActive(false);
        Side defenderSide = defender?.Side ?? village.Side;
        CurrentScene = TacticalMode;

        //If a human is either the army or the garrison, it gets to control both.
        Empire defenderEmpire = State.World.GetEmpireOfSide(defenderSide);
        TacticalAIType defenderType = defenderEmpire?.TacticalAIType ?? TacticalAIType.Full;
        if (village != null && State.World.GetEmpireOfSide(village.Side)?.TacticalAIType == TacticalAIType.None) defenderType = TacticalAIType.None;

        Empire attackerEmpire = State.World.GetEmpireOfSide(invader.Side);
        if (State.World.Relations != null)
        {
            if (village != null)
                RelationsManager.VillageAttacked(invader.EmpireOutside, village.Empire);
            else
                RelationsManager.ArmyAttacked(invader.EmpireOutside, defender.EmpireOutside);
            if (village != null && defender != null && !Equals(defender.Side, village.Side))
            {
                RelationsManager.ArmyAttacked(invader.EmpireOutside, defender.EmpireOutside);
            }
        }


        TacticalMode.ClearNames();
        TacticalMode.Begin(tiletype, village, invader, defender, attackerEmpire?.TacticalAIType ?? TacticalAIType.Full, defenderType, tacticalBattleOverride);
        StrategicControlsLocked = false;
        if (_currentScene == TacticalMode)
        {
            CenterCameraOnTile((int)(Config.TacticalSizeX * .5f), (int)(Config.TacticalSizeY * .5f));
            CameraController.SetZoom(Config.TacticalSizeX * .5f);
        }
    }

    public void SwitchToTacticalOnLoadedGame()
    {
        StrategyMode.gameObject.SetActive(false);
        CurrentScene = TacticalMode;
        PureTactical = State.World.MainEmpires == null;
        CameraController.LoadTacticalCamera();
    }

    public void ActivateRecruitMode(Village village, Empire empire, RecruitMode.ActivatingEmpire activatingEmpire = RecruitMode.ActivatingEmpire.Self)
    {
        RecruitMode.Begin(village, empire, activatingEmpire);
        CurrentScene = RecruitMode;
    }

    public void ActivateRecruitMode(Empire actingEmpire, Army army, RecruitMode.ActivatingEmpire activatingEmpire = RecruitMode.ActivatingEmpire.Self)
    {
        RecruitMode.BeginWithoutVillage(actingEmpire, army, activatingEmpire);
        CurrentScene = RecruitMode;
    }

    public void ActivateRecruitMode(MercenaryHouse house, Empire actingEmpire, Army army, RecruitMode.ActivatingEmpire activatingEmpire = RecruitMode.ActivatingEmpire.Self)
    {
        RecruitMode.BeginWithMercenaries(house, actingEmpire, army, activatingEmpire);
        CurrentScene = RecruitMode;
    }

    public void ActivateEndSceneWin(Side side)
    {
        var winners = State.World.MainEmpires.Where(s => s.IsAlly(State.World.GetEmpireOfSide(side)));
        string winner = "";
        bool first = true;
        foreach (Empire emp in winners)
        {
            if (first)
                winner += $"{emp.Name}";
            else
                winner += $", {emp.Name}";
            first = false;
        }

        EndScene.Win(winner);
        CurrentScene = EndScene;
    }

    public void ActivateEndSceneLose()
    {
        EndScene.Lose(State.World.ActingEmpire.Race);
        CurrentScene = EndScene;
    }

    public void OpenSaveLoadMenu() => Menu.OpenSaveLoadMenu();

    public void AskQuickLoad()
    {
        if (FindObjectOfType<DialogBox>() != null) return;
        DialogBox box = Instantiate(DialogBoxPrefab).GetComponent<DialogBox>();
        box.SetData(DoQuickLoad, "Load Game", "Cancel", "Are you sure you want to quickload?");
    }

    private void DoQuickLoad()
    {
        StatScreen.gameObject.SetActive(false);
        State.Load($"{State.SaveDirectory}Quicksave.sav");
    }

    public void ClearPureTactical() => PureTactical = false;
}