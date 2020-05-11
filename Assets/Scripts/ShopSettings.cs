using UnityEngine;
using System.Collections.Generic;

public class ShopSettings : ScriptableObject
{
    public Currency currency;
    public List<ItemType> acceptedTypes;
}