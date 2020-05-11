using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

//This represents an itemSlot of the players inventory.
public class InventoryItemSlot : AbsItemSlot
{
    public override void RightClick()
    {
        if (InvManagerUI.ContainerOpened)
        {
            //container TryAddItem(..., overflow = this stored item.);
        }
        else if (InvManagerUI.ShopOpened)
        {
            //sell the item if possible.
            var currency = (InvManagerUI._Shop as ShopInventory).MyCurrency;
        }
        else if(displayedItem.type == ItemType.Usable)
        {
            //Try to use the item.
        }
        else if (displayedItem.type == ItemType.Equipment)
        {
            //try to equip this.
        }
    }

    public override void ShiftLeftClick()
    {
        return;
        //i dont really need this tbh lmao
    }
}