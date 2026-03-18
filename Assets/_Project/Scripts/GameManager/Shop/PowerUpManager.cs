using System;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : GenericSingleton<PowerUpManager>
{
    [Header ("PowerUps Database")]
    [SerializeField] private List<SO_PowerUpItem> _allPowerUps;

    private Dictionary<ObjectID, int> _powerUpLevels = new();
    private Dictionary<ObjectID, SO_PowerUpItem> _lookup = new();

    public event Action OnPowerUpsUpgraded;

    protected override void Awake()
    {
        base.Awake();
        BuildLookup();
    }

    #region UPGRADE
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
    #endregion

    #region LOOKUP
    private void BuildLookup()
    {
        _lookup.Clear();

        foreach (var item in _allPowerUps)
        {
            if (item == null) continue;

            _lookup.TryAdd(item.ID, item);
        }
    }

    public SO_PowerUpItem GetItem(ObjectID id)
    {
        return _lookup.TryGetValue(id, out var item) ? item : null;
    }

    public List<SO_PowerUpItem> GetAllItems()
    {
        return _allPowerUps;
    }
    #endregion

    #region LEVEL
    public int GetLevel(SO_PowerUpItem item)
    {
        if (_powerUpLevels.TryGetValue(item.ID, out var level)) return level;

        return 0;
    }

    public void SetLevel(SO_PowerUpItem item, int level)
    {
        _powerUpLevels[item.ID] = level;
    }

    public void SetLevels(Dictionary<ObjectID, int> levels)
    {
        _powerUpLevels = new Dictionary<ObjectID, int>(levels);
    }

    public Dictionary<ObjectID, int> GetAllPowerUpLevels()
    {
        return new Dictionary<ObjectID, int>(_powerUpLevels);
    }
    #endregion
}