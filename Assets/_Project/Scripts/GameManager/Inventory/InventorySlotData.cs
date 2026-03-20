
using UnityEngine;

[System.Serializable]
public class InventorySlotData
{
    [SerializeField] private SO_GenericItem _item;
    [SerializeField] private int _quantity;

    public SO_GenericItem Item => _item;
    public int Quantity { get => _quantity; set => _quantity = value; }

    public InventorySlotData(SO_GenericItem item)
    {
        _item = item;
        _quantity = Item.IsStackable ? 1 : 0;
    }
}
