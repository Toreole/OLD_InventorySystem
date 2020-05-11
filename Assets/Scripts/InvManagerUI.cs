using UnityEngine.UI;

//use this to find out what kind of Inventories are opened atm. Designed for one Player.
public static class InvManagerUI
{ 
    public static bool ShopOpened { get { return _Shop != null; } }
    public static AbsInventory _Shop { get; set; }

    public static bool ContainerOpened { get { return _Container != null; } }
    public static AbsInventory _Container { get; set; }

    public static bool InventoryOpened { get; set; }
}