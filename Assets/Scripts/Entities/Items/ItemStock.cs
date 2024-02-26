using OdinSerializer;
using System.Collections.Generic;
using System.Linq;


internal class ItemStock
{
    [OdinSerialize]
    private Dictionary<ItemType,int> _items = new Dictionary<ItemType, int>();
    //public Dictionary<ItemType,int> Items { get => _items; set => _items = value; }

    internal void AddItem(ItemType type, int quantity = 1)
    {
        if (_items == null) _items = new Dictionary<ItemType, int>();
        if (_items.ContainsKey(type))
        {
            _items[type] += quantity;
        }
        else
            _items[type] = quantity;
    }

    internal bool HasItem(ItemType type)
    {
        if (_items == null) _items = new Dictionary<ItemType, int>();
        if (_items.TryGetValue(type, out int num))
        {
            return num > 0;
        }

        return false;
    }

    internal int ItemCount(ItemType type)
    {
        if (_items == null) _items = new Dictionary<ItemType, int>();
        if (_items.TryGetValue(type, out int num))
        {
            return num;
        }

        return 0;
    }

    internal bool TakeItem(ItemType type)
    {
        if (_items == null) _items = new Dictionary<ItemType, int>();
        if (_items.TryGetValue(type, out int num))
        {
            if (num > 0)
            {
                _items[type]--;
                return true;
            }
        }

        return false;
    }

    internal List<ItemType> GetAllSpellBooks()
    {
        if (_items == null) _items = new Dictionary<ItemType, int>();
        List<ItemType> items = new List<ItemType>();
        foreach (var item in _items)
        {
            if (item.Key >= ItemType.FireBall && item.Key <= ItemType.GateMaw)
            {
                if (item.Value > 0)
                {
                    for (int i = 0; i < item.Value; i++)
                    {
                        items.Add(item.Key);
                    }
                }
            }
        }

        return items;
    }

    internal List<ItemType> SellAllWeaponsAndAccessories(Empire empire)
    {
        if (_items == null) _items = new Dictionary<ItemType, int>();
        List<ItemType> items = new List<ItemType>();
        foreach (var item in _items)
        {
            if (item.Key < ItemType.FireBall)
            {
                if (item.Value > 0)
                {
                    empire.AddGold(State.World.ItemRepository.GetItem(item.Key).Cost / 2 * item.Value);
                }
            }
        }

        return items;
    }

    internal bool TransferAllItems(ItemStock destination)
    {
        if (_items == null) _items = new Dictionary<ItemType, int>();
        bool foundItem = false;
        foreach (var item in _items.ToList())
        {
            if (item.Value > 0)
            {
                foundItem = true;
                destination.AddItem(item.Key, item.Value);
                _items[item.Key] = 0;
            }
        }

        return foundItem;
    }


    internal bool TransferAllItems(ItemStock destination, ref List<Item> foundItems)
    {
        if (_items == null) _items = new Dictionary<ItemType, int>();
        bool foundItem = false;
        foreach (var item in _items.ToList())
        {
            if (item.Value > 0)
            {
                foundItem = true;
                foundItems.Add(State.World.ItemRepository.GetItem(item.Key));
                destination.AddItem(item.Key, item.Value);
                _items[item.Key] = 0;
            }
        }

        return foundItem;
    }
}