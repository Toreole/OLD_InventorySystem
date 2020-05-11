using System;
using UnityEngine;

//Item class that will be used to represent Runtime Items.
[System.Serializable]
public class Item
{
    //reference to a ItemSO type
    public ItemSO reference;
    //stackSize of this item, defaulted to 1 as 0 or below is invalid as a stack.
    public int stackSize = -1;
    //current cooldown time for this item. EquipItems have 0.1 cooldown. 
    public float CoolDownTime { get; set; }

    //Empty item.
    public Item() { }
    //Item count = 1, item reference set.
    public Item(ItemSO itemSO) { reference = itemSO; }
    //Item with a left over amount. Useful later on.
    public Item(ItemSO itemSO, ref int count) { reference = itemSO; count = count - MaxStack; stackSize = Mathf.Clamp(count, 1, MaxStack); }
    //Item with a given count, disregards left over count.
    public Item(ItemSO itemSO, int count) { reference = itemSO; stackSize = Mathf.Clamp(count, 1, MaxStack); }

    //Just a Helper to clone these lol, maybe useless
    internal Item Clone() { return new Item(this.reference, this.stackSize); }

    //general property block for convenience
    public bool IsEquippable { get { return reference.type == ItemType.Equipment; } }
    public bool IsUsable { get { return reference.type == ItemType.Usable; } }
    public int MaxStack { get { return reference.maxStack; } }
    public int StackValue { get { return stackSize * reference.baseValue; } }
    public int SingleValue { get { return reference.baseValue; } }
    public Sprite DisplaySprite { get { return reference.sprite; } }
    public string Name { get { return reference.name; } }
    public string Description { get { return reference.description; } }
    public ItemType ItemType { get { return reference.type; } }

    //Special properties for EquipItems (if the item reference is not a piece of equipment, it will return -1 as default.)
    public int RequiredLevel { get { if (reference.type == ItemType.Equipment) { var item = reference as EquipItemSO; return item.requiredLevel; } return -1; } }
    public int Durability { get { if (reference.type == ItemType.Equipment) { var item = reference as EquipItemSO; return item.durability; } return -1; } }
    public EquipType ArmorType { get { if (reference.type == ItemType.Equipment) { var item = reference as EquipItemSO; return item.equipType; } return EquipType.Invalid; } }

    //Special property for bags lol.
    public int Slots { get { if (reference.type == ItemType.Bag) { var item = reference as BagSO; return item.slots; } return -1; } }
}