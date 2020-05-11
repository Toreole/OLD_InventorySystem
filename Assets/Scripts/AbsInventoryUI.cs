using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

//This class is used to represent a inventory on the UI. 
public abstract class AbsInventoryUI : MonoBehaviour
{
    public Transform slotParent;
    public GameObject slotPrefab;
    public List<AbsItemSlot> slots;
    public Text nameText;

    public ItemSO emptyItem;

    //called upon creation of this UI element.
    public void SetupFor<T>(AbsInventory inventory) where T : AbsItemSlot
    {
        if (slotParent.childCount != 0)
            DestroyAllChildren();
        for (int i = 0; i < inventory.inventorySize; i++ )
        {
            var slot = Instantiate(slotPrefab, slotParent).GetComponent<T>();
            slot.partInventory = inventory;
            slot.indexInInventory = i;
            slots.Add(slot);
        }
        var trans = transform as RectTransform;
        var size = (Mathf.Ceil(inventory.inventorySize / 5f) * 55) + 30;
        trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
        nameText.text = inventory.name;
        Refresh(inventory.containedItems);
    }

    //refresh for the Items that are stored. (no need to know the entire inventory basically.
    public void Refresh(Item[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            var item = items[i];
            if (item == null)
                items[i] = new Item(emptyItem);

            else if (item.stackSize <= 0)
                items[i] = new Item(emptyItem);

            item = items[i];
            string count = (item.ItemType == ItemType.Invalid) ? "" : item.stackSize.ToString();
            slots[i].SetItem(items[i].reference, count);
        }
    }

    private void DestroyAllChildren()
    {
        slotParent.DetachChildren();
    }
}