using System;
using System.Collections.Generic;

public class PowerUpManager : GenericSingleton<PowerUpManager>
{
    private Dictionary<ObjectID, int> _powerUpLevels = new();

    public event Action OnPowerUpsUpgraded;

    public int GetLevel(SO_PowerUpItem item)
    {
        if (_powerUpLevels.TryGetValue(item.ID, out var level)) return level;

        return 1;
    }

    public bool CanUpgrade(SO_PowerUpItem item)
    {
        int current = GetLevel(item);

        return current < item.MaxLevel;
    }

    public void UpgradePowerUp(SO_PowerUpItem item)
    {
        if (!CanUpgrade(item)) return;

        int current = GetLevel(item);

        _powerUpLevels[item.ID] = current + 1;

        OnPowerUpsUpgraded?.Invoke();
    }

    public void SetLevel(SO_PowerUpItem item, int level)
    {
        _powerUpLevels[item.ID] = level;
    }

    public Dictionary<ObjectID, int> GetAllPowerUpLevels()
    {
        return _powerUpLevels;
    }
}
