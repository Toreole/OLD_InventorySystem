using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Item class that handles the creation of basic Items in the Editor.
[CreateAssetMenu]
public class ItemSO : ScriptableObject {

    //The name of the item for display in the UI; Uses rich text !.
    public new string name;
    //The description for the item in the UI; Uses rich text !.
    [TextArea(1, 5)]
    public string description;
    //The base value that will be used for trading with NPCs (sell = 0.65*baseValue, buy = 1*baseValue)
    public int baseValue;
    //optional : public float weight;
    //The sprite that will be used to display the Item in the ItemSlot.
    public Sprite sprite;
    //The maximum Stack capacity for this particular Item.
    public int maxStack;
    //The Type of Item this is supposed to represent. (This allows to handle it correctly and cast the different SubTypes).
    public ItemType type = ItemType.Default;
}