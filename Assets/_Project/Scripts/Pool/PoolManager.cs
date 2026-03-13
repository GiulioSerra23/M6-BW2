
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : GenericSingleton<PoolManager>
{
    [SerializeField] private PoolEntry[] _pools;

    private Dictionary<PoolType, ObjectPool> _poolDictionary;

    protected override void Awake()
    {
        base.Awake();

        _poolDictionary = new Dictionary<PoolType, ObjectPool>();
        MapDictionary();
    }

    private void MapDictionary()
    {
        foreach (var entry in _pools)
        {
            _poolDictionary.TryAdd(entry.PoolType, entry.Pool);
        }
    }

    public ObjectPool GetPool(PoolType poolType)
    {
        if (_poolDictionary.TryGetValue(poolType, out var pool)) return pool;
        return null;
    }
}
