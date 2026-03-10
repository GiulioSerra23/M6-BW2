
using UnityEngine;

public class PoolManager : GenericSingleton<PoolManager>
{
    [SerializeField] private PoolEntry[] _pools;

    public ObjectPool GetPool(PoolType poolType)
    {
        foreach (var poolEntry in _pools)
        {
            if (poolEntry.PoolType == poolType) return poolEntry.Pool;
        }
        return null;
    }
}
