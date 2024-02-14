using UnityEngine;

public class Shop
{
    private Empire _empire;
    private Unit _unit;
    private readonly Village _village;
    private Army _army;
    private readonly int _unitItemSlots = 2;
    private readonly bool _inTown = true;

    private const int MaxSellSlots = 5;

    private ShopPanel _shopUI;

    public Shop(Empire empire, Village village, Unit unit, Army army, ShopPanel newShopUI, bool inTown)
    {
        this._empire = empire;
        this._village = village;
        this._unit = unit;
        this._army = army;
        this._inTown = inTown;
        _shopUI = newShopUI;
        _unitItemSlots = unit.Items.Length;
        if (_shopUI.SellPanels.Length == 0)
        {
            _shopUI.SellPanels = new ShopSellPanel[MaxSellSlots];
            for (int x = 0; x < MaxSellSlots; x++)
            {
                _shopUI.SellPanels[x] = Object.Instantiate(_shopUI.SellPrefab, new Vector3(0, 0), new Quaternion(), _shopUI.ButtonFolder).GetComponent<ShopSellPanel>();
                int slot = x;
                _shopUI.SellPanels[x].SellButton.onClick.AddListener(() => State.GameManager.RecruitMode.ShopSellItem(slot)); //These are done this way to avoid tying it to the first shop instance
                _shopUI.SellPanels[x].MoveToInventoryButton.onClick.AddListener(() => State.GameManager.RecruitMode.ShopTransferToInventory(slot));
            }
        }

        if (_shopUI.BuyPanels.Length == 0)
        {
            _shopUI.BuyPanels = new ShopBuyPanel[State.World.ItemRepository.NumItems];

            for (int x = 0; x < State.World.ItemRepository.NumItems; x++)
            {
                _shopUI.BuyPanels[x] = Object.Instantiate(_shopUI.BuyPrefab, new Vector3(0, 0), new Quaternion(), _shopUI.ButtonFolder).GetComponent<ShopBuyPanel>();
                //shopUI.BuyItemButton[x].GetComponent<RectTransform>().sizeDelta = new Vector2(600, 60);
                int type = x;
                _shopUI.BuyPanels[x].BuyButton.onClick.AddListener(() => State.GameManager.RecruitMode.ShopGenerateBuyButton(type)); //These are done this way to avoid tying it to the first shop instance
                _shopUI.BuyPanels[x].TakeFromInventoryButton.onClick.AddListener(() => State.GameManager.RecruitMode.ShopTransferItemToCharacter(type));
                _shopUI.BuyPanels[x].SellFromInventoryButton.onClick.AddListener(() => State.GameManager.RecruitMode.ShopSellItemFromInventory(type));
                Item item = State.World.ItemRepository.GetItem(x);
                if (item is SpellBook book)
                    _shopUI.BuyPanels[x].Description.text = $"{item.Name} - cost {item.Cost} - {book.DetailedDescription().Replace('\n', ' ')}";
                else
                    _shopUI.BuyPanels[x].Description.text = $"{item.Name} - cost {item.Cost} - {item.Description}";
            }
        }

        RegenButtonTextAndClickability();
    }

    public void TransferItemToInventory(int slot)
    {
        _army.ItemStock.AddItem(State.World.ItemRepository.GetItemType(_unit.GetItem(slot)));
        _unit.SetItem(null, slot);
        RegenButtonTextAndClickability();
    }

    public void TransferItemToCharacter(int type)
    {
        int slot = -1;
        for (int i = 0; i < _unit.Items.Length; i++)
        {
            if (_unit.Items[i] == null)
            {
                slot = i;
                break;
            }
        }

        if (slot == -1) return;
        if (_army.ItemStock.TakeItem((ItemType)type))
        {
            _unit.SetItem(State.World.ItemRepository.GetItem(type), slot);
        }

        RegenButtonTextAndClickability();
    }

    public void SellItemFromInventory(int type)
    {
        if (_army.ItemStock.TakeItem((ItemType)type))
        {
            _empire.AddGold(State.World.ItemRepository.GetItem(type).Cost / 2);
        }

        RegenButtonTextAndClickability();
    }

    public void SellItem(int slot)
    {
        SellItem(_empire, _unit, slot);
        RegenButtonTextAndClickability();
    }

    public static void SellItem(Empire empire, Unit unit, int slot)
    {
        if (unit.GetItem(slot) != null)
        {
            empire.AddGold(unit.GetItem(slot).Cost / 2);
            unit.SetItem(null, slot);
        }
    }

    public bool BuyItem(int type)
    {
        bool bought = BuyItem(_empire, _unit, State.World.ItemRepository.GetItem(type));
        if (bought) RegenButtonTextAndClickability();
        return bought;
    }

