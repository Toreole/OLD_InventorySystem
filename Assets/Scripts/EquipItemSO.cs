using UnityEngine;
using System.Collections;

//This Item class represents equippable items.
[CreateAssetMenu]
public class EquipItemSO : ItemSO
{
    public new ItemType type = ItemType.Equipment;
    //The type of type this is, representing the "weight" unique per class
    public EquipType equipType;
    //the slot # this will be equipped into
    public byte slot;
    //the max durability of this piece of equipment
    public int durability;
    //the amount of protection against physical attacks this grants.
    public float physicalProtection;
    //the amount of protection against magical attacks this grants.
    public float magicalProtection;
    //override the MaxStack to be one by default. (Should not be changed).
    public new int maxStack = 1;
    //the required level to equip this piece.
    public int requiredLevel = 1;

    /**optional variables: 
               => PlayerAttribute enum contains (Endurance/Strength/Agility/Intelligence/etc.)
        public PlayerAttribute mainAttribute; 
        public int itemLevel;
        public int attributeMultiplicator; -> scales the mainAttribute value with the itemLevel.
    */

}