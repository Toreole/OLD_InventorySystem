using UnityEngine;
using System.Collections.Generic;

//The monobehavior that contains the inventory.
public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory localInstance;

    //public InventoryCluster<Inventory> inventory; -- wont show up in the standard editor. woul dneed custom inspector.
    public Inventory[] inventories = { new Inventory(5, "small bag"), new Inventory(6, "blue baggerino"), new Inventory(11, "medium bag"), new Inventory(16, "big boi bag") }; //can use up to 4 bags.
    public GameObject inventoryPrefab;
    public Dictionary<Currency, int> currencies = new Dictionary<Currency, int>();

    public ItemSO tomato;

    private void Awake()
    {
        localInstance = this;

        if(currencies.Count == 0)
        {
            currencies.Add(Currency.Gold, 0);
            currencies.Add(Currency.Resources, 0);
        }
    }

    private void Start()
    {
        for(int i = 0; i < 60; i++)
            inventories[0].TryAddItem(tomato);
        foreach(var inv in inventories)
        {
            inv.inventoryPrefab = inventoryPrefab;
            inv.ShowUI();
            inv.RefreshUI();
        }
    }
}