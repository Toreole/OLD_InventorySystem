using UnityEngine;
using System.Collections;

//a bag that you can put into your bag slots. WOW style heck yeah. 
public class BagSO : ItemSO
{
    public int slots = 5;
    public new int maxStack = 1;
    public new ItemType type = ItemType.Bag;
}