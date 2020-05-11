using UnityEngine;
using System.Collections;

//Item class that represents those Items that the player can use at runtime.
[CreateAssetMenu]
public class UsableItemSO : ItemSO
{
    //This represents an Effect that can be invoked through this item. (A spell or something else). => SpellCaster.cs (position, target, entityMask, prefab)
    //Alternatively: replace this with your custom implementation of casting.
    public string invokeEffect;

    //The cooldown for this usable item.
    public float cooldown;
}