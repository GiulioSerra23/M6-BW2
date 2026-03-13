
using UnityEngine;

public enum PoolType
{
    TILE_ONE_TRAIN_POOL, // nomi provvisori
    TILE_TWO_TRAIN_POOL,
    PICKUP_COIN_POOL,
}

[System.Serializable]
public class PoolEntry
{
    [SerializeField] private PoolType _poolType;
    [SerializeField] private ObjectPool _pool;

    public PoolType PoolType => _poolType;
    public ObjectPool Pool => _pool;
}
