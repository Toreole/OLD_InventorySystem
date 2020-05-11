using UnityEngine;
using System.Collections;

//The Inventory used by the player.
[System.Serializable]
public class Inventory : AbsInventory
{
    public BagSO bagInfo = null;
    public Inventory() { }
    public Inventory(int size)
    {
        inventorySize = size;
        containedItems = new Item[size];
    }
    public Inventory(int size, string n_name)
    {
        inventorySize = size;
        containedItems = new Item[size];
        this.name = n_name;
    }
    public Inventory(BagSO info)
    {
        bagInfo = info;
        inventorySize = info.slots;
        containedItems = new Item[inventorySize];
        this.name = bagInfo.name;
    }
}