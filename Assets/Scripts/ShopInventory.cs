using System.Collections.Generic;
public class ShopInventory : AbsInventory
{
    public ShopSettings settings;
    public bool AcceptsEquipment { get { return (settings.acceptedTypes.Contains(ItemType.Equipment) ); } }
    public bool AcceptsGeneric { get { return (settings.acceptedTypes.Contains(ItemType.Default) || settings.acceptedTypes.Contains(ItemType.Usable) || settings.acceptedTypes.Contains(ItemType.Bag)); } }
    public Currency MyCurrency { get { return settings.currency; } }

    public override void ShowUI()
    {
        base.ShowUI();
        InvManagerUI._Shop = this;
    }

    public override void DestroyUI()
    {
        InvManagerUI._Shop = null;
        base.DestroyUI();
    }

    public override void HideUI()
    {
        InvManagerUI._Shop = null;
        base.HideUI();
    }

    public override void RefreshUI()
    {
        base.RefreshUI();
    }
}