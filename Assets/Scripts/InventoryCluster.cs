//This GENERIC class is used for when a single Object has multiple inventories (the bag style in WoW for example)
[System.Serializable]
public class InventoryCluster<T> where T : AbsInventory
{
    public T[] inventories;
    public int inventoryCount = 4;

    //Init this just in case lol
    private void Init()
    {
        if (inventories == null)
            inventories = new T[inventoryCount];
    }
}