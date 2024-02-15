using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class RecruitMode : SceneBase
{
    private Unit _leader;
    private Village _village;
    private MercenaryHouse _mercenaryHouse;
    private Army _army;
    private Empire _empire;
    internal UnitCustomizer Customizer;
    private Shop _shop;
    private VillageView _villageView;

    private List<Unit> _dismissList;

    private int _selectedIndex;

    private ActivatingEmpire _activatingEmpire;

    private InfoPanel _infoPanel;

    public RecruitPanel RecruitUI;
    public ArmySectionsPanel ArmyUI;
    public ShopPanel ShopUI;
    public RenamePanel RenameUI;
    public LevelUpPanel LevelUpUI;
    public HirePanel HireUI;
    public MercenaryPanel MercenaryPanelUI;
    public MercenaryHousePanel MercenaryScreenUI;
    public CustomizerPanel CustomizerUI;
    public VillageViewPanel VillageUI;
    public GameObject BlockerUI;
    public BulkBuyPanel BulkBuyUI;
    public WeaponStocker WeaponStockerUI;
    public ConfigAutoLevelUpPanel ConfigAutoLevelUpUI;
    public RecruitUI RaceUI;
    public RecruitUI PopUI;

    public RecruitCheatsPanel CheatMenu;

    public Button ShowBanner;
    public TMP_Dropdown BannerType;

    private ActorUnit[] _displayUnits;
    private Unit[] _displayCreatedUnit;

    private bool _failedToMakeFriendlyArmy = false;

    public enum ActivatingEmpire
    {
        Self,
        Ally,
        Observer,
        Infiltrator
    }

    public void BeginWithoutVillage(Empire actingEmpire, Army startArmy, ActivatingEmpire activatingEmpire)
    {
        this._activatingEmpire = activatingEmpire;
        _army = startArmy;
        InitializeBanners();
        _village = null;
        _mercenaryHouse = null;
        _empire = actingEmpire;
        ArmyUI.ArmyName.text = _army.Name;
        RecruitUI.gameObject.SetActive(false);
        MercenaryPanelUI.gameObject.SetActive(false);
        _leader = _army.LeaderIfInArmy();
        ArmyUI.AlliedArmyText.gameObject.SetActive(false);
        ArmyUI.ShopText.text = "Inventory";
        SetUpDisplay();
        _dismissList = new List<Unit>();
    }

    public void BeginWithMercenaries(MercenaryHouse merc, Empire actingEmpire, Army startArmy, ActivatingEmpire activatingEmpire)
    {
        _mercenaryHouse = merc;
        this._activatingEmpire = activatingEmpire;
        _army = startArmy;
        ArmyUI.ArmyName.text = _army.Name;
        InitializeBanners();
        _village = null;
        _empire = actingEmpire;
        RecruitUI.gameObject.SetActive(false);
        MercenaryPanelUI.gameObject.SetActive(true);
        MercenaryPanelUI.SpecialMercenariesButton.gameObject.SetActive(MercenaryHouse.UniqueMercs.Any());
        _leader = _army.LeaderIfInArmy();
        ArmyUI.AlliedArmyText.gameObject.SetActive(false);
        ArmyUI.ShopText.text = "Inventory";
        SetUpDisplay();
        _dismissList = new List<Unit>();
    }

    public void Begin(Village village, Empire empire, ActivatingEmpire activatingEmpire)
    {
        this._activatingEmpire = activatingEmpire;
        this._village = village;
        this._empire = empire;
        _failedToMakeFriendlyArmy = false;
        _mercenaryHouse = null;
        InitializeBanners();
        SetArmy();
        ArmyUI.RecruitSoldier.text = "Recruit Soldier (" + Config.ArmyCost + "G)";
        ArmyUI.ShopText.text = "Shop";
        RecruitUI.TownName.text = this._village.Name;
        ArmyUI.ArmyName.text = _army?.Name ?? "";
        RecruitUI.gameObject.SetActive(true);
        MercenaryPanelUI.gameObject.SetActive(false);
        _leader = _army?.LeaderIfInArmy();
        if (_army != null)
            SetUpDisplay();
        else
            RecruitUI.AddUnit.gameObject.SetActive(Config.CheatAddUnitButton);
        GenText();
    }

    private void SetArmy()
    {
        _army = null;
        //find an army
        ArmyUI.AlliedArmyText.gameObject.SetActive(false);

        for (int i = 0; i < _empire.Armies.Count; i++)
        {
            if (_empire.Armies[i].Position.Matches(_village.Position))
            {
                _army = _empire.Armies[i];
                break;
            }
        }

        if (_army == null)
        {
            if (StrategicUtilities.ArmyAt(_village.Position) == null)
            {
                if (_activatingEmpire != ActivatingEmpire.Observer && _empire.Armies.Count() < Config.MaxArmies)
                {
                    _army = new Army(_empire, new Vec2I(_village.Position.X, _village.Position.Y), _empire.Side);
                    _empire.Armies.Add(_army);
                    _army.HealRate = _village.Healrate();
                }
                else
                {
                    _activatingEmpire = ActivatingEmpire.Observer;
                    _failedToMakeFriendlyArmy = true;
                }
            }
            else
            {
                if (_activatingEmpire != ActivatingEmpire.Observer) ArmyUI.AlliedArmyText.gameObject.SetActive(true);
                foreach (Army armyCheck in StrategicUtilities.GetAllArmies())
                {
                    if (armyCheck.Position.Matches(_village.Position))
                    {
                        _army = armyCheck;
                        if (_army.EmpireOutside.IsEnemy(_village.Empire)) _activatingEmpire = ActivatingEmpire.Observer;
                        break;
                    }
                }
            }
        }

        BannerType.gameObject.SetActive(_army != null);
        InitializeBanners();
    }

    private void InitializeBanners()
    {
        if (_army != null)
        {
            if (BannerType.options.Count < 4)
            {
                foreach (BannerType type in (BannerType[])Enum.GetValues(typeof(BannerType)))
                {
                    BannerType.options.Add(new TMP_Dropdown.OptionData(type.ToString()));
                }

                for (int i = 0; i < CustomBannerTest.Sprites.Length; i++)
                {
                    if (CustomBannerTest.Sprites[i] == null) break;
                    BannerType.options.Add(new TMP_Dropdown.OptionData($"Custom {i + 1}"));
                }

                BannerType.onValueChanged.AddListener((s) => UpdateArmyBanner());
            }

            BannerType.value = _army.BannerStyle;
        }
    }

    private void UpdateArmyBanner()
    {
        if (_army == null) return;
        _army.BannerStyle = BannerType.value;
    }

    public void Select(int num)
    {
        _selectedIndex = num;
        UpdateUnitInfoPanel();
        RefreshUnitPanelButtons();
        if (ArmyUI.UnitInfoArea.Length == 0) return;
    }

    public void RefreshUnitPanelButtons()
    {
        bool validUnit = _army?.Units.Count > _selectedIndex && _selectedIndex != -1;
        Unit unit = null;
        if (_selectedIndex != -1 && _army?.Units.Count() > _selectedIndex) unit = _army?.Units[_selectedIndex];
        ArmyUI.Rename.interactable = validUnit && unit.Type != UnitType.SpecialMercenary;
        ArmyUI.Shop.interactable = _activatingEmpire < ActivatingEmpire.Observer && validUnit && unit != null && (unit.FixedGear == false || unit.HasTrait(TraitType.BookEater));
        var dismissText = ArmyUI.Dismiss.gameObject.GetComponentInChildren(typeof(Text)) as Text;

        if (unit != null && Equals(unit.FixedSide, _empire.Side) && unit.IsInfiltratingSide(unit.Side) && _activatingEmpire > ActivatingEmpire.Ally)
        {
            dismissText.text = "Exfiltrate";
            ArmyUI.Dismiss.interactable = State.GameManager.StrategyMode.IsPlayerTurn;
        }
        else
        {
            dismissText.text = "Dismiss";
            ArmyUI.Dismiss.interactable = _activatingEmpire < ActivatingEmpire.Observer && validUnit && unit != null && unit != _army?.EmpireOutside.Leader;
        }

        ArmyUI.ConfigAutoLevelUp.interactable = _activatingEmpire < ActivatingEmpire.Observer && validUnit;
        ArmyUI.Customizer.interactable = validUnit;
        if (_village != null) RecruitUI.ImprintUnit.interactable = validUnit && _activatingEmpire == ActivatingEmpire.Self && unit.Type != UnitType.SpecialMercenary && unit != _army?.EmpireOutside.Leader;
    }

    public void RefreshRecruitPanelButtons()
    {
        RecruitUI.CheapUpgrade.gameObject.SetActive(_activatingEmpire < ActivatingEmpire.Observer);
        RecruitUI.HireSoldier.gameObject.SetActive(_activatingEmpire < ActivatingEmpire.Observer);
        RecruitUI.HireVillageMerc.gameObject.SetActive(_activatingEmpire < ActivatingEmpire.Observer);
        RecruitUI.RecruitSoldier.gameObject.SetActive(_activatingEmpire < ActivatingEmpire.Observer);
        RecruitUI.StockWeapons.interactable = (_activatingEmpire < ActivatingEmpire.Observer || _failedToMakeFriendlyArmy) && _village.Empire == _empire;
        RecruitUI.CheapUpgrade.interactable = _activatingEmpire == ActivatingEmpire.Self && _army.Units.Count > 0;
        RecruitUI.RecruitSoldier.interactable = _activatingEmpire == ActivatingEmpire.Self && _village.GetTotalPop() > 3 && _army.Units.Count < _army.MaxSize;
        RecruitUI.HireSoldier.interactable = _activatingEmpire == ActivatingEmpire.Self && _village.GetRecruitables().Count > 0 && _village.GetTotalPop() > 3 && _army.Units.Count < _army.MaxSize;
        RecruitUI.HireVillageMerc.interactable = _activatingEmpire == ActivatingEmpire.Self && (_village.Mercenaries?.Count > 0 || _village.Adventurers?.Count > 0) && _army.Units.Count < _army.MaxSize;
        RecruitUI.VillageView.interactable = (_activatingEmpire < ActivatingEmpire.Observer || _failedToMakeFriendlyArmy) && _village.GetTotalPop() > 0 && _village.Empire == _empire;

        RecruitUI.ResurrectLeader.gameObject.SetActive(_activatingEmpire != ActivatingEmpire.Observer && _empire.Leader != null && _empire.Leader.Health <= 0);
    }


    public override void CleanUp()
    {
        if (_dismissList != null && _dismissList.Count > 0)
        {
            StrategicUtilities.ProcessTravelingUnits(_dismissList, _army);
            _dismissList.Clear();
        }

        _army = null;
        _selectedIndex = -1;
        for (int x = 0; x < Config.MaximumPossibleArmy; x++)
        {
            if (ArmyUI.UnitInfoArea.Length > x) ArmyUI.UnitInfoArea[x].gameObject.SetActive(false);
        }
    }

    public void Recruit()
    {
        if (_village != null && _village.VillagePopulation.Population.Count > 1 && _village.VillagePopulation.Population.Where(s => s.Population > s.Hireables).Count() > 1)
        {
            ButtonCallback(1);
        }
        else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            var unit = _village.RecruitPlayerUnit(_empire, _army);
            for (int i = 0; i < 48; i++)
            {
                if (unit != null)
                {
                    unit = _village.RecruitPlayerUnit(_empire, _army);
                }
                else
                    break;
            }

            UpdateActorList();
            GenText();
        }
        else
        {
            if (_village.RecruitPlayerUnit(_empire, _army) != null)
            {
                UpdateActorList();
                GenText();
            }
        }

        Select(_army.Units.Count - 1);
    }

    private void GenText()
    {
        RecruitUI.Gold.text = _empire.Gold + " gold ";

        if (_village.GetTotalPop() > 0)
        {
            RecruitUI.Population.text = _village.VillagePopulation.GetPopReport();
        }
        else
        {
            RecruitUI.Population.text = "Empty";
        }

        RecruitUI.DefenderCount.text = $"{_village.Garrison} / {_village.MaxGarrisonSize} defenders";
        RecruitUI.Income.text = _village.GetIncome() + " income";
        RefreshRecruitPanelButtons();
        RefreshUnitPanelButtons();
    }

    private void UpdateUnitInfoPanel()
    {
        if (_army?.Units.Count > _selectedIndex && _selectedIndex != -1)
        {
            _infoPanel.RefreshStrategicUnitInfo(_army.Units[_selectedIndex]);
            ArmyUI.LevelUp.interactable = _activatingEmpire < ActivatingEmpire.Observer && _army.Units[_selectedIndex].HasEnoughExpToLevelUp();
            ArmyUI.AutoLevelUp.interactable = _activatingEmpire < ActivatingEmpire.Observer && _army.Units.Where(s => s.HasEnoughExpToLevelUp()).Any();
        }
        else
        {
            ArmyUI.InfoPanel.InfoText.text = "";
            ArmyUI.LevelUp.interactable = false;
            ArmyUI.AutoLevelUp.interactable = false;
        }
    }

    public override void ReceiveInput()
    {
        if (WeaponStockerUI.gameObject.activeSelf)
        {
            UpdateWeaponStocker();
        }

        if (_selectedIndex == -1 || _selectedIndex >= _army?.Units.Count || ArmyUI.UnitInfoArea.Length == 0)
            ArmyUI.Selector.SetActive(false);
        else
        {
            ArmyUI.Selector.SetActive(true);
            ArmyUI.Selector.transform.position = ArmyUI.UnitInfoArea[_selectedIndex].transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (VillageUI.gameObject.activeSelf || CustomizerUI.gameObject.activeSelf || ShopUI.gameObject.activeSelf || BulkBuyUI.gameObject.activeSelf ||
                WeaponStockerUI.gameObject.activeSelf || ConfigAutoLevelUpUI.gameObject.activeSelf || HireUI.gameObject.activeSelf || RenameUI.gameObject.activeSelf)
                ButtonCallback(10);
            else if (MercenaryScreenUI.gameObject.activeSelf)
                ButtonCallback(81);
            else if (BlockerUI.activeSelf == false && MercenaryScreenUI.gameObject.activeSelf == false && HireUI.gameObject.activeSelf == false &&
                     CustomizerUI.gameObject.activeSelf == false && VillageUI.gameObject.activeSelf == false && VillageUI.gameObject.activeSelf == false && FindObjectOfType<DialogBox>() == false)
                State.GameManager.SwitchToStrategyMode();
        }


        if (Input.GetKeyDown(KeyCode.Escape) && CheatMenu.gameObject.activeSelf)
        {
            ButtonCallback(86);
        }
    }


    public void ButtonCallback(int id)
    {
        switch (id)
        {
            case 1:
                BlockerUI.SetActive(true);
                BuildRaceDisplay();
                break;
            case 2:
                BlockerUI.SetActive(true);
                BuildHiringView();
                break;
            case 3:
                BuildVillageView();
                break;
            case 4:
                BuildCustomizer();
                break;
            case 5:
                BlockerUI.SetActive(true);
                BuildRename();
                break;
            case 6:
                BlockerUI.SetActive(true);
                BuildLevelUp();
                break;
            case 7:
                Dismiss();
                break;
            case 8:
                State.GameManager.SwitchToStrategyMode();
                break;
            case 9:
                BlockerUI.SetActive(true);
                BuildShop();
                break;
            case 10:
                VillageUI.gameObject.SetActive(false);
                CustomizerUI.gameObject.SetActive(false);
                ShopUI.gameObject.SetActive(false);
                HireUI.gameObject.SetActive(false);
                BulkBuyUI.gameObject.SetActive(false);
                RenameUI.gameObject.SetActive(false);
                WeaponStockerUI.gameObject.SetActive(false);
                ConfigAutoLevelUpUI.gameObject.SetActive(false);
                BlockerUI.SetActive(false);
                _shop = null;
                if (_selectedIndex != -1 && _displayUnits?.Length > _selectedIndex && _displayUnits[_selectedIndex] != null) _displayUnits[_selectedIndex].UpdateBestWeapons();
                UpdateUnitInfoPanel();
                UpdateDrawnActors();
                if (_village != null) GenText();
                break;
            case 11:
                if (_army == null) return;
                if (_army.Units.Count <= _selectedIndex)
                {
                    break;
                }

                Unit unit = _army.Units[_selectedIndex];
                if (RenameUI.NewName.text.Length > 0)
                {
                    unit.Name = RenameUI.NewName.text;
                }

                RenameUI.gameObject.SetActive(false);
                BlockerUI.SetActive(false);
                UpdateUnitInfoPanel();
                UpdateDrawnActors();
                break;
            case 12:
                RenameUI.gameObject.SetActive(false);
                BlockerUI.SetActive(false);
                break;
            case 13:
                HireUI.gameObject.SetActive(false);
                BlockerUI.SetActive(false);
                break;
            case 14:
                BulkBuyUI.gameObject.SetActive(true);
                BlockerUI.SetActive(true);
                RefreshBulkBuy();
                break;
            case 15:
                WeaponStockerUI.gameObject.SetActive(true);
                BlockerUI.SetActive(true);
                BuildWeaponStocker();
                break;
            case 16:
                if (_army == null) return;
                if (_army.Units.Count <= _selectedIndex)
                {
                    break;
                }

                ConfigAutoLevelUpUI.gameObject.SetActive(true);
                ConfigAutoLevelUpUI.Open(_army.Units[_selectedIndex]);
                BlockerUI.SetActive(true);
                break;
            case 17:
                if (_selectedIndex != -1 && _displayUnits?.Length > _selectedIndex && _displayUnits[_selectedIndex] != null) BuildClone(_displayUnits[_selectedIndex].Unit);
                break;
            case 18:
                RaceUI.gameObject.SetActive(false);
                BlockerUI.SetActive(false);
                break;
            case 19:
                if (Config.CheatPopulation && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
                {
                    var box1 = State.GameManager.CreateInputBox();
                    box1.SetData(SetPopulation, "Set new population", "Cancel change", "Cheat to set the village population?  (In multi-race villages, lowering kills randomly, and raising acts like breeding)", 5);

                    break;
                }

                SetUpPopUI();
                BlockerUI.SetActive(true);
                break;

            case 60:
                DialogBox box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
                box.SetData(AutoLevelUp, "Spend them!", "Cancel", "This will spend all remaining level ups for this army.   It may not do as good a job as manually picking, and it may not pick exactly what you want, but it is fast.");
                break;
            case 80:
                BuildMercenaryView(false);
                break;
            case 81:
                MercenaryScreenUI.gameObject.SetActive(false);
                break;
            case 82:
                BuildMercenaryView(true);
                break;
            case 83:
                BuildVillageMercenaryView();
                break;
            case 84:
                PopUI.gameObject.SetActive(false);
                BlockerUI.SetActive(false);
                break;
            case 85:
                if (_army == null)
                {
                    State.GameManager.CreateMessageBox("Can't enter this screen without an army");
                    break;
                }

                CheatMenu.gameObject.SetActive(true);
                CheatMenu.Setup(_army);
                BlockerUI.SetActive(true);
                break;
            case 86:
                CheatMenu.gameObject.SetActive(false);
                BlockerUI.SetActive(false);
                break;
            case 2001:
                BlockerUI.SetActive(true);
                BuildHiringView("STR");
                break;
            case 2002:
                BlockerUI.SetActive(true);
                BuildHiringView("DEX");
                break;
            case 2003:
                BlockerUI.SetActive(true);
                BuildHiringView("MND");
                break;
            case 2004:
                BlockerUI.SetActive(true);
                BuildHiringView("WLL");
                break;
            case 2005:
                BlockerUI.SetActive(true);
                BuildHiringView("END");
                break;
            case 2006:
                BlockerUI.SetActive(true);
                BuildHiringView("AGI");
                break;
            case 2007:
                BlockerUI.SetActive(true);
                BuildHiringView("VOR");
                break;
            case 2008:
                BlockerUI.SetActive(true);
                BuildHiringView("STM");
                break;
            case 4000:
                BannerType.gameObject.SetActive(true);
                ShowBanner.gameObject.SetActive(false);
                break;
            case 4001:
                CheatAddUnit();
                break;
        }

        if (id > 19 && id < 30)
        {
            if (_army == null) return;
            if (_army.Units.Count <= _selectedIndex)
            {
                return;
            }

            Unit unit = _army.Units[_selectedIndex];
            unit.LevelUp((Stat)id - 20);
            LevelUpUI.gameObject.SetActive(false);
            BlockerUI.SetActive(false);
            UpdateUnitInfoPanel();
            UpdateDrawnActors();
        }

        if (id == 70)
        {
            if (_army == null) return;
            if (_army.Units.Count == _empire.MaxArmySize)
            {
                State.GameManager.CreateMessageBox("Army is already maximum size");
                return;
            }

            if (_empire.Gold < 100)
            {
                State.GameManager.CreateMessageBox("You need at least 100 gold to resurrect the leader");
                return;
            }

            DialogBox box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
            box.SetData(ResurrectLeader, "Yes", "No", "Resurrect Leader at this town for 100 gold?");
        }
    }

    private void SetPopulation(int p)
    {
        _village.SetPopulation(p);
        GenText();
    }

    private void SetUpPopUI()
    {
        int children = PopUI.ActorFolder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(PopUI.ActorFolder.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < _village.VillagePopulation.Population.Count; i++)
        {
            if (_village.VillagePopulation.Population[i].Population > 0)
            {
                GameObject obj = Instantiate(PopUI.RecruitPanel, PopUI.ActorFolder);
                UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
                // Side was 1 for Unit
                ActorUnit actor = new ActorUnit(new Vec2I(0, 0), new Unit(Race.Dog.ToSide(), _village.VillagePopulation.Population[i].Race, 0, true));
                TextMeshProUGUI text = obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
                var racePar = RaceParameters.GetTraitData(actor.Unit);
                text.text = $"{_village.VillagePopulation.Population[i].Race}\nTotal: {_village.VillagePopulation.Population[i].Population}\nFavored Stat: {State.RaceSettings.GetFavoredStat(actor.Unit.Race)}\nDefault Traits:\n{State.RaceSettings.ListTraits(actor.Unit.Race)}";
                sprite.UpdateSprites(actor);
            }
        }

        PopUI.gameObject.SetActive(true);
    }

    internal void RenameVillage(string name)
    {
        if (_village != null) _village.Name = name;
        RecruitUI.TownName.text = _village.Name;
    }

    internal void RenameArmy(string name)
    {
        if (_army != null) _army.Name = name;
        ArmyUI.ArmyName.text = _army.Name;
    }

    internal void BuildClone(Unit unit)
    {
        int baseXp = _village.GetStartingXp();
        int effectiveXp = Math.Max((int)(unit.Experience - baseXp), 10);
        int diff = Math.Max((int)(effectiveXp - (unit.SavedCopy?.Experience ?? 0)), 10);
        int cost = 20 + (int)(effectiveXp * 0.1f + 0.2f * diff);

        string previous = "";

        if (unit.SavedCopy != null)
        {
            previous = $"Previous imprint --  Level: {unit.SavedCopy.Level}  Exp: {unit.SavedCopy.Experience}";
        }
        else
        {
            previous = "This unit has no saved imprint";
        }

        var box = State.GameManager.CreateDialogBox();
        box.SetData(() => MakeClone(unit, cost), "Imprint", "Cancel", $"Imprint this soul? (Costs {cost})\nAllows unit to respawn with saved stats at this location if it dies." +
                                                                      $"\nCost is based on experience of other active imprint, total experience, and is reduced by the innate exp for this town\n{previous}");
    }

    private void MakeClone(Unit unit, int cost)
    {
        if (_empire.Gold < cost) return;
        _empire.SpendGold(cost);
        var clonedUnit = unit.Clone();
        unit.SavedCopy = clonedUnit;
        unit.SavedVillage = _village;
        GenText();
    }

    private void ResurrectLeader()
    {
        _empire.SpendGold(100);
        _empire.Leader.LeaderLevelDown();
        _empire.Leader.Health = _empire.Leader.MaxHealth;
        _empire.Leader.FixedSide = _empire.Side;
        _empire.Leader.Type = UnitType.Leader;
        if (_village.GetStartingXp() > _empire.Leader.Experience)
        {
            _empire.Leader.SetExp(_village.GetStartingXp());
        }

        State.World.Stats.ResurrectedLeader(_empire.Side);
        _army.Units.Add(_empire.Leader);
        if (Config.LeadersRerandomizeOnDeath)
        {
            _empire.Leader.TotalRandomizeAppearance();
            _empire.Leader.ReloadTraits();
            _empire.Leader.InitializeTraits();
        }

        UpdateActorList();
        GenText();
    }

    private void BuildRename()
    {
        if (_army.Units.Count > _selectedIndex)
        {
            RenameUI.gameObject.SetActive(true);
        }
    }

    private void BuildWeaponStocker()
    {
        WeaponStockerUI.BuyMace.onClick.RemoveAllListeners();
        WeaponStockerUI.BuyAxe.onClick.RemoveAllListeners();
        WeaponStockerUI.BuyBow.onClick.RemoveAllListeners();
        WeaponStockerUI.BuyCompoundBow.onClick.RemoveAllListeners();
        WeaponStockerUI.SellMace.onClick.RemoveAllListeners();
        WeaponStockerUI.SellAxe.onClick.RemoveAllListeners();
        WeaponStockerUI.SellBow.onClick.RemoveAllListeners();
        WeaponStockerUI.SellCompoundBow.onClick.RemoveAllListeners();
        WeaponStockerUI.BuyMace.onClick.AddListener(() => _village.BuyWeaponPotentiallyBulk(ItemType.Mace, _empire));
        WeaponStockerUI.BuyAxe.onClick.AddListener(() => _village.BuyWeaponPotentiallyBulk(ItemType.Axe, _empire));
        WeaponStockerUI.BuyBow.onClick.AddListener(() => _village.BuyWeaponPotentiallyBulk(ItemType.Bow, _empire));
        WeaponStockerUI.BuyCompoundBow.onClick.AddListener(() => _village.BuyWeaponPotentiallyBulk(ItemType.CompoundBow, _empire));
        WeaponStockerUI.SellMace.onClick.AddListener(() => _village.SellWeaponPotentiallyBulk(ItemType.Mace, _empire));
        WeaponStockerUI.SellAxe.onClick.AddListener(() => _village.SellWeaponPotentiallyBulk(ItemType.Axe, _empire));
        WeaponStockerUI.SellBow.onClick.AddListener(() => _village.SellWeaponPotentiallyBulk(ItemType.Bow, _empire));
        WeaponStockerUI.SellCompoundBow.onClick.AddListener(() => _village.SellWeaponPotentiallyBulk(ItemType.CompoundBow, _empire));
        UpdateWeaponStocker();
    }

    private void UpdateWeaponStocker()
    {
        int maces = _village.Weapons.Where(s => s == ItemType.Mace).Count();
        int axes = _village.Weapons.Where(s => s == ItemType.Axe).Count();
        int bows = _village.Weapons.Where(s => s == ItemType.Bow).Count();
        int compoundBows = _village.Weapons.Where(s => s == ItemType.CompoundBow).Count();
        WeaponStockerUI.Maces.text = $"Maces: {maces}";
        WeaponStockerUI.Axes.text = $"Axes: {axes}";
        WeaponStockerUI.Bows.text = $"Bows: {bows}";
        WeaponStockerUI.CompoundBows.text = $"Compound Bows: {compoundBows}";
        WeaponStockerUI.SellMace.interactable = maces > 0;
        WeaponStockerUI.SellAxe.interactable = axes > 0;
        WeaponStockerUI.SellBow.interactable = bows > 0;
        WeaponStockerUI.SellCompoundBow.interactable = compoundBows > 0;
        WeaponStockerUI.RemainingGold.text = $"Remaining Gold: {_empire.Gold}";
    }

    public void FinalizeAutoLevelUI(bool copy)
    {
        ButtonCallback(10);
        ApplyTo(_army.Units[_selectedIndex]);

        if (copy)
        {
            foreach (ActorUnit actor in _displayUnits)
            {
                if (actor != null) ApplyTo(actor.Unit);
            }
        }


        void ApplyTo(Unit unit)
        {
            if (ConfigAutoLevelUpUI.Custom)
            {
                unit.AIClass = AIClass.Custom;
                unit.StatWeights = new StatWeights()
                {
                    Weight = new float[(int)Stat.None]
                    {
                        ConfigAutoLevelUpUI.Sliders[0].value,
                        ConfigAutoLevelUpUI.Sliders[1].value,
                        ConfigAutoLevelUpUI.Sliders[2].value,
                        ConfigAutoLevelUpUI.Sliders[3].value,
                        ConfigAutoLevelUpUI.Sliders[4].value,
                        ConfigAutoLevelUpUI.Sliders[5].value,
                        ConfigAutoLevelUpUI.Sliders[6].value,
                        ConfigAutoLevelUpUI.Sliders[7].value,
                        ConfigAutoLevelUpUI.Sliders[8].value,
                    }
                };
            }

            unit.AutoLeveling = ConfigAutoLevelUpUI.AutoSpend.isOn;
            unit.AIClass = (AIClass)ConfigAutoLevelUpUI.Dropdown.value;
        }
    }

    private void BuildLevelUp()
    {
        if (_army == null)
        {
            BlockerUI.SetActive(false);
            return;
        }

        if (_army.Units.Count > _selectedIndex && _selectedIndex != -1)
        {
            if (_army.Units[_selectedIndex].HasEnoughExpToLevelUp())
            {
                LevelUpUI.gameObject.SetActive(true);
                BuildLvlButtons();
            }
        }
        else
        {
            BlockerUI.SetActive(false);
            return;
        }
    }

    private void BuildLvlButtons()
    {
        Stat[] r = _army.Units[_selectedIndex].GetLevelUpPossibilities(_army.Units[_selectedIndex].Predator);
        for (int i = 0; i < LevelUpUI.StatButtons.Length; i++)
        {
            LevelUpUI.StatButtons[i].gameObject.SetActive(i < r.Length);
        }

        for (int i = 0; i < r.Length; i++)
        {
            int statInt = (int)r[i];
            Stat stat = r[i];
            int currentValue = _army.Units[_selectedIndex].GetStatBase(stat);
            int increase = 5;
            LevelUpUI.StatButtons[i].onClick.RemoveAllListeners();
            LevelUpUI.StatButtons[i].GetComponentInChildren<Text>().text = $"{stat} ({currentValue} -> {currentValue + increase})";
            LevelUpUI.StatButtons[i].onClick.AddListener(() =>
            {
                ButtonCallback(20 + statInt);

                if (LevelUpUI.KeepOpen.isOn && _army.Units[_selectedIndex].HasEnoughExpToLevelUp())
                {
                    ButtonCallback(6);
                }
                else
                    LevelUpUI.gameObject.SetActive(false);
            });
        }
    }

    public void RefreshBulkBuy()
    {
        int cheapCost = CheapFitCost();
        int expensiveCost = ExpensiveFitCost();
        int accessoryCost = AccessoryFitCost();
        BulkBuyUI.BuyCheap.interactable = _empire.Gold >= cheapCost && cheapCost > 0;
        BulkBuyUI.BuyExpensive.interactable = _empire.Gold >= expensiveCost && expensiveCost > 0;
        BulkBuyUI.BuyAccessories.interactable = _empire.Gold >= accessoryCost && accessoryCost > 0;
        BulkBuyUI.BuyCheap.GetComponentInChildren<Text>().text = $"Purchase Basic Weapons\nCost : {cheapCost} gold";
        BulkBuyUI.BuyExpensive.GetComponentInChildren<Text>().text = $"Purchase Advanced Weapons\nCost : {expensiveCost} gold";
        BulkBuyUI.BuyAccessories.GetComponentInChildren<Text>().text = $"Purchase Accessories\nCost : {accessoryCost} gold\nIt only buys for units with weapons\nIt might have different priorities than you";
    }

    private void AutoLevelUp()
    {
        if (_army?.Units == null) return;
        foreach (Unit unit in _army.Units)
        {
            StrategicUtilities.SpendLevelUps(unit);
        }

        UpdateUnitInfoPanel();
        UpdateDrawnActors();
    }


    private int CheapFitCost()
    {
        int cost = 0;
        foreach (ActorUnit actor in _displayUnits)
        {
            if (actor != null)
            {
                if (actor.Unit.HasFreeItemSlot() == false || actor.Unit.FixedGear || (actor.Unit.Items[0]?.LockedItem ?? false)) continue;
                if (actor.Unit.GetBestMelee().Damage == 2 && actor.Unit.GetBestRanged() == null)
                {
                    if (actor.Unit.BestSuitedForRanged())
                        cost += State.World.ItemRepository.GetItem(ItemType.Bow).Cost;
                    else
                        cost += State.World.ItemRepository.GetItem(ItemType.Mace).Cost;
                }
            }
        }

        return cost;
    }

    public void CheapFit()
    {
        foreach (ActorUnit actor in _displayUnits)
        {
            if (actor != null)
            {
                if (actor.Unit.HasFreeItemSlot() == false || actor.Unit.FixedGear || (actor.Unit.Items[0]?.LockedItem ?? false)) continue;
                if (actor.Unit.GetBestMelee().Damage == 2 && actor.Unit.GetBestRanged() == null)
                {
                    if (actor.Unit.BestSuitedForRanged())
                        Shop.BuyItem(_empire, actor.Unit, State.World.ItemRepository.GetItem(ItemType.Bow));
                    else
                        Shop.BuyItem(_empire, actor.Unit, State.World.ItemRepository.GetItem(ItemType.Mace));
                    actor.UpdateBestWeapons();
                }
            }
        }

        UpdateDrawnActors();
        UpdateUnitInfoPanel();
        GenText();
    }

    private int ExpensiveFitCost()
    {
        int cost = 0;
        foreach (ActorUnit actor in _displayUnits)
        {
            if (actor != null)
            {
                if ((actor.Unit.HasFreeItemSlot() || actor.Unit.HasSpecificWeapon(ItemType.Bow, ItemType.Mace)) == false || actor.Unit.FixedGear || (actor.Unit.Items[0]?.LockedItem ?? false)) continue;
                if (actor.Unit.GetBestMelee().Damage == 2 && actor.Unit.GetBestRanged() == null)
                {
                    if (actor.Unit.BestSuitedForRanged())
                        cost += State.World.ItemRepository.GetItem(ItemType.CompoundBow).Cost;
                    else
                        cost += State.World.ItemRepository.GetItem(ItemType.Axe).Cost;
                }
                else if (BulkBuyUI.SellAndBuy.isOn)
                {
                    if (actor.Unit.GetBestMelee() == State.World.ItemRepository.GetItem(ItemType.Mace))
                    {
                        cost -= State.World.ItemRepository.GetItem(ItemType.Mace).Cost / 2;
                        cost += State.World.ItemRepository.GetItem(ItemType.Axe).Cost;
                    }

                    if (actor.Unit.GetBestRanged() == State.World.ItemRepository.GetItem(ItemType.Bow))
                    {
                        cost -= State.World.ItemRepository.GetItem(ItemType.Bow).Cost / 2;
                        cost += State.World.ItemRepository.GetItem(ItemType.CompoundBow).Cost;
                    }
                }
            }
        }

        return cost;
    }

    public void ExpensiveFit()
    {
        foreach (ActorUnit actor in _displayUnits)
        {
            if (actor != null)
            {
                if ((actor.Unit.HasFreeItemSlot() || actor.Unit.HasSpecificWeapon(ItemType.Bow, ItemType.Mace)) == false || actor.Unit.FixedGear || (actor.Unit.Items[0]?.LockedItem ?? false)) continue;
                if (actor.Unit.GetBestMelee().Damage == 2 && actor.Unit.GetBestRanged() == null)
                {
                    if (actor.Unit.BestSuitedForRanged())
                        Shop.BuyItem(_empire, actor.Unit, State.World.ItemRepository.GetItem(ItemType.CompoundBow));
                    else
                        Shop.BuyItem(_empire, actor.Unit, State.World.ItemRepository.GetItem(ItemType.Axe));
                    actor.UpdateBestWeapons();
                }
                else if (BulkBuyUI.SellAndBuy.isOn)
                {
                    if (actor.Unit.GetBestMelee() == State.World.ItemRepository.GetItem(ItemType.Mace))
                    {
                        Shop.SellItem(_empire, actor.Unit, actor.Unit.GetItemSlot(State.World.ItemRepository.GetItem(ItemType.Mace)));
                        Shop.BuyItem(_empire, actor.Unit, State.World.ItemRepository.GetItem(ItemType.Axe));
                        actor.UpdateBestWeapons();
                    }

                    if (actor.Unit.GetBestRanged() == State.World.ItemRepository.GetItem(ItemType.Bow))
                    {
                        Shop.SellItem(_empire, actor.Unit, actor.Unit.GetItemSlot(State.World.ItemRepository.GetItem(ItemType.Bow)));
                        Shop.BuyItem(_empire, actor.Unit, State.World.ItemRepository.GetItem(ItemType.CompoundBow));
                        actor.UpdateBestWeapons();
                    }
                }
            }
        }

        UpdateDrawnActors();
        UpdateUnitInfoPanel();
        GenText();
    }

    private int AccessoryFitCost()
    {
        int cost = 0;
        foreach (ActorUnit actor in _displayUnits)
        {
            if (actor != null)
            {
                if (actor.Unit.HasFreeItemSlot() == false || actor.Unit.FixedGear) continue;
                if (actor.Unit.GetBestRanged() != null)
                {
                    if (actor.Unit.HasSpecificWeapon(ItemType.Gloves) == false)
                        cost += State.World.ItemRepository.GetItem(ItemType.Gloves).Cost;
                    else
                        cost += State.World.ItemRepository.GetItem(ItemType.Shoes).Cost;
                }
                else if (actor.Unit.GetBestMelee().Damage != 2)
                {
                    if (actor.Unit.GetStat(Stat.Agility) < 12 || actor.Unit.HasSpecificWeapon(ItemType.Helmet))
                        cost += State.World.ItemRepository.GetItem(ItemType.Shoes).Cost;
                    else
                        cost += State.World.ItemRepository.GetItem(ItemType.Helmet).Cost;
                }
            }
        }

        return cost;
    }

    public void AccessoryFit()
    {
        foreach (ActorUnit actor in _displayUnits)
        {
            if (actor != null)
            {
                if (actor.Unit.HasFreeItemSlot() == false || actor.Unit.FixedGear) continue;
                if (actor.Unit.GetBestRanged() != null)
                {
                    if (actor.Unit.HasSpecificWeapon(ItemType.Gloves) == false)
                        Shop.BuyItem(_empire, actor.Unit, State.World.ItemRepository.GetItem(ItemType.Gloves));
                    else
                        Shop.BuyItem(_empire, actor.Unit, State.World.ItemRepository.GetItem(ItemType.Shoes));
                }
                else if (actor.Unit.GetBestMelee().Damage != 2)
                {
                    if (actor.Unit.GetStat(Stat.Agility) < 12 || actor.Unit.HasSpecificWeapon(ItemType.Helmet))
                        Shop.BuyItem(_empire, actor.Unit, State.World.ItemRepository.GetItem(ItemType.Shoes));
                    else
                        Shop.BuyItem(_empire, actor.Unit, State.World.ItemRepository.GetItem(ItemType.Helmet));
                }
            }
        }

        UpdateDrawnActors();
        UpdateUnitInfoPanel();
        GenText();
    }

    public void AccessoryBulkSell()
    {
        foreach (ActorUnit actor in _displayUnits)
        {
            if (actor != null)
            {
                for (int i = 0; i < actor.Unit.Items.Length; i++)
                {
                    if (actor.Unit.Items[i] != null)
                    {
                        if (actor.Unit.Items[i] is Accessory)
                        {
                            actor.Unit.SetItem(null, i);
                        }
                    }
                }
            }
        }

        UpdateUnitInfoPanel();
        GenText();
    }

    private void BuildVillageView()
    {
        if (_village != null)
        {
            if (_villageView == null)
            {
                _villageView = new VillageView(_village, VillageUI);
                _villageView.Open(_village, _empire);
            }
            else
                _villageView.Open(_village, _empire);
        }
    }

    public void ForceRefreshVillageView()
    {
        _villageView?.Refresh();
    }

    private void BuildCustomizer()
    {
        if (_army.Units.Count > _selectedIndex)
        {
            if (Customizer == null)
                Customizer = new UnitCustomizer(_army.Units[_selectedIndex], CustomizerUI);
            else
                Customizer.SetUnit(_army.Units[_selectedIndex]);
            CustomizerUI.gameObject.SetActive(true);
        }
    }

    public void CopySkintoneFromCustomizer()
    {
        Unit source = Customizer.Unit;
        foreach (Unit unit in _army.Units.Where(s => Equals(s.Race, source.Race) && s.Type != UnitType.Leader))
        {
            unit.SkinColor = source.SkinColor;
        }
    }

    public void CopyHairColorFromCustomizer()
    {
        Unit source = Customizer.Unit;
        foreach (Unit unit in _army.Units.Where(s => Equals(s.Race, source.Race) && s.Type != UnitType.Leader))
        {
            unit.HairColor = source.HairColor;
        }
    }

    public void CopyHairStyleFromCustomizer()
    {
        Unit source = Customizer.Unit;
        foreach (Unit unit in _army.Units.Where(s => Equals(s.Race, source.Race) && s.Type != UnitType.Leader))
        {
            unit.HairStyle = source.HairStyle;
        }
    }

    public void CopyBodyColorFromCustomizer()
    {
        Unit source = Customizer.Unit;
        foreach (Unit unit in _army.Units.Where(s => Equals(s.Race, source.Race) && s.Type != UnitType.Leader))
        {
            unit.AccessoryColor = source.AccessoryColor;
        }
    }

    public void CopyBreastSizeFromCustomizer()
    {
        if (Customizer.Unit.HasBreasts == false) return;
        Unit source = Customizer.Unit;
        foreach (Unit unit in _army.Units.Where(s => Equals(s.Race, source.Race) && s.HasBreasts == source.HasBreasts && s.Type != UnitType.Leader))
        {
            unit.SetDefaultBreastSize(source.BreastSize);
        }
    }

    public void CopyClothingTypeFromCustomizer()
    {
        Unit source = Customizer.Unit;
        foreach (Unit unit in _army.Units.Where(s => Equals(s.Race, source.Race) &&
                                                    (s.Type == source.Type || (!Equals(source.Race, Race.Slime) && source.Type == UnitType.Leader && (s.Type == UnitType.Soldier || s.Type == UnitType.Mercenary || s.Type == UnitType.Adventurer)))))
        {
            unit.ClothingType = source.ClothingType;
            unit.ClothingType2 = source.ClothingType2;
            unit.ClothingHatType = source.ClothingHatType;
            unit.ClothingAccessoryType = source.ClothingAccessoryType;
            unit.ClothingExtraType1 = source.ClothingExtraType1;
            unit.ClothingExtraType2 = source.ClothingExtraType2;
            unit.ClothingExtraType3 = source.ClothingExtraType3;
            unit.ClothingExtraType4 = source.ClothingExtraType4;
            unit.ClothingExtraType5 = source.ClothingExtraType5;
        }
    }

    public void CopyClothingColorFromCustomizer()
    {
        Unit source = Customizer.Unit;
        foreach (Unit unit in _army.Units.Where(s => Equals(s.Race, Race.Panther) == Equals(source.Race, Race.Panther))) //Since panthers use different color systems
        {
            unit.ClothingColor = source.ClothingColor;
            unit.ClothingColor2 = source.ClothingColor2;
            unit.ClothingColor3 = source.ClothingColor3;
        }
    }

    public void CopyEyeColorFromCustomizer()
    {
        Unit source = Customizer.Unit;
        foreach (Unit unit in _army.Units.Where(s => Equals(s.Race, source.Race)))
        {
            unit.EyeColor = source.EyeColor;
        }
    }

    public void CopyEyeTypeFromCustomizer()
    {
        Unit source = Customizer.Unit;
        foreach (Unit unit in _army.Units.Where(s => Equals(s.Race, source.Race)))
        {
            unit.EyeType = source.EyeType;
        }
    }

    public void RandomizeUnit()
    {
        RaceFuncs.GetRace(Customizer.Unit).RandomCustomCall(Customizer.Unit);
        Customizer.RefreshView();
        Customizer.RefreshGenderSelector();
    }

    private void BuildShop()
    {
        if (_army.Units.Count > _selectedIndex)
        {
            Unit unit = _army.Units[_selectedIndex];
            if (unit != null)
            {
                _shop = new Shop(_empire, _village, unit, _army, ShopUI, _village != null);
                ShopUI.gameObject.SetActive(true);
            }
        }
    }

    internal void ShopSellItem(int slot) => _shop.SellItem(slot);
    internal void ShopTransferToInventory(int slot) => _shop.TransferItemToInventory(slot);
    internal void ShopTransferItemToCharacter(int type) => _shop.TransferItemToCharacter(type);
    internal void ShopSellItemFromInventory(int type) => _shop.SellItemFromInventory(type);

    internal void ShopGenerateBuyButton(int type)
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
            box.SetData(() =>
                {
                    _shop.BuyForAll(type);
                    State.GameManager.RecruitMode.SetUpDisplay();
                }, "Buy", "Cancel", $"Buy item for all units in army? Cost : {_shop.BuyForAllCost(type)}  (you were holding shift)");
        }
        else
            _shop.BuyItem(type);
    }


    private void BuildMercenaryView(bool special)
    {
        int children = MercenaryScreenUI.Folder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(MercenaryScreenUI.Folder.transform.GetChild(i).gameObject);
        }

        List<MercenaryContainer> list;
        if (special)
            list = MercenaryHouse.UniqueMercs;
        else
            list = _mercenaryHouse.Mercenaries;
        foreach (var merc in list)
        {
            GameObject obj = Instantiate(MercenaryScreenUI.HireableObject, MercenaryScreenUI.Folder);
            UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
            ActorUnit actor = new ActorUnit(new Vec2I(0, 0), merc.Unit);
            Text genderText = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
            Text expText = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
            GameObject equipRow = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(2).gameObject;
            GameObject statRow1 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(3).gameObject;
            GameObject statRow2 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(4).gameObject;
            GameObject statRow3 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(5).gameObject;
            GameObject statRow4 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(6).gameObject;
            Text traitList = obj.transform.GetChild(2).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
            Text hireButton = obj.transform.GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<Text>();

            string gender;

            if (merc.Unit.GetGender() == Gender.None)
            {
                genderText.text += $"{merc.Title}";
            }
            else
            {
                if (merc.Unit.GetGender() == Gender.Hermaphrodite)
                    gender = "Herm";

                else
                    gender = merc.Unit.GetGender().ToString();
                genderText.text = $"{gender} {merc.Title}";
            }

            expText.text = $"Level {merc.Unit.Level} ({(int)merc.Unit.Experience} EXP)";

            equipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(0)?.Name;
            equipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(1)?.Name;
            if (actor.Unit.HasTrait(TraitType.Resourceful))
            {
                equipRow.transform.GetChild(2).gameObject.SetActive(true);
                equipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(0)?.Name;
                equipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(1)?.Name;
                equipRow.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(2)?.Name;
            }
            else
            {
                equipRow.transform.GetChild(2).gameObject.SetActive(false);
                equipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(0)?.Name;
                equipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(1)?.Name;
            }

            statRow1.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Strength).ToString();
            statRow1.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Dexterity).ToString();
            statRow2.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Mind).ToString();
            statRow2.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Will).ToString();
            statRow3.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Endurance).ToString();
            statRow3.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Agility).ToString();
            if (actor.PredatorComponent != null)
            {
                statRow4.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Voracity).ToString();
                statRow4.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Stomach).ToString();
            }
            else
                statRow4.SetActive(false);

            hireButton.text = "Hire Unit (" + merc.Cost.ToString() + "G)";
            traitList.text = RaceEditorPanel.TraitListToText(merc.Unit.GetTraits, true).Replace(", ", "\n");

            actor.UpdateBestWeapons();
            sprite.UpdateSprites(actor);
            sprite.Name.text = merc.Unit.Name;
            Button button = obj.GetComponentInChildren<Button>();
            button.onClick.AddListener(() => HireMercenary(merc, obj));
        }

        UpdateMercenaryScreenText();
        MercenaryScreenUI.gameObject.SetActive(true);
    }

    private void HireMercenary(MercenaryContainer merc, GameObject obj)
    {
        if (_empire.Gold >= merc.Cost)
        {
            if (_army.Units.Count < _army.MaxSize)
            {
                _army.Units.Add(merc.Unit);
                merc.Unit.Side = _army.Side;
                _empire.SpendGold(merc.Cost);
                _mercenaryHouse.Mercenaries.Remove(merc);
                MercenaryHouse.UniqueMercs.Remove(merc);
                Destroy(obj);
                UpdateActorList();
                UpdateMercenaryScreenText();
            }
        }
    }

    private void HireVillageMercenary(MercenaryContainer merc, GameObject obj)
    {
        if (_village.HireSpecialUnit(_empire, _army, merc))
        {
            Destroy(obj);
            UpdateActorList();
            UpdateMercenaryScreenText();
        }
    }

    private void BuildVillageMercenaryView()
    {
        int children = MercenaryScreenUI.Folder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(MercenaryScreenUI.Folder.transform.GetChild(i).gameObject);
        }

        List<MercenaryContainer> list;
        list = _village.Mercenaries.Concat(_village.Adventurers).ToList();
        foreach (var merc in list)
        {
            GameObject obj = Instantiate(MercenaryScreenUI.HireableObject, MercenaryScreenUI.Folder);
            UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
            ActorUnit actor = new ActorUnit(new Vec2I(0, 0), merc.Unit);
            //Text text = obj.transform.GetChild(3).GetComponent<Text>();
            Text genderText = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
            Text expText = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
            GameObject equipRow = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(2).gameObject;
            GameObject statRow1 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(3).gameObject;
            GameObject statRow2 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(4).gameObject;
            GameObject statRow3 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(5).gameObject;
            GameObject statRow4 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(6).gameObject;
            Text traitList = obj.transform.GetChild(2).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
            Text hireButton = obj.transform.GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<Text>();

            string gender;

            if (merc.Unit.GetGender() == Gender.None)
            {
                genderText.text += $"{merc.Title}";
            }
            else
            {
                if (merc.Unit.GetGender() == Gender.Hermaphrodite)
                    gender = "Herm";

                else
                    gender = merc.Unit.GetGender().ToString();
                genderText.text = $"{gender} {merc.Title}";
            }

            traitList.text = RaceEditorPanel.TraitListToText(merc.Unit.GetTraits, true).Replace(", ", "\n");
            expText.text = $"Level {merc.Unit.Level} ({(int)merc.Unit.Experience} EXP)";
            if (actor.Unit.HasTrait(TraitType.Resourceful))
            {
                equipRow.transform.GetChild(2).gameObject.SetActive(true);
                equipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(0)?.Name;
                equipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(1)?.Name;
                equipRow.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(2)?.Name;
            }
            else
            {
                equipRow.transform.GetChild(2).gameObject.SetActive(false);
                equipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(0)?.Name;
                equipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(1)?.Name;
            }

            statRow1.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Strength).ToString();
            statRow1.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Dexterity).ToString();
            statRow2.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Mind).ToString();
            statRow2.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Will).ToString();
            statRow3.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Endurance).ToString();
            statRow3.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Agility).ToString();
            if (actor.PredatorComponent != null)
            {
                statRow4.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Voracity).ToString();
                statRow4.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Stomach).ToString();
            }
            else
                statRow4.SetActive(false);

            hireButton.text = "Hire Unit (" + merc.Cost.ToString() + "G)";

            //text.text = $"{merc.Title}\nLevel: {merc.Unit.Level} Exp: {(int)merc.Unit.Experience}\n" +
            //    $"Items: {merc.Unit.GetItem(0)?.Name} {merc.Unit.GetItem(1)?.Name}\n" +
            //     $"Str: {merc.Unit.GetStatBase(Stat.Strength)} Dex: {merc.Unit.GetStatBase(Stat.Dexterity)} Agility: {merc.Unit.GetStatBase(Stat.Agility)}\n" +
            //    $"Mind: {merc.Unit.GetStatBase(Stat.Mind)} Will: {merc.Unit.GetStatBase(Stat.Will)} Endurance: {merc.Unit.GetStatBase(Stat.Endurance)}\n";
            //if (actor.Unit.Predator)
            //    text.text += $"Vore: {merc.Unit.GetStatBase(Stat.Voracity)} Stomach: {merc.Unit.GetStatBase(Stat.Stomach)}";

            actor.UpdateBestWeapons();
            sprite.UpdateSprites(actor);
            sprite.Name.text = merc.Unit.Name;
            Button button = obj.GetComponentInChildren<Button>();
            button.onClick.AddListener(() => HireVillageMercenary(merc, obj));
        }

        UpdateMercenaryScreenText();
        MercenaryScreenUI.gameObject.SetActive(true);
    }

    private void UpdateMercenaryScreenText()
    {
        MercenaryScreenUI.ArmySize.text = $"Army Size {_army.Units.Count} / {_army.MaxSize}";
        MercenaryScreenUI.RemainingGold.text = $"Remaining Gold: {_empire.Gold}";
    }

    private void BuildHiringView(string sorting = "")
    {
        int children = HireUI.ActorFolder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(HireUI.ActorFolder.transform.GetChild(i).gameObject);
        }

        List<Unit> units = _village.VillagePopulation.GetRecruitables().OrderByDescending(t => t.Experience).OrderByDescending(t => t.Level).ToList();
        if (sorting == "STR")
            units = units.OrderByDescending(t => t.GetStat(Stat.Strength)).ToList();
        else if (sorting == "DEX")
            units = units.OrderByDescending(t => t.GetStat(Stat.Dexterity)).ToList();
        else if (sorting == "MND")
            units = units.OrderByDescending(t => t.GetStat(Stat.Mind)).ToList();
        else if (sorting == "WLL")
            units = units.OrderByDescending(t => t.GetStat(Stat.Will)).ToList();
        else if (sorting == "END")
            units = units.OrderByDescending(t => t.GetStat(Stat.Endurance)).ToList();
        else if (sorting == "AGI")
            units = units.OrderByDescending(t => t.GetStat(Stat.Agility)).ToList();
        else if (sorting == "VOR")
            units = units.OrderByDescending(t => t.GetStat(Stat.Voracity)).ToList();
        else if (sorting == "STM") units = units.OrderByDescending(t => t.GetStat(Stat.Stomach)).ToList();
        foreach (Unit unit in units)
        {
            GameObject obj = Instantiate(HireUI.HiringUnitPanel, HireUI.ActorFolder);
            UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
            ActorUnit actor = new ActorUnit(new Vec2I(0, 0), unit);
            //Text text = obj.transform.GetChild(3).GetComponent<Text>();
            Text genderText = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
            Text expText = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
            GameObject equipRow = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(2).gameObject;
            GameObject statRow1 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(3).gameObject;
            GameObject statRow2 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(4).gameObject;
            GameObject statRow3 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(5).gameObject;
            GameObject statRow4 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(6).gameObject;
            Text traitList = obj.transform.GetChild(2).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Text>();

            string gender;
            if (actor.Unit.GetGender() != Gender.None)
            {
                if (actor.Unit.GetGender() == Gender.Hermaphrodite)
                    gender = "Herm";
                else
                    gender = actor.Unit.GetGender().ToString();
                genderText.text = $"{gender}";
            }

            expText.text = $"Level {unit.Level} ({(int)unit.Experience} EXP)";
            if (actor.Unit.HasTrait(TraitType.Resourceful))
            {
                equipRow.transform.GetChild(2).gameObject.SetActive(true);
                equipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = unit.GetItem(0)?.Name;
                equipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = unit.GetItem(1)?.Name;
                equipRow.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = unit.GetItem(2)?.Name;
            }
            else
            {
                equipRow.transform.GetChild(2).gameObject.SetActive(false);
                equipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = unit.GetItem(0)?.Name;
                equipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = unit.GetItem(1)?.Name;
            }

            statRow1.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Strength).ToString();
            statRow1.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Dexterity).ToString();
            statRow2.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Mind).ToString();
            statRow2.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Will).ToString();
            statRow3.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Endurance).ToString();
            statRow3.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Agility).ToString();
            if (actor.PredatorComponent != null)
            {
                statRow4.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Voracity).ToString();
                statRow4.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Stomach).ToString();
            }
            else
                statRow4.SetActive(false);

            traitList.text = RaceEditorPanel.TraitListToText(unit.GetTraits, true).Replace(", ", "\n");
            //text.text += $"STR: {unit.GetStatBase(Stat.Strength)} DEX: { unit.GetStatBase(Stat.Dexterity)}\n" +
            //    $"MND: {unit.GetStatBase(Stat.Mind)} WLL: { unit.GetStatBase(Stat.Will)} \n" +
            //    $"END: {unit.GetStatBase(Stat.Endurance)} AGI: {unit.GetStatBase(Stat.Agility)}\n";
            //if (actor.PredatorComponent != null)
            //    text.text += $"VOR: {unit.GetStatBase(Stat.Voracity)} STM: { unit.GetStatBase(Stat.Stomach)}";
            actor.UpdateBestWeapons();
            sprite.UpdateSprites(actor);
            sprite.Name.text = unit.Name;
            Button button = obj.GetComponentInChildren<Button>();
            button.onClick.AddListener(() => Hire(unit));
            button.onClick.AddListener(() => Destroy(obj));
        }

        HireUI.ActorFolder.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300 * (1 + _village.VillagePopulation.GetRecruitables().Count / 3));
        HireUI.gameObject.SetActive(true);
    }


    private void Hire(Unit unit)
    {
        if (_village.HireUnit(_empire, _army, unit))
        {
            if (unit.HasTrait(TraitType.Infiltrator) && !unit.IsInfiltratingSide(unit.Side))
            {
                unit.OnDiscard = () =>
                {
                    _village.VillagePopulation.AddHireable(unit);
                    Debug.Log(unit.Name + " is returning to " + _village.Name);
                };
            }

            UpdateActorList();
            GenText();
        }
        else
        {
            ButtonCallback(13);
            if (_empire.Gold > 9)
                State.GameManager.CreateMessageBox("Population too low to hire additional units");
            else
                State.GameManager.CreateMessageBox("Not enough gold to hire additional units");
        }
    }

    private void Dismiss()
    {
        if (_army.Units.Count > _selectedIndex)
        {
            Unit unit = _army.Units[_selectedIndex];
            var dismissText = ArmyUI.Dismiss.gameObject.GetComponentInChildren(typeof(Text)) as Text;
            if (dismissText.text == "Exfiltrate")
            {
                Exfiltrate(unit);
                return;
            }

            if (unit != null)
            {
                _army.Units.Remove(unit);
                UpdateActorList();
                if (_village != null)
                {
                    if (RaceFuncs.IsMainRaceOrMercOrMonster(unit.Side))
                    {
                        if (_village.GetTotalPop() == 0)
                        {
                            if (RaceFuncs.IsUniqueMercsOrRebelsOrBandits(unit.Side))
                                _village.Race = State.World.GetEmpireOfSide(_army.Side).ReplacedRace;
                            else
                                _village.Race = unit.Race;
                        }

                        _village.VillagePopulation.AddHireable(unit);
                    }

                    GenText();
                }
                else
                {
                    _dismissList.Add(unit);
                    RefreshUnitPanelButtons();
                }
            }
        }

        if (_selectedIndex > 0)
            Select(_selectedIndex - 1);
        else
            Select(0);
    }

    private void Exfiltrate(Unit unit)
    {
        unit.Side = unit.FixedSide;
        Army destinationArmy = null;
        foreach (Army a in _empire.Armies)
        {
            if (a.Position.GetDistance(_army.Position) < 2 && a.Units.Count < a.MaxSize)
            {
                destinationArmy = a;
            }
        }

        if (destinationArmy == null)
        {
            if (_empire.Armies.Count() >= Config.MaxArmies)
            {
                State.GameManager.CreateMessageBox("You already have the maximum number of armies and no existing one with free space is adjacent.");
                return;
            }

            bool foundSpot = false;
            int x = 0;
            int y = 0;
            for (int i = 0; i < 50; i++)
            {
                x = State.Rand.Next(_army.Position.X - 1, _army.Position.X + 2);
                y = State.Rand.Next(_army.Position.Y - 1, _army.Position.Y + 2);

                if (StrategicUtilities.IsTileClear(new Vec2I(x, y)))
                {
                    foundSpot = true;
                    break;
                }
            }

            if (foundSpot)
            {
                Vec2I destLoc = new Vec2I(x, y);
                destinationArmy = new Army(_empire, new Vec2I(destLoc.X, destLoc.Y), unit.FixedSide);
            }
            else
            {
                State.GameManager.CreateMessageBox("Couldn't find a free space for the unit to exfiltrate to.");
                return;
            }
        }

        _army.Units.Remove(unit);
        destinationArmy.Units.Add(unit);
        _empire.Armies.Add(destinationArmy);
        State.GameManager.SwitchToStrategyMode();
    }

    public void SetUpDisplay()
    {
        RecruitUI.CheapUpgrade.gameObject.SetActive(false);
        RecruitUI.HireSoldier.gameObject.SetActive(false);
        RecruitUI.HireVillageMerc.gameObject.SetActive(false);
        RecruitUI.RecruitSoldier.gameObject.SetActive(false);
        RecruitUI.ResurrectLeader.gameObject.SetActive(false);
        if (ArmyUI.UnitInfoArea.Length != Config.MaximumPossibleArmy)
        {
            //for (int i = ArmyUI.UnitFolder.childCount - 1; i >= 0; i--)
            //{
            //    Destroy(ArmyUI.UnitFolder.GetChild(i).gameObject);
            //}
            ArmyUI.UnitInfoArea = new UIUnitSprite[Config.MaximumPossibleArmy];
            for (int i = 0; i < Config.MaximumPossibleArmy; i++)
            {
                ArmyUI.UnitInfoArea[i] = Instantiate(State.GameManager.UIUnit, ArmyUI.UnitFolder).GetComponent<UIUnitSprite>();
                ArmyUI.UnitInfoArea[i].gameObject.SetActive(false);
            }
        }

        ArmyUI.Selector.transform.SetAsLastSibling();
        if (_infoPanel == null)
        {
            _infoPanel = new InfoPanel(ArmyUI.InfoPanel);
        }

        _infoPanel.ClearText();
        _displayUnits = new ActorUnit[Config.MaximumPossibleArmy];
        _displayCreatedUnit = new Unit[Config.MaximumPossibleArmy];
        for (int x = 0; x < Config.MaximumPossibleArmy; x++)
        {
            ArmyUI.UnitInfoArea[x].SetIndex(x);
        }

        UpdateActorList();
        if (_army.Units.Count > 0)
            Select(0);
        else
        {
            _selectedIndex = -1;
            UpdateUnitInfoPanel();
        }

        BannerType.gameObject.SetActive(false);
        ShowBanner.gameObject.SetActive(true);
        RecruitUI.AddUnit.gameObject.SetActive(Config.CheatAddUnitButton);
        RefreshUnitPanelButtons();
    }

    public void UpdateActorList()
    {
        _leader = _army.LeaderIfInArmy();
        Vec2I noLoc = new Vec2I(0, 0);
        for (int x = 0; x < Config.MaximumPossibleArmy; x++)
        {
            if (x >= _army.Units.Count)
            {
                _displayUnits[x] = null;
                _displayCreatedUnit[x] = null;
                continue;
            }
            else if (_army.Units[x] != _displayCreatedUnit[x])
            {
                _displayUnits[x] = new ActorUnit(noLoc, _army.Units[x]);
                _displayCreatedUnit[x] = _army.Units[x];
                _displayUnits[x].UpdateBestWeapons();
                _army.Units[x].CurrentLeader = _leader;
            }
            //else it already exists and is correct, so we do nothing
        }

        ArmyUI.UnitInfoAreaSize.sizeDelta = new Vector2(1400, Mathf.Max((5 + _army.Units.Count()) / 6 * 240, 900));
        UpdateDrawnActors();
    }

    private void CheatAddUnit()
    {
        if (_army == null)
        {
            State.GameManager.CreateMessageBox("Can't create new enemy armies with this function");
            return;
        }

        Empire thisEmpire = State.World.GetEmpireOfSide(_army.Side);
        thisEmpire = thisEmpire ?? _empire;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            for (int i = 0; i < 48; i++)
            {
                if (_army.Units.Count < _army.MaxSize)
                {
                    Unit unit = new Unit(thisEmpire.Side, thisEmpire.ReplacedRace, thisEmpire.StartingXp, thisEmpire.CanVore);
                    _army.Units.Add(unit);
                }
            }
        }
        else
        {
            if (_army.Units.Count < _army.MaxSize)
            {
                Unit unit = new Unit(thisEmpire.Side, thisEmpire.ReplacedRace, thisEmpire.StartingXp, thisEmpire.CanVore);
                _army.Units.Add(unit);
            }
        }

        if (_village != null) GenText();
        UpdateActorList();
    }

    /// <summary>
    ///     Auto-called by UpdateActorList for now
    /// </summary>
    private void UpdateDrawnActors()
    {
        if (_army == null) return;

        for (int x = 0; x < Config.MaximumPossibleArmy; x++)
        {
            if (_displayUnits[x] == null)
            {
                ArmyUI.UnitInfoArea[x].gameObject.SetActive(false);
                continue;
            }

            ArmyUI.UnitInfoArea[x].gameObject.SetActive(true);
            ArmyUI.UnitInfoArea[x].UpdateSprites(_displayUnits[x]);
            ArmyUI.UnitInfoArea[x].Name.text = _army.Units[x].Name;
        }
    }

    public void BuildRaceDisplay()
    {
        int children = RaceUI.ActorFolder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(RaceUI.ActorFolder.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < _village.VillagePopulation.Population.Count; i++)
        {
            if (_village.VillagePopulation.Population[i].Population - _village.VillagePopulation.Population[i].Hireables > 0)
            {
                GameObject obj = Instantiate(RaceUI.RecruitPanel, RaceUI.ActorFolder);
                UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
                // Side was 1 for Unit
                ActorUnit actor = new ActorUnit(new Vec2I(0, 0), new Unit(Race.Dog.ToSide(), _village.VillagePopulation.Population[i].Race, 0, true));
                TextMeshProUGUI text = obj.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
                var racePar = RaceParameters.GetTraitData(actor.Unit);
                text.text = $"{_village.VillagePopulation.Population[i].Race}\nAvailable: {_village.VillagePopulation.Population[i].Population - _village.VillagePopulation.Population[i].Hireables}\nFavored Stat: {State.RaceSettings.GetFavoredStat(actor.Unit.Race)}\nDefault Traits:\n{State.RaceSettings.ListTraits(actor.Unit.Race)}";
                sprite.UpdateSprites(actor);
                Button button = obj.GetComponentInChildren<Button>();
                Race tempRace = _village.VillagePopulation.Population[i].Race;
                button.onClick.AddListener(() => Recruit(tempRace));
                button.onClick.AddListener(() => BuildRaceDisplay());
            }
        }

        RaceUI.gameObject.SetActive(true);
    }

    private void Recruit(Race race)
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            var unit = _village.RecruitPlayerUnit(_empire, _army, race);
            for (int i = 0; i < 48; i++)
            {
                if (unit != null)
                {
                    if (unit.HasTrait(TraitType.Infiltrator) && !unit.IsInfiltratingSide(unit.Side))
                    {
                        unit.OnDiscard = () =>
                        {
                            _village.VillagePopulation.AddHireable(unit);
                            Debug.Log(unit.Name + " is returning to " + _village.Name);
                        };
                    }

                    unit = _village.RecruitPlayerUnit(_empire, _army, race);
                }
                else
                    break;
            }

            UpdateActorList();
            GenText();
        }
        else
        {
            Unit unit = _village.RecruitPlayerUnit(_empire, _army, race);
            if (unit != null)
            {
                if (unit.HasTrait(TraitType.Infiltrator) && !unit.IsInfiltratingSide(unit.Side))
                {
                    unit.OnDiscard = () =>
                    {
                        _village.VillagePopulation.AddHireable(unit);
                        Debug.Log(unit.Name + " is returning to " + _village.Name);
                    };
                }

                UpdateActorList();
                GenText();
            }
        }
    }
}