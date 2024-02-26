using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ArmyExchanger : MonoBehaviour
{
    private UIUnitSprite[] _leftUnitSprites;
    private UIUnitSprite[] _rightUnitSprites;

    public Transform LeftFolder;
    public Transform RightFolder;

    internal Army LeftArmy;
    internal Army RightArmy;

    internal bool LeftReceived;
    internal bool RightReceived;

    private InfoPanel _infoPanel;
    public UnitInfoPanel Info;

    private ActorUnit[] _leftActors;
    private ActorUnit[] _rightActors;
    private Unit[] _leftUnits;
    private Unit[] _rightUnits;

    private int _leftSelected;
    private int _rightSelected;

    public GameObject LeftSelector;
    public GameObject RightSelector;

    public Button MoveToRight;
    public Button MoveToLeft;

    public GameObject ItemExchangeSet;
    public Transform ItemExchangeFolder;


    public GameObject ItemExchangePanel;

    private ItemExchangeControl[] _itemExchangers;

    private bool _fullArmies;

    internal void Initialize(Army left, Army right)
    {
        if (_infoPanel == null)
        {
            _infoPanel = new InfoPanel(Info);
        }
        else
            Select(0);

        _fullArmies = left.Units.Count == left.MaxSize && right.Units.Count == right.MaxSize;
        if (_fullArmies)
        {
            MoveToLeft.GetComponentInChildren<Text>().text = "Exchange Selected Units";
            MoveToRight.GetComponentInChildren<Text>().text = "Exchange Selected Units";
        }
        else
        {
            MoveToLeft.GetComponentInChildren<Text>().text = "Transfer Unit to Left Army";
            MoveToRight.GetComponentInChildren<Text>().text = "Transfer Unit to Right Army";
        }

        if (_leftUnitSprites == null || _leftUnitSprites.Length != Config.MaximumPossibleArmy)
        {
            _leftUnitSprites = new UIUnitSprite[Config.MaximumPossibleArmy];
            _rightUnitSprites = new UIUnitSprite[Config.MaximumPossibleArmy];
            for (int i = 0; i < Config.MaximumPossibleArmy; i++)
            {
                _leftUnitSprites[i] = Instantiate(State.GameManager.UIUnit, LeftFolder).GetComponent<UIUnitSprite>();
            }

            for (int i = 0; i < Config.MaximumPossibleArmy; i++)
            {
                _rightUnitSprites[i] = Instantiate(State.GameManager.UIUnit, RightFolder).GetComponent<UIUnitSprite>();
            }
        }

        LeftArmy = left;
        RightArmy = right;

        LeftReceived = false;
        RightReceived = false;

        _leftSelected = 0;
        _rightSelected = 0;

        _leftActors = new ActorUnit[Config.MaximumPossibleArmy];
        _leftUnits = new Unit[Config.MaximumPossibleArmy];
        _rightActors = new ActorUnit[Config.MaximumPossibleArmy];
        _rightUnits = new Unit[Config.MaximumPossibleArmy];

        for (int x = 0; x < Config.MaximumPossibleArmy; x++)
        {
            _leftUnitSprites[x].SetIndex(x);
            _rightUnitSprites[x].SetIndex(x + Config.MaximumPossibleArmy);
        }

        UpdateActorList();
    }

    private void Update()
    {
        if (gameObject.activeSelf == false) return;
        if (_infoPanel != null && LeftSelector.gameObject.activeSelf == false) Select(0);
        RefreshSelectors();
    }

    public void UpdateActorList()
    {
        Vec2I noLoc = new Vec2I(0, 0);
        for (int x = 0; x < Config.MaximumPossibleArmy; x++)
        {
            if (x >= LeftArmy.Units.Count)
            {
                _leftUnits[x] = null;
                _leftActors[x] = null;
            }
            else if (LeftArmy.Units[x] != _leftUnits[x])
            {
                _leftActors[x] = new ActorUnit(noLoc, LeftArmy.Units[x]);
                _leftUnits[x] = LeftArmy.Units[x];
                _leftActors[x].UpdateBestWeapons();
                _leftActors[x].Unit.Side = LeftArmy.Side;
            }

            if (x >= RightArmy.Units.Count)
            {
                _rightUnits[x] = null;
                _rightActors[x] = null;
            }
            else if (RightArmy.Units[x] != _rightUnits[x])
            {
                _rightActors[x] = new ActorUnit(noLoc, RightArmy.Units[x]);
                _rightUnits[x] = RightArmy.Units[x];
                _rightActors[x].UpdateBestWeapons();
                _rightActors[x].Unit.Side = RightArmy.Side;
            }
            //else it already exists and is correct, so we do nothing
        }

        UpdateDrawnActors();
    }

    private void UpdateDrawnActors()
    {
        for (int x = 0; x < Config.MaximumPossibleArmy; x++)
        {
            if (_leftActors[x] == null)
            {
                _leftUnitSprites[x].gameObject.SetActive(false);
            }
            else
            {
                _leftUnitSprites[x].gameObject.SetActive(true);
                _leftUnitSprites[x].UpdateSprites(_leftActors[x]);
                _leftUnitSprites[x].Name.text = LeftArmy.Units[x].Name;
            }

            if (_rightActors[x] == null)
            {
                _rightUnitSprites[x].gameObject.SetActive(false);
            }
            else
            {
                _rightUnitSprites[x].gameObject.SetActive(true);
                _rightUnitSprites[x].UpdateSprites(_rightActors[x]);
                _rightUnitSprites[x].Name.text = RightArmy.Units[x].Name;
            }
        }

        LeftFolder.GetComponent<RectTransform>().sizeDelta = new Vector2(760, Mathf.Max((3 + LeftArmy.Units.Count) / 4 * 240, 1020));
        RightFolder.GetComponent<RectTransform>().sizeDelta = new Vector2(760, Mathf.Max((3 + RightArmy.Units.Count) / 4 * 240, 1020));
    }

    public void Select(int num)
    {
        if (num < Config.MaximumPossibleArmy)
            _leftSelected = num;
        else
            _rightSelected = num - Config.MaximumPossibleArmy;
        RefreshSelectors();
    }

    private void RefreshSelectors()
    {
        if (_leftSelected < LeftArmy.Units.Count)
        {
            LeftSelector.SetActive(true);
            LeftSelector.transform.position = _leftUnitSprites[_leftSelected].transform.position;
        }
        else
            LeftSelector.SetActive(false);

        if (_rightSelected < RightArmy.Units.Count)
        {
            RightSelector.SetActive(true);
            RightSelector.transform.position = _rightUnitSprites[_rightSelected].transform.position;
        }
        else
            RightSelector.SetActive(false);
    }

    public void UpdateInfo(int num)
    {
        Info.InfoText.text = "";
        if (num < 0) return;
        Unit hoveredUnit;
        if (num < Config.MaximumPossibleArmy)
            hoveredUnit = _leftUnits[num];
        else
            hoveredUnit = _rightUnits[num - Config.MaximumPossibleArmy];
        if (hoveredUnit != null)
        {
            _infoPanel.RefreshStrategicUnitInfo(hoveredUnit);
        }
    }

    public void TransferToLeft()
    {
        if (_fullArmies)
        {
            Exchange();
            return;
        }

        if (_rightSelected >= RightArmy.Units.Count || LeftArmy.Units.Count == LeftArmy.MaxSize) return;

        if (RightArmy.Units[_rightSelected] == RightArmy.EmpireOutside.Leader && !Equals(LeftArmy.Side, RightArmy.Side))
        {
            State.GameManager.CreateMessageBox("Can't trade heroes between races");
            return;
        }

        LeftArmy.Units.Add(RightArmy.Units[_rightSelected]);
        RightArmy.Units.RemoveAt(_rightSelected);
        UpdateActorList();
        LeftReceived = true;
        if (_rightSelected >= RightArmy.Units.Count)
        {
            _rightSelected = Mathf.Max(_rightSelected - 1, 0);
            RefreshSelectors();
        }
    }

    public void TransferToRight()
    {
        if (_fullArmies)
        {
            Exchange();
            return;
        }


        if (_leftSelected >= LeftArmy.Units.Count || RightArmy.Units.Count == RightArmy.MaxSize) return;

        if (LeftArmy.Units[_leftSelected] == LeftArmy.EmpireOutside.Leader && !Equals(LeftArmy.Side, RightArmy.Side))
        {
            State.GameManager.CreateMessageBox("Can't trade heroes between races");
            return;
        }

        var village = StrategicUtilities.GetVillageAt(RightArmy.Position);
        if (village != null && RightArmy.EmpireOutside != null && village.Empire.IsEnemy(RightArmy.EmpireOutside) && LeftArmy.Units[_leftSelected] == LeftArmy.EmpireOutside.Leader)
        {
            State.GameManager.CreateMessageBox("Leaders can't infiltrate");
            return;
        }

        RightArmy.Units.Add(LeftArmy.Units[_leftSelected]);
        LeftArmy.Units.RemoveAt(_leftSelected);
        UpdateActorList();
        RightReceived = true;
        if (_leftSelected >= LeftArmy.Units.Count)
        {
            _leftSelected = Mathf.Max(_leftSelected - 1, 0);
            RefreshSelectors();
        }
    }

    public void TransferItemToLeft(ItemType type)
    {
        if (RightArmy.ItemStock.HasItem(type))
        {
            RightArmy.ItemStock.TakeItem(type);
            LeftArmy.ItemStock.AddItem(type);
        }

        RefreshItemCounts();
    }

    public void TransferItemToRight(ItemType type)
    {
        if (LeftArmy.ItemStock.HasItem(type))
        {
            LeftArmy.ItemStock.TakeItem(type);
            RightArmy.ItemStock.AddItem(type);
        }

        RefreshItemCounts();
    }

    private void RefreshItemCounts()
    {
        for (int i = 0; i < _itemExchangers.Length; i++)
        {
            _itemExchangers[i].UpdateValues(LeftArmy.ItemStock.ItemCount(_itemExchangers[i].Type), RightArmy.ItemStock.ItemCount(_itemExchangers[i].Type));
            _itemExchangers[i].gameObject.SetActive(LeftArmy.ItemStock.HasItem(_itemExchangers[i].Type) || RightArmy.ItemStock.HasItem(_itemExchangers[i].Type));
        }
    }

    public void OpenItemExchanger()
    {
        if (_itemExchangers == null)
        {
            _itemExchangers = new ItemExchangeControl[(int)ItemType.Resurrection + 1];
            for (int i = 0; i < _itemExchangers.Length; i++)
            {
                _itemExchangers[i] = Instantiate(ItemExchangeSet, ItemExchangeFolder).GetComponent<ItemExchangeControl>();
                _itemExchangers[i].Type = (ItemType)i;
            }
        }

        RefreshItemCounts();
        ItemExchangePanel.SetActive(true);
    }

    public void Exchange()
    {
        if (_rightSelected >= RightArmy.Units.Count || _leftSelected >= LeftArmy.Units.Count) return;

        if (LeftArmy.Units[_rightSelected] == LeftArmy.EmpireOutside.Leader && !Equals(LeftArmy.Side, RightArmy.Side))
        {
            State.GameManager.CreateMessageBox("Can't trade heroes between races");
            return;
        }

        if (RightArmy.Units[_rightSelected] == RightArmy.EmpireOutside.Leader && !Equals(LeftArmy.Side, RightArmy.Side))
        {
            State.GameManager.CreateMessageBox("Can't trade heroes between races");
            return;
        }

        Unit leftTemp = LeftArmy.Units[_leftSelected];
        Unit rightTemp = RightArmy.Units[_rightSelected];
        LeftArmy.Units.Remove(leftTemp);
        RightArmy.Units.Remove(rightTemp);
        LeftArmy.Units.Add(rightTemp);
        RightArmy.Units.Add(leftTemp);
        UpdateActorList();
        LeftReceived = true;
        RightReceived = true;
    }

    public void Close()
    {
        if (RightArmy.Units.Count == 0)
            RightArmy.ItemStock.TransferAllItems(LeftArmy.ItemStock);
        else if (LeftArmy.Units.Count == 0) LeftArmy.ItemStock.TransferAllItems(RightArmy.ItemStock);
    }
}