using System;

public class ShopManager : GenericSingleton<ShopManager>
{
    public event Action OnItemPurchased;

    public bool CanBuy(SO_PowerUpItem item)
    {
        int currentLevel = PowerUpManager.Instance.GetLevel(item);

        if (currentLevel >= item.MaxLevel) return false;

        int cost = item.GetCost(currentLevel);

        return CoinsManager.Instance.HasReachedCoins(cost);
    }

    public void Buy(SO_PowerUpItem item)
    {
        if (!CanBuy(item)) return;

        int currentLevel = PowerUpManager.Instance.GetLevel(item);

        int cost = item.GetCost(currentLevel);
        CoinsManager.Instance.SetCoins(CoinsManager.Instance.TotalCoins - cost);

        PowerUpManager.Instance.UpgradePowerUp(item);
        if (currentLevel == 0)
        {
            InventoryManager.Instance.AddItem(item);
        }

        OnItemPurchased?.Invoke();
    }
}