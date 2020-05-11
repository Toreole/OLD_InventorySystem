using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//the abstract inventory main class. Requires at least ONE object to be tagged as "InventoryParent" for UI to work.
public abstract class AbsInventory
{
    //while in netherlands i need to copy and paste these: < >
    //the items stored in this inventory.
    public Item[] containedItems;
    //the max amount of items that can be in this inventory.
    public int inventorySize = -1;
    //UI stuff.
    public GameObject inventoryPrefab;
    private AbsInventoryUI myUI;
    public InventoryType inventoryType;
    public string name;

    private const string inventoryParentTag = "InventoryParent";

    public bool TryAddItem(ItemSO itemSO)
    {
        Item item = new Item(itemSO, 1);
        Item overflowCatch;
        return (TryAddItem(item, out overflowCatch));
    }

    //out overflow item count later on?
    public bool TryAddItem(Item item, out Item overflow)
    {
        bool itemAdded = false;
        IEnumerable<Item> existingItems;
        if (ContainsItem(item.reference, out existingItems))
        {
            itemAdded = AddItemToExisting(item, out overflow, existingItems);
            if(overflow != null)
            {
                if (AddItemToNew(overflow))
                {
                    overflow = null;
                    itemAdded = true;
                }
            }
        }
        else
        {
            if (AddItemToNew(item))
            {
                itemAdded = true;
                overflow = null;
            }
            else
            {
                itemAdded = false;
                overflow = item;
            }
        }
        return itemAdded;
    }

    //this should add items to the inventory until: A: item.count == 0 or B: item.count > 0 && inventory is full.
    private bool AddItemToExisting(Item item, out Item overflow, IEnumerable<Item> existingitems)
    {
        //< >
        foreach (var existItem in existingitems)
        {
            var maxAdd = item.MaxStack - existItem.stackSize;
            if (maxAdd > 0)
            {
                if (item.stackSize < maxAdd)
                {
                    existItem.stackSize += item.stackSize;
                    item.stackSize = 0;
                    break;
                }
                else
                {
                    item.stackSize -= maxAdd;
                    existItem.stackSize = existItem.MaxStack;
                    if (item.stackSize <= 0)
                        break;
                }
            }
        }
        //if all stacks are filled, try to add the left over items to a new stack.
        if (item.stackSize > 0)
            if (AddItemToNew(item))
            {
                overflow = null;
                return true;
            } 

        overflow = item;
        return false;
    }

    //does not require overflow, as it will only add the full stack or not.
    private bool AddItemToNew(Item item)
    {
        for (int i = 0; i < containedItems.Length; i++)
        {
            if (containedItems[i] == null || containedItems[i].reference == null || containedItems[i].ItemType == ItemType.Invalid)
            {
                containedItems[i] = item;
                return true;
            }
        }
        return false;
    }

    private bool ContainsItem(ItemSO reference, out IEnumerable<Item> existingItems)
    {
        existingItems = containedItems.Where(a => a != null); 
        if (existingItems.Count() == 0) return false;
        existingItems = existingItems.Where(x => x.reference == reference);
        return existingItems.Count() > 0;
    }

    //remove a type of item (count), do not do anything when the given count is bigger than the amount of stored items of this type tho.
    public bool TryRemoveItem(ItemSO itemType) { return TryRemoveItem(itemType, 1); }
    public bool TryRemoveItem(ItemSO itemType, int count)
    {
        IEnumerable<Item> existingItems;
        if(ContainsItem(itemType, out existingItems))
        {
            if (existingItems.Sum(x => x.stackSize) < count)
                return false;
            else
            {
                //remove items until count == 0
                foreach(var item in existingItems)
                {
                    if (count <= 0)
                    {
                        break;
                    }

                    int delta = Mathf.Clamp((item.stackSize - count), 0, item.stackSize);
                    item.stackSize -= delta;
                    count -= delta;
                }
                return true;
            }
        }
        else
         return false;
    }

    //Show the UI lul
    public virtual void ShowUI()
    {
        if (myUI == null)
            CreateUI();
        else
        {
            myUI.gameObject.SetActive(true);
            myUI.Refresh(containedItems);
        }
    }

    public virtual void RefreshUI()
    {
        if(myUI != null)
            myUI.Refresh(containedItems);
    }

    //temporarily disable it i guess.
    public virtual void HideUI()
    {
        myUI.gameObject.SetActive(false);
    }

    //Create and set up the UI correctly.
    private void CreateUI()
    {
        var parent = GameObject.FindGameObjectWithTag(inventoryParentTag);
        if(parent == null)
        {
            Debug.Log("could not find the parent. is the tag set up?");
            return;
        }
        myUI = Object.Instantiate(inventoryPrefab, parent.transform).GetComponent<AbsInventoryUI>();
        myUI.SetupFor<InventoryItemSlot>(this);
    }

    //just destroy the damn gameobject lmao
    public virtual void DestroyUI()
    {
        Object.Destroy(myUI.gameObject);
    }
}