    public static bool BuyItem(Empire empire, Unit unit, Item type)
    {
        int freeItemSlot = -1;
        for (int j = 0; j < unit.Items.Length; j++)
        {
            if (unit.GetItem(j) == null)
            {
                freeItemSlot = j;
                break;
            }
        }

        if (freeItemSlot == -1 || empire.Gold < type.Cost)
        {
            return false;
        }

        empire.SpendGold(type.Cost);
        State.World.Stats.SpentGoldOnArmyEquipment(type.Cost, empire.Side);
        unit.SetItem(type, freeItemSlot);
        return true;
    }

    internal int BuyForAllCost(int type)
    {
        var item = State.World.ItemRepository.GetItem(type);
        int cost = 0;
        foreach (Unit unit in _army?.Units)
        {
            if (unit.HasFreeItemSlot() == false || unit.FixedGear) continue;
            if (unit.GetItemSlot(item) != -1) continue;
            cost += item.Cost;
        }

        return cost;
    }

    internal void BuyForAll(int type)
    {
        var item = State.World.ItemRepository.GetItem(type);
        foreach (Unit unit in _army?.Units)
        {
            if (unit.HasFreeItemSlot() == false || unit.FixedGear) continue;
            if (unit.GetItemSlot(item) != -1) continue;
            BuyItem(_empire, unit, item);
        }

        RegenButtonTextAndClickability();
    }

    private void RegenButtonTextAndClickability()
    {
        RegenBuyClickable();
        RegenSellText();
    }

    private void RegenBuyClickable()
    {
        for (int i = 0; i < _shopUI.BuyPanels.Length; i++)
        {
            if (_shopUI.BuyPanels[i] == null) continue;
            var racePar = RaceParameters.GetTraitData(_unit);
            if (racePar.CanUseRangedWeapons == false && State.World.ItemRepository.ItemIsRangedWeapon(i))
            {
                _shopUI.BuyPanels[i].gameObject.SetActive(false);
                continue;
            }

            Item item = State.World.ItemRepository.GetItem(i);
            if ((_unit.HasTrait(TraitType.Feral) || _unit.FixedGear) && item is Weapon)
            {
                _shopUI.BuyPanels[i].gameObject.SetActive(false);
                continue;
            }

            _shopUI.BuyPanels[i].gameObject.SetActive(true);

            _shopUI.BuyPanels[i].BuyButton.interactable = _inTown;
            _shopUI.BuyPanels[i].SellFromInventoryButton.interactable = _inTown && _army.ItemStock.HasItem((ItemType)i);

            if (item is SpellBook book)
            {
                if (book.Tier > (_village?.NetBoosts.SpellLevels ?? -5) + 1)
                {
                    if (_army.ItemStock.HasItem((ItemType)i) == false)
                    {
                        _shopUI.BuyPanels[i].gameObject.SetActive(false);
                        continue;
                    }
                    else
                    {
                        _shopUI.BuyPanels[i].BuyButton.interactable = false;
                    }
                }
            }


            _shopUI.BuyPanels[i].TakeFromInventoryButton.interactable = _army.ItemStock.HasItem((ItemType)i);
            _shopUI.BuyPanels[i].InventoryButtonText.text = $"Take from army inventory (You have {_army.ItemStock.ItemCount((ItemType)i)})";
            for (int j = 0; j < _unit.Items.Length; j++)
            {
                if (_unit.Items[j] == item)
                {
                    _shopUI.BuyPanels[i].BuyButton.interactable = false;
                    _shopUI.BuyPanels[i].TakeFromInventoryButton.interactable = false;
                }
            }

            if (item.Cost > _empire.Gold) _shopUI.BuyPanels[i].BuyButton.interactable = false;
        }
    }

    private void RegenSellText()
    {
        //rebuild sell buttons
        for (int i = 0; i < MaxSellSlots; i++)
        {
            _shopUI.SellPanels[i].gameObject.SetActive(i < _unitItemSlots);
            if (i >= _unitItemSlots) continue; //continue instead of break so it will hide the rest
            if (_unit.GetItem(i) != null)
            {
                _shopUI.SellPanels[i].Description.text = $"{_unit.GetItem(i).Name} -- sells for {_unit.GetItem(i).Cost / 2}";
                _shopUI.SellPanels[i].SellButton.interactable = _inTown && _unit.GetItem(i).LockedItem == false;
                _shopUI.SellPanels[i].MoveToInventoryText.text = $"Move to army inventory (You have {_army.ItemStock.ItemCount(State.World.ItemRepository.GetItemType(_unit.GetItem(i)))})";
                _shopUI.SellPanels[i].MoveToInventoryButton.interactable = _unit.GetItem(i).LockedItem == false;
            }
            else
            {
                _shopUI.SellPanels[i].Description.text = "empty";
                _shopUI.SellPanels[i].MoveToInventoryText.text = $"Move to inventory";
                _shopUI.SellPanels[i].SellButton.interactable = false;
                _shopUI.SellPanels[i].MoveToInventoryButton.interactable = false;
            }
        }
    }
}