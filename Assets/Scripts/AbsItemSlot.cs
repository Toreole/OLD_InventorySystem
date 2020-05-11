using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//this is the abstract class representing a single itemSlot in an inventoryUI element. Yes this uses a shitton of interfaces lol.
public abstract class AbsItemSlot : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public AbsInventory partInventory;
    public ItemSO displayedItem;
    public int indexInInventory;
    public Image display;
    public Text countTextObj;
    bool pointerOver;
    private const float HOVER_TIME = 0.2f;

    public bool IsEmpty { get { return (displayedItem == null || displayedItem.type == ItemType.Invalid); } }

    //Set the item with the ScriptableObjet for convenience. count as string lets it be empty.
    public void SetItem(ItemSO toSet, string count)
    {
        displayedItem = toSet;
        display.sprite = toSet.sprite;
        CountText = count;
    }
    public string CountText { get { return countTextObj.text; } set { countTextObj.text = value; } }

    //destroy this gameobject if it has no parent.
    private void OnTransformParentChanged()
    {
        if (transform.parent == null)
            Destroy(gameObject);
    }
    
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        //activate the drag thing.
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        //move the drag thing.
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        //If the drag ends in a Shop inventory, then Sell the item. 
        var endDrag = eventData.pointerCurrentRaycast.gameObject;
        if (endDrag == null)
        {
            print("enddrag is null");
            HandleNullDragEnd();
            return;
        }
        var endSlot = endDrag.GetComponent(typeof(AbsItemSlot)) as AbsItemSlot;
        if (endSlot == null)
        {
            //disable the drag thing.
            print("endslot is null");
            return;
        }
        var targetInv = endSlot.partInventory;
        if (targetInv.inventoryType == InventoryType.Drop || targetInv.inventoryType == InventoryType.EMPTY)
            return;
        //TODO : drop half of the slot into the other slot when the right mouse button is used instead.
        DropInto(endSlot, eventData.button == PointerEventData.InputButton.Right);
    }

    //TODO if a different inventory type is opened, put it in there.
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            RightClick();
        else if (eventData.button == PointerEventData.InputButton.Left && Input.GetKey(KeyCode.LeftShift))
            ShiftLeftClick();
    }

    //try to transfer item, or buy if its a shop. sell to shop.
    public abstract void RightClick();

    //try to transfer item. do nothing in shop
    public abstract void ShiftLeftClick();

    //When the mouse is over and there is a valid item stored in this slot, activate the hover description after a given amount of time when the pointer hasnt exit.
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (displayedItem == null || displayedItem.type == ItemType.Invalid)
            return;
        pointerOver = true;
        StopCoroutine("OnHover");
        StartCoroutine(OnHover());
    }

    //pretty simple
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        pointerOver = false;
        ItemHoverDescription.instance.gameObject.SetActive(false);
    }

    //activate the hover description after a bit of time.
    public virtual IEnumerator OnHover()
    {
        yield return new WaitForSeconds(HOVER_TIME);
        if(pointerOver)
        {
            var hover = ItemHoverDescription.instance;
            hover.SetItem(displayedItem);
        }
    }

    //TODO this shit lol. actually do the thing.
    void HandleNullDragEnd()
    {

    }

    //drop my item into the targetted slot. Drop half if rightclick.
    //TODO: shop is opened, sell the item i guess.
    void DropInto(AbsItemSlot slot, bool dropHalf)
    {
        //handy stuff for readability.
        var targetInv = slot.partInventory;
        var slotIndex = slot.indexInInventory;

        if (slot.IsEmpty)
        {
            //drop half of the stack into the other slot when its empty.
            if (dropHalf)
            {
                int halfAmount = Mathf.CeilToInt(partInventory.containedItems[indexInInventory].stackSize / 2f);
                partInventory.containedItems[indexInInventory].stackSize -= halfAmount;
                targetInv.containedItems[slotIndex] = new Item(displayedItem, halfAmount);
            }
            else
            {
                //just swap the item into the other slot.
                targetInv.containedItems[slotIndex] = partInventory.containedItems[indexInInventory];
                partInventory.containedItems[indexInInventory] = null;
            }
        }
        else if (slot.displayedItem == this.displayedItem)
        {
            //if both items are the same, try to combine their stacks.
            CombineStacks(targetInv, slotIndex);
        }
        else
        {
            //when both slots contain different items, swap their content.
            var temp = targetInv.containedItems[slotIndex];
            targetInv.containedItems[slotIndex] = partInventory.containedItems[indexInInventory];
            partInventory.containedItems[indexInInventory] = temp;
        }
        //TODO: Refresh the UI for both slots instead of the entire inventories 
        targetInv.RefreshUI();
        partInventory.RefreshUI();
    }

    //Combine two stacks of the same item. < >
    void CombineStacks(AbsInventory inv, int targetIndex)
    {
        var itemInfo = inv.containedItems[targetIndex];
        if (itemInfo.stackSize >= itemInfo.MaxStack)
            return;

        //the potential amount of items that could get added
        var maxAdd = itemInfo.MaxStack - itemInfo.stackSize;

        //the real amount that will get added to the existing stack (itemInfo)
        var realAdd = Mathf.Clamp(maxAdd, 0, partInventory.containedItems[indexInInventory].stackSize);
        
        //remove the real amount.
        partInventory.containedItems[indexInInventory].stackSize -= realAdd;
        if (partInventory.containedItems[indexInInventory].stackSize <= 0)
            partInventory.containedItems[indexInInventory] = null;
        //finally, ADD the amount.
        inv.containedItems[targetIndex].stackSize += realAdd;
    }
